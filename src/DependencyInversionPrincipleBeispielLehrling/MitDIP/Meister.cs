namespace DependencyInversionPrincipleBeispielLehrling.MitDIP
{
    public class Meister
    {
        public void GibAnweisung()
        {
            Lehrling lehrling = new Lehrling();
            Schaufel schaufel = new Schaufel();
            lehrling.GrabeLoch(schaufel);

            Bagger bagger = new Bagger();
            lehrling.GrabeLoch(bagger);
        }
    }
}