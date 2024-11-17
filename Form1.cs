﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ASM1__DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string pass = txtPass.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter your name", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter your pass", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (name == "admin" && pass == "5678")
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                }
                else if (name == "employee" && pass == "1234")
                {
                  Form4 form4 = new Form4();
                    form4.Show();
                    this.Hide();
                }
                else if (name == "customer" && pass == "9999")
                {
                    Form5 form5 = new Form5();
                    form5.Show();
                    this.Hide();
                }
                else if (name == "product" && pass == "6666")
                {
                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(" Invalid username or password", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult check_Exit = MessageBox.Show("Do you want to exit?", "Confirm",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check_Exit == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
