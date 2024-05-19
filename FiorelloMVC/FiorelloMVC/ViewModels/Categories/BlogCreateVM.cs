using System.ComponentModel.DataAnnotations;

namespace FiorelloMVC.ViewModels.Categories
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20)]
        public string Title { get; set; }
       
        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
        
        public string ? Image { get; set; }
    }
}
