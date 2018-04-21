using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentClass
/// </summary>
public class DepartmentClass
{
	public DepartmentClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Insert_Departments(int CompBranchID, string DeptName)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertDepartments", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompBranchID", CompBranchID);
                cmd.Parameters.AddWithValue("@DeptName", DeptName);

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

    public string Update_Departments(string DeptName, int DeptID)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateDepartments", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeptID", DeptID);
                cmd.Parameters.AddWithValue("@DeptName", DeptName);

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

    public DataTable GetBeptByBranch(int CompBranchID)
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetDepartmentByBranch", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompBranchID", CompBranchID);

                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }
    }


}