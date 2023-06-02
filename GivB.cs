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
    public partial class GivB : Form
    {
        public GivB()
        {
            InitializeComponent();
            if (Boss.Bosskey == 0)
            {
                Bill.Enabled = false;
            }
            populate();
            populate2();
        }

        // 创建一个新的数据库连接
        SqlConnection Con = new SqlConnection(@"Data Source=GPTMAN; Initial Catalog=WorldBookShop; Integrated Security=True");

        // 当用户单击“dataGridView1”的单元格时，更新文本框的文本
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        // 定义一个方法来填充“dataGridView1”的数据
        private void populate()
        {
            // 打开数据库连接
            Con.Open();

            // 定义查询语句
            string query = "select BrId, BName, UserName, BookNum, BorrowDate from BorrowTb2";

            // 创建一个新的SqlDataAdapter对象并填充数据表
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);

            // 将数据表绑定到“dataGridView1”
            dataGridView1.DataSource = ds.Tables[0];

            // 关闭数据库连接
            Con.Close();
        }

        // 定义一个方法来填充“dataGridView2”的数据
        private void populate2()
        {
            // 打开数据库连接
            Con.Open();

            // 定义查询语句
            string query = "select BillId, UName, Amount from BillTb1";

            // 创建一个新的SqlDataAdapter对象并填充数据表
            SqlDataAdapter adapter_1 = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter_1);
            var ds = new DataSet();
            adapter_1.Fill(ds);

            // 将数据表绑定到“dataGridView2”
            dataGridView2.DataSource = ds.Tables[0];
            
            // 关闭数据库连接
            Con.Close();
        }

        // 当用户点击“Exit”按钮时，打开仪表板窗口并隐藏当前窗口
        private void Exit_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Hide();
        }

        // 当用户点击“del”按钮时，从数据库中删除选定的行并更新书籍表中的数量
        private void del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                //DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                string bookName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                int quantity = Convert.ToInt32(textBox4.Text);

                // Delete the row from the DataGridView

                //dataGridView1.Rows.RemoveAt(selectedRowIndex);

                try
                {
                    // 打开数据库连接
                    Con.Open();
                    // 更新书籍表中的数量并从借阅表中删除选定的行
                    string query = $"UPDATE BookTb1 SET BQty = BQty + '{quantity}' WHERE BTitle = '{bookName}'";
                    string query_1 = $"DELETE FROM BorrowTb2 WHERE BName = '{bookName}'";
                    SqlCommand sqlCommand = new SqlCommand(query, Con);
                    SqlCommand sqlCommand1 = new SqlCommand(query_1, Con);
                    SqlCommand cmd = sqlCommand;
                    SqlCommand cmd2 = sqlCommand1;
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    // 关闭数据库连接
                    Con.Close();
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
                MessageBox.Show("请先选择要删除的行");
            }
            // 重新填充“dataGridView1”的数据并重置所有文本框的文本
            populate();
            Reset();
        }

        // 当用户点击“button1”按钮时，重置所有文本框的文本
        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // 定义一个方法来重置所有文本框的文本
        private void Reset() 
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }

        // 定义一个方法来重置“UName”和“Money”文本框的文本
        private void Reset_2()
        {
            UName.Text = string.Empty;
            Money.Text = string.Empty;
        }

        // 当用户单击“dataGridView2”的单元格时，更新“UName”和“Money”文本框的文本
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            Money.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
        }

        // 当用户点击“Reset2”按钮时，重置“UName”和“Money”文本框的文本
        private void Reset2_Click(object sender, EventArgs e)
        {
            Reset_2();
        }

        // 当用户点击“Bill”按钮时，从数据库中删除选定的行
        private void Bill_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                string UserName = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();                
                
                // 删除DataGridView中选定的行
                dataGridView2.Rows.RemoveAt(selectedRowIndex);

                try
                {
                    Con.Open();

                    // 从账单表中删除选定的行
                    string query_1 = $"DELETE FROM BillTb1 WHERE UName = '{UserName}'";
                    //SqlCommand sqlCommand = new SqlCommand(query, Con);
                    SqlCommand sqlCommand1 = new SqlCommand(query_1, Con);
                    //SqlCommand cmd = sqlCommand;
                    SqlCommand cmd2 = sqlCommand1;
                    //cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    // 关闭数据库连接
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
            else
            {
                MessageBox.Show("请先选择要删除的行");
            }

            // 重新填充“dataGridView2”的数据并重置“UName”和“Money”文本框的文本
            populate2();
            Reset_2();
        }

        private void GivB_Load(object sender, EventArgs e)
        {

        }
    }
}
