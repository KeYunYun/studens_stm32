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


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        const byte Open1 = 0x01;
        const byte Close1 = 0x81;

        const byte Open2 = 0x02;
        const byte Close2 =0x82;

        const byte Open3 = 0x03;
        const byte Close3 =0x83;

        byte[] SerialPortDataBuffer = new byte[1];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ovalShape1.FillColor = Color.Gray;
            SerchAndAddSerialToComBox( serialPort1,comboBox1 );

        }
        private void   SerchAndAddSerialToComBox(SerialPort MyPort,ComboBox MyBox )
        {
           // string [] MyString =new string [20];
            string Buffer;
            MyBox.Items.Clear();
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();
                  //  MyString[i - 1] = Buffer;
                    MyBox.Items.Add(Buffer);
                    MyPort.Close();
                }
                catch
                {
 
                }
            }
            //MyBox.Text=MyString[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                ovalShape1.FillColor = Color.Gray;
                try
                {
                    serialPort1.Close();

                }
                catch {
                    MessageBox.Show("串口关闭失败");
                }
                button2.Text = "打开串口";
            }
            else
            {
                ovalShape1.FillColor = Color.Green;
                try {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button2.Text="关闭串口";
                }
                catch
                {
                    MessageBox.Show("串口打开失败");
                }
            }

        }

        private void WriteByteToSerialPort(byte data)
        {
            byte[] Buffer = new byte[] { data };
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write(Buffer, 0, 1);    
                }
                catch
                {
                    MessageBox.Show("串口数据发送出错");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerchAndAddSerialToComBox(serialPort1, comboBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Open1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Close1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Close2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Open2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Open3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Close3);
        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {

        }
    }
}
