# Feedback

## Git Folder Structure

The repository's folder structure consists of:

- **doc**: contains project documentation files.
- **docker**: folder used to store Docker Compose files, configurations, and some PowerShell scripts.
- **src**: holds the application's source code. The solution file is located within this folder, and for each service, a separate folder has been created.
- **test**: contains tests for each service.

## Prerequisite Steps to Solve the Challenge

1. Update the .NET version from 3.1 to 7 (since .NET Core 3.1 is out of support).
2. Adjust the Docker Compose file and separate the core app service from the accessor service.
3. Resolve the issue on the Movie service: run `docker log` to locate the environment variable that needs modification.
4. Solve the GRPC implementation; simply add the API KEY found in Swagger.

## Challenge Analysis 

**[Provided API](http://localhost:7172/swagger/index.html)** should be considered an external service. The challenge aims to explore how to handle an external service.

From my point of view, we have three options:

### 1 - Synchronous Approach

Create another service external to "Cinema" that is responsible for communicating with the "Provided API" to retrieve movies and create showtimes. Everything is handled synchronously using GRPC calls.

- Advantages: Simplicity
- Disadvantages: High coupling with target services.

*(For this demo, even if I choose this solution, I will not suggest it in a real-world scenario.)*

### 2 - Asynchronous Approach

Create another service external to "Cinema" that is responsible for communicating with the "Provided API" to retrieve movies and create showtimes. Communication with the Provided API is handled in GRPC synchronously, but communication with the target service will be made using Service Bus messages.

- Advantages: Decoupled from receivers.
- Disadvantages: More complex than the Synchronous approach.

*(Not chosen for time reasons.)*

### 3 - Sidecar Container

Ref.: [Dapr Documentation](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/howto-invoke-non-dapr-endpoints/)

In this scenario, it is not necessary to develop a new aggregator project. The call is made directly on the target service through a sidecar container, which could be achieved using projects like Dapr.

- Advantages: Once Dapr is configured, it simplifies the architecture. Many features are pre-made. Cloud agnostic.
- Disadvantages: Strong coupling with Dapr in our solution. A sidecar is needed for each container developed, potentially increasing hosting costs.

*(Not chosen for time reasons; for this demo, Dapr is considered overkill.)*

## Architecture Overview

The approach used is Domain-Driven Design (DDD).
The first step was to create the DomainModel project and remodel the entities, decoupling them as much as possible.

The following modifications were made:

- `Seat` has become a record and, therefore, a value type.
- The `Auditorium` entity has become a separate entity invoked in the creation of showtimes to obtain the definition of the halls.
- `Showtime + Movie + Showtime seats` (new): When creating a showtime, the auditorium definitions are retrieved, and seats for the showtime are created.
- `TicketEntity` should not have references to other entities.

Some patterns used in the Domain:

- Factory pattern static create (Always Valid pattern)
- Outbox pattern used in the `Ticket` entity to manage domain events.

The application's structure is as follows:

- **API**: contains models, controllers, and startup configurations.
- **Application**: contains command and query handlers.
- **Domain**: contains domain entities.
- **Persistence**: manages persistence with EF Core.

### API

Model versioned, using a package called AutoMapper to decouple API Models from Read and Write Application Models.

### Application

CQRS approach for CRUD: two separate models for read and write.
Currently, the same Data Model is used, but this could change in the future.

- Manages the publication of domain events.
- Handles any Integration Events with other services (e.g., Payment).

### Persistence

Details of the persistence layer:
- Refactored the persistence layer: IConfigurationBuilder for greater order.
- Implemented saving domain events with an interceptor.

### Test

In the test folder, there is a .jmx file that allows testing endpoints with [JMeter](https://jmeter.apache.org/).

As an example, a unit test project for some domains has been included in the solution.

*Analyzing Tests*

Reaching a satisfactory test coverage percentage does not guarantee well-written tests.

A useful tool to check the robustness of our tests is [Stryker](https://stryker-mutator.io/).

In the src solution, there is a RunStryker.ps1 file that allows running test mutators.

Here is an example of the output:

- [Output at the first run](docs\mutation-report.FirstRun.html)
- [Output after the second run and some fixes](docs\mutation-report.SecondRun.html)

## Cache

In the Shared folder, a project called ServiceCache has been added.
This service uses IDistributedCache from Microsoft Extension Library; this component is bound to reading using the "AddStackExchangeRedisCache" method provided by the same library.

## Execution Tracking

To solve this task, a DiagnosticsMiddleware has been added to the solution under the "Api.Common" project.
