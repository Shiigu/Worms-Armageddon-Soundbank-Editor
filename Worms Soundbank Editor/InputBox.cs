using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Worms_Soundbank_Editor
{
    public partial class InputBox : Form
    {
        public static InputBox ip;
        protected string Input { get; private set; }
        public InputBox()
        {
            InitializeComponent();
        }

        public InputBox(string Title = "", string Prompt = "", string DefaultInput = "", FormStartPosition pos = FormStartPosition.CenterScreen)
        {
            InitializeComponent();
            Text = Title;
            txtString.Text = DefaultInput;
            lblPrompt.Text = Prompt;
            StartPosition = pos;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Input = txtString.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Input = null;
            Close();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.KeyCode == Keys.Return ? false : e.KeyCode != Keys.Return))
            {
                btnOk_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }

        public static string Show(string Title = "", string Prompt = "", string DefaultInput = "", FormStartPosition pos = FormStartPosition.CenterScreen)
        {
            string inputText;
            InputBox inputBox = new InputBox(Title, Prompt, DefaultInput, pos);
            ip = inputBox;
            try
            {
                ip.ShowDialog();
                inputText = string.IsNullOrWhiteSpace(ip.Input) ? DefaultInput : ip.Input;
            }
            finally
            {
                if (inputBox != null)
                {
                    ((IDisposable)inputBox).Dispose();
                }
            }
            return inputText;
        }

        private void txtString_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = !string.IsNullOrEmpty(txtString.Text);
        }
    }
}
