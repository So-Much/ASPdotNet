namespace BackEnd.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Images { get; set; } = "";
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
    }
}
