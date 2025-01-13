using Task7.BL.Interfaces;
using Task7.DAL.Entities;
using Task7.DAL.Validators;

namespace Task7.BL.Factories;

public class PaperLibraryFactory : ILibraryAbstractFactory
{
    public PaperLibraryFactory() { }

    public Catalog CreateCatalog(IEnumerable<Book> books)
    {
        if (books == null)
        {
            throw new ArgumentNullException(nameof(books));
        }

        if (!EntityValidator.AreBooksTheSameType(books, typeof(PaperBook)))
        {
            throw new ArgumentException("Invalid format of books");
        }

        Catalog catalog = new Catalog();

        foreach (var book in books.OfType<PaperBook>())
        {
            catalog.AddBook(book.Isbns[0], book);
        }

        return catalog;
    }

    public IEnumerable<string> CreatePressReleaseItems(IEnumerable<Book> books)
    {
        if (books == null)
        {
            throw new ArgumentNullException(nameof(books));
        }

        if (!EntityValidator.AreBooksTheSameType(books, typeof(PaperBook)))
        {
            throw new ArgumentException("Invalid format of books");
        }

        return books.OfType<PaperBook>().Select(b => b.Publisher).Distinct().ToList();
    }
}

