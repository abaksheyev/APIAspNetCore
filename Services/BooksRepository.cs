using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using APIAspNetCore.Context;
using APIAspNetCore.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace APIAspNetCore.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext _context;

        public BooksRepository(BooksContext contxt){
            _context = contxt ?? throw new ArgumentNullException(nameof(contxt));
        }

        public Book GetBook(Guid id)
        {
             return  _context.Books
                                .Include(b=>b.Author)
                                .FirstOrDefault(b=>b.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            _context.Database.ExecuteSqlCommand("WAITFOR DELAY '00:00:02';");
             return  _context.Books
                                .Include(b=>b.Author)
                                .ToList();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
           await _context.Database.ExecuteSqlCommandAsync("WAITFOR DELAY '00:00:02';");
           return await _context.Books
                                .Include(b=>b.Author)
                                .ToListAsync();
        }

        public async Task<Book> GetBooksAsync(Guid id)
        {
            return await _context.Books
                                .Include(b=>b.Author)
                                .FirstOrDefaultAsync(b=>b.Id == id);
        }


        public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> booksIds)
        {
            return await _context.Books
                                .Where(b => booksIds.Contains(b.Id))
                                .Include(b=>b.Author)
                                .ToListAsync();
        }

        public void AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }

             _context.Add(bookToAdd);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return (await _context.SaveChangesAsync() > 0);
        }
        public void Dispose()
        {
           Dispose(true);
           GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing){
            if(disposing)
            {
                if(_context != null){
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}