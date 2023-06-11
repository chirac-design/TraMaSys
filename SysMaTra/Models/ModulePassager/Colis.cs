using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Colis
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Valeur")]
        public float Valeur { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "DestinationId")]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Destinateur")]
        public string Destinateur { get; set; }

        [Required]
        [StringLength(100)]
        [Phone]
        [Display(Name = "DestinateurPhone")]
        public string DestinateurPhone { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Destinataire")]
        public string Destinatire { get; set; }

        [Required]
        [Phone]
        [StringLength(100)]
        [Display(Name = "DestinatairePhone")]
        public string DestinatairePhone { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "IsPaye")]
        public bool IsPaye { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Statut")]
        public string Statut { get; set; }

        public Bus Bus { get; set; }
        public DateTime Date { get; set; } = (DateTime.Now);
        public string Created { get; set; } = (DateTime.Now).ToString("dd/MM/yyyy");
    }
}