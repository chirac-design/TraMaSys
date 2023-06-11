using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Bus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Matricule")]
        public string Matricule { get; set; }

        [Required]
        [Display(Name = "PlaceNum")]
        public int PlaceNum { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Classe")]
        public string Classe { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Statut")]
        public string Statut { get; set; } = "Free";

        public Voyage Voyage { get; set; }

        public ICollection<Passager> Passagers { get; set; }

        public ICollection<Colis> Colis { get; set; }

        public string Created { get; set; } = (DateTime.Now).ToString("dd/MM/yyyy");
    }
}
