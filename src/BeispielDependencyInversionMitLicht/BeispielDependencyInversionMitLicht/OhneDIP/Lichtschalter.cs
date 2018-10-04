namespace BeispielDependencyInversionMitLicht.OhneDIP
{
    public class Lichtschalter
    {
        private readonly Gluehlampenlicht _gluehlampenlicht;

        public Lichtschalter()
        {
            _gluehlampenlicht = new Gluehlampenlicht();
        }

        public void An()
        {
            _gluehlampenlicht.An();
        }

        public void Aus()
        {
            _gluehlampenlicht.Aus();
        }
    }
}
