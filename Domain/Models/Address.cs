using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public   class Address
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string AddressLine { get; set; }
        public string? Locality { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        [JsonIgnore]
        public City City { get; set; }
        [JsonIgnore]
        public State State { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
        public string Pincode { get; set; }
        public string? Contact { get; set; }
        public string UserId { get; set; }
        public bool  IsPrimary { get; set; }
        public AppUser User { get; set; }
    }
}
