using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product:BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Rating { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public string Highlights { get; set; }
        public string Color { get; set; }
        public int Warranty { get; set; }
        public int SeatingCapacity { get; set; }
        public string AssemblyDetails { get; set; }
        public string PackageDetails { get; set; }
        [NotMapped]
        public virtual List<Image>? Images { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public Category? Category { get; set; }
        public Category? SubCategory { get; set; }
        public Brand? Brand { get; set; }
    }
  
    public enum DiscountType {
    Flat=1,
    Percent
    }
   
    
    


}
