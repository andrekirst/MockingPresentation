namespace DependencyInversionPrincipleBeispielLehrling.MitDIPOhneSchnittstelle
{
    public class Meister
    {
        public Meister()
        {
        }

        public void GibAnweisung()
        {
            Lehrling lehrling = new Lehrling();
            lehrling.GrabeLoch(new Schaufel());
        }
    }
}