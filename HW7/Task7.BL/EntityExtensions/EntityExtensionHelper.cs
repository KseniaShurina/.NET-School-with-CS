using Task7.DAL.DTO;
using Task7.DAL.Entities;
using Task7.DAL.Validators;

namespace Task7.BL.EntityExtensions;

public static class EntityExtensionHelper
{
    public static Book MapEntityToBook(this DtoBook book)
    {
        if (EntityValidator.IsIsbn(book.Identifiers[0]))
        {
            return new PaperBook
                (book.Title,
                    new HashSet<Author>(book.Authors.Select(a =>
                        new Author(a.FirstName, a.LastName, a.DateOfBirthday))),
                    book.Identifiers,
                    null,
                    null
                );
        }

        return new EBook
            (book.Title,
                new HashSet<Author>(book.Authors.Select(a =>
                    new Author(a.FirstName, a.LastName, a.DateOfBirthday))),
                book.Identifiers[0],
                null
            );
    }

    public static Author MapToEntityAuthor(this DtoAuthor author)
    {
        return new Author(author.FirstName, author.LastName, author.DateOfBirthday);
    }

    public static Catalog MapToEntityCatalog(this DtoCatalog dtoCatalog)
    {
        Catalog catalog = new Catalog();

        foreach (var book in dtoCatalog.Books)
        {
            catalog.AddBook(book.Identifiers[0], book.MapEntityToBook());
        }
        return catalog;
    }
}
