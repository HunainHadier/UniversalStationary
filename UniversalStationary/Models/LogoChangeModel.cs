namespace UniversalStationary.Models
{
    public class LogoChangeModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? SiteName { get; set; }

        public string? sitediscription { get; set; }

        public string? gmail { get; set; }

        public string? Phonenumber { get; set; }

        public string? address { get; set; }


        public string? logofile { get; set; }
        
        public string? sitebanners { get; set; }
    }
}
