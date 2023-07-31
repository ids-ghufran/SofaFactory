using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Image : BaseEntity
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public int ImageId { get; set; }
    }
}
