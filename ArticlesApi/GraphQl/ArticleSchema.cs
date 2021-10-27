using System;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class ArticleSchema : FederatedSchema
    {
        public ArticleSchema(IServiceProvider provider, ArticleQuery query)
            : base(provider)
        {
            Query = query;
        }
    }
}