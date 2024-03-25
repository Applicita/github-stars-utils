namespace GitHubStarredCommon;

public record Owner
{
    public string Login { get; set; }
    public int Id { get; set; }
    public string Node_Id { get; set; }
    public string Avatar_Url { get; set; }
    public string Html_Url { get; set; }
}