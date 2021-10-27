# GraphQL Federation in Dotnet

Mostly copy-paste from https://github.com/graphql-dotnet/graphql-dotnet/pull/1669 (https://github.com/rabotaua/graphql-dotnet/blob/federation/src/GraphQL/Utilities/Federation/Proposal.cs)

# How to run

1. Run AccountsApi
    
    ```
    dotnet run --project AccountsApi
    ```
    
    Playground: http://localhost:4001/graphql/playground)

    Example query: [example.graphql](./AccountsApi/GraphQl/example.graphql)

2. Run ArticlesApi

    ```
    dotnet run --project ArticlesApi
    ```
    
    Playground: http://localhost:4002/graphql/playground)

    Example query: [example.graphql](./ArticlesApi/GraphQl/example.graphql)

3. Run Gateway
    
    ```
    cd Gateway
    npm i
    node ./index.js
    ```

    Gateway: http://localhost:4000/

    Example query: [example.graphql](./Gateway/example.graphql)
    
    Example logs:
    ```
    --------------------------------------------------
    POST http://host.docker.internal:4001/graphql
    BODY: {"query":"{accounts{id name __typename}}","variables":{}}
    RESPONSE: {"data":{"accounts":[{"id":1,"name":"Name-1","__typename":"Account"},{"id":2,"name":"Name-2","__typename":"Account"},{"id":3,"name":"Name-3","__typename":"Account"}]}}
    --------------------------------------------------
    POST http://host.docker.internal:4002/graphql
    BODY: {"query":"query($representations:[_Any!]!){_entities(representations:$representations){...on Account{articles{id title account{id __typename}}}}}","variables":{"representations":[{"__typename":"Account","id":1},{"__typename":"Account","id":2},{"__typename":"Account","id":3}]}}
    RESPONSE: {"data":{"_entities":[{"__typename":"Account","articles":[{"id":1,"title":"Title-1","account":{"id":1,"__typename":"Account"}},{"id":2,"title":"Title-2","account":{"id":1,"__typename":"Account"}}]},{"__typename":"Account","articles":[{"id":3,"title":"Title-3","account":{"id":2,"__typename":"Account"}}]},{"__typename":"Account","articles":[]}]}}
    --------------------------------------------------
    POST http://host.docker.internal:4001/graphql
    BODY: {"query":"query($representations:[_Any!]!){_entities(representations:$representations){...on Account{name}}}","variables":{"representations":[{"__typename":"Account","id":1},{"__typename":"Account","id":1},{"__typename":"Account","id":2}]}}
    RESPONSE: {"data":{"_entities":[{"__typename":"Account","name":"Name-1"},{"__typename":"Account","name":"Name-1"},{"__typename":"Account","name":"Name-2"}]}}
    --------------------------------------------------
    ```
