namespace BeispielDependencyInversionMitLicht.MitDIP;

public class Lichtschalter(ILichtmodul lichtmodul)
{
    public void An()
    {
        lichtmodul.An();
    }

    public void Aus()
    {
        lichtmodul.Aus();
    }
}