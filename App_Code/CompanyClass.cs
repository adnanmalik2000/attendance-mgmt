using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for CompanyClass
/// </summary>
public class CompanyClass
{
	public CompanyClass()
	{
		//
		// TODO: Add constructor logic here
		//

        
	}

    public int CompanyID { get; set; }

    public string CompanyName { get; set; }

    public string CompanyIDForApp { get; set; }

    public string ImgPath { get; set; }

    public List<CompanyClass> CheckCompanyID(string companyID)
    {
        List<CompanyClass> Users = new List<CompanyClass>();
        string errorMessage = string.Empty;
        Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        //write to diagnostics that this routine was called, along with the calling parameters.
        Trace.TraceInformation("[CheckCompanyID] ");
        DataTable dt = new DataTable();
        int tryCount = 0;
        bool success = false;



        //This is the overall try/catch block to handle non-SQL exceptions and trace them.
        try
        {
            //This is the top of the retry loop. 
            do
            {
                //blank this out in case it loops back around and works the next time
                //increment the number of tries
                tryCount++;

                //this is the try block for the SQL code 
                try
                {
                    //put all SQL code in using statements, to make sure you are disposing of 
                    //  connections, commands, datareaders, etc.
                    //note that this gets the connection string from GlobalStaticProperties,
                    //  which retrieves it the first time from the Role Configuration.
                    using (SqlConnection cnx
                      = new SqlConnection(AppProperties.dbConnectionString))
                    {

                        cnx.Open();

                        //Execute the stored procedure and get the data.
                        using (SqlCommand cmd = new SqlCommand("CheckCompanyKey", cnx))
                        {


                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm1 = new SqlParameter("@CompanyIDForApp", SqlDbType.VarChar);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = companyID;
                            cmd.Parameters.Add(prm1);

                       
                            SqlDataAdapter da = new SqlDataAdapter(cmd);

                            da.Fill(dt);
                            //the call to get the data was successful
                            //any error after this is not caused by connection problems, so no retry is needed
                            success = true;

                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                errorMessage = "No company were found";
                            }

                            foreach (DataRow dr in dt.Rows)
                            {
                                CompanyClass user = new CompanyClass();
                                user.CompanyID = Convert.ToInt32(dr["CompanyID"].ToString());
                                user.CompanyName = dr["CompanyName"].ToString();
                                user.CompanyIDForApp = dr["CompanyIDForApp"].ToString();
                                user.ImgPath = dr["LogoName"].ToString();
                                Users.Add(user);
                            }


                        }//using SqlCommand
                    } //using SqlConnection
                }
                catch (SqlException ex)
                {
                    //This is handling the SQL Exception. It traces the method and parameters, the retry #, 
                    //  how long it's going to sleep, and the exception that occurred.
                    //Note that it is using the array retrySleepTime set up in GlobalStaticProperties.
                    errorMessage = ex.Message.ToString();
                    //Trace.TraceError("[SaveAttendeeData] attendee = {0},  Try #{2}, "
                    //  + "will sleep {3}ms. SQL Exception = {4}",
                    //   _attendee.Name, tryCount,
                    //    GlobalAppHelper.retrySleepTime[tryCount - 1], ex.ToString());

                    //if it is not the last try, sleep before looping back around and trying again
                    if (tryCount < AppProperties.MaxTryCount
                      && AppProperties.retrySleepTime[tryCount - 1] > 0)
                        Thread.Sleep(AppProperties.retrySleepTime[tryCount - 1]);
                }
                //it loops until it has tried more times than specified, or the SQL Execution succeeds
            } while (tryCount < AppProperties.MaxTryCount && !success);
        }
        //catch any general exception that occurs and send back an error message
        catch (Exception ex)
        {
            errorMessage = ex.Message.ToString();
        }
        Trace.Flush();

        return Users;

    }

    public string Insert_Company(string companyName, string CompAppID, string LogoName, string LogoPath, string LogoType, int CompanyID)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@CompanyIdForApp", CompAppID);
                cmd.Parameters.AddWithValue("@LogoName", LogoName);
                cmd.Parameters.AddWithValue("@LogoPath", LogoPath);
                cmd.Parameters.AddWithValue("@LogoType", LogoType);
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
               
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

    public string Update_Company(string companyName, string CompAppID, int CompanyID, int haslogo, string LogoName, string LogoPath, string LogoType)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@CompanyIDForApp", CompAppID);
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                cmd.Parameters.AddWithValue("@HasLogo", haslogo);
                cmd.Parameters.AddWithValue("@LogoName", LogoName);
                cmd.Parameters.AddWithValue("@LogoPath", LogoPath);
                cmd.Parameters.AddWithValue("@LogoType", LogoType);

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

    public DataTable GetCompanies()
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetCompanies", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
              
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public int GetMaxCompanyID()
    {
       
        int maxId;
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("select MAX(companyID + 1) from Company", conn))
            {
               
               conn.Open();
                maxId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

            }
        }

        return maxId;
    }
}