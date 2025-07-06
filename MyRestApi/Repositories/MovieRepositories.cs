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
            return (Guid)(await _db.ExecuteScalarAsync(sql, movie))!;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            const string sql = "SELECT * FROM movies;";
            return await _db.QueryAsync<Movie>(sql);
        }

        public async Task<Movie?> GetMovieById(Guid id)
        {
            const string sql = "SELECT * FROM movies WHERE id = @Id;";
            return await _db.QueryFirstOrDefaultAsync<Movie>(sql, new { Id = id });
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
            return await _db.ExecuteAsync(sql, new
            {
                Id = id,
                movie.Title,
                movie.Description,
                movie.Duration,
                movie.Rating,
                movie.PosterUrl
            }) > 0;
        }

        public async Task<bool> DeleteMovie(Guid id)
        {
            const string sql = "DELETE FROM movies WHERE id = @Id;";
            return await _db.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}