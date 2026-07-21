using Microsoft.AspNetCore.Mvc;

namespace urun_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GirisKontrolController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public GirisKontrolController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("arac/{plakaNo}")]
    public async Task<IActionResult> AracGirisiKontrolEt(string plakaNo)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"http://localhost:5100/api/plaka/sorgula/{plakaNo}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(502, new { mesaj = "Plaka Tanıma Sistemi'nden geçersiz yanıt alındı." });
            }

            var icerik = await response.Content.ReadAsStringAsync();
            return Content(icerik, "application/json");
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, new { mesaj = "Plaka Tanıma Sistemi şu anda erişilemez durumda." });
        }
    }
}
