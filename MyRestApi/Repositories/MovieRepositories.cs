using System.Data;
using Dapper;
using MyRestApi.Models;

namespace MyRestApi.Repositories;

public class MovieRepository: IMovieRepository
{
    private readonly IDbConnection _db;

    public MovieRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task AddMovie(Movie movie)
    {
        const string sql = @"INSERT INTO movies (title, description, duration, rating, poster_url)
        VALUES (@Title, @Description, @Duration, @Rating, @PosterUrl)";

        await _db.ExecuteAsync(sql, movie);
    }

    public async Task PatchMovie(Movie movie)
    {
        const string sql = @"
            UPDATE movies
            SET title = @Title,
                description = @Description,
                duration = @Duration,
                rating = @Rating,
                poster_url = @PosterUrl
            WHERE id = @Id";

        await _db.ExecuteAsync(sql, movie);
    }

    public async Task<List<Movie>?> GetAllMovies()
    {
        const string sql = @"SELECT * FROM movies";
        return (await _db.QueryAsync<Movie>(sql)).ToList();
    }

    public async Task<Movie?> GetMovieById(int idx)
    {
        const string sql = @"SELECT * FROM movies WHERE id = @Id";
        return await _db.QuerySingleOrDefaultAsync<Movie>(sql, new { Id = idx });
    }

    public async Task DeleteMovieById(int idx)
    {
        const string sql = @"DELETE FROM movies WHERE id = @Id";
        await _db.ExecuteAsync(sql, new { Id = idx });
    }
}