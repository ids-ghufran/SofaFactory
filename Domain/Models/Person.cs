using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? UserName { get ;set; }
        public string? EmailAddress { get; set;}
        public string MobileNo { get; set; }
        public string UserImage { get; set;}    
        public AppUser? User { get; set; }
        public List<Address>? AddressList { get; set; }
    }
}
