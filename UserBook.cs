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
    public partial class UserBook : Form
    {
        public UserBook()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        private void UserBook_Load(object sender, EventArgs e)
        {
            // 创建一个新的 ToolTip 对象 p_8
            ToolTip p_8 = new ToolTip();
            p_8.ShowAlways = true; // 设置 ShowAlways 属性为 true，即使父控件不活动也显示 ToolTip
            p_8.InitialDelay = 200; // 设置 InitialDelay 属性为 200 毫秒，即鼠标悬停在控件上后 200 毫秒后显示 ToolTip
            p_8.ReshowDelay = 300; // 设置 ReshowDelay 属性为 300 毫秒，即鼠标移开再移回控件后 300 毫秒后重新显示 ToolTip
            p_8.IsBalloon = true; // 设置 IsBalloon 属性为 true，以气球形状显示 ToolTip
            p_8.SetToolTip(this.button8, "退出程序"); // 为 button8 控件设置 ToolTip 文本为“退出程序”

            // 创建一个新的 ToolTip 对象 p_12
            ToolTip p_12 = new ToolTip();
            p_12.ShowAlways = true; // 设置 ShowAlways 属性为 true，即使父控件不活动也显示 ToolTip
            p_12.InitialDelay = 200; // 设置 InitialDelay 属性为 200 毫秒，即鼠标悬停在控件上后 200 毫秒后显示 ToolTip
            p_12.ReshowDelay = 300; // 设置 ReshowDelay 属性为 300 毫秒，即鼠标移开再移回控件后 300 毫秒后重新显示 ToolTip
            p_12.IsBalloon = true; // 设置 IsBalloon 属性为 true，以气球形状显示 ToolTip
            p_12.SetToolTip(this.button12, "购买服务"); // 为 button12 控件设置 ToolTip 文本为“购买服务”

            // 创建一个新的 ToolTip 对象 p_10
            ToolTip p_10 = new ToolTip();
            p_10.ShowAlways = true; // 设置 ShowAlways 属性为 true，即使父控件不活动也显示 ToolTip
            p_10.InitialDelay = 200; // 设置 InitialDelay 属性为 200 毫秒，即鼠标悬停在控件上后 200 毫秒后显示 ToolTip
            p_10.ReshowDelay = 300; // 设置 ReshowDelay 属性为 300 毫秒，即鼠标移开再移回控件后 300 毫秒后重新显示 ToolTip
            p_10.IsBalloon = true; // 设置 IsBalloon 属性为 true，以气球形状显示 ToolTip
            p_10.SetToolTip(this.button10, "借书服务"); // 为 button10 控件设置 ToolTip 文本为“借书服务”

            // 创建一个新的 ToolTip 对象 p_14
            ToolTip p_14 = new ToolTip();
            p_14.ShowAlways = true; // 设置 ShowAlways 属性为 true，即使父控件不活动也显示 ToolTip
            p_14.InitialDelay = 200; // 设置 InitialDelay 属性为 200 毫秒，即鼠标悬停在控件上后 200 毫秒后显示 ToolTip
            p_14.ReshowDelay = 300; // 设置 ReshowDelay 属性为 300 毫秒，即鼠标移开再移回控件后 300 毫秒后重新显示 ToolTip
            p_14.IsBalloon = true; // 设置 IsBalloon 属性为 true，以气球形状显示 ToolTip
            p_14.SetToolTip(this.button14, "图书查询服务"); // 为 button14 控件设置 ToolTip 文本为“图书查询服务”
            
            UName.Text = Login.UserName;
        }
        private void Reset()
        {
            //清空刷新文本框
            BTitleTb.Text = string.Empty;
            BauthTb.Text = string.Empty;
            BCatCb.Text = "点击选择分类";
            QtyTb.Text = string.Empty;
            PriceTb.Text = string.Empty;
            key = 0;
        }

        private void Filter_Search()
        {
            string searchText1 = BTitleTb.Text.Trim();
            string searchText2 = BauthTb.Text.Trim();

            // 创建 SQL 查询语句
            string query = $"SELECT * FROM BookTb1 WHERE BTitle = '{searchText1}' OR BAuthor = '{searchText2}'";

            // 创建数据库连接
            using (SqlConnection connection = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True"))
            {
                // 创建适配器和数据集
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();

                // 打开数据库连接
                connection.Open();

                // 填充数据集
                adapter.Fill(ds, "Books");

                // 设置 DataGridView 的数据源
                BookDGV.DataSource = ds.Tables["Books"];
            }
        }
        private void populate()
        {//初始化datagridview
            Con.Open();
            string query = "select * from BookTb1";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        int key = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString(); // 设置 BTitleTb 的文本内容为 BookDGV 中选中行的第二个单元格的值
            BauthTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString(); // 设置 BauthTb 的文本内容为 BookDGV 中选中行的第三个单元格的值
            BCatCb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString(); // 设置 BCatCb 的选中项为 BookDGV 中选中行的第四个单元格的值
            QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString(); // 设置 QtyTb 的文本内容为 BookDGV 中选中行的第五个单元格的值
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString(); // 设置 PriceTb 的文本内容为 BookDGV 中选中行的第六个单元格的值

            if (BTitleTb.Text == "") // 如果 BTitleTb 的文本内容为空
            {
                key = 0; // 设置 key 的值为 0
            }
            else // 否则
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString()); // 设置 key 的值为 BookDGV 中选中行的第一个单元格的值转换为整数
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Filter_Search();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Books.Bookey == 1 || Users.UBookey == 1 || DashBoard.DBBookey == 1)
            {
                Books books = new Books();
                books.Show();
                this.Close();
            }
            else if (Books.Bookey == 0)
            {
                // 显示一个新的 Login 窗体
                Login login = new Login();
                login.Show();
                // 关闭当前窗体
                this.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //刷新图书列表
            populate();
            CatCbSearchCb.Text = "选定图书类型";
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Filter()
        {
            Con.Open(); // 打开数据库连接
            string query = $"select * from BookTb1 where BCat = '{CatCbSearchCb.SelectedItem.ToString()}'"; // 构造 SQL 查询语句，查询 BookTb1 表中 BCat 列的值与 CatCbSearchCb 选中项相同的所有行
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con); // 创建一个新的 SqlDataAdapter 对象，用于执行查询并将结果填充到 DataSet 中
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter); // 创建一个新的 SqlCommandBuilder 对象，用于自动生成用于更新数据库的命令
            var ds = new DataSet(); // 创建一个新的 DataSet 对象，用于存储从数据库获取的数据
            adapter.Fill(ds); // 使用 SqlDataAdapter 对象的 Fill 方法将从数据库获取的数据填充到 DataSet 对象中
            BookDGV.DataSource = ds.Tables[0]; // 将 BookDGV 的数据源设置为 DataSet 对象中的第一个表，从而在控件中显示数据
            Con.Close(); // 关闭数据库连接
        }

        private void CatCbSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //调用Filter的查询功能
            Filter();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Borr broowBook = new Borr();
            broowBook.Show();
            this.Hide();
        }

        private void UName_Click(object sender, EventArgs e)
        {

        }
    }
}
