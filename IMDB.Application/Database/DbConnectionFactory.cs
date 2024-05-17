using Npgsql;
using System.Data;

namespace IMDB.Application.Database
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
    }
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}