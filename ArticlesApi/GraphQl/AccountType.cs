using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class AccountType : FederatedObjectGraphType<Account>
    {
        public AccountType(ArticleRepository articleRepository, IDataLoaderContextAccessor accessor)
        {
            Name = nameof(Account);

            ExtendByKey("id");

            Field(x => x.Id).External();

            FieldAsync<NonNullGraphType<ListGraphType<NonNullGraphType<ArticleType>>>>(
                nameof(Account.Articles),
                resolve: async ctx =>
                {
                    return articleRepository.GetByAccountId(ctx.Source.Id);
                });

            ResolveReference(ctx =>
            {
                var id = (int)ctx.Arguments["id"];
                return new Account
                {
                    Id = id
                };
            });
        }
    }
}
