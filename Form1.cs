using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace DB_Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StudentDal _studentDal = new StudentDal();

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'libraryDataSetStudentId3.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter1.Fill(this.libraryDataSetStudentId3.Students);
            // TODO: This line of code loads data into the 'libraryDataSetBookId4.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter2.Fill(this.libraryDataSetBookId4.Books);
            // TODO: This line of code loads data into the 'libraryDataSetTypeId3.Types' table. You can move, or remove it, as needed.
            this.typesTableAdapter1.Fill(this.libraryDataSetTypeId3.Types);
            // TODO: This line of code loads data into the 'libraryDataSetAuthorId3.Authors' table. You can move, or remove it, as needed.
            this.authorsTableAdapter1.Fill(this.libraryDataSetAuthorId3.Authors);
            // TODO: This line of code loads data into the 'libraryDataSetTypeId2.Types' table. You can move, or remove it, as needed.
            this.typesTableAdapter.Fill(this.libraryDataSetTypeId2.Types);
            // TODO: This line of code loads data into the 'libraryDataSetBookId3.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter1.Fill(this.libraryDataSetBookId3.Books);
            // TODO: This line of code loads data into the 'libraryDataSetStudentId2.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.libraryDataSetStudentId2.Students);
            // TODO: This line of code loads data into the 'libraryDataSetBookId2.Books' table. You can move, or remove it, as needed.
            //this.booksTableAdapter.Fill(this.libraryDataSetBookId2.Books);
            // TODO: This line of code loads data into the 'libraryDataSetAuthorId2.Authors' table. You can move, or remove it, as needed.
            this.authorsTableAdapter.Fill(this.libraryDataSetAuthorId2.Authors);

            lblSearch.BackColor = System.Drawing.Color.Transparent;
            grpBoxType.BackColor = System.Drawing.Color.Transparent;
            grpBoxTypeUpdate.BackColor = System.Drawing.Color.Transparent;
            grpBoxAuthor.BackColor = System.Drawing.Color.Transparent;
            grpBoxAuthorUpdate.BackColor = System.Drawing.Color.Transparent;
            grpBoxBook.BackColor = System.Drawing.Color.Transparent;
            grpBoxBookUpdate.BackColor = System.Drawing.Color.Transparent;
            grpBoxBorrow.BackColor = System.Drawing.Color.Transparent;
            grpBoxBorrowUpdate.BackColor = System.Drawing.Color.Transparent;
            grpBoxStudent.BackColor = System.Drawing.Color.Transparent;
            grpBoxUpdate.BackColor = System.Drawing.Color.Transparent;

            //this.studentsTableAdapter1.Fill(this.libraryDataSetStudentId.Students);
            //this.booksTableAdapter.Fill(this.libraryDataSetBookId.Books);
            //this.typesTableAdapter1.Fill(this.libraryDataSetTypes.Types);
            //this.authorsTableAdapter1.Fill(this.libraryDataSetAuthorId.Authors);
            //this.studentsTableAdapter.Fill(this.libraryDataSetStudentsClass.Students);
            //this.authorsTableAdapter.Fill(this.libraryDataSetAuthors.Authors);


            //this.typesTableAdapter.Fill(this.libraryDataSet.Types);
            //LoadStudents();
            cbxData.Text = "Öğrenciler";


            ClockForDateTime();

        }

        private void ClockForDateTime()
        {
            dateTimeBorrowTakenUpdate.CustomFormat = "dd-MM-yyyy HH:mm";
            dateTimeBorrowTakenUpdate.Format = DateTimePickerFormat.Custom;

            dateTimeBorrow.CustomFormat = "dd-MM-yyyy HH:mm";
            dateTimeBorrow.Format = DateTimePickerFormat.Custom;


            dateTimeBorrowBroughtUpdate.CustomFormat = "dd-MM-yyyy HH:mm";
            dateTimeBorrowBroughtUpdate.Format = DateTimePickerFormat.Custom;


            dateTimeBorrowBroughtD.CustomFormat = "dd-MM-yyyy HH:mm";
            dateTimeBorrowBroughtD.Format = DateTimePickerFormat.Custom;
        }

        private void LoadStudents()
        {
            dgwLibrary.DataSource = _studentDal.GetAll();
        }

        public void ClearForStudentAdd()
        {
            tbxStudentName.Clear();
            tbxStudentSurname.Clear();
            cbxGender.SelectedIndex = -1;
            dateTimeStudent.CustomFormat = " ";
            dateTimeStudent.Format = DateTimePickerFormat.Custom;
            cbxStudentClass.SelectedIndex = -1;
            tbxStudentPoint.Clear();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                _studentDal.StudentAdd(new Student
                {
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbxStudentName.Text),
                    Surname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbxStudentSurname.Text),
                    BirthDate = Convert.ToDateTime((dateTimeStudent.Text)),
                    Gender = Convert.ToChar(cbxGender.Text.ToUpper()),
                    Class = cbxStudentClass.Text,
                    Point = Convert.ToInt32(tbxStudentPoint.Text)
                });
                LoadStudents();


                MessageBox.Show("Öğrenci eklendi.", "BAŞARILI!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.InnerException + exception.Message);
                MessageBox.Show("Tüm alanları doğru girdiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
            finally
            {
                ClearForStudentAdd();
                dateTimeStudent.CustomFormat = "dd/MM/yyyy";
                dateTimeStudent.Format = DateTimePickerFormat.Custom;
            }

        }

        public void ClearForBorrowClear()
        {
            cbxBorrowBookId.SelectedIndex = -1;
            cbxBorrowStudentId.SelectedIndex = -1;

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxData.Text == "Öğrenciler")
            {
                //StudentDal studentDal = new StudentDal();
                dgwLibrary.DataSource = _studentDal.GetAll();
                OnlyStudent();
                lblSearch.Text = "Öğrenci Ara";
                txtBoxSearch.Visible = true;
                lblSearch.Visible = true;

            }
            else if (cbxData.Text == "Yazarlar")
            {
                AuthorDal authorDal = new AuthorDal();
                dgwLibrary.DataSource = authorDal.GetAll();
                OnlyAuthor();
                lblSearch.Text = "Yazar Ara";
                txtBoxSearch.Visible = true;
                lblSearch.Visible = true;

            }
            else if (cbxData.Text == "Kitaplar")
            {
                BookDal bookDal = new BookDal();
                dgwLibrary.DataSource = bookDal.GetAll();
                lblSearch.Text = "Kitap Ara";
                OnlyBook();
                txtBoxSearch.Visible = true;
                lblSearch.Visible = true;


            }
            else if (cbxData.Text == "Ödünç Alma Kayıtları")
            {
                BorrowDal borrowDal = new BorrowDal();
                dgwLibrary.DataSource = borrowDal.GetAll();
                lblSearch.Visible = false;
                txtBoxSearch.Visible = false;
                OnlyBorrow();
                ClearForBorrowClear();
                cbxBorrowBookIdUpdate.SelectedIndex = -1;
                cbxBorrowStudentIdUpdate.SelectedIndex = -1;

            }
            else if (cbxData.Text == "Kitap Türleri")
            {
                TypeDal typeDal = new TypeDal();
                dgwLibrary.DataSource = typeDal.GetAll();
                lblSearch.Text = "Kitap Türü";
                OnlyType();
                txtBoxSearch.Visible = true;
                lblSearch.Visible = true;

            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        public void ClearForBookAdd()
        {
            tbxBookName.Clear();
            tbxBookPageCount.Clear();
            tbxBookPoint.Clear();
            cbxBookAuthorId.SelectedIndex = -1;
            cbxBookTypeId.SelectedIndex = -1;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BookDal bookDal = new BookDal();

            try
            {
                if (cbxBookAuthorId.Text == "" || tbxBookName.Text == "" || tbxBookPageCount.Text == "" || tbxBookPoint.Text == "" || cbxBookTypeId.Text == "")
                {
                    MessageBox.Show("Tüm alanları doldurduğunuzdan emin olun.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    bookDal.BookAdd(new Book
                    {
                        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbxBookName.Text),
                        PageCount = Convert.ToInt32(tbxBookPageCount.Text),
                        Point = Convert.ToInt32(tbxBookPoint.Text),
                        AuthorId = Convert.ToInt32(cbxBookAuthorId.Text),
                        TypeId = Convert.ToInt32(cbxBookTypeId.Text)


                    });
                    dgwLibrary.DataSource = bookDal.GetAll();
                    MessageBox.Show("Kitap eklendi.", "BAŞARILI!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Tüm alanları doğru girdiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally { ClearForBookAdd(); }
        }

        //Enabled - Disabled Functions For SQL Operation Panels
        public void EnableStudent()
        {
            grpBoxStudent.Visible = true;
            grpBoxUpdate.Visible = true;
        }
        public void DisableStudent()
        {
            grpBoxStudent.Visible = false;
            grpBoxUpdate.Visible = false;
        }

        public void EnableAuthor()
        {
            grpBoxAuthor.Visible = true;
            grpBoxAuthorUpdate.Visible = true;
        }
        public void DisableAuthor()
        {
            grpBoxAuthor.Visible = false;
            grpBoxAuthorUpdate.Visible = false;
        }

        public void EnableBook()
        {
            grpBoxBook.Visible = true;
            grpBoxBookUpdate.Visible = true;
        }

        public void DisableBook()
        {
            grpBoxBook.Visible = false;
            grpBoxBookUpdate.Visible = false;
        }

        public void EnableBorrow()
        {
            grpBoxBorrow.Visible = true;
            grpBoxBorrowUpdate.Visible = true;
        }

        public void DisableBorrow()
        {
            grpBoxBorrow.Visible = false;
            grpBoxBorrowUpdate.Visible = false;
        }

        public void EnableType()
        {
            grpBoxType.Visible = true;
            grpBoxTypeUpdate.Visible = true;
        }

        public void DisableType()
        {
            grpBoxType.Visible = false;
            grpBoxTypeUpdate.Visible = false;
        }

        public void OnlyStudent()
        {
            DisableType();
            DisableBorrow();
            DisableAuthor();
            DisableBook();
            EnableStudent();
        }

        public void OnlyBook()
        {
            DisableType();
            DisableBorrow();
            DisableAuthor();
            DisableStudent();
            EnableBook();
        }

        public void OnlyAuthor()
        {
            DisableType();
            DisableBorrow();
            DisableStudent();
            DisableBook();
            EnableAuthor();
        }

        public void OnlyType()
        {
            DisableAuthor();
            DisableBook();
            DisableStudent();
            DisableBorrow();
            EnableType();
        }

        public void OnlyBorrow()
        {
            DisableType();
            DisableAuthor();
            DisableBook();
            DisableStudent();
            EnableBorrow();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cbxData.Text == "Öğrenciler")
            {
                Student studentDal = new Student();
                dgwLibrary.DataSource = _studentDal.GetAll();

            }
            else if (cbxData.Text == "Yazarlar")
            {
                AuthorDal authorDal = new AuthorDal();
                dgwLibrary.DataSource = authorDal.GetAll();

            }
            else if (cbxData.Text == "Kitaplar")
            {
                BookDal bookDal = new BookDal();
                dgwLibrary.DataSource = bookDal.GetAll();


            }
            else if (cbxData.Text == "Ödünç Alma Kayıtları")
            {
                BorrowDal borrowDal = new BorrowDal();
                dgwLibrary.DataSource = borrowDal.GetAll();

            }
            else if (cbxData.Text == "Kitap Türleri")
            {
                TypeDal typeDal = new TypeDal();
                dgwLibrary.DataSource = typeDal.GetAll();

            }
            else
            {
                LoadStudents();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorDal authorDal = new AuthorDal();

            try
            {

                if (tbxAuthorName.Text == "" || tbxAuthorSurname.Text == "")
                {
                    MessageBox.Show("Tüm alanları doldurduğunuzdan emin olun.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    authorDal.AuthorAdd(new Author
                    {
                        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbxAuthorName.Text),
                        Surname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbxAuthorSurname.Text)

                    });
                    dgwLibrary.DataSource = authorDal.GetAll();
                    MessageBox.Show("Yazar eklendi.", "BAŞARILI!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Tüm alanları doğru girdiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                tbxAuthorName.Clear();
                tbxAuthorSurname.Clear();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TypeDal typeDal = new TypeDal();

            try
            {
                if (tbxType.Text == "")
                {
                    MessageBox.Show("Tüm alanları doldurduğunuzdan emin olun.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    typeDal.TypeAdd(new Type
                    {
                        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbxType.Text),

                    });
                    dgwLibrary.DataSource = typeDal.GetAll();
                    MessageBox.Show("Kitap türü eklendi.", "BAŞARILI!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Tüm alanları doğru girdiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                tbxType.Clear();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            BorrowDal borrowDal = new BorrowDal();

            try
            {
                if (cbxBorrowBookId.Text == "" || cbxBorrowStudentId.Text == "" || dateTimeBorrowBroughtD.Text == "" ||
                    dateTimeBorrow.Text == "")
                {
                    MessageBox.Show("Tüm alanları doldurduğunuzdan emin olun.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    borrowDal.BorrowAdd(new Borrow
                    {
                        StudentId = Convert.ToInt32(cbxBorrowStudentId.Text),
                        BookId = Convert.ToInt32(cbxBorrowBookId.Text),
                        TakenDate = Convert.ToDateTime(dateTimeBorrow.Text),
                        BroughtDate = Convert.ToDateTime(dateTimeBorrowBroughtD.Text)


                    });
                    dgwLibrary.DataSource = borrowDal.GetAll();
                    MessageBox.Show("Ödünç kaydı eklendi.", "BAŞARILI!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Tüm alanları doğru girdiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cbxBorrowBookId.SelectedIndex = -1;
                cbxBorrowStudentId.SelectedIndex = -1;

            }
        }



        private void dgwLibrary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cbxData.Text == "Öğrenciler")
            {
                dateTimeStdntUpd.CustomFormat = "dd/MM/yyyy";
                dateTimeStdntUpd.Format = DateTimePickerFormat.Custom;

                tbxStudentNameUpdate.Text = dgwLibrary.CurrentRow.Cells[1].Value.ToString();
                tbxStudentSurnameUpdate.Text = dgwLibrary.CurrentRow.Cells[2].Value.ToString();
                dateTimeStdntUpd.Text = dgwLibrary.CurrentRow.Cells[3].Value.ToString();
                cbxStudentGenderUpdate.Text = dgwLibrary.CurrentRow.Cells[4].Value.ToString();
                cbxStudentClassUpdate.Text = dgwLibrary.CurrentRow.Cells[5].Value.ToString();
                tbxStudentPointUpdate.Text = dgwLibrary.CurrentRow.Cells[6].Value.ToString();
                tbxStudentIdUpdate.Text = dgwLibrary.CurrentRow.Cells[0].Value.ToString();
            }
            else if (cbxData.Text == "Kitap Türleri")
            {
                tbxTypeIdUpdate.Text = dgwLibrary.CurrentRow.Cells[0].Value.ToString();
                tbxTypeBookUpdate.Text = dgwLibrary.CurrentRow.Cells[1].Value.ToString();
            }
            else if (cbxData.Text == "Ödünç Alma Kayıtları")
            {
                tbxBorrowIdUpdate.Text = dgwLibrary.CurrentRow.Cells[0].Value.ToString();
                cbxBorrowStudentIdUpdate.Text = dgwLibrary.CurrentRow.Cells[1].Value.ToString();
                cbxBorrowBookIdUpdate.Text = dgwLibrary.CurrentRow.Cells[2].Value.ToString();
                dateTimeBorrowTakenUpdate.Text = dgwLibrary.CurrentRow.Cells[3].Value.ToString();
                dateTimeBorrowBroughtUpdate.Text = dgwLibrary.CurrentRow.Cells[4].Value.ToString();

            }
            else if (cbxData.Text == "Yazarlar")
            {
                tbxAuthorIdUpdate.Text = dgwLibrary.CurrentRow.Cells[0].Value.ToString();
                tbxAuthorNameUpdate.Text = dgwLibrary.CurrentRow.Cells[1].Value.ToString();
                tbxAuthorSurnameUpdate.Text = dgwLibrary.CurrentRow.Cells[2].Value.ToString();
            }
            else if (cbxData.Text == "Kitaplar")
            {
                tbxBookIdUpdate.Text = dgwLibrary.CurrentRow.Cells[0].Value.ToString();
                tbxBookNameUpdate.Text = dgwLibrary.CurrentRow.Cells[1].Value.ToString();
                tbxBookPageCountUpdate.Text = dgwLibrary.CurrentRow.Cells[2].Value.ToString();
                tbxBookPointUpdate.Text = dgwLibrary.CurrentRow.Cells[3].Value.ToString();
                cbxBookAuthorIdUpdate.Text = dgwLibrary.CurrentRow.Cells[4].Value.ToString();
                //tbxBookTypeIdUpdate.Text = dgwLibrary.CurrentRow.Cells[5].Value.ToString();
                comboBox1.Text = dgwLibrary.CurrentRow.Cells[5].Value.ToString();
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxStudentNameUpdate.Text == "" || tbxStudentSurnameUpdate.Text == "" || dateTimeStdntUpd.Text == "" || cbxStudentGenderUpdate.Text == ""
                    || cbxStudentClassUpdate.Text == "" || tbxStudentPointUpdate.Text == "")
                {
                    MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doldurduğunuzdan emin olun.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    Student student = new Student
                    {
                        StudentId = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value),
                        Name = tbxStudentNameUpdate.Text,
                        Surname = tbxStudentSurnameUpdate.Text,
                        BirthDate = Convert.ToDateTime(dateTimeStdntUpd.Text),
                        Gender = Convert.ToChar(cbxStudentGenderUpdate.Text),
                        Class = cbxStudentClassUpdate.Text,
                        Point = Convert.ToInt32(tbxStudentPointUpdate.Text)
                    };
                    _studentDal.StudentUpdate(student);
                    LoadStudents();
                    MessageBox.Show("Güncelleme başarılı.", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doğru girdiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Öğrenci Sİlme Fonksiyonu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbxStudentIdUpdate.Text == "")
            {
                MessageBox.Show("Silme işlemi yapılamadı. Veri seçtiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value);

                    if (_studentDal.CheckOfStudentBorrow(id) != 0)
                    {
                        var count = _studentDal.CheckOfStudentBorrow(id);
                        MessageBox.Show(
                            "Ödünç alma tablosunda bu öğrenciye bağlı " + count +
                            " adet kayıt olduğundan silme işlemi yapılamaz!", "HATA!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        //_studentDal.BorrowDeleteByStudent(id);
                        _studentDal.StudentDelete(id);
                        LoadStudents();
                        MessageBox.Show("Silme işlemi başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Silme işlemi yapılamadı.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    tbxStudentIdUpdate.Clear();
                    tbxStudentNameUpdate.Clear();
                    tbxStudentSurnameUpdate.Clear();
                    cbxStudentGenderUpdate.SelectedIndex = -1;
                    cbxStudentClassUpdate.SelectedIndex = -1;
                    tbxStudentPointUpdate.Clear();
                }
            }
        }

        private void btnStudentClear_Click(object sender, EventArgs e)
        {

            tbxStudentIdUpdate.Clear();

            dateTimeStdntUpd.CustomFormat = " ";
            dateTimeStdntUpd.Format = DateTimePickerFormat.Custom;

            tbxStudentNameUpdate.Clear();
            tbxStudentSurnameUpdate.Clear();
            cbxStudentGenderUpdate.SelectedIndex = -1;
            cbxStudentClassUpdate.SelectedIndex = -1;
            tbxStudentPointUpdate.Clear();

            dateTimeStdntUpd.CustomFormat = "dd/MM/yyyy";
            dateTimeStdntUpd.Format = DateTimePickerFormat.Custom;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TypeDal typeDal = new TypeDal();
            try
            {
                if (tbxTypeBookUpdate.Text == "")
                {
                    MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doldurduğunuzdan emin olun.", "HATA!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    Type type = new Type
                    {
                        TypeId = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value),
                        Name = tbxTypeBookUpdate.Text,

                    };
                    typeDal.TypeUpdate(type);
                    dgwLibrary.DataSource = typeDal.GetAll();
                    MessageBox.Show("Güncelleme başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doğru girdiğinizden emin olun.", "HATA!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                tbxTypeBookUpdate.Clear();
                tbxTypeIdUpdate.Clear();
            }
        }

        private void btnClearType_Click(object sender, EventArgs e)
        {
            tbxTypeBookUpdate.Clear();
            tbxTypeIdUpdate.Clear();
        }

        private void btnClearBorrow_Click(object sender, EventArgs e)
        {
            cbxBorrowBookIdUpdate.SelectedIndex = -1;
            //tbxBorrowBroughtUpdate.Clear();
            //tbxBorrowTakenUpdate.Clear();
            cbxBorrowStudentIdUpdate.SelectedIndex = -1;
            tbxBorrowIdUpdate.Clear();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            BorrowDal borrowDal = new BorrowDal();
            try
            {
                if (cbxBorrowBookIdUpdate.Text == "" || cbxBorrowStudentIdUpdate.Text == "" ||
                    dateTimeBorrowTakenUpdate.Text == "" || dateTimeBorrowBroughtUpdate.Text == "")
                {
                    MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doldurduğunuzdan emin olun.", "HATA!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    Borrow borrow = new Borrow
                    {
                        BorrowId = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value),
                        StudentId = Convert.ToInt32(cbxBorrowStudentIdUpdate.Text),
                        BookId = Convert.ToInt32(cbxBorrowBookIdUpdate.Text),
                        TakenDate = Convert.ToDateTime(dateTimeBorrowTakenUpdate.Text),
                        BroughtDate = Convert.ToDateTime(dateTimeBorrowBroughtUpdate.Text)
                    };
                    borrowDal.BorrowUpdate(borrow);
                    dgwLibrary.DataSource = borrowDal.GetAll();
                    MessageBox.Show("Güncelleme başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doğru girdiğinizden emin olun.", "HATA!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cbxBorrowBookIdUpdate.SelectedIndex = -1;
                cbxBorrowStudentIdUpdate.SelectedIndex = -1;
            }
        }

        private void btnClearAuthor_Click(object sender, EventArgs e)
        {
            tbxAuthorIdUpdate.Clear();
            tbxAuthorNameUpdate.Clear();
            tbxAuthorSurnameUpdate.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AuthorDal authorDal = new AuthorDal();

            try
            {
                if (tbxAuthorNameUpdate.Text == "" || tbxAuthorSurnameUpdate.Text == "")
                {
                    MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doldurduğunuzdan emin olun.", "HATA!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    Author author = new Author
                    {
                        AuthorId = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value),
                        Name = tbxAuthorNameUpdate.Text,
                        Surname = tbxAuthorSurnameUpdate.Text
                    };
                    authorDal.AuthorUpdate(author);
                    dgwLibrary.DataSource = authorDal.GetAll();
                    MessageBox.Show("Güncelleme başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doğru girdiğinizden emin olun.", "HATA!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                tbxAuthorIdUpdate.Clear();
                tbxAuthorNameUpdate.Clear();
                tbxAuthorSurnameUpdate.Clear();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearForBookUpdate();
        }

        public void ClearForBookUpdate()
        {
            tbxBookIdUpdate.Clear();
            tbxBookNameUpdate.Clear();
            tbxBookPageCountUpdate.Clear();
            tbxBookPointUpdate.Clear();
            cbxBookAuthorIdUpdate.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BookDal bookDal = new BookDal();

            try
            {
                if (tbxBookNameUpdate.Text == "" || tbxBookPageCountUpdate.Text == "" ||
                    tbxBookPointUpdate.Text == "" || cbxBookAuthorIdUpdate.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doldurduğunuzdan emin olun.", "HATA!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    var typeId = comboBox1.SelectedValue;
                    Book book = new Book
                    {
                        BookId = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value),
                        Name = tbxBookNameUpdate.Text,
                        PageCount = Convert.ToInt32(tbxBookPageCountUpdate.Text),
                        Point = Convert.ToInt32(tbxBookPointUpdate.Text),
                        AuthorId = Convert.ToInt32(cbxBookAuthorIdUpdate.Text),
                        TypeId = Convert.ToInt32(comboBox1.Text)
                    };
                    bookDal.BookUpdate(book);
                    dgwLibrary.DataSource = bookDal.GetAll();
                    MessageBox.Show("Güncelleme başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Güncelleme yapılamadı. Tüm alanları doğru girdiğinizden emin olun.", "HATA!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ClearForBookUpdate();
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgwLibrary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)

        {
            if (tbxBookIdUpdate.Text == "")
            {
                MessageBox.Show("Silme işlemi yapılamadı. Veri seçtiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                BookDal bookdal = new BookDal();
                try
                {
                    int id = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value);

                    if (bookdal.CheckOfBooksBorrow(id) != 0)
                    {
                        var count = bookdal.CheckOfBooksBorrow(id);
                        MessageBox.Show(
                            "Ödünç alma tablosunda ID'si " + id + " numaralı kitaba bağlı " + count +
                            " adet kayıt olduğundan silme işlemi yapılamaz!", "HATA!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        //_studentDal.BorrowDeleteByStudent(id);
                        bookdal.BookDelete(id);
                        dgwLibrary.DataSource = bookdal.GetAll();
                        MessageBox.Show("Silme işlemi başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Silme işlemi yapılamadı.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    tbxBookIdUpdate.Clear();
                    tbxBookNameUpdate.Clear();
                    tbxBookPageCountUpdate.Clear();
                    tbxBookPointUpdate.Clear();
                    cbxBookAuthorIdUpdate.SelectedIndex = -1;
                    comboBox1.SelectedIndex = -1;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var id2 = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[1].Value);

            if (tbxBorrowIdUpdate.Text == "")
            {
                MessageBox.Show("Silme işlemi yapılamadı. Veri seçtiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (_studentDal.CheckStudentForBorrows(id2) != 0)
            {
                var count = _studentDal.CheckStudentForBorrows(id2);
                MessageBox.Show(id2 + " numaralı ID'ye sahip öğrenci elindeki mevcut " + count + " kitabı getirmeden silme işlemi yapılamaz!", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                BorrowDal borrowDal = new BorrowDal();
                try
                {
                    int id = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value);
                    borrowDal.BorrowDelete(id);
                    dgwLibrary.DataSource = borrowDal.GetAll();
                    MessageBox.Show("Silme işlemi başarılı.", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Silme işlemi yapılamadı.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    cbxBorrowBookIdUpdate.SelectedIndex = -1;
                    cbxBorrowStudentIdUpdate.SelectedIndex = -1;
                    tbxBorrowIdUpdate.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbxAuthorIdUpdate.Text == "")
            {
                MessageBox.Show("Silme işlemi yapılamadı. Veri seçtiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                AuthorDal authorDal = new AuthorDal();
                try
                {
                    int id = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value);

                    if (authorDal.CheckOfAuthorsBook(id) != 0)
                    {
                        var count = authorDal.CheckOfAuthorsBook(id);
                        MessageBox.Show(
                            "Kitaplar tablosunda ID'si " + id + " numaralı yazara bağlı " + count +
                            " adet kayıt olduğundan silme işlemi yapılamaz!", "HATA!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        //_studentDal.BorrowDeleteByStudent(id);
                        authorDal.AuthorDelete(id);
                        dgwLibrary.DataSource = authorDal.GetAll();
                        MessageBox.Show("Silme işlemi başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Silme işlemi yapılamadı.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    tbxAuthorIdUpdate.Clear();
                    tbxAuthorNameUpdate.Clear();
                    tbxAuthorSurnameUpdate.Clear();
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (tbxTypeIdUpdate.Text == "")
            {
                MessageBox.Show("Silme işlemi yapılamadı. Veri seçtiğinizden emin olun.", "HATA!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                TypeDal typeDal = new TypeDal();
                try
                {
                    int id = Convert.ToInt32(dgwLibrary.CurrentRow.Cells[0].Value);

                    if (typeDal.CheckOfTypesBook(id) != 0)
                    {
                        var count = typeDal.CheckOfTypesBook(id);
                        MessageBox.Show(
                            "Kitaplar tablosunda ID'si " + id + " numaralı türe bağlı " + count +
                            " adet kayıt olduğundan silme işlemi yapılamaz!", "HATA!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        //_studentDal.BorrowDeleteByStudent(id);
                        typeDal.TypeDelete(id);
                        dgwLibrary.DataSource = typeDal.GetAll();
                        MessageBox.Show("Silme işlemi başarılı.", "BAŞARILI", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Silme işlemi yapılamadı.", "HATA!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    tbxTypeIdUpdate.Clear();
                    tbxTypeBookUpdate.Clear();
                }
            }
        }

        private void SearchAuthors(string key)
        {
            AuthorDal authorDal = new AuthorDal();
            var result = authorDal.GetAll().Where(a => a.Name.ToLower().Contains(key.ToLower())).ToList();
            dgwLibrary.DataSource = result;
        }

        private void SearchBooks(string key)
        {
            BookDal bookDal = new BookDal();
            var result = bookDal.GetAll().Where(b => b.Name.ToLower().Contains(key.ToLower())).ToList();
            dgwLibrary.DataSource = result;
        }

        private void SearchTypes(string key)
        {
            TypeDal typeDal = new TypeDal();
            var result = typeDal.GetAll().Where(t => t.Name.ToLower().Contains(key.ToLower())).ToList();
            dgwLibrary.DataSource = result;
        }

        private void SearchStudents(string key)
        {
            var result = _studentDal.GetAll().Where(s => s.Name.ToLower().Contains(key.ToLower())).ToList();
            dgwLibrary.DataSource = result;
        }
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbxData.Text == "Öğrenciler")
            {
                dgwLibrary.DataSource = _studentDal.GetAll();
                SearchStudents(txtBoxSearch.Text);

            }
            else if (cbxData.Text == "Yazarlar")
            {
                AuthorDal authorDal = new AuthorDal();
                dgwLibrary.DataSource = authorDal.GetAll();
                SearchAuthors(txtBoxSearch.Text);
            }
            else if (cbxData.Text == "Kitaplar")
            {
                BookDal bookDal = new BookDal();
                dgwLibrary.DataSource = bookDal.GetAll();
                SearchBooks(txtBoxSearch.Text);

            }
            else if (cbxData.Text == "Kitap Türleri")
            {
                TypeDal typeDal = new TypeDal();
                dgwLibrary.DataSource = typeDal.GetAll();
                SearchTypes(txtBoxSearch.Text);

            }

        }
    }
}
