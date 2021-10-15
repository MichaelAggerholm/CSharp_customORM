using System;
using System.Data.SqlClient;

namespace ORM
{
    public class Database
    {
        public SqlConnection connection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = ".\\SQLEXPRESS",
                    InitialCatalog = "Test",
                    IntegratedSecurity = true
                };

                return new SqlConnection(builder.ToString());
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}