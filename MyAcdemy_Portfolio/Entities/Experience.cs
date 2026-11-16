using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Experience
    {
        public int ExperienceID { get; set; }

        [Required(ErrorMessage = "Pozisyon zorunludur.")]
        [StringLength(100, ErrorMessage = "Pozisyon en fazla 100 karakter olabilir.")]
        public string Title { get; set; } = "";

        [StringLength(100, ErrorMessage = "Şirket adı en fazla 100 karakter olabilir.")]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; } = "";

        [StringLength(100, ErrorMessage = "Alt başlık en fazla 100 karakter olabilir.")]
        public string? SideTitle { get; set; }

        [Required(ErrorMessage = "Tarih aralığı zorunludur.")]
        [StringLength(50, ErrorMessage = "Tarih aralığı en fazla 50 karakter olabilir.")]
        public string Duration { get; set; } = "";

        [Range(1950, 2100, ErrorMessage = "Başlangıç yılı 1950 ile 2100 arasında olmalıdır.")]
        public int StartYear { get; set; }                 // Tarih sıralaması için

        [Required(ErrorMessage = "Çalışma türü zorunludur.")]
        [StringLength(30)]
        public string WorkType { get; set; } = "";

        public bool IsActive { get; set; } = true;          // Pasifse sitede görünmez
    }
}
