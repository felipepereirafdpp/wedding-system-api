namespace wedding_system_api.Dtos
{
    public class PresentResponseDto
    {
        public Guid Id { get; set; }
        public string NamePresent { get; set; } = string.Empty;
        public decimal PricePresent { get; set; }
        public string? DescriptionPresent { get; set; }
        public string? UrlFoto { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}