using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace urun_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    // Basitlik için sabit kullanıcı bilgisi (gerçek hayatta veritabanından gelir, şifre hash'lenir)
    private const string SabitKullaniciAdi = "admin";
    private const string SabitSifre = "1234";

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public class GirisBilgisi
    {
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
    }

    [HttpPost("login")]
    public IActionResult Login(GirisBilgisi girisBilgisi)
    {
        if (girisBilgisi.KullaniciAdi != SabitKullaniciAdi || girisBilgisi.Sifre != SabitSifre)
        {
            return Unauthorized(new { mesaj = "Kullanıcı adı veya şifre hatalı." });
        }

        var gizliAnahtar = _configuration["Jwt:GizliAnahtar"]
            ?? throw new InvalidOperationException("Jwt:GizliAnahtar ayarlanmamış.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(gizliAnahtar);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, girisBilgisi.KullaniciAdi)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { token = tokenString });
    }
}