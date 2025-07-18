using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Moq;
using Moq.Dapper;
using MyRestApi.Models;
using MyRestApi.Repositories;
using Xunit;

namespace MyRestApi.Tests
{
    public class MovieRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDb;
        private readonly MovieRepository _repo;

        public MovieRepositoryTests()
        {
            _mockDb = new Mock<IDbConnection>();
            _repo = new MovieRepository(_mockDb.Object);
        }

        // [Fact]
        // public async Task CreateMovie_ReturnsGuid()
        // {
        //     var expectedId = Guid.NewGuid();
        //     var movie = new Movie { Title = "Test", Description = "Desc", Duration = 120, Rating = "PG", PosterUrl = "url" };

        //     _mockDb.SetupDapperAsync(c => c.ExecuteScalarAsync(It.IsAny<string>(), movie, null, null, null))
        //            .ReturnsAsync(expectedId);

        //     var result = await _repo.CreateMovie(movie);

        //     Assert.Equal(expectedId, result);
        // }

        [Fact]
        public async Task GetAllMovies_ReturnsList()
        {
            var expectedList = new List<Movie> { new Movie { Title = "Test" } };

            _mockDb.SetupDapperAsync(c => c.QueryAsync<Movie>(It.IsAny<string>(), null, null, null, null))
                   .ReturnsAsync(expectedList);

            var result = await _repo.GetAllMovies();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetMovieById_ReturnsMovie()
        {
            var movieId = Guid.NewGuid();
            var expected = new Movie { Id = movieId, Title = "Test" };

            _mockDb.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<Movie>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                   .ReturnsAsync(expected);

            var result = await _repo.GetMovieById(movieId);

            Assert.NotNull(result);
            Assert.Equal(movieId, result!.Id);
        }

        [Fact]
        public async Task UpdateMovie_ReturnsTrue_WhenAffected()
        {
            var movieId = Guid.NewGuid();
            var movie = new Movie { Title = "Test" };

            _mockDb.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                   .ReturnsAsync(1);

            var result = await _repo.UpdateMovie(movieId, movie);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteMovie_ReturnsTrue_WhenAffected()
        {
            var movieId = Guid.NewGuid();

            _mockDb.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                   .ReturnsAsync(1);

            var result = await _repo.DeleteMovie(movieId);

            Assert.True(result);
        }
    }
}