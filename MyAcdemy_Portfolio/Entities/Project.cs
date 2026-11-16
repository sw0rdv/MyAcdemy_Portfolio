using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Proje adı en fazla 100 karakter olabilir.")]
        public string ProjectName { get; set; } = "";

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string ProjectDescription { get; set; } = "";

        [Url(ErrorMessage = "Geçerli bir adres giriniz (https://...).")]
        public string? ProjectUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir adres giriniz (https://...).")]
        public string? GithubUrl { get; set; }

        public string? ProjectImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign key + navigation property (bire-çok)
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
