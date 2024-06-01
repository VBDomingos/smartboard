using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Repositories
{
    public abstract class DatabaseConnection : IDisposable
    {
        protected SqlConnection connection;

        public DatabaseConnection(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        public void Dispose()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
