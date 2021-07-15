using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Worms_Soundbank_Editor.Utils;

namespace Worms_Soundbank_Editor
{
    public partial class frmMain : Form
    {
        private List<Sound> sounds;
        private string WAPath;
        private string SpeechPath;
        private string SoundbankPath;
        private string CurrentSoundbank;
        private string TempDirectory;
        private const string SOUND_SAMPLE_TOO_LONG = "The sound sample is very long. It might interfere with other sounds during gameplay.\n\nConsider trimming the sound with a sound editor.";
        private const string DEFAULT_SOUNDBANK = @"./DATA/User/Speech/English";
        private const string NEW_SOUNDBANK = "<New Soundbank>";
        private const string EXTENSION_FILTER = "Sounds (*.wav, *.mp3, *.aiff, *.ogg)|*.wav; *.mp3; *.aiff; *.ogg";
        private const string UNSAVED_CHANGES_PROMPT = "You have some unsaved changes on your current soundbank.\nDo you want to save them?";
        private const string EXISTING_SOUNDBANK_PROMPT = "This action will overwrite the entire soundbank, including deleting sound files that are not part of the new list. This operation CANNOT be undone.\nDo you want to save them?";
        private const string DELETE_SOUNDBANK_PROMPT = "This action will delete the entire soundbank. This operation CANNOT be undone.\nDo you really want to delete this soundbank?";
        private const string ALREADY_RUNNING_MESSAGE = "Worms Soundbank Editor is already running!";
        private const string EXCEPTION_MESSAGE_BOX_TITLE = "Whoops!";
        private const string APPLICATION_NAME = "Worms Soundbank Editor";
        private const string RECTANGLE_IMAGE = "rect";
        private const string NO_IMAGE = "";
        private bool Changed = false;
        private bool ShowUnusedSounds = false;

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public frmMain()
        {
            InitializeComponent();
            var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
            var isAlreadyRunning = processes.Count() > 1;
            if (isAlreadyRunning)
            {
                MessageBox.Show(ALREADY_RUNNING_MESSAGE, EXCEPTION_MESSAGE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(0);
            }
            else if (!File.Exists($"{Path.GetDirectoryName(Application.ExecutablePath)}/../WA.exe"))
            {
                AskPath(true);
                BringProcessToFront(processes[0]);
            }
            else
            {
                WAPath = "../";
                SpeechPath = "./Speech";
            }
            tpCheckbox.SetToolTip(chkShowUnusedSounds, "When checked, sounds considered unused will show up on the list.\n\nSuch sounds will be able to be modified as a result.");
            CurrentSoundbank = NEW_SOUNDBANK;
            ofdFile.Filter = EXTENSION_FILTER;
            InitializeSoundList();
            ResetEverything();
            LoadSoundbankList(ref cmbSoundbankList);
            cmbSoundbankList.SelectedIndex = 0;
        }
        public static void BringProcessToFront(Process process)
        {
            IntPtr handle = process.MainWindowHandle;
            SetForegroundWindow(handle);
        }

        public void AskPath(bool FirstTime)
        {
            bool askingForExecutable;
            bool foundExecutable = false;
            if(File.Exists("./path"))
            {
                WAPath = File.ReadAllText("./path");
                foundExecutable = File.Exists($"{WAPath}/WA.exe");
            }
            if (!foundExecutable)
            {
                do
                {
                    WAPath = string.Empty;
                    fbdFolder.Description = "Please indicate Worms Armageddon's folder";
                    if (fbdFolder.ShowDialog() == DialogResult.OK)
                    {
                        WAPath = fbdFolder.SelectedPath;
                        if (!File.Exists(string.Concat(WAPath, "/WA.exe")))
                        {
                            MessageBox.Show("Worms Armageddon's executable file cannot be found", EXCEPTION_MESSAGE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            foundExecutable = true;
                        }
                    }
                    else if (FirstTime)
                    {
                        Environment.Exit(0);
                    }
                    askingForExecutable = !foundExecutable;
                }
                while (askingForExecutable);
                File.WriteAllText("./path", WAPath);
            }
            SpeechPath = string.Concat(WAPath, "/User/Speech");
        }

        public void LoadSoundbankList(ref ComboBox list)
        {
            list.Items.Clear();
            list.Items.Add(NEW_SOUNDBANK);
            string[] directories = Directory.GetDirectories(SpeechPath);
            for (int i = 0; i < directories.Length; i++)
            {
                string folder = directories[i];
                char[] splitChar = new char[] { '/' };
                string[] folders = folder.Split(splitChar);
                string gameFolderPath = folders[folders.Length - 1];
                splitChar = new char[] { '\\' };
                string[] soundBankFolderPath = gameFolderPath.Split(splitChar);
                string soundBank = soundBankFolderPath[soundBankFolderPath.Length - 1];
                list.Items.Add(soundBank);
            }
        }

        private void InitializeSoundList()
        {
            sounds = new List<Sound>();
            var constr = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = .\Resources\Sounds-list.xls; Extended Properties = 'Excel 8.0;HDR=Yes;IMEX=1';";
            using (var conn = new OleDbConnection(constr))
            {
                conn.Open();
                var command = new OleDbCommand("Select * from [Sounds$]", conn);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sounds.Add(new Sound(reader["File Name"].ToString(), reader["Display Name"].ToString(), reader["Description"].ToString(), bool.Parse(reader["Used"].ToString())));
                    }
                }
            }
            if (File.Exists("./Resources/Rectangle.bmp"))
            {
                var image = Image.FromFile("./Resources/Rectangle.bmp");
                var imageList = new ImageList();
                var blankImage = new Bitmap(42, 16);
                imageList.ImageSize = new Size(42, 16);
                imageList.Images.Add(key: "", image: blankImage);
                imageList.Images.Add(key: RECTANGLE_IMAGE, image: image);
                lvSoundbankSounds.SmallImageList = imageList;
            }
            FillSoundList();
        }

