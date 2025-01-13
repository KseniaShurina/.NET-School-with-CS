using Xunit;

namespace Task7.DAL.Tests;

public sealed class CsvConverterTests
{
    [Theory]
    [InlineData("Celebrating Ramadan = Rama?an al-mu?a??am", false)]
    [InlineData("Is your mama a llama [text (board books)]", false)]
    [InlineData("La munÌƒeca de Elizabeti", false)]
    [InlineData("urn:isbn:3869304626", false)]
    [InlineData("John Steinbeck's The grapes of wrath", true)]
    [InlineData("How do birds find their way?", true)]
    [InlineData("Coyotes All Around (MathStart 2)", true)]
    [InlineData("26 Fairmount Avenue", true)]
    [InlineData("I have a dream", true)]
    [InlineData("Close to shore : the terrifying shark attacks of 1916", true)]


    public void ConvertToTitle_ValidInput_ReturnsExpectedResult(string input, bool isOk)
    {
        var title = CsvConverter.ConvertToTitle(input);

        Assert.Equal(isOk, !string.IsNullOrEmpty(title));
    }
}