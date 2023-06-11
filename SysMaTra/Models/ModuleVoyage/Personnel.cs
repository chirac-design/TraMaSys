using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Personnel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Prenom")]
        public string Prenom { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Fonction")]
        public string Fonction { get; set; }

        public int AdresseId { get; set; }
        public Adresse Adresse {get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name = "Statut")]
        public string Statut { get; set; }


        public Voyage Voyage { get; set; }

        public DateTime Date { get; set; } = (DateTime.Now);
        public string Created { get; set; } = (DateTime.Now).ToString("dd/MM/yyyy");
    }
}