        private void FillSoundList()
        {
            lvSoundbankSounds.Items.Clear();
            sounds.ForEach(sound =>
            {
                if (ShowUnusedSounds || sound.Used)
                {
                    var listViewItem = new ListViewItem
                    {
                        ImageKey = null
                    };
                    listViewItem.SubItems.Add(sound.Used ? sound.DisplayName : $"{sound.DisplayName} (UNUSED)");
                    listViewItem.SubItems.Add(string.Empty);
                    listViewItem.ToolTipText = sound.Description;
                    if (!CurrentSoundbank.Equals(NEW_SOUNDBANK))
                    {
                        var soundPath = $"{SoundbankPath}/{sound.FileName}";
                        if (File.Exists(soundPath))
                        {
                            listViewItem.ImageKey = RECTANGLE_IMAGE;
                            sound.SoundPath = soundPath;
                            sound.SoundDuration = WavFileUtils.GetWavFileDuration(sound.SoundPath).TotalSeconds;
                            listViewItem.SubItems[2].Text = $"{sound.SoundDuration.ToString("0.##")} sec.";
                            listViewItem.ToolTipText = soundPath;
                        }
                    }
                    lvSoundbankSounds.Items.Add(listViewItem);
                }
            });

        }

        private Sound FindSound(string displayName)
        {
            var displayNameRegardlessOfUsed = displayName.Replace(" (UNUSED)", "");
            foreach(Sound sound in sounds)
            {
                if (sound.DisplayName.Equals(displayNameRegardlessOfUsed))
                    return sound;
            }
            return null;
        }

