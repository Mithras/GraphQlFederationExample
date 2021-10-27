using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphQL.Server;
using GraphQL.Utilities.Federation;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    public static class FederatedGraphQLBuilderExtensions
    {
        public static IGraphQLBuilder AddFederation(this IGraphQLBuilder builder, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => x.IsSubclassGeneric(typeof(FederatedObjectGraphType<>)));
            builder.AddFederation(types);
            return builder;
        }
        public static IGraphQLBuilder AddFederation(this IGraphQLBuilder builder, IEnumerable<Type> types)
        {
            builder.Services.AddSingleton<AnyScalarGraphType>();
            builder.Services.AddSingleton<ServiceGraphType>();
            builder.Services.AddSingleton(new EntityType(types));
            return builder;
        }


        private static bool IsSubclassGeneric(this Type type, Type genericType)
        {
            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (genericType == cur)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}
