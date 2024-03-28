using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GitHubStarredCommon;

public sealed class GitHubRepoMap : ClassMap<GitHubRepo>
{
    public GitHubRepoMap()
    {
        //AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Id);
        Map(m => m.Node_Id);
        Map(m => m.Name);
        Map(m => m.Full_Name);
        References<OwnerMap>(m => m.Owner);
        Map(m => m.Html_Url).Name("RepoHtml_Url");
        Map(m => m.Description);
        Map(m => m.Url).Name("Api_Url");
        Map(m => m.Language);
        References<LicenseMap>(m => m.License);
        Map(m => m.Pushed_At);
        Map(m => m.Created_At);
        Map(m => m.Updated_At);

        // Handling Topics and Lists with custom converter
        Map(m => m.Topics).TypeConverter<SemicolonListConverter>();
        Map(m => m.Lists).TypeConverter<SemicolonListConverter>();
    }
}

public sealed class OwnerMap : ClassMap<Owner>
{
    public OwnerMap()
    {
        // Mapping Owner fields. Since AutoMap is not used here, explicitly map necessary fields
        Map(m => m.Login);
        Map(m => m.Id).Name("OwnerId");
        Map(m => m.Node_Id).Name("OwnerNode_Id");
        Map(m => m.Avatar_Url).Name("OwnerAvatar_Url");
        // Override the Html_Url mapping for Owner
        Map(m => m.Html_Url).Name("OwnerHtml_Url");
    }
}

public sealed class LicenseMap : ClassMap<License>
{
    public LicenseMap()
    {
        Map(m => m.Name);
        Map(m => m.Url).Name("License_Url");
        Map(m => m.Node_Id).Name("LicenceNode_Id");
        Map(m => m.Html_Url).Name("LicenseHtml_Url");
        Map(m => m.Key);
        Map(m => m.Spdx_Id);
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