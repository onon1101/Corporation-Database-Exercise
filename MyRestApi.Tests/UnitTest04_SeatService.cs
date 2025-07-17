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
    public class UnitTest_SeatService
    {
        [Fact]
        public async Task CreateSeatAsync_ValidSeat_ReturnsSeatId()
        {
            // Arrange
            var seat = new Seat
            {
                Id = Guid.NewGuid(),
                TheaterId = Guid.NewGuid(),
                SeatNumber = "A1"
            };
            var mockRepo = new Mock<ISeatRepository>();
            mockRepo.Setup(r => r.CreateSeat(seat)).ReturnsAsync(seat.Id);

            var service = new SeatService(mockRepo.Object);

            // Act
            var result = await service.CreateSeatAsync(seat);

            // Assert
            Assert.Equal(seat.Id, result);
            mockRepo.Verify(r => r.CreateSeat(seat), Times.Once);
        }

        [Fact]
        public async Task GetSeatsByTheaterAsync_ExistingTheater_ReturnsSeatList()
        {
            // Arrange
            var theaterId = Guid.NewGuid();
            var seats = new List<Seat>
            {
                new Seat { Id = Guid.NewGuid(), TheaterId = theaterId, SeatNumber = "A1" },
                new Seat { Id = Guid.NewGuid(), TheaterId = theaterId, SeatNumber = "A2" }
            };
            var mockRepo = new Mock<ISeatRepository>();
            mockRepo.Setup(r => r.GetSeatsByTheater(theaterId)).ReturnsAsync(seats);

            var service = new SeatService(mockRepo.Object);

            // Act
            var result = await service.GetSeatsByTheaterAsync(theaterId);

            // Assert
            Assert.Equal(2, result?.AsList().Count);
            Assert.All(result, s => Assert.Equal(theaterId, s.TheaterId));
        }

        [Fact]
        public async Task DeleteSeatAsync_ExistingSeat_ReturnsTrue()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var mockRepo = new Mock<ISeatRepository>();
            mockRepo.Setup(r => r.DeleteSeat(seatId)).ReturnsAsync(true);

            var service = new SeatService(mockRepo.Object);

            // Act
            var result = await service.DeleteSeatAsync(seatId);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.DeleteSeat(seatId), Times.Once);
        }

        [Fact]
        public async Task DeleteSeatAsync_NonExistingSeat_ReturnsFalse()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var mockRepo = new Mock<ISeatRepository>();
            mockRepo.Setup(r => r.DeleteSeat(seatId)).ReturnsAsync(false);

            var service = new SeatService(mockRepo.Object);

            // Act
            var result = await service.DeleteSeatAsync(seatId);

            // Assert
            Assert.False(result);
        }
    }

    // Extension method for compatibility in older environments
    public static class EnumerableExtensions
    {
        public static List<T> AsList<T>(this IEnumerable<T> source)
        {
            return source is List<T> list ? list : new List<T>(source);
        }
    }
}