using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace zh_gyak_25._12._02
{
    internal class FleetHandler
    {
        private List<Ship> Fleet = new List<Ship>();
        private DateTime CurrentDate;
        public FleetHandler(string fileName) {
            using StreamReader sr = new StreamReader(@$"../../../src/{fileName}", encoding: System.Text.Encoding.UTF8);
            CurrentDate = DateTime.Parse(sr.ReadLine());
            while (!sr.EndOfStream)
            {
                Fleet.Add(new Ship(sr.ReadLine()));
            }
        }

        public int TotalShipCount()
        {
            return Fleet.Count();
        }

        public bool HasAnyDeceasedCrew()
        {
            foreach (Ship ship in Fleet)
            {
                if (ship.GetCrewStatus() == CrewStatusType.Deceased)
                {
                    return true;
                }
            }
            return false;
        }

        public double AverageCargo(ShipClassType shipClass)
        {
            int totalCargo = 0;
            int shipCount = 0;
            foreach (var item in Fleet)
            {
                if(item.GetShipClass().Equals(shipClass))
                {
                    totalCargo += item.GetCargo();
                    shipCount++;
                }
            }
            return (double)totalCargo / shipCount;
        }

        public Ship[] GetShipsGroupedByRisk()
        {
            Ship[] sortedFleet = new Ship[Fleet.Count()];
            foreach (Ship ship in Fleet)
            {
                if ((ship.GetShipClass().Equals(ShipClassType.Cargo) || ship.GetShipClass().Equals(ShipClassType.Research)) && ship.NeedsRescue(CurrentDate))
                {
                    sortedFleet.Append(ship);
                }
            }
            foreach (Ship ship in Fleet)
            {
                if (!sortedFleet.Contains(ship))
                {
                    sortedFleet.Append(ship);
                }
            }
            return sortedFleet;
        }
        public void GenerateReport()
        {
            Console.WriteLine("The current date is: " + CurrentDate);
            Console.WriteLine("The total ship count is " + TotalShipCount());
            Console.WriteLine("Average cargo by ship types:");
            Console.WriteLine($"Cargo: {AverageCargo(ShipClassType.Cargo)}\nMillitary: {AverageCargo(ShipClassType.Military)}\nResearch: {AverageCargo(ShipClassType.Research)}\nMining: {AverageCargo(ShipClassType.Mining)}\nColonilal: {AverageCargo(ShipClassType.Colonial)}\nRescue: {AverageCargo(ShipClassType.Rescue)}");
            Console.WriteLine("Detailed info sorted by risk:");
            Ship[] sortedFleet = GetShipsGroupedByRisk();
            foreach (Ship ship in sortedFleet)
            {
                if(ship != null)Console.WriteLine(ship.GetStatusReport(CurrentDate));
            }
            Console.WriteLine(Fleet.First().GetStatusReport(CurrentDate));
        }
    }
}
