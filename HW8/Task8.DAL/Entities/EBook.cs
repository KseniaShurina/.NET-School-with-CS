using Task8.DAL.Entities;

namespace Task8.DAL.Entities;

public class EBook : Book
{
    public string Identifier { get; }

    // A list of available electronic formats.
    public List<string>? Formats { get; private set; }

    public EBook(string title, IEnumerable<Author> authors, string identifier, IEnumerable<string>? formats = null) :
        base(title, authors)
    {
        Identifier = identifier;
        if (formats != null)
        {
            Formats = new List<string>(formats);
        }
    }

    public override string ToString() => $"Title: {Title}, Identifier: {Identifier}";
}
