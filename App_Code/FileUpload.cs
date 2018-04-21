using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for FileUpload
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FileUpload : System.Web.Services.WebService {

    public FileUpload () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void UploadImages()
    {
        try
        {
            //HTTP Context to get access to the submitted data 
            HttpContext postedContext = HttpContext.Current;
            //File Collection that was submitted with posted data 
            HttpFileCollection Files = postedContext.Request.Files;
            //Make sure a file was posted 

            string imgname = (string)postedContext.Request.Form["imgname"];
            string userid = (string)postedContext.Request.Form["empid"];
            string status = (string)postedContext.Request.Form["status"];


            string filename, ext, imgpath, strPathAndQuery;
            DateTime nowdate = DateTime.Now.Date;
            filename = imgname.Split('.')[0];
            ext = imgname.Split('.')[1];
            filename = userid + "-" + status + "-" + nowdate.Year.ToString() + nowdate.Month.ToString() + nowdate.Day.ToString();
            filename = filename + "." + ext;

            if (Files.Count == 1 && Files[0].ContentLength > 1 && imgname != null && imgname != "")
            {
                //The byte array we'll use to write the file with 
                byte[] binaryWriteArray = new byte[Files[0].InputStream.Length];
                //Read in the file from the InputStream 
                Files[0].InputStream.Read(binaryWriteArray, 0, (int)Files[0].InputStream.Length);

                // strPathAndQuery = "D:\\Published_Apps\\HaloodieWeb\\NewImages\\";
                imgpath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadImages/") + filename;

                //Open the file stream 
                FileStream objfilestream = new FileStream(imgpath, FileMode.Create, FileAccess.ReadWrite);
                //Write the file and close it 
                objfilestream.Write(binaryWriteArray, 0, binaryWriteArray.Length);
                objfilestream.Close();

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = new Dictionary<string, object>();
                rows.Add(row);
                row.Add("response", "true");
                HttpContext.Current.Response.Write(serializer.Serialize(rows.ToArray().ToList()));

            }
            else
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = new Dictionary<string, object>();
                rows.Add(row);
                row.Add("response", "system error");
                HttpContext.Current.Response.Write(serializer.Serialize(rows.ToArray().ToList()));

            }




        }
        catch (Exception ex1)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>();
            rows.Add(row);
            row.Add("response", "Problem: " + ex1);
            HttpContext.Current.Response.Write(serializer.Serialize(rows.ToArray().ToList()));

        }
    }


    [WebMethod]
    public void UploadEmployeeImages()
    {
        try
        {
            //HTTP Context to get access to the submitted data 
            HttpContext postedContext = HttpContext.Current;
            //File Collection that was submitted with posted data 
            HttpFileCollection Files = postedContext.Request.Files;
            //Make sure a file was posted 

            string imgname = (string)postedContext.Request.Form["imgname"];
            string userid = (string)postedContext.Request.Form["empid"];
            

            string filename, ext, imgpath, strPathAndQuery;
            DateTime nowdate = DateTime.Now.Date;
            filename = imgname.Split('.')[0];
            ext = imgname.Split('.')[1];
            filename = userid + "-" + "Emp";
            filename = filename + "." + ext;

            string path = @"~\CompEmpImages\";
            string imagepath = path + "\\" + filename;
            string imagetype = "." + ext;

            int empid = Convert.ToInt32(userid);

            EmpClass obj_emp = new EmpClass();
            string result = obj_emp.Update_EmpImage(empid, filename, imagepath, imagetype);

            if (Files.Count == 1 && Files[0].ContentLength > 1 && imgname != null && imgname != "")
            {
                //The byte array we'll use to write the file with 
                byte[] binaryWriteArray = new byte[Files[0].InputStream.Length];
                //Read in the file from the InputStream 
                Files[0].InputStream.Read(binaryWriteArray, 0, (int)Files[0].InputStream.Length);

                // strPathAndQuery = "D:\\Published_Apps\\HaloodieWeb\\NewImages\\";
                imgpath = System.Web.Hosting.HostingEnvironment.MapPath("~/CompEmpImages/") + filename;

                //Open the file stream 
                FileStream objfilestream = new FileStream(imgpath, FileMode.Create, FileAccess.ReadWrite);
                //Write the file and close it 
                objfilestream.Write(binaryWriteArray, 0, binaryWriteArray.Length);
                objfilestream.Close();

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = new Dictionary<string, object>();
                rows.Add(row);
                row.Add("response", "true");
                HttpContext.Current.Response.Write(serializer.Serialize(rows.ToArray().ToList()));

            }
            else
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = new Dictionary<string, object>();
                rows.Add(row);
                row.Add("response", "system error");
                HttpContext.Current.Response.Write(serializer.Serialize(rows.ToArray().ToList()));

            }




        }
        catch (Exception ex1)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>();
            rows.Add(row);
            row.Add("response", "Problem: " + ex1);
            HttpContext.Current.Response.Write(serializer.Serialize(rows.ToArray().ToList()));

        }
    }
}
