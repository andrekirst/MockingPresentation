using System;

namespace DependencyInversionPrincipleBeispielLehrling.OhneDIP
{
    public class Lehrling
    {
        private readonly Schaufel meineSchaufel;

        public Lehrling()
        {
            meineSchaufel = new Schaufel();
        }

        public void GrabeLoch()
        {
            var datum = DateTime.Now;

            datum.AddDays(1);
            meineSchaufel.Buddel();
        }
    }
}