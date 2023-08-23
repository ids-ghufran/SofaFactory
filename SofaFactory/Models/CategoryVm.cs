using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SofaFactory.Models
{
    public class CategoryVm
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public int? ImageId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
