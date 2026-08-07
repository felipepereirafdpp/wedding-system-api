using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wedding_system_api.Data;
using wedding_system_api.Models;

namespace wedding_system_api.Constructs
{
    [Route("api/[controller]")]
    [ApiController]
    public class presentsController : ControllerBase
    {
        private readonly wedding_system_apiContext _context;

        public presentsController(wedding_system_apiContext context)
        {
            _context = context;
        }

        // GET: api/presents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<presentsModel>>> GetpresentsModel()
        {
            return await _context.presentsModel.ToListAsync();
        }

        // GET: api/presents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<presentsModel>> GetpresentsModel(Guid id)
        {
            var presentsModel = await _context.presentsModel.FindAsync(id);

            if (presentsModel == null)
            {
                return NotFound();
            }

            return presentsModel;
        }

        // PUT: api/presents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutpresentsModel(Guid id, presentsModel presentsModel)
        {
            if (id != presentsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(presentsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!presentsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/presents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<presentsModel>> PostpresentsModel(presentsModel presentsModel)
        {
            _context.presentsModel.Add(presentsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetpresentsModel", new { id = presentsModel.Id }, presentsModel);
        }

        // DELETE: api/presents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletepresentsModel(Guid id)
        {
            var presentsModel = await _context.presentsModel.FindAsync(id);
            if (presentsModel == null)
            {
                return NotFound();
            }

            _context.presentsModel.Remove(presentsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool presentsModelExists(Guid id)
        {
            return _context.presentsModel.Any(e => e.Id == id);
        }
    }
}
