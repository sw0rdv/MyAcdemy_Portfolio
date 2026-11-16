using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Skill
    {
        public int SkillID { get; set; }

        [Required(ErrorMessage = "Yetenek adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Yetenek adı en fazla 50 karakter olabilir.")]
        public string SkillName { get; set; } = "";

        [Required(ErrorMessage = "Grup adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Grup adı en fazla 50 karakter olabilir.")]
        public string SkillCategory { get; set; } = "";   // Örn: Backend Development

        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır.")]
        public int Percentage { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
