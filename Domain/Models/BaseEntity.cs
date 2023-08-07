using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }= DateTime.Now;
        [ForeignKey("CreatedBy")]
        [Column]
        public string? CreatedById { get; set; }
        [ForeignKey("UpdatedBy")]
        [Column]
        public string? UpdatedById { get; set; }
        [JsonIgnore]
        public AppUser? CreatedBy { get; set; }
        [JsonIgnore]
        public AppUser? UpdatedBy { get; set; }
    }
}
