namespace Task7.DAL.DTO;

public class DtoBook
{
    public string Title { get; set; }
    public List<string> Identifiers { get; set; }
    public List<DtoAuthor> Authors { get; set; }

    public DtoBook() { }
}
