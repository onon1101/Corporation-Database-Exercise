using Xunit;
using Moq;
using MyRestApi.Services;
using MyRestApi.Models;
using System;
using System.Collections.Generic;
using MyRestApi.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace MyRestApi.Tests
{
    public class UnitTest03_TheaterServiceTest
    {

        [Fact]
        public async Task CreateTheaterAsync_SuccessfullyCreatesTheaterAndSeats()
        {
            // Arrange
            var theater = new Theater { Name = "New Theater", TotalSeats = 3 };
            var theaterId = Guid.NewGuid();
            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(repo => repo.CreateTheater(theater))
                .ReturnsAsync(Result<Guid>.Success(theaterId));

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            // Act
            var result = await service.CreateTheaterAsync(theater);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(theaterId, result.Ok);
            mockSeatRepo.Verify(repo => repo.CreateSeat(It.IsAny<Seat>()), Times.Exactly(theater.TotalSeats));
        }


        [Fact]
        public async Task CreateTheaterAsync_FailureToCreateTheater_ReturnsFailure()
        {
            // Arrange
            var theater = new Theater { Name = "Fail Theater", TotalSeats = 5 };
            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();
            var failureResult = Result<Guid>.Fail("Failed to create theater");

            mockRepo
                .Setup(repo => repo.CreateTheater(theater))
                .ReturnsAsync(failureResult);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            // Act
            var result = await service.CreateTheaterAsync(theater);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to create theater", result.Error);
            mockSeatRepo.Verify(repo => repo.CreateSeat(It.IsAny<Seat>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTheaterAsync_ExistingId_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.DeleteTheater(id))
                .ReturnsAsync(true);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.DeleteTheaterAsync(id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTheaterAsync_NonExistingId_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.DeleteTheater(id))
                .ReturnsAsync(false);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.DeleteTheaterAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task GetAllTheatersAsync_ReturnsAllTheaters()
        {
            var theaters = new List<Theater>
    {
        new Theater { Id = Guid.NewGuid(), Name = "A" },
        new Theater { Id = Guid.NewGuid(), Name = "B" }
    };

            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.GetAllTheaters())
                .ReturnsAsync(theaters);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.GetAllTheatersAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, t => t.Name == "A");
            Assert.Contains(result, t => t.Name == "B");
        }

        [Fact]
        public async Task GetTheaterByIdAsync_ExistingId_ReturnsTheater()
        {
            var id = Guid.NewGuid();
            var theater = new Theater { Id = id, Name = "Sample Theater" };

            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.GetTheaterById(id))
                .ReturnsAsync(theater);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.GetTheaterByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("Sample Theater", result.Name);
        }

        [Fact]
        public async Task GetTheaterByIdAsync_NonExistingId_ReturnsNull()
        {
            var id = Guid.NewGuid();

            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.GetTheaterById(id))
                .ReturnsAsync((Theater?)null);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.GetTheaterByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTheaterAsync_ValidData_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var updateData = new Theater { Name = "Updated Theater" };

            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.UpdateTheater(id, updateData))
                .ReturnsAsync(true);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.UpdateTheaterAsync(id, updateData);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateTheaterAsync_Failure_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            var updateData = new Theater { Name = "Fail Update" };

            var mockRepo = new Mock<ITheaterRepository>();
            var mockSeatRepo = new Mock<ISeatRepository>();

            mockRepo
                .Setup(r => r.UpdateTheater(id, updateData))
                .ReturnsAsync(false);

            var service = new TheaterService(mockRepo.Object, mockSeatRepo.Object);

            var result = await service.UpdateTheaterAsync(id, updateData);

            Assert.False(result);
        }

    }
}