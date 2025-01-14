using Task8.BL.Factories;
using Task8.BL.Services;
using Task8.DAL.Entities;
using Task8.DAL.Interfaces;
using Task8.DAL.Providers;

//Providers
IDataProvider xmlDataProvider = new XmlDataProvider();
IDataProvider jsonDataProvider = new JsonDataProvider();
IDataProvider scvDataProvider = new CsvDataProvider();

//SCV
var csvBooks = await scvDataProvider.GetBooks();

var paperBooks = csvBooks.Where(book => book is PaperBook).ToList();
var eBooks = csvBooks.Where(book => book is EBook).ToList();

// Create LibraryBuilder
var builder = LibraryBuilder.GetInstance();
// Create paper library
var paperLibrary = builder.BuildLibrary("Paper", new List<Book>(paperBooks));
// Create e library
var eLibrary = builder.BuildLibrary("Electronic", new List<Book>(eBooks));

//XML
await xmlDataProvider.SaveBooks(paperLibrary.Catalog.GetAllBooks());
Console.WriteLine();
Console.WriteLine("Books from XML:");
foreach (var book in await xmlDataProvider.GetBooks())
{
    Console.WriteLine(book);
}

//JSON
await jsonDataProvider.SaveBooks(paperLibrary.Catalog.GetAllBooks());
Console.WriteLine();
Console.WriteLine("Books from JSON:");
foreach (var book in await jsonDataProvider.GetBooks())
{
    Console.WriteLine(book);
}

// GetBookTitles
Console.WriteLine("GetAsync Book Titles:");
var bookTitles = paperLibrary.Catalog.GetBookTitles();
foreach (var title in bookTitles)
{
    Console.WriteLine(title);
}

// GetBooksByAuthor
Console.WriteLine();
Console.WriteLine("GetAsync Books By Author:");
var firstAuthor = paperLibrary.Catalog.GetAllBooks().FirstOrDefault(b => b.Authors.Any()).Authors.First();
Console.WriteLine(firstAuthor.ToString());
var booksByAuthor = paperLibrary.Catalog.GetBooksByAuthor(firstAuthor);
foreach (var book in booksByAuthor)
{
    Console.WriteLine(book);
}

// GetBookByIsbn
//TODO: A book may contain many identifiers. We can add a book with one id and look for a different id
Console.WriteLine();
Console.WriteLine("GetAsync Book By Isbn:");
var bookByIsbn1 = paperLibrary.Catalog.GetBookByIsbn("703424633");
Console.WriteLine($"{nameof(bookByIsbn1)}: {bookByIsbn1}");
var bookByIsbn2 = paperLibrary.Catalog.GetBookByIsbn("0385730861");
Console.WriteLine($"{nameof(bookByIsbn2)}: {bookByIsbn2}");

//GetNumberOfBooksByAuthor
Console.WriteLine();
Console.WriteLine("GetAsync Number Of Books By Author:");
foreach (var author in paperLibrary.Catalog.GetNumberOfBooksByAuthor())
{
    Console.WriteLine(author);
}

//EBookService to get out number of pages
Console.WriteLine();
Console.WriteLine("Amount of pages of each EBook:");
var ebookService = new EBookService();
await ebookService.InitializePagesForEBooksAsync(eLibrary);