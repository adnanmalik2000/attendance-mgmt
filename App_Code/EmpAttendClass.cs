using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for EmpAttendClass
/// </summary>
public class EmpAttendClass
{
	public EmpAttendClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int AttendID { get; set; }

    public string CheckIn { get; set; }

    public string CheckOut { get; set; }

    public int AttDetailID { get; set; }

    public string BreakOut { get; set; }

    public string BreakIn { get; set; }

    public List<EmpClass> GetPresentEmployees(int EmpID)
    {
        List<EmpClass> Users = new List<EmpClass>();
        string errorMessage = string.Empty;
        Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        //write to diagnostics that this routine was called, along with the calling parameters.
        Trace.TraceInformation("[GetPresentEmployees] ");
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
                        using (SqlCommand cmd = new SqlCommand("GetPresentEmployees", cnx))
                        {


                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm1 = new SqlParameter("@EmpID", SqlDbType.Int);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = EmpID;
                            cmd.Parameters.Add(prm1);

                           
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
                                user.CheckIn = dr["CheckIn"].ToString();
                                user.LogoPath = dr["LogoName"].ToString();
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

    public List<EmpAttendClass> Insert_EmpLogs(int EmpID, DateTime time, DateTime AttDate, int status, int AttID)
    {
        string errorMessage = string.Empty;
        List<EmpAttendClass> listEmpAttd = new List<EmpAttendClass>();
        Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        //write to diagnostics that this routine was called, along with the calling parameters.
        Trace.TraceInformation("[SaveEmpLogs] ");
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
                        using (SqlCommand cmd = new SqlCommand("InsertEmpLogs", cnx))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter prm1 = new SqlParameter("@EmpID", SqlDbType.Int);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = EmpID;
                            cmd.Parameters.Add(prm1);

                            prm1 = new SqlParameter("@time", SqlDbType.DateTime);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = time;
                            cmd.Parameters.Add(prm1);

                            prm1 = new SqlParameter("@AttDate", SqlDbType.Date);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = AttDate;
                            cmd.Parameters.Add(prm1);

                            prm1 = new SqlParameter("@status", SqlDbType.Int);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = status;
                            cmd.Parameters.Add(prm1);


                            prm1 = new SqlParameter("@AttID", SqlDbType.NVarChar);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = AttID;
                            cmd.Parameters.Add(prm1);

                            SqlParameter outputparam = new SqlParameter("@result", SqlDbType.Int);
                            outputparam.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(outputparam);

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            //the call to get the data was successful
                            //any error after this is not caused by connection problems, so no retry is needed
                            success = true;
                            string result = outputparam.Value.ToString();
                            EmpAttendClass emp_obj= new EmpAttendClass();
                            emp_obj.AttendID = Convert.ToInt32(outputparam.Value.ToString());
                            listEmpAttd.Add(emp_obj);

                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                errorMessage = "No Errors found";
                            }
                            else
                            {
                                errorMessage = "Data saved successfully";
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
        
        return listEmpAttd;
    }

    public List<EmpAttendClass> GetEmpLastStatus(int EmpID)
    {
        List<EmpAttendClass> Users = new List<EmpAttendClass>();
        string errorMessage = string.Empty;
        Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        //write to diagnostics that this routine was called, along with the calling parameters.
        Trace.TraceInformation("[GetEmpLastStatus] ");
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
                        using (SqlCommand cmd = new SqlCommand("GetEmpLastAttdStatus", cnx))
                        {


                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm1 = new SqlParameter("@EmpID", SqlDbType.Int);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = EmpID;
                            cmd.Parameters.Add(prm1);


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
                                EmpAttendClass user = new EmpAttendClass();
                                user.AttendID = Convert.ToInt32(dr["EmpAttID"].ToString());
                                if (dr["LogDetailId"].ToString() != "")
                                user.AttDetailID = Convert.ToInt32(dr["LogDetailId"].ToString());
                                user.BreakIn = dr["BreakIn"].ToString();
                                user.BreakOut = dr["BreakOut"].ToString();
                                user.CheckIn = dr["CheckIn"].ToString();
                                user.CheckOut = dr["CheckOut"].ToString();

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

    
}