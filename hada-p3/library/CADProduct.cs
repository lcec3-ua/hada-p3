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
        public CADProduct() 
        { 
            constring = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }
        public bool Create(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "INSERT INTO Products (code, name, amount, price, category, creationDate) VALUES (@code, @name, @amount, @price, @category, @creationDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.code);
                    command.Parameters.AddWithValue("@name", en.name);
                    command.Parameters.AddWithValue("@amount", en.amount);
                    command.Parameters.AddWithValue("@price", en.price);
                    command.Parameters.AddWithValue("@category", en.category);
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

        public bool Update(ENProduct en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "UPDATE Products SET name = @name, amount = @amount, price = @price, category = @category, creationDate = @creationDate WHERE code = @code";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@code", en.code);
                    command.Parameters.AddWithValue("@name", en.name);
                    command.Parameters.AddWithValue("@amount", en.amount);
                    command.Parameters.AddWithValue("@price", en.price);
                    command.Parameters.AddWithValue("@category", en.category);
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

        public bool Delete(ENProduct en) 
        { 
        
        }

        public bool Read(ENProduct en) 
        {

        }

        public bool ReadFirst(ENProduct en)
        {

        }

        public bool ReadNext(ENProduct en)
        {

        }

        public bool ReadPrev(ENProduct en)
        {

        }
    }
}
