# Genealogy server

The aim of this project is to make an app to be used when doing genealogy.

## MediatR

In this app I am using CQRS and mediator pattern by using the [MediatR](https://github.com/jbogard/MediatR) package.
[I have been inspired by this Youtube series by Nick Chapsas.](https://www.youtube.com/watch?v=YzOBrVlthMk)

## Db - Neo4j

Since the relationship between the enteties are as important as the enteties themself I thought this poject could be a good time to try a graph database. The choice fell on Neo4j.

### To get a local instance, do the steps below.

[Here is Docker documentation](https://neo4j.com/developer/docker-run-neo4j/)

```
docker run \
    --name genealogy_server_dev \
    -p7474:7474 -p7687:7687 \
    -d \
    -v $HOME/neo4j/data:/data \
    -v $HOME/neo4j/logs:/logs \
    -v $HOME/neo4j/import:/var/lib/neo4j/import \
    -v $HOME/neo4j/plugins:/plugins \
    --env NEO4J_AUTH=neo4j/test \
    neo4j:latest
```

```
docker stop genealogy_server_dev

docker rm genealogy_server_dev
```

The C# client i am using is Neo4jClient. [Documentation can be found here.](https://github.com/Readify/Neo4jClient/wiki)
