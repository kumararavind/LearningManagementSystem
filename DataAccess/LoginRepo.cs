//using LMSProfile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;

namespace DataAccess
{
    public class LoginRepo
    {
        private SqlConnection con;
        private LoginLogout ll;
        private ProfileModel model;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["LMSDB"].ConnectionString.ToString();
            con = new SqlConnection(constr);

        }

        public LoginLogout dropdownrepo()
        {
            ll = new LoginLogout();
            connection();
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("SELECT account_id,account_type FROM Account_Details", con))
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(ds);
                List<LoginLogout> account = new List<LoginLogout>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LoginLogout uobj = new LoginLogout();
                    uobj.AccountID = Convert.ToInt32(ds.Tables[0].Rows[i]["account_id"].ToString());
                    uobj.AccountType = ds.Tables[0].Rows[i]["account_type"].ToString();
                    account.Add(uobj);
                }
                ll.AccountList = account;
            }
            con.Close();
            return ll;
        }

        public ProfileModel dropdownrepo1()
        {
            model = new ProfileModel();
            connection();
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("SELECT account_id,account_type FROM Account_Details", con))
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(ds);
                List<ProfileModel> account = new List<ProfileModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ProfileModel uobj = new ProfileModel();
                    uobj.AccountID = Convert.ToInt32(ds.Tables[0].Rows[i]["account_id"].ToString());
                    uobj.AccountType = ds.Tables[0].Rows[i]["account_type"].ToString();
                    account.Add(uobj);
                }
                model.AccountList = account;
            }
            con.Close();
            return model;
        }

        public bool AddUsersrepo(ProfileModel model, FormCollection form)
        {
            connection();
            string accountid = Convert.ToString(form["AccountList"]);
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SP_Admin_Instructor_User", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.Name);
                cmd.Parameters.AddWithValue("@email", model.Email);
                cmd.Parameters.AddWithValue("@dob", model.DateOfBirth);
                cmd.Parameters.AddWithValue("@phno", model.Contact);
                cmd.Parameters.AddWithValue("@gender", model.Gender);
                cmd.Parameters.AddWithValue("@address", model.Address);
                cmd.Parameters.AddWithValue("@password", model.Password);
                cmd.Parameters.AddWithValue("@accountid", accountid);
                cmd.Parameters.AddWithValue("@status", "Create");
                int i = cmd.ExecuteNonQuery();
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
}
