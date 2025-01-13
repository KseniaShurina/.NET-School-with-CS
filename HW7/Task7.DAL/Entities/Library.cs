namespace Task7.DAL.Entities;

public class Library
{
    // All books are `PaperBook` or all books are `EBook`
    public Catalog Catalog { get; }
    // For PaperBooks catalog - list of all publishers, for EBook catalog - list of all available electronic formats
    public List<string> PressReleaseItems { get; }

    public Library(Catalog catalog, IEnumerable<string> pressReleaseItems)
    {
        Catalog = catalog;
        PressReleaseItems = pressReleaseItems.ToList();
    }

    public override bool Equals(object? obj)
    {

        if (obj is not Library library)
        {
            return false;
        }

        if (!PressReleaseItems.Equals(library.PressReleaseItems))
        {
            return false;
        }

        return Catalog.Equals(library.Catalog);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Catalog, PressReleaseItems);
    }
}