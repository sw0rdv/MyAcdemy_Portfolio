namespace MyAcdemy_Portfolio.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }
        public string Title { get; set; }          // Ad Soyad (Merhaba, Ben ...)
        public string SubTitle { get; set; }       // Rozet yazısı / unvan
        public string Description { get; set; }    // Açıklama paragrafı
        public string ImageUrl { get; set; }       // Profil fotoğrafı

        public string? TypingTexts { get; set; }   // Yazı animasyonu cümleleri (her satır bir cümle)

        public string? Card1Title { get; set; }    // Sol üst yüzen kart
        public string? Card1SubTitle { get; set; }
        public string? Card2Title { get; set; }    // Sağ alt yüzen kart
        public string? Card2SubTitle { get; set; }
    }
}
