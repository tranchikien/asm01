using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM1__DB
{
    public partial class Form3 : Form
    {
        string connecstring = @"Data Source=LAPTOP-8078I30L\SQLEXPRESS;Initial Catalog=QuanLyBanHang1;Integrated Security=True;TrustServerCertificate=True;";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
            conn = new SqlConnection(@"Data Source=LAPTOP-8078I30L\SQLEXPRESS;Initial Catalog=QuanLyBanHang1;Integrated Security=True;TrustServerCertificate=True");
        
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dt = new DataTable();
                using (conn = new SqlConnection(connecstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT * FROM Product", conn);
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

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (conn = new SqlConnection(connecstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO Product (ProductID, ProductName, SellingPrice, InventoryQuantity) VALUES (@ProductID, @ProductName, @SellingPrice, @InventoryQuantity)", conn);
                    cmd.Parameters.AddWithValue("@ProductID", txtID.Text);
                    cmd.Parameters.AddWithValue("@ProductName", txtName.Text);
                    cmd.Parameters.AddWithValue("@SellingPrice", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@InventoryQuantity", txtQuantity.Text);
                    cmd.ExecuteNonQuery();
                    LoadData();

                    MessageBox.Show("Product added successfully.");
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = e.RowIndex;
                txtID.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtName.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtPrice.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                txtQuantity.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) 
        {
            try
            {
                using (conn = new SqlConnection(connecstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE Product SET ProductName = @ProductName, SellingPrice = @SellingPrice, InventoryQuantity = @InventoryQuantity WHERE ProductID = @ProductID", conn);
                    cmd.Parameters.AddWithValue("@ProductID", txtID.Text);
                    cmd.Parameters.AddWithValue("@ProductName", txtName.Text);
                    cmd.Parameters.AddWithValue("@SellingPrice", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@InventoryQuantity", txtQuantity.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No customer found with this ID.");
                    }
                    
                }
               
                LoadData(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using(conn = new SqlConnection(connecstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTRO Product(ProductID,Productname,InventoryQuantity,SellingPrice)VALUES(@ProductID,@ProductName,@InventoryQuantity,@SellingPrice)", conn);
                    cmd.Parameters.AddWithValue("@ProductID", txtID.Text);
                    cmd.Parameters.AddWithValue("@ProductName", txtName.Text);
                    cmd.Parameters.AddWithValue("@InventoryQuantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@SellingPrice", txtPrice.Text);
                    int rowsAffected= cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User has been successful save.");
                    }
                    else
                    {
                        MessageBox.Show("No customer found with this ID.");
                    }
                    //conn.Close();
                 
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            //Clear();
            try
            {
                using (conn = new SqlConnection(connecstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Product WHERE ProductID=@ID", conn);
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
                    LoadData();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Clear()
        {
            txtID.Text ="";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";

        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                using (conn = new SqlConnection(connecstring))
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT * FROM Product WHERE ProductID = @ProductID, ProductName = @ProductName, ProductQuantity = @ProductQuantity, ProductPrice = @ProductPrice", conn);
                    cmd.Parameters.AddWithValue("@ProductID", txtID.Text);
                    cmd.Parameters.AddWithValue("@ProductName", "%" + txtName.Text + "%");
                    cmd.Parameters.AddWithValue("@ProductQuantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@ProductPrice", txtPrice.Text);

                    //adt = new SqlDataAdapter(cmd);
                    //adt.Fill(dt);
                    //dataGridView1.DataSource = dt;
                    //cmd.ExecuteNonQuery();
                    LoadData();

                    MessageBox.Show("Product seach successfully.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy tên sản phẩm đã chọn từ DataGridView  
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                string productName = dataGridView1.Rows[rowIndex].Cells["ProductName"].Value.ToString();

                // Cập nhật thông tin sản phẩm  
                txtName.Text = productName;

                // Tạo đường dẫn tới hình ảnh  
                string imagePath = Path.Combine(Application.StartupPath, "Images", productName + ".jpg"); // hoặc ".png"  

                // Kiểm tra xem hình ảnh có tồn tại không  
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath); // Hiển thị hình ảnh  
                }
                else
                {
                    pictureBox1.Image = null; // Nếu không có hình ảnh  
                    MessageBox.Show("Hình ảnh không tồn tại cho sản phẩm này.");
                }
            }
        }
    }
}
