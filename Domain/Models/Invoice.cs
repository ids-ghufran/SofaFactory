using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateTime InvoiceDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public Order Order { get; set; }
    }
    public enum PaymentStatus
    {
        Unpaid=1,
        Processing,
        Paid,
        Failed
    }
}
