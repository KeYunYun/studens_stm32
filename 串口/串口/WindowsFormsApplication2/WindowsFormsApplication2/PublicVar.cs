using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public partial class PublicVar
    {

        //定义串口的全局变量,设置成静态的
        public static byte[] g_ReceiveByteArray;//全局变量，存放接收的数据
        public static byte[] g_SendByteArray;   //全局变量，存放要发送的数据
        public static byte[] g_SendByteLast;    //全局变量，存放最后的数据
        public static string g_SCIComNum;       //全局变量，存放选择的串口号
        public static int g_SCIBaudRate;        //全局变量，存放选择的波特率
    }
}
