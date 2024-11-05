namespace BeispielDependencyInversionMitLicht.OhneDIP;

public class Lichtschalter
{
    private readonly Gluehlampenlicht _gluehlampenlicht = new();

    public void An()
    {
        Gluehlampenlicht.An();
    }

    public void Aus()
    {
        _gluehlampenlicht.Aus();
    }
}
