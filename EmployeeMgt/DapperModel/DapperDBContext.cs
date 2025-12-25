using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeMgt.DapperModel
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;  
        public DapperDBContext(IConfiguration configuration ) 
        {
            _configuration = configuration;
            this._connectionString = _configuration.GetConnectionString("DefaultConnection");   
        }
        public IDbConnection CreateConection() =>new SqlConnection(_connectionString);

        /// <summary>
        /// Creates and opens a SqlConnection asynchronously. Returns an open SqlConnection.
        /// Caller must dispose the returned connection when finished.
        /// </summary>
        public async Task<SqlConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);
            return sqlConnection;
        }

    }
}
