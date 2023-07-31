using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Brand : BaseEntity
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set;}
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
