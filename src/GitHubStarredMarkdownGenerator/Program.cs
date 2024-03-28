using System.Globalization;
using System.Text;
using CsvHelper;
using GitHubStarredCommon;

namespace GitHubStarredMarkdownGenerator;

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
            records = csv.GetRecords<GitHubRepo>()
                .Select(LowerCaseRepo)
                .ToList();
        }

        Console.WriteLine($"{records.Count}: Starred GitHub repos imported");

        // create a distinct list of record.Lists and for each entry in the list, create a markdown file

        var distinctLists = records.SelectMany(record => record.Lists).Distinct().ToList();

        if(distinctLists.Count == 0)
        {
         distinctLists.Add("all");
        }
        
        foreach (var list in distinctLists)
        {
            var markdownFileName = Path.ChangeExtension(list + ".txt", ".md"); // Using list name for file name

            var reposForThisList = records
                .Where(record => record.Lists.Contains(list) || list == "all")
                .OrderBy(record => record.Name)
                .ToList();

            // Call the function to export to Markdown
            await ExportToMarkdown(reposForThisList, markdownFileName);
        }

        Console.WriteLine("Processing complete.");

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

    }

    public static async Task ExportToMarkdown(List<GitHubRepo> repositories, string fileName)
    {
        var markdownBuilder = new StringBuilder();

        foreach (var repo in repositories)
        {
            markdownBuilder.AppendLine($"## {repo.Name}");
            markdownBuilder.AppendLine($"**Owner:** {repo.Owner?.Login ?? "N/A"}  ");
            markdownBuilder.AppendLine($"**URL:** [{repo.Html_Url}]({repo.Html_Url})  ");
            markdownBuilder.AppendLine($"**Description:** {repo.Description ?? "N/A"}  ");
            markdownBuilder.AppendLine($"**Language:** {repo.Language ?? "N/A"}  ");
            markdownBuilder.AppendLine($"**Created At:** {repo.Created_At.ToString("yyyy-MM-dd")}  ");
            markdownBuilder.AppendLine($"**Last Pushed:** {repo.Pushed_At.ToString("yyyy-MM-dd")}  ");
            if (repo.Topics?.Any() == true)
            {
                markdownBuilder.AppendLine($"**Topics:** {string.Join(", ", repo.Topics)}  ");
            }
            markdownBuilder.AppendLine();
        }

        var downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GitHubStarredUtils","Markdown");

        if (!Directory.Exists(downloadsPath))
        {
            Directory.CreateDirectory(downloadsPath);
        }

        var filePath = Path.Combine(downloadsPath, fileName);

        var markdownFilePath = Path.Combine(downloadsPath, fileName);
        
        await File.WriteAllTextAsync(markdownFilePath, markdownBuilder.ToString());
        
        Console.WriteLine($"Markdown File was created at {markdownFilePath}");
    }
    
    private static GitHubRepo LowerCaseRepo(GitHubRepo repo)
    {
        // Convert all necessary string properties to lower-case
        repo.Topics = repo.Topics?.Select(t => t.ToLower()).ToList()!;
        repo.Lists = repo.Lists?.Select(t => t.ToLower()).ToList()!;
        
        return repo;
    }
}