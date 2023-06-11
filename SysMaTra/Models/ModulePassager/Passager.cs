using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Passager
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

        //[Required]
        [DataType(DataType.Upload)]
        [Display(Name = "ImageCNI")]
        public string ImageCNI { get; set; } // image de type file

        public ICollection<Adresse> Adresses { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Destination")]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Classe")]
        public string Classe { get; set; }

        public ICollection<Bagage> Bagages { get; set; }

        [Required]
        [Display(Name = "IsPaye")]
        public bool IsPaye { get; set; }

        [Required]
        [Display(Name = "AgenceId")]
        public int AgenceId { get; set; }
        public Agence Agence { get; set; }

        public DateTime Date { get; set; } = (DateTime.Now);
        public string Created { get; set; } = (DateTime.Now).ToString("dd/MM/yyyy");

        public Bus Bus { get; set; }
    }
}