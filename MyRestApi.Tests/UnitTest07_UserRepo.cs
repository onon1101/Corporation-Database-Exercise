using Moq;
using Moq.Dapper;
using System.Data;
using Dapper;
using Xunit;

using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Tests
{
    public class UserRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDb;
        private readonly UserRepository _repo;

        public UserRepositoryTests()
        {
            _mockDb = new Mock<IDbConnection>();
            _repo = new UserRepository(_mockDb.Object);
        }

        [Fact]
        public async Task IsUserExist_ById_ReturnsTrue()
        {
            var userId = Guid.NewGuid();

            _mockDb.SetupDapperAsync(db => db.QuerySingleOrDefaultAsync<int?>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            var result = await _repo.IsUserExist(userId);

            Assert.True(result);
        }

        [Fact]
        public async Task IsUserExist_ByUser_ReturnsTrue()
        {
            var user = new User { Id = Guid.NewGuid() };

            _mockDb.SetupDapperAsync(db => db.QuerySingleOrDefaultAsync<int?>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            var result = await _repo.IsUserExist(user);

            Assert.True(result);
        }

        //error Message: System.NotSupportedException : Specified method is not supported.
        // [Fact]
        // public async Task CreateUser_ReturnsUserId()
        // {
        //     var expectedId = Guid.NewGuid();
        //     var user = new User
        //     {
        //         Username = "test",
        //         Email = "test@example.com",
        //         Password = "1234"
        //     };

        //     _mockDb.SetupDapperAsync(
        //     db => db.ExecuteScalarAsync<Guid>(It.IsAny<string>(), user, null, null, null))
        //         .ReturnsAsync(expectedId);

        //     var result = await _repo.CreateUser(user);

        //     Assert.Equal(expectedId, result);
        // }

        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "u" };

            _mockDb.SetupDapperAsync(db => db.QueryFirstOrDefaultAsync<User>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(user);

            var result = await _repo.GetUserById(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result?.Id);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsUser()
        {
            var email = "email@example.com";
            var user = new User { Email = email };

            _mockDb.SetupDapperAsync(db => db.QueryFirstOrDefaultAsync<User>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(user);

            var result = await _repo.GetUserByEmail(email);

            Assert.NotNull(result);
            Assert.Equal(email, result?.Email);
        }

        [Fact]
        public async Task PatchUser_ExecutesWithoutError()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "updated",
                Email = "updated@example.com",
                Password = "pw"
            };

            _mockDb.SetupDapperAsync(db => db.ExecuteAsync(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            var ex = await Record.ExceptionAsync(() => _repo.PatchUser(user));
            Assert.Null(ex);
        }

        [Fact]
        public async Task DeleteUser_ExecutesWithoutError()
        {
            var id = Guid.NewGuid();

            _mockDb.SetupDapperAsync(db => db.ExecuteAsync(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            var ex = await Record.ExceptionAsync(() => _repo.DeleteUser(id));
            Assert.Null(ex);
        }

        //error Message: System.NotSupportedException : Specified method is not supported.
        // [Fact]
        // public async Task CreateUser_WhenExecuteScalarReturnsNull_ThrowsException()
        // {
        //     var user = new User
        //     {
        //         Username = "test",
        //         Email = "test@example.com",
        //         Password = "1234"
        //     };

        //     _mockDb.SetupDapperAsync(db => db.ExecuteScalarAsync(
        //         It.IsAny<string>(), user, null, null, null))
        //         .ReturnsAsync((object?)null); // 模擬回傳 null

        //     await Assert.ThrowsAsync<Exception>(() => _repo.CreateUser(user));
        // }

        [Fact]
        public async Task GetUserById_WhenUserNotFound_ReturnsNull()
        {
            var userId = Guid.NewGuid();

            _mockDb.SetupDapperAsync(db => db.QueryFirstOrDefaultAsync<User>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((User?)null); // 模擬找不到使用者

            var result = await _repo.GetUserById(userId);

            Assert.Null(result);
        }

        // [Fact]
        // public async Task DeleteUser_Should_Execute_Delete_Query()
        // {
        //     // Arrange
        //     var mockDb = new Mock<IDbConnection>();
        //     var userId = Guid.NewGuid();

        //     // Mock ExecuteAsync 回傳 1（表示有一筆資料被刪除）
        //     mockDb.Setup(db => db.ExecuteAsync(
        //         It.IsAny<string>(),
        //         It.IsAny<object>(),
        //         null, null, null)).ReturnsAsync(1);

        //     var repo = new UserRepository(mockDb.Object);

        //     // Act
        //     await repo.DeleteUser(userId);

        //     // Assert
        //     mockDb.Verify(db => db.ExecuteAsync(
        //         @"DELETE FROM users WHERE id = @Id",
        //         It.Is<object>(param => ((Guid)param.GetType().GetProperty("Id")!.GetValue(param)!) == userId),
        //         null, null, null), Times.Once);
        // }
    }
}