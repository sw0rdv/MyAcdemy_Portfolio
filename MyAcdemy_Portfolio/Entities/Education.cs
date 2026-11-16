using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Education
    {
        public int EducationID { get; set; }

        [Required(ErrorMessage = "Okul adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Okul adı en fazla 150 karakter olabilir.")]
        public string Title { get; set; } = "";

        [StringLength(150, ErrorMessage = "Bölüm en fazla 150 karakter olabilir.")]
        public string? SubTitle { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Tarih aralığı zorunludur.")]
        [StringLength(50, ErrorMessage = "Tarih aralığı en fazla 50 karakter olabilir.")]
        public string Duration { get; set; } = "";        // Görünen metin: 2021 - 2025

        [Range(1950, 2100, ErrorMessage = "Başlangıç yılı 1950 ile 2100 arasında olmalıdır.")]
        public int StartYear { get; set; }                 // Tarih sıralaması için

        public bool IsActive { get; set; } = true;          // Pasifse sitede görünmez
    }
}
