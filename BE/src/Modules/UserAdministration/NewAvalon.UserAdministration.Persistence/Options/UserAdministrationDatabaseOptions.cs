using Npgsql;

namespace NewAvalon.UserAdministration.Persistence.Options
{
    public sealed class UserAdministrationDatabaseOptions
    {
        public string Username { get; init; }

        public string Password { get; init; }

        public string Host { get; init; }

        public int Port { get; init; }

        public string Database { get; init; }

        public string GetConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Username = Username,
                Password = Password,
                Host = Host,
                Port = Port,
                Database = Database
            };

            return builder.ConnectionString;
        }
    }
}
