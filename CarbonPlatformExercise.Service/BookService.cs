using CarbonPlatformExercise.Data;
using CarbonPlatformExercise.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarbonPlatformExercise.Service
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<Book> GetBookByUrl(string id);
        Task<Book> InsertBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        Task DeleteExpiredBooks(); 
    }

    public class BookService : IBookService
    {
        private IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _bookRepository.Get(id);
        }

        public async Task<Book> GetBookByUrl(string url)
        {
            return await _bookRepository.Context.FirstOrDefaultAsync(itm => itm.Url == url);
        }
        public async Task<Book> InsertBook(Book book)
        {
          return await _bookRepository.Insert(book);
        }
        public async Task UpdateBook(Book book)
        {
            book.UpdatedAt = DateTime.Now;
           await _bookRepository.Update(book);
        }

        public async Task DeleteBook(int id)
        {
            Book book = await GetBook(id);
           await _bookRepository.Remove(book);
           await _bookRepository.SaveChanges();
        }

        public async Task DeleteExpiredBooks()
        {
         var books = (await _bookRepository.GetAll()).Where(itm => itm.Expiry != null);
            foreach (var book in books)
            {
                if(book.Expiry <= DateTime.Now)
                {
                  await  _bookRepository.Delete(book);
                }
            }
        }
    }
}
