using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Setting
    {
        public int SettingID { get; set; }

        [Required(ErrorMessage = "Site başlığı zorunludur.")]
        [StringLength(100, ErrorMessage = "Site başlığı en fazla 100 karakter olabilir.")]
        public string SiteTitle { get; set; } = "";

        [StringLength(160, ErrorMessage = "Açıklama en fazla 160 karakter olabilir.")]
        public string? MetaDescription { get; set; }

        // wwwroot altındaki CV dosyasının yolu, örn: /uploads/cv/cv_8f3a....pdf
        public string? CvFilePath { get; set; }
        public DateTime? CvUpdatedDate { get; set; }
    }
}
