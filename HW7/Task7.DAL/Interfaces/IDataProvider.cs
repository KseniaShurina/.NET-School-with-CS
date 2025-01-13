using Task7.DAL.Entities;

namespace Task7.DAL.Interfaces;

public interface IDataProvider
{
    public Task<IEnumerable<Book>> GetBooks();

    public Task SaveBooks(IEnumerable<Book> paperBooks);
}

