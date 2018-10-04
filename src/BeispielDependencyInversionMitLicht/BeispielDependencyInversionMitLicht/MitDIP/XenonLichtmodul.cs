using System;

namespace BeispielDependencyInversionMitLicht.MitDIP
{
    public class XenonLichtmodul : ILichtmodul
    {
        public void An()
        {
            Console.WriteLine("Xenon-Licht ist an");
        }

        public void Aus()
        {
            Console.WriteLine("Xenon-Licht ist aus");
        }
    }
}
