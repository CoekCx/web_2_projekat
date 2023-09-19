using System.Data;

namespace NewAvalon.Abstractions.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection(string connectionString);
    }
}
