using ECGWeb.DB;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCode;
using System.Linq;
using System.Threading.Tasks;

namespace ECGWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Studies")]
    public class StudiesController : Controller
    {
        private readonly StudyDBContext _context;

        public StudiesController(StudyDBContext context)
        {
            _context = context;
        }

        // GET: api/Studies
        [HttpGet]
        public IActionResult Getstudies(ODataQueryOptions<Study> queryOptions)
        {
            var items = queryOptions.ApplyTo(_context.studies);

            return Ok(items);
        }

        // GET: api/Studies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var study = await _context.studies.SingleOrDefaultAsync(m => m.ID == id);

            if (study == null)
            {
                return NotFound();
            }

            return Ok(study);
        }

        // PUT: api/Studies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudy([FromRoute] int id, [FromBody] Study study)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != study.ID)
            {
                return BadRequest();
            }

            _context.Entry(study).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyExists(id))
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

        // POST: api/Studies
        [HttpPost]
        public async Task<IActionResult> PostStudy([FromBody] Study study)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.studies.Add(study);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudy", new { id = study.ID }, study);
        }

        // DELETE: api/Studies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var study = await _context.studies.SingleOrDefaultAsync(m => m.ID == id);
            if (study == null)
            {
                return NotFound();
            }

            _context.studies.Remove(study);
            await _context.SaveChangesAsync();

            return Ok(study);
        }

        private bool StudyExists(int id)
        {
            return _context.studies.Any(e => e.ID == id);
        }
    }
}