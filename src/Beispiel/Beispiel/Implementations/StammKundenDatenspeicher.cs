using System.Linq;
using System.Collections.Generic;
using Beispiel.Interfaces;
using Beispiel.Models;

namespace Beispiel.Implementations;

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
            .Where(p => p.Name.Contains(filter, System.StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }
}
