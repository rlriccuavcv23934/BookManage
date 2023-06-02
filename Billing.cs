using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BookManage
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }



        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        private void button8_Click(object sender, EventArgs e)
        {

            // 检查是否有选中的行
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定退出???", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); // 弹出提示框询问用户是否确定加入购物车
                if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    // 获取选中行的索引
                    int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                    // 获取选中行的书名和数量
                    string bookName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    int quantity = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

                    // 从 dataGridView1 中删除选中的行
                    dataGridView1.Rows.RemoveAt(selectedRowIndex);

                    try
                    {
                        // 打开数据库连接
                        Con.Open();
                        // 更新书籍表中对应书名的库存数量
                        string query = $"UPDATE BookTb1 SET BQty = BQty + '{quantity}' WHERE BTitle = '{bookName}'";
                        SqlCommand sqlCommand = new SqlCommand(query, Con);
                        SqlCommand cmd = sqlCommand;
                        cmd.ExecuteNonQuery();

                        // 关闭数据库连接
                        Con.Close();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        // 显示异常信息
                        MessageBox.Show(ex.Message);
                        Con.Close();
                    }
                }
            }
            else if (Books.Bookey == 1 || Users.UBookey == 1 || DashBoard.DBBookey == 1)
            {
                Books books = new Books();
                books.Show();
                this.Close();
            }
            else if (Books.Bookey == 0)
            {
                Application.Exit();
            }
            // 调用 populate 方法
                      
        }

        private void populate()
        {
            // 打开数据库连接
            Con.Open();
            // 创建查询语句，用于获取书籍信息
            string query = "select BId, BTitle, BAuthor, BQty, BPrice from BookTb1";
            // 创建一个 SqlDataAdapter 对象，用于执行查询
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            // 创建一个 SqlCommandBuilder 对象
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            // 创建一个 DataSet 对象，用于存储查询结果
            var ds = new DataSet();
            // 将查询结果填充到 DataSet 中
            adapter.Fill(ds);
            // 将 DataSet 中的数据绑定到 BookD 控件上
            BookD.DataSource = ds.Tables[0];
            // 关闭数据库连接
            Con.Close();

        }


        int key = 0, stock = 0;
        private void BookD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 从 BookD 控件的选中行中获取书籍 ID，并显示在 textBox1 控件中
            textBox1.Text = BookD.SelectedRows[0].Cells[0].Value.ToString();
            // 从 BookD 控件的选中行中获取书籍标题，并显示在 BTitleTb 控件中
            BTitleTb.Text = BookD.SelectedRows[0].Cells[1].Value.ToString();
            // 将 QtyTb 控件的文本设置为空字符串
            QtyTb.Text = "";
            // 从 BookD 控件的选中行中获取书籍价格，并显示在 PriceTb 控件中
            PriceTb.Text = BookD.SelectedRows[0].Cells[4].Value.ToString();

            // 检查 BTitleTb 控件的文本是否为空
            if (BTitleTb.Text == "")
            {
                // 如果为空，则将 key 和 stock 变量设置为 0
                key = 0;
                stock = 0;
            }
            else
            {
                // 否则，将 key 和 stock 变量分别设置为选中行的书籍 ID 和库存量
                key = Convert.ToInt32(BookD.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookD.SelectedRows[0].Cells[3].Value.ToString());
            }
        }

        private void Reset()
        {
            //清空重置文本框
            textBox1.Text = string.Empty;
            BTitleTb.Text = string.Empty;
            QtyTb.Text = string.Empty;
            PriceTb.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            //BilliTotal.Text = "$: ";
            //populate();
        }

        private void ResetBuy_Click(object sender, EventArgs e)
        {
            //点击按钮,调用Reset()方法
            Reset();
            key = 0;
        }

        private void UpdateBookNumber()
        {
            // 计算新的库存量
            int NewQty = stock - Convert.ToInt32(QtyTb.Text);
            try
            {
                // 打开数据库连接
                Con.Open();
                // 创建更新语句，用于更新书籍表中对应书籍 ID 的库存量
                string query = $"update BookTb1 set BQty = '{NewQty}' where BId = '{key}'";
                // 创建一个 SqlCommand 对象，用于执行更新语句
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                // 关闭数据库连接
                Con.Close();
                // 调用 populate 和 Reset 方法
                populate();
                Reset();
            }
            catch (Exception ex)
            {
                // 显示异常信息
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }

        // 定义一个整型变量 n 和一个浮点型变量 GridTotal
        int n = 0;
        float GridTotal = 0;

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {//清空订单

        }
        int DeletedTotal = 0;
        private void button11_Click(object sender, EventArgs e)
        {//删除订单
         // 检查是否有选中的行
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 获取选中行的索引
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                // 获取选中行的书名和数量
                string bookName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                int quantity = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

                // 从 dataGridView1 中删除选中的行
                dataGridView1.Rows.RemoveAt(selectedRowIndex);

                try
                {
                    // 打开数据库连接
                    Con.Open();
                    // 更新书籍表中对应书名的库存数量
                    string query = $"UPDATE BookTb1 SET BQty = BQty + '{quantity}' WHERE BTitle = '{bookName}'";
                    SqlCommand sqlCommand = new SqlCommand(query, Con);
                    SqlCommand cmd = sqlCommand;
                    cmd.ExecuteNonQuery();

                    // 关闭数据库连接
                    Con.Close();
                    Reset();
                }
                catch (Exception ex)
                {
                    // 显示异常信息
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
            else
            {
                // 如果没有选中的行，则显示提示信息
                MessageBox.Show("请先选择要删除的行");
            }
            // 调用 populate 方法
            populate();
        }

        private void button12_Click(object sender, EventArgs e)
        {//修改订单

        }

        private void button13_Click(object sender, EventArgs e)
        {//清空刷新订单
            Reset();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 从 dataGridView1 控件的选中行中获取书籍 ID，并显示在 textBox2 控件中
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            // 从 dataGridView1 控件的选中行中获取书籍标题，并显示在 textBox3 控件中
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            // 检查 textBox2 控件的文本是否为空
            if (textBox2.Text == "")
            {
                // 如果为空，则将 key 变量设置为 0
                key = 0;
            }
            else
            {
                // 否则，将 key 变量设置为选中行的书籍 ID
                key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }*/
        }
        //int prodid, prodqty, prodprice, total, pos = 60;
        //string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {/*
            e.Graphics.DrawString("知世书店", new Font("幼圆", 10, FontStyle.Bold), Brushes.Black, new Point(80));
            e.Graphics.DrawString("编号  产品  价格  数量  总计", new Font("幼圆", 9, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["dataGridViewTextBoxColumn1"].Value);
                prodname = "" + row.Cells["dataGridViewTextBoxColumn3"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column1231241"].Value);
                prodqty = Convert.ToInt32(row.Cells["dataGridViewTextBoxColumn7"].Value);
                total = Convert.ToInt32(row.Cells["dataGridViewTextBoxColumn8"].Value);
                e.Graphics.DrawString("" + prodid, new Font("幼圆", 9, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("幼圆", 9, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("幼圆", 9, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("幼圆", 9, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("幼圆", 9, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20; 
            }
            e.Graphics.DrawString("订单总额: $" + GridTotal, new Font("幼圆", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("===========知世书店===========", new Font("幼圆", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            pos = 100;
            GridTotal = 0;*/
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            // 将 UName 控件的文本设置为 Login.UserName
            UName.Text = Login.UserName;

            // 为 button8 按钮创建一个 ToolTip 对象
            ToolTip p_8 = new ToolTip();
            // 设置 ToolTip 始终显示
            p_8.ShowAlways = true;
            // 设置 ToolTip 的初始延迟为 200 毫秒
            p_8.InitialDelay = 200;
            // 设置 ToolTip 的重新显示延迟为 300 毫秒
            p_8.ReshowDelay = 300;
            // 设置 ToolTip 使用气球样式
            p_8.IsBalloon = true;
            // 为 button8 按钮设置悬浮提示
            p_8.SetToolTip(this.button8, "退出程序");

            // 为 button12 按钮创建一个 ToolTip 对象
            ToolTip p_12 = new ToolTip();
            // 设置 ToolTip 始终显示
            p_12.ShowAlways = true;
            // 设置 ToolTip 的初始延迟为 200 毫秒
            p_12.InitialDelay = 200;
            // 设置 ToolTip 的重新显示延迟为 300 毫秒
            p_12.ReshowDelay = 300;
            // 设置 ToolTip 使用气球样式
            p_12.IsBalloon = true;
            // 为 button12 按钮设置悬浮提示
            p_12.SetToolTip(this.button12, "购买服务");

            // 为 button10 按钮创建一个 ToolTip 对象
            ToolTip p_10 = new ToolTip();
            // 设置 ToolTip 始终显示
            p_10.ShowAlways = true;
            // 设置 ToolTip 的初始延迟为 200 毫秒
            p_10.InitialDelay = 200;
            // 设置 ToolTip 的重新显示延迟为 300 毫秒
            p_10.ReshowDelay = 300;
            // 设置 ToolTip 使用气球样式
            p_10.IsBalloon = true;
            // 为 button10 按钮设置悬浮提示
            p_10.SetToolTip(this.button10, "借书服务");

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
            p_14.SetToolTip(this.button14, "图书查询服务");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {//结算
         // 检查 dataGridView1 控件的第一行第一列的值是否为空
            if (dataGridView1.Rows[0].Cells[0].Value == null)
            {
                // 如果为空，则显示提示信息
                MessageBox.Show("购物车还没有宝贝! ! !");
            }
            else
            {
                try
                {
                    // 打开数据库连接
                    Con.Open();
                    // 创建插入语句，用于将用户名和总金额插入到 BillTb1 表中
                    string query = $"insert into BillTb1(UName, Amount) values('{UName.Text}', {GridTotal})";
                    // 创建一个 SqlCommand 对象，用于执行插入语句
                    SqlCommand sqlCommand = new SqlCommand(query, Con);
                    SqlCommand cmd = sqlCommand;
                    cmd.ExecuteNonQuery();
                    // 显示提示信息
                    MessageBox.Show("订单信息已保存成功! ! !");
                    // 关闭数据库连接
                    Con.Close();
                    // 清空 dataGridView1 控件的所有行
                    dataGridView1.Rows.Clear();
                    // 将 BilliTotal 控件的文本设置为空字符串
                    BilliTotal.Text = "";
                    // 调用 Reset 方法
                    Reset();
                }
                catch (Exception ex)
                {
                    // 显示异常信息
                    MessageBox.Show(ex.Message); 
                    Con.Close();
                }
            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
;
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

        private void button14_Click_1(object sender, EventArgs e)
        {
            UserBook userBook = new UserBook(); 
            userBook.Show();
            this.Hide();
        }

        private void AddBuy_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > stock || QtyTb.Text == "0")
            {
                MessageBox.Show("请选中或修改购买量"); // 如果输入的数量为空、大于库存或等于0，则弹出提示框
            }
            else
            {
                float total = Convert.ToSingle(QtyTb.Text) * Convert.ToSingle(PriceTb.Text); // 计算总价
                DataGridViewRow dataGridViewRow = new DataGridViewRow(); // 创建一个新的 DataGridViewRow 对象
                dataGridViewRow.CreateCells(dataGridView1); // 为该行创建单元格
                DialogResult result = MessageBox.Show("是否确定加入购物车", "订单加入", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); // 弹出提示框询问用户是否确定加入购物车
                if (result == DialogResult.Yes) // 如果用户选择“是”
                {
                    dataGridViewRow.Cells[0].Value = n + 1; // 设置该行第一列的值为 n+1
                    dataGridViewRow.Cells[1].Value = BTitleTb.Text; // 设置该行第二列的值为 BTitleTb 的文本内容
                    dataGridViewRow.Cells[2].Value = QtyTb.Text; // 设置该行第三列的值为 QtyTb 的文本内容
                    dataGridViewRow.Cells[3].Value = PriceTb.Text; // 设置该行第四列的值为 PriceTb 的文本内容
                    dataGridViewRow.Cells[4].Value = total; // 设置该行第五列的值为 total 的值
                    dataGridView1.Rows.Add(dataGridViewRow); // 将该行添加到 dataGridView1 中
                    n++; // n 自增 1

                    UpdateBookNumber(); // 调用 UpdateBookNumber 函数
                    GridTotal = GridTotal + total; // 更新 GridTotal 的值
                    BilliTotal.Text = "$: " + Convert.ToString(GridTotal); // 更新 BilliTotal 的文本内容
                }
                else
                {
                    MessageBox.Show("请修改后确定加入购物车", "订单加入"); // 如果用户选择“否”，则弹出提示框
                }
            }
            populate(); // 调用 populate 函数
        }
    }
}
