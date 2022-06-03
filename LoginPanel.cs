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

namespace DB_Library
{
    public partial class LoginPanel : Form
    {
        public LoginPanel()
        {
            InitializeComponent();
        }

        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\mtay2;Initial Catalog=library;Integrated Security=True");

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private void ClearTextboxes()
        {
            tbxUsername.Clear();
            tbxPassword.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblLoginInfo_Click(object sender, EventArgs e)
        {

        }

        private void tbxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (person_status == 1) //Admin
            {
                ConnectionControl();
                SqlCommand command =
                    new SqlCommand("select * from Accounts where Username='" + tbxUsername.Text + "' and Password ='" + tbxPassword.Text + "'", _connection);

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.Read()) //Admin Logged!
                    {
                       
                        Form1 formLibrary = new Form1();
                        formLibrary.ShowDialog();
                        Close();

                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz bilgiler yanlış!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearTextboxes();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Beklenmeyen bir hata!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextboxes();
                }
                finally
                {
                    reader.Close();
                    _connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Beklenmeyen bir hata!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int person_status = 1;

        private void btnStudent_Click(object sender, EventArgs e)
        {
            StudentPanel studentPanel = new StudentPanel();
            studentPanel.ShowDialog();
            Close();
        }
    }
}
