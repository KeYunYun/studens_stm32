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


        delegate void handleinterfaceupdatedelegate(Object textbox,
                                                            string text);

        //串口默认情况
        private string msg = "无校验,8位数据位,1位停止位(No parity,8 data " +
                            "bits,1 stop bit)";
        private string str = "串口号(Serial Port Number)、波特率(Baud Rate):";

        SCI sci = new SCI();    //要调用SCI类中所定义的函数
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button1.Text = "打开串口";
            this.comboBox1.Enabled = true;    //[波特率选择框]处于可用状态
            this.comboBox2.Enabled = true;　//[串口选择框]处于可用状态

            //自动搜索串口,并将其加入到[串口选择框]中
            int i;
            string[] SCIPorts;
            SCIPorts = System.IO.Ports.SerialPort.GetPortNames();
            this.comboBox1.Items.Clear();//首先将现有的项清除掉
            for (i = 0; i < SCIPorts.Length; i++)
                //向[串口选择框]中添加搜索到的串口号
                this.comboBox1.Items.Add(SCIPorts[i]);

            //设置各组合框的初始显示值
            if (SCIPorts.Length != 0)
            {
                this.button1.Enabled = true;
                this.comboBox1.SelectedIndex = 0;
                this.comboBox2.SelectedIndex = 0;
               // this.CbSCISendType.SelectedIndex = 0;

                //设置初始的串口号与波特率
                PublicVar.g_SCIComNum = this.comboBox1.Text;
                PublicVar.g_SCIBaudRate = int.Parse(this.comboBox2.Text);
                //显示当前串口信与状态信息
                this.textBox1.Text = str + PublicVar.g_SCIComNum + "、" +
                                   PublicVar.g_SCIBaudRate + "\n" + msg;
              //  this.TSSLState.Text = "无操作,请先选择波特率与串口号,打开串口," +
              //                   "然后发送数据";
            }
            else
            {
                this.textBox1.Text = "没有可用的串口,请检查!";
                this.button1.Enabled = false;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PublicVar.g_SCIComNum = this.comboBox1.Text;
            this.textBox1.Text = "过程提示:选择串口号";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PublicVar.g_SCIBaudRate = int.Parse(this.comboBox2.Text);
            this.textBox1.Text = "过程提示:选择波特率";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Flag;//标记打开是否成功
            //根据按钮BtnSCISwitch显示内容执行打开或关闭串口操作
            if (this.button1.Text.CompareTo("打开串口") == 0)
            {
                //提示当前正在执行打开串口操作
                this.textBox1.Text = "过程提示:正在打开串口...";
                //进行串口的初始化,并用Flag返回结果
                Flag = sci.SCIInit(serialPort1, PublicVar.g_SCIComNum,
                            PublicVar.g_SCIBaudRate);

                if (Flag == true)//串口打开成功
                {
                    //显示打开串口相关信息
                    this.textBox1.Text = str + PublicVar.g_SCIComNum +
                        "、" + PublicVar.g_SCIBaudRate + "\n" + msg;

                    this.button1.Text = "关闭串口";
                    //[串口选择框]处于禁用状态
                    this.comboBox1.Enabled = false;
                    //[波特率选择框]处于禁用状态
                    this.comboBox2.Enabled = false;
                    //状态上显示结果信息
                    this.textBox1.Text =  "打开" + PublicVar.g_SCIComNum + "成功!" + "波特率选择：" + PublicVar.g_SCIBaudRate;
                  //  this.pictureBox1.Image = SerialPort.Properties.Resources.Run;
                }
                else//串口打开失败
                {
                    this.textBox1.Text =  "打开" + PublicVar.g_SCIComNum + "失败!";
                  //  this.pictureBox1.Image = SerialPort.Properties.Resources.Run_static;
                }
            }
            else if (this.button1.Text == "关闭串口")
            {
                //提示当前操作
                this.textBox1.Text = "过程提示:正在关闭串口...";
                //执行关闭串口操作,并用Flag返回结果
                Flag = sci.SCIClose(this.serialPort1);
                if (Flag == true)
                {
                    this.textBox1.Text = str + PublicVar.g_SCIComNum
                             + "、" + PublicVar.g_SCIBaudRate + "\n" + msg;
                    this.button1.Text = "打开串口";
                    //[串口选择框]处于可用状态
                    this.comboBox1.Enabled = true;
                    //[波特率选择框]处于可用状态
                    this.comboBox2.Enabled = true;
                    this.textBox1.Text += "关闭" + PublicVar.g_SCIComNum + "成功!";
                   // this.pictureBox1.Image = SerialPort.Properties.Resources.Run_static;
                }
                else//串口关闭失败
                {
                    this.textBox1.Text += "关闭" + PublicVar.g_SCIComNum + "失败!";
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "过程提示:选择数据发送方式!";
            //选择某一种发送方式时,首先清空发送框
            this.textBox2.Text = string.Empty;

            //选择的是发送字符串
            if (comboBox3.SelectedIndex == 0)
            {
                this.textBox1.Text = "请输入字符串!";
                //this.TSSLState.Text = "你选择了发送字符串!";
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                this.textBox1.Text = "请输入十进制数,以逗号(英文输入法下)隔开"
                                    + ",数据范围0-255,允许用退格键";
                //this.TSSLState.Text = "你选择了发送十进制数!";
            }
            else
            {
                this.textBox1.Text = "请输入十六进制数,以逗号(英文输入法下)隔"
                                    + "开,数据范围00-FF,允许用退格键";
               // this.TSSLState.Text = "你选择了发送十六进制数! ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "过程提示: 执行发送数据...";

            bool Flag;//判断数据发送是否成功
            int i, count = 0;//len保存发送数据的长度
            int len;

            //0表示选择是字符发送,1表示的是十进制发送,2表示十六进制发送
            int SendType;
            SendType = comboBox3.SelectedIndex;

            //定义一个ArrayList类的实例对象,实现一个数组,其大小在添加元
            //素时自动变化
            System.Collections.ArrayList SendData = new System.Collections.ArrayList()
                ;

            //如果串口没有打开
            if (!serialPort1.IsOpen)
            {
                //状态条进行提示
                this.textBox1.Text += "请先打开串口!";
                return;
            }
            //如果发送数据为空
            if (this.textBox2.Text == string.Empty)
            {
                this.textBox1.Text += "发送数据不得为空!";
                return;
            }

            if (SendType == 0)//选择的是以字符串方式发送
            {
                this.textBox1.Text = "以字符串方式发送数据!";
                //将要发送的数据进行编码,并获取编码后的数据长度
                len = System.Text.Encoding.Default.GetBytes(this.textBox2.Text).Length;
                //sci.SCIReceInt(SCIPort,len);//设置产生接收中断的字节数  【2014-5-5 注释，否则会导致程序无响应】
                //动态分配len字节单元内容用来存放发送数据
                PublicVar.g_SendByteArray = new byte[len];
                //获取TbSCISend文本的码值
                PublicVar.g_SendByteArray = System.Text.Encoding.Default.GetBytes(this.textBox2.Text);
                 
            }
            else //选择的是以十进制或者是十六进制发送数据
            {
                //sci.SCIReceInt(SCIPort, 1);//设置产生接收中断的字节数    【2014-5-5 注释，否则会导致程序无响应】
                foreach (string str in this.textBox2.Text.Split(','))
                {
                    //排除掉连续两个逗号,说明之间没有数
                    if (str != string.Empty)
                    {
                        if (SendType == 1)//选择的是以十进制方式发送
                        {
                            //将文本框中的数转化为十进制存入ArrayList类的实例对象
                            SendData.Add(Convert.ToByte(str, 10));
                            count++;//进行计数,统计有效的数据个数
                        }
                        else
                        {
                            //将文本框中的数转化为十六进制存入ArrayList类的实例对象
                            SendData.Add(Convert.ToByte(str, 16));
                            count++;//进行计数,统计有效的数据个数
                        }
                    }
                }
                //动态分配空间存放发送数据
                PublicVar.g_SendByteArray = new byte[count];

                //将已经转化后的数据放入到全局变量g_SendByteArray中
                for (i = 0; i < count; i++)
                    PublicVar.g_SendByteArray[i] = (byte)SendData[i];
            }

            //发送全局变量_SendByteArray中的数据,并返回结果
            Flag = sci.SCISendData(this.serialPort1, ref PublicVar.g_SendByteArray);

            if (Flag == true)//数据发送成功
                this.textBox1.Text += "数据发送成功!";
            else
                this.textBox1.Text += "数据发送失败!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
        }
        private void FrmSCI_FormClosing(object sender,
           FormClosingEventArgs e)
        {
            try
            {
                sci.SCIClose(serialPort1);
            }
            catch
            { }
        }
      
        private void SCIPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            String str = String.Empty;
            bool Flag;//标记串口接收数据是否成功
            int len;//标记接收的数据的长度
            char c;
            byte[] ch2 = new byte[2];
            //调用串口接收函数,并返回结果
            Flag = sci.SCIReceiveData(serialPort1, ref PublicVar.g_ReceiveByteArray);
            if (Flag == true)//串口接收数据成功
            {
                len = PublicVar.g_ReceiveByteArray.Length;
                //对于字符串形式,考虑到可能有汉字,
                //直接调用系统定义的函数,处理整个字符串
                str = Encoding.Default.GetString(PublicVar.g_ReceiveByteArray);

                SCIUpdateRevtxtbox(textBox3, str);
               

                //十进制和十六进制形式按字节进行处理
                for (int i = 0; i < len; i++)
                {
                    //十进制都是按照三位来显示,字节之间有空格表示区分
                    SCIUpdateRevtxtbox(textBox4,PublicVar.g_ReceiveByteArray[i].ToString("D3") + "  ");
                   
                    //十六进制都是按照两位来显示,字节之间有空格表示区分
                    SCIUpdateRevtxtbox(textBox5, PublicVar.g_ReceiveByteArray[i].ToString("X2") + "  ");
                }
                       

                // sci.SCIReceInt(SCIPort, 1);//设置产生接收中断的字节数【2014-5-5 注释，否则会导致程序无响应】
                this.textBox1.Text = "过程提示:数据接收成功!";
            }
            //接收数据失败
            else
            {
                //sci.SCIReceInt(SCIPort, 1);//设置产生接收中断的字节数【2014-5-5 注释，否则会导致程序无响应】 
                this.textBox1.Text = "过程提示:数据接收失败!";
            }
        }
        private void SCIUpdateRevtxtbox(Object textbox, string text)
        {
            //textbox显示文本与串口执行不在同一个线程中
            if (((TextBox)textbox).InvokeRequired)
            {
                handleinterfaceupdatedelegate InterFaceUpdate = new handleinterfaceupdatedelegate(SCIUpdateRevtxtbox);
                   
                this.Invoke(InterFaceUpdate, new object[] { textbox, text });
            }
            else
            {
                ((TextBox)textbox).Text += text;
                //把光标放在最后一行
                ((TextBox)textbox).SelectionStart =
                                           ((TextBox)textbox).Text.Length;
                //将文本框中的内容调整到当前插入符号位置
                ((TextBox)textbox).ScrollToCaret();
            }
        }


        private void CbSCISendType_SelectedIndexChanged(object sender,
                                                        EventArgs e)
        {
            this.textBox1.Text = "过程提示:选择数据发送方式!";
            //选择某一种发送方式时,首先清空发送框
            this.textBox1.Text = string.Empty;

            //选择的是发送字符串
            if (comboBox3.SelectedIndex == 0)
            {
               // this.LabNote.Text = "请输入字符串!";
                this.textBox1.Text = "你选择了发送字符串!";
            }
            else if (comboBox3.SelectedIndex == 1)
            {
               // this.LabNote.Text = "请输入十进制数,以逗号(英文输入法下)隔开"
               //                     + ",数据范围0-255,允许用退格键";
                this.comboBox3.Text = "你选择了发送十进制数!";
            }
            else
            {
               // this.LabNote.Text = "请输入十六进制数,以逗号(英文输入法下)隔"
              //                      + "开,数据范围00-FF,允许用退格键";
                this.textBox1.Text = "你选择了发送十六进制数! ";
            }
        }
        private void TbSCISend_KeyPress(object sender, KeyPressEventArgs e)
        {
            int select = comboBox3.SelectedIndex;
            if (select == 1)//输入的是十进制的数
            {
                //除了数字、逗号和退格键,其他都不给输入
                if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == 0x08
                    || e.KeyChar == ',')
                {
                    //输入的是数字,可以任意输入逗号与退格符
                    if (e.KeyChar >= '0' && e.KeyChar <= '9')
                    {
                        //在文本框中没有逗号时,
                        //this.TbSCISend.Text.LastIndexOf(',')默认为-1
                        //逗号之后出现第三个数字时,
                        //才用得着考虑是否会大于255
                        if (this.textBox2.Text.Length -
                            this.textBox2.Text.LastIndexOf(',') >= 2)
                        {
                            //考虑如果输入的话,是否会超出255
                            if (int.Parse(
                                    this.textBox2.Text.Substring(
                                 textBox2.Text.LastIndexOf(',') + 1)) * 10
                                    + e.KeyChar - '0' > 255)
                            {
                                e.Handled = true;
                                this.textBox1.Text = "输入数据不得大于255";
                            }
                            //默认情况下是允许输入的,即e.Handled = false
                        }
                    }
                }
                else
                {
                    e.Handled = true;//除了逗号、数字0~9,其他都不给输入
                    this.textBox1.Text = "输入数据必须是0-9,或者逗号"
                                          + ",或者退格符";
                }
            }
            //十六进制的处理方式与十进制相同,只是判断是否大于255时不太一样
            else if (select == 2)
            {
                //除了数字、大写字母、小写字母、逗号和退格键,其他都不给输入
                if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar >= 'a'
                    && e.KeyChar <= 'f' || e.KeyChar >= 'A' && e.KeyChar <=
                    'F' || e.KeyChar == 0x08 || e.KeyChar == ',')
                {
                    //逗号和退格符可以任意输入,只考虑输入数字的情况
                    if (e.KeyChar >= '0' && e.KeyChar <= '9')
                    {
                        if (this.textBox2.Text.Length -
                             this.textBox2.Text.LastIndexOf(',') >= 2)
                        {
                            if (Convert.ToInt32(textBox2.Text.Substring(
                               textBox2.Text.LastIndexOf(',') + 1), 16) * 16
                                + (e.KeyChar - '0') > 255)
                            {
                                e.Handled = true;
                                this.textBox1.Text = "输入数据不得大于255";
                            }
                        }
                    }
                    else if (e.KeyChar >= 'a' && e.KeyChar <= 'f'
                        || e.KeyChar >= 'A' && e.KeyChar <= 'F')
                    {
                        if (this.textBox2.Text.Length -
                             this.textBox2.Text.LastIndexOf(',') >= 2)
                        {
                            //无论是大写字母还是小写字母都转化成大写字母判断
                            if (Convert.ToInt32(textBox2.Text.Substring(
                               textBox2.Text.LastIndexOf(',') + 1), 16) * 16
                                + (Char.ToUpper(e.KeyChar) - 'A') > 255)
                            {
                                e.Handled = true;
                                this.textBox1.Text = "输入数据不得大于255";
                            }
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                    this.textBox1.Text = "输入数据必须是0-9,a-f,A-F,或者逗号"
                                          + ",或者退格符";
                }
            }
        }

    }
}
