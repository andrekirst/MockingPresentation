using Beispiel.Models;
using System.Collections.Generic;

namespace Beispiel.Interfaces
{
    public interface IKundenInteraktor
    {
        List<Kunde> SucheKunden(string filter);
    }
}
