using System;
using System.ComponentModel.DataAnnotations;

namespace SofaFactory.Models
{
	public class SliderVm
	{
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? BtnLink { get; set; }
        [Required(ErrorMessage = "Please select slider Image")]
        public IFormFile Image { get; set; }
    }
}

