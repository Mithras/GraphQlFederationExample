using System.Collections.Generic;
using System.Linq;

namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class ArticleRepository
    {
        private static readonly Article[] Articles = new[]
        {
            new Article
            {
                Id = 1,
                Title = "Title-1",
                AccountId = 1
            },
            new Article
            {
                Id = 2,
                Title = "Title-2",
                AccountId = 1
            },
            new Article
            {
                Id = 3,
                Title = "Title-3",
                AccountId = 2
            },
        };

        public IEnumerable<Article> GetAll() => Articles;
        public Article GetById(int id) => Articles.FirstOrDefault(x => x.Id == id);
        public IEnumerable<Article> GetByAccountId(int id) => Articles.Where(x => x.AccountId == id);
    }
}
