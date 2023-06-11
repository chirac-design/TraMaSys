using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Requete
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Objet")]
        public string Objet { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Contenu")]
        public string Contenu { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Piece")]
        public string Piece { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Destinataire")]
        public string Destinataire { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Statut")]
        public string Statut { get; set; }

        [Required]
        public DateTime Date { get; set; } = (DateTime.Now);
        public string Created { get; set; } = (DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
    }
}
