using Moq;
using MyRestApi.Models;
using MyRestApi.Repositories;
using MyRestApi.Services;

namespace MyRestApi.Tests;

public class UnitTest02_MovieService
{
    [Fact]
    public async Task CreateMovie_ReturnSuccessMessage()
    {
        Guid id = Guid.NewGuid();
        Movie movie = new Movie
        {
            Id = id,
            Title = "testTitle",
            Description = "testDescription",
            Duration = 120,
            Rating = "testRating",
            PosterUrl = "testUrl",
        };

        var mockRepo = new Mock<IMovieRepository>();
        mockRepo
            .Setup(repo => repo.CreateMovie(It.IsAny<Movie>()))
            .ReturnsAsync(id);

        var service = new MovieService(mockRepo.Object);

        var result = await service.CreateMovieAsync(movie);

        Assert.Equal(id, result);
        mockRepo.Verify(repo => repo.CreateMovie(It.Is<Movie>(m => m.Title == "testTitle")), Times.Once);
    }

    [Fact]
    public async Task GetAllMovie_ReturnSuccessMessage()
    {
        Guid id = Guid.NewGuid();
        Movie[] movies =
        {
            new Movie
            {
                Id = id,
                Title = "testTitle",
                Description = "testDescription",
                Duration = 120,
                Rating = "testRating",
                PosterUrl = "testUrl",
            },
            new Movie
            {
                Id = id,
                Title = "testTitle01",
                Description = "testDescription01",
                Duration = 120,
                Rating = "testRating01",
                PosterUrl = "testUrl01",
            }
        };

        var mockRepo = new Mock<IMovieRepository>();
        mockRepo
            .Setup(repo => repo.GetAllMovies())
            .ReturnsAsync(movies);

        var service = new MovieService(mockRepo.Object);

        var result = await service.GetAllMoviesAsync();

        Assert.Equal(movies, result);
        mockRepo.Verify(repo => repo.GetAllMovies(), Times.Once);
    }
    [Fact]
    public async Task GetMovieById_ReturnSuccess()
    {
        Guid id = Guid.NewGuid();
        Movie movie = new Movie
        {
            Id = id,
            Title = "testTitle",
            Description = "testDescription",
            Duration = 120,
            Rating = "testRating",
            PosterUrl = "testUrl",
        };

        var mockRepo = new Mock<IMovieRepository>();
        mockRepo
            .Setup(repo => repo.GetMovieById(id))
            .ReturnsAsync(movie);

        var service = new MovieService(mockRepo.Object);

        var result = await service.GetMovieByIdAsync(id);

        Assert.Equal(movie, result);
        mockRepo.Verify(repo => repo.GetMovieById(id), Times.Once);
    }

    [Fact]
    public async Task UpdateMovie_ReturnSuccessMessage()
    {
        Guid id = Guid.NewGuid();
        Movie movie = new Movie
        {
            Id = id,
            Title = "testTitle",
            Description = "testDescription",
            Duration = 120,
            Rating = "testRating",
            PosterUrl = "testUrl",
        };

        var mockRepo = new Mock<IMovieRepository>();
        mockRepo
            .Setup(repo => repo.UpdateMovie(id, It.IsAny<Movie>()))
            .ReturnsAsync(true);

        var service = new MovieService(mockRepo.Object);

        var result = await service.UpdateMovieAsync(id, movie);

        Assert.True(result);
        mockRepo.Verify(repo => repo.UpdateMovie(id, It.Is<Movie>(m => m.Title == "testTitle")), Times.Once);
    }

    [Fact]
    public async Task DeleteMovie_ReturnSuccessMessage()
    {
        Guid id = Guid.NewGuid();

        var mockRepo = new Mock<IMovieRepository>();
        mockRepo
            .Setup(repo => repo.DeleteMovie(id))
            .ReturnsAsync(true);

        var service = new MovieService(mockRepo.Object);

        var result = await service.DeleteMovieAsync(id);

        Assert.True(result);
        mockRepo.Verify(repo => repo.DeleteMovie(id), Times.Once);
    }
}