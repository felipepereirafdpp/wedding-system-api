using Microsoft.EntityFrameworkCore;
using wedding_system_api.Data;
using wedding_system_api.Dtos;
using wedding_system_api.InterfaceServices;
using wedding_system_api.Models;

namespace wedding_system_api.Services
{
    public class PresentService : IPresentInterface
    {
        private readonly wedding_system_apiContext _context;

        public PresentService(wedding_system_apiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PresentResponseDto>> GetAllAsync()
        {
            var presents = await _context.presentsModel.ToListAsync();
            return presents.Select(ToResponseDto);
        }

        public async Task<PresentResponseDto?> GetByIdAsync(Guid id)
        {
            var present = await _context.presentsModel.FindAsync(id);

            if (present == null)
            {
                return null;
            }

            return ToResponseDto(present);
        }

        public async Task<PresentResponseDto> CreateAsync(CreatePresentDto createPresentDto)
        {
            string? urlFoto = null;

            if (createPresentDto.UrlFoto != null &&
                createPresentDto.UrlFoto.Length > 0)
            {
                var pastaImagens = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images");

                Directory.CreateDirectory(pastaImagens);

                var extensao = Path.GetExtension(createPresentDto.UrlFoto.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var caminhoCompleto = Path.Combine(
                    pastaImagens,
                    nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await createPresentDto.UrlFoto.CopyToAsync(stream);
                }

                urlFoto = $"/images/{nomeArquivo}";
            }

            var present = new presentsModel
            {
                NamePresent = createPresentDto.NamePresent,
                PricePresent = createPresentDto.PricePresent,
                DescriptionPresent = createPresentDto.DescriptionPresent,
                UrlFoto = urlFoto
            };

            _context.presentsModel.Add(present);
            await _context.SaveChangesAsync();

            return ToResponseDto(present);
        }

        public async Task<bool> UpdateAsync(Guid id, presentsModel present)
        {
            if (id != present.Id)
            {
                return false;
            }

            _context.Entry(present).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(id))
                {
                    return false;
                }
                throw;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var present = await _context.presentsModel.FindAsync(id);
            if (present == null)
            {
                return false;
            }

            _context.presentsModel.Remove(present);
            await _context.SaveChangesAsync();
            return true;
        }

        // caso precise criar a função patch com as regras para substituir a imagem, porem acredito que nao sera necessario

        private async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.presentsModel.AnyAsync(e => e.Id == id);
        }

        private static PresentResponseDto ToResponseDto(presentsModel present)
        {
            return new PresentResponseDto
            {
                Id = present.Id,
                NamePresent = present.NamePresent,
                PricePresent = present.PricePresent,
                DescriptionPresent = present.DescriptionPresent,
                UrlFoto = present.UrlFoto
            };
        }
    }
}