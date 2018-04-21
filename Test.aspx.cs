using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        EmpService obj = new EmpService();
        //string s = obj.GetUsernameByID(2);
    }

    [WebMethod]
    public void Insert_Rest_Images()
    {
        try
        {
            //HTTP Context to get access to the submitted data 
            HttpContext postedContext = HttpContext.Current;
            //File Collection that was submitted with posted data 
            HttpFileCollection Files = postedContext.Request.Files;
            //Make sure a file was posted 
            string tokenid = (string)postedContext.Request.Form["tokenid"];
            if (tokenid.Equals("491e8ebe-1ffc-4ea9-8026-d4030c50e37c"))
            {
                string imgname = (string)postedContext.Request.Form["imgname"];
                string userid = (string)postedContext.Request.Form["empid"];
                string status = (string)postedContext.Request.Form["status"];
              

                string filename, ext, imgpath, strPathAndQuery;
                DateTime nowdate = DateTime.Now;
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
                        imgpath = Server.MapPath("~/UploadImages/") + filename;

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

            else
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = new Dictionary<string, object>();
                rows.Add(row);
                row.Add("response", "invalid token");
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