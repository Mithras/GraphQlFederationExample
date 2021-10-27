using System.Linq;
using GraphQL.DataLoader;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class ArticleType : FederatedObjectGraphType<Article>
    {
        public ArticleType(ArticleRepository articleRepository, IDataLoaderContextAccessor accessor)
        {
            Name = nameof(Article);

            Key("id");

            Field(x => x.Id);
            Field(x => x.Title);

            Field<AccountType>(
                nameof(Article.Account),
                resolve: ctx =>
                {
                    return new Account { Id = ctx.Source.AccountId };
                });

            // not used
            ResolveReference(ctx => accessor.Context.GetOrAddBatchLoader<int, Article>(
                this.GetResolveReferenceLoaderKey(),
                async (items) =>
                {
                    return items.ToDictionary(id => id, id => articleRepository.GetById(id));
                }
            ).LoadAsync((int)ctx.Arguments["id"]));
        }
    }
}
