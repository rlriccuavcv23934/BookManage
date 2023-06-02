using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace BookManage
{
    public partial class Borr : Form
    {
        public Borr()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        private void populate()
        {//初始化datagridview表
            Con.Open();
            string query = "select BId, BTitle, BQty from BookTb1";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            BookDBr.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Bill_Click(object sender, EventArgs e)
        {
            /*ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.InitialDelay = 200;
            p.ReshowDelay = 300;
            p.IsBalloon = true;
            p.SetToolTip(this.button12, "购买服务");*/
            Billing bill = new Billing();
            bill.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (DGVBorr.SelectedRows.Count > 0) // 如果 DGVBorr 中有选中的行
            {
                DialogResult result = MessageBox.Show("确定退出???", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); // 弹出提示框询问用户是否确定加入购物车
                if (result == DialogResult.No)
                {
                    return;
                }
                else if(result == DialogResult.Yes)
                {
                    int selectedRowIndex = DGVBorr.SelectedCells[0].RowIndex; // 获取选中单元格的行索引
                    string bookName = DGVBorr.SelectedRows[0].Cells[1].Value.ToString(); // 获取选中行的第二个单元格的值（即书名）
                    int quantity = Convert.ToInt32(DGVBorr.SelectedRows[0].Cells[2].Value.ToString()); // 获取选中行的第三个单元格的值（即数量）并转换为整数
                    DGVBorr.Rows.RemoveAt(selectedRowIndex); // 从 DGVBorr 中移除选中的行
                    try // 尝试执行以下代码
                    {
                        Con.Open(); // 打开数据库连接
                        string query = $"UPDATE BookTb1 SET BQty = BQty + '{quantity}' WHERE BTitle = '{bookName}'"; // 构造 SQL 更新语句，将 BookTb1 表中 BTitle 列的值为 bookName 的行的 BQty 列的值增加 quantity
                        SqlCommand cmd = new SqlCommand(query, Con); // 创建一个新的 SqlCommand 对象，用于执行更新语句
                        SqlCommand sqlCommand = cmd;
                        sqlCommand.ExecuteNonQuery(); // 执行更新语句
                        Con.Close(); // 关闭数据库连接
                        ResetBor(); // 调用 ResetBor 方法
                    }
                    catch (Exception ex) // 如果发生异常
                    {
                        MessageBox.Show(ex.Message); // 显示异常信息
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
        }

        int key = 0, stock = 0, n = 0;
        private void ResetBor()
        {
            BId.Text = string.Empty;
            BName.Text = string.Empty;
            textBox1.Text = string.Empty;
            BNameB.Text = string.Empty;
            BNum.Text = string.Empty;
        }
        private void ResetBorr_Click(object sender, EventArgs e)
        {
            ResetBor();
            key = 0;
        }

        private void UpdateBorrNumber()
        {
            int NewQty = stock - Convert.ToInt32(textBox1.Text); // 计算新的数量，即 stock 减去 textBox1 中输入的值
            try // 尝试执行以下代码
            {
                Con.Open(); // 打开数据库连接
                string query = $"update BookTb1 set BQty = '{NewQty}' where BId = '{key}'"; // 构造 SQL 更新语句，将 BookTb1 表中 BId 列的值为 key 的行的 BQty 列的值更新为 NewQty
                SqlCommand cmd = new SqlCommand(query, Con); // 创建一个新的 SqlCommand 对象，用于执行更新语句
                cmd.ExecuteNonQuery(); // 执行更新语句
                Con.Close(); // 关闭数据库连接
                populate(); // 调用 populate 方法
                ResetBor(); // 调用 ResetBor 方法
            }
            catch (Exception ex) // 如果发生异常
            {
                MessageBox.Show(ex.Message); // 显示异常信息
                Con.Close();
            }
        }

        private void AddBorr_Click(object sender, EventArgs e)
        {//记住需要将DataGridView设定为"行选择"
            //int borrowQuantity;
            //Boolean b = int.TryParse(BorrowB.Text, out borrowQuantity) && borrowQuantity > stock;

            //Boolean b = Convert.ToInt32(BorrowB.Text) > stock;
            if (textBox1.Text == "" || Convert.ToInt32(textBox1.Text) > stock) // 如果 textBox1 的文本内容为空或转换为整数后的值大于 stock
            {
                MessageBox.Show("请选中或修改购买量"); // 显示提示信息
            }
            else // 否则
            {
                DataGridViewRow DGV = new DataGridViewRow(); // 创建一个新的 DataGridViewRow 对象
                DGV.CreateCells(DGVBorr); // 为该行创建单元格
                DialogResult result = MessageBox.Show("是否确定加入借书单", "订单加入", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); // 弹出提示框询问用户是否确定加入借书单
                if (result == DialogResult.Yes) // 如果用户选择“是”
                {
                    DGV.Cells[0].Value = n + 1; // 设置该行第一列的值为 n+1
                    DGV.Cells[1].Value = BName.Text; // 设置该行第二列的值为 BName 的文本内容
                    DGV.Cells[2].Value = textBox1.Text; // 设置该行第三列的值为 textBox1 的文本内容
                    DGV.Cells[3].Value = BDate.Text; // 设置该行第四列的值为 BDate 的文本内容
                    n++; // n 自增 1
                    DGVBorr.Rows.Add(DGV); // 将该行添加到 DGVBorr 中

                    UpdateBorrNumber(); // 调用 UpdateBorrNumber 方法
                }
                else // 如果用户选择“否”
                {
                    MessageBox.Show("请修改后确定加入借书单", "订单加入"); // 显示提示信息
                }
            }
        }

        private void BorrwB_TextChanged(object sender, EventArgs e)
        {

        }

        private void DGVBorr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BNameB.Text = DGVBorr.SelectedRows[0].Cells[1].Value.ToString(); // 设置 BNameB 的文本内容为 DGVBorr 中选中行的第二个单元格的值
            BNum.Text = DGVBorr.SelectedRows[0].Cells[2].Value.ToString(); // 设置 BNum 的文本内容为 DGVBorr 中选中行的第三个单元格的值
            if (BNameB.Text == "") // 如果 BNameB 的文本内容为空
            {
                key = 0; // 设置 key 的值为 0
            }
            else // 否则
            {
                key = Convert.ToInt32(DGVBorr.SelectedRows[0].Cells[0].Value.ToString()); // 设置 key 的值为 DGVBorr 中选中行的第一个单元格的值转换为整数
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//删除借书单
            if (DGVBorr.SelectedRows.Count > 0) // 如果 DGVBorr 中有选中的行
            {
                int selectedRowIndex = DGVBorr.SelectedCells[0].RowIndex; // 获取选中单元格的行索引
                string bookName = DGVBorr.SelectedRows[0].Cells[1].Value.ToString(); // 获取选中行的第二个单元格的值（即书名）
                int quantity = Convert.ToInt32(DGVBorr.SelectedRows[0].Cells[2].Value.ToString()); // 获取选中行的第三个单元格的值（即数量）并转换为整数
                DGVBorr.Rows.RemoveAt(selectedRowIndex); // 从 DGVBorr 中移除选中的行
                try // 尝试执行以下代码
                {
                    Con.Open(); // 打开数据库连接
                    string query = $"UPDATE BookTb1 SET BQty = BQty + '{quantity}' WHERE BTitle = '{bookName}'"; // 构造 SQL 更新语句，将 BookTb1 表中 BTitle 列的值为 bookName 的行的 BQty 列的值增加 quantity
                    SqlCommand cmd = new SqlCommand(query, Con); // 创建一个新的 SqlCommand 对象，用于执行更新语句
                    SqlCommand sqlCommand = cmd;
                    sqlCommand.ExecuteNonQuery(); // 执行更新语句
                    Con.Close(); // 关闭数据库连接
                    ResetBor(); // 调用 ResetBor 方法
                }
                catch (Exception ex) // 如果发生异常
                {
                    MessageBox.Show(ex.Message); // 显示异常信息
                    Con.Close();
                }
            }
            else // 如果 DGVBorr 中没有选中的行
            {
                MessageBox.Show("请先选择要删除的行"); // 显示提示信息
            }
            populate(); // 调用 populate 方法
        }

        private void BorrowSet_Click(object sender, EventArgs e)
        {
            ResetBor();
        }


        private void BorrowSave_Click(object sender, EventArgs e)
        {
            if (DGVBorr.Rows[0].Cells[0].Value == null) // 如果借单中没有图书
            {
                MessageBox.Show("借单中还没有图书! ! !"); // 显示消息框
            }
            else // 否则
            {
                try // 尝试执行以下代码
                {
                    DateTime borrowDate; // 定义一个日期时间变量
                    if (!DateTime.TryParse(BDate.Text, out borrowDate)) // 如果无法将文本框中的文本转换为日期时间格式
                    {
                        MessageBox.Show("无效的日期格式。"); // 显示消息框
                        return; // 返回
                    }

                    string formattedDate = borrowDate.ToString("yyyy-MM-dd HH:mm:ss"); // 将日期时间格式化为字符串

                    Con.Open(); // 打开数据库连接
                    foreach (DataGridViewRow row in DGVBorr.Rows) // 遍历数据网格视图中的每一行
                    {
                        if (row.Cells[1].Value != null) // 如果该行的第二个单元格的值不为空
                        {
                            string BName = row.Cells[1].Value.ToString(); // 获取该行第二个单元格的值并转换为字符串
                            int BookNum = Convert.ToInt32(row.Cells[2].Value.ToString()); // 获取该行第三个单元格的值并转换为整数
                            string query = $"insert into BorrowTb2 (BName, UserName, BorrowDate, BookNum) values ('{BName}', '{UName.Text}', '{formattedDate}', '{BookNum}')"; // 定义插入语句
                            SqlCommand cmd = new SqlCommand(query, Con); // 创建一个新的 SqlCommand 对象
                            cmd.ExecuteNonQuery(); // 执行非查询语句
                        }
                    }

                    MessageBox.Show("借单信息已保存成功! ! !"); // 显示消息框
                    DGVBorr.Rows.Clear(); // 清除数据网格视图中的所有行
                    ResetBor(); // 调用 ResetBor 方法重置借单信息
                    Con.Close(); // 关闭数据库连接
                }
                catch (Exception ex) // 如果发生异常
                {
                    MessageBox.Show(ex.Message); // 显示异常信息
                    Con.Close();
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
            this.Close();
        }

        private void Borr_Load(object sender, EventArgs e)
        {
            UName.Text = Login.UserName;
            BDate.Text = System.DateTime.Now.ToString();


            //button8的悬浮提示
            ToolTip p_8 = new ToolTip();
            p_8.ShowAlways = true;
            p_8.InitialDelay = 200;
            p_8.ReshowDelay = 300;
            p_8.IsBalloon = true;
            p_8.SetToolTip(this.Exit, "退出程序");


            //button12的悬浮提示
            ToolTip p_12 = new ToolTip();
            p_12.ShowAlways = true;
            p_12.InitialDelay = 200;
            p_12.ReshowDelay = 300;
            p_12.IsBalloon = true;
            p_12.SetToolTip(this.Bill, "购买服务");

            //button10的悬浮提示
            ToolTip p_10 = new ToolTip();
            p_10.ShowAlways = true;
            p_10.InitialDelay = 200;
            p_10.ReshowDelay = 300;
            p_10.IsBalloon = true;
            p_10.SetToolTip(this.button10, "借书服务");

            //button14的悬浮提示
            ToolTip p_14 = new ToolTip();
            p_14.ShowAlways = true;
            p_14.InitialDelay = 200;
            p_14.ReshowDelay = 300;
            p_14.IsBalloon = true;
            p_14.SetToolTip(this.button14, "图书查询服务");
        }

        private void BookDBr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BId.Text = BookDBr.SelectedRows[0].Cells[0].Value.ToString(); // 将选中行的第一个单元格的值转换为字符串并赋值给 BId 文本框
            BName.Text = BookDBr.SelectedRows[0].Cells[1].Value.ToString(); // 将选中行的第二个单元格的值转换为字符串并赋值给 BName 文本框
            textBox1.Text = string.Empty; // 清空 textBox1 文本框的内容

            if (BId.Text == "") // 如果 BId 文本框的内容为空
            {
                key = 0; // 将 key 变量设为 0
                stock = 0; // 将 stock 变量设为 0
            }
            else // 否则
            {
                key = Convert.ToInt32(BookDBr.SelectedRows[0].Cells[0].Value.ToString()); // 将选中行的第一个单元格的值转换为整数并赋值给 key 变量
                stock = Convert.ToInt32(BookDBr.SelectedRows[0].Cells[2].Value.ToString()); // 将选中行的第三个单元格的值转换为整数并赋值给 stock 变量
            }
        }
    }
}

