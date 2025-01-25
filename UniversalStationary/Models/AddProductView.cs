namespace UniversalStationary.Models
{
    public class AddProductView
    {
        public string? productname { get; set; }

        public string? description { get; set; }

        public IFormFile? productpicture { get; set; }

        public string? Brand { get; set; }

        public string? Category { get; set; }

        public string? Discount { get; set; }

        public string? Stock { get; set; }

        public string? Price { get; set; }

        public bool? Trendingproducts { get; set; } = true;

        public bool? NewArrival { get; set; } = true;

        public bool? FeaturedProduct { get; set; } = true;

        public string? Rating { get; set; }

        public DateTime? Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; } = DateTime.Now;

    }
}
