using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace urun_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UrunlerController : ControllerBase
{
    private readonly SirketDbContext _context;

    public UrunlerController(SirketDbContext context)
    {
        _context = context;
    }

    // GET: api/urunler
    // [AllowAnonymous]: sınıf seviyesindeki [Authorize]'ı bu metot için geçersiz kılar.
    // Böylece React arayüzü, token göndermeden ürün listesini görebilir.
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Urun>>> GetAll()
    {
        var urunler = await _context.Urunler.ToListAsync();
        return Ok(urunler);
    }

    // GET: api/urunler/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Urun>> GetById(int id)
    {
        var urun = await _context.Urunler.FindAsync(id);
        if (urun == null)
        {
            return NotFound(new { mesaj = $"ID {id} olan ürün bulunamadı." });
        }
        return Ok(urun);
    }

    // POST: api/urunler
    [HttpPost]
    public async Task<ActionResult<Urun>> Create(Urun yeniUrun)
    {
        _context.Urunler.Add(yeniUrun);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = yeniUrun.UrunID }, yeniUrun);
    }

    // PUT: api/urunler/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Urun guncelUrun)
    {
        var urun = await _context.Urunler.FindAsync(id);
        if (urun == null)
        {
            return NotFound(new { mesaj = $"ID {id} olan ürün bulunamadı." });
        }

        urun.UrunAdi = guncelUrun.UrunAdi;
        urun.Fiyat = guncelUrun.Fiyat;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/urunler/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var urun = await _context.Urunler.FindAsync(id);
        if (urun == null)
        {
            return NotFound(new { mesaj = $"ID {id} olan ürün bulunamadı." });
        }

        _context.Urunler.Remove(urun);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // ⚠️ GÜVENSİZ - SQL Injection'a açık, sadece öğretim amaçlı
    [HttpGet("ara-guvensiz/{urunAdi}")]
    public IActionResult AraGuvensiz(string urunAdi)
    {
        var connectionString = "Server=localhost;Database=SirketDB;Trusted_Connection=True;TrustServerCertificate=True;";
        var sonuclar = new List<object>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var sorgu = $"SELECT * FROM Urunler WHERE UrunAdi = '{urunAdi}'";
            var command = new SqlCommand(sorgu, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    sonuclar.Add(new
                    {
                        UrunID = reader["UrunID"],
                        UrunAdi = reader["UrunAdi"],
                        Fiyat = reader["Fiyat"]
                    });
                }
            }
        }

        return Ok(sonuclar);
    }

    // ✅ GÜVENLİ - Parametreli sorgu, SQL Injection'a kapalı
    [HttpGet("ara-guvenli/{urunAdi}")]
    public IActionResult AraGuvenli(string urunAdi)
    {
        var connectionString = "Server=localhost;Database=SirketDB;Trusted_Connection=True;TrustServerCertificate=True;";
        var sonuclar = new List<object>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var sorgu = "SELECT * FROM Urunler WHERE UrunAdi = @UrunAdi";
            var command = new SqlCommand(sorgu, connection);
            command.Parameters.AddWithValue("@UrunAdi", urunAdi);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    sonuclar.Add(new
                    {
                        UrunID = reader["UrunID"],
                        UrunAdi = reader["UrunAdi"],
                        Fiyat = reader["Fiyat"]
                    });
                }
            }
        }

        return Ok(sonuclar);
    }
}