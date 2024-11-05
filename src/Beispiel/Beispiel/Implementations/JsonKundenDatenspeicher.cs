using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.Json;
using Beispiel.Interfaces;
using Beispiel.Models;

namespace Beispiel.Implementations;

public class JsonKundenDatenspeicher(IFileSystem fileSystem) : IKundenDatenspeicher
{
    public List<Kunde> SucheKunden(string filter)
    {
        var json = fileSystem.File.ReadAllText("kunden.json");
        return JsonSerializer.Deserialize<List<Kunde>>(json)!
            .Where(p => p.Name.Contains(filter, System.StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }
}
