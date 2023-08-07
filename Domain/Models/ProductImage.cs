using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductImage
    {
            
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int ImageId { get; set; }
            public Image? Image { get; set; }
            public Product Product { get; set; }
            public int Rank { get; set; }
       
    }
}
