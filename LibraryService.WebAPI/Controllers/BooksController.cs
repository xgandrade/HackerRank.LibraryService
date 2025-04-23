using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;
using LibraryService.WebAPI.DTO;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int libraryId)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            var books = await _booksService.Get(libraryId);

            if (library == null)
                return NotFound();

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Created(int libraryId, [FromBody] Book book)
        {
            var libraryExists = (await _librariesService.Get(new[] { libraryId })).Any();
            if (libraryExists)
                book.LibraryId = libraryId;
            else
                return NotFound();

            var addedBook = await _booksService.Add(book);
            var bookDto = new BookForm()
            {
                Id = addedBook.Id,
                Name = addedBook.Name,
                Category = addedBook.Category,
                LibraryId = addedBook.LibraryId
            };

            return CreatedAtAction(nameof(Created), bookDto);
        }
    }
}