using Task7.BL.Factories;
using Task7.DAL.Entities;
using Task7.DAL.Interfaces;
using Task7.DAL.Repositories;

// Create authors
var author1 = new Author("George", "Orwell", new DateTime(1903, 6, 25));
var author2 = new Author("Yuval Noah", "Harari", new DateTime(1976, 2, 24));
var author3 = new Author("J.K.", "Rowling", new DateTime(1965, 7, 31));
var author4 = new Author("Jane", "Austen", new DateTime(1775, 12, 16));

// Create PaperBook
var paperBook1 = new PaperBook(
    "1984",
    new HashSet<Author> { author1 },
    new List<string> { "9780451524935", "9780451526342", "9780141036144" },
    new DateTime(1949, 6, 8),
    "Secker & Warburg"
);

var paperBook2 = new PaperBook(
    "Sapiens: A Brief History of Humankind",
    new HashSet<Author> { author2 },
    new List<string> { "9780062316097", "9780062316110", "9780099590088" },
    new DateTime(2011, 9, 4),
    "Harper"
);

var paperBook3 = new PaperBook(
    "Pride and Prejudice",
    new HashSet<Author> { author4 },
    new List<string> { "9780141439518", "9780199535569", "9780141199078" },
    new DateTime(1813, 1, 28),
    "T. Egerton"
);

var paperBook4 = new PaperBook(
    "Harry Potter and the Philosopher's Stone",
    new HashSet<Author> { author3 },
    new List<string> { "9780747532743", "9780439554930", "9781408855898" },
    new DateTime(1997, 6, 26),
    "Bloomsbury"
);

var paperBook5 = new PaperBook(
    "Homo Deus: A Brief History of Tomorrow",
    new HashSet<Author> { author2 },
    new List<string> { "9780062464316", "9780099590088" },
    new DateTime(2015, 9, 4),
    "Harper"
);

var paperBook6 = new PaperBook(
    "21 Lessons for the 21st Century",
    new HashSet<Author> { author2 },
    new List<string> { "9780525512172", "9781787330672" },
    new DateTime(2018, 8, 30),
    "Penguin"
);

// Create EBook
var eBook1 = new EBook(
    "Sapiens: A Brief History of Humankind",
    new HashSet<Author> { author2 },
    "heidispryi00spyr",
    new List<string> { "PDF", "EPUB", "MOBI" }
);
var eBook2 = new EBook(
    "Harry Potter and the Chamber of Secrets",
    new HashSet<Author> { author3 },
    "harrychamber00rowling",
    new List<string> { "EPUB", "MOBI" }
);

var eBook3 = new EBook(
    "Emma",
    new HashSet<Author> { author4 },
    "emmaausten00jane",
    new List<string> { "PDF", "EPUB" }
);

var eBook4 = new EBook(
    "Homo Deus: A Brief History of Tomorrow",
    new HashSet<Author> { author2 },
    "jrfghjbryi00spyr",
    new List<string> { "PDF", "EPUB", "MOBI" }
);

var builder = LibraryBuilder.GetInstance();
var paperLibrary = builder.BuildLibrary("Paper", new List<Book>(){ paperBook1, paperBook2, paperBook3, paperBook4, paperBook5, paperBook6 });

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
var booksByAuthor = paperLibrary.Catalog.GetBooksByAuthor(author2);
foreach (var book in booksByAuthor)
{
    Console.WriteLine(book);
}

// GetBookByIsbn
Console.WriteLine();
Console.WriteLine("GetAsync Book By Isbn:");
var bookByIsbn1 = paperLibrary.Catalog.GetBookByIsbn("9780062316097"); // paperBook2
Console.WriteLine($"{nameof(bookByIsbn1)}: {bookByIsbn1}");
var bookByIsbn2 = paperLibrary.Catalog.GetBookByIsbn("9780141439518"); // paperBook3
Console.WriteLine($"{nameof(bookByIsbn2)}: {bookByIsbn2}");

//GetNumberOfBooksByAuthor
Console.WriteLine();

Console.WriteLine("GetAsync Number Of Books By Author:");
foreach (var author in paperLibrary.Catalog.GetNumberOfBooksByAuthor())
{
    Console.WriteLine(author);
}

//Providers
IDataProvider xmlDataProvider = new XmlDataProvider();
IDataProvider jsonDataProvider = new JsonDataProvider();
IDataProvider scvDataProvider = new ScvDataProvider();

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

//SCV
var books = await scvDataProvider.GetBooks();
foreach (var book in books)
{
    Console.WriteLine(book);
}