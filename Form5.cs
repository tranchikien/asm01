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

namespace ASM1__DB
{
    public partial class Form5 : Form
    {

        string connectionString = @"Data Source=LAPTOP-8078I30L\SQLEXPRESS;Initial Catalog=QuanLyBanHang1;Integrated Security=True;TrustServerCertificate=True;";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=LAPTOP-8078I30L\SQLEXPRESS;Initial Catalog=QuanLyBanHang1;Integrated Security=True;TrustServerCertificate=True");
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CustomerID, CustomerName, PhoneNumber, Address FROM Customers";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu khách hàng: {ex.Message}");
            }
        }


        private void LoadTransactionHistory(int customerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT S.SaleID, P.ProductName, S.QuantitySold, S.SaleDate, E.EmployeeName 
                        FROM Sales S
                        INNER JOIN Product P ON S.ProductID = P.ProductID
                        INNER JOIN Employee E ON S.EmployeeID = E.EmployeeID
                        WHERE S.CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử giao dịch: {ex.Message}");
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                   
                    string query = "INSERT INTO Customers (CustomerID, CustomerName, PhoneNumber, Address) VALUES (@ID, @Name, @Phone, @Address)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadCustomerData();
                    ClearCustomerForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.Message}");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Customers SET CustomerName = @Name, PhoneNumber = @Phone, Address = @Address WHERE CustomerID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật khách hàng thành công!");
                        LoadCustomerData();
                        ClearCustomerForm();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng với ID này.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật khách hàng: {ex.Message}");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
         string.IsNullOrWhiteSpace(txtNumber.Text) ||
         string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng trước khi lưu!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        // Nếu không có ID -> Thêm mới khách hàng
                        cmd = new SqlCommand(@"
                    INSERT INTO Customers (CustomerName, PhoneNumber, Address) 
                    VALUES (@Name, @Phone, @Address)", conn);
                    }
                    else
                    {
                        // Nếu có ID -> Cập nhật thông tin khách hàng
                        cmd = new SqlCommand(@"
                    UPDATE Customers 
                    SET CustomerName = @Name, PhoneNumber = @Phone, Address = @Address 
                    WHERE CustomerID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    }

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lưu thông tin khách hàng thành công!");
                        LoadCustomerData();
                        ClearCustomerForm();
                    }
                    else
                    {
                        MessageBox.Show("Không thể lưu thông tin khách hàng.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu thông tin: {ex.Message}");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string deleteSalesQuery = "DELETE FROM Sales WHERE CustomerID = @ID";
                    using (SqlCommand cmdDeleteSales = new SqlCommand(deleteSalesQuery, conn))
                    {
                        cmdDeleteSales.Parameters.AddWithValue("@ID", txtID.Text);
                        cmdDeleteSales.ExecuteNonQuery();
                    }
                    
                    string query = "DELETE FROM Customers WHERE CustomerID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadCustomerData();
                        ClearCustomerForm();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng với ID này.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.Message}");
            }
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT CustomerID, CustomerName, PhoneNumber, Address FROM Customers WHERE CustomerName LIKE @Search OR PhoneNumber LIKE @Search OR  Address LIKE @Search";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Search", "%" + btnSeach.Text + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng phù hợp!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }


        private void dataGridViewCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                txtNumber.Text = dataGridView1.Rows[e.RowIndex].Cells["PhoneNumber"].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();

                int customerId = int.Parse(txtID.Text);
                LoadTransactionHistory(customerId);
            }
        }
        private void ClearCustomerForm()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtNumber.Text = "";
            txtAddress.Text = "";
        }
    }

       


        

        

        
    
}
