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
    public delegate void port_DataReceived(object sender, SerialDataReceivedEventArgs e);
    public delegate void HideWindow();
    public delegate void OpenPort();
    public delegate void ClosePort();
    public delegate Point GetMainPos();
    public delegate int GetMainWidth();

 
    public partial class Form1 : Form
    {
        string st;
        Form3 access;
        Form2 displayer;
        const byte Open1 = 0x01;
        const byte Close1 = 0x81;

        const byte Open2 = 0x02;
        const byte Close2 = 0x82;

        const byte Open3 = 0x03;
        const byte Close3 = 0x83;
        public Form1()
        {
            InitializeComponent();
            serialPort1.Encoding = Encoding.GetEncoding("GB2312");
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);      //串口数据接收事件
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            button9.Enabled = false;
            button6.Enabled = false;
            button8.Enabled = false;
            
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
            comboBox2.Text = "9600";
        }
        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
            int i=0;
            if (radioButton4.Checked)
            {
               
                textBox1.AppendText(serialPort1.ReadExisting());
                string str = serialPort1.ReadExisting();
                Class1.datas = str ;
            }
            else if (radioButton3.Checked)
            {
                byte[] data = new byte[serialPort1.BytesToRead];                                //定义缓冲区，因为串口事件触发时有可能收到不止一个字节
                serialPort1.Read(data, 0, data.Length);
               // if (displayer != null)
                //    displayer.AddData(data);
                foreach (byte Member in data)                                                   //遍历用法
                {
                    string str = Convert.ToString(Member, 16).ToUpper();
                    Class1.datas = str;
                    textBox1.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + " ");
                }
            }
            else
            {
              //  serialPort1.r
                byte[] data = new byte[serialPort1.BytesToRead];                                //定义缓冲区，因为串口事件触发时有可能收到不止一个字节
                serialPort1.Read(data, 0, data.Length);
                byte data1;
                int t = data.Length;
                int[] Member = new int[t];
               for (int j = 0; j<2;j++ )
               //  foreach (byte Member in data)                                                   //遍历用法
                {
                
                 //   Member[j]= data[j];
                    //   string str = Convert.ToString(Member, 10).ToUpper();
                    //  Class1.datas = str;
                 //  int k = 0;
                 //  k = j%2;
                   Member[j] = data[j];
                   textBox1.AppendText(" " + Member[j]);
                   Class1.datas = Convert.ToString(Member[0]);
                   Class1.datat = Convert.ToString(Member[1]);
                   data1=data[1];
                  
                        switch (Member[1])
                        {
                            case 0: st = "无"; break;
                            case 1: st = "有"; break;
                            default: st = "  "; break;
                        }
                      
                         switch (Member[0])
                         {
                             case 2: ; textBox3.Text = st + "光照"; if (displayer != null) displayer.AddData(data1); break;
                             case 3: textBox4.Text = st + "障碍"; if (displayer != null) displayer.AddData2(data1); break;
                             case 12: textBox5.Text = st + "振动"; if (displayer != null) displayer.AddData3(data1); break;
                            // default: textBox3.Text = ""; break;
                         }
                

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

        private void button7_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Open3);
            button8.Enabled = true;
            button7.Enabled = false;

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

        private void button10_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Open1);
            button9.Enabled = true;
            button10.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Close1);
            button10.Enabled = true;
            button9.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Open2);
            button6.Enabled = true;
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Close2);
            button6.Enabled = false;
            button5.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(Close3);
            button8.Enabled = false;
            button7.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            access=new Form3();
            access.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
