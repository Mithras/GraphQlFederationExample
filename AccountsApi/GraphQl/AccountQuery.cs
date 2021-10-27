using GraphQL;
using GraphQL.Types;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.AccountsApi.GraphQl
{
    public class AccountQuery : FederatedQuery
    {
        public AccountQuery(AccountRepository accountRepository)
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<AccountType>>>>(
                "accounts",
                resolve: ctx =>
                {
                    return accountRepository.GetAll();
                });

            Field<AccountType>(
                "account",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<int>("id");
                    return accountRepository.GetById(id);
                });
        }
    }
}
