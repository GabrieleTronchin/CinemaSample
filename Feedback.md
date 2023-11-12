### Feedback

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


### Solution - **Reserve seats**

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

-------

Rifatorizzato il layer persistence: IConfigurationBuildinder per maggior disaccoppiamento.

Disaccoppiati i seats degli auditorium ( may change?! ) da quelli degli showtime