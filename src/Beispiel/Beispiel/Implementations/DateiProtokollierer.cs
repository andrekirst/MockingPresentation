using System;
using System.Globalization;
using System.IO;
using Beispiel.Interfaces;

namespace Beispiel.Implementations
{
    public class DateiProtokollierer : IProtokollierer
    {
        public void ProtokolliereInformation(string meldung)
        {
            File.AppendAllText(path: "log.txt", contents: $"INFO[{DateTime.Now.ToString(CultureInfo.CurrentCulture)}]: {meldung}{Environment.NewLine}");
        }

        public void ProtokolliereFehler(string meldung)
        {
            File.AppendAllText(path: "log.txt", contents: $"ERROR[{DateTime.Now.ToString(CultureInfo.CurrentCulture)}]: {meldung}{Environment.NewLine}");
        }
    }
}
