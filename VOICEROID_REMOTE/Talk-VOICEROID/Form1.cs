using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Talk_VOICEROID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program.GetVoiceroid2hWnd() == IntPtr.Zero) { MessageBox.Show("Could not find VOICEROID2 Editor"); Application.Exit(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox1.Text == "") { MessageBox.Show("No Message!"); return; }
            string chara = comboBox1.Text;
            string text = textBox1.Text;
            Task.Run(() => Program.talk(chara,text));
        }
    }
}
