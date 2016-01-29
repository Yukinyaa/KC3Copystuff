using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileWorker.setChromeDataDirectory(textBox1.Text);
        }

        private void Override_Click(object sender, EventArgs e)
        {
            FileWorker.overrideAppname(Username.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileWorker.restore();
            MessageBox.Show("done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileWorker.backup();
            MessageBox.Show("done");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileWorker.setChromeDataDirectory();
        }
    }
}
