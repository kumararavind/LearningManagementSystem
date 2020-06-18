using LMSProfile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Repository
{
    public class EnrollRepo
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private EnrollModel model;
        
        private void connection()
        {
            string connect = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            con = new SqlConnection(connect);
            cmd = new SqlCommand("SP_CRUD_Enrollments", con);
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public List<EnrollModel> GetAllCourseEnrollRepo()
        {
            connection();
            List<EnrollModel> EnrollList = new List<EnrollModel>();
            using (SqlCommand cmd = new SqlCommand("select course_id,course_category_id,course_name,course_instructor_id,course_length_hrs,course_desc,course_startdate,course_enddate from Course_Details", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                EnrollList.Add(
                    new EnrollModel
                    {
                        courseId = Convert.ToInt32(dr["course_id"]),
                        catId = Convert.ToInt32(dr["course_category_id"]),
                        courseName = Convert.ToString(dr["course_name"]),
                        instructorId = Convert.ToInt32(dr["course_instructor_id"])
                    }
                    );
                }
            }

            return EnrollList;
        }
        public bool AddEnrollRepo(EnrollModel obj,object s1)
        {

            connection();
            cmd.Parameters.AddWithValue("@catid", obj.catId);
            cmd.Parameters.AddWithValue("@courseid", obj.courseId);
            cmd.Parameters.AddWithValue("@instructorid", obj.instructorId);
            cmd.Parameters.AddWithValue("@userid", s1);
            cmd.Parameters.AddWithValue("@enrollmentid",1);
            cmd.Parameters.AddWithValue("@status", "Create");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }

        public List<EnrollModel> GetAllCourseEnrolledRepo(object s1,object s2)
        {
            connection();
            cmd.Parameters.AddWithValue("@accountid", s1);
            cmd.Parameters.AddWithValue("@userid",s2);
            cmd.Parameters.AddWithValue("@status", "Read");
            List<EnrollModel> EnrollList1 = new List<EnrollModel>();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            cmd.ExecuteNonQuery();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                EnrollList1.Add(
                    new EnrollModel
                    {
                        Id = Convert.ToInt32(dr["enroll_id"]),
                        courseName = Convert.ToString(dr["course_name"]),
                        catName = Convert.ToString(dr["category_name"]),
                        instructorName = Convert.ToString(dr["instructor_name"]),
                        userName = Convert.ToString(dr["user_name"]),
                        enrollmentStatus = Convert.ToInt32(dr["enrollment_status"])
                    }
                    );
            }

            return EnrollList1;
        }

        public EnrollModel dropdownrepo()
        {
            model = new EnrollModel();
            connection();
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("select enroll_id,enroll_status from enrollment_status", con))
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(ds);
                List<EnrollModel> Enroll = new List<EnrollModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnrollModel uobj = new EnrollModel();
                    uobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["enroll_id"].ToString());
                    uobj.enrollmentType = ds.Tables[0].Rows[i]["enroll_status"].ToString();
                    Enroll.Add(uobj);
                }
                model.EnrollmentList = Enroll;
            }
            con.Close();
            return model;
        }

        public bool UpdateEnrolledRepo(EnrollModel obj, object s1,FormCollection form)
        {

            connection();
            int s2 = Convert.ToInt32(form["EnrollmentList"]);
            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.Parameters.AddWithValue("@accountid", s1);
            cmd.Parameters.AddWithValue("@enrollmentid", s2);
            cmd.Parameters.AddWithValue("@status", "Update");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }

        public bool DeleteEnrollment(int Id,object s1)
        {
            connection();
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@accountid", s1);
            cmd.Parameters.AddWithValue("@status", "Delete");

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
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