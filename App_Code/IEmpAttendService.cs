using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmpAttendService" in both code and config file together.
[ServiceContract]
public interface IEmpAttendService
{
	[OperationContract]
    [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.WrappedRequest,
       UriTemplate = "GetPresentEmployees")]
    List<EmpClass> GetPresentEmployees(int EmpID);

    [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.WrappedRequest,
       UriTemplate = "InsertEmpLogs")]
    List<EmpAttendClass> InsertEmpLogs(int EmpID, string time, string AttDate, int status, int AttID);

}
