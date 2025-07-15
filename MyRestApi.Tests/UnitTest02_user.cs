using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;
using Moq;
using MyRestApi.Tests;
using MyRestApi.DTO;
using Microsoft.Identity.Client;



namespace MyRestApi.Tests;
 
[Collection("SharedTestCollection")]
public class UnitTest02_user
{


    private readonly HttpClient _client;
    private readonly UnitTest_Util _util;

    public UnitTest02_user(TestSetup setup)
    {
        _client = setup.Factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test"); // 設定為 Test 環境
        }).CreateClient();
    }

    [Fact]
    public async Task RegisterUser_ReturnSuccessMessage()
    {

    }

    [Fact]
    public async Task RegisterUser_ReturnSuccessMessage()
    {
        var payload = new
        {
            username = "firstuser",
            email = "test@test.com",
            password = "test123",
        };

        var response = await UnitTest_Util.GetResponse<UserResponseDTO>(_client, "/api/user/register", payload);
        Assert.Equal(payload.username, response.Username);
        Assert.Equal(payload.email, response.Email);
    }

    // [Fact]
    // public async Task LoginUser_ReturnSuccessMessage()
    // {
    //
    //     // register user
    //     var payloadReg = new
    //     {
    //         username = "firstuser01",
    //         email = "test01@test.com",
    //         password = "test123",
    //     };
    //
    //     await UnitTest_Util.GetResponse<UserResponseDTO>(_client, "/api/user/register", payloadReg);
    //
    //
    //     // login
    //     var payload = new
    //     {
    //         email = "test01@test.com",
    //         password = "test123",
    //
    //     };
    //
    //     var response = await UnitTest_Util.GetResponse<UserResponseDTO>(_client, "/api/user/login", payload);
    //     Assert.Equal(payload.email, response.Email);
    //     Assert.Equal("firstuser01", response.Username);
    // }
    //
    // [Fact]
    // public async Task GetUserInfo_ReturnSuccessMessage()
    // {
    //     // register user
    //     var payloadReg = new
    //     {
    //         username = "firstuser02",
    //         email = "test02@test.com",
    //         password = "test123",
    //     };
    //
    //     var responseReg = await UnitTest_Util.GetResponse<UserResponseDTO>(_client, "/api/user/register", payloadReg);
    //
    //     // profile
    //     var response = await _client.GetAsync($"/api/user/profile/{responseReg.Id}");
    //     response.EnsureSuccessStatusCode();
    //
    //     var userResponse = await response.Content.ReadFromJsonAsync<UserResponseDTO>();
    //     Assert.NotNull(userResponse);
    //     Assert.Equal(responseReg.Id, userResponse.Id);
    //     Assert.Equal("firstuser02", userResponse.Username);
    //     Assert.Equal("test02@test.com", userResponse.Email);
    // }
}
