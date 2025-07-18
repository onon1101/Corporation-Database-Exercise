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
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _mockRepo;
        private readonly ReservationService _service;

        public ReservationServiceTests()
        {
            _mockRepo = new Mock<IReservationRepository>();
            _service = new ReservationService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateReservationAsync_ReturnsReservationId()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var reservation = new Reservation
            {
                Id = reservationId,
                UserId = Guid.NewGuid(),
                ScheduleId = Guid.NewGuid()
            };
            var seatIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            _mockRepo.Setup(r => r.CreateReservation(reservation, seatIds)).ReturnsAsync(reservationId);

            // Act
            var result = await _service.CreateReservationAsync(reservation, seatIds);

            // Assert
            Assert.Equal(reservationId, result);
            _mockRepo.Verify(r => r.CreateReservation(reservation, seatIds), Times.Once);
        }

        [Fact]
        public async Task GetReservationsByUserAsync_ReturnsUserReservations()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var reservations = new List<Reservation>
            {
                new Reservation { Id = Guid.NewGuid(), UserId = userId },
                new Reservation { Id = Guid.NewGuid(), UserId = userId }
            };

            _mockRepo.Setup(r => r.GetReservationsByUser(userId)).ReturnsAsync(reservations);

            // Act
            var result = await _service.GetReservationsByUserAsync(userId);

            // Assert
            Assert.Equal(2, result is not null ? result.Count() : 0);
            _mockRepo.Verify(r => r.GetReservationsByUser(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteReservationAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteReservation(reservationId)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteReservationAsync(reservationId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteReservation(reservationId), Times.Once);
        }
    }
}