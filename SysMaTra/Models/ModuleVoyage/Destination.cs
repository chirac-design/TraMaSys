using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        public ICollection<Agence> Agences { get; set; }

        public ICollection<Passager> Passagers { get; set; }
        public ICollection<Colis> Colis { get; set; }
        public ICollection<Voyage> Voyages { get; set; }

        public int TrajetId { get; set; }
        public Trajet Trajet { get; set; }
    }
}
