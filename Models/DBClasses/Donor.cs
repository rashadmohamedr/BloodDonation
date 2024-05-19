namespace BloodDonation.Models.DBClasses
{
    public class Donor
    {
        public int DonorID { get; set; }
        public string BloodType { get; set; }
        public string Gender { get; set; }
        public string Travel { get; set; }
        public string MedicationHistory { get; set; }
        public string IllnessHistory { get; set; }
        public string DonationInterval { get; set; }
        public string EligibilityStatus { get; set; }
        public int Weight { get; set; }
        public int UserID { get; set; }
        public int TeamID { get; set; }
        public int TeamLeaderID { get; set; }
    }
}
