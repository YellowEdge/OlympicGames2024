using System.ComponentModel.DataAnnotations;

namespace OgWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Display(Name="Display Order")]
        [Range(1,100,ErrorMessage="Display Order must be in range of 1-100 !")]
        public int DisplayOrder { get; set; }
    }
}
