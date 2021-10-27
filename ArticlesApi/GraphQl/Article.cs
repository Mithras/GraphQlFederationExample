namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
