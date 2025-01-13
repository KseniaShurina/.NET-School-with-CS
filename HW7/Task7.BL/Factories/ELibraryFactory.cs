using Task7.BL.Interfaces;
using Task7.DAL.Entities;
using Task7.DAL.Validators;

namespace Task7.BL.Factories;

public class ELibraryFactory : ILibraryAbstractFactory
{
    public ELibraryFactory() { }

    public Catalog CreateCatalog(IEnumerable<Book> books)
    {
        if (books == null)
        {
            throw new ArgumentNullException(nameof(books));
        }

        if (!EntityValidator.AreBooksTheSameType(books, typeof(EBook)))
        {
            throw new ArgumentException("Invalid format of books");
        }

        Catalog catalog = new Catalog();

        foreach (var book in books.OfType<EBook>())
        {
            catalog.AddBook(book.Identifier, book);
        }

        return catalog;
    }

    public IEnumerable<string> CreatePressReleaseItems(IEnumerable<Book> books)
    {
        if (books == null)
        {
            throw new ArgumentNullException(nameof(books));
        }

        if (!EntityValidator.AreBooksTheSameType(books, typeof(EBook)))
        {
            throw new ArgumentException("Invalid format of books");
        }

        return books.OfType<EBook>().SelectMany(b => b.Formats).Distinct().ToList();
    }
}