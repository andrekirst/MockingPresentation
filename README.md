# MockingPresentation

## Anleitung für das Beispiel "SucheKunden"

1. Neues Projekt `MockingBeispiel` als .Net Core 2.1 erstellen.
1. xUnit-Testprojekt `MockingBeispiel.Tests` erstellen.
1. Referenz von Test-Projekt `MockingBeispiel.Tests` zu `MockingBeispiel` erstellen.
1. Wechsel zum Projekt `MockingBeispiel`.
1. Ordner "Interfaces" in Projekt erstellen.
1. Um mit dem System zu interagieren, brauchen wir als erstes einen Interactor. Dies implementieren wir zunächst als Schnittstelle. Warum wir dies tun, wird später erläutert. Erstellen einer Schnittstelle für `IKundenInteraktor`.
    ```csharp
    public interface IKundenInteraktor
    {
    }
    ```
1. Neue Methode "SucheKunden" zur Schnittstelle `IKundenInteraktor` hinzufügen:
    ```csharp
    List<Kunde> SucheKunden(string filter);
    ```
    Komplett
    ```csharp
    public interface IKundenInteraktor
    {
        List<Kunde> SucheKunden(string filter);
    }
    ```
    Der Typ `Kunde` ist unbekannt und muss nun erstellt werden.
1. Neuen Ordner `Models` unterhalb des Projektes `MockingBeispiel` erstellen.
1. Neue Klasse `Kunde` unterhalb des Ordners `Models` anlegen.
    ```csharp
    public class Kunde
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    ```
1. Die Datei für `IKundenInteraktor` sollte nun folgendermaßen aussehen:
    ```csharp
    using System.Collections.Generic;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Interfaces
    {
        public interface IKundenInteraktor
        {
            List<Kunde> SucheKunden(string filter);
        }
    }
    ```
1. Einen ähnlichen Konstrukt brauchen wir auch für den Zugriff auf den Datenspeicher für die Kunden.
1. Neue Schnittstelle `IKundenDatenspeicher` unterhalb des Ordners `Interfaces` erstellen:
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
1. Als nächstes brauchen wir noch eine Schnittstelle für das Protokollieren einer Informationsmeldung und eines Fehlers. Hierzu eine neue Schnittstelle namens `IProtokollierer` unterhalb des Ordners `Interfaces` erstellen.
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
1. Erstellen der Klasse `KundenInteraktor` im Ordner `Implementations`
1. Da die Klasse `KundenInteraktor` die Schnittstelle `IKundenInteraktor` implementieren soll, muss dies als solches deklariert werden.
    ```csharp
    using System;
    using System.Collections.Generic;
    using MockingBeispiel.Interfaces;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Implementations
    {
        public class KundenInteraktor : IKundenInteraktor
        {
            public List<Kunde> SucheKunden(string filter)
            {
                throw new NotImplementedException();
            }
        }
    }
    ```
1. Derzeit wirft die Methode `SucheKunden` noch die Ausnahme `NotImplementedException`. Dies ist vollkommen okay, da wir derzeit noch keine Logik haben und diese später anhand der Tests ersetzen.
1. Da wir den Interaktor verwenden wollen, um Daten aus einem Datenspeicher (`IKundenDatenspeicher`) und Meldungen protokollieren wollen (`IProtokollierer`), müssen wir diese referenzieren. Dies geschieht über eine Konstruktor-Injektion. In diesem Fall müssen wir beide Schnittstelle dem Konstruktor der Klasse `KundenInteraktor` übergeben und speichern.
    ```csharp
    private readonly IKundenDatenspeicher _kundenDatenspeicher;
    private readonly IProtokollierer _protokollierer;

    public KundenInteraktor(
        IKundenDatenspeicher kundenDatenspeicher,
        IProtokollierer protokollierer)
    {
        _kundenDatenspeicher = kundenDatenspeicher;
        _protokollierer = protokollierer;
    }
    ```
    Komplette Datei für `KundenInteraktor`
    ```csharp
    using System;
    using System.Collections.Generic;
    using MockingBeispiel.Interfaces;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Implementations
    {
        public class KundenInteraktor : IKundenInteraktor
        {
            private readonly IKundenDatenspeicher _kundenDatenspeicher;
            private readonly IProtokollierer _protokollierer;

            public KundenInteraktor(
                IKundenDatenspeicher kundenDatenspeicher,
                IProtokollierer protokollierer)
            {
                _kundenDatenspeicher = kundenDatenspeicher;
                _protokollierer = protokollierer;
            }

            public List<Kunde> SucheKunden(string filter)
            {
                throw new NotImplementedException();
            }
        }
    }
    ```
1. Nun wollen wir den ersten Test schreiben, in dem es darum geht, wenn der Datenspeicher 0 Datensätze zurückgibt, dass eine leere Liste von Kunden zurückgegeben wird und die Informationsmeldung "Keine Kunden gefunden" protokolliert wird.
1. Um das Verhalten des Datenspeichers und des Protokollierers zu simulieren, benötigen wir zunächst noch ein Mocking-Framework. Hierzu verwnde ich das [Moq](https://github.com/Moq/moq4).
1. Moq muss nun als NuGet-Paket in das Test-Projekt `MockingBeispiel.Tests` eingebunden werden.
1. Dazu in Visual Studio unter "Tools" > "Nuget Package Manager", den Eintrag "Package Manager Console" öffnen und folgenden Befehl ausführen:
    ```powershell
    Install-Package Moq -ProjectName MockingBeispiel.Tests
    ```
1. Warten, bis alls abhängigen Pakete installiert sind
1. Im Test-Projekt `MockingBeispiel.Tests` nun folgende Test-Klasse erstellen: `KundenInteraktorTests`. Diese muss nun folgendermaßen aussehen:
    ```csharp
    using System;
    using Xunit;

    namespace MockingBeispiel.Tests
    {
        public class KundenInteraktorTests
        {
            [Fact(DisplayName = "Datenspeicher gibt 0 Datensaetze zurück. Erwarte leere Liste und protokolliere Meldung \"Keine Kunden gefunden\".")]
            public void Datenspeicher_gibt_0_Datensaetze_zurueck_Erwarte_leere_Liste_und_protokolliere_Meldung()
            {
                throw new NotImplementedException();
            }
        }
    }
    ```
