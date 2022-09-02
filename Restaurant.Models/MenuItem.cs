using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "le nom est obligatoire !!!")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Display(Name = "MenuItem Image")]
        public string? ImageUrl { get; set; }

        [Range(1, 100, ErrorMessage = "Le prix doit etre entre 1 et 100")]
        public double Price { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }
        [ForeignKey("FoodTypeId")]
        public FoodType FoodType { get; set; }
    }
}
