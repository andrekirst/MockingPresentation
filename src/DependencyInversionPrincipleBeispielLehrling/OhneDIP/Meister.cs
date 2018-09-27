namespace DependencyInversionPrincipleBeispielLehrling.OhneDIP
{
    public class Meister
    {
        public void GibAnweisung()
        {
            Lehrling lehrling = new Lehrling();
            lehrling.GrabeLoch();
        }
    }
}