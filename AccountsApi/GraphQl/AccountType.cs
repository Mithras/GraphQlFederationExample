using System.Linq;
using GraphQL.DataLoader;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.AccountsApi.GraphQl
{
    public class AccountType : FederatedObjectGraphType<Account>
    {
        public AccountType(AccountRepository accountRepository, IDataLoaderContextAccessor accessor)
        {
            Name = nameof(Account);

            Key("id");

            Field(x => x.Id);
            Field(x => x.Name);

            ResolveReference(ctx => accessor.Context.GetOrAddBatchLoader<int, Account>(
                this.GetResolveReferenceLoaderKey(),
                async (items) =>
                {
                    return items.ToDictionary(id => id, id => accountRepository.GetById(id));
                }
            ).LoadAsync((int)ctx.Arguments["id"]));
        }
    }
}
