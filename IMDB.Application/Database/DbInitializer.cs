using Dapper;

namespace IMDB.Application.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        public async Task InitializeAsync()
        {
            using var connection = await dbConnectionFactory.CreateConnectionAsync();
            await connection.ExecuteAsync("""
                CREATE TABLE IF NOT EXISTS movies(
                    id UUID primary key,
                    slug TEXT not null,
                    title TEXT not null,
                    yearofrelease INTEGER not null
                );
                """);

            await connection.ExecuteAsync("""
                CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS movies_slug_idx
                ON movies
                USING BTREE(slug);                
                """);
        }
    }
}
