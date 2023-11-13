# Feedback

## Git Folder Structure

The repository's folder structure consists of:

- **doc**: contains project documentation files.
- **docker**: folder used to store docker compose file, configuration, and some script powershell.
- **src**: holds the application's source code. Solution file is located within this folder, and for each service, a separate folder has been created.
- **test**: contains tests for each service.

## Prerequirements steps to solve the challange

1. Update NET version from 3.1 to 7. ( NET Core 3.1 is out of support.)
2. Adjust docker compose file, and separate core app service from accessor service.
3. Solve issue on Movie service: just run docker log to locate the env to modify.
4. Solve GRPC implementation, just add API KEY founded in swagger.

## Challenge Analysis 

**[Provided API](http://localhost:7172/swagger/index.html).** should be consider as an external service.
Purpose of challage is see how to handle with external service.

From my point of view we have 3 options:
1. Synchronous approach
2. Asynchronous approach
3. Use a Sidecar container


### 1 - Synchronous approach

Creating another service external to "Cinema" that is responsible for communicating with the "Provided API" to retrieve movies and create showtimes. Everything is handled synchronously using GRPC calls.

Advantages: Simplicity
Disadvantages: high coupling with target services.

( For this demo, even if i choose this soltution, I will not suggest it in a real world shenario.)

### 2 - Asynchronous approach

Creating another service external to "Cinema" that is responsible for communicating with the "Provided API" to retrieve movies and create showtimes. Comunication with Provided API is handle in GRPC synchronously but comunication with target service will be made using Service Bus messages.

Advantages: Decopled from reciver/s.
Disadvantages: More complex than Synchronous approach.

( Not choosen for time reason )


### 3 - Sidecar Container

Ref.:(https://docs.dapr.io/developing-applications/building-blocks/service-invocation/howto-invoke-non-dapr-endpoints/)

In this shenario is not necessary to develop a new aggregator project. The call has been done direcly on the target service throw sidecar container. This shenario could be reached using projects like Dapr.

Advantages: Once Dapr is configured, it simplifies the architecture. Many features pre-made. Cloud agnostic.
Disadvantages: Strong coupling with Dapr of our solution. A sidecar is needed for each container developed, this may increase hosting costs.

( Not choosen for time reason / for this demo Dapr is overkill )

## Archtecture overview

L'approccio utilizzato è tipo DDD, quindi la prima cosa da fare è stato creare il progetto DomainModel e rimodellare le entità disaccoppiandole il più possibile.

Sono state fatte le seguenti modifiche:

- Seat è diventata un record e quindi un value type
- L'entità auditorium è diventata un entità a se stante. Viene invocata nella creazione degli showtime per avere la definzione delle sale.
- Showtime + Movie + Showtime seats (new): Quando si crea uno showtime, viene recuperata la def degli auditorium di riferimento e creati i seat per showtime.
- TicketEntity non deve aver riferimento ad altre entità.

Alcuni pattern usati nel Domain:
- Factory pattern statc create (Always Valid pattern)
- outbox pattern utilizzato nell entità Ticket per gestire gli eventi di dominio.


La struttura dell applicativo si presenta quindi cosi:

- API: contiene i modelli, controller e cfg di startup.
- Application: contiene command e query handler.
- Domain: contiene le entità di dominio.
- Presistence: gestisce la persistenza con ef core.

### API
 - model versioned, using a package called automapper to decople API Models from Read and Write Application Models.

### Application

Approccio a CQRS per le CRUD: due modelli separati per read e write. Ad oggi viene utilizzato lo stesso repository. Ma in futuro questo potrebbe cambiare.

Gestisce il publish degli eventi di dominio.

Gestisce eventuali Integration Events con altri servizi ( es.: Payment ).

### Persistence

Gestice la persistenza su InMemoryDB con EF core.
- Rifatorizzato il layer persistence: IConfigurationBuildinder per maggior ordine.
- Implementato il salvataggio dei domain event con un interceptor


### Test

Nella folder test è presente un file .jmx che consente di provare gli endpoint con Jmer.

A titolo di esempio è stato inserito un progetto di unit test per alcuni domini.

Nella solition src è presente un file RunStryker.ps1 consente di eseguire dei mutator test, la sola code coverage non è a mio avviso sufficente come parametro.
