namespace zh_gyak_25._12._02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FleetHandler fl = new FleetHandler("weyland-yutani.csv");
            fl.GenerateReport();
        }
    }
}
