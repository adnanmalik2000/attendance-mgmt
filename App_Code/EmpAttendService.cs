using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmpAttendService" in code, svc and config file together.
public class EmpAttendService : IEmpAttendService
{
    public List<EmpClass> GetPresentEmployees(int EmpID)
	{
        List<EmpClass> users = new List<EmpClass>();
        EmpAttendClass objemp = new EmpAttendClass();
        users = objemp.GetPresentEmployees(EmpID);
        return users;

	}

    public List<EmpAttendClass> InsertEmpLogs(int EmpID, string time, string AttDate, int status, int AttID)
    {
        EmpAttendClass objemp = new EmpAttendClass();
        List<EmpAttendClass> listAttd = new List<EmpAttendClass>();
        DateTime t = Convert.ToDateTime(time.ToString());
        DateTime t1 = Convert.ToDateTime(AttDate);
        listAttd = objemp.Insert_EmpLogs(EmpID, t, t1, status,AttID);
        return listAttd;
    }

    public List<EmpAttendClass> GetEmpLastStatus(int EmpID)
    {
        EmpAttendClass obj_Attend = new EmpAttendClass();
        List<EmpAttendClass> listattendance = obj_Attend.GetEmpLastStatus(EmpID);
        return listattendance;
    }

}
