using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using GitHubStarredCommon;
using System.Globalization;

public sealed class GitHubRepoMap : ClassMap<GitHubRepo>
{
    public GitHubRepoMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Topics).TypeConverter<SemicolonListConverter>();
        Map(m => m.Lists).TypeConverter<SemicolonListConverter>();
    }
}

public class SemicolonListConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return text?.Split(';').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new List<string>();
    }
    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value is List<string> list ? string.Join(";", list) : "";
    }
}