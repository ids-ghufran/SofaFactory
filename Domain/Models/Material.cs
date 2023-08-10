using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
    public class SeatingCapacity
    {
        public int Id { get; set;}
        public string Label { get; set; }
    }
    public class StorageType
    {
        public int Id { get; set;}  
        public string Label { get; set; }
    }
    public class Size
    {
        public int Id { get; set;}  
        public string Label { get; set; }
    }
}
