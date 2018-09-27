using System;
using Beispiel.Interfaces;

namespace Beispiel.Implementations
{
    public class BunterKonsolenProtokollierer : IProtokollierer
    {
        public void ProtokolliereInformation(string meldung)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value: meldung);
            Console.ResetColor();
        }

        public void ProtokolliereFehler(string meldung)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value: meldung);
            Console.ResetColor();
        }
    }
}
