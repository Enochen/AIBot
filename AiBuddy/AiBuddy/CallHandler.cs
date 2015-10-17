namespace AiBuddy
{
    internal class CallHandler
    {
        public static void GetChamp(string s)
        {
            switch (s)
            {
                case "Soraka":
                    Champions.Soraka.Soraka.Initialize();
                    break;
                default:
                    return;
            }
        }
    }
}