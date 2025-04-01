using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace library
{
    public class CADCategory
    {
        private string constring; 

        public CADCategory()
        {
            constring = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public bool Read(ENCategory en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "SELECT * FROM Categories WHERE id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", en.Id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        en.Name = reader["name"].ToString();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Category operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }
        public List<ENCategory> ReadAll()
        {
            List<ENCategory> categories = new List<ENCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string query = "SELECT * FROM Categories";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ENCategory category = new ENCategory();
                        category.Id = Convert.ToInt32(reader["id"]);
                        category.Name = reader["name"].ToString();
                        categories.Add(category);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Category operation has failed. Error: {0}", ex.Message);
            }
            return categories;
        }
    }
}
