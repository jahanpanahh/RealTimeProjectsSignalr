namespace SignalRSample
{
    public static class SD
    {
        static SD()
        {
            DeathyHallowRace = new Dictionary<string, int>();
            DeathyHallowRace.Add(Cloak, 0);
            DeathyHallowRace.Add(Stone, 0);
            DeathyHallowRace.Add(Wand, 0);
        }
        public static string Wand = "wand";

        public static string Stone = "stone";

        public static string Cloak = "cloak";

        public static Dictionary<string, int> DeathyHallowRace; 
    }
}
