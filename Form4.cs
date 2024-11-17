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
using System.Linq.Expressions;


namespace ASM1__DB
{
    public partial class Form4 : Form
    {
        string connectionstring = @"Data Source=LAPTOP-8078I30L\SQLEXPRESS;Initial Catalog=QuanLyBanHang1;Integrated Security=True;TrustServerCertificate=True;";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        public Form4()
        {
            InitializeComponent();
        }


        private void Form4_Load(object sender, EventArgs e)
        {

            conn = new SqlConnection(@"Data Source=LAPTOP-8078I30L\SQLEXPRESS;Initial Catalog=QuanLyBanHang1;Integrated Security=True;TrustServerCertificate=True");
            LoadEmployeeData();


        }

        private void LoadEmployeeData()
        {
            try
            {
                using (conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    dt = new DataTable();
                    cmd = new SqlCommand("SELECT * FROM Employee", conn);
                    adt = new SqlDataAdapter(cmd);
                    adt.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void SaveEmployee()
        {
            try
            {
                using (conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    if (txtID.Text == "")
                    {
                        // Thêm mới nhân viên
                        cmd = new SqlCommand("INSERT INTO Employee (EmployeeName, Username, Password, Position) VALUES (@Name, @Username, @Password, @Position)", conn);
                    }
                    else
                    {
                        // Cập nhật thông tin nhân viên
                        cmd = new SqlCommand("UPDATE Employee SET EmployeeName=@Name, Username=@Username, Password=@Password, Position=@Position WHERE EmployeeID=@ID", conn);
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    }

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Password",txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Position",txtPosition.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lưu thông tin nhân viên thành công!");
                        LoadEmployeeData();
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi lưu thông tin nhân viên.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO Employee (EmployeeID, EmployeeName, Username, Password, Position) VALUES (@ID, @Name, @Username, @Password, @Position)", conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Username",txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Position",txtPosition.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm nhân viên thành công!");
                    LoadEmployeeData();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE Employee SET EmployeeName=@Name, Username=@Username, Password=@Password, Position=@Position WHERE EmployeeID=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Username",txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Position",txtPosition.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên với ID này.");
                    }
                    LoadEmployeeData();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEmployee();
        }




        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeID=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa nhân viên thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên với ID này.");
                    }
                    LoadEmployeeData();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            txtID.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtUserName.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtPosition.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }


        private void ClearTextBoxes()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtPosition.Text = "";
        }

    }


}
