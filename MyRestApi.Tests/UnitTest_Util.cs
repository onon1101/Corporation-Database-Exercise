using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;
using MyRestApi.Tests;

namespace MyRestApi.Tests;

public class UnitTest_Util : IClassFixture<WebApplicationFactory<Program>>
{

    // post
    public static async Task<T> GetResponse<T>(HttpClient client, string url, object payload)
    {
        var response = await client.PostAsJsonAsync(url, payload);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
    // get
    public static async Task<T> GetResponse<T>(HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

}