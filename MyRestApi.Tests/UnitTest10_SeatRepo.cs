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
    public class SeatRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDb;
        private readonly SeatRepository _repo;

        public SeatRepositoryTests()
        {
            _mockDb = new Mock<IDbConnection>();
            _repo = new SeatRepository(_mockDb.Object);
        }

        // [Fact]
        // public async Task CreateSeat_ReturnsGuid()
        // {
        //     // Arrange
        //     var newSeatId = Guid.NewGuid();
        //     var seat = new Seat
        //     {
        //         TheaterId = Guid.NewGuid(),
        //         SeatNumber = "A1"
        //     };

        //     _mockDb.SetupDapperAsync(x =>
        //         x.ExecuteScalarAsync(It.IsAny<string>(), seat, null, null, null))
        //         .ReturnsAsync(newSeatId);

        //     // Act
        //     var result = await _repo.CreateSeat(seat);

        //     // Assert
        //     Assert.Equal(newSeatId, result);
        // }

        [Fact]
        public async Task GetSeatsByTheater_ReturnsSeats()
        {
            // Arrange
            var theaterId = Guid.NewGuid();
            var expectedSeats = new List<Seat>
            {
                new Seat { Id = Guid.NewGuid(), TheaterId = theaterId, SeatNumber = "A1" },
                new Seat { Id = Guid.NewGuid(), TheaterId = theaterId, SeatNumber = "A2" }
            };

            _mockDb.SetupDapperAsync(x =>
                x.QueryAsync<Seat>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(expectedSeats);

            // Act
            var result = await _repo.GetSeatsByTheater(theaterId);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                seat => Assert.Equal("A1", seat.SeatNumber),
                seat => Assert.Equal("A2", seat.SeatNumber));
        }

        [Fact]
        public async Task DeleteSeat_ReturnsTrue_WhenDeleted()
        {
            // Arrange
            _mockDb.SetupDapperAsync(x =>
                x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            var result = await _repo.DeleteSeat(Guid.NewGuid());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSeat_ReturnsFalse_WhenNotDeleted()
        {
            // Arrange
            _mockDb.SetupDapperAsync(x =>
                x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(0);

            // Act
            var result = await _repo.DeleteSeat(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
    }
}