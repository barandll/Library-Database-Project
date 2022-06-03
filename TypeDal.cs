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
    public class TypeDal
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\mtay2;Initial Catalog=library;Integrated Security=True");

        public List<Type> GetAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Select * from Types", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Type> types = new List<Type>();

            while (reader.Read())
            {
                Type type = new Type
                {
                    TypeId = Convert.ToInt32(reader["TypeId"]),
                    Name = reader["Name"].ToString(),


                };
                types.Add(type);
            }

            reader.Close();
            _connection.Close();
            return types;


        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }


        public void TypeAdd(Type type)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Types values(@name)", _connection);
            command.Parameters.AddWithValue("@name", type.Name);
            command.ExecuteNonQuery();
            _connection.Close();

        }

        public void TypeUpdate(Type type)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Types set Name=@name where TypeId=@typeId", _connection);
            command.Parameters.AddWithValue("@name", type.Name);
            command.Parameters.AddWithValue("@typeId", type.TypeId);
            command.ExecuteNonQuery();
            _connection.Close();
        }


        public void TypeDelete(int id)
        {
            ConnectionControl();
            try
            {

                SqlCommand command = new SqlCommand("Delete from Types where TypeId=@id", _connection);

                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Tür silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally
            {
                _connection.Close();
            }


        }

        public int CheckOfTypesBook(int typeId)
        {
            var count = 0;
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("select Count(TypeId) from Books where TypeId =@id", _connection);
                command.Parameters.AddWithValue("@id", typeId);
                command.ExecuteNonQuery();
                count = (Int32)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Tür silme sırasında beklenmeyen hata!." + e.Message);
            }
            finally { _connection.Close(); }

            return count;
        }

    }
}
