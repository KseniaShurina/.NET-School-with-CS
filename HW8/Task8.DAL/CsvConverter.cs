using System.Text.RegularExpressions;
using Task8.DAL.Validators;
using Task8.DAL.Entities;

namespace Task8.DAL;

internal static class CsvConverter
{
    private const string RegexPattern = @"^[A-Za-z0-9=\- ()':]+(\?[ ]*)?$";

    private const string NotAllowed = "(?i).*isbn.*";

    public static string? ConvertToTitle(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return null;
        }

        line.Trim();

        if (line.Contains(";"))
        {
            // Remove text after semicolon and white spaces and Normalize
            line = Regex.Replace(line, @";.*", "");
        }

        if (line.Contains(":") && !line.Contains("isbn"))
        {
            // Remove white spaces before colon
            line = Regex.Replace(line, @"\s+:", ":");
        }

        if (!Regex.IsMatch(line, RegexPattern) || Regex.IsMatch(line, NotAllowed))
        {
            return null;
        }

        return line;
    }

    public static List<Author>? ConvertToAuthor(string stringWithAuthorData)
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
                if (firstName != null && lastName != null && Regex.IsMatch(firstName, RegexPattern) && Regex.IsMatch(lastName, RegexPattern))
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

    public static List<string>? ConvertToListOfPublishers(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return null;
        }

        return line.Split(';')
            .Where(publisher => !string.IsNullOrEmpty(publisher))
            .Select(publisher => Regex.Replace(line, @"\s+:", ":").Trim()).ToList();
    }

    public static string? ConvertIdentifier(string line)
    {
        if (line.Contains("isbn"))
        {
            var isbn = Regex.Replace(line, @"\D+", "");

            return EntityValidator.IsIsbn(isbn) ? isbn : null;
        }

        return line;
    }

    public static List<string>? ConvertToListOfIdentifiers(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return null;
        }

        if (line.Contains("isbn"))
        {
            // \D - Means only numbers
            var list = line.Split(',').Select(isbn => Regex.Replace(isbn, @"\D", "")).ToList();

            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (!EntityValidator.IsIsbn(list[i]))
                {
                    list.RemoveAt(i);
                }
            }

            return list.Count == 0 ? null : list;
        }

        return null;
    }
}

