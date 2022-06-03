using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Library
{
    public class BorrowDal
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\mtay2;Initial Catalog=library;Integrated Security=True");

        public List<Borrow> GetAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Select * from Borrows order by StudentId asc", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Borrow> borrows = new List<Borrow>();

            while (reader.Read())
            {
                Borrow borrow = new Borrow
                {
                    BorrowId = Convert.ToInt32(reader["BorrowId"]),
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    BookId = Convert.ToInt32(reader["BookId"]),
                    TakenDate = Convert.ToDateTime(reader["TakenDate"]),
                    BroughtDate = Convert.ToDateTime(reader["BroughtDate"])


                };
                borrows.Add(borrow);
            }

            reader.Close();
            _connection.Close();
            return borrows;


        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }


        public void BorrowAdd(Borrow borrow)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Borrows values(@studentId,@bookId,@takenDate,@broughtDate)", _connection);
            command.Parameters.AddWithValue("@studentId", borrow.StudentId);
            command.Parameters.AddWithValue("@bookId", borrow.BookId);
            command.Parameters.AddWithValue("@takenDate", borrow.TakenDate);
            command.Parameters.AddWithValue("@broughtDate", borrow.BroughtDate);
            command.ExecuteNonQuery();
            _connection.Close();

        }

        public void BorrowUpdate(Borrow borrow)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Borrows set StudentId=@studentId, BookId=@bookId, TakenDate=@takenDate, BroughtDate=@broughtDate where BorrowId=@borrowId", _connection);
            command.Parameters.AddWithValue("@studentId", borrow.StudentId);
            command.Parameters.AddWithValue("@bookId", borrow.BookId);
            command.Parameters.AddWithValue("@takenDate", borrow.TakenDate);
            command.Parameters.AddWithValue("@broughtDate", borrow.BroughtDate);
            command.Parameters.AddWithValue("@borrowId", borrow.BorrowId);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void BorrowDelete(int id)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("Delete from Borrows where BorrowId=@borrowId", _connection);

                command.Parameters.AddWithValue("@borrowId", id);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Ödünç kaydı silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally
            {
                _connection.Close();
            }


        }
    }
}
