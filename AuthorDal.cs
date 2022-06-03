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
    public class AuthorDal
    {

        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\mtay2;Initial Catalog=library;Integrated Security=True");

        public List<Author> GetAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Select * from Authors", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Author> authors = new List<Author>();

            while (reader.Read())
            {
                Author author = new Author
                {
                    AuthorId = Convert.ToInt32(reader["AuthorId"]),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),

                };
                authors.Add(author);
            }

            reader.Close();
            _connection.Close();
            return authors;


        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }


        public void AuthorAdd(Author author)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Authors values(@name,@surname)", _connection);
            command.Parameters.AddWithValue("@name", author.Name);
            command.Parameters.AddWithValue("@surname", author.Surname);
            command.ExecuteNonQuery();
            _connection.Close();

        }



        public void AuthorUpdate(Author author)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Authors set Name=@name, Surname=@surname where AuthorId=@authorId", _connection);
            command.Parameters.AddWithValue("@name", author.Name);
            command.Parameters.AddWithValue("@surname", author.Surname);
            command.Parameters.AddWithValue("@authorId", author.AuthorId);

            command.ExecuteNonQuery();
            _connection.Close();
        }


        public void AuthorDelete(int id)
        {
            ConnectionControl();
            try
            {

                SqlCommand command = new SqlCommand("Delete from Authors where AuthorId=@id", _connection);

                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Yazar silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally
            {
                _connection.Close();
            }


        }

        public int CheckOfAuthorsBook(int authorId)
        {
            var count = 0;
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("select Count(AuthorId) from Books where AuthorId =@id", _connection);
                command.Parameters.AddWithValue("@id", authorId);
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
