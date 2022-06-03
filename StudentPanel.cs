using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Library
{
    public partial class StudentPanel : Form
    {
        public StudentPanel()
        {
            InitializeComponent();
        }

        private void StudentPanel_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'libraryDataSetStudentPanel.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter.Fill(this.libraryDataSetStudentPanel.Books);

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void SearchBooks(string key)
        {
            BookDal bookDal = new BookDal();
            var result = bookDal.GetAll().Where(b => b.Name.ToLower().Contains(key.ToLower())).ToList();
            dataGridView1.DataSource = result;
        }

        private void tbxSearchBook_TextChanged(object sender, EventArgs e)
        {
            SearchBooks(tbxSearchBook.Text);
        }
    }
}
