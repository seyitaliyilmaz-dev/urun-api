# Ürün Yönetimi API

ASP.NET Core Web API ve Entity Framework Core kullanılarak geliştirilmiş, SQL Server veritabanına bağlı bir ürün yönetim servisi.

## Özellikler

- **CRUD İşlemleri:** Ürünler için Ekle, Oku, Güncelle, Sil endpoint'leri
- **JWT Kimlik Doğrulama:** Token tabanlı güvenli erişim, `/api/auth/login` üzerinden giriş
- **RESTful Tasarım:** Doğru HTTP metotları (GET, POST, PUT, DELETE) ve durum kodları (200, 201, 204, 404, 401)
- **Güvenli Kodlama:** SQL Injection'a karşı parametreli sorgu kullanımı, güvenli/güvensiz örneklerle karşılaştırmalı yapı
- **Servisler Arası Entegrasyon:** `HttpClient` ile bağımsız bir dış servise (plaka tanıma sistemi) istek atma örneği

## Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- JWT (JSON Web Token)

## Çalıştırma

```bash
dotnet restore
dotnet run
```

