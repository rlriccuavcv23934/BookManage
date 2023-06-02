using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManage
{
    public partial class AdminPwd : Form
    {
        public AdminPwd()
        {
            InitializeComponent();
        }
        // 定义一个公共静态整数变量来存储“key”的值
        public static int key = 0;

        // 当用户点击“button2”时，检查文本框中的文本
        private void button2_Click(object sender, EventArgs e)
        {
            // 如果文本框中的文本为“C#求高分”，则将“key”变量设置为1并打开注册窗口
            if (UNameTb.Text == "C#求高分" || UNameTb.Text == "王娜老师真美")
            {
                key = 1;
                Convert.ToInt32(key);
                Sign sign = new Sign();
                sign.Show();
                this.Hide();
            }
            // 否则，显示错误消息并将“key”变量设置为0
            else
            {
                MessageBox.Show("请输入正确答案", "输入提示");
                key = 0;
                Convert.ToInt32(key);
            }
        }

        // 当用户点击“button1”时，重置文本框的文本
        private void button1_Click(object sender, EventArgs e)
        {
            UNameTb.Text = "C#求高分";//正常使用时应当为清空
            MessageBox.Show("密码为'C#求高分或王娜老师真美'", "密码提示");
        }

        private void AdminPwd_Load(object sender, EventArgs e)
        {

        }

        private void UNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
