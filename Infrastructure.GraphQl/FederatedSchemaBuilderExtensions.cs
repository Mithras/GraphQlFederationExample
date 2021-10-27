using System.Collections.Generic;
using GraphQL.Types;
using GraphQLParser.AST;

namespace Microsoft.EcoManager.Infrastructure.GraphQl
{
    public static class FederatedSchemaBuilderExtensions
    {
        /// <summary>
        /// Public helper which will allow us to set "__EXTENSION_AST_MetaField__" on a type which then will be used by FederatedSchemaPrinter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void BuildExtensionAstMeta(this IProvideMetadata type, string name, string value = null)
        {
            var definition = BuildGraphQLObjectTypeDefinition();
            var directive = BuildGraphQLDirective(name, value, ASTNodeKind.Argument);
            AddDirective(definition, directive);
            // type.AddExtensionAstType(definition);
            type.Metadata["__EXTENSION_AST_MetaField__"] = new List<ASTNode> { definition };
        }

        /// <summary>
        /// Public helper which will allow us to set "__AST_MetaField__" on a field which then will be used by FederatedSchemaPrinter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void BuildAstMeta(this IProvideMetadata type, string name, string value = null)
        {
            // var definition = type.GetAstType<GraphQLObjectTypeDefinition>() ?? BuildGraphQLObjectTypeDefinition();
            var definition = (GraphQLObjectTypeDefinition)type.GetMetadata<ASTNode>("__AST_MetaField__", () => BuildGraphQLObjectTypeDefinition());
            var directive = BuildGraphQLDirective(name, value);
            AddDirective(definition, directive);
            //type.SetAstType(definition);
            type.Metadata["__AST_MetaField__"] = definition;
        }


        /// <summary>
        /// Internal helper to build GraphQLDirective instance which is then used by FederationSchemaPrinter while printing federated schema sdl
        /// Directives are stored in fields metadata under key "__AST_MetaField__" and does not used anywhere else
        /// Additional properties like Location are added to avoid null ref exceptions only
        /// </summary>
        /// <param name="name">name of the directive, e.g. key, extend, provides, requires</param>
        /// <param name="value">value for fields argument, e.g. @key(fields: "id")</param>
        /// <param name="kind">for field directives - ASTNodeKind.StringValue, for type directives - ASTNodeKind.Argument</param>
        /// <returns></returns>
        private static GraphQLDirective BuildGraphQLDirective(string name, string value = null, ASTNodeKind kind = ASTNodeKind.StringValue) => new GraphQLDirective
        {
            Name = new GraphQLName
            {
                Value = name,
                Location = new GraphQLLocation()
            },
            Arguments = string.IsNullOrEmpty(value) ? new List<GraphQLArgument>() : new List<GraphQLArgument>() {
                        new GraphQLArgument
                        {
                            Name = new GraphQLName {
                                Value = "fields",
                                Location = new GraphQLLocation()
                            },
                            Value = new GraphQLScalarValue(kind) {
                                Value = value,
                                Location = new GraphQLLocation()
                            },
                            Location = new GraphQLLocation()
                        }
                    },
            Location = new GraphQLLocation()
        };

        /// <summary>
        /// The same as above
        /// </summary>
        /// <returns></returns>
        private static GraphQLObjectTypeDefinition BuildGraphQLObjectTypeDefinition() => new GraphQLObjectTypeDefinition
        {
            Directives = new List<GraphQLDirective>(),
            Location = new GraphQLLocation(),
            Fields = new List<GraphQLFieldDefinition>()
        };

        private static void AddDirective(GraphQLObjectTypeDefinition definition, GraphQLDirective directive) => ((List<GraphQLDirective>)definition.Directives).Add(directive);
    }
}
