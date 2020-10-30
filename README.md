# graphql-samples

## Apollo Federation with GraphQL.NET

Using Apollo Federation, we are composing a single unified data graph whose implementation has been federated across three independent GraphQL APIs - each one responsible for implementing only the part of the graph that it is responsible for.

The services have been split up by concern - not by type - which is made possible by schema type extensions and schema type referencing. For example, the core `Movie` type is defined in the Movie Studio API, but it is being extended by the Movie Reviews API (adds the`reviews` field) and the `Movie` type is being referenced in the `Session` type defined in the Movie Theatre API.

The gateway is a NodeJS application that uses Apollo Gateway with Express.js integration to compose the distinct schemas into a single federated graph and to execute queries across the implementing services.

The GraphQL APIs have been implemented in .NET, however they are compatible with GraphQL APIs that are implemented in any language that also supports the Apollo Federation spec e.g. Apollo Server (NodeJS).

You can also opt to keep the default controller middleware in .NET which will allow your API to support both REST and GraphQL at the same time. E.g. GraphQL at `/gql` and REST at `/api/[controller]/[action]`.

## Implementing services

The following is a list of the implementing services and a link to their partial schema:

- Movie Studio API - [schema.gql](movie-studio/src/MovieStudio.Api/schema.gql)
- Movie Theatre API - [schema.gql](movie-theatre/src/MovieTheatre.Api/schema.gql)
- Movie Reviews API - [schema.gql](movie-reviews/src/ReviewService.Api/schema.gql)

The implementing services are registered in the Apollo Gateway constructor in [index.js](gateway/index.js)

## Dependencies

- Docker Engine
- Docker Compose

## Run

Use docker compose to start up the implementing services and the gatway

```pwsh
docker-compose up --build -d
```

The composed schema can be visualised by navigating to http://localhost:4000/voyager

The GraphQL Playground can be accessed by navigating to http://localhost:4000/graphql

Sample query that spans all 3 implementing services (click on Query Plan to see execution plan)

```gql
{
  nextSession {
    theatre
    time
    movie {
      title
      genres
      released
      cast {
        name
      }
      reviews {
        stars
        message
        containsSpoilers
      }
    }
  }
}
```

Stop the services

```pwsh
docker-compose stop
```
