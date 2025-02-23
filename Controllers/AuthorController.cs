using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Data;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        public AuthorController(LibraryDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefaultAsync(b => b.Id == id);

            if (author == null) return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthors), new { id = author.Id }, author);
        }
    }
}
