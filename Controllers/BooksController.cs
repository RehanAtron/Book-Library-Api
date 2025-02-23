using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Data;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        public BooksController(LibraryDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return Ok(book);
        }
        [HttpGet("ByAuthor/{authorId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId)
        {
            var books = await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
            return Ok(books);
        }

        [HttpGet("ByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByCategory(int categoryId)
        {
            var books = await _context.Books.Where(b => b.CategoryId == categoryId).ToListAsync();
            return Ok(books);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }
    }
}
