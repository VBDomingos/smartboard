using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public abstract class DatabaseConnection : IDisposable
    {
        protected SqlConnection connection;
        private readonly IConfiguration _configuration;


        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
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
