using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class SocialMedia
    {
        public int SocialMediaID { get; set; }

        [Required(ErrorMessage = "Platform adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Platform adı en fazla 50 karakter olabilir.")]
        public string PlatformName { get; set; } = "";     // Örn: GitHub

        [Required(ErrorMessage = "Profil adresi zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir adres giriniz (https://...).")]
        [StringLength(300)]
        public string Url { get; set; } = "";

        [Required(ErrorMessage = "İkon seçiniz.")]
        [StringLength(50)]
        public string IconClass { get; set; } = "bi-link-45deg";   // Bootstrap Icons sınıfı, örn: bi-github

        [Range(0, 100, ErrorMessage = "Sıra 0 ile 100 arasında olmalıdır.")]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;          // Pasifse sitede görünmez
    }
}
