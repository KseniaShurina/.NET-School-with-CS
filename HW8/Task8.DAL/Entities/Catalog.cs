using Task8.DAL.Validators;

namespace Task8.DAL.Entities;

/// <summary>
/// Represents a catalog of books, allowing for various operations such as adding books,
/// retrieving books by author, title, or ISBN, and generating reports about authors and their books.
/// </summary>
public class Catalog
{
    private readonly Dictionary<string, Book> _books = new();

    /// <summary>
    /// Adds a book to the catalog if it passes validation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the book fails validation or already exists in the catalog.</exception>
    public void AddBook(string id, Book book)
    {
        if (!EntityValidator.AcceptBook(book))
        {
            throw new ArgumentException("There is something wrong with book properties");
        }

        _books!.Add(id, book);
    }

    /// <summary>
    /// Get a set of book titles from the catalog, sorted alphabetically.
    /// </summary>
    /// <returns></returns>
    public List<string> GetBookTitles()
    {
        var result = _books.Values
            .Select(b => b.Title)
            .OrderBy(b => b).ToList();

        return result;
    }

    /// <summary>
    /// Retrieve from the catalog a set of books by the specified author name, sorted alphabetically.
    /// </summary>
    /// <param name="author"></param>
    /// <returns>A list of tuples where each tuple contains an author and their corresponding book count.</returns>
    public List<Book> GetBooksByAuthor(Author author)
    {
        var books = _books.Values
            .Where(b => b.Authors!.Contains(author))
            .ToList();

        return books;
    }

    /// <summary>
    /// Get from the catalog a set of tuples of the form “author’s name – the number of his books in the catalog” for all authors.
    /// </summary>
    /// <returns>A list of tuples where each tuple contains an author and their corresponding book count.</returns>
    public List<(Author, int)> GetNumberOfBooksByAuthor()
    {
        var result = _books
            .SelectMany(a => a.Value.Authors)
            .GroupBy(author => author)
            .Select(group => (group.Key, group.Count())).ToList();

        return result;
    }

    /// <summary>
    /// Get book by ISBN. If ISBN has format XXX-X-XX-XXXXXX-X, convert to XXXXXXXXXXXXX
    /// </summary>
    /// <param name="isbn">The ISBN of the book to retrieve.</param>
    /// <returns>The book with the specified ISBN, or null if no book is found.</returns>
    public Book? GetBookByIsbn(string isbn)
    {
        var book = _books
            .Where(b => b.Key.ToString() == isbn)
            .Select(b => b.Value)
            .FirstOrDefault();

        return book;
    }

    /// <summary>
    /// Retrieves all books currently stored in the catalog.
    /// </summary>
    /// <returns>A list of all books in the catalog.</returns>
    public List<Book> GetAllBooks()
    {
        return _books.Values.ToList();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Catalog catalog)
        {
            return false;
        }
        
        return catalog._books.Values.OrderBy(book => book.Title).SequenceEqual(_books.Values.OrderBy(book => book.Title));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_books.Values);
    }
}
