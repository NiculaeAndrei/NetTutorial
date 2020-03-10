using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CifsController : ControllerBase
    {

        private readonly IProcessReq _processReq;


        public CifsController(IProcessReq processReq)
        {

            _processReq = processReq;

        }

       
        // GET: api/Cifs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cif>> GetCif(long id)
        {
            //var cif = await _context.Cifs.FindAsync(id);
            var cif = await _processReq.GetCif(id);

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
            

            if (!_processReq.CheckDatabase())
            {
                return BadRequest("SQL Server Offline");
            }

            if (!_processReq.CheckAnaf())
            {
                return BadRequest("ANAF Service Offline");
            }

            var c = await _processReq.Process(cif);
            if (c == null)
            {
                return BadRequest("Cif Invalid");
            }

            



            return CreatedAtAction(nameof(GetCif), new { id = cif.Id }, c);
        }


       
    }
}
