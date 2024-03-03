// See https://aka.ms/new-console-template for more information
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;


internal class Program
{
    static readonly HttpClient client = new HttpClient();
    private static async Task Main(string[] args)
    {
        string? token = ConfigurationManager.AppSettings["GitHubToken"]
            ?? throw new ApplicationException("Cannot find GitHub Personal Token");
        
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var repositories = await GetStarredRepositories();

        await ExportToCsv(repositories);
    }

    private static async Task ExportToCsv(List<GitHubRepo> repositories)
    {
        Console.WriteLine($"Number of Starred Repositories found {repositories.Count}");
    }
    private static async Task<List<GitHubRepo>> GetStarredRepositories()
    {
        var repositories = new List<GitHubRepo>();
        string url = "https://api.github.com/user/starred";

        while (url != null)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var repos = await response.Content.ReadFromJsonAsync<List<GitHubRepo>>();
            
            if(repos != null)
            {
                repositories.AddRange(repos);
            }
            if (response.Headers.Contains("Link"))
            {
                // Extract next page URL from Link header (if present)
                url = ExtractNextPageUrl(response.Headers.GetValues("Link"));
            }
            else
            {
                url = null;
            }
        }

        return repositories;
    }

    static string? ExtractNextPageUrl(IEnumerable<string> linkHeader)
    {
        foreach (var part in linkHeader)
        {
            if (part.Contains("rel=\"next\""))
            {
                return part.Substring(part.IndexOf('<') + 1, part.IndexOf('>') - part.IndexOf('<') - 1);
            }
        }
        return null;
    }

    public class GitHubRepo
    {
        public string Name { get; set; }
        public string Html_Url { get; set; }
        public string Description { get; set; }
        // Add more properties as needed
    }
}