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
        int count=0;
        int time;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i;
            label3.Text = "0秒";
            for (i = 1; i < 100; i++)
            {
                comboBox1.Items.Add (i.ToString() + " 秒");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            label3.Text = ((time - count).ToString()) + "秒";
            progressBar1.Value = count;
            if (time == count)
            {
                timer1.Stop();
                System.Media.SystemSounds.Asterisk.Play();
                MessageBox.Show("时间到");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = comboBox1.Text;
            string data = str.Substring(0, 2);
            time = Convert.ToInt16(data);
            progressBar1.Maximum = time;
            timer1.Start();

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
