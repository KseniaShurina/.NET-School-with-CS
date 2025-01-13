using Task7.BL.Interfaces;
using Task7.DAL.Entities;

namespace Task7.BL.Factories;

public class LibraryBuilder
{
    private static readonly LibraryBuilder Builder = new LibraryBuilder();
    private readonly Dictionary<string, ILibraryAbstractFactory> _factories;

    public static LibraryBuilder GetInstance() => Builder;

    private LibraryBuilder()
    {
        _factories = new Dictionary<string, ILibraryAbstractFactory>()
        {
            { "Paper", new PaperLibraryFactory() },
            { "Electronic", new ELibraryFactory() }
        };
    }

    public Library BuildLibrary(string type, List<Book> books)
    {
        if (books == null)
        {
            throw new ArgumentNullException(nameof(books));
        }

        var expectedType = type == "Paper" ? typeof(PaperBook) : typeof(EBook);
        if (books.Any(book => book.GetType() != expectedType))
        {
            throw new ArgumentException($"Invalid type of book in {nameof(books)}");
        }
        
        _factories.TryGetValue(type, out ILibraryAbstractFactory? factory);
        var catalog = _factories[type].CreateCatalog(books);
        var pressReleaseItems = _factories[type].CreatePressReleaseItems(books);

        return new Library(catalog, pressReleaseItems);
    }
}