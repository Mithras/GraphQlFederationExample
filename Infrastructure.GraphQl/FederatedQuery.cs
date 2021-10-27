using System.Reflection;
using GraphQL.Types;
using GraphQL.Utilities.Federation;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    /// <summary>
    /// Small helper which does add Query._service and Query._entities
    /// </summary>
    public class FederatedQuery : ObjectGraphType
    {
        public FederatedQuery()
        {
            // https://github.com/graphql-dotnet/graphql-dotnet/blob/master/src/GraphQL/Utilities/Federation/FederatedSchemaBuilder.cs
            var addRootEntityFields = typeof(FederatedSchemaBuilder).GetMethod("AddRootEntityFields", BindingFlags.Instance | BindingFlags.NonPublic);
            addRootEntityFields.Invoke(new FederatedSchemaBuilder(), new[] { new Schema { Query = this } });
        }
    }
}
