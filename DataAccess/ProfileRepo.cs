//using LMSProfile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Business;

namespace DataAccess
{
    public class ProfileRepo
    {
        private SqlConnection con;
        private SqlCommand com;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["LMSDB"].ConnectionString.ToString();
            con = new SqlConnection(constr);
            com = new SqlCommand("SP_Display_Profile", con);
            com.CommandType = CommandType.StoredProcedure;

        }

        public ProfileModel GetProfile(ProfileModel model, object s1, object s2)
        {

            connection();
            com.Parameters.AddWithValue("@status", "GET");
            com.Parameters.AddWithValue("@accountid", s1);
            com.Parameters.AddWithValue("@userid", s2);
            con.Open();
            com.ExecuteNonQuery();

            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                model.Name = sdr["name"].ToString();
                model.Email = sdr["email"].ToString();
                model.Gender = sdr["gender"].ToString();
                model.Contact = Convert.ToInt64(sdr["phno"]);
                model.Address = sdr["address"].ToString();
                model.Wallet = Convert.ToInt64(sdr["wallet"]);

            }
            con.Close();
            return model;
        }

        public ProfileModel PostProfile(ProfileModel model, object s1, object s2)
        {

            connection();
            com.Parameters.AddWithValue("@status", "UPDATE");
            com.Parameters.AddWithValue("@ph_no", model.Contact);
            com.Parameters.AddWithValue("@address", model.Address);
            com.Parameters.AddWithValue("@accountid", s1);
            com.Parameters.AddWithValue("@userid", s2);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return model;
        }

    }
}
