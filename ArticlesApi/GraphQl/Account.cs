using System.Collections.Generic;

namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class Account
    {
        public int Id { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
