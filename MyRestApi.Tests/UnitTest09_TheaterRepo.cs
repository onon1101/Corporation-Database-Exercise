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
    public class TheaterRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDb;
        private readonly TheaterRepository _repo;

        public TheaterRepositoryTests()
        {
            _mockDb = new Mock<IDbConnection>();
            _repo = new TheaterRepository(_mockDb.Object);
        }

        // [Fact]
        // public async Task CreateTheater_ReturnsSuccess_WhenNotExists()
        // {
        //     var theater = new Theater
        //     {
        //         Name = "Test",
        //         Location = "Taipei",
        //         TotalSeats = 200
        //     };
        //     var newId = Guid.NewGuid();

        //     _mockDb.SetupDapperAsync(c =>
        //         c.ExecuteScalarAsync<Guid?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
        //         .ReturnsAsync((Guid?)null);

        //     _mockDb.SetupDapperAsync(c =>
        //         c.ExecuteScalarAsync(It.IsAny<string>(), theater, null, null, null))
        //         .ReturnsAsync(newId);

        //     Result<Guid> result = await _repo.CreateTheater(theater);

        //     Assert.True(result.IsSuccess);
        //     Assert.Equal(newId, result.Ok);
        // }

        // [Fact]
        // public async Task CreateTheater_ReturnsFail_WhenExists()
        // {
        //     var theater = new Theater
        //     {
        //         Name = "Test",
        //         Location = "Taipei",
        //         TotalSeats = 200
        //     };

        //     var existingId = Guid.NewGuid();

        //     _mockDb.SetupDapperAsync(c =>
        //         c.ExecuteScalarAsync<Guid?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
        //         .ReturnsAsync(existingId);

        //     var result = await _repo.CreateTheater(theater);

        //     Assert.False(result.IsSuccess);
        //     Assert.Equal("[Theater] the theater is existed.", result.Error);
        // }

        [Fact]
        public async Task DeleteTheater_ReturnsTrue_WhenRowDeleted()
        {
            _mockDb.SetupDapperAsync(c =>
                c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            var result = await _repo.DeleteTheater(Guid.NewGuid());

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllTheaters_ReturnsList()
        {
            var expected = new List<Theater>
            {
                new Theater { Name = "Test", Location = "City", TotalSeats = 100 }
            };

            _mockDb.SetupDapperAsync(c =>
                c.QueryAsync<Theater>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            var result = await _repo.GetAllTheaters();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetTheaterById_ReturnsTheater()
        {
            var id = Guid.NewGuid();
            var expected = new Theater { Id = id, Name = "Test" };

            _mockDb.SetupDapperAsync(c =>
                c.QueryFirstOrDefaultAsync<Theater>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(expected);

            var result = await _repo.GetTheaterById(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task UpdateTheater_ReturnsTrue_WhenUpdated()
        {
            var id = Guid.NewGuid();
            var theater = new Theater { Name = "NewName", Location = "NewPlace", TotalSeats = 300 };

            _mockDb.SetupDapperAsync(c =>
                c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            var result = await _repo.UpdateTheater(id, theater);

            Assert.True(result);
        }
    }
}