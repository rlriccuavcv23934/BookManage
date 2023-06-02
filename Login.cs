using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManage
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        // 创建一个新的数据库连接
        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        // 当用户点击“button3”时，应用程序将退出
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 定义一个公共静态字符串变量来存储用户名
        public static string UserName = "";

        // 当用户在“textBox2”中输入文本时，将其字符更改为“*”
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UPassTb.PasswordChar = '*';
        }

        // 当用户点击“button2”时，执行登录操作
        private void button2_Click(object sender, EventArgs e)
        {
            // 打开数据库连接
            Con.Open();

            // 查询用户表中是否有匹配的用户名和密码
            SqlDataAdapter Acc = new SqlDataAdapter($"Select count(*) from UserTb1 where UName = '{UNameTb.Text}' and UPassword = '{UPassTb.Text}'", Con);

            // 查询管理员表中是否有匹配的用户名和密码
            SqlDataAdapter Acc_1 = new SqlDataAdapter($"Select count(*) from Admin where AdName = '{UNameTb.Text}' and AdPwd = '{UPassTb.Text}'", Con);

            // 创建一个新的数据表来存储管理员查询结果
            DataTable dt_1 = new DataTable();
            Acc_1.Fill(dt_1);

            // 创建一个新的数据表来存储用户查询结果
            DataTable dt = new DataTable();
            Acc.Fill(dt);

            // 如果用户类型为“User”，并且查询结果为1，则登录成功并打开相应窗口
            if (dt.Rows[0][0].ToString() == "1" && Admin.Checked == false && User.Checked == true)
            {   
                Welcome welcome = new Welcome();
                welcome.Show();
                Thread.Sleep(3000);
                welcome.Close();

                UserName = UNameTb.Text;
                UserBook bookBill = new UserBook();
                bookBill.Show();
                this.Hide();
            }
            // 如果用户类型为“Admin”，并且查询结果为1，则登录成功并打开相应窗口
            else if (Admin.Checked == true && dt_1.Rows[0][0].ToString() == "1" && User.Checked == false)
            {
                UserName = UNameTb.Text;
                Books books = new Books();
                books.Show();
                this.Hide();
            }
            // 否则，显示错误消息
            else
            {
                MessageBox.Show("用户名或密码或用户类型错误! ! !");
            }

            // 关闭数据库连接
            Con.Close();
        }

        // 当“Admin”复选框状态更改时，不执行任何操作
        private void Admin_CheckedChanged(object sender, EventArgs e)
        {

        }

        // 当用户点击“button1”时，执行注册操作
        private void button1_Click(object sender, EventArgs e)
        {
            // 如果用户类型为“User”，则打开相应窗口
            if (User.Checked == true)
            {
                Sign sign = new Sign();
                sign.Show();
                this.Hide();
            }
            // 如果用户类型为“Admin”，则打开相应窗口
            else if (Admin.Checked == true)
            {
                AdminPwd Adminsign = new AdminPwd();
                Adminsign.Show();
            }
            // 否则，显示错误消息
            else
            {
                MessageBox.Show("选择注册用户类型!!!");
            }
        }

        // 当窗口加载时，为“button1”添加工具提示
        private void Login_Load(object sender, EventArgs e)
        {
            ToolTip p_8 = new ToolTip();
            p_8.ShowAlways = true;
            p_8.InitialDelay = 200;
            p_8.ReshowDelay = 300;
            p_8.IsBalloon = true;
            p_8.SetToolTip(this.button1, "右侧选择用户类型");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("忘记密码联系客服: 18565273040" , "忘记密码");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
