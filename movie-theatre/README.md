# Movie Theatre App

## About

A GraphQL API for movie session info. Supports Apollo Federation.

## Requirements

- .NET Core 3.1 SDK - [download](https://dotnet.microsoft.com/download)
- Add graphql-dotnet MyGet feed to `Nuget.config` - [source](https://www.myget.org/F/graphql-dotnet/api/v3/index.json)

## Getting started

- Clone the repo:

```sh
git clone https://github.com/anton-iades/graphql-samples.git
```

- Navigate to the start-up project:

```sh
cd movie-theatre/src/MovieTheatre.Api
```

- Run the program:

```sh
dotnet run # listens on http://localhost:5000
```

## Visualise the schema: `/ui/voyager`

Visualise the API as an interactive graph.

## Use the GraphQL IDE: `/ui/playground`

Self-hosted GraphQL IDE to explore the schema, write and execute queries, and more.

```gql
# sample query
{
  nextSession {
    theatre
    time
    movie {
      id
    }
  }
}
```
