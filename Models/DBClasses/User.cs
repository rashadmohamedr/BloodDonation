namespace BloodDonation.Models.DBClasses
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }   
        public string Password { get; set; }
        public string Phone { get; set;}
        public string DateOfBirth { get; set; }
    }
}
