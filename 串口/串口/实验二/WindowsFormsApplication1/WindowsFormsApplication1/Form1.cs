using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 20; i++)
            {
                comboBox1.Items.Add("COM"+i.ToString());
            }
            comboBox1.Text = "COM1";
            comboBox2.Text = "4800";
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(!radioButton3.Checked)
            {
                string str = serialPort1.ReadExisting();//以字符形式接受
                textBox1.AppendText(str);

            }
            else
            {
                byte data;
                data = (byte)serialPort1.ReadByte();
                string str = Convert.ToString(data, 10).ToUpper();
                textBox1.AppendText(" "+(str.Length==1?"."+str:str )+"");

            }
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.Open();
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch {
                MessageBox.Show("端口错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch(Exception err)
            {

            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] data =new  byte[1];
            if (serialPort1.IsOpen)
            {
                if (textBox2.Text != "")
                {
                    if (!radioButton1.Checked)
                    {
                        try
                        {
                            serialPort1.WriteLine(textBox2.Text);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("串口读入失败");
                            serialPort1.Close();
                            button1.Enabled = true;
                            button2.Enabled = false;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < (textBox2.Text.Length -textBox2.Text.Length % 2) / 2; i++)
                        {
                            data[0] = Convert.ToByte(textBox2.Text.Substring(i*2,2),16);
                            serialPort1.Write(data,0,1);
                        }
                        if (textBox2.Text.Length % 2 != 0)
                        {
                            data[0] = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length-1, 1),16);
                            serialPort1.Write(data, 0, 1);
                        }
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
