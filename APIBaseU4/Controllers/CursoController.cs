using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBaseU4.Models;
using AutoMapper;
using APIBaseU4.Dtos;

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly APIBaseU4Context _context;
    public CursoController(APIBaseU4Context context, IMapper mapper)
    {
        _context = context;
        this._mapper = mapper;
    }

    // GET: api/Curso
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoDto>>> GetCurso()
    {
        var modelos = await _context.Curso.ToListAsync();

        var dtos = _mapper.Map<List<Curso>, List<CursoDto>>(modelos);

        return dtos;
    }

    // GET: api/Curso/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CursoDto>> GetCurso(int id)
    {
        var curso = await _context.Curso.FindAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        var cursoDto = _mapper.Map<CursoDto>(curso);
        return cursoDto;
    }

    // PUT: api/Curso/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurso(int? id, CursoDto cursoDto)
    {
        if (id != cursoDto.Id)
        {
            return BadRequest();
        }

        var cursoDB = await _context.Curso.FindAsync(id);
        if (cursoDB == null)
        {
            return NotFound();
        }

        _mapper.Map<CursoDto, Curso>(cursoDto, cursoDB);

        _context.Entry(cursoDB).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CursoExists(id))
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

    // POST: api/Curso
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<CursoDto>> PostCurso(CursoDto cursoDto)
    {
        var cursoDB = _mapper.Map<Curso>(cursoDto);
        _context.Curso.Add(cursoDB);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCurso", new { id = cursoDto.Id }, cursoDto);
    }

    // DELETE: api/Curso/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int? id)
    {
        var curso = await _context.Curso.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _context.Curso.Remove(curso);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CursoExists(int? id)
    {
        return _context.Curso.Any(e => e.Id == id);
    }
}
