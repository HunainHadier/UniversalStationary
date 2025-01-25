namespace UniversalStationary.Models
{
    public class AddProductModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? productname { get; set; }

        public string? description { get; set; }

        public string? productpicture { get; set; }

        public string? Brand { get; set; }

        public string? Category { get; set; }

        public string? Discount { get; set; }

        public string? Stock { get; set; }

        public string? Price { get; set; }

        public bool? Trendingproducts { get; set; } = true;

        public bool? NewArrival { get; set; } = true;

        public string? Rating { get; set; }

        public bool? FeaturedProduct { get; set; } = true;

        public DateTime? Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; } = DateTime.Now;

    }
}
