using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Category
    {
        [Key] // primary key => clé primaire 
        public int Id { get; set; }
        /*****************************/

        [Required(ErrorMessage ="le nom de la catégorie est obligatoire")]
        [MaxLength(100,ErrorMessage ="la taille du nom de la catégorie ne doit pas depassé 100 car")]
        [Display(Name ="Category Name")]
        public string Name { get; set; }

        /**************************************/
        [Display(Name ="Display Order")]
        [Required(ErrorMessage = "DisplayOrder est obligatoire")]
        [Range(1,100,ErrorMessage ="display order doit etre entre 1 et 100")]
        public int DisplayOrder { get; set; }
    }
}
