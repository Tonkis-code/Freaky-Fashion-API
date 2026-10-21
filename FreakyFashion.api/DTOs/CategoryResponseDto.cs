
namespace FreakyFashion.api.DTOs
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required string UrlSlug { get; set; }
        public List<ProductResponseDto> Products { get; set; } = new();
    }
}
