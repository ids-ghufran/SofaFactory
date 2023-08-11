using System;
namespace Domain.Models
{
	public class Slider
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
        public string? BtnLink { get; set; }
		public int ImageId { get; set; }
		public Image? Image { get; set; }
	}
}

