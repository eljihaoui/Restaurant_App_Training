using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class FoodType
    {
        [Key]
        [Display(Name ="Food Type ID")]
        public int FoodTypeId { get; set; }

        [Required]
        [Display(Name ="Food Type Name")]
        [StringLength(30,MinimumLength =3)]
        public string FoodTypeName { get; set; }    
    }
}
