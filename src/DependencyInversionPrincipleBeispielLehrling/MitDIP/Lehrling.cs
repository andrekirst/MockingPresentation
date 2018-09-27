namespace DependencyInversionPrincipleBeispielLehrling.MitDIP
{
    public class Lehrling
    {
        public void GrabeLoch(IGrabewerkzeug grabewerkzeug)
        {
            grabewerkzeug.Buddel();
        }
    }
}