namespace Task7.DAL.Entities;

public abstract class Book
{
    public string Title { get; }
    public HashSet<Author>? Authors { get; } = new();

    protected Book(string title, IEnumerable<Author>? authors = null)
    {
        Title = title;
        if (authors != null)
        {
            Authors = new HashSet<Author>(authors);
        }
    }

    public override string ToString() => $"Title: {Title}";

    public override bool Equals(object? obj)
    {
        if (obj is not Book book)
        {
            return false;
        }

        // && (And) checks that all conditions are true.
        if (Title != book.Title)
        {
            return false;
        }

        if (book.Authors != null && Authors != null && !Authors.OrderBy(a => a.LastName).SequenceEqual(book.Authors.OrderBy(a => a.LastName)))
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Authors);
    }
}