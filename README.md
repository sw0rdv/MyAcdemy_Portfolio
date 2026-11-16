<h1 align="center">MyAcdemy_Portfolio</h1>

<p align="center">
  ASP.NET Core MVC ile geliştirilmiş, yönetim paneli üzerinden tamamen güncellenebilen kişisel portföy sitesi.
</p>


<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white" alt=".NET" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=flat-square" alt="ASP.NET Core MVC" />
  <img src="https://img.shields.io/badge/Entity%20Framework-Core-68217A?style=flat-square" alt="EF Core" />
  <img src="https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Bootstrap-5-7952B3?style=flat-square&logo=bootstrap&logoColor=white" alt="Bootstrap" />
</p>

---

## Hakkında

MyAcdemy_Portfolio, statik bir portföy sayfasının ötesine geçerek **içeriğinin tamamı veritabanından beslenen** bir kişisel site projesidir. Projeler, eğitim bilgileri, yetenekler, sertifikalar ve iletişim bilgileri; kod değiştirmeye gerek kalmadan `/Admin` altındaki yönetim panelinden eklenip güncellenebilir.

Ziyaretçiden gelen iletişim formu mesajları veritabanına kaydedilir, panelde okundu/okunmadı durumuyla takip edilir ve dashboard üzerinde istatistik olarak görselleştirilir.

## Öne Çıkan Özellikler

### Ziyaretçi Tarafı
- Dinamik hakkımda, eğitim, deneyim ve yetenek bölümleri
- Kategoriye göre listelenen proje vitrini (GitHub ve canlı demo linkleri ile)
- Sertifika ve başarı listesi
- Veritabanına kayıt düşen iletişim formu

### Yönetim Paneli (`/Admin`)
- **Dashboard** — toplam/okunmuş/okunmamış mesaj sayısı, okunma oranı halka grafiği, proje sayısı, son mesajlar tablosu ve hızlı işlem kısayolları
- **Tam CRUD** — Projeler, Kategoriler, Eğitim, Deneyim, Yetenekler, Sertifikalar ve Hakkımda bölümleri için ekleme / güncelleme / silme
- **Mesaj yönetimi** — gelen mesajları listeleme, detay görüntüleme, okundu olarak işaretleme
- Area tabanlı yapı sayesinde panel ile public site birbirinden tamamen ayrık

## Teknoloji Yığını

| Katman | Teknoloji |
|---|---|
| Framework | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core (Code First) |
| Veritabanı | Microsoft SQL Server |
| Arayüz | Razor Views, Bootstrap 5, Bootstrap Icons |
| Tipografi | Plus Jakarta Sans |
| Mimari | Areas (Admin / Default), Dependency Injection, Tag Helpers |

## Ekran Görüntüleri

### Yönetim Paneli — Dashboard
<img width="2539" height="1301" alt="1" src="https://github.com/user-attachments/assets/21e70a4d-3f36-4e4a-b0ab-5976bb5c30df" />

<img width="2544" height="1306" alt="2" src="https://github.com/user-attachments/assets/a242962d-a89f-4204-a8c7-d0871edfc699" />

### Proje Listesi
<img width="1942" height="1307" alt="5" src="https://github.com/user-attachments/assets/4b5f7532-a11d-4576-8e4c-9a21b8483bfa" />

<img width="2535" height="1300" alt="6" src="https://github.com/user-attachments/assets/a0530cdc-6925-4eda-b442-caf82e756928" />
### Ziyaretçi Ana Sayfa

<img width="1790" height="1245" alt="3" src="https://github.com/user-attachments/assets/77fc9ce0-e13c-45e5-ac35-b8106445ef2f" />

<img width="1748" height="1298" alt="4" src="https://github.com/user-attachments/assets/20cb3415-f71a-491d-a260-3a6ef219a7e8" />
## Veri Modeli

| Tablo | Açıklama |
|---|---|
| `Projects` | Proje adı, açıklama, görsel ve repo/demo linki; `Categories` ile ilişkili |
| `Categories` | Proje kategorileri |
| `Educations` | Okul, bölüm, dönem ve açıklama |
| `Experiences` | İş deneyimleri |
| `Skills` | Yetenekler ve seviye bilgisi |
| `Certificates` | Sertifika adı, veren kurum, yıl ve belge linki |
| `Abouts` | Hakkımda metni ve kişisel bilgiler |
| `Contacts` | Site genelinde gösterilen iletişim bilgileri |
| `Messages` | İletişim formundan gelen mesajlar ve okunma durumu |

## Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server (LocalDB, Express veya tam sürüm)
- Visual Studio 2022 ya da VS Code

### Adımlar

```bash
# 1. Projeyi klonla
git clone https://github.com/sw0rdv/MyAcdemy_Portfolio.git
cd MyAcdemy_Portfolio
```

**2. Bağlantı dizesini ayarla** — `Context/PortfolioContext.cs` (veya `appsettings.json`) içindeki sunucu adını kendi ortamına göre düzenle:

```csharp
optionsBuilder.UseSqlServer(
    "server=(localdb)\\MSSQLLocalDB;database=PortfolioDbNight;integrated security=true;TrustServerCertificate=true;");
```

**3. Veritabanını oluştur**

```bash
dotnet tool install --global dotnet-ef   # kurulu değilse
dotnet ef database update
```

**4. Projeyi çalıştır**

```bash
dotnet run
```

Site `https://localhost:7xxx` adresinde, yönetim paneli `https://localhost:7xxx/Admin` adresinde açılır.

## Proje Yapısı

```
MyAcdemy_Portfolio/
├── Areas/
│   ├── Admin/
│   │   ├── Controllers/     # Project, Education, Skill, Message, About ...
│   │   └── Views/
│   │       ├── AdminLayout/ # Panel layout'u
│   │       ├── Dashboard/   # İstatistik ekranı
│   │       └── ...
│   └── Default/             # Ziyaretçiye açık sayfalar
├── Context/
│   └── PortfolioContext.cs  # DbContext ve DbSet tanımları
├── Entities/                # Project, Category, Education, Skill, Message ...
├── Migrations/              # EF Core migration geçmişi
└── wwwroot/                 # css, js, img, lib
```

## Yol Haritası

- [ ] Panel için kimlik doğrulama (ASP.NET Core Identity)
- [ ] Projelere görsel dosyası yükleme (URL yerine upload)
- [ ] İletişim formu mesajlarında e-posta bildirimi
- [ ] Çok dilli destek (TR / EN)
- [ ] Yayına alma (Azure / IIS)

## İletişim

**Özgür Nuri Vural**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-ozgurnurivural-0A66C2?style=flat-square&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/ozgurnurivural)
[![GitHub](https://img.shields.io/badge/GitHub-Sw0rdV-181717?style=flat-square&logo=github&logoColor=white)](https://github.com/Sw0rdV)
[![Website](https://img.shields.io/badge/Web-ozgurnurivural.com-8276D9?style=flat-square&logo=googlechrome&logoColor=white)](https://ozgurnurivural.com)

---

<p align="center"><sub>Geliştiren <b>Sw0rdV</b></sub></p>
