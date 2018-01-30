using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        OleDbConnection objconnection;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
        }
        private void LinkData()
        {
            string connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\C#\串口\综合\Database1.accdb";
            objconnection = new OleDbConnection(connstr);
            objconnection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             try{
                LinkData();
                string ss = DateTime.Now.ToLocalTime().ToString();
                string sss = Class1.datas;
            string sql = "insert into data1 values('"+ ss +"','"+ sss +"')";
            OleDbCommand cmd = new OleDbCommand(sql, objconnection);
             OleDbDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                     objconnection.Close();

                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "error");
                 objconnection.Close();
             }
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
             try
            {

                LinkData();
                string sql = "select * from data1";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, objconnection); //创建适配对象
                DataTable dt = new DataTable(); //新建表对象
                da.Fill(dt); //用适配对象填充表对象
                dataGridView1.DataSource = dt; //将表对象作为DataGridView的数据源
            }
            finally
            {
                objconnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                LinkData();
                string sql = "DELETE * FROM data1";
                OleDbCommand cmd = new OleDbCommand(sql, objconnection);
                OleDbDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    objconnection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
                objconnection.Close();
            }
               
        }
        }
  }
