using Task7.DAL.DTO;
using Task7.DAL.DtoExtensions;
using Task7.DAL.Entities;
using Task7.DAL.Interfaces;
using Task7.DAL.Repositories;
using Task7.DAL.Validators;

namespace Task7.DAL.Providers;

public class XmlDataProvider : IDataProvider
{
    private readonly IRepository<DtoBook> _xmlRepository = new XmlRepository<DtoBook>();

    public async Task<IEnumerable<Book>> GetBooks()
    {
        var dtoBooks = await _xmlRepository.GetAsync();
        if (dtoBooks == null)
        {
            throw new ArgumentNullException(nameof(dtoBooks));
        }

        var paperBooks = new List<PaperBook>();

        foreach (var book in dtoBooks)
        {
            //TODO: Think carefully about this type
            dynamic restoredBook = null;

            if (EntityValidator.IsIsbn(book.Identifiers[0]))
            {
                restoredBook = CreatePaperBook(book);
            }
            else
            {
                restoredBook = CreateEBook(book);
            }

            if (!EntityValidator.AcceptBook(restoredBook))
            {
                throw new ArgumentException("An error occurred while converting the book");
            }

            paperBooks.Add(restoredBook);
        }

        return paperBooks;
    }

    public async Task SaveBooks(IEnumerable<Book> books)
    {
        var dtoBooks = books.Select(book => book.MapToDtoBook());
        await _xmlRepository.SaveAsync(dtoBooks);
    }

    private PaperBook CreatePaperBook(DtoBook dtoBook)
    {
        return new PaperBook(
            dtoBook.Title,
            new HashSet<Author>(dtoBook.Authors.Select(a =>
                new Author(a.FirstName, a.LastName, a.DateOfBirthday))),
            new List<string>(dtoBook.Identifiers),
            //TODO: How to deal with specific properties if I avoid them?
            null,
            null);
    }

    private EBook CreateEBook(DtoBook dtoBook)
    {
        return new EBook(
            dtoBook.Title,
            new HashSet<Author>(dtoBook.Authors.Select(a =>
                new Author(a.FirstName, a.LastName, a.DateOfBirthday))),
            dtoBook.Identifiers[0]);
    }
}

