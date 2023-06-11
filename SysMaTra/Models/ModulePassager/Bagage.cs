using System.ComponentModel.DataAnnotations;

namespace SysMaTra.Models
{
    public class Bagage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Poids")]
        public float Poids { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string Image { get; set; } // image de type file

        [Required]
        [Display(Name = "PassagerId")]
        public int PassagerId { get; set; }
        public Passager Passager { get; set; }
    }
}