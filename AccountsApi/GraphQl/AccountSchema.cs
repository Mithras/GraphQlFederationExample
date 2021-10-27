using System;
using Microsoft.EcoManager.Infrastructure.GraphQl;

namespace Microsoft.EcoManager.AccountsApi.GraphQl
{
    public class AccountSchema : FederatedSchema
    {
        public AccountSchema(IServiceProvider provider, AccountQuery query)
            : base(provider)
        {
            Query = query;
        }
    }
}