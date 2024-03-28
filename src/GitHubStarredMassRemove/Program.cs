using System.Configuration;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using CsvHelper;
using GitHubStarredCommon;
using GitHubStarredCommon;

namespace GitHubStarredMassRemove
{
    internal class Program
    {
        private static readonly HttpClient Client = new();
        
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

            // Initialize HttpClient
            var token = ConfigurationManager.AppSettings["GitHubToken"]
                        ?? throw new ApplicationException("Cannot find GitHub Personal Token");

            Client.BaseAddress = new Uri("https://api.github.com");
            Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            foreach (var repo in records)
            {
                // Call the function to unstar github repositories
                
                await UnstarGitHubRepo(repo,Client);
            }

            Console.WriteLine("Processing complete.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        public static async Task UnstarGitHubRepo(GitHubRepo repository, HttpClient httpClient)
        {
                // Build the request URI
                string requestUri = $"/user/starred/{repository.Owner.Login}/{repository.Name}";

                Console.WriteLine($"Removing Star from {requestUri}");

                // Send a DELETE request to the unstar endpoint
                var response = await httpClient.DeleteAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error unstarring repo: {content}");
                    return;
                }

                Console.WriteLine($"Successfully Removed Star from {repository.Owner.Login}/{repository.Name}");

        }
    }
}
