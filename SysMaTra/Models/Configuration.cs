using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Slogan")]
        public string Slogan { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        public ICollection<Couleur> Couleurs { get; set; }

        public string Created { get; set; } = DateTime.Now.ToString("JJ/MM/AAAA");
    }
}
