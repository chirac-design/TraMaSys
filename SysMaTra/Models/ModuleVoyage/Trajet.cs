using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Trajet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Tarif")]
        public double Tarif { get; set; }

        public ICollection<Destination> Destinations { get; set; }
        public ICollection<Voyage> Voyages { get; set; }
    }
}
