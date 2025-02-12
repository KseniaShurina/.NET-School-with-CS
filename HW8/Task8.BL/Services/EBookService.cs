using PuppeteerSharp;
using System.Text.RegularExpressions;
using Task8.DAL.Entities;

namespace Task8.BL.Services
{
    internal class EBookService
    {
        private const string BaseUrl = "https://archive.org/details/";
        private const string RegexToGetOutAmountOfBookPages = @"(\d+)(?=\))";

        public async Task InitializePagesForEBooksAsync(Library library)
        {
            var eBooks = library.Catalog.GetAllBooks().OfType<EBook>();
            foreach (var eBook in eBooks)
            {
                if (!string.IsNullOrEmpty(eBook.Identifier))
                {
                    eBook.Pages = await GetNumberOfPagesAsync(eBook.Identifier);
                    Console.WriteLine($"EBook: {eBook.Title}, Pages: {eBook.Pages ?? null}");
                }
            }
        }

        public async Task<int?> GetNumberOfPagesAsync(string identifier)
        {
            string url = $"{BaseUrl}{identifier}";

            try
            {
                await new BrowserFetcher().DownloadAsync(BrowserTag.Stable);

               await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true
                });

               await using var page = await browser.NewPageAsync();

                // Go to a book
                await page.GoToAsync(url);

                // Waiting for span.BRcurrentpage to load
                await page.WaitForSelectorAsync("span.BRcurrentpage");

                var spanContent = await page.EvaluateExpressionAsync<string>(
                    "document.querySelector('span.BRcurrentpage')?.textContent");

                if (spanContent == null)
                {
                    return null;
                }

                // Get number from string
                var match = Regex.Match(spanContent, RegexToGetOutAmountOfBookPages);
                if (match.Success)
                {
                    return int.Parse(match.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching pages: {ex.Message}, Book's identifier: {identifier}");
            }

            return null;
        }
    }
}
