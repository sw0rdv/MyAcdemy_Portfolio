using System.ComponentModel.DataAnnotations;

namespace MyAcdemy_Portfolio.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        public string CategoryName { get; set; } = "";

        // Bire-çok: bir kategoride birden çok proje
        public List<Project>? Projects { get; set; }
    }
}
