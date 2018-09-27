using System;
using System.Linq;
using System.Collections.Generic;
using Beispiel.Interfaces;
using Beispiel.Models;

namespace Beispiel.Implementations
{
    public class StammKundenDatenspeicher : IKundenDatenspeicher
    {
        public List<Kunde> SucheKunden(string filter)
        {
            return new List<Kunde>
            {
                new Kunde { Id= Guid.NewGuid().ToString(), Name = "Karla Kolumna" },
                new Kunde { Id= Guid.NewGuid().ToString(), Name = "Tierpfleger Karl" },
                new Kunde { Id= Guid.NewGuid().ToString(), Name = "Benjamin Blümchen" }
            }
            .Where(predicate: p => p.Name.ToLower().Contains(value: filter.ToLower()))
            .ToList();
        }
    }
}
