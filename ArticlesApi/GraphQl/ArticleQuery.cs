using GraphQL;
using GraphQL.Types;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.ArticlesApi.GraphQl
{
    public class ArticleQuery : FederatedQuery
    {
        public ArticleQuery(ArticleRepository articleRepository)
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ArticleType>>>>(
                "articles",
                resolve: ctx =>
                {
                    return articleRepository.GetAll();
                });

            Field<ArticleType>(
                "article",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<int>("id");
                    return articleRepository.GetById(id);
                });
        }
    }
}
