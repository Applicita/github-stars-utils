using System.Configuration;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;


internal class Program
{
    static readonly HttpClient client = new HttpClient();
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Starting GitHub Starred Repositories Exporter...");

        try
        {
            var token = ConfigurationManager.AppSettings["GitHubToken"]
                        ?? throw new ApplicationException("Cannot find GitHub Personal Token");

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filename = $"starred_repos_{timestamp}.csv";

            var repositories = await GetStarredRepositories();


            await ExportToCsv(repositories,filename);

            Console.WriteLine("Export complete. Check the starred_repos.csv file.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
     
    }

    private static async Task ExportToCsv(List<GitHubRepo> repositories,string fileName)
    {
        var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        if (!Directory.Exists(downloadsPath))
        {
            Directory.CreateDirectory(downloadsPath);
        }
        
        var filePath = Path.Combine(downloadsPath, fileName);

        await using (var writer = new StreamWriter(filePath))
        await using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<GitHubRepoMap>();
            await csv.WriteRecordsAsync(repositories.OrderByDescending(repo => repo.Pushed_At));
        }

        Console.WriteLine($"CSV was created at {filePath}");

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public sealed class GitHubRepoMap : ClassMap<GitHubRepo>
    {
        public GitHubRepoMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Topics).TypeConverter<SemicolonListConverter>();
        }
    }
    public class SemicolonListConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value is List<string> list ? string.Join(";", list) : "";
        }
    }


    private static async Task<List<GitHubRepo>> GetStarredRepositories()
    {
        var repositories = new List<GitHubRepo>();

        var url = "https://api.github.com/user/starred";
        var pageNumber = 1;

        while (url != null)
        {
            Console.WriteLine($"Fetching page {pageNumber} of starred repositories...");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Output header information
            if (response.Headers.Contains("X-RateLimit-Limit"))
            {
                Console.WriteLine("Rate Limit: " + response.Headers.GetValues("X-RateLimit-Limit").FirstOrDefault());
            }
            if (response.Headers.Contains("X-RateLimit-Remaining"))
            {
                Console.WriteLine("Rate Limit Remaining: " + response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault());
            }

            //var reposString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"{reposString}");
            
            var repos = await response.Content.ReadFromJsonAsync<List<GitHubRepo>>();
            
            if(repos != null)
            {
                repositories.AddRange(repos);
            }

            if (response.Headers.Contains("Link"))
            {
                // Extract next page URL from Link header (if present)
                url = ExtractNextPageUrl(response.Headers.GetValues("Link"));
                pageNumber++;
            }
            else
            {
                url = null;
            }
        }

        Console.WriteLine($"Fetched {repositories.Count} repositories in total.");

        return repositories;
    }

    static string? ExtractNextPageUrl(IEnumerable<string> linkHeader)
    {
        foreach (var part in linkHeader)
        {
            // Split the parts of the link header
            var sections = part.Split(',');

            foreach (var section in sections)
            {
                // Identify the section that contains 'rel="next"'
                if (section.Contains("rel=\"next\""))
                {
                    // Extract and return the URL
                    var startIndex = section.IndexOf('<') + 1;
                    var endIndex = section.IndexOf('>');
                    return section.Substring(startIndex, endIndex - startIndex);
                }
            }
        }

        return null;
    }


    public record GitHubRepo
    {
        public int Id { get; set; }
        public string Node_Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public Owner Owner { get; set; }
        public string Html_Url { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public License License { get; set; }
        public DateTime Pushed_At { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public List<string> Topics { get; set; }
    }

    public record Owner
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string Node_Id { get; set; }
        public string Avatar_Url { get; set; }
        public string Html_Url { get; set; }
    }

    public record License
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Spdx_Id { get; set; }
        public string Node_Id { get; set; }
        public string Html_Url { get; set; }
    }
}