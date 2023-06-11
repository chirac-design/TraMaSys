using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SysMaTra.Models
{
    public class Voyage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Classe")]
        public string Classe { get; set; }

        [Required]
        [Display(Name = "Trajet")]
        public int TrajetId { get; set; }
        public Trajet Trajet { get; set; }

        [Display(Name = "BusId")]
        public int BusId { get; set; }
        public Bus Bus { get; set; }

        public ICollection<Personnel> Personnels { get; set; }

        [StringLength(100)]
        [Display(Name = "Depart")]
        public string Depart { get; set; }

        [StringLength(100)]
        [Display(Name = "Arrivee")]
        public string Arrivee { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Statut")]
        public string Statut { get; set; }

        public DateTime Date { get; set; } = (DateTime.Now);
        public string Created { get; set; } = (DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
    }
}