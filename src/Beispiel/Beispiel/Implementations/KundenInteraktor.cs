using System.Collections.Generic;
using Beispiel.Interfaces;
using Beispiel.Models;
using System.Linq;

namespace Beispiel.Implementations
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
            try
            {
                List<Kunde> kunden = _kundenDatenspeicher.SucheKunden(filter: filter);

                _protokollierer.ProtokolliereInformation(meldung: kunden.Any()
                    ? $"{kunden.Count} Kunden gefunden"
                    : "Keine Kunden gefunden");

                return kunden;
            }
            catch
            {
                _protokollierer.ProtokolliereFehler(meldung: "Es ist ein Fehler aufgetreten");
            }

            return new List<Kunde>();
        }
    }
}
