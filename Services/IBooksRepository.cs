using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIAspNetCore.Services
{
    public interface IBooksRepository
    {
        IEnumerable<Entities.Book> GetBooks();

        Entities.Book GetBook(Guid id);

        Task<IEnumerable<Entities.Book>> GetBooksAsync();

        Task<IEnumerable<Entities.Book>> GetBooksAsync(IEnumerable<Guid> booksIds);

        Task<Entities.Book> GetBooksAsync(Guid id);

        void AddBook(Entities.Book bookToAdd);

        Task<bool> SaveChangesAsync();
    }
}