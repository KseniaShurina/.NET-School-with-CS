using System.Globalization;
using CsvHelper;
using Task8.DAL.DTO;
using Task8.DAL.Entities;
using Task8.DAL.Interfaces;
using Task8.DAL.Validators;

namespace Task8.DAL.Providers;

public class CsvDataProvider : IDataProvider
{
    public CsvDataProvider() { }

    private const string PathToFile =
        @"D:\Xeni\Repositories\Coherent-solutions-.NET-School\HW8\Files\books_info.csv";

    public async Task<IEnumerable<Book>> GetBooks()
    {

        var listOfBooks = new List<Book>();

        using (var reader = new StreamReader(PathToFile))
        using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
        {
            var books = csvReader.GetRecords<CsvBook>();

            foreach (var book in books)
            {
                Book? restoredBook = EntityValidator.IsIsbn(book.Identifier)
                    ? CreatePaperBook(book)
                    : CreateEBook(book);

                if (restoredBook != null)
                {
                    listOfBooks.Add(restoredBook);
                }
            }
        }

        return listOfBooks;
    }

    public Task SaveBooks(IEnumerable<Book> paperBooks)
    {
        throw new NotImplementedException();
    }

    public PaperBook? CreatePaperBook(CsvBook csvBook)
    {
        var title = CsvConverter.ConvertToTitle(csvBook.Title);
        var authors = CsvConverter.ConvertToAuthor(csvBook.Creator);
        var isbns = CsvConverter.ConvertToListOfIdentifiers(csvBook.RelatedExternalId);
        var publicationDate = DateTime.Parse(csvBook.PublicationDate);
        //TODO: Could be many publishers
        var publisher = CsvConverter.ConvertToListOfPublishers(csvBook.Publisher)[0] ?? null;

        if (string.IsNullOrEmpty(title) || authors == null || isbns == null || publisher == null)
        {
            return null;
        }

        return new PaperBook(title, authors, isbns, publicationDate, publisher);
    }

    public EBook? CreateEBook(CsvBook csvBook)
    {
        var title = CsvConverter.ConvertToTitle(csvBook.Title);
        var authors = CsvConverter.ConvertToAuthor(csvBook.Creator);
        var identifier = CsvConverter.ConvertIdentifier(csvBook.Identifier);
        var formats = CsvConverter.ConvertToListOfFormats(csvBook.Formats);
        if (string.IsNullOrEmpty(title) || authors == null || string.IsNullOrEmpty(identifier) || formats.Count == 0)
        {
            return null;
        }

        return new EBook(title, authors, identifier, formats);
    }
}

