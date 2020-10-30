# graphql-samples

Using Apollo Federation, we are composing a single unified data graph whose implementation has been federated across three independent GraphQL APIs - each one responsible for implementing only the part of the graph that it is responsible for.

The services have been split up by concern - not by type - which is made possible by schema type extensions and schema type referencing. For example, the core `Movie` type is defined in the Movie Studio API, but it is being extended by the Movie Reviews API (adds the`reviews` field) and the `Movie` type is being referenced in the `Session` type defined in the Movie Theatre API.

The GraphQL APIs have been implemented in .NET, however they are compatible with GraphQL APIs that are implemented in any language that also supports the Apollo Federation spec e.g. Apollo Server (NodeJS).

You can also opt to keep the default controller middleware in .NET which will allow your API to support both REST and GraphQL at the same time. E.g. GraphQL at `/gql` and REST at `/api/[controller]/[action]`.

## Implementing services

The following is a list of the implementing services and a link to their partial schema:

- Movie Studio API - [schema.gql](movie-studio/src/schema.gql)
- Movie Theatre API - [schema.gql](movie-theatre/src/schema.gql)
- Movie Reviews API - [schema.gql](movie-reviews/src/schema.gql)

## The gateway

The gateway is a NodeJS application that uses Apollo Gateway with Express.js integration to compose the distinct schemas into a single federated graph and to execute queries across the implementing services. The implementing services are registered in the Apollo Gateway constructor in [index.js](gateway/index.js)

## Run

See `docker-compose.yaml` - more details coming soon

## More coming soon

- This example is still under development
