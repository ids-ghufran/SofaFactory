using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string  UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Comments { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public AppUser? User { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
    public enum OrderStatus {
    Pending=1,
    Received,
    Processing,
    Dispatched,
    Delivered
    }
    public class OrderDetails
    {
        public int Id { get; set;}
        public int OrderId { get; set;}
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal  Tax { get; set; }
        public decimal SubTotal { get; set; }
        public Order? Order { get; set; }
    }
}
