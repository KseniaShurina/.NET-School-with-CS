using System.Text.RegularExpressions;
using Task7.DAL.Entities;

namespace Task7.DAL;

internal static class CsvConverter
{
    public static List<Author>? CreateAuthor(string stringWithAuthorData)
    {
        if (string.IsNullOrEmpty(stringWithAuthorData))
        {
            return null;
        }

        List<string> authorList = new List<string>();
        List<Author> authors = new List<Author>();
        if (stringWithAuthorData.Contains(','))
        {
            var revertedName = stringWithAuthorData.Split(',');
            foreach (var v in revertedName)
            {
                if (!Regex.IsMatch(v, @"\d"))
                {
                    authorList.Add(v);
                }
            }

            string? firstName = null;
            string? lastName = null;
            for (int i = 0; i <= authorList.Count - 1; i++)
            {
                if (i % 2 != 0)
                {
                    firstName = authorList[i].Trim();
                }
                else
                {
                    lastName = authorList[i].Trim();
                }
                if (firstName != null && lastName != null)
                {
                    authors.Add(new Author(firstName, lastName, null));
                    firstName = null;
                    lastName = null;
                }
            }
        }
        else
        {
            var revertedName = stringWithAuthorData.Split(' ');

            if (revertedName.Length == 2)
            {
                authors.Add(new Author(revertedName[0].Trim(), revertedName[1].Trim(), null));
            }
        }

        return authors;
    }

    public static List<string> ConvertToListOfFormats(string line)
    {
        var result = line.Split(',').ToList();

        return result;
    }

    public static string ConvertIdentifier(string line)
    {
        if (line.Contains("isbn"))
        {
            var isbn = Regex.Replace(line, @"\D+", "");
            return isbn;
        }

        return line;
    }
    // New York : Bradbury Press ; Toronto : Maxwell Macmillan Canada ; New York : Maxwell Macmillan International
    // New York : Knopf : Distributed by Random House
    public static List<string> ConvertToListOfPublishers(string line)
    {
        return line.Split(';')
            .Where(publisher => !string.IsNullOrEmpty(publisher))
            .Select(publisher => publisher.Trim()).ToList();
    }

    public static List<string>? ConvertToListOfIdentifiers(string line)
    {
        if (line.Contains("isbn"))
        {
            // \D - Means only numbers
            return line.Split(',').Select(isbn => Regex.Replace(isbn, @"\D", "")).ToList();
        }

        return null;
    }

    public static string? ConvertToTitle(string line)
    {
        return Regex.IsMatch(line, @"[a-z]") ? line.Trim() : null;
    }
}

