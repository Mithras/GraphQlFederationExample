import { ApolloServer } from "apollo-server";
import { ApolloGateway, RemoteGraphQLDataSource } from "@apollo/gateway";
import fetch from "node-fetch";

class LoggingRemoteGraphQLDataSource extends RemoteGraphQLDataSource {
  fetcher = async (input, init) => {
    const response = await fetch(input, init);
    const responseClone = response.clone();
    console.log(`${init.method} ${init.url}`);
    console.log("BODY:", init.body);
    console.log("RESPONSE:", await responseClone.text());
    console.log("--------------------------------------------------");
    return response;
  };
}

const gateway = new ApolloGateway({
  serviceList: [
    { name: "accounts", url: "http://localhost:4001/graphql" },
    { name: "articles", url: "http://localhost:4002/graphql" },
  ],
  buildService: ({ url }) => new LoggingRemoteGraphQLDataSource({ url }),
});

const server = new ApolloServer({ gateway });
server
  .listen({ port: 4000, host: "0.0.0.0" })
  .then((url) => console.log("gateway", url));
