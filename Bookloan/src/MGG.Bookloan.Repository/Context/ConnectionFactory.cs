using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MGG.Bookloan.Repository.Context
{
    public class ConnectionFactory
    {
        public readonly SqlConnection Conn;
        private readonly object _obj = new object();

        public ConnectionFactory(IConfiguration config)
        {
            if (Conn == null)
            {
                lock (_obj)
                {
                    if (Conn == null)
                        Conn = new SqlConnection(config.GetConnectionString("DefaultConnection"));
                }
            }
        }
    }
}