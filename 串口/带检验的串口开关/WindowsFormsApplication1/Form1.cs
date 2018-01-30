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
        byte DataSended = 0;
        byte[] DataTosend = new byte[] {0x01,0x02,0x03 };
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);       //添加串口中断事件
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button MyButton = (Button)sender;
            DataSended = Convert.ToByte(MyButton.Tag);
            SendDataToSerialPort(serialPort1, DataTosend[DataSended - 1]);
        }

        private void SendDataToSerialPort(SerialPort MyPort, byte DataToSend)                            //单字节发送数据    
        {
            byte[] DatasToWrite = new byte[] { DataToSend };                                               //数据包
            if (serialPort1.IsOpen)
            {
                try
                {
                    MyPort.Write(DatasToWrite, 0, 1);                                                  //发数据
                    timer1.Interval = 3 * 1000;                                                        //设定超时时间
                    timer1.Start();                                                                    //定时器
                }
                catch
                {
                    MessageBox.Show("串口数据写入错误", "错误");
                }
            }
        }

        private void SetOvlShape(int which)                                                               //填充颜色   
        {
            switch (which)
            {
                case 1:
                    ovalShape1.FillColor = Color.Green;
                    ovalShape2.FillColor = Color.Red;
                    ovalShape3.FillColor = Color.Red;
                    break;
                case 2:
                    ovalShape1.FillColor = Color.Red;
                    ovalShape2.FillColor = Color.Green;
                    ovalShape3.FillColor = Color.Red;
                    break;
                case 3:
                    ovalShape1.FillColor = Color.Red;
                    ovalShape2.FillColor = Color.Red;
                    ovalShape3.FillColor = Color.Green;
                    break;
                case 4:
                    ovalShape1.FillColor = Color.Green;
                    ovalShape2.FillColor = Color.Green;
                    ovalShape3.FillColor = Color.Green;
                    break;
                default:
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)                                          //打开/关闭串口
        {
            if (serialPort1.IsOpen)                                                                     //一堆处理……
            {
                try
                {
                    serialPort1.Close();
                }
                catch
                {

                }
                button1.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;                                              //串口号    
                    serialPort1.Open();                                                                 //打开
                }
                catch
                {
                    MessageBox.Show("串口打开错误，请检查", "串口");
                }
                button1.Text = "关闭串口";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)                                          //定时器事件
        {
            string MyStr = DataSended.ToString() + "路数据校验超时，请检查";                          //Messagebox内容 
            timer1.Stop();
            MessageBox.Show(MyStr, "错误");
        }
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte DataReceived = (byte)(~serialPort1.ReadByte());                                           //单字节读取
            try
            {
                timer1.Stop();                                                                          //关定时器
            }
            catch { }
            if (DataSended == 0)                                                                        //防止下位机乱发，不处理
                return;
            SetOvlShape(DataReceived);
            try
            {
                if (DataToSend[DataSended - 1] == DataReceived)                                            //校验数据
                {
                    MessageBox.Show("数据校验成功", "成功！");                                              //弹出提示
                }
                else
                {
                    MessageBox.Show("数据校验失败", "数据校验失败");
                }
            }
            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
