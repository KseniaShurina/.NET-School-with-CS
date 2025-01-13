using Task8.DAL.Validators;

namespace Task8.DAL.Entities;

public class Author
{
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime? DateOfBirthday { get; }

    public Author(string firstName, string lastName, DateTime? dateOfBirthday)
    {
        if (!EntityValidator.AcceptNameOfAuthor(firstName, lastName))
        {
            throw new ArgumentException("Invalid format of name");
        }

        FirstName = firstName;
        LastName = lastName;
        DateOfBirthday = dateOfBirthday;
    }

    public override string ToString() => $"{FirstName} {LastName} {DateOfBirthday?.ToShortDateString() ?? null}";

    public override bool Equals(object? obj)
    {
        if (obj is not Author author)
        {
            return false;
        }

        // && (And) checks that all conditions are true.
        if (FirstName != author.FirstName && LastName != author.LastName && DateOfBirthday != author.DateOfBirthday)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, DateOfBirthday);
    }
}