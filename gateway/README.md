# API Gateway

## About

Composes the standalone GraphQL APIs into a unified graph and acts as a unified query interface. Implemented with Apollo Gateway.

## Requirements

- Node.js - [download](https://nodejs.org/en/)

## Getting started

- Clone the repo:

```sh
git clone https://github.com/anton-iades/graphql-samples.git
```

- Navigate to the start-up project:

```sh
cd gateway
```

- Install the dependencies:

```sh
npm i
```

- Run the program

```sh
npm start
```

## Use the GraphQL IDE: `/graphql`

Self-hosted GraphQL IDE to explore the schema, write and execute queries, and more.

```gql
# sample query
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
        birthDate
      }
      writtenBy {
        name
      }
      directedBy {
        name
      }
    }
  }
}
```
