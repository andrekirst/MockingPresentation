# MockingPresentation

## Anleitung für das Beispiel "SucheKunden"

1. Neues Projekt `MockingBeispiel` als .Net Core 2.1 erstellen
1. xUnit-Testprojekt `MockingBeispiel.Tests` erstellen
1. Referenz von Test-Projekt `MockingBeispiel.Tests` zu `MockingBeispiel` erstellen
1. Wechsel zum Projekt `MockingBeispiel`
1. Ordner "Interfaces" in Projekt erstellen
1. Um mit dem System zu interagieren, brauchen wir als erstes einen Interactor. Dies implementieren wir zunächst als Schnittstelle. Warum wir dies tun, wird später erläutert. Erstellen einer Schnittstelle für `IKundenInteractor`
    ```csharp
    public interface IKundenInteractor
    {
    }
    ```
1. Neue Methode "SucheKunden" zur Schnittstelle `IKundenInteractor` hinzufügen
    ```csharp
    List<Kunde> SucheKunden(string filter);
    ```
    Komplett
    ```csharp
    public interface IKundenInteractor
    {
        List<Kunde> SucheKunden(string filter);
    }
    ```
    Der Typ `Kunde` ist unbekannt und muss nun erstellt werden
1. Neuen Ordner `Models` unterhalb des Projektes `MockingBeispiel` erstellen
1. Neue Klasse `Kunde` unterhalb des Ordners `Models` anlegen
    ```csharp
    public class Kunde
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    ```
1. Die Datei für `IKundenInteractor` sollte nun folgendermaßen aussehen:
    ```csharp
    using System.Collections.Generic;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Interfaces
    {
        public interface IKundenInteractor
        {
            List<Kunde> SucheKunden(string filter);
        }
    }
    ```
1. Einen ähnlichen Konstrukt brauchen wir auch für den Zugriff auf den Datenspeicher für die Kunden
1. Neue Schnittstelle `IKundenDatenspeicher` unterhalb des Ordners `Interfaces` erstellen
    ```csharp
    using System.Collections.Generic;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Interfaces
    {
        public interface IKundenDatenspeicher
        {
            List<Kunde> SucheKunden(string filter);
        }
    }
    ```
1. Als nächstes brauchen wir noch eine Schnittstelle für das Protokollieren einer Informationsmeldung und eines Fehlers. Hierzu eine neue Schnittstelle namens `IProtokollierer` unterhalb des Ordners `Interfaces` erstellen
    ```csharp
    namespace MockingBeispiel.Interfaces
    {
        public interface IProtokollierer
        {
            void ProtokolliereInformation(string meldung);

            void ProtokolliereFehler(string meldung);
        }
    }
    ```
1. Da wir die "Interaktion" testen wollen, implementieren wir zunächst eine leere Hülle
1. Erstellen des Ordners `Implementations` unterhalb des Projektes `MockingBeispiel`
1. Erstellen der Klasse `KundenInteractor` im Ordner `Implementations`
1. Da die Klasse `KundenInteractor` die Schnittstelle `IKundenInteractor` implementieren soll, muss dies als solches deklariert werden.
    ```csharp
    using System.Collections.Generic;
    using MockingBeispiel.Interfaces;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Implementations
    {
        public class KundenInteractor : IKundenInteractor
        {
            public List<Kunde> SucheKunden(string filter)
            {
                throw new System.NotImplementedException();
            }
        }
    }
    ```
