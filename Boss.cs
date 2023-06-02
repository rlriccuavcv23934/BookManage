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
    public partial class Boss : Form
    {
        public Boss()
        {
            InitializeComponent();

        }


        public static int Bosskey = -1;

        private void button2_Click(object sender, EventArgs e)
        {
            // 如果文本框中的文本为“C#求高分”，则将“key”变量设置为1并打开注册窗口
            if (UNameTb.Text == "C#求高分" || UNameTb.Text == "王娜老师真美")
            {
                Bosskey = 1;
                Convert.ToInt32(Bosskey);
                GivB givB = new GivB();
                givB.Show();
                this.Close();
            }
            // 否则，显示错误消息并将“key”变量设置为0
            else
            {
                MessageBox.Show("请输入正确答案", "输入提示");
                Bosskey = 0;
                Convert.ToInt32(Bosskey);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UNameTb.Text = "C#求高分";//正常使用时应当为清空
            MessageBox.Show("密码为'C#求高分或王娜老师真美'", "密码提示");
        }
    }
}
