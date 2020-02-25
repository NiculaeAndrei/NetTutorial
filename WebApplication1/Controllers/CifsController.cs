using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CifsController : ControllerBase
    {
        private readonly CifContext _context;

        public CifsController(CifContext context)
        {
            _context = context;
        }

        // GET: api/Cifs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cif>>> GetCifs()
        {
            return await _context.Cifs.ToListAsync();
        }

        // GET: api/Cifs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cif>> GetCif(long id)
        {
            var cif = await _context.Cifs.FindAsync(id);

            if (cif == null)
            {
                return NotFound();
            }

            return cif;
        }

        // PUT: api/Cifs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCif(long id, Cif cif)
        {
            if (id != cif.Id)
            {
                return BadRequest();
            }

            _context.Entry(cif).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CifExists(id))
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

        // POST: api/Cifs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cif>> PostCif(Cif cif)
        {
            if (CifValidator.Validate(cif.Name) == true)
            {
                cif.IsValid = true;
                _context.Cifs.Add(cif);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetCif", new { id = cif.Id }, cif);
                return CreatedAtAction(nameof(GetCif), new { id = cif.Id }, cif);

            }
            else return BadRequest();
        }

        // DELETE: api/Cifs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cif>> DeleteCif(long id)
        {
            var cif = await _context.Cifs.FindAsync(id);
            if (cif == null)
            {
                return NotFound();
            }

            _context.Cifs.Remove(cif);
            await _context.SaveChangesAsync();

            return cif;
        }

        private bool CifExists(long id)
        {
            return _context.Cifs.Any(e => e.Id == id);
        }
    }
}
