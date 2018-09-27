using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Beispiel.Interfaces;
using Beispiel.Models;
using Newtonsoft.Json;

namespace Beispiel.Implementations
{
    public class JsonKundenDatenspeicher : IKundenDatenspeicher
    {
        private readonly IFileSystem _fileSystem;

        public JsonKundenDatenspeicher(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public List<Kunde> SucheKunden(string filter)
        {
            // Hier könnte man auch wieder abstrahieren
            string json = _fileSystem.File.ReadAllText(path: "kunden.json");
            return JsonConvert.DeserializeObject<List<Kunde>>(value: json)
                .Where(predicate: p => p.Name.ToLower().Contains(value: filter.ToLower()))
                .ToList();
        }
    }
}
