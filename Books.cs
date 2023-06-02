using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookManage
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        //定义一个方法来填充“BookDGV”的数据
        private void populate()
        {
            // 打开数据库连接
            Con.Open();

            // 定义查询语句
            string query = "select * from BookTb1";

            // 创建一个新的SqlDataAdapter对象并填充数据表
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);

            // 将数据表绑定到“BookDGV”
            BookDGV.DataSource = ds.Tables[0];
            Con.Close(); // 关闭数据库连接
        }

        // 当用户点击“label5”时，打开仪表板窗口并隐藏当前窗口
        private void label5_Click(object sender, EventArgs e)
        {
            DashBoard dashboard = new DashBoard();
            dashboard.Show();
            this.Hide();
        }

        // 当窗口加载时，更新“UName”标签的文本
        private void Books_Load(object sender, EventArgs e)
        {
            UName.Text = Login.UserName;
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
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // 当用户点击“button3”按钮时，打开登录窗口并隐藏当前窗口
        private void button3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        // 当用户点击“SaveBtn”按钮时，检查文本框中的文本是否为空并向数据库中插入数据
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BauthTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1)
            {
                MessageBox.Show("信息未填写完全! ! !");
            } 
            else
            {
                try
                {//$"select * from DogUser where WeChatID='{textBox1.Text}' and DogPwd='{textBox2.Text}'";
                    Con.Open();
                    // 定义插入语句并执行插入操作
                    string query = $"insert into BookTb1 values('{BTitleTb.Text}', '{BauthTb.Text}', '{BCatCb.SelectedItem.ToString()}', '{QtyTb.Text}', '{PriceTb.Text}')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("图书信息已保存成功! ! !");
                    Con.Close();

                    // 重新填充“BookDGV”的数据并重置所有文本框的文本
                    populate();
                    Reset();
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        // 当用户更改“CatCbSearchCb”的选定项时，调用“Filter”方法来过滤数据
        private void CatCbSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        // 定义一个方法来过滤数据
        private void Filter()
        {
            Con.Open();
            string query = $"select * from BookTb1 where BCat = '{CatCbSearchCb.SelectedItem.ToString()}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            // 创建一个新的数据集并填充数据
            var ds = new DataSet(); //创建一个名为ds的新DataSet对象，它将用于存储从数据库获取的数据。
            adapter.Fill(ds); //使用SqlDataAdapter对象adapter的Fill方法将从数据库获取的数据填充到DataSet对象ds中。
            BookDGV.DataSource = ds.Tables[0];//将名为BookDGV的DataGridView组件的数据源设置为DataSet对象ds中的第一个数据表（索引为0），从而将数据显示在DataGridView组件中。
            Con.Close();
        }


        //当用户点击“button6”按钮时，重新填充“BookDGV”的数据并重置“CatCbSearchCb”的文本
        private void button6_Click(object sender, EventArgs e)
        {
            //SELECT name FROM books WHERE value = 0;
            Con.Open();
            //string query = $"select * from BookTb1 where BCat = '{CatCbSearchCb.SelectedItem.ToString()}'";
            SqlDataAdapter adapter_2 = new SqlDataAdapter($"SELECT BTitle FROM BookTb1 WHERE BQty = 0;", Con);
            // 创建一个新的 DataTable 对象，用于存储查询结果
            DataTable dataTable_2 = new DataTable();
            adapter_2.Fill(dataTable_2);
            if (dataTable_2.Rows.Count > 0)
            {
                string bookNames = string.Join("\n", dataTable_2.Rows.OfType<DataRow>().Select(row => row["BTitle"].ToString()));
                MessageBox.Show($"以下书籍库存为0：\n{bookNames}\n 补货联系电话: 18565273040", "补货提醒");
            }
            else
            {
                MessageBox.Show("没有找到库存为0的书籍。");
            }
            Con.Close();
            populate();
            CatCbSearchCb.Text = "选定图书类型";
        }

        // 当用户点击“ResetBtn”按钮时，重置所有文本框的文本
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // 定义一个方法来重置所有文本框的文本并将“key”变量设置为0
        private void Reset()
        {
            BTitleTb.Text = string.Empty;
            BauthTb.Text = string.Empty;
            BCatCb.Text = "点击选择分类";
            QtyTb.Text = string.Empty;
            PriceTb.Text = string.Empty;
            key = 0;
        }

        // 定义一个整数变量来存储“key”的值
        int key = 0;

        // 当用户单击“BookDGV”的单元格时，更新文本框的文本并根据“BTitleTb”文本框中的文本设置“key”变量的值
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // 更新文本框的文本
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BauthTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BCatCb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (BTitleTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        // 当用户点击“DeleteBtn”按钮时，检查“key”变量的值并从数据库中删除选定的行
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("貌似未选中信息! ! !");
            }
            else
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();

                    // 定义删除语句并执行删除操作
                    string query = $"delete from BookTb1 where BId = '{key}'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();

                    // 显示成功消息
                    MessageBox.Show("图书信息已删除成功! ! !");

                    // 关闭数据库连接
                    Con.Close();

                    // 重新填充“BookDGV”的数据并重置所有文本框的文本
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }


        // 当单击“编辑”按钮时调用此方法
        private void EditBtn_Click(object sender, EventArgs e)
        {
            // 检查任何文本字段是否为空或组合框中是否未选择任何项目
            if (BTitleTb.Text == "" || BauthTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1)
            {
                // 如果任何字段为空，则向用户显示消息框
                MessageBox.Show("貌似信息未完善! ! !");
            }
            else
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();
                    // 使用SQL查询更新数据库中的图书信息
                    string query = $"update BookTb1 set BTitle = '{BTitleTb.Text}', BAuthor = '{BauthTb.Text}', BCat = '{BCatCb.SelectedItem.ToString()}', BQty = '{QtyTb.Text}', BPrice = '{PriceTb.Text}' where BId = '{key}'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    // 向用户显示消息框，指示图书信息已成功更新
                    MessageBox.Show("图书信息已修改成功! ! !");
                    // 关闭数据库连接
                    Con.Close();
                    // 刷新表单中显示的数据
                    populate();
                    // 重置表单字段
                    Reset();
                }
                catch (Exception ex)
                {
                    // 如果发生异常，则向用户显示带有错误消息的消息框
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        // 单击button8时调用此方法
        private void button8_Click(object sender, EventArgs e)
        {
            // 创建登录表单的新实例并显示它
            Login login = new Login();
            login.Show();
            // 隐藏此表单
            this.Hide();
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            // 方法在单击 label4 时调用。它创建 Users 表单的新实例并显示它，然后隐藏当前表单。
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //方法在单击 pictureBox4 时调用。它创建 Users 表单的新实例并显示它，然后隐藏当前表单。
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //方法在单击 pictureBox5 时调用。它创建 DashBoard 表单的新实例并显示它，然后隐藏当前表单。
            DashBoard dashboard = new DashBoard();
            dashboard.Show();
            this.Hide();
        }

        private void UName_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            //方法在单击 label6 时调用。它创建 Borr 表单的新实例并显示它，然后隐藏当前表单
            Borr borr = new Borr();
            borr.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            // 方法在单击 pictureBox7 时调用。它创建 Borr 表单的新实例并显示它，然后隐藏当前表单
            Borr borr = new Borr();
            borr.Show();
            this.Hide();
        }

        private void CatCbSearchCb_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        private void Search_Click(object sender, EventArgs e)
        {
            //调用Filter_Search方法
            Filter_Search();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public static int Bookey = 0;

        private void Bill_Click(object sender, EventArgs e)
        {
            Bookey = 1;
            Convert.ToInt32(Bookey);
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }
    }
}
