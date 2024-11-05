using System.Collections.Generic;
using Beispiel.Interfaces;
using Beispiel.Models;
using System.Linq;

namespace Beispiel.Implementations;

public class KundenInteraktor(
    IKundenDatenspeicher kundenDatenspeicher,
    IProtokollierer protokollierer)
    : IKundenInteraktor
{
    public List<Kunde> SucheKunden(string filter = "")
    {
        try
        {
            var kunden = kundenDatenspeicher.SucheKunden(filter);

            protokollierer.ProtokolliereInformation(kunden.Any()
                ? $"{kunden.Count} Kunden gefunden"
                : "Keine Kunden gefunden");

            return kunden;
        }
        catch
        {
            protokollierer.ProtokolliereFehler("Es ist ein Fehler aufgetreten");
        }

        return new List<Kunde>();
    }
}
