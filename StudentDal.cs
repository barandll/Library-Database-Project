using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Library
{
    public class StudentDal
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\mtay2;Initial Catalog=library;Integrated Security=True");

        public List<Student> GetAll()
        {

            ConnectionControl();

            SqlCommand command = new SqlCommand("Select * from Students", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Student> students = new List<Student>();

            while (reader.Read())
            {
                Student student = new Student
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    Gender = Convert.ToChar(reader["Gender"]),
                    Class = reader["Class"].ToString(),
                    Point = Convert.ToInt32(reader["Point"])
                };
                students.Add(student);
            }

            reader.Close();
            _connection.Close();
            return students;
        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void StudentAdd(Student student)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Students values(@name,@surname,@birthDate,@gender,@class,@point)", _connection);
            command.Parameters.AddWithValue("@name", student.Name);
            command.Parameters.AddWithValue("@surname", student.Surname);
            command.Parameters.AddWithValue("@birthDate", student.BirthDate);
            command.Parameters.AddWithValue("@gender", student.Gender);
            command.Parameters.AddWithValue("@class", student.Class);
            command.Parameters.AddWithValue("@point", student.Point);
            command.ExecuteNonQuery();
            _connection.Close();

        }


        public void StudentUpdate(Student student)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Students set Name=@name, Surname=@surname, BirthDate=@birthDate, Gender=@gender,Class=@class, Point=@point where StudentId=@studentId", _connection);
            command.Parameters.AddWithValue("@name", student.Name);
            command.Parameters.AddWithValue("@surname", student.Surname);
            command.Parameters.AddWithValue("@birthDate", student.BirthDate);
            command.Parameters.AddWithValue("@gender", student.Gender);
            command.Parameters.AddWithValue("@class", student.Class);
            command.Parameters.AddWithValue("@point", student.Point);
            command.Parameters.AddWithValue("@studentId", student.StudentId);
            command.ExecuteNonQuery();
            _connection.Close();

        }

        public void StudentDelete(int id)
        {
            ConnectionControl();
            try
            {

                SqlCommand command = new SqlCommand("Delete from Students where StudentId=@id", _connection);

                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Öğrenci silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally
            {
                _connection.Close();
            }


        }

        
        public int CheckStudentForBorrows(int studentId)
        {
            Int32 count = 0;
            ConnectionControl();
            try
            {
                ///SELECT COUNT([BorrowId]) FROM[library].[dbo].[Borrows]where studentid = 360 and BroughtDate > SYSDATETIME();
                SqlCommand command = new SqlCommand("Select Count(BorrowId) from Borrows where StudentId=@id and BroughtDate > @sysDate", _connection);

                command.Parameters.AddWithValue("@id", studentId);
                command.Parameters.AddWithValue("@sysDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm"));

                count = (Int32)command.ExecuteScalar();

            }
            catch (Exception e)
            {
                MessageBox.Show("Öğrenci silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return count;
        }

        public int CheckOfStudentBorrow(int studentId)
        {
            var count = 0;
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("select Count(StudentId) from Borrows where StudentId =@id", _connection);
                command.Parameters.AddWithValue("@id", studentId);
                command.ExecuteNonQuery();
                count = (Int32)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Öğrenci silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally { _connection.Close(); }

            return count;
        }

        
    }
}
