using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManage
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");
        private void button3_Click(object sender, EventArgs e)
        {//点击按钮弹出对应窗体
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            // 为 button14 按钮创建一个 ToolTip 对象
            ToolTip p_14 = new ToolTip();
            // 设置 ToolTip 始终显示
            p_14.ShowAlways = true;
            // 设置 ToolTip 的初始延迟为 200 毫秒
            p_14.InitialDelay = 200;
            // 设置 ToolTip 的重新显示延迟为 300 毫秒
            p_14.ReshowDelay = 300;
            // 设置 ToolTip 使用气球样式
            p_14.IsBalloon = true;
            // 为 button14 按钮设置悬浮提示
            p_14.SetToolTip(this.Bill, "图书结算服务");
            // 打开数据库连接
            Con.Open();

            // 创建一个新的 SqlDataAdapter 对象，用于查询书籍库存
            SqlDataAdapter adapter = new SqlDataAdapter($"Select sum(BQty) from BookTb1", Con);
            // 创建一个新的 DataTable 对象，用于存储查询结果
            DataTable dataTable = new DataTable();
            // 将查询结果填充到 dataTable 中
            adapter.Fill(dataTable);
            // 将查询结果显示在 BookStockLbl 标签上
            BookStockLbl.Text = dataTable.Rows[0][0].ToString();

            // 创建一个新的 SqlDataAdapter 对象，用于查询账单总额
            SqlDataAdapter adapter_1 = new SqlDataAdapter($"Select sum(Amount) from BillTb1", Con);
            // 创建一个新的 DataTable 对象，用于存储查询结果
            DataTable dataTable_1 = new DataTable();
            // 将查询结果填充到 dataTable_1 中
            adapter_1.Fill(dataTable_1);
            // 将查询结果显示在 AmountLbl 标签上，并添加货币符号
            AmountLbl.Text = "$ " + dataTable_1.Rows[0][0].ToString();

            // 创建一个新的 SqlDataAdapter 对象，用于查询用户总数
            SqlDataAdapter adapter_2 = new SqlDataAdapter($"Select count(UId) from UserTb1", Con);
            // 创建一个新的 DataTable 对象，用于存储查询结果
            DataTable dataTable_2 = new DataTable();
            // 将查询结果填充到 dataTable_2 中
            adapter_2.Fill(dataTable_2);
            // 将查询结果显示在 UserTotalLbl 标签上，并添加单位
            UserTotalLbl.Text = dataTable_2.Rows[0][0].ToString() + " 位";

            // 创建一个新的 SqlDataAdapter 对象，用于查询借阅书籍总数
            SqlDataAdapter adapter_3 = new SqlDataAdapter($"Select sum(BookNum) from BorrowTb2", Con);
            // 创建一个新的 DataTable 对象，用于存储查询结果
            DataTable dataTable_3 = new DataTable();
            // 将查询结果填充到 dataTable_3 中
            adapter_3.Fill(dataTable_3);
            // 将查询结果显示在 label9 标签上，并添加单位
            label9.Text = dataTable_3.Rows[0][0].ToString() + "本";

            // 关闭数据库连接
            Con.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {//图书; 点击按钮弹出对应窗体
            Books books = new Books();
            books.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {//图书; 点击按钮弹出对应窗体
            Books books = new Books();
            books.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {//点击弹出对应窗体
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {//点击弹出对应窗体
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {//点击弹出对应窗体
            Borr borr = new Borr();
            borr.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {//点击弹出对应窗体
            Borr borr = new Borr();
            borr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {//点击按钮弹出对应窗体
            Boss Bosssign = new Boss();
            Bosssign.Show();
            if(Boss.Bosskey == 1 || Boss.Bosskey == 0)
            {
                GivB givB = new GivB();
                givB.Show();
                this.Hide();
            }
            
        }
        public static int DBBookey = 0;
        private void Bill_Click(object sender, EventArgs e)
        {
            DBBookey = 1;
            Convert.ToInt32(DBBookey);
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }
    }
}
