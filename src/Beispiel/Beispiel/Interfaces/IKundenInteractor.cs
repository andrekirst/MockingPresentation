using Beispiel.Models;
using System.Collections.Generic;

namespace Beispiel.Interfaces
{
    public interface IKundenInteractor
    {
        List<Kunde> SucheKunden(string filter);
    }
}
