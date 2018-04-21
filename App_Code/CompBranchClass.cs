using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompBranchClass
/// </summary>
public class CompBranchClass
{
	public CompBranchClass()
	{
		//
		// TODO: Add constructor logic here
		//

        
	}

    public int compBranchID { get; set; }

    public string branchName { get; set; }

    public float latitude { get; set; }

    public float longitude { get; set; }

    public string address { get; set; }

    public float distance { get; set; }

    public bool chkDistance { get; set; }

    public string Insert_CompBranch(string branchName, double latitude, double longitude, string address, decimal distance, bool chkdist, int CompID, bool UploadImage)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertCompBranch", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchName", branchName);
                cmd.Parameters.AddWithValue("@Latitude", latitude);
                cmd.Parameters.AddWithValue("@Longitude", longitude);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Distance", distance);
                cmd.Parameters.AddWithValue("@ChkDistance", chkdist);
                cmd.Parameters.AddWithValue("@CompanyID", CompID);
                cmd.Parameters.AddWithValue("@UploadImage",UploadImage);


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

    public string Update_CompBranch(string branchName, double latitude, double longitude, string address, decimal distance, bool chkdist, int CompBranchID)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateCompBranch", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchName", branchName);
                cmd.Parameters.AddWithValue("@Latitude", latitude);
                cmd.Parameters.AddWithValue("@Longitude", longitude);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Distance", distance);
                cmd.Parameters.AddWithValue("@ChkDistance", chkdist);
                cmd.Parameters.AddWithValue("@CompBranchID", CompBranchID);


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

    public DataTable GetCompBranchByCompID(int CompanyID)
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetBranchesByCompID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);

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