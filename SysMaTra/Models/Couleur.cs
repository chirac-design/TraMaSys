using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Couleur
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public Configuration Configuration { get; set; }
    }
}
