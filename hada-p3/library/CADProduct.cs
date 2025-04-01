using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace library
{
    public class CADProduct
    {
        private string constring;

        // INICIALIZAR LA CADENA DE CONEXION A LA BD
        public CADProduct() 
        { 
            constring = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        // CREAR NUEVO PRODUCTO EN LA BD
        public bool Create(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "INSERT INTO Products (code, name, amount, price, category, creationDate) VALUES (@code, @name, @amount, @price, @category, @creationDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.Code);
                    command.Parameters.AddWithValue("@name", en.Name);
                    command.Parameters.AddWithValue("@amount", en.Amount);
                    command.Parameters.AddWithValue("@price", en.Price);
                    command.Parameters.AddWithValue("@category", en.Category);
                    command.Parameters.AddWithValue("@creationDate", en.CreationDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // ACTUALIZA DATOS DE UN PRODUCTO CON LOS DATOS DE en.
        public bool Update(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "UPDATE Products SET name = @name, amount = @amount, price = @price, category = @category, creationDate = @creationDate WHERE code = @code";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.Code);
                    command.Parameters.AddWithValue("@name", en.Name);
                    command.Parameters.AddWithValue("@amount", en.Amount);
                    command.Parameters.AddWithValue("@price", en.Price);
                    command.Parameters.AddWithValue("@category", en.Category);
                    command.Parameters.AddWithValue("@creationDate", en.CreationDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // BORRAR PRODUCTO INDICADO
        public bool Delete(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "DELETE FROM Products WHERE code = @code";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.Code);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // DEVUELVE PRODUCTO LELIDO DE LA BD
        public bool Read(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "SELECT * FROM Products WHERE code = @code";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.Code);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        en.Name = reader["name"].ToString();
                        en.Amount = Convert.ToInt32(reader["amount"]);
                        en.Price = Convert.ToSingle(reader["price"]);
                        en.Category = Convert.ToInt32(reader["category"]);
                        en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // DEVUELVE SOLO EL PRIMER PRODUCTO INDICADO LEIDO DE LA BD
        public bool ReadFirst(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "SELECT TOP 1 * FROM Products ORDER BY id ASC";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        en.Code = reader["code"].ToString();
                        en.Name = reader["name"].ToString();
                        en.Amount = Convert.ToInt32(reader["amount"]);
                        en.Price = Convert.ToSingle(reader["price"]);
                        en.Category = Convert.ToInt32(reader["category"]);
                        en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // DEVUELVE SOLO EL PRODUCTO SIGUIENTE AL INDICADO
        public bool ReadNext(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "SELECT TOP 1 * FROM Products WHERE id > (SELECT id FROM Products WHERE code = @code) ORDER BY id ASC";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.Code);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        en.Code = reader["code"].ToString();
                        en.Name = reader["name"].ToString();
                        en.Amount = Convert.ToInt32(reader["amount"]);
                        en.Price = Convert.ToSingle(reader["price"]);
                        en.Category = Convert.ToInt32(reader["category"]);
                        en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // DEVUELVE SOLO EL PRODUCTO ANTERIOR AL INDICADO
        public bool ReadPrev(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "SELECT TOP 1 * FROM Products WHERE id < (SELECT id FROM Products WHERE code = @code) ORDER BY id DESC";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.Code);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        en.Code = reader["code"].ToString();
                        en.Name = reader["name"].ToString();
                        en.Amount = Convert.ToInt32(reader["amount"]);
                        en.Price = Convert.ToSingle(reader["price"]);
                        en.Category = Convert.ToInt32(reader["category"]);
                        en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }
    }
}