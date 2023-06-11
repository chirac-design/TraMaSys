using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Agence
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Description")]
        [StringLength(254)]
        public string Description { get; set; }

        [Required]
        public int AdresseId { get; set; }
        public Adresse Adresse { get; set; }

        public ICollection<Passager> Passagers { get; set; }

        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        public string Created { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
