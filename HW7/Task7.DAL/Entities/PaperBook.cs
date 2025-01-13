namespace Task7.DAL.Entities;

public class PaperBook : Book
{
    public List<string> Isbns { get; }
    public DateTime? PublicationDate { get; }
    public string? Publisher { get; private set; }

    public PaperBook(string title, IEnumerable<Author> authors, IEnumerable<string> isbns, DateTime? publicationDate = null, 
        string? publisher = null) : base(title, authors)
    {
        Isbns = new List<string>(isbns);
        PublicationDate = publicationDate;
        Publisher = publisher;
    }

    public override string ToString() => $"Title: {Title}, Publication date: {PublicationDate}, Publisher: {Publisher}";
}