using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer.DbContext
{
    public class DbContextBase
    {
        private static IDbConnection _dbContext;
        public static IDbConnection GetConnection()
        {
            return _dbContext = new SqlConnection(ConfigurationManager.AppSettings["dbContext"]);
        }
    }
}
