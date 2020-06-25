//using LMSProfile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Business;

namespace DataAccess
{
    public class CategoryRepo
    {
        public SqlConnection con;
        public SqlCommand com;
        //To Handle connection related activities    
        public void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            con = new SqlConnection(constr);
            com = new SqlCommand("SP_CRUD_Category", con);
            com.CommandType = CommandType.StoredProcedure;

        }

        public bool AddCategory(CategoryModel obj)
        {

            connection();
            com.Parameters.AddWithValue("@catname", obj.catName);
            com.Parameters.AddWithValue("@cattype", obj.catType);
            com.Parameters.AddWithValue("@status", "Create");
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
        //To view category details with generic list     
        public List<CategoryModel> GetAllCategory()
        {
            connection();
            List<CategoryModel> CatList = new List<CategoryModel>();
            com.Parameters.AddWithValue("@status", "Read");
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            com.ExecuteNonQuery();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                CatList.Add(
                    new CategoryModel
                    {
                        catId = Convert.ToInt32(dr["cat_id"]),
                        catName = Convert.ToString(dr["cat_name"]),
                        catType = Convert.ToString(dr["cat_type"])
                    }
                    );
            }

            return CatList;
        }
        public bool UpdateCategory(CategoryModel obj)
        {

            connection();
            com.Parameters.AddWithValue("@catid", obj.catId);
            com.Parameters.AddWithValue("@catname", obj.catName);
            com.Parameters.AddWithValue("@cattype", obj.catType);
            com.Parameters.AddWithValue("@status", "Update");
            con.Open();
            int i = com.ExecuteNonQuery();
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
        public bool DeleteCategory(int Id)
        {

            connection();
            com.Parameters.AddWithValue("@catid", Id);
            com.Parameters.AddWithValue("@status", "Delete");

            con.Open();
            int i = com.ExecuteNonQuery();
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
