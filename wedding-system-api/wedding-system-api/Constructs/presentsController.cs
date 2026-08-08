using Microsoft.AspNetCore.Mvc;
using wedding_system_api.Dtos;
using wedding_system_api.Models;
using wedding_system_api.Services.InterfaceServices;

namespace wedding_system_api.Constructs
{
    [Route("api/[controller]")]
    [ApiController]
    public class presentsController : ControllerBase
    {
        private readonly IPresentInterface _presentService;

        public presentsController(IPresentInterface presentService)
        {
            _presentService = presentService;
        }

        // GET: api/presents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresentResponseDto>>> GetpresentsModel()
        {
            return Ok(await _presentService.GetAllAsync());
        }

        // GET: api/presents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PresentResponseDto>> GetpresentsModel(Guid id)
        {
            var present = await _presentService.GetByIdAsync(id);

            if (present == null)
            {
                return NotFound();
            }

            return present;
        }

        // PUT: api/presents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutpresentsModel(Guid id, presentsModel presentsModel)
        {
            if (id != presentsModel.Id)
            {
                return BadRequest();
            }

            var atualizado = await _presentService.UpdateAsync(id, presentsModel);

            if (!atualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/presents
        [HttpPost]
        public async Task<ActionResult<PresentResponseDto>> PostpresentsModel([FromForm] CreatePresentDto createPresentDto)
        {
            var criado = await _presentService.CreateAsync(createPresentDto);

            return CreatedAtAction("GetpresentsModel", new { id = criado.Id }, criado);
        }

        // DELETE: api/presents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletepresentsModel(Guid id)
        {
            var deletado = await _presentService.DeleteAsync(id);

            if (!deletado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}