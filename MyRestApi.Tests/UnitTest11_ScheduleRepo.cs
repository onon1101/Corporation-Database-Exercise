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
    public class ScheduleRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDb;
        private readonly ScheduleRepository _repo;

        public ScheduleRepositoryTests()
        {
            _mockDb = new Mock<IDbConnection>();
            _repo = new ScheduleRepository(_mockDb.Object);
        }

        // [Fact]
        // public async Task CreateSchedule_ReturnsGuid()
        // {
        //     // Arrange
        //     var newId = Guid.NewGuid();
        //     var schedule = new Schedule
        //     {
        //         MovieId = Guid.NewGuid(),
        //         TheaterId = Guid.NewGuid(),
        //         StartTime = DateTime.UtcNow,
        //         EndTime = DateTime.UtcNow.AddHours(2)
        //     };

        //     _mockDb.SetupDapperAsync(x =>
        //         x.ExecuteScalarAsync(It.IsAny<string>(), schedule, null, null, null))
        //         .ReturnsAsync(newId);

        //     // Act
        //     var result = await _repo.CreateSchedule(schedule);

        //     // Assert
        //     Assert.Equal(newId, result);
        // }

        [Fact]
        public async Task GetAllSchedules_ReturnsList()
        {
            // Arrange
            var expected = new List<Schedule>
            {
                new Schedule { Id = Guid.NewGuid(), MovieId = Guid.NewGuid(), TheaterId = Guid.NewGuid(), StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(2) }
            };

            _mockDb.SetupDapperAsync(x =>
                x.QueryAsync<Schedule>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            // Act
            var result = await _repo.GetAllSchedules();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetSchedulesByMovie_ReturnsSchedules()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            var expected = new List<Schedule>
            {
                new Schedule { Id = Guid.NewGuid(), MovieId = movieId, TheaterId = Guid.NewGuid(), StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(2) }
            };

            _mockDb.SetupDapperAsync(x =>
                x.QueryAsync<Schedule>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(expected);

            // Act
            var result = await _repo.GetSchedulesByMovie(movieId);

            // Assert
            Assert.All(result, s => Assert.Equal(movieId, s.MovieId));
        }

        [Fact]
        public async Task DeleteSchedule_ReturnsTrue_WhenDeleted()
        {
            // Arrange
            _mockDb.SetupDapperAsync(x =>
                x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            var result = await _repo.DeleteSchedule(Guid.NewGuid());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSchedule_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            _mockDb.SetupDapperAsync(x =>
                x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(0);

            // Act
            var result = await _repo.DeleteSchedule(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
    }
}