using Task8.DAL.Entities;

namespace Task8.BL.Interfaces;

public interface ILibraryAbstractFactory
{
    public Catalog CreateCatalog(IEnumerable<Book> books);
    public IEnumerable<string> CreatePressReleaseItems(IEnumerable<Book> books);
}