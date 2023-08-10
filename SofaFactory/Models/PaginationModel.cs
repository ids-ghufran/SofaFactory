namespace SofaFactory.Models
{
    public class PaginationModel<T>
    {
        public List<T> Model { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }

        public int PageLength { get; set; }
    }
}
