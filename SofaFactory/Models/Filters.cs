namespace SofaFactory.Models
{
    public class Filters
    {
        public List<string>? Materials { get; set; }
        public List<string>? SeatingCapacities { get; set; }
        public List<string>? Sizes { get; set; }
        public List<string>? StorageTypes { get; set; }
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 0;
    }
}
