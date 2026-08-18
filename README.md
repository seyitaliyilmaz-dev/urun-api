# Ürün Yönetimi API

ASP.NET Core Web API ve Entity Framework Core kullanılarak geliştirilmiş, SQL Server veritabanına bağlı bir ürün yönetim servisi.

## Özellikler

- **CRUD İşlemleri:** Ürünler için Ekle, Oku, Güncelle, Sil endpoint'leri
- **JWT Kimlik Doğrulama:** Token tabanlı güvenli erişim, `/api/auth/login` üzerinden giriş
- **RESTful Tasarım:** Doğru HTTP metotları (GET, POST, PUT, DELETE) ve durum kodları (200, 201, 204, 404, 401)
- **Güvenli Kodlama:** SQL Injection'a karşı parametreli sorgu kullanımı, güvenli/güvensiz örneklerle karşılaştırmalı yapı
- **Servisler Arası Entegrasyon:** `HttpClient` ile bağımsız bir dış servise (plaka tanıma sistemi) istek atma örneği
- **API Dokümantasyonu:** Swagger/OpenAPI ile otomatik, tıklanabilir dokümantasyon (`/swagger`)
- **Test Senaryoları:** Postman koleksiyonu ile 8 farklı senaryo (başarılı/başarısız giriş, yetkili/yetkisiz erişim, tam CRUD döngüsü)

## Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- JWT (JSON Web Token)
- Swashbuckle (Swagger/OpenAPI)

## Çalıştırma

```bash
dotnet restore
dotnet run
```

Çalıştıktan sonra API dokümantasyonuna şu adresten ulaşabilirsiniz: http://localhost:5200/swagger

## API Test Senaryoları (Postman)

`urun-api-postman-collection.json` dosyası, Postman'e import edilebilecek 8 test senaryosu içerir:

1. Login - Başarılı Giriş
2. Login - Hatalı Şifre
3. Ürünleri Listele - Token Yok (401 beklenir)
4. Ürünleri Listele - Token Var (200 beklenir)
5. Ürün Ekle (201 beklenir)
6. Ürün Getir - Bulunamadı (404 beklenir)
7. Ürün Güncelle (204 beklenir)
8. Ürün Sil (204 beklenir)

Postman'de **Import** butonuyla bu dosyayı içe aktararak tüm senaryoları çalıştırabilirsiniz.

## Güvenlik Notu

Bu proje bir öğrenme/staj çalışmasıdır. JWT gizli anahtarı gibi hassas bilgiler `appsettings.json` üzerinden okunmaktadır; gerçek bir üretim ortamında bu tür değerlerin ortam değişkenleri (environment variables) veya bir secret manager (örn. Azure Key Vault) üzerinden yönetilmesi önerilir.