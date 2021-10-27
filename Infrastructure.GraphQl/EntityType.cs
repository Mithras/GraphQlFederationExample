using System;
using System.Collections.Generic;
using GraphQL.Types;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    public class EntityType : UnionGraphType
    {
        public EntityType(IEnumerable<Type> types)
        {
            Name = "_Entity";

            foreach (var type in types)
            {
                Type(type);
            }
        }
    }
}
