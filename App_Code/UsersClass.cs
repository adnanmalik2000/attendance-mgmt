using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsersClass
/// </summary>
public class UsersClass
{
	public UsersClass()
	{
		//
        // TODO: Add constructor logic here  
		//
	}

    public DataTable CheckUserLogin(string username, String password)
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CheckUserLogin", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username",username);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public DataTable GetUserByCompID(int companyID)
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetUsersByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", companyID);

                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public string Insert_Users(string Username, string Password, int CompID,string Role, bool Status)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertUsers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@CompanyID", CompID);
                cmd.Parameters.AddWithValue("@RoleID", Role);
                cmd.Parameters.AddWithValue("@Status", Status);

                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@result";
                outPutParameter.SqlDbType = System.Data.SqlDbType.VarChar;
                outPutParameter.Size = 20;
                outPutParameter.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);

                conn.Open();
                int qry_res = cmd.ExecuteNonQuery();
                conn.Close();

                result = outPutParameter.Value.ToString();
                if (result.Equals("Exists"))
                {
                    return "Exists";
                }
                else
                {
                    if (qry_res == 1)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }


            }
        }
    }

    public string Update_Users(string Password, int UserID, bool Status)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateUsers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                 cmd.Parameters.AddWithValue("@Status", Status);

             

                conn.Open();
                int qry_res = cmd.ExecuteNonQuery();
                conn.Close();

               
                    if (qry_res == 1)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
             }
        }
    }
}