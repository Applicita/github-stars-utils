using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using CsvHelper;
using GitHubStarredCommon;

namespace GitHubStarredListGenerator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            string filePath;

            if (args.Length > 0)
            {
                // If a filename is provided as a command line argument, use it
                filePath = args[0];
            }
            else
            {
                // Otherwise, ask the user to input the filename
                Console.WriteLine("Please enter the path to the CSV file:");
                filePath = Console.ReadLine();
            }

            // Ensure that filePath is not null or empty
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("No file path provided. Exiting application.");
                return;
            }

            var records = new List<GitHubRepo>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GitHubRepoMap>();
                records = csv.GetRecords<GitHubRepo>().ToList();
            }

            Console.WriteLine($"{records.Count}: Starred GitHub repos imported");
            
            await CreateLists(records);

            Console.WriteLine("Processing complete.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static async Task CreateLists(List<GitHubRepo> records)
        {
            throw new NotImplementedException();
        }
    }
}
