using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Company : System.Web.UI.Page
{
    string role;
    protected void Page_Load(object sender, EventArgs e)
    {
        SuccessMsg.Visible = false;
        ExistMsg.Visible = false;
        if (Session["Role"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            role = Session["Role"].ToString();
        }
        if (!IsPostBack)
        {
            GetCompanies();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        CompanyClass obj_comp = new CompanyClass();

        string comAppID = RandomString(5);

        int maxid = obj_comp.GetMaxCompanyID();

        string logoname = "";
        string logoPath = "";
        string logoType = "";

        string path = @"~\CompEmpImages\";
        if (FileUploadInsert.HasFile)
        {
            logoType = Path.GetExtension(FileUploadInsert.FileName);
            logoname = maxid + "-" + "Comp" + logoType;
            logoPath = path + "\\" + logoname;
            FileUploadInsert.SaveAs(Server.MapPath(path + "\\" + logoname));
        }

       

        string result = obj_comp.Insert_Company(txtCompany.Text, comAppID, logoname, logoPath, logoType,maxid);
        if (result.Equals("Exists"))
        {
            ExistMsg.Visible = true;
        }
        else
        {
            SuccessMsg.Visible = true;
        }
        GetCompanies();
    }

    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private void GetCompanies()
    {
        DataTable dt= new DataTable();
        CompanyClass obj_comp = new CompanyClass();

        dt = obj_comp.GetCompanies();
        grdCompany.DataSource = dt;
        grdCompany.DataBind();
    }

    protected void btnupdatecomp_Click(object sender, EventArgs e)
    {
        CompanyClass obj_comp = new CompanyClass();
        int compid = Convert.ToInt32(txtComIDUpdate.Text);
        string logoname = "";
        string logoPath = "";
        string logoType = "";

        int haslogo = 0;

        string path = @"~\CompEmpImages\";
        if (FileUploadUpdate.HasFile)
        {
            haslogo = 1;
            logoType = Path.GetExtension(FileUploadUpdate.FileName);
            logoname = compid + "-" + "Comp" + logoType;
            logoPath = path + "\\" + logoname;
            FileUploadUpdate.SaveAs(Server.MapPath(path + "\\" + logoname));
        }


        string result = obj_comp.Update_Company(txtComNameUpdate.Text,txtComAppIDUpdate.Text, Convert.ToInt32(txtComIDUpdate.Text),haslogo,logoname,logoPath,logoType);
        if (result.Equals("Exists"))
        {
            ExistMsg.Visible = true;
        }
        else
        {
            SuccessMsg.Visible = true;
        }

        GetCompanies();
    }
}