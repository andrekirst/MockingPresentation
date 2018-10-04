using System;

namespace BeispielDependencyInversionMitLicht.MitDIP
{
    public class H4Lichtmodul : ILichtmodul
    {
        public void An()
        {
            Console.WriteLine("H4-Licht ist an");
        }

        public void Aus()
        {
            Console.WriteLine("H4-Licht ist aus");
        }
    }
}
