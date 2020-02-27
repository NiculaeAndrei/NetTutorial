using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utils;
using Newtonsoft.Json;


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

        

        // POST: api/Cifs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cif>> PostCif(Cif cif)
        {
            if (CifValidator.Validate(cif.Name) == true)
            {


                cif.IsCached = false;
                cif.DataApel=DateTime.Now.ToString("yyyy-MM-dd");
                cif.IsValid = true;
                string _name=cif.Name;

                var query = await _context.Cifs.Where(s => s.DataApel == cif.DataApel && s.Name == cif.Name).FirstOrDefaultAsync();
                if (query == null) { 

                if (cif.Name.StartsWith("RO"))
                { _name = cif.Name.Substring(2);}
                string rez = AnafReq.SendReq(_name);
                
                RootObject t= JsonConvert.DeserializeObject<RootObject>(rez);
                
                if(!t.cod.Equals(200)) return BadRequest();
                
                cif.Denumire = t.found.First().denumire;
                cif.Adresa = t.found.First().adresa;
                cif.PlatitorTVA = t.found.First().scpTVA;
                cif.StatusTVAIncasare = t.found.First().statusTvaIncasare;
                cif.StatusActiv = t.found.First().statusInactivi;
                cif.IsCached = true;
                _context.Cifs.Add(cif);
                await _context.SaveChangesAsync();
                cif.IsCached = false;

                    //return CreatedAtAction("GetCif", new { id = cif.Id }, cif);
                    return CreatedAtAction(nameof(GetCif), new { id = cif.Id }, cif);
                }

                return CreatedAtAction(nameof(GetCif), new { id = query.Id }, query);
            }
            else return BadRequest();
        }

        
        private bool CifExists(long id)
        {
            return _context.Cifs.Any(e => e.Id == id);
        }
    }
}
