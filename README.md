# MockingPresentation

## Anleitung für das Beispiel "SucheKunden"

1. Neues Projekt `MockingBeispiel` als .Net Core 2.1 erstellen.
1. xUnit-Testprojekt `MockingBeispiel.Tests` erstellen.
1. Referenz von Test-Projekt `MockingBeispiel.Tests` zu `MockingBeispiel` erstellen.
1. Wechsel zum Projekt `MockingBeispiel`.
1. Ordner "Interfaces" in Projekt erstellen.
1. Um mit dem System zu interagieren, brauchen wir als erstes einen Interaktor. Dies implementieren wir zunächst als Schnittstelle. Warum wir dies tun, wird später erläutert. Erstellen einer Schnittstelle für `IKundenInteraktor`.
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
1. Um die Tests lesbarer zu machen, installieren wir auch das NuGet-Package [Shouldly](https://github.com/shouldly/shouldly):
    ```powershell
    Install-Package Shouldly -ProjectName MockingBeispiel.Tests
    ```
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
1. Im nächsten Schritt müssen wir unser "System" (`KundenInteraktor`), das wir testen wollen, für die Simulierung der anderen Systeme (Datenspeicher, Protokollierer) vorbereiten. Dazu müssen wir die Schnittstellen `IKundenDatenspeicher` und `IProtokollierer` mocken und dem `KundenInteraktor` übergeben. Dies tun wir im Konstruktor der Testklasse `KundenInteraktorTests`. Die Datei muss nun folgendermaßen aussehen:
    ```csharp
    using MockingBeispiel_20181019.Interfaces;
    using MockingBeispiel_20181019.Implementations;
    using System;
    using Moq;
    using Xunit;

    namespace MockingBeispiel_20181019.Tests
    {
        public class KundenInteraktorTests
        {
            private readonly KundenInteraktor _systemUnderTest;
            private readonly Mock<IKundenDatenspeicher> _mockKundenDatenspeicher;
            private readonly Mock<IProtokollierer> _mockProtokollierer;

            public KundenInteraktorTests()
            {
                _mockKundenDatenspeicher = new Mock<IKundenDatenspeicher>();
                _mockProtokollierer = new Mock<IProtokollierer>();

                _systemUnderTest = new KundenInteraktor(
                    kundenDatenspeicher: _mockKundenDatenspeicher.Object,
                    protokollierer: _mockProtokollierer.Object);
            }

            [Fact(DisplayName = "Datenspeicher gibt 0 Datensaetze zurück. Erwarte leere Liste und protokolliere Informationsmeldung \"Keine Kunden gefunden\".")]
            public void Datenspeicher_gibt_0_Datensaetze_zurueck_Erwarte_leere_Liste_und_protokolliere_Informationsmeldung()
            {
                throw new NotImplementedException();
            }
        }
    }
    ```
1. Nun wollen wir den ersten Test mit folgenden Anforderungen implementieren, wenn die Suche 0 Datensätze zurückgibt:
    1. Gebe leere Liste zurück
    1. Protokolliere "Keine Kunden gefunden"
1. Der Test muss nun folgenermaßen aussehen:
    ```csharp
    public void Datenspeicher_gibt_0_Datensaetze_zurueck_Erwarte_leere_Liste_und_protokolliere_Informationsmeldung()
    {
        // Arrange
        _mockKundenDatenspeicher
            .Setup(expression: s => s.SucheKunden(It.IsAny<string>()))
            .Returns(value: new List<Kunde>());

        // Act
        List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

        // Assert
        actual.ShouldNotBeNull();
        actual.ShouldBeEmpty();

        _mockProtokollierer.Verify(
            expression: v => v.ProtokolliereInformation("Keine Kunden gefunden"),
            times: Times.Once);
    }
    ```
    1. Arrange
        1. Mit der Methode `Setup` kann man einen Ausdruch angeben, der ausgeführt werden soll. Die Methode `Returns` gibt dann an, welcher Wert dafür zurückgegeben werden soll. Dies ist die "Simulierung".
    1. Act
        1. Hier die eigentliche Funktion aufgerufen
    1. Assert
        1. Als erstes überprüfen wir mit `actual.ShouldNotBeNull();`, dass der Wert in `actual` nicht `null` ist.
        1. Danach testen wir mit `actual.ShouldBeEmpty();`, dass die Liste, die zurückgegeben wurde, leer ist.
        1. Zuletzt überprüfen wir, dass die Meldung "Keine Kunden gefunden" protokolliert wurde. Dazu nutzen wir die Methode `Verify` des Mocking-Objektes für `IProtokollierer`.
            1. Der Parameter `expression` gibt an, welches Ausdruck überprüft werden soll.
            1. Der Parameter `times` gibt an, wie oft der Ausdruck gelaufen sein muss. In diesem Fall ein mal.
1. Wenn wir den Test im "Test-Explorer" laufen lassen, erscheint in der Meldung des Testes folgender Test: *Message: System.NotImplementedException : The method or operation is not implemented.*. Dies kommt daher, dass die Methode `KundenInteraktor.SucheKunden(string filter)` noch die Ausnahme `NotImplementedException` geworfen wird. Dies wollen wir nun ändern, in dem wir die Implementierung anpassen. Damit der Code grün und "TDD-Konform" ist, sollte die Methode für `KundenInteraktor.SucheKunden(string filter)` folgendermaßen aussehen:
    ```csharp
    public List<Kunde> SucheKunden(string filter)
    {
        _protokollierer.ProtokolliereInformation(meldung: "Keine Kunden gefunden");
        return _kundenDatenspeicher.SucheKunden(filter: filter);
    }
    ```
1. Wenn wir den Test nun noch einmal ausführen, wird er grün.
1. Nun wollen wir den nächsten Test implementieren, wenn die suche beispielsweise 2 Datensätze zurückgibt:
    1. Liste mit 2 Datensätzen
    1. Protokolliere "2 Kunden gefunden"
1. Der Test muss nun folgendermaßen aussehen:
    ```csharp
    [Fact(DisplayName = "Datenspeicher gibt 2 Datensaetze zurück. Erwarte Liste mit 2 Datensätzen und protokolliere Informationsmeldung \"2 Kunden gefunden\".")]
    public void Datenspeicher_gibt_2_Datensaetze_zurueck_Erwarte_Liste_mit_2_Datensaetzen_und_protokolliere_Meldung()
    {
        // Arrange
        _mockKundenDatenspeicher
            .Setup(expression: s => s.SucheKunden(It.IsAny<string>()))
            .Returns(value: new List<Kunde>()
            {
                new Kunde { Id = 1, Name = "Kunde 1" },
                new Kunde { Id = 2, Name = "Kunde 2" }
            });

        // Act
        List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

        // Assert
        actual.ShouldNotBeNull();
        actual.Count.ShouldBe(expected: 2);

        _mockProtokollierer.Verify(
            expression: v => v.ProtokolliereInformation("2 Kunden gefunden"),
            times: Times.Once);
    }
    ```
1. Folgende Dinge haben wir nun zum vorherigen Test geändert
    1. Es werden aus dem KundenDatenspeicher keine Einträge mehr geliefert, sondern jetzt 2 Kunden.
    1. Die Anzahl der Einträge sollte **2** betragen: `actual.Count.ShouldBe(expected: 2);`
    1. Es soll jetzt "2 Kunden gefunden" protokolliert werden.
1. Wenn wir jetzt alle Tests ausführen, ist der erste Test, den wir geschrieben haben, weiterhin grün. Aber der neu erstellte Test rot.
1. Nun müssen wir den Test grün bekommen und die Methode `KundenInteraktor.SucheKunden(string filter)` anpassen. Sie muss nun folgenermaßen aussehen:
    ```csharp
    public List<Kunde> SucheKunden(string filter)
    {
        List<Kunde> gefundeneKunden = _kundenDatenspeicher.SucheKunden(filter: filter);
        if (gefundeneKunden.Any())
        {
            _protokollierer.ProtokolliereInformation(meldung: $"{gefundeneKunden.Count} Kunden gefunden");
        }
        else
        {
            _protokollierer.ProtokolliereInformation(meldung: "Keine Kunden gefunden");
        }
        return gefundeneKunden;
    }
    ```
1. Wenn wir alle Tests ausführen, sind nun beide Tests grün. Nun können wir in diesem Schritt einmal den Code umgestalten. Dies könnte so aussehen:
    ```csharp
    public List<Kunde> SucheKunden(string filter)
    {
        List<Kunde> gefundeneKunden = _kundenDatenspeicher.SucheKunden(filter: filter);
        _protokollierer.ProtokolliereInformation(meldung:
            gefundeneKunden.Any()
            ? $"{gefundeneKunden.Count} Kunden gefunden"
            : "Keine Kunden gefunden");
        return gefundeneKunden;
    }
    ```
1. Nochmaliges ausführen aller Tests bringt die Gewissheit, der Code funktioniert immer noch, wie die Tests es vorgeben.
1. Nun zum letzten Test, wenn ein Fehler im KundenDatenspeicher auftritt
    1. Leere Liste zurückgeben
    1. Fehlermeldung "Es ist ein Fehler aufgetreten" protokollieren
1. Dieser Test muss nun folgendermaßen aussehen:
    ```csharp
    [Fact(DisplayName = "Datenspeicher wirft einen Fehler. Erwarte leere Liste und protokolliere Fehlermeldung \"Es ist ein Fehler aufgetreten\".")]
    public void Datenspeicher_wirft_Fehler_Erwarte_leere_Liste_und_protokolliere_Fehlermeldung()
    {
        // Arrange
        _mockKundenDatenspeicher
            .Setup(expression: s => s.SucheKunden(It.IsAny<string>()))
            .Throws<Exception>();

        // Act
        List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

        // Assert
        actual.ShouldNotBeNull();
        actual.ShouldBeEmpty();

        _mockProtokollierer.Verify(
            expression: v => v.ProtokolliereFehler("Es ist ein Fehler aufgetreten"),
            times: Times.Once);
    }
    ```
1. Alle Tests ausführen führt dazu, dass der neue Test fehlschlägt. Das heißt, wir müssen wieder die Implementierung von `KundenInteraktor.SucheKunden(string filter)` anpassen. Die Implementierung würde nun folgendermaßen aussehen:
    ```csharp
    public List<Kunde> SucheKunden(string filter)
    {
        try
        {
            List<Kunde> gefundeneKunden = _kundenDatenspeicher.SucheKunden(filter: filter);
            _protokollierer.ProtokolliereInformation(meldung:
                gefundeneKunden.Any()
                ? $"{gefundeneKunden.Count} Kunden gefunden"
                : "Keine Kunden gefunden");
            return gefundeneKunden;
        }
        catch
        {
            _protokollierer.ProtokolliereFehler(meldung: "Es ist ein Fehler aufgetreten");
        }

        return new List<Kunde>();
    }
    ```
1. Ein erneutes ausführen aller Tests führt dazu, dass nun alle Tests grün sind.

## Weiterführung des Beispieles mit Implementierungen des Datenspeichers und Protokollierers

:information_source: **Vorbereitung:**

Als Vorbereitung für die Implementierungen, muss für das Beispiel das NuGet-Package [Autofac](https://github.com/autofac/Autofac). Dieses Framework dient zur Umsetzung des Inversion of Control-Prinzipes unter Einsatz von Dependency Injection.

```powershell
Install-Package Autofac -ProjectName MockingBeispiel
```

1. Im Projekt für `MockingBeispiel` erstellen wir im Ordner `Implementations` die Klasse `StammKundenDatenspeicher` und implementieren diese mit der Schnittstelle `IKundenDatenspeicher`. Die Datei könnte folgendermaßen aussehen:
    ```csharp
    using System.Collections.Generic;
    using System.Linq;
    using MockingBeispiel.Interfaces;
    using MockingBeispiel.Models;

    namespace MockingBeispiel.Implementations
    {
        public class StammKundenDatenspeicher : IKundenDatenspeicher
        {
            public List<Kunde> SucheKunden(string filter)
            {
                return new List<Kunde>
                {
                    new Kunde { Id= 1, Name = "Karla Kolumna" },
                    new Kunde { Id= 2, Name = "Tierpfleger Karl" },
                    new Kunde { Id= 3, Name = "Benjamin Blümchen" }
                }
                .Where(predicate: p => p.Name.ToLower().Contains(value: filter.ToLower()))
                .ToList();
            }
        }
    }
    ```
1. Als nächstes erstellen wir im Ordner eine Implementierung für `IProtokollierer` mit dem Klassenname `KonsolenProtokollierer`. Die Datei könnte folgendermaßen aussehen:
    ```csharp
    using System;
    using MockingBeispiel.Interfaces;

    namespace MockingBeispiel.Implementations
    {
        public class KonsolenProtokollierer : IProtokollierer
        {
            public void ProtokolliereInformation(string meldung)
            {
                Console.WriteLine(value: meldung);
            }

            public void ProtokolliereFehler(string meldung)
            {
                Console.WriteLine(value: meldung);
            }
        }
    }
    ```
1. Im nächsten Schritt wollen wir die einzelnen Module "zusammenbauen" und mal etwas sehen :smirk:. Dazu gehen wir zur `Main`-Methode in der Datei `Program.cs`. Der Code könnte folgendermaßen aussehen:
    ```csharp
    using System;
    using Autofac;
    using MockingBeispiel.Implementations;
    using MockingBeispiel.Interfaces;
    using MockingBeispiel.Models;

    namespace MockingBeispiel
    {
        public static class Program
        {
            public static void Main(string[] args)
            {
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder
                    .RegisterType<KundenInteraktor>()
                    .As<IKundenInteraktor>();
                containerBuilder
                    .RegisterType<KonsolenProtokollierer>()
                    .As<IProtokollierer>();
                containerBuilder
                    .RegisterType<StammKundenDatenspeicher>()
                    .As<IKundenDatenspeicher>();

                IKundenInteraktor kundenInteraktor = containerBuilder
                    .Build()
                    .Resolve<IKundenInteraktor>();

                foreach (Kunde kunde in kundenInteraktor.SucheKunden(filter: ""))
                {
                    Console.WriteLine(value: $"{kunde.Id} - {kunde.Name}");
                }

                Console.WriteLine(value: "Fertsch");
                Console.ReadKey();
            }
        }
    }
    ```
    Erläuterung:
    1. Die Klasse `ContainerBuilder` dient dazu, die einzelnen Schnittstellen und Implementierungen zu registrieren.
    1. Die Methode `RegisterType<TImplementer>` gibt an, welche Implementierung genutzt werden soll.
    1. Die Methode `As<TService>` gibt an, welche Schnittstelle genutzt werden soll.
    1. Die Methode `Build` "baut" die registrierten Komponenten und die Methode `Resolve<TService>` löst die Implementierung automatisch auf.
    1. Zuletzt wird die Suche ausgeführt und durch die Liste iteriert und ausgegeben. Die Konsole sollte nun folgendes ausgeben:
        ```bash
        3 Kunden gefunden
        1 - Karla Kolumna
        2 - Tierpfleger Karl
        3 - Benjamin Blümchen
        Fertsch
        ```
1. Nun sind wir in der Lage, weitere Implementierungen für `IKundenDatenspeicher` und `IProtokollierer` zu erstellen, ohne die Implementierung von `KundenInteraktor` anzupassen. Die Daten für den Datenspeicher können nun aus einer Datenbank kommen, aus einer JSON-Datei, aus einer externen API, etc. Das selbe gilt für den Protokollierer. Ausgabe in verschiedenen Farben auf der Konsole, Datei-Ausgabe, Ereignisse, Nutzung einer externen Komponente (z.B. NLog, log4net, etc.) ist nun möglich. Zur Wiederholung: Ohne Anpassung der Implementierung von `KundenInteraktor`.
