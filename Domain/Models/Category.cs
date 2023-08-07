﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ImageId { get; set; }
        public Image? Image { get; set; }
        public int ParentId { get; set; }
    }
}
