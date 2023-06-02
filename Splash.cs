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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        // 定义一个整数变量来存储进度条的值
        int startops = 0;

        // 当计时器触发“Tick”事件时，更新进度条和百分比标签
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 增加进度条的值
            startops += 1;
            Myprogress.Value = startops;

            // 更新百分比标签的文本
            PercentageLbl.Text = startops + "%";

            // 如果进度条的值达到100，则停止计时器并打开登录窗口
            if (Myprogress.Value == 100)
            {
                Myprogress.Value = 0;
                timer1.Stop();
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        // 当窗口加载时，启动计时器
        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
