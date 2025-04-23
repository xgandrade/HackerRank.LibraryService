﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Book>> Get(int libraryId, int[] ids)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }

        public async Task<Book> Add(Book book)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }

        public async Task<Book> Update(Book book)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Book book)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }
    }

    public interface IBooksService
    {
        Task<IEnumerable<Book>> Get(int libraryId, int[] ids);

        Task<Book> Add(Book book);

        Task<Book> Update(Book book);

        Task<bool> Delete(Book book);
    }
}
