namespace MyRestApi.Tests;

public class UnitTest01_UserServices
{
  public UnitTest01_UserServices(TestSetup setup)
  {
    _client = setup.Factory.WithWEbHostBuilder(builder => 
        {
        builder.UseEnvironment("Test");
        }).CreateClient();
  }

  [Fact]
  public async Task {
    var mockUserService = new Mock<> 
  }
}
