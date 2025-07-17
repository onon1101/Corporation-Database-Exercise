using Microsoft.AspNetCore.Hosting;
using Moq;
using MyRestApi.Models;
using MyRestApi.Repositories;

namespace MyRestApi.Tests;

public class UnitTest01_UserServices
{

  [Fact]
  public async Task RegisterUser_ReturnSuccessMessage()
  {
    Guid id = Guid.NewGuid();
    User user = new User
    {
      Id = id,
      Username = "testuser",
      Email = "test@example.com",
      Password = "securepassword"

    };
    var mockRepo = new Mock<IUserRepository>();
    mockRepo
      .Setup(repo => repo.CreateUser(It.IsAny<User>()))
      .ReturnsAsync(id);

    var services = new UserService(mockRepo.Object);

    // Act
    var result = await services.RegisterUserAsync(user);

    // Assert
    Assert.Equal(id, result);
    mockRepo.Verify(repo => repo.CreateUser(It.Is<User>(u => u.Username == "testuser")), Times.Once);
  }

  [Fact]
  public async Task AuthenticateUser_ReturnSuccessMessage()
  {
    Guid id = Guid.NewGuid();
    User user = new User
    {
      Id = id,
      Username = "testuser",
      Email = "test@example.com",
      Password = "securepassword"

    };

    var mockRepo = new Mock<IUserRepository>();
    mockRepo
      .Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
      .ReturnsAsync(user);

    var service = new UserService(mockRepo.Object);

    var result = await service.AuthenticateUserAsync(user.Email, user.Password);

    Assert.Equal(result, user);
    mockRepo.Verify(repo => repo.GetUserByEmail(It.Is<string>(email => email == "test@example.com")), Times.Once);
  }

[Fact]
  public async Task GetUserById_ReturnSuccessMessage()
  {
    Guid id = Guid.NewGuid();
    User user = new User
    {
      Id = id,
      Username = "testuser",
      Email = "test@example.com",
      Password = "securepassword"
    };

    var mockRepo = new Mock<IUserRepository>();
    mockRepo
      .Setup(repo => repo.GetUserById(It.IsAny<Guid>()))
      .ReturnsAsync(user);

    var service = new UserService(mockRepo.Object);
    var result = await service.GetUserById(id);

    Assert.Equal(user, result);
    mockRepo.Verify(repo => repo.GetUserById(It.Is<Guid>(i => i == id)), Times.Once);
  }
}
