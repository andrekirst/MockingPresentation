using Beispiel.Models;
using System.Collections.Generic;

namespace Beispiel.Interfaces;

public interface IKundenDatenspeicher
{
    List<Kunde> SucheKunden(string filter = "");
}
