using System.Xml.Serialization;
using Task7.DAL.Interfaces;

namespace Task7.DAL.Repositories;

public class XmlRepository<T> : IRepository<T>
{
    public XmlRepository() { }

    private const string PathToFile = @"D:\Xeni\Repositories\Coherent-solutions-.NET-School\HW7\Files\XML files\books.xml";

    public Task SaveAsync(IEnumerable<T> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        using FileStream file = new FileStream(PathToFile, FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(file, items.ToList());

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>?> GetAsync()
    {
        await using FileStream file = new FileStream(PathToFile, FileMode.Open);
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

        var deserializedList = serializer.Deserialize(file) as IEnumerable<T>;

        return deserializedList ?? null;
    }
}
