using Dapper;
using MyRestApi.Models;
using System.Data;

namespace MyRestApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDbConnection _db;

        public MovieRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Guid> CreateMovie(Movie movie)
        {
            const string sql = @"
                INSERT INTO movies (title, description, duration, rating, poster_url)
                VALUES (@Title, @Description, @Duration, @Rating, @PosterUrl)
                RETURNING id;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Title", movie.Title, DbType.String);
            parameters.Add("Description", movie.Description, DbType.String);
            parameters.Add("Duration", movie.Duration, DbType.Int32);
            parameters.Add("Rating", movie.Rating, DbType.String);
            parameters.Add("PosterUrl", movie.PosterUrl, DbType.String);

            return (Guid)(await _db.ExecuteScalarAsync(sql, parameters))!;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            const string sql = "SELECT * FROM movies;";
            return await _db.QueryAsync<Movie>(sql);
        }

        public async Task<Movie?> GetMovieById(Guid id)
        {
            const string sql = "SELECT * FROM movies WHERE id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            return await _db.QueryFirstOrDefaultAsync<Movie>(sql, parameters);
        }

        public async Task<bool> UpdateMovie(Guid id, Movie movie)
        {
            const string sql = @"
                UPDATE movies
                SET title = COALESCE(@Title, title),
                    description = COALESCE(@Description, description),
                    duration = COALESCE(@Duration, duration),
                    rating = COALESCE(@Rating, rating),
                    poster_url = COALESCE(@PosterUrl, poster_url)
                WHERE id = @Id;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Title", movie.Title, DbType.String);
            parameters.Add("Description", movie.Description, DbType.String);
            parameters.Add("Duration", movie.Duration, DbType.Int32);
            parameters.Add("Rating", movie.Rating, DbType.String);
            parameters.Add("PosterUrl", movie.PosterUrl, DbType.String);

            return await _db.ExecuteAsync(sql, parameters) > 0;
        }

        public async Task<bool> DeleteMovie(Guid id)
        {
            const string sql = "UPDATE movies SET is_deleted = TRUE WHERE id = @Id;";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            return await _db.ExecuteAsync(sql, parameters) > 0;
        }
    }
}