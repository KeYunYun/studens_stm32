using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        //委托,将从串口接收到的数据显示到接收框里面
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BtnSCISwitch.Text = "打开串口(Open SCI)";
            this.CbSCIBaud.Enabled = true;    //[波特率选择框]处于可用状态
            this.CbSCIComNum.Enabled = true;　//[串口选择框]处于可用状态

            //自动搜索串口,并将其加入到[串口选择框]中
            int i;
            string[] SCIPorts;
            SCIPorts = System.IO.Ports.SerialPort.GetPortNames();
            this.CbSCIComNum.Items.Clear();//首先将现有的项清除掉
            for (i = 0; i < SCIPorts.Length; i++)
                //向[串口选择框]中添加搜索到的串口号
                this.CbSCIComNum.Items.Add(SCIPorts[i]);

            //设置各组合框的初始显示值
            if (SCIPorts.Length != 0)
            {
                this.BtnSCISwitch.Enabled = true;
                this.CbSCIBaud.SelectedIndex = 0;
                this.CbSCIComNum.SelectedIndex = 0;
                this.CbSCISendType.SelectedIndex = 0;

                //设置初始的串口号与波特率
                PublicVar.g_SCIComNum = this.CbSCIComNum.Text;
                PublicVar.g_SCIBaudRate = int.Parse(this.CbSCIBaud.Text);
                //显示当前串口信与状态信息
                this.LblSCI.Text = str + PublicVar.g_SCIComNum + "、" +
                                   PublicVar.g_SCIBaudRate + "\n" + msg;
                this.TSSLState.Text = "无操作,请先选择波特率与串口号,打开串口," +
                                 "然后发送数据";
            }
            else
            {
                this.TSSLState.Text = "没有可用的串口,请检查!";
                this.BtnSCISwitch.Enabled = false;
            }

        }

        private void CbSCIBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            PublicVar.g_SCIBaudRate = int.Parse(this.CbSCIBaud.Text);
            this.TSSLState.Text = "过程提示:选择波特率";
        }

        private void CbSCIComNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            PublicVar.g_SCIComNum = this.CbSCIComNum.Text;
            this.TSSLState.Text = "过程提示:选择串口号";
        }

        private void BtnSCISwitch_Click(object sender, EventArgs e)
        {
            bool Flag;//标记打开是否成功
            //根据按钮BtnSCISwitch显示内容执行打开或关闭串口操作
            if (this.BtnSCISwitch.Text.CompareTo("打开串口(Open SCI)") == 0)
            {
                //提示当前正在执行打开串口操作
                this.TSSLState.Text = "过程提示:正在打开串口...";
                //进行串口的初始化,并用Flag返回结果
                Flag = sci.SCIInit(SCIPort, PublicVar.g_SCIComNum,
                            PublicVar.g_SCIBaudRate);

                if (Flag == true)//串口打开成功
                {
                    //显示打开串口相关信息
                    this.LblSCI.Text = str + PublicVar.g_SCIComNum +
                        "、" + PublicVar.g_SCIBaudRate + "\n" + msg;

                    this.BtnSCISwitch.Text = "关闭串口(Close SCI)";
                    //[串口选择框]处于禁用状态
                    this.CbSCIComNum.Enabled = false;
                    //[波特率选择框]处于禁用状态
                    this.CbSCIBaud.Enabled = false;
                    //状态上显示结果信息
                    this.TSSLState.Text = this.TSSLState.Text +
                                          "打开" + PublicVar.g_SCIComNum + "成功!" + "波特率选择：" + PublicVar.g_SCIBaudRate;
                    //this.pictureBox1.Image = SerialPort.Properties.Resources.Run;
                }
                else//串口打开失败
                {
                    this.TSSLState.Text = this.TSSLState.Text +
                                          "打开" + PublicVar.g_SCIComNum + "失败!";
                    //this.pictureBox1.Image = SerialPort.Properties.Resources.Run_static;
                }
            }
            else if (this.BtnSCISwitch.Text == "关闭串口(Close SCI)")
            {
                //提示当前操作
                this.TSSLState.Text = "过程提示:正在关闭串口...";
                //执行关闭串口操作,并用Flag返回结果
                Flag = sci.SCIClose(this.SCIPort);
                if (Flag == true)
                {
                    this.LblSCI.Text = str + PublicVar.g_SCIComNum
                             + "、" + PublicVar.g_SCIBaudRate + "\n" + msg;
                    this.BtnSCISwitch.Text = "打开串口(Open SCI)";
                    //[串口选择框]处于可用状态
                    this.CbSCIComNum.Enabled = true;
                    //[波特率选择框]处于可用状态
                    this.CbSCIBaud.Enabled = true;
                    this.TSSLState.Text += "关闭" + PublicVar.g_SCIComNum + "成功!";
                    //this.pictureBox1.Image = SerialPort.Properties.Resources.Run_static;
                }
                else//串口关闭失败
                {
                    this.TSSLState.Text += "关闭" + PublicVar.g_SCIComNum + "失败!";
                }

            }
        }

        private void TbSCISend_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
