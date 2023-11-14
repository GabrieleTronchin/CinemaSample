docker build -t cinema-api  -f ..\src\Cinema\Cinema.API\Dockerfile ..\src
docker build -t cinema-aggregator-api  -f ..\src\Movies\Movies.Aggregator.API\Dockerfile ..\src

