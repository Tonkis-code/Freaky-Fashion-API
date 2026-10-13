namespace FreakyFashion.api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Image {  get; set; }
        public required string UrlSlug { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
