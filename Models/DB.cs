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
        public DB()
        {
            string conStr = "Data Source=SQL8010.site4now.net;Initial Catalog=db_aa8e0c_blooddb;User Id=db_aa8e0c_blooddb_admin;Password=123456BloodDB";
            con = new SqlConnection(conStr);
        }
        public bool AddNewEntityCustomized(Dictionary<String, string> Dict)
        {
            return true;
            con.Open();
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
        public void AddUserSignUp(Dictionary<String, string> Dict)
        {
            con.Open();
            Dict["UserID"] = "2";
            string Q = $"INSERT INTO [User] ([UserId],Name,Email,Password,Phone,DateOfBirth,UserType) VALUES ('{Dict["UserID"]}','{Dict["Name"]}','{Dict["Email"]}','{Dict["Password"]}','{Dict["Phone"]}','{Dict["DateOfBirth"]}','{Dict["UserType"]}');";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.ExecuteNonQuery();
            switch (Dict["UserType"])
            {
                case ("A")://Admin
                    Q = $"INSERT INTO [Admin] ([AdminID],[UserID]) VALUES ('{Dict["AdminID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to admin_main
                    break;
                case ("C")://Coordinator
                    Q = $"INSERT INTO [Admin] ([AdminID],[UserID]) VALUES ('{Dict["AdminID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to Coordinator
                    break;
                case ("D")://Donor
                    Q = $"INSERT INTO [Donor] ([StaffID],[UserID]) VALUES ('{Dict["StaffID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to Donor
                    break;
                case ("S")://Staff
                    Q = $"INSERT INTO [Staff] ([StaffID],[UserID]) VALUES ('{Dict["StaffID"]}','{Dict["UserID"]}');";
                    cmd = new SqlCommand(Q, con);
                    cmd.ExecuteNonQuery();
                    //redirect to Staff
                    break;
            }
            con.Close();
        }
        public void SignIn(string Email, string Pass)
        {
            con.Open();
            string Q = "SELECT [Password],[UserID],[UserType] FROM [User] WHERE [User].[Email]=\"" + Email + "\";";
            SqlCommand cmd = new SqlCommand(Q, con);
            String UserType = "";
            switch (UserType)
            {
                case ("A")://Admin
                    break;
                case ("C")://Coordinator
                    break;
                case ("D")://Donor
                    cmd.ExecuteNonQuery();
                    break;
                case ("S")://Staff
                    break;
                case ("X")://SuperAdmin
                    break;
            }
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

        



     public void GetColumnCount( string tableName)
            {
                
                    con.Open();
            string Q;
                Q = "SELECT COUNT(*) FROM  [" + tableName + "] ";
                SqlCommand cmd = new SqlCommand(Q, con);
                cmd.ExecuteNonQuery();

            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                string result = res.ToString();
                Console.WriteLine(result);
            };

            con.Close();

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


    }
    }

