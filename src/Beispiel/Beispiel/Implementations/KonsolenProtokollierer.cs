using System;
using Beispiel.Interfaces;

namespace Beispiel.Implementations
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
