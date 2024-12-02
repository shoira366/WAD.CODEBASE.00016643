using System.ComponentModel.DataAnnotations;

namespace WAD.CODEBASE._00016643.DTOs
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name must not exceed 100 characters.")]
        public string Name { get; set; }
    }
}
