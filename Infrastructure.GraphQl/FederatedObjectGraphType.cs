using System;
using System.Threading.Tasks;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL.Utilities.Federation;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    /// <summary>
    /// Can be replaced with ObjectGraphType extensions, the same as above - idea to allow us to define type directives, e.g.:
    /// public class AcmeType: ObjectGraphType {
    ///   public AcmeType() {
    ///     // Metadata["__AST_MetaField__"] = new GraphQLObjectTypeDefinition() {...};
    ///     Key("id");
    ///   }
    /// }
    ///
    /// TODO:
    /// - there is only two cases: `type Account @key(fields:"id")` and `extend type Account @key(fields:"id")`
    /// - will be nice to have to enforce declaration one of above
    /// - will be nice to have to enforce resolve reference declaration
    /// </summary>
    /// <typeparam name="TSourceType"></typeparam>
    public class FederatedObjectGraphType<TSourceType> : ObjectGraphType<TSourceType>
    {
        public void Key(string fields) => this.BuildAstMeta("key", fields);

        public void ExtendByKey(string fields)
        {
            this.BuildExtensionAstMeta("key", fields);
            Key(fields);
        }

        public void ResolveReference(Func<FederatedResolveContext, TSourceType> resolver) =>
            ResolveReference(new SyncFuncFederatedResolver<TSourceType>(resolver));
        public void ResolveReference(Func<FederatedResolveContext, Task<TSourceType>> resolver) =>
            ResolveReference(new FuncFederatedResolver<TSourceType>(resolver));
        public void ResolveReference(Func<FederatedResolveContext, IDataLoaderResult<TSourceType>> resolver) =>
            ResolveReference(new DataLoaderFederatedResolver<TSourceType>(resolver));
        public void ResolveReference(IFederatedResolver resolver)
        {
            // Metadata[FederatedSchemaBuilder.RESOLVER_METADATA_FIELD] = resolver;
            Metadata["__FedResolver__"] = resolver;
        }

        public string GetResolveReferenceLoaderKey() => $"{nameof(TSourceType)}-ResolveReference";


        public class DataLoaderFederatedResolver<T> : IFederatedResolver
        {
            private readonly Func<FederatedResolveContext, IDataLoaderResult<T>> _resolver;

            public DataLoaderFederatedResolver(Func<FederatedResolveContext, IDataLoaderResult<T>> resolver)
            {
                _resolver = resolver;
            }

            public Task<object> Resolve(FederatedResolveContext context)
            {
                return Task.FromResult<object>(_resolver(context));
            }
        }

        public class SyncFuncFederatedResolver<T> : IFederatedResolver
        {
            private readonly Func<FederatedResolveContext, T> _resolver;

            public SyncFuncFederatedResolver(Func<FederatedResolveContext, T> resolver)
            {
                _resolver = resolver;
            }

            public Task<object> Resolve(FederatedResolveContext context)
            {
                return Task.FromResult<object>(_resolver(context));
            }
        }
    }
}
