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
            string Q = $"INSERT INTO User (Name,Email,Password,Phone,DateOfBirth) VALUES ('{Dict["Name"]}','{Dict["Email"]}','{Dict["Password"]}','{Dict["Phone"]}','{Dict["DateOfBirth"]}');";
            Q = $"INSERT INTO User (Name,Email,Password,Phone,DateOfBirth) VALUES ('name1','example@gmail.com','pass123','202201683','12-12-2022);";
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.ExecuteNonQuery();
            Q = "SELECT UserID FROM User WHERE Email= '" + Dict["Email"] + "'; ";
            cmd = new SqlCommand(Q, con);
            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                string result = res.ToString();
                Console.WriteLine(result);
            };
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
        }*/

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



        }
    }

