using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBaseU4.Models;
using APIBaseU4.Dtos;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class NivelController : ControllerBase
{
    private readonly APIBaseU4Context _context;
    private readonly IMapper _mapper;

    public NivelController(APIBaseU4Context context, IMapper mapper)
    {
        _context = context;
        this._mapper = mapper;
    }

    // GET: api/Nivel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nivel>>> GetNivel()
    {
        return await _context.Nivel.ToListAsync();
    }

    // GET: api/Nivel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Nivel>> GetNivel(int id)
    {
        var nivel = await _context.Nivel.FindAsync(id);

        if (nivel == null)
        {
            return NotFound();
        }

        return nivel;
    }

    // PUT: api/Nivel/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNivel(int? id, Nivel nivel)
    {
        if (id != nivel.Id)
        {
            return BadRequest();
        }

        _context.Entry(nivel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NivelExists(id))
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

    // POST: api/Nivel
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Nivel>> PostNivel(Nivel nivel)
    {
        _context.Nivel.Add(nivel);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNivel", new { id = nivel.Id }, nivel);
    }

    // DELETE: api/Nivel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNivel(int? id)
    {
        var nivel = await _context.Nivel.FindAsync(id);
        if (nivel == null)
        {
            return NotFound();
        }

        _context.Nivel.Remove(nivel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //---------------------------------------------------------------------
    //esta es la nueva ruta para obtener los cursos de un nivel en el api, se puede acceder a ella con la ruta: api/Nivel/{id}/Cursos, esto se debe usar en el lado de la relacion que sea muchos
    // GET: api/Nivel/5/Cursos
    [HttpGet("{id}/Cursos")]
    public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursosByNivel(int? id)
    {
        var nivel = await _context.Nivel.FindAsync(id);
        if (nivel == null)
        {
            return NotFound();
        }

        var cursos = _context.Curso.Where(c => c.NivelId == id).ToList();
        var cursosDtos = _mapper.Map<List<CursoDto>>(cursos);

        return cursosDtos;
    }

    private bool NivelExists(int? id)
    {
        return _context.Nivel.Any(e => e.Id == id);
    }
}
