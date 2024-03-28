namespace GitHubStarredCommon
{

    public record GitHubRepo
    {
        public int Id { get; set; }
        public string Node_Id { get; set; }
        public string Name { get; set; }
        public string Full_Name { get; set; }
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
        public List<string> Lists { get; set; }
    }
}


