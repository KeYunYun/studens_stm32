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
    public delegate void ShowWindow();
    public delegate void HideWindow();
    public delegate void OpenPort();
    public delegate void ClosePort();
    public delegate Point GetMainPos();
    public delegate int GetMainWidth();
    public partial class Form1 : Form
    {
        Form2 displayer;
        public Form1()
        {
            InitializeComponent();
            serialPort1.Encoding = Encoding.GetEncoding("GB2312");
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);      //串口数据接收事件
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            
        }
        public void ClosePort()//关闭串口，供委托调用
        {
            try
            {
                serialPort1.Close();
            }
            catch (System.Exception)
            {

            }
        }

        private Point GetMyPos()//供委托调用
        {
            return this.Location;
        }

        public void OpenPort()//打开串口，供委托调用
        {
            try
            {
                serialPort1.Open();
            }
            catch (System.Exception)
            {
                MessageBox.Show("串口打开失败，请检查", "错误");
            }
        }

        public void ShowMe()//供委托调用
        {
            this.Show();
        }

        public void HideMe()//供委托调用
        {
            this.Hide();
        }

        int GetMyWidth()//供委托调用
        {
            return this.Width;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i;
            string[] SCIPorts;
            SCIPorts = System.IO.Ports.SerialPort.GetPortNames();
            this.comboBox1.Items.Clear();//首先将现有的项清除掉
            for (i = 0; i < SCIPorts.Length; i++)
                //向[串口选择框]中添加搜索到的串口号
                this.comboBox1.Items.Add(SCIPorts[i]);
            comboBox1.Text = "COM1";
            comboBox2.Text = "4800";
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!radioButton3.Checked)
            {
                textBox1.AppendText(serialPort1.ReadExisting());                                //串口类会自动处理汉字，所以不需要特别转换
            }
            else
            {
                byte[] data = new byte[serialPort1.BytesToRead];                                //定义缓冲区，因为串口事件触发时有可能收到不止一个字节
                serialPort1.Read(data, 0, data.Length);
                if (displayer != null)
                    displayer.AddData(data);
                foreach (byte Member in data)                                                   //遍历用法
                {
                    string str = Convert.ToString(Member, 16).ToUpper();
                    textBox1.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + " ");
                }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CreateNewDrawer()//创建ADC绘制窗口
        {
            displayer = new Form2();//创建新对象
            displayer.ShowMainWindow = new ShowWindow(ShowMe);//初始化类成员委托
            displayer.HideMainWindow = new HideWindow(HideMe);
            displayer.GetMainPos = new GetMainPos(GetMyPos);
            displayer.CloseSerialPort = new ClosePort(ClosePort);
            displayer.OpenSerialPort = new OpenPort(OpenPort);
            displayer.GetMainWidth = new GetMainWidth(GetMyWidth);
            displayer.Show();//显示窗口
        }
        private void CreateDisplayer()
        {
            this.Left = 0;
            CreateNewDrawer();
            Rectangle Rect = Screen.GetWorkingArea(this);
            displayer.SetWindow(Rect.Width - this.Width, new Point(this.Width, this.Top));//设置绘制窗口宽度，以及坐标
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (displayer == null)//第一次创建Displayer = null
            {
                CreateDisplayer();
            }
            else
            {
                if (displayer.IsDisposed)//多次创建通过判断IsDisposed确定串口是否已关闭，避免多次创建
                {
                    CreateDisplayer();
                }
            }
        }
    }
}
