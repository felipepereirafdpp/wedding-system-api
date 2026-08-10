namespace wedding_system_api.Dtos
{
    public class PresentResponseDto
    {
        public Guid Id { get; set; }
        public string NamePresent { get; set; }
        public decimal PricePresent { get; set; }
        public string DescriptionPresent { get; set; }
        public string? UrlFoto { get; set; }
    }
}