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