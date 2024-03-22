# Clinica CaniZzoo - Applicazione per la gestione di una clinica veterinaria

## Configurazione del Database

Prima di avviare l'applicazione, assicurati di configurare correttamente la connessione al database. Apri il file `web.config` (o `app.config` se stai sviluppando un'applicazione console) e trova la sezione relativa alla connection string. Assicurati di sostituire `DataSource=nome_server` con il nome corretto del server del tuo database.

```xml
<connectionStrings>
    <add name="NomeConnessione" connectionString="Data Source=nome_server;Initial Catalog=nome_database;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
Inoltre nel database alla tabella Users è necessario creare i record per gli admin che avranno ruolo "AdminV" (per i veterinari) e "AdminF" (per i farmacisti), nella sezione di registrazione utente è possibile registrarsi solo come User.

## Descrizione
Clinica CaniZzoo è un'applicazione web sviluppata con ASP.NET MVC 5.2.9 e Entity Framework 6.4.4 per la gestione di una clinica veterinaria. Si
concentra principalmente sull'interazione desktop e utilizza Bootstrap 5.2.3 come libreria per la progettazione dell'interfaccia utente.

## Funzionalità Principali

### Per gli Amministratori:
- **Gestione degli animali**: Anagrafare animali nel database, modificare o eliminare le loro schede.
- **Gestione dati visite e ricoveri**: Possibilità di tenere traccia delle visite e dei ricoveri di ogni animale.
- **Sezione ricoveri animali smarriti**: Sezione per gli animali che non hanno un proprietario.
- **Gestione prodotti farmaceutici**: Sezione della farmacia della clinica per registrare prodotti alimentari o medicinali, con dati riguardo
all'utilizzo e alla fornitura.
- **Tracciamento vendite prodotti**: Sezione per tracciare vendite ai clienti e recuperarne lo storico tramite il codice fiscale.
- **Ricerca armadietto del medicinale**: Sezione per velocizzare la ricerca dei prodotti per armadietto e cassetto.

### Per gli Utenti Registrati:
- **Ricerca animali**: Possibilità di cercare un animale utilizzando il codice del microchip.

## Requisiti Tecnici
- **ASP.NET MVC**: Versione 5.2.9
- **Entity Framework**: Versione 6.4.4
- **Bootstrap**: Versione 5.2.3
