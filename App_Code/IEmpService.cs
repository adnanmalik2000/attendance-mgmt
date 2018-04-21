using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmpService" in both code and config file together.
[ServiceContract]
public interface IEmpService
{
    [OperationContract]
    [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           UriTemplate = "GetUsernameByID")]
    List<EmpClass> GetUsernameByID(int EmpID);

    [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Wrapped,
       UriTemplate = "CheckLogingUser")]
    List<EmpClass> CheckLogingUser(string userName, string password);

    [WebInvoke(Method = "POST",
      RequestFormat = WebMessageFormat.Json,
      ResponseFormat = WebMessageFormat.Json,
      BodyStyle = WebMessageBodyStyle.WrappedRequest,
      UriTemplate = "CheckCompanyID")]
    List<CompanyClass> CheckCompanyID(string companyID);

    //[WebInvoke(Method = "POST",
    //  RequestFormat = WebMessageFormat.Json,
    //  ResponseFormat = WebMessageFormat.Json,
    //  BodyStyle = WebMessageBodyStyle.WrappedRequest,
    //  UriTemplate = "UploadImages")]
    //void UploadImages();
}
