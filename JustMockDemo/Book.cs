using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JustMockDemo
{
    public class Book
    {
        public int Id { get; private set; }
        public string Tile { get; set; }
        
    }
    public interface IBookRepository
    {
        Book GetWhere(Expression<Func<Book, bool>> expression);
    }

    public class BookService
    {
        private IBookRepository reposotory;

        public BookService(IBookRepository repository)
        {
            this.reposotory = repository;
        }

        public Book GetSingleBook(int id)
        {
            return reposotory.GetWhere(book => book.Id == id);
        }
    }
}
