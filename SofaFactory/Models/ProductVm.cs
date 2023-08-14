using System;
using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace SofaFactory.Models
{
	public class ProductVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Rating { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Emi { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public string Highlights { get; set; }
        public string Color { get; set; }
        public int Warranty { get; set; }
        public int SeatingCapacityId { get; set; }
        public int MaterialId { get; set; }
        public int SizeId { get; set; }
        public int ShapeId { get; set; }
        public int BrandId { get; set; }
        public int StorageTypeId { get; set; }
        public string AssemblyDetails { get; set; }
        public string PackageDetails { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}

