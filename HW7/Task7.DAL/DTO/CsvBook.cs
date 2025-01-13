using CsvHelper.Configuration.Attributes;

namespace Task7.DAL.DTO;

public class CsvBook
{
    // Name is mapping between a column name in the CSV and a class property.
    [Name("creator")]
    public string Creator { get; set; }

    [Name("format")]
    public string Formats { get; set; }

    [Name("identifier")]
    public string Identifier { get; set; }

    [Name("publicdate")]
    public string PublicationDate { get; set; }

    [Name("publisher")]
    public string Publisher { get; set; }

    [Name("related-external-id")]
    public string? RelatedExternalId { get; set; }

    [Name("title")]
    public string Title { get; set; }
}