        private void LoadSoundbank(string bank)
        {
            if (Changed)
            {
                if (MessageBox.Show(UNSAVED_CHANGES_PROMPT, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (!string.IsNullOrWhiteSpace(CurrentSoundbank))
                        SaveSoundbank();
                    else
                        btnSaveAs_Click(null, null);
                }
            }
            ResetEverything();
            CurrentSoundbank = bank;
            SoundbankPath = string.Concat(SpeechPath, "/", bank);
            cmbSoundbankList.Text = bank;
            FillSoundList();
        }

        private void SaveSoundbank()
        {
            var newDirectory = $"{SpeechPath}/{CurrentSoundbank}";
            if (!Directory.Exists(newDirectory))
               Directory.CreateDirectory(newDirectory);
            sounds.ForEach(sound =>
            {
                if(!sound.SoundPath.Equals($"{newDirectory}/{sound.FileName}"))
                    File.Delete($"{newDirectory}/{sound.FileName}");
                if(!string.IsNullOrEmpty(sound.SoundPath))
                {
                    File.Move(sound.SoundPath, $"{newDirectory}/{sound.FileName}");
                    sound.SoundPath = $"{newDirectory}/{sound.FileName}";
                    var soundItem = lvSoundbankSounds.FindItemWithText(sound.DisplayName, true, 0);
                    soundItem.ToolTipText = sound.SoundPath;
                }
            });
            if (Directory.Exists(TempDirectory))
                Directory.Delete(TempDirectory, true);
            var itemList = new List<string>();
            foreach (object item in cmbSoundbankList.Items)
                if(!item.ToString().Equals(NEW_SOUNDBANK))
                    itemList.Add(item.ToString());
            itemList.Add(CurrentSoundbank);
            itemList.Sort();
            itemList.Insert(0, NEW_SOUNDBANK);
            cmbSoundbankList.Items.Clear();
            itemList.ForEach(item => cmbSoundbankList.Items.Add(item));
            cmbSoundbankList.Text = CurrentSoundbank;
            MessageBox.Show("Saved successfully!", APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSave.Enabled = true;
            btnDeleteBank.Enabled = true;
            Changed = false;
        }

        private void PlayCurrentSound()
        {
            var selectedItem = lvSoundbankSounds.SelectedItems.Count > 0 ? lvSoundbankSounds.SelectedItems[0] : null;
            if (selectedItem != null)
            {
                var selectedSound = FindSound(selectedItem.SubItems[1].Text);
                if (selectedSound != null)
                {
                    if (File.Exists(selectedSound.SoundPath))
                        WavFileUtils.PlaySound(selectedSound.SoundPath);
                    else
                        WavFileUtils.PlaySound($"{WAPath}/{DEFAULT_SOUNDBANK}/{selectedSound.FileName}");
                }
            }
        }

        private void ResetEverything()
        {
            Changed = false;
            foreach (ListViewItem item in lvSoundbankSounds.Items)
            {
                item.Selected = false;
                item.ImageKey = "NO_IMAGE";
                var selectedSound = FindSound(item.SubItems[1].Text);
                if (selectedSound != null)
                {
                    item.ToolTipText = selectedSound.Description;
                }
                item.SubItems[0].Text = string.Empty;
                item.SubItems[2].Text = string.Empty;
            }
            sounds.ForEach(sound =>
            {
                sound.SoundPath = string.Empty;
                sound.SoundDuration = 0;
            });
            CurrentSoundbank = string.Empty;
            SoundbankPath = string.Empty;
            if (Directory.Exists(TempDirectory))
                Directory.Delete(TempDirectory);
            btnDeleteSound.Enabled = false;
        }

        private void lvSoundbankSounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = lvSoundbankSounds.SelectedItems.Count > 0 ? lvSoundbankSounds.SelectedItems[0] : null;
            if (selectedItem != null)
            {
                var selectedSound = FindSound(selectedItem.SubItems[1].Text);
                if (selectedSound != null)
                {
                    txtSoundDescription.Text = selectedSound.Description;
                    btnPlay.Enabled = true;
                    btnSet.Enabled = true;
                    btnDeleteSound.Enabled = !string.IsNullOrWhiteSpace(selectedSound.SoundPath);
                }
                else
                    txtSoundDescription.Text = "";
            }
            else
                txtSoundDescription.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Changed)
            {
                if (MessageBox.Show(UNSAVED_CHANGES_PROMPT, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (!string.IsNullOrWhiteSpace(CurrentSoundbank))
                        SaveSoundbank();
                    else
                        btnSaveAs_Click(null, null);
                }
            }
            Application.Exit();
        }

        private void lvSoundbankSounds_DoubleClick(object sender, EventArgs e)
        {
            PlayCurrentSound();
        }

        private void cmbSoundbankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentSoundbank != cmbSoundbankList.SelectedItem.ToString())
            {
                if (!cmbSoundbankList.SelectedItem.ToString().Equals(NEW_SOUNDBANK))
                {
                    CurrentSoundbank = cmbSoundbankList.SelectedItem.ToString();
                    LoadSoundbank(CurrentSoundbank);
                    btnSave.Enabled = true;
                    btnDeleteBank.Enabled = true;
                }
                else
                {
                    ResetEverything();
                    CurrentSoundbank = "";
                    btnSave.Enabled = false;
                    btnDeleteBank.Enabled = false;
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            var selectedItem = lvSoundbankSounds.SelectedItems.Count > 0 ? lvSoundbankSounds.SelectedItems[0] : null;
            if (selectedItem != null)
            {
                var selectedSound = FindSound(selectedItem.SubItems[1].Text);
                if (selectedSound != null)
                {
                    if (ofdFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            TempDirectory = !cmbSoundbankList.Text.Equals(NEW_SOUNDBANK) ? $"{SoundbankPath}/TEMP" : $"{SpeechPath}/TEMP";
                            if (!Directory.Exists(TempDirectory))
                            {
                                var di = Directory.CreateDirectory(TempDirectory);
                                di.Attributes |= FileAttributes.Hidden;
                            }
                            var tempSoundPath = $"{TempDirectory}/{selectedSound.FileName}";
                            if (File.Exists(tempSoundPath))
                                File.Delete(tempSoundPath);
                            if (selectedSound.FileName.Equals("COLLECT.WAV") && MessageBox.Show("Do you wish for the application to include the Crate Pickup at the start of the sound effect?", APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                var placeholderPath = $"{TempDirectory}/placeholder_{selectedSound.FileName}";
                                WavFileUtils.ConvertToWav(ofdFile.FileName, placeholderPath);
                                WavFileUtils.To16Bit(placeholderPath);
                                WavFileUtils.Concatenate(tempSoundPath, new string[] { "./Resources/COLLECT-SOUND.WAV", $"{TempDirectory}/placeholder_{selectedSound.FileName}" });
                                File.Delete($"{TempDirectory}/placeholder_{selectedSound.FileName}");
                            }
                            else
                            {
                                WavFileUtils.ConvertToWav(ofdFile.FileName, tempSoundPath);
                                WavFileUtils.To16Bit(tempSoundPath);
                            }
                            Changed = true;
                            selectedItem.ImageKey = RECTANGLE_IMAGE;
                            selectedSound.SoundPath = tempSoundPath;
                            selectedSound.SoundDuration = WavFileUtils.GetWavFileDuration(selectedSound.SoundPath).TotalSeconds;
                            selectedItem.ToolTipText = ofdFile.FileName;
                            selectedItem.SubItems[2].Text = $"{selectedSound.SoundDuration:0.##} sec.";
                            btnDeleteSound.Enabled = true;
                            WavFileUtils.PlaySound(selectedSound.SoundPath);
                            if (selectedSound.SoundDuration > 4)
                                MessageBox.Show(SOUND_SAMPLE_TOO_LONG, APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, EXCEPTION_MESSAGE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayCurrentSound();
        }

        private void btnDeleteSound_Click(object sender, EventArgs e)
        {
            var selectedItem = lvSoundbankSounds.SelectedItems.Count > 0 ? lvSoundbankSounds.SelectedItems[0] : null;
            if (selectedItem != null)
            {
                var selectedSound = FindSound(selectedItem.SubItems[1].Text);
                if (selectedSound != null)
                {
                    try
                    {
                        selectedItem.ImageKey = "NO_IMAGE";
                        selectedItem.ToolTipText = selectedSound.Description;
                        selectedItem.SubItems[0].Text = "";
                        selectedItem.SubItems[2].Text = "";
                        selectedSound.SoundPath = "";
                        selectedSound.SoundDuration = 0;
                        btnDeleteSound.Enabled = false;
                        Changed = true;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, EXCEPTION_MESSAGE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSoundbank();
        }

        private void btnDeleteBank_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(DELETE_SOUNDBANK_PROMPT, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                cmbSoundbankList.SelectedIndex = 0;
                Directory.Delete(SoundbankPath, true);
                ResetEverything();
                btnSave.Enabled = false;
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            var soundbankName = InputBox.Show("New character", "Write the name of the character you want to create", "", FormStartPosition.CenterScreen);
            if(soundbankName != null)
            {
                var soundbankAlreadyExists = CurrentSoundbank.Equals(soundbankName, StringComparison.InvariantCultureIgnoreCase);
                DialogResult overwritePrompt = DialogResult.None;
                if(soundbankAlreadyExists)
                    overwritePrompt = MessageBox.Show(UNSAVED_CHANGES_PROMPT, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(!soundbankAlreadyExists || overwritePrompt == DialogResult.Yes)
                {
                    if(soundbankAlreadyExists)
                        Directory.Delete(SoundbankPath, true);
                    CurrentSoundbank = soundbankName;
                    SaveSoundbank();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
                       Color.Black, 1, ButtonBorderStyle.Dotted,
                       Color.Black, 1, ButtonBorderStyle.Dotted,
                       Color.Black, 1, ButtonBorderStyle.Dotted,
                       Color.Black, 1, ButtonBorderStyle.Dotted);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel2.ClientRectangle,
                       Color.Black, 1, ButtonBorderStyle.Dotted, // left
                       Color.Black, 1, ButtonBorderStyle.Dotted, // top
                       Color.Black, 1, ButtonBorderStyle.Dotted, // right
                       Color.Black, 1, ButtonBorderStyle.Dotted);// bottom
        }

        private void chkShowUnusedSounds_CheckedChanged(object sender, EventArgs e)
        {
            ShowUnusedSounds = chkShowUnusedSounds.Checked;
            FillSoundList();
        }
    }
}
