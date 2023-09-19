using NewAvalon.Abstractions.Data;
using NewAvalon.Abstractions.ServiceLifetimes;
using Npgsql;
using System.Data;

namespace NewAvalon.Persistence.Factories
{
    public sealed class SqlConnectionFactory : ISqlConnectionFactory, ITransient
    {
        public IDbConnection GetOpenConnection(string connectionString)
        {
            var dbConnection = new NpgsqlConnection(connectionString);

            dbConnection.Open();

            return dbConnection;
        }
    }
}
