### Feedback


Introduzione:

due righe

Analisi as is e primi passi:

1. Update NET version from 3.1 to 7. ( NET Core 3.1 is out of support.)

# Steps to solve task

### Solution Folders

1. Modificata la struttura in src/test/doc/docker
2. Sistemati gli script docker ( ho altri container e contesti creati .ps1 )

### Fixing Minors

1. Servizio Movies, con variabile di Fails %
2. GRPC mancava l'API KEY


### Solution **Create showtime**

L'api fornita viene vista come un servizio esterno.
Opzini di comunicazione:
1. La ui recupera i dati e li manda via rest al servizio ( accoppiamento UI )
2. Aggregator prj sincrono (x)
3. Aggregator prj async => invece di grpc si utilizza un BUS
4. Accoppiamento con servizio e sidecar container (https://docs.dapr.io/concepts/dapr-services/sidecar/)

++ REBUS Implementation con IDistributedCache +++

### Archtecture overview

## main pakage used

-------

API:
Modelli versionati + Automapper per disaccoppiare comandi da API Models

ReadModel per semplicità torna cosi com'è ma si potrebbe aggiungere.

-------

Application:
https://medium.com/@dbottiau/a-naive-introduction-to-cqrs-in-c-9d0d99cd2d54

Queries: Usa il repo ma si potrebbe fare con altri Orm es. Dapper
Commands: Put/post/ Delete  => Inegration Events

https://learn.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/integration-event-based-microservice-communications


-------
Domain:

Una volta deciso come disacoppiare il servizio che fornisce l'elenco degli showtime, è necessario individuare e dividere i bouded context.

Divisione di BC in questo modo:
- Seat diventa un value tipe
- Auditorium è la definizione
- Showtime + Movie + Showtime seats (new): Quando si crea uno showtime, viene recuperata la def degli auditorium di riferimento e creati i seat per showtime.
- TicketEntity non deve aver riferimento ad altre entità.

Factory pattern statc create (Always Valid pattern)

A questo punto creiamo costruttori e validazioni per i modelli.

 private set; => si modificano solo tramite metodi di dominio.

in questo modo possiamo già inserire alcune delle validazioni richieste.
Disaccoppiati i seats degli auditorium ( may change?! ) da quelli degli showtime.

Repositori disacoppiati volutamente...

(La logica dei domain è tenetura seprata ma si potrebbe accorpare sotto un unica entiti avendo un base type per le entità)
Domain Event:
https://medium.com/design-microservices-architecture-with-patterns/outbox-pattern-for-microservices-architectures-1b8648dfaa27
+
Quarz for parsing and publish domain events
https://www.quartz-scheduler.net/

-------
Persistence Layer

- Rifatorizzato il layer persistence: IConfigurationBuildinder per maggior ordine.
- Implementato il salvataggio dei domain event con un interceptor


--- Tests

Jmeter per loading e automatismi
+
Esempio Unit
+
Esempio Mutator con stryker

Due righe su code coverage

-----

Bonus progressive GUID vs identity:

