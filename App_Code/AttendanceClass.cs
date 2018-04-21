using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AttendanceClass
/// </summary>
public class AttendanceClass
{
	public AttendanceClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetDailyAttendance(DateTime AttDate)
    {
        using (SqlConnection conn = new SqlConnection(AppProperties.dbConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetEmpWHorsByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttDate",AttDate);

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