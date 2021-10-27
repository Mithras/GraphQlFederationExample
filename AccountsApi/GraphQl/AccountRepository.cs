using System.Collections.Generic;
using System.Linq;

namespace Microsoft.EcoManager.AccountsApi.GraphQl
{
    public class AccountRepository
    {
        private static readonly Account[] Accounts = new[]
        {
            new Account
            {
                Id = 1,
                Name = "Name-1"
            },
            new Account
            {
                Id = 2,
                Name = "Name-2"
            },
            new Account
            {
                Id = 3,
                Name = "Name-3"
            }
        };

        public IEnumerable<Account> GetAll() => Accounts;
        public Account GetById(int id) => Accounts.FirstOrDefault(x => x.Id == id);
    }
}
