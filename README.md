# Cinema Sample Project (DDD)

In this project, I will present a microservice-based solution for managing a Cinema. 
We assume the existence of an external service from which we retrieve movie information. 

To bridge the gap between our solution and the external service, we've developed a Movie Aggregator project, serving as an anti-corruption layer. 
This layer also incorporates a cache to mitigate potential failures in the external service.

Subsequently, we have the Cinema project, housing our business logic.
I've designed the architecture with a focus on adhering to the best practices of Domain-Driven Design (DDD) and cleanliness.

## Git Folder Structure

The repository's folder structure consists of:

- **doc**: contains project documentation files.
- **docker**: folder used to store Docker Compose files, configurations, and some PowerShell scripts.
- **src**: holds the application's source code. The solution file is located within this folder, and for each service, a separate folder has been created.
- **test**: contains tests for each service.

## Movie Aggregator Project

This aggregator is responsible for creating a showtime on the cinema API. 
Ideally, this service should be situated behind a gateway. As mentioned earlier, it acts as an anti-corruption layer. 

The objective is to encapsulate an external call in order to:
- Standardize input and output.
- Maintain a secure connection with the external source.
- Mitigate failures by utilizing a cache.

## Cinema Project

The approach used is Domain-Driven Design (DDD).

The application's structure is as follows:

- **API**: contains models, controllers, and startup configurations.
- **Application**: contains command and query handlers.
- **Domain**: contains domain entities.
- **Persistence**: manages persistence with EF Core.

Additional specifications about the architecture:
- I implement the CQRS pattern and utilize MediatR to manage in-service messaging.
- Class conversion between application layers is facilitated by AutoMapper.
- Domain events are managed using the Transaction Outbox pattern with EF Core and interceptor.
- An example of Integration event is provided using a service bus and MassTransit.
- Data persistence is handled by EF Core with an InMemory Configuration for testing purposes.


## Test

In the test folder, there is a .jmx file that allows testing endpoints with [JMeter](https://jmeter.apache.org/).
As an example, a unit test project for some domains has been included in the solution.

*Analyzing Tests*

Reaching a satisfactory test coverage percentage does not guarantee well-written tests.

A useful tool to check the robustness of our tests is [Stryker](https://stryker-mutator.io/).

In the src solution, there is a RunStryker.ps1 file that allows running test mutators.

Here is an example of the output:

- [Output at the first run](docs/mutation-report.FirstRun.html)
- [Output after the second run and some fixes](docs/mutation-report.SecondRun.html)

