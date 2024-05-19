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

            string Q = $"select * from  { table} ";
            con.Open();
            SqlCommand cmd = new SqlCommand(Q,con);
            dt.Load(cmd.ExecuteReader());

            return dt;

        }


    }
}
