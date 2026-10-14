namespace FreakyFashion.api.DTOs
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string Image {  get; set; }

    }
}
