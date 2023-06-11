using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Adresse
    {
        public int id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Domicile")]
        public string Domicile { get; set; }

        public Agence Agence { get; set; }
        public ICollection<Passager> Passagers { get; set; }
        public ICollection<Personnel> Personnels { get; set; }
    }
}
