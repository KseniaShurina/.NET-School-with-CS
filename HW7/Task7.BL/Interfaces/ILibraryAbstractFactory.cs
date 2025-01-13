using Task7.DAL.Entities;

namespace Task7.BL.Interfaces;

public interface ILibraryAbstractFactory
{
    public Catalog CreateCatalog(IEnumerable<Book> books);
    public IEnumerable<string> CreatePressReleaseItems(IEnumerable<Book> books);
}