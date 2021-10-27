using GraphQL.Builders;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    /// <summary>
    /// FieldBuillderExtensions, provide shortcuts for filling field metadata with expected objects, e.g. instead of:
    /// Field().Name("foo").FieldType.Metadata["__AST_MetaField__"] = new GraphQLObjectTypeDefinition() {...};
    /// we will have:
    /// Field().Name("foo").Requires("bar");
    /// </summary>
    public static class FederatedFieldBuilderExtensions
    {
        private static FieldBuilder<TSourceType, TReturnType> BuildAstMeta<TSourceType, TReturnType>(FieldBuilder<TSourceType, TReturnType> fieldBuilder, string name, string value = null)
        {
            fieldBuilder.FieldType.BuildAstMeta(name, value);
            return fieldBuilder;
        }

        public static FieldBuilder<TSourceType, TReturnType> Key<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder, string fields) => BuildAstMeta(fieldBuilder, "key", fields);
        public static FieldBuilder<TSourceType, TReturnType> Requires<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder, string fields) => BuildAstMeta(fieldBuilder, "requires", fields);
        public static FieldBuilder<TSourceType, TReturnType> Provides<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder, string fields) => BuildAstMeta(fieldBuilder, "provides", fields);
        public static FieldBuilder<TSourceType, TReturnType> External<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder) => BuildAstMeta(fieldBuilder, "external");
    }
}
