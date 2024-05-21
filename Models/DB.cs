using BloodDonation.Models.DBClasses;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace BloodDonation.Models
{
    public class DB
    {
        public SqlConnection con { get; set; }
        public Models.DBClasses.User u { get; set; }
        public DB()
        {
            string conStr = "Data Source=SQL8010.site4now.net;Initial Catalog=db_aa8e0c_blooddb;User Id=db_aa8e0c_blooddb_admin;Password=123456BloodDB";
            con = new SqlConnection(conStr);
        }
        public bool AddNewEntityCustomized(Dictionary<String, string> Dict)
        {
            return true;
            //con.Open();
            string Q;
            switch (Dict["Table"])
            {
                case "Admin":
                    Q = $"INSERT INTO Admin ( AdminID , UserID ) Values ('{Dict["AdminID"]}','{Dict["User ID"]}'')";
                    break;
                case "AnEvent":
                    Q = "";
                    break;
                case "Coordinator":
                    Q = $"INSERT INTO Coordinater ( CoordinatorID , UserID ) Values ('{Dict["AdminID"]}','{Dict["User ID"]}')";
                    break;
                case "Donor":
                    Q = $"INSERT INTO Donor (BloodType , Gender , Travel , MedicationHistory , Weight , IllnessHistory , DonationInterval , EligabilityStatus , UserID, TeamID , TeamLeaderID) Values ('{Dict["BloodType"]}',' '{Dict["Gender"]}', '{Dict["Travel"]}; , '{Dict["MedHis"]}' , '{Dict["Weight"]}', '{Dict["IllHis"]}','{Dict["Donation_interval"]}','{Dict["Eligability"]}','{Dict["user_id"]}','{Dict["team_id"]}','{Dict["team_lead_id"]}') ";
                    break;
                case "Experience":
                    break;
                case "Staff":
                    Q = $"INSERT INTO Staff ( StaffID , YearsOfExperience , Role  ) Values ('{Dict["staff_id"]}','{Dict["role"]}')";
                    break;
                case "Team":
                    break;
                case "User":
                    break;
            }
            //int numVal = Int32.Parse("-105");
            //Console.WriteLine(numVal);
            return true;
        }
        public (string, string) AddUserSignUp(Dictionary<String, string> Dict)
        {
            int UserID = Int32.Parse(GetColumnCount("User"))+1;
            Dict["UserID"] = UserID.ToString() ;
            con.Open();
            string Q = $"INSERT INTO [User] ([UserId],Name,Email,Password,Phone,DateOfBirth,UserType) VALUES ('{Dict["UserID"]}','{Dict["Name"]}','{Dict["Email"]}','{Dict["Password"]}','{Dict["Phone"]}','{Dict["DateOfBirth"]}','{Dict["UserType"]}');";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.ExecuteNonQuery();
            con.Close();

            foreach (KeyValuePair<string, string> property in Dict)
            {
                Console.WriteLine($"{property.Key}: {property.Value}");
            }
            switch (Dict["UserType"])
            {
                case ("A")://Admin
                    Dict["AdminID"] = (Int32.Parse(GetColumnCount("Admin")) + 1).ToString();
                    Q = $"INSERT INTO [Admin] ([AdminID],[UserID]) VALUES ('{Dict["AdminID"]}','{Dict["UserID"]}');";
                    con.Open();
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to admin_main
                    con.Close();
                    return ("/Staff/Admin_main", Dict["UserID"].ToString());
                    break;
                case ("C")://Coordinator
                    int CoordinatorID = Int32.Parse(GetColumnCount("Coordinator")) + 1;
                    Dict["CoordinatorID"] = CoordinatorID.ToString();
                    Q = $"INSERT INTO [Coordinator] ([CoordinatorID],[UserID]) VALUES ('{Dict["CoordinatorID"]}','{Dict["UserID"]}');"; con.Open();
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to coordinator_main
                    con.Close();
                    return ("/Coordinator/Coordinator_main", Dict["UserID"].ToString());
                    break;
                case ("D")://Donor
                    int DonorID = Int32.Parse(GetColumnCount("Donor")) + 1;
                    Dict["DonorID"] = DonorID.ToString();
                    Q = $"INSERT INTO [Donor] ([DonorID],[UserID]) VALUES ('{Dict["DonorID"]}','{Dict["UserID"]}');"; con.Open();
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to donor_main
                    con.Close();
                    return ("/Donor/Donor_main", Dict["UserID"].ToString());
                    break;
                case ("S")://Staff
                    int StaffID = Int32.Parse(GetColumnCount("Staff")) + 1;
                    Dict["StaffID"] = StaffID.ToString();
                    Q = $"INSERT INTO [Staff] ([StaffID],[UserID]) VALUES ('{Dict["StaffID"]}','{Dict["UserID"]}');"; con.Open();
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to staff_main
                    con.Close();
                    return ("/Staff/Staff_main", Dict["UserID"].ToString());
                    break;
            }
            con.Close();
            return ("incorrectData", "");
        }
        public (string, string) SignIn(string Email, string Pass)
        {
            con.Open();
            string Q = "SELECT [Password],[UserID],[UserType] FROM [User] WHERE [User].[Email]=\'" + Email + "\';";
            SqlCommand cmd = new SqlCommand(Q, con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            u = new Models.DBClasses.User();
            u.UserID = (int)dt.Rows[0]["UserID"];
            u.UserType = (string)dt.Rows[0]["UserType"];
            u.Password = (string)dt.Rows[0]["Password"];
            con.Close();
            if (u.Password== Pass)
            {
                switch (u.UserType)
                {
                    case ("A")://Admin
                        return ("Pages/Staff/Admin_main", u.UserID.ToString());
                    case ("C")://Coordinator
                        return ("Pages/Staff/Coordinator_main", u.UserID.ToString());
                    case ("D")://Donor
                        return ("Pages/User/Donor_main", u.UserID.ToString());
                    case ("S")://Staff
                        return ("Pages/Staff/Staff_main", u.UserID.ToString());
                    case ("X")://SuperAdmin
                        return ("Pages/Staff/Admin_main", "S"+u.UserID.ToString());
                    
                }
            }
            return ("UncorrectPassword",""); 
        }

        /*
        public Dictionary<string, int> getFavouriteCodeEditors()
        {
            Dictionary<string, int> labelsAndCounts = new Dictionary<string, int>();

            try
            {
                con.Open();

                string query = "select code_editor, count(code_editor) as count from student_info group by code_editor";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                // store labels and counts inside the dictionary
                while (sdr.Read())
                {
                    labelsAndCounts.Add(sdr["code_editor"].ToString(), (int)sdr["count"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { con.Close(); }

            return labelsAndCounts;
        }
        https://docs.google.com/spreadsheets/d/13M4cA44iRI1xT4YgI958HUh2GtbM1QXg7X7lO9j5W2k/edit#
         */

        public DataTable ReadTable(string table)
        {
            DataTable dt = new DataTable();

            string Q = $"SELECT * FROM [db_aa8e0c_blooddb].[dbo].[" + table + "];";
            con.Open();
            SqlCommand cmd = new SqlCommand(Q, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;

        }





        public string GetColumnCount(string tableName)
        {

            con.Open();
            string Q;
            Q = "SELECT COUNT(*) FROM  [" + tableName + "] ";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.ExecuteNonQuery();

            object res = cmd.ExecuteScalar();
            string result ="";
            if (res != null)
            {
                result = res.ToString();
                Console.WriteLine(result);
              
            };
            con.Close();
            return result;
            

        }


        public void max(BloodDonation.Models.DBClasses.Donor donor_t)
        {

            con.Open();
            string Q;
            Q = "SELECT DISTINCT (BloodType) FROM BloodDonation.Models.DBClasses.Donor  ";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.ExecuteReader();

            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                string result = res.ToString();
                Console.WriteLine(result);
            };

            con.Close();

        }

        public (string, string) AddDonor(Dictionary<String, string> Dict)
        {
            int UserID = Int32.Parse(GetColumnCount("User")) + 1;
            Dict["UserID"] = UserID.ToString();
            con.Open();
            string Q = $"INSERT INTO [User] ([UserId],Name,Email,Password,Phone,DateOfBirth,UserType) VALUES ('{Dict["UserID"]}','{Dict["Name"]}','{Dict["Email"]}','{Dict["Password"]}','{Dict["Phone"]}','{Dict["DateOfBirth"]}','{Dict["UserType"]}');";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.ExecuteNonQuery();
            /*
             
            switch (Dict["UserType"])
            {
                case ("A")://Admin
                    Q = $"INSERT INTO [Admin] ([AdminID],[UserID]) VALUES ('{Dict["AdminID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to admin_main
                    con.Close();
                    return ("Pages/Staff/Admin_main", u.UserID.ToString());
                    break;
                case ("C")://Coordinator
                    Q = $"INSERT INTO [Admin] ([AdminID],[UserID]) VALUES ('{Dict["AdminID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to Coordinator
                    con.Close();
                    return ("Pages/Staff/Coordinator_main", u.UserID.ToString());
                    break;
                case ("D")://Donor
                    Q = $"INSERT INTO [Donor] ([StaffID],[UserID]) VALUES ('{Dict["StaffID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to Donor
                    con.Close();
                    return ("Pages/User/Donor_main", u.UserID.ToString());
                    break;
                case ("S")://Staff
                    Q = $"INSERT INTO [Staff] ([StaffID],[UserID]) VALUES ('{Dict["StaffID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to Staff
                    con.Close();
                    return ("Pages/Staff/Staff_main", u.UserID.ToString());
                    break;
            }
            */
            con.Close();
            return ("incorrectData", "");
        }

    }
    }

