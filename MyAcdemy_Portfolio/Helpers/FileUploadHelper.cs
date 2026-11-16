namespace MyAcdemy_Portfolio.Helpers
{
    /// <summary>
    /// wwwroot/uploads altına güvenli dosya kaydetme.
    /// - Uzantı ve içerik türü beyaz listeyle kontrol edilir
    /// - Boyut sınırı uygulanır
    /// - Dosya adı kullanıcıdan alınmaz, Guid ile benzersiz üretilir (path traversal engellenir)
    /// - Dosyanın ilk baytları (magic number) gerçek türü doğrulamak için okunur
    /// </summary>
    public static class FileUploadHelper
    {
        public static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        public const long MaxImageSize = 2 * 1024 * 1024;   // 2 MB
        public const long MaxPdfSize = 5 * 1024 * 1024;     // 5 MB

        /// <summary>Görseli doğrular; hata varsa mesajını döner, yoksa null.</summary>
        public static string? ValidateImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!ImageExtensions.Contains(ext))
                return "Sadece JPG, PNG veya WEBP görsel yükleyebilirsiniz.";
            if (!file.ContentType.StartsWith("image/"))
                return "Yüklenen dosya bir görsel değil.";
            if (file.Length == 0 || file.Length > MaxImageSize)
                return "Görsel boyutu en fazla 2 MB olabilir.";
            if (!HasImageSignature(file))
                return "Dosya içeriği geçerli bir görsel değil.";
            return null;
        }

        /// <summary>PDF'i doğrular; hata varsa mesajını döner, yoksa null.</summary>
        public static string? ValidatePdf(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (ext != ".pdf" || file.ContentType != "application/pdf")
                return "Sadece PDF dosyası yükleyebilirsiniz.";
            if (file.Length == 0 || file.Length > MaxPdfSize)
                return "PDF boyutu en fazla 5 MB olabilir.";

            // PDF dosyaları "%PDF" ile başlar
            var header = ReadHeader(file, 4);
            if (header.Length < 4 || header[0] != 0x25 || header[1] != 0x50 || header[2] != 0x44 || header[3] != 0x46)
                return "Dosya içeriği geçerli bir PDF değil.";
            return null;
        }

        /// <summary>Dosyayı wwwroot/uploads/{folder} altına benzersiz adla kaydeder, site içi yolunu döner.</summary>
        public static async Task<string> SaveAsync(IFormFile file, IWebHostEnvironment env, string folder, string prefix)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var directory = Path.Combine(env.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(directory);

            var fileName = $"{prefix}_{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(directory, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{fileName}";
        }

        /// <summary>Daha önce bu yardımcıyla yüklenmiş bir dosyayı siler (dış URL'lere dokunmaz).</summary>
        public static void DeleteIfLocal(string? webPath, IWebHostEnvironment env)
        {
            if (string.IsNullOrEmpty(webPath) || !webPath.StartsWith("/uploads/")) return;

            var uploadsRoot = Path.GetFullPath(Path.Combine(env.WebRootPath, "uploads"));
            var fullPath = Path.GetFullPath(Path.Combine(env.WebRootPath, webPath.TrimStart('/')));

            // Güvenlik: yalnızca uploads klasörünün içindeki dosyalar silinebilir
            if (fullPath.StartsWith(uploadsRoot) && File.Exists(fullPath))
                File.Delete(fullPath);
        }

        private static bool HasImageSignature(IFormFile file)
        {
            var h = ReadHeader(file, 12);
            if (h.Length < 4) return false;
            bool jpg = h[0] == 0xFF && h[1] == 0xD8 && h[2] == 0xFF;
            bool png = h[0] == 0x89 && h[1] == 0x50 && h[2] == 0x4E && h[3] == 0x47;
            bool webp = h.Length >= 12 && h[0] == 0x52 && h[1] == 0x49 && h[2] == 0x46 && h[3] == 0x46
                        && h[8] == 0x57 && h[9] == 0x45 && h[10] == 0x42 && h[11] == 0x50;
            return jpg || png || webp;
        }

        private static byte[] ReadHeader(IFormFile file, int count)
        {
            using var stream = file.OpenReadStream();
            var buffer = new byte[count];
            int read = stream.Read(buffer, 0, count);
            return buffer.Take(read).ToArray();
        }
    }
}
