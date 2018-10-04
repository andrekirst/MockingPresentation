using System;

namespace BeispielDependencyInversionMitLicht.OhneDIP
{
    public class Gluehlampenlicht
    {
        public void An()
        {
            Console.WriteLine("Gluehlampen an");
        }

        public void Aus()
        {
            Console.WriteLine("Gluehlampen aus");
        }
    }
}
