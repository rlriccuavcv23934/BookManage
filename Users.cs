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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from UserTb1";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            UsersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            //定义重置清空文本框
            UNameTb.Text = string.Empty;
            UPhoneTb.Text = string.Empty;
            UAddrTb.Text = string.Empty;
            UPwdTb.Text = string.Empty;    
            key = 0;
        }

        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");
        // 当单击 button2 时调用此方法
        private void button2_Click(object sender, EventArgs e)
        {
            // 检查文本字段是否为空
            if (UNameTb.Text == "" || UPhoneTb.Text == "" || UAddrTb.Text == "" || UPwdTb.Text == "")
            {
                // 如果任何字段为空，则向用户显示消息框
                MessageBox.Show("信息未填写完全! ! !");
            }
            else
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();
                    // 使用SQL查询将用户信息插入数据库
                    string query = $"insert into UserTb1 values('{UNameTb.Text}', '{UPhoneTb.Text}', '{UAddrTb.Text}', '{UPwdTb.Text}')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    // 向用户显示消息框，指示用户信息已成功保存
                    MessageBox.Show("用户信息已保存成功! ! !");
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
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            //方法在单击 button5 时调用。它调用 Reset() 方法来重置表单字段。
            Reset();
        }


        // 定义一个整数变量 key，其初始值为 0
        int key = 0;

        // 当单击 button4 时调用此方法
        private void button4_Click(object sender, EventArgs e)
        {
            // 检查 key 的值是否为 0
            if (key == 0)
            {
                // 如果 key 的值为 0，则向用户显示消息框
                MessageBox.Show("貌似未选中信息! ! !");
            }
            else
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();
                    // 使用SQL查询从数据库中删除用户信息
                    string query = $"delete from UserTb1 where UId = '{key}'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    // 向用户显示消息框，指示用户信息已成功删除
                    MessageBox.Show("用户信息已删除成功! ! !");
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
                }
            }
        }


        // 当单击 UsersDGV 的单元格内容时调用此方法
        private void UsersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 将文本字段的值设置为所选行中相应单元格的值
            UNameTb.Text = UsersDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPwdTb.Text = UsersDGV.SelectedRows[0].Cells[2].Value.ToString();
            UPhoneTb.Text = UsersDGV.SelectedRows[0].Cells[3].Value.ToString();
            UAddrTb.Text = UsersDGV.SelectedRows[0].Cells[4].Value.ToString();
            

            // 检查 UNameTb 文本字段的值是否为空
            if (UNameTb.Text == "")
            {
                // 如果 UNameTb 文本字段的值为空，则将 key 的值设置为 0
                key = 0;
            }
            else
            {
                // 否则，将 key 的值设置为所选行中第一个单元格的值
                key = Convert.ToInt32(UsersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }


        // 当单击 button1 时调用此方法
        private void button1_Click(object sender, EventArgs e)
        {
            // 检查文本字段是否为空
            if (UNameTb.Text == "" || UPhoneTb.Text == "" || UPwdTb.Text == "" || UAddrTb.Text == "")
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
                    // 使用SQL查询更新数据库中的用户信息
                    string query = $"update UserTb1 set UName = '{UNameTb.Text}', UPassword = '{UAddrTb.Text}', UPhone = '{UPwdTb.Text}', UAdd = '{UPhoneTb.Text}' where UId = '{key}'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    // 向用户显示消息框，指示用户信息已成功更新
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
                }
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {//退出
            Application.Exit();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            books.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            books.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        public static int UBookey = 0;
        private void Bill_Click(object sender, EventArgs e)
        {
            UBookey = 1;
            Convert.ToInt32(UBookey);
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }

        private void Users_Load(object sender, EventArgs e)
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

            ToolTip p_15 = new ToolTip();
            p_15.ShowAlways = true;
            p_15.InitialDelay = 200;
            p_15.ReshowDelay = 300;
            p_15.IsBalloon = true;
            p_14.SetToolTip(this.SaveBtn, "用户名查询");
        }


        private void Filter_Search()
        {
            string searchText1 = UNameTb.Text.Trim();

            // 创建 SQL 查询语句
            string query = $"SELECT * FROM UserTb1 WHERE [UName] = '{searchText1}'";

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
                UsersDGV.DataSource = ds.Tables["Books"];
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Filter_Search();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
