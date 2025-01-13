using Task7.DAL.DTO;
using Task7.DAL.Entities;

namespace Task7.DAL.DtoExtensions;

internal static class DtoExtensionHelper
{
    public static DtoBook MapToDtoBook(this Book book)
    {
        var identifiers = new List<string>();
        if (book is PaperBook paperBook)
        {
            // Assign the list of ISBNs for PaperBook
            identifiers = paperBook.Isbns;
        }
        else if (book is EBook eBook)
        {
            // Assign the identifier for EBook
            identifiers.Add(eBook.Identifier);
        }
        return new DtoBook
        {
            Title = book.Title,
            Identifiers = identifiers,
            Authors = book.Authors!.Select(a => a.MapToDtoAuthor()).ToList(),
        };
    }

    public static DtoAuthor MapToDtoAuthor(this Author author)
    {
        return new DtoAuthor
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirthday = author.DateOfBirthday,
        };
    }

    public static DtoCatalog MapToDtoCatalog(this Catalog catalog)
    {
        DtoCatalog dtoCatalog = new DtoCatalog();
        var list = catalog.GetAllBooks();
        foreach (var book in list)
        {
            dtoCatalog.Books.Add(book.MapToDtoBook());
        }
        return dtoCatalog;
    }
}
