using System.Globalization;

namespace wedding_system_api.Models
{
    public class presentsModel
    {
        public  Guid Id { get; set; }
        public string NamePresent { get; set; }
        public decimal? PricePresent { get; set; } = 0;
        public string? DescriptionPresent { get; set; }
        public string? UrlFoto { get; set; }
    }
}
