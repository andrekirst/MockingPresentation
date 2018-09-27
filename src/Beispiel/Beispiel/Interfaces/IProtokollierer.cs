namespace Beispiel.Interfaces
{
    public interface IProtokollierer
    {
        void ProtokolliereInformation(string meldung);

        void ProtokolliereFehler(string meldung);
    }
}
