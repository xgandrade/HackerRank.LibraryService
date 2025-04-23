using LibraryService.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.WebAPI.Services
{
    public class BooksService : IBooksService
    {
        private readonly LibraryContext _libraryContext;

        public BooksService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Book>> Get(int libraryId)
        {
            var books = _libraryContext.Books.AsQueryable();
            books = books.Where(x => x.LibraryId == libraryId);

            return await books.ToListAsync();
        }

        public async Task<Book> Add(Book book)
        {
            await _libraryContext.Books.AddAsync(book);
            await _libraryContext.SaveChangesAsync();

            return book;
        }

        public async Task<Book> Update(Book book)
        {
            var projectForChanges = await _libraryContext.Books.SingleAsync(x => x.Id == book.Id);
            projectForChanges.Name = book.Name;
            projectForChanges.Category = book.Category;
            projectForChanges.LibraryId = book.LibraryId;

            _libraryContext.Books.Update(projectForChanges);
            await _libraryContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(Book book)
        {
            var projectForDelete = _libraryContext.Books.FirstOrDefault(x => x.Id == book.Id);
            if (projectForDelete == null)
                return false;

            _libraryContext.Books.Remove(projectForDelete);
            await _libraryContext.SaveChangesAsync();

            return true;
        }
    }

    public interface IBooksService
    {
        Task<IEnumerable<Book>> Get(int libraryId);

        Task<Book> Add(Book book);

        Task<Book> Update(Book book);

        Task<bool> Delete(Book book);
    }
}
