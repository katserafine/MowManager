namespace MowManager.Models
{
    public class Crew
    {
        // Unique ID to reference instance
        public int ID { get; set; }

        // Number of crew members
        public int NumCrewMembers { get; set; }

        // Contact number for crew lead
        public string LeadContactCell { get; set; }
    }
}