using System.Data;
using System.Data.SqlClient;
using System.Numerics;

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
        public bool AddNewEntityCustomized(Dictionary<String,string> Dict)
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
                    Q = $"";
                    break;
                case "Coordinator":
                    break;
                case "Donor":
                    break;
                case "Experience":
                    break;
                case "Staff":
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
        public void AddUserSignUp(Dictionary<String, string> Dict) {
            con.Open();
            string Q = $"INSERT INTO Users (Name,Email,Password,Phone,DateOfBirth) VALUES '{Dict["Name"]}','{Dict["Email"]}','{Dict["Password"]}','{Dict["Phone"]}','{Dict["DateOfBirth"]}');";
            SqlCommand cmd = new SqlCommand(Q,con);
            cmd.ExecuteNonQuery();
            Q = "SELECT UserID FROM User WHERE Email= '"+ Dict["Email"]+ "'; ";
            cmd=new SqlCommand(Q,con);
            DataTable dt = new DataTable();
         //   dt.Load( cmd.ExecuteScalar());
        }
        public void AddAdminSignUp() { }
        public void AddCoordinatorSignUp() { }
        public void AddDonorSignUp() { }
        public void AddStaffSignUp() { }
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

            string Q = $"SELECT * FROM [db_aa8e0c_blooddb].[dbo].["+ table+"]; ";
            Q= "SELECT * FROM [db_aa8e0c_blooddb].[dbo].[User];";
            con.Open();
            SqlCommand cmd = new SqlCommand(Q,con);
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;

        }


    }
}
