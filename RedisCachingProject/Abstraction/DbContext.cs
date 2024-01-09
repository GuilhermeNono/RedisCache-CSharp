using System.Data;
using System.Data.SqlClient;

namespace RedisCachingProject.Abstraction;

public class DbContext: IDbContext
{
    
    private IDbConnection? _connection;
    private readonly string _connectionName = "SqlServerConfiguration";
    private readonly string _connectionString;

    public IDbConnection Connection => Instance();

    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(_connectionName) ??
                            throw new ArgumentNullException(
                                $"Could not load configuration {_connectionName}, see your appsetting file");;
    }

    private IDbConnection Instance()
    {
        _connection ??= new SqlConnection(_connectionString);
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }
}