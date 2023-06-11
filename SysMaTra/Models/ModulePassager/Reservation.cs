using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Reservation : Passager
    {

        [Required]
        [Display(Name = "NombrePlaces")]
        public int NombrePlaces { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Depart")]
        public DateTime Depart { get; set; }

    }
}
