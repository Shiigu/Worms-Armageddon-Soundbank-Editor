
namespace Worms_Soundbank_Editor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSoundbankList = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDeleteBank = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lvSoundbankSounds = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnDeleteSound = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSoundDescription = new System.Windows.Forms.TextBox();
            this.chkShowUnusedSounds = new System.Windows.Forms.CheckBox();
            this.tpCheckbox = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scheme";
            // 
            // cmbSoundbankList
            // 
            this.cmbSoundbankList.FormattingEnabled = true;
            this.cmbSoundbankList.Location = new System.Drawing.Point(220, 22);
            this.cmbSoundbankList.Name = "cmbSoundbankList";
            this.cmbSoundbankList.Size = new System.Drawing.Size(241, 28);
            this.cmbSoundbankList.TabIndex = 1;
            this.cmbSoundbankList.SelectedIndexChanged += new System.EventHandler(this.cmbSoundbankList_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(70, 61);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(151, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteBank
            // 
            this.btnDeleteBank.Enabled = false;
            this.btnDeleteBank.Location = new System.Drawing.Point(384, 61);
            this.btnDeleteBank.Name = "btnDeleteBank";
            this.btnDeleteBank.Size = new System.Drawing.Size(151, 32);
            this.btnDeleteBank.TabIndex = 3;
            this.btnDeleteBank.Text = "Delete";
            this.btnDeleteBank.UseVisualStyleBackColor = true;
            this.btnDeleteBank.Click += new System.EventHandler(this.btnDeleteBank_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Soundbank sounds";
            // 
            // lvSoundbankSounds
            // 
            this.lvSoundbankSounds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvSoundbankSounds.FullRowSelect = true;
            this.lvSoundbankSounds.GridLines = true;
            this.lvSoundbankSounds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSoundbankSounds.HideSelection = false;
            this.lvSoundbankSounds.Location = new System.Drawing.Point(16, 139);
            this.lvSoundbankSounds.Name = "lvSoundbankSounds";
            this.lvSoundbankSounds.ShowItemToolTips = true;
            this.lvSoundbankSounds.Size = new System.Drawing.Size(451, 334);
            this.lvSoundbankSounds.TabIndex = 5;
            this.lvSoundbankSounds.UseCompatibleStateImageBehavior = false;
            this.lvSoundbankSounds.View = System.Windows.Forms.View.Details;
            this.lvSoundbankSounds.SelectedIndexChanged += new System.EventHandler(this.lvSoundbankSounds_SelectedIndexChanged);
            this.lvSoundbankSounds.DoubleClick += new System.EventHandler(this.lvSoundbankSounds_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 165;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 65;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(484, 223);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(151, 32);
            this.btnPlay.TabIndex = 7;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnSet
            // 
            this.btnSet.Enabled = false;
            this.btnSet.Location = new System.Drawing.Point(484, 261);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(151, 32);
            this.btnSet.TabIndex = 8;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnDeleteSound
            // 
            this.btnDeleteSound.Enabled = false;
            this.btnDeleteSound.Location = new System.Drawing.Point(484, 299);
            this.btnDeleteSound.Name = "btnDeleteSound";
            this.btnDeleteSound.Size = new System.Drawing.Size(151, 32);
            this.btnDeleteSound.TabIndex = 9;
            this.btnDeleteSound.Text = "Delete";
            this.btnDeleteSound.UseVisualStyleBackColor = true;
            this.btnDeleteSound.Click += new System.EventHandler(this.btnDeleteSound_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(484, 441);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(151, 32);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Quit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(227, 61);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(151, 32);
            this.btnSaveAs.TabIndex = 11;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveAs);
            this.panel1.Controls.Add(this.btnDeleteBank);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.cmbSoundbankList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-8, -9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 109);
            this.panel1.TabIndex = 12;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSoundDescription);
            this.panel2.Location = new System.Drawing.Point(-5, 484);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 73);
            this.panel2.TabIndex = 13;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // txtSoundDescription
            // 
            this.txtSoundDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtSoundDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSoundDescription.Location = new System.Drawing.Point(21, 6);
            this.txtSoundDescription.Multiline = true;
            this.txtSoundDescription.Name = "txtSoundDescription";
            this.txtSoundDescription.Size = new System.Drawing.Size(601, 47);
            this.txtSoundDescription.TabIndex = 7;
            // 
            // chkShowUnusedSounds
            // 
            this.chkShowUnusedSounds.Location = new System.Drawing.Point(484, 139);
            this.chkShowUnusedSounds.Name = "chkShowUnusedSounds";
            this.chkShowUnusedSounds.Size = new System.Drawing.Size(151, 46);
            this.chkShowUnusedSounds.TabIndex = 14;
            this.chkShowUnusedSounds.Text = "Show unused\r\nsounds";
            this.chkShowUnusedSounds.UseVisualStyleBackColor = true;
            this.chkShowUnusedSounds.CheckedChanged += new System.EventHandler(this.chkShowUnusedSounds_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 544);
            this.Controls.Add(this.chkShowUnusedSounds);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDeleteSound);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lvSoundbankSounds);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Worms Soundbank Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSoundbankList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDeleteBank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvSoundbankSounds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnDeleteSound;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSoundDescription;
        private System.Windows.Forms.CheckBox chkShowUnusedSounds;
        private System.Windows.Forms.ToolTip tpCheckbox;
    }
}

