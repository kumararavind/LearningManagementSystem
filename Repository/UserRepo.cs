using LMSProfile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LMSProfile.Repository
{
    public class UserRepo
    {
        private SqlConnection con;
        private SqlCommand com;
        private ProfileModel model;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            con = new SqlConnection(constr);
            com = new SqlCommand("SP_Admin_Instructor_User", con);
            com.CommandType = CommandType.StoredProcedure;

        }

        public List<ProfileModel> GetAllUserRepo()
        {
            connection();
            List<ProfileModel> ProList = new List<ProfileModel>();
            com.Parameters.AddWithValue("@status", "Read");
            com.Parameters.AddWithValue("@accountid", 3);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            com.ExecuteNonQuery();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                ProList.Add(
                    new ProfileModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["name"]),
                        Email = Convert.ToString(dr["email"]),
                        Contact = Convert.ToInt64(dr["phno"]),
                        Address = Convert.ToString(dr["address"])
                    }
                    );
            }

            return ProList;
        }

        public bool DeleteUser(int Id)
        {

            connection();
            com.Parameters.AddWithValue("@id", Id);
            com.Parameters.AddWithValue("@accountid", 3);
            com.Parameters.AddWithValue("@status", "Delete");

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i < 0)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
    }
}