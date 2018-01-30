using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public partial class SCI
    {
        /// ----------------------------------------------------------------
        /// <summary>                                                       
        /// 功    能:初始化串口,并打开串口                                  
        /// 内部调用:无                                                     
        /// </summary>                                                      
        /// <param name="Port">串口控件,串口类型</param>                    
        /// <param name="ComNum">串口号,字符串类型</param>                  
        /// <param name="Baud">波特率,整型</param>                          
        /// <returns>返回一个布尔值,当串口成功打开后,返回true               
        ///          当串口打开异常时,返回false </returns>                  
        /// ----------------------------------------------------------------
        public bool SCIInit(System.IO.Ports.SerialPort Port,
                            string ComNum, Int32 Baud)
        {
            try
            {
                Port.Close();               //保证初始化之前是关闭的
                Port.PortName = ComNum;     //设置串口号
                Port.BaudRate = Baud;       //设置波特率
                Port.Parity = System.IO.Ports.Parity.None;//设置无奇偶校验
                Port.DataBits = 8;          //设置8比特数据位
                Port.StopBits = System.IO.Ports.StopBits.One;//设置1位停止位
                Port.ReadBufferSize = 4096; //接收缓冲区大小(字节) 
                Port.WriteBufferSize = 2048;//发送缓冲区大小(字节)
                Port.Open();                //打开串口
            }
            catch
            {

                return false; //产生错误,返回false
            }
            return true;
        }

        ///-----------------------------------------------------------------
        /// <summary>                                                       
        /// 功    能:串口发送数据                                           
        /// 内部调用:无                                                     
        /// </summary>                                                      
        /// <param name="Port">串口控件,串口类型</param>                   
        /// <param name="SendArray">存放要发送的数据,字节数组</param>      
        /// <returns>返回一个布尔值,当发送成功后,返回True;                 
        ///          否则返回False</returns>                                
        ///-----------------------------------------------------------------
        public bool SCISendData(System.IO.Ports.SerialPort Port,
                                       ref byte[] SendArray)
        {
            try
            {
                Port.Write(SendArray, 0, SendArray.Length);//通过串口发送出去
            }
            catch
            {
                //产生错误,返回false
                return false;
            }
            return true;
        }

        ///-----------------------------------------------------------------
        /// <summary>                                                       
        /// 功    能:收取串口接收缓冲区数据，放入数组。数组为传地址方式传参
        ///          数带入。             
        /// 函数调用:无                                                     
        /// </summary>                                                      
        /// <param name="Port">串口控件,串口类型</param>                    
        /// <param name="ReceiveArray">存放接收数据数组,字节数组</param>    
        /// <returns>返回一个布尔值,当接收成功后,返回true                   
        ///          否则,返回false</returns>                               
        ///-----------------------------------------------------------------
        public bool SCIReceiveData(System.IO.Ports.SerialPort Port,
                                    ref byte[] ReceiveArray)
        {
            int len;
            len = Port.BytesToRead;//获取接收缓冲区中的字节数
            try
            {
                ReceiveArray = new byte[len];
                //从接收缓冲区中读取数据，将其放入ReceiveArray中,并清除缓冲区            
                Port.Read(ReceiveArray, 0, len);
            }
            catch
            {
                return false;//产生错误,返回false（0）
            }
            return true;//正确，返回true（1）
        }

        ///-----------------------------------------------------------------
        /// <summary>                                                      
        /// 功    能:关闭串口                                               
        /// 内部调用:无                                                     
        /// </summary>                                                      
        /// <param name="Port">串口控件,串口类型</param>                    
        /// <returns>返回一个布尔值,当串口成功关闭后,返回true              
        ///          否则返回false</returns>                                
        ///-----------------------------------------------------------------
        public bool SCIClose(System.IO.Ports.SerialPort Port)
        {
            try
            {
                Port.DiscardInBuffer(); //丢弃接收缓冲区的数据
                Port.DiscardOutBuffer();//丢弃发送缓冲区的数据
                Port.Dispose();         //释放串行通信的所有资源
                Port.Close();           //关闭串口
            }
            catch
            {
                //产生错误,返回false
                return false;
            }
            return true;
        }

        ///-----------------------------------------------------------------
        /// <summary>                                                       
        /// 功    能:设置串口“DataReceived”事件（中断）的触发条件                                          
        /// 函数调用:无                                                     
        /// </summary>                                                     
        /// <param name="Port">串口控件,串口类型</param>                    
        /// <param name="a">设置“DataReceived”事件的触发条件,整型</param>                 
        ///-----------------------------------------------------------------
        public void SCIReceInt(System.IO.Ports.SerialPort Port, int a)
        {
            //设置串口接收中断条件
            Port.ReceivedBytesThreshold = a;
        }
    }
}
