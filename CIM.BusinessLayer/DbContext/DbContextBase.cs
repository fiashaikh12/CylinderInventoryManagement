using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DbContext
{
    public class DbContextBase
    {
        private IDbConnection _dbContext;

        protected IDbConnection GetConnection()
        {
            return _dbContext = new SqlConnection(ConfigurationManager.AppSettings["dbContext"]);
        }

        protected bool CloseConnection()
        {
            bool IsConnectionClosed = false;
            if (_dbContext.State == ConnectionState.Open)
            {
                _dbContext.Close();
                _dbContext.Dispose();
                IsConnectionClosed = true;
            }
            return IsConnectionClosed;
        }
    }
}
