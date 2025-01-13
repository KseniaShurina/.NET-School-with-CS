using System.Text.RegularExpressions;
using Task7.DAL.Validators;

namespace Task7.DAL.Entities
{
    public class Isbn
    {
        private string _isbn;
        private const string Pattern = @"\d{3}-?\d{1}-?\d{2}-?\d{6}-?\d{1}";

        public string IsbnNumber
        {
            get
            {
                return _isbn;
            }
            private set
            {
                if (value.Length < 12)
                {
                    CreateIsbn(value);
                }

                if (EntityValidator.AcceptIsbn(value)) throw new ArgumentException("ISBN is not valid");
                _isbn = value;
            }
        }

        public Isbn(){}
        public Isbn(string isbn)
        {
            if (Regex.IsMatch(isbn, Pattern))
            {
                IsbnNumber = isbn;
            }
            else
            {
                IsbnNumber = CreateIsbn(isbn);
            }
        }

        string CreateIsbn(string isbn)
        {
            int[] arr = new int[12];
            for (int i = 0; i < isbn.Length; i++)
            {
                arr[i] = int.Parse(isbn[i].ToString());
            }

            var controlNumber = ControlNumber(arr);

            return isbn + controlNumber;
        }

        int ControlNumber(int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                // Even position
                if (i % 2 != 0)
                {
                    sum += array[i] * 3;
                }
                // Odd position
                else
                {
                    sum += array[i];
                }
            }

            int rest = 10 - sum % 10;

            if (rest == 10)
            {
                return 0;
            }
            return rest;
        }

        public override string ToString()
        {
            return IsbnNumber;
        }

        public override int GetHashCode()
        {
            return IsbnNumber.GetHashCode();
        }
    }
}
