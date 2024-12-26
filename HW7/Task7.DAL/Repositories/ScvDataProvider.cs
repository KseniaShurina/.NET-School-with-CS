﻿using System.Globalization;
using CsvHelper;
using Task7.DAL.DTO;
using Task7.DAL.Entities;
using Task7.DAL.Interfaces;
using Task7.DAL.Validators;

namespace Task7.DAL.Repositories;

public class ScvDataProvider : IDataProvider
{
    public ScvDataProvider() { }

    private readonly IRepository<string> _scvRepository = new ScvRepository<string>();
    private const string PathToFile =
        @"D:\Xeni\Repositories\Coherent-solutions-.NET-School\HW7\Files\books_info.csv";

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

    

    public PaperBook CreatePaperBook(CsvBook csvBook)
    {
        var title = CsvConverter.ConvertToTitle(csvBook.Title);
        var authors = CsvConverter.CreateAuthor(csvBook.Creator);
        var isbns = CsvConverter.ConvertToListOfIdentifiers(csvBook.RelatedExternalId);
        var publicationDate = DateTime.Parse(csvBook.PublicationDate);
        // Could be many publishers
        var publishers = CsvConverter.ConvertToListOfPublishers(csvBook.Publisher);

        // Only for electronic books?
        //var identifier = CsvConverter.ConvertIdentifier(csvBook.Identifier);
        if (string.IsNullOrEmpty(title) || authors == null || isbns == null)
        {
            return null;
        }
        return new PaperBook(title, authors, isbns, publicationDate, csvBook.Publisher);
    }

    public EBook CreateEBook(CsvBook csvBook)
    {
        var title = CsvConverter.ConvertToTitle(csvBook.Title);
        var authors = CsvConverter.CreateAuthor(csvBook.Creator);
        var identifier = CsvConverter.ConvertIdentifier(csvBook.Identifier);
        var formats = CsvConverter.ConvertToListOfFormats(csvBook.Formats);
        if (string.IsNullOrEmpty(title) || authors == null || string.IsNullOrEmpty(identifier))
        {
            return null;
        }

        return new EBook(title, authors, identifier, formats);
    }
}

