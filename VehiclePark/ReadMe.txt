# Vehicle Park

Vehicle Park is mijn oplevering voor de opdracht van SUREbusiness

## Installation

1. Maak een database aan in Microsoft SQL Server Management Studio genaamt: vehiclepark.
2. Start Visual Studio op.
3. Wijzig de DefaultConnection string naar je eigen inloggevens.
4. Voer het volgende uit in de package manager console:
```bash
add-migration init
```
5. Als dit is gelukt voer dan de volgende uit:
```bash
update-database
```
6. Bekijk of het model 'vehicle' in de database staat.

## Usage

### De API endpoints gebruiken

Na het opstarten van de applicatie kom je terecht bij het Swagger portaal.

Hier staan 4 opties:

1. Alle vehicles ophalen uit de database
2. De dummy vehicles toevoegen aan de database
3. Een vehicle updaten
4. Een kentenen valideren

### Het filteren van de auto's

Het is modelijk om de auto's the filteren, dit heb ik via oData kunnen realiseren.
Dit kan alleen niet via het SwaggerHub gedaan worden.

Daarom het volgende:

1. Om de auto's op te halen voer het volgende uit via de url balk: https://localhost:7073/vehicles
2. Om de auto's te filteren op de huidige bezitter vul je de url aan met $filter=loanedTo eq 'Naam'
3. Dus bijvoorbeeld met een 1 van de dummies: https://localhost:7073/vehicle?$filter=loanedTo eq 'Robin'

#### Nog goed om te weten

De eerste auto uit de dummy auto's bevat een valide kenteken om te gebruiken met de kenteken checker

## Tot slot

Ik heb voor deze opdracht veel tijd genomen.
Ik vond de validate van het wijzigen van een auto eerst best lastig, later kwam ik er achter dat een case switch velen malen makkelijker te gebruiken was hiervoor.
Ik heb mijn best gedaan met de kennis die ik bezit, en ik hoop dat alles duidelijk is zo.
Als er nog vragen voordat het 2e gesprek begint hoor ik die natuurlijk graag.

Robin Ruis