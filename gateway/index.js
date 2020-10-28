const express = require("express");
const { ApolloServer } = require("apollo-server-express");
const { ApolloGateway } = require("@apollo/gateway");
const { express: voyagerMiddleware } = require("graphql-voyager/middleware");

const gateway = new ApolloGateway({
  serviceList: [
    { name: "movie-studio", url: "http://movie-studio/gql" },
    { name: "movie-theatre", url: "http://movie-theatre/gql" },
    { name: "movie-reviews", url: "http://movie-reviews/gql" },
  ],
});

const app = express();

// Pass the ApolloGateway to the ApolloServer constructor
const server = new ApolloServer({
  gateway,

  // Disable subscriptions (not currently supported with ApolloGateway)
  subscriptions: false,
});

app.use("/voyager", voyagerMiddleware({ endpointUrl: server.graphqlPath }));

server.applyMiddleware({ app });

app.listen(process.env.port || 4000, () => {
  console.log(`🚀 Server ready on port ${process.env.port || 4000}`);
});
