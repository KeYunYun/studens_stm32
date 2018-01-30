using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private const int Unit_length = 32;//单位格大小
        private int DrawStep = 8;//默认绘制单位
        private const int Y_Max = 512;//Y轴最大数值
        private const int MaxStep = 33;//绘制单位最大值
        private const int MinStep = 1;//绘制单位最小值
        private const int StartPrint = 32;//点坐标偏移量
        private List<byte> DataList = new List<byte>();//数据结构----线性链表
        private List<byte> DataList2 = new List<byte>();//数据结构----线性链表
        private List<byte> DataList3 = new List<byte>();//数据结构----线性链表
        private Pen TablePen = new Pen(Color.Yellow);//轴线颜色
        private Pen LinesPen = new Pen(Color.Black);//波形颜色
        private Pen LinesPen2 = new Pen(Color.Blue);//波形颜色
        private Pen LinesPen3 = new Pen(Color.Red);//波形颜色
        public ShowWindow ShowMainWindow;//定义显示主窗口委托访问权限为public
        public HideWindow HideMainWindow;//定义隐藏主窗口委托
        public OpenPort OpenSerialPort;//定义打开串口委托
        public ClosePort CloseSerialPort;//定义关闭串口委托
        public GetMainPos GetMainPos;//定义获取主窗口信息委托(自动对齐)
        public GetMainWidth GetMainWidth;//定义获取主窗口宽度(自动对齐)
        public Form2()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint,
                          true);//开启双缓冲

            this.UpdateStyles();
            InitializeComponent();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)//画
        {
            String Str = "";
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            e.Graphics.FillRectangle(Brushes.White, e.Graphics.ClipBounds);

            //Draw Y 纵向轴绘制
            for (int i = 0; i <= this.ClientRectangle.Width / Unit_length; i++)
            {
                e.Graphics.DrawLine(TablePen, StartPrint + i * Unit_length, StartPrint, StartPrint + i * Unit_length, StartPrint + Y_Max);//画线

                gp.AddString((i * (Unit_length / DrawStep)).ToString(), this.Font.FontFamily, (int)FontStyle.Regular, 12, new RectangleF(StartPrint + i * Unit_length - 7, this.ClientRectangle.Height - StartPrint + 4, 400, 50), null);//添加文字
            }
            //Draw X 横向轴绘制
            for (int i = 0; i <= this.ClientRectangle.Height / Unit_length; i++)
            {
                int k = 0;
                e.Graphics.DrawLine(TablePen, StartPrint, (i + 1) * Unit_length, this.ClientRectangle.Width, (i + 1) * Unit_length);//画线
            //    Str = ((16 - i) ).ToString();
                k = i % 2;
              //  Str = "0x" + (Str.Length == 1 ? Str + "0" : Str);
                if (k == 0)
                    Str = "0";
                else
                    Str = "1";
                   // break;
                gp.AddString(Str, this.Font.FontFamily, (int)FontStyle.Regular, 14, new RectangleF(0, StartPrint + i * Unit_length - 8, 400, 50), null);//添加文字
            }
            e.Graphics.DrawPath(Pens.Black, gp);//写文字
            if (DataList.Count - 1 >= (this.ClientRectangle.Width - StartPrint) / DrawStep)//如果数据量大于可容纳的数据量，即删除最左数据
            {
                DataList.RemoveRange(0, DataList.Count - (this.ClientRectangle.Width - StartPrint) / DrawStep - 1);
            }
            // = DataList.Count;
            for (int i = 0; i < DataList.Count - 1; i++)//绘制
            {
                e.Graphics.DrawLine(LinesPen, StartPrint + i * DrawStep, 17 * Unit_length - DataList[i] *32, StartPrint + (i + 1) * DrawStep, 17 * Unit_length - DataList[i + 1] * 32);
            }
            for (int i = 0; i < DataList2.Count - 1; i++)//绘制
            {
                e.Graphics.DrawLine(LinesPen2, StartPrint + i * DrawStep, 15 * Unit_length - DataList2[i] * 32, StartPrint + (i + 1) * DrawStep, 15 * Unit_length - DataList2[i + 1] * 32);
            }
            for (int i = 0; i < DataList3.Count - 1; i++)//绘制
            {
                e.Graphics.DrawLine(LinesPen3, StartPrint + i * DrawStep, 13 * Unit_length - DataList3[i] * 32, StartPrint + (i + 1) * DrawStep, 13* Unit_length - DataList3[i + 1] * 32);
            }

        }

        public void SetWindow(int width, Point Pt)//允许主窗口设置自己
        {
            int height = this.ClientRectangle.Height;
            height = this.Height - height;
            int BorderWeigh = this.Width - this.ClientRectangle.Width;
            this.Size = new Size(width - (width - BorderWeigh) % Unit_length, height + Y_Max + StartPrint + Unit_length);
            this.Location = Pt;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void AddData(byte Data)
        {
           // for (int i = 0; i < Data.Length; i++)
                DataList.Add(Data);//链表尾部添加数据
            Invalidate();//刷新显示
        }
        public void AddData2(byte Data)
        {
            // for (int i = 0; i < Data.Length; i++)
            DataList2.Add(Data);//链表尾部添加数据
            Invalidate();//刷新显示
        }
        public void AddData3(byte Data)
        {
            // for (int i = 0; i < Data.Length; i++)
            DataList3.Add(Data);//链表尾部添加数据
            Invalidate();//刷新显示
        }

        private void Drawer_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShowMainWindow();//关闭自己，显示主窗口
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
