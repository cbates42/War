using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Services.Logger
{
    public class DbLogger
    {
        Service service = new Service();
        private string _sqlConnString;

        public DbLogger()
        {
            PrepConnection();
        }
        public void PrepConnection()
        {
            SqlConnectionStringBuilder connBldr = new SqlConnectionStringBuilder();
            connBldr.DataSource = $"(localdb)\\MSSQLLocalDB";
            connBldr.IntegratedSecurity = true;
            connBldr.InitialCatalog = $"PROG455SP23";
            _sqlConnString = connBldr.ConnectionString;
        }

        public void LogError(string message, Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                string query = $"Insert into dbo.logger (LogLevel,LogMessage,Exception)" +
                $"VALUES( 'Error','{message}','{ex}')";

                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void LogInfo(string message)
        {
            string query = $"Insert into dbo.logger (LogLevel,LogMessage)" +
                $"VALUES( 'Information','{message}')";

            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void LogWarning(string message)
        {
            string query = $"Insert into dbo.logger (LogLevel,LogMessage)" +
                $"VALUES( 'Warning','{message}')";

            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
        }

    }
}
