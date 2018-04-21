using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for EmpClass
/// </summary>
public class EmpClass
{
	public EmpClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int EmpID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
    public decimal Distance { get; set; }
    public Boolean ChkDistance { get; set; }
    public Boolean UploadImage { get; set; }
    public string Attendance { get; set; }

    public string CheckIn { get; set; }

    public string LogoPath { get; set; }
    public List<EmpClass> GetUserNameByEmpID(int EmpID)
    {
        string errorMessage = string.Empty;
        Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        //write to diagnostics that this routine was called, along with the calling parameters.
        Trace.TraceInformation("[ReturnUserName] ");
        int tryCount = 0;
        bool success = false;
        List<EmpClass> EmpList = new List<EmpClass>();

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
                        using (SqlCommand cmd = new SqlCommand("GetUserNameByEmpID", cnx))
                        {


                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();

                            SqlParameter prm = new SqlParameter("@EmpID", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = EmpID;
                            cmd.Parameters.Add(prm);

                             da.Fill(dt);
                            //the call to get the data was successful
                            //any error after this is not caused by connection problems, so no retry is needed
                            success = true;

                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                errorMessage = "No user were found";
                            }

                            JavaScriptSerializer ser = new JavaScriptSerializer();

                            foreach (DataRow dr in dt.Rows)
                            {
                                EmpClass user = new EmpClass();
                                user.EmpID = Convert.ToInt32(dr["EmpID"].ToString());
                                user.FirstName = dr["FirstName"].ToString();
                                user.LastName = dr["LastName"].ToString();
                                user.UserName = dr["UserName"].ToString();
                                user.ChkDistance = Convert.ToBoolean(dr["ChkDistance"].ToString());
                                if (dr["distance"].ToString() != "")
                                user.Distance = Convert.ToDecimal(dr["distance"].ToString());
                                user.Latitude = Convert.ToDouble(dr["latitude"].ToString());
                                user.Longitude = Convert.ToDouble(dr["longitude"].ToString());
                                user.UploadImage = Convert.ToBoolean(dr["UploadImage"].ToString());

                                EmpAttendClass obj = new EmpAttendClass();
                                List<EmpAttendClass> list = new List<EmpAttendClass>();
                               list = obj.GetEmpLastStatus(EmpID);
                               user.Attendance = ser.Serialize(list);
                                    


                                EmpList.Add(user);
                            }

                            // errorMessage = "{'username': " + dt.Rows[0][0].ToString() + "}";
                        }//using SqlCommand
                    } //using SqlConnection"
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
                    //    GlobalAppHelper.retrySleepTime[tryCount - 1], ex.Tostring());

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




        return EmpList;
    }

    public List<EmpClass> CheckLogingUser(string userName, string password)
    {
       List<EmpClass> Users = new List<EmpClass>();
        string errorMessage = string.Empty;
        Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        //write to diagnostics that this routine was called, along with the calling parameters.
        Trace.TraceInformation("[GetUserLogin] ");
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
                        using (SqlCommand cmd = new SqlCommand("CheckUserCredentials", cnx))
                        {


                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm1 = new SqlParameter("@UserName", SqlDbType.VarChar);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = userName;
                            cmd.Parameters.Add(prm1);

                            SqlParameter prm2 = new SqlParameter("@Password", SqlDbType.VarChar);
                            prm2.Direction = ParameterDirection.Input;
                            prm2.Value = password;
                            cmd.Parameters.Add(prm2);

                            SqlDataAdapter da = new SqlDataAdapter(cmd);

                            da.Fill(dt);
                            //the call to get the data was successful
                            //any error after this is not caused by connection problems, so no retry is needed
                            success = true;

                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                errorMessage = "No user were found";
                            }

                            foreach (DataRow dr in dt.Rows)
                            {
                                EmpClass user = new EmpClass();
                                user.EmpID = Convert.ToInt32(dr["EmpID"].ToString());
                                user.FirstName = dr["FirstName"].ToString();
                                user.LastName = dr["LastName"].ToString();
                                user.UserName = dr["UserName"].ToString();
                              
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

    public string Insert_Employee(string fName, string lName, string Username, string password, int Deptid, int EmpID, string LogoName, string LogoPath, string LogoType)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Insertemployees", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", fName);
                cmd.Parameters.AddWithValue("@LastName", lName);
                cmd.Parameters.AddWithValue("@UserName", Username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@DeptID", Deptid);
                cmd.Parameters.AddWithValue("@LogoName", LogoName);
                cmd.Parameters.AddWithValue("@LogoPath", LogoPath);
                cmd.Parameters.AddWithValue("@LogoType", LogoType);
                cmd.Parameters.AddWithValue("@EmpID", EmpID);
         
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

    public DataTable GetEmployeesByDeptID(int DeptID)
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetEmployeesByDept", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeptID", DeptID);

                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public string Update_Employee(string fName, string lName, string password, int status, double lati, double longi, string address,
        decimal dist, bool chkDist, int empID, bool UploadImage, int haslogo, string LogoName, string LogoPath, string LogoType)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateEmployee", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", fName);
                cmd.Parameters.AddWithValue("@LastName", lName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Status", status);

                cmd.Parameters.AddWithValue("@Latitude", lati);
                cmd.Parameters.AddWithValue("@Longitude", longi);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Distance", dist);
                cmd.Parameters.AddWithValue("@ChkDistance", chkDist);
                cmd.Parameters.AddWithValue("@UploadImage", UploadImage);

                cmd.Parameters.AddWithValue("@LogoName", LogoName);
                cmd.Parameters.AddWithValue("@LogoPath", LogoPath);
                cmd.Parameters.AddWithValue("@LogoType", LogoType);
                cmd.Parameters.AddWithValue("@HasLogo", haslogo);

                cmd.Parameters.AddWithValue("@EmpID", empID);

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

    public int GetMaxEmpID()
    {

        int maxId;
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("select MAX(EmpID + 1) from Employee", conn))
            {

                conn.Open();
                maxId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

            }
        }

        return maxId;
    }

     public string Update_EmpImage(int empID, string LogoName, string LogoPath, string LogoType)
    {
        string result = "";
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateEmpImage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LogoName", LogoName);
                cmd.Parameters.AddWithValue("@LogoPath", LogoPath);
                cmd.Parameters.AddWithValue("@LogoType", LogoType);

                cmd.Parameters.AddWithValue("@EmpID", empID);

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