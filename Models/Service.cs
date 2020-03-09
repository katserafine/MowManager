using System;

namespace MowManager.Models
{
    public class Service
    {
        // Unique ID to reference instance
        public int ID { get; set; }

        // Service Address
        public string Address { get; set; }

        // Service Pricing
        public Pricing Pricing { get; set; }

        // Service Frequency
        public string Frequency { get; set; }

        // Service Day
        public string Day { get; set; }

        // Service Crew
        public Crew Crew { get; set; }

        // Service Market
        public string Market { get; set; }

        // Area to Mow
        public string AreaToMow { get; set; }

        // Bag Mow?
        public bool BagMow { get; set; }

        // Restarts
        public int Restarts { get; set; }

        // Skips
        public int Skips { get; set; }

        // Dog Redos
        public int DogRedos { get; set; }

        // Service Start Date
        public DateTime StartDate { get; set; }

        // Send Schedule on a given week number in the year
        public int SendScheduleWeekNum { get; set; }

        // Skip a service date on a future given week number in the year
        public int FutureSkipWeekNum { get; set; }
    }
}