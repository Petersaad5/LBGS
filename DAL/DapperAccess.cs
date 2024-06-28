using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class DapperAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;  
        public DapperAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BankConn")!;
        }

        public IEnumerable<T> Query<T>(string storedProcedure, object? parameters)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                try
                {
                    return db.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                 

                    throw new Exception(ex.Message);
                }                
            }
        }

        public int Execute(string storedProcedure, object? parameters)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var affectedRows = db.Execute(storedProcedure, parameters, transaction, commandType: CommandType.StoredProcedure);

                        transaction.Commit();

                        return affectedRows;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
