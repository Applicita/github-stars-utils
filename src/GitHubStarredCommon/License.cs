namespace GitHubStarredCommon;

public record License
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Spdx_Id { get; set; }
    public string Node_Id { get; set; }
    public string Html_Url { get; set; }
}