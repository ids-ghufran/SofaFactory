using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime Date { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Invoice Invoice { get; set; }
        public string PaymentMethod { get; set; }
        public string GatewayId { get; set; }
        public string GatewayName { get; set;}
        public string GatewayToken { get; set;}
    }
}
