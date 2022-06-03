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
    public class BookDal
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\mtay2;Initial Catalog=library;Integrated Security=True");

        public List<Book> GetAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Select * from Books order by AuthorId asc", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Book> books = new List<Book>();

            while (reader.Read())
            {
                Book book = new Book
                {
                    BookId = Convert.ToInt32(reader["BookId"]),
                    Name = reader["Name"].ToString(),
                    PageCount = Convert.ToInt32(reader["PageCount"]),
                    Point = Convert.ToInt32(reader["Point"]),
                    AuthorId = Convert.ToInt32(reader["AuthorId"]),
                    TypeId = Convert.ToInt32(reader["TypeId"]),

                };
                books.Add(book);
            }

            reader.Close();
            _connection.Close();
            return books;


        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void BookAdd(Book book)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Books values(@name,@pageCount,@point,@authorId,@typeId)", _connection);
            command.Parameters.AddWithValue("@name", book.Name);
            command.Parameters.AddWithValue("@pageCount", book.PageCount);
            command.Parameters.AddWithValue("@point", book.Point);
            command.Parameters.AddWithValue("@authorId", book.AuthorId);
            command.Parameters.AddWithValue("@typeId", book.TypeId);
            command.ExecuteNonQuery();
            _connection.Close();

        }

        public void BookUpdate(Book book)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Books set Name=@name, PageCount=@pageCount, Point=@point, AuthorId=@authorId, TypeId=@typeId where BookId=@bookId", _connection);
            command.Parameters.AddWithValue("@name", book.Name);
            command.Parameters.AddWithValue("@pageCount", book.PageCount);
            command.Parameters.AddWithValue("@point", book.Point);
            command.Parameters.AddWithValue("@authorId", book.AuthorId);
            command.Parameters.AddWithValue("@typeId", book.TypeId);
            command.Parameters.AddWithValue("@bookId", book.BookId);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void BookDelete(int id)
        {
            ConnectionControl();
            try
            {

                SqlCommand command = new SqlCommand("Delete from Books where BookId=@id", _connection);

                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Kitap silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally
            {
                _connection.Close();
            }


        }

        public int CheckOfBooksBorrow(int bookId)
        {
            var count = 0;
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("select Count(BookId) from Borrows where BookId =@id", _connection);
                command.Parameters.AddWithValue("@id", bookId);
                command.ExecuteNonQuery();
                count = (Int32)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Kitap silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally { _connection.Close(); }

            return count;
        }
    }
}
