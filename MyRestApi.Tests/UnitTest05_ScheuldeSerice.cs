using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MyRestApi.Models;
using MyRestApi.Repositories;
using MyRestApi.Services;

namespace MyRestApi.Tests
{
    public class ScheduleServiceTests
    {
        private readonly Mock<IScheduleRepository> _mockRepo;
        private readonly ScheduleService _service;

        public ScheduleServiceTests()
        {
            _mockRepo = new Mock<IScheduleRepository>();
            _service = new ScheduleService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateScheduleAsync_ReturnsScheduleId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var schedule = new Schedule { Id = id };
            _mockRepo.Setup(r => r.CreateSchedule(schedule)).ReturnsAsync(id);

            // Act
            var result = await _service.CreateScheduleAsync(schedule);

            // Assert
            Assert.Equal(id, result);
            _mockRepo.Verify(r => r.CreateSchedule(schedule), Times.Once);
        }

        [Fact]
        public async Task GetAllSchedulesAsync_ReturnsAllSchedules()
        {
            // Arrange
            var schedules = new List<Schedule>
            {
                new Schedule { Id = Guid.NewGuid() },
                new Schedule { Id = Guid.NewGuid() }
            };
            _mockRepo.Setup(r => r.GetAllSchedules()).ReturnsAsync(schedules);

            // Act
            var result = await _service.GetAllSchedulesAsync();

            // Assert
            Assert.Equal(2, result?.ToList().Count);
            _mockRepo.Verify(r => r.GetAllSchedules(), Times.Once);
        }

        [Fact]
        public async Task GetSchedulesByMovieAsync_ReturnsSchedules()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            var schedules = new List<Schedule>
            {
                new Schedule { Id = Guid.NewGuid(), MovieId = movieId }
            };
            _mockRepo.Setup(r => r.GetSchedulesByMovie(movieId)).ReturnsAsync(schedules);

            // Act
            var result = await _service.GetSchedulesByMovieAsync(movieId);

            // Assert
            Assert.Single(result);
            Assert.Equal(movieId, result.First().MovieId);
            _mockRepo.Verify(r => r.GetSchedulesByMovie(movieId), Times.Once);
        }

        [Fact]
        public async Task DeleteScheduleAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteSchedule(id)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteScheduleAsync(id);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteSchedule(id), Times.Once);
        }
    }
}