using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zh_gyak_25._12._02
{
    enum ShipClassType {
        Cargo,
        Military,
        Research,
        Mining,
        Colonial,
        Rescue
    }

    enum CrewStatusType
    {
        Active = 1,
        InCryosleep = 2,
        MissingInAction = 3,
        Deceased = 4
    }
    internal class Ship
    {
        private string Name;
        private ShipClassType Class;
        private int CrewCapacity;
        private CrewStatusType CrewStatus;
        private int Cargo;
        private DateTime LastMessage;

        public Ship(string sor)
        {
            List<string> adatok = sor.Split(';').ToList();
            Name = adatok[0];
            Class = (ShipClassType)Enum.Parse(typeof(ShipClassType), adatok[1]);
            CrewCapacity = int.Parse(adatok[2]);
            CrewStatus = (CrewStatusType)Enum.Parse(typeof(CrewStatusType), adatok[3]);
            Cargo = int.Parse(adatok[4]);
            LastMessage = DateTime.Parse(adatok[5]);
        }

        public string GetName()
        {
            return Name;
        }

        public ShipClassType GetShipClass()
        {
            return Class;
        }

        public CrewStatusType GetCrewStatus()
        {
            return CrewStatus;
        }

        public DateTime GetLastMessage()
        {
            return LastMessage;
        }

        public int GetCargo()
        {
            return Cargo;
        }

        public int DaysSinceLastMessage(DateTime currentDate)
        {
            return (currentDate - LastMessage).Days;
        }

        public bool NeedsRescue(DateTime currentDate)
        {
            if (DaysSinceLastMessage(currentDate) > 30 && CrewStatus.Equals(1) || DaysSinceLastMessage(currentDate) > 3650 && CrewStatus.Equals(2) || CrewStatus.Equals(3) || CrewStatus.Equals(4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"{Name} ({Class})";
        }
        public string GetStatusReport(DateTime currentDate)
        {
            return $"=== {ToString()}) ===\n  Crew: {CrewCapacity} ({CrewStatus})\n  Last message: {DaysSinceLastMessage(currentDate)} days\n  Rescue needed: {(NeedsRescue(currentDate) ? "Yes" : "No")}";
        }

        
    }
}
