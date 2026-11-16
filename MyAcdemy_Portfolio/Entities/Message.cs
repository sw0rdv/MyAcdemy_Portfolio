using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Message
    {
        public int MessageID { get; set; }

        [Required(ErrorMessage = "Lütfen adınızı ve soyadınızı giriniz.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ad soyad 3-100 karakter arasında olmalıdır.")]
        public string NameSurname { get; set; } = "";

        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(150)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Lütfen mesaj konusunu belirtiniz.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Konu 3-150 karakter arasında olmalıdır.")]
        public string Subject { get; set; } = "";

        [Required(ErrorMessage = "Lütfen mesajınızı yazınız.")]
        [StringLength(4000, MinimumLength = 10, ErrorMessage = "Mesaj 10-4000 karakter arasında olmalıdır.")]
        public string MessageDetail { get; set; } = "";

        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
