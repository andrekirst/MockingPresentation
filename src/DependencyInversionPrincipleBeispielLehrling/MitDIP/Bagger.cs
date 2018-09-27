using System;

namespace DependencyInversionPrincipleBeispielLehrling.MitDIP
{
    public class Bagger : IGrabewerkzeug
    {
        public void Buddel()
        {
            Console.WriteLine("Graben mit einem Bagger");
        }
    }
}