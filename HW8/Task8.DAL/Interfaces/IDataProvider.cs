using Task8.DAL.Entities;

namespace Task8.DAL.Interfaces;

public interface IDataProvider
{
    public Task<IEnumerable<Book>> GetBooks();

    public Task SaveBooks(IEnumerable<Book> paperBooks);
}

