using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }

        [Range(1,100, ErrorMessage ="Count msut be less then 100")]
        [Required]
        public int Count { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public ApplicationUser ApplicationUser { get; set; }
    }
}
