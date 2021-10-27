using System;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities.Federation;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    /// <summary>
    /// Small extensions just to hide registration of required types, can be extension instead
    /// </summary>
    public class FederatedSchema : Schema
    {
        public FederatedSchema() : this(new DefaultServiceProvider()) { }
        public FederatedSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.RegisterType<AnyScalarGraphType>();
            this.RegisterType<ServiceGraphType>();
            this.RegisterType<EntityType>();
        }
    }
}
