using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Event_Manager.Dapper
{
    public class DapperContext
    {
        //@ConfigurationSettings@
        //check the connection string name in appsettings.JSON
        private const string connectionStringName = "EventManager@SQLServer";

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = GetConnetionString();
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);


        private string GetConnetionString()
        {
            return _configuration.GetConnectionString(connectionStringName) 
                //if connection string is not configurated go in exception
                ?? throw new Exception("Connection string not found in appsettings.json");
        }
    }
}
