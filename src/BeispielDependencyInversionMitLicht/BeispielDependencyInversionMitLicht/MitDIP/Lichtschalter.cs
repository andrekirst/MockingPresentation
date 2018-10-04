namespace BeispielDependencyInversionMitLicht.MitDIP
{
    public class Lichtschalter
    {
        private readonly ILichtmodul _lichtmodul;

        public Lichtschalter(ILichtmodul lichtmodul)
        {
            _lichtmodul = lichtmodul;
        }

        public void An()
        {
            _lichtmodul.An();
        }

        public void Aus()
        {
            _lichtmodul.Aus();
        }
    }
}
