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
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(PortDataReceivedEvent); //串口数据接收事件 
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;                                   //
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(620, 160);
        }
        ProgressBar[] GetProgressBar()
        {
            return new ProgressBar[] { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5, 
                                       progressBar6, progressBar7, progressBar8, progressBar9, progressBar10,
            };//返回一个对象数组
        }

        private void PortDataReceivedEvent(object sender, SerialDataReceivedEventArgs args) //串口数据接收
        {
            ProgressBar[] MyProgressBar = GetProgressBar();
            byte[] Data = new byte[serialPort1.BytesToRead];
            serialPort1.Read(Data, 0, Data.Length);//读
            foreach (byte MyData in Data)
            {
                for (int i = 1; i < 10; i++)
                {
                    MyProgressBar[10 - i].Value = MyProgressBar[10 - i - 1].Value;
                }
                progressBar1.Value = (int)MyData;
         
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)//判断
            {
                groupBox2.Visible = false; //第二个框框不可见
                this.Size = new Size(620, 160); //缩小
                serialPort1.Close();//此处可不加try，catch
                ProgressBar[] MyProgeressBar = GetProgressBar();
                foreach (ProgressBar theBar in MyProgeressBar)//遍历，每个progressbar归零
                {
                    theBar.Value = 0;
                }
                button1.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;//串口号
                    serialPort1.Open(); //打开
                    groupBox2.Visible = true; //第二个框框可见
                    this.Size = new Size(620, 436); //放大
                    button1.Text = "关闭串口";//按键标题
                }
                catch
                {
                    MessageBox.Show("串口打开错误", "错误");
                }
            }

        }

    }
}
