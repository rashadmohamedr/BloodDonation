namespace BloodDonation.Models.DBClasses
{
    public class Experience
    {
        public int ExperienceID {  get; set; }
        public int EventID {  get; set; }
        public int DonorID {  get; set; }
        public int Rating {  get; set; }
        public string Description {  get; set; }
        public string subject { get; set; }
        public string EmployeeToBeReported { get; set; }
    }
}
