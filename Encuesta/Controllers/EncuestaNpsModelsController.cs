using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Encuesta.DataAccess;
using Encuesta.Models;

namespace Encuesta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestaNpsModelsController : ControllerBase
    {
        private readonly EncuestaDBContext _context;

        public EncuestaNpsModelsController(EncuestaDBContext context)
        {
            _context = context;
        }

        // GET: api/EncuestaNpsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestaNpsModel>>> GetEncuestaNps()
        {
            return await _context.EncuestaNps.ToListAsync();
        }

        // GET: api/EncuestaNpsModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncuestaNpsModel>> GetEncuestaNpsModel(int id)
        {
            var encuestaNpsModel = await _context.EncuestaNps.FindAsync(id);

            if (encuestaNpsModel == null)
            {
                return NotFound();
            }

            return encuestaNpsModel;
        }

        // PUT: api/EncuestaNpsModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncuestaNpsModel(int id, EncuestaNpsModel encuestaNpsModel)
        {
            if (id != encuestaNpsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(encuestaNpsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestaNpsModelExists(id))
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

        // POST: api/EncuestaNpsModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EncuestaNpsModel>> PostEncuestaNpsModel(EncuestaNpsModel encuestaNpsModel)
        {
            _context.EncuestaNps.Add(encuestaNpsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncuestaNpsModel", new { id = encuestaNpsModel.Id }, encuestaNpsModel);
        }

        // DELETE: api/EncuestaNpsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncuestaNpsModel(int id)
        {
            var encuestaNpsModel = await _context.EncuestaNps.FindAsync(id);
            if (encuestaNpsModel == null)
            {
                return NotFound();
            }

            _context.EncuestaNps.Remove(encuestaNpsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncuestaNpsModelExists(int id)
        {
            return _context.EncuestaNps.Any(e => e.Id == id);
        }
    }
}
