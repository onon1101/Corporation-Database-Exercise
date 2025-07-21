using MyRestApi.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyRestApi.WebApi;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly KafkaProducer _kafkaProducer;

    public TestController(KafkaProducer kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    [HttpGet]
    public async Task<IActionResult> GetHelloWorld()
    {
        await _kafkaProducer.SendMovieAsync(new { message = "hello world from Kafka" });
        return Ok(new { message = "hello world" });
    }

    [HttpGet("kafka-test")]
    public async Task<IActionResult> KafkaTest()
    {
        try
        {
            await _kafkaProducer.SendMovieAsync(new { message = "Test Kafka connection" });
            return Ok("Kafka message sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Kafka connection failed: {ex.Message}");
        }
    }
}