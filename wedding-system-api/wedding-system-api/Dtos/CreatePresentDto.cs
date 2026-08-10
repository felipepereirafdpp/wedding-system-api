using Microsoft.AspNetCore.Http;

namespace wedding_system_api.Dtos
{
    public class CreatePresentDto
    {

        public string NamePresent { get; set; }
        public decimal PricePresent { get; set; }
        public string DescriptionPresent { get; set; }
        public IFormFile? UrlFoto { get; set; }
    }
}