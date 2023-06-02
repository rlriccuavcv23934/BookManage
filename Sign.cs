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
    public partial class Sign : Form
    {
        public Sign()
        {
            InitializeComponent();
            if(AdminPwd.key == 1)
            {
                Addre.ReadOnly = true;
                iPhone.ReadOnly = true;
            }
        }
        // 创建一个新的数据库连接
        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        // 当用户点击“button2”时，执行注册操作
        private void button2_Click(object sender, EventArgs e)
        {
            // 如果用户类型为“Admin”，则向管理员表中插入数据
            if (AdminPwd.key == 1)
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();

                    // 定义插入语句
                    string query = $"insert into Admin(AdName, AdPwd) values('{UNameTb.Text}', '{UPassTb.Text}')";

                    // 创建一个新的SqlCommand对象并执行插入语句
                    SqlCommand sqlCommand = new SqlCommand(query, Con);
                    SqlCommand cmd = sqlCommand;
                    cmd.ExecuteNonQuery();

                    // 显示成功消息
                    MessageBox.Show("管理员信息已注册成功! ! !");

                    // 关闭数据库连接
                    Con.Close();

                    // 打开登录窗口并隐藏当前窗口
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    // 如果发生异常，则显示异常消息
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
            else
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();

                    // 定义插入语句
                    string query = $"insert into UserTb1(UName, UPhone, UAdd, UPassword) values('{UNameTb.Text}', '{iPhone.Text}', '{Addre.Text}', {UPassTb.Text})";

                    // 创建一个新的SqlCommand对象并执行插入语句
                    SqlCommand sqlCommand = new SqlCommand(query, Con);
                    SqlCommand cmd = sqlCommand;
                    cmd.ExecuteNonQuery();

                    // 显示成功消息
                    MessageBox.Show("用户信息已注册成功! ! !");

                    // 关闭数据库连接
                    Con.Close();

                    // 打开登录窗口并隐藏当前窗口
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                }
                catch
                {
                    // 如果发生异常，则显示错误消息
                    MessageBox.Show("用户输入信息缺少!!!", "信息缺少");
                    Con.Close();
                }
            }
        }

        // 当用户点击“button8”时，打开登录窗口并隐藏当前窗口
        private void button8_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        // 定义一个方法来重置所有文本框的文本
        private void RestSign()
        {
            UNameTb.Text = string.Empty;
            UPassTb.Text = string.Empty;
            Addre.Text = string.Empty;
            iPhone.Text = string.Empty;
        }

        // 当用户点击“button1”时，重置所有文本框的文本
        private void button1_Click(object sender, EventArgs e)
        {
            RestSign();
        }


        private void Sign_Load(object sender, EventArgs e)
        {
            UNameTb.Text = "输入你的用户昵称";
        }
    }
}
