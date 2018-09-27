using System;

namespace DependencyInversionPrincipleBeispielLehrling.MitDIP
{
    public class Schaufel : IGrabewerkzeug
    {
        public void Buddel()
        {
            Console.WriteLine("Graben mit einer Schaufel");
        }
    }
}