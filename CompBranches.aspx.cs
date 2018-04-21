using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CompBranches : System.Web.UI.Page
{
    string role;
    protected void Page_Load(object sender, EventArgs e)
    {
        ExistMsg.Visible = false;
        SuccessMsg.Visible = false;
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
            getBranches(Convert.ToInt32(ddlcompany.SelectedValue));
        }
    }

    private void GetCompanies()
    {
        CompanyClass obj_comp = new CompanyClass();
        DataTable dt = new DataTable();
        dt = obj_comp.GetCompanies();

        ddlcompany.DataSource = dt;
        ddlcompany.DataTextField = "CompanyName";
        ddlcompany.DataValueField = "CompanyID";
        ddlcompany.DataBind();

      
        ddlcompanyAdd.DataSource = dt;
        ddlcompanyAdd.DataTextField = "CompanyName";
        ddlcompanyAdd.DataValueField = "CompanyID";
        ddlcompanyAdd.DataBind();

        if (role == "Admin")
        {
            string compID = Session["CompanyID"].ToString();
            ListItem itemToAdd = ddlcompany.Items.FindByValue(compID);
            ddlcompany.Items.Clear();
            ddlcompany.Items.Add(itemToAdd);

            ddlcompanyAdd.Items.Clear();
            ddlcompanyAdd.Items.Add(itemToAdd);
        }

    }

    private void getBranches(int companyID)
    {
        CompBranchClass obj_cb = new CompBranchClass();
        DataTable dt = new DataTable();
        dt = obj_cb.GetCompBranchByCompID(companyID);
        grdCompany.DataSource = dt;
        grdCompany.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string branchName = txtBranchName.Text;
        double lati = Convert.ToDouble(txtlatitude.Text);
        double longi = Convert.ToDouble(txtlongitude.Text);
        decimal distance = Convert.ToDecimal(txtdistance.Text);
        string address = txtaddress.Text;
        int compID = Convert.ToInt32(ddlcompanyAdd.SelectedValue);
        bool chkDist = Convert.ToBoolean(chkDistance.Checked);
        bool uploadimg = Convert.ToBoolean(chkUploadimg.Checked);

        CompBranchClass obj_branch = new CompBranchClass();
       string result = obj_branch.Insert_CompBranch(branchName,lati,longi,address,distance,chkDist,compID,uploadimg);
       if (result.Equals("Exists"))
       {
           ExistMsg.Visible = true;
       }
       else
       {
           SuccessMsg.Visible = true;
       }

       getBranches(Convert.ToInt32(ddlcompany.SelectedValue));
    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBranches(Convert.ToInt32(ddlcompany.SelectedValue));
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string branchName = txtBranchName.Text;
        double lati = Convert.ToDouble(txtlatitude.Text);
        double longi = Convert.ToDouble(txtlongitude.Text);
        decimal distance = Convert.ToDecimal(txtdistance.Text);
        string address = txtaddress.Text;
        int compID = Convert.ToInt32(ddlcompanyAdd.SelectedValue);
        bool chkDist = Convert.ToBoolean(chkDistance.Checked);

        CompBranchClass obj_branch = new CompBranchClass();
        string result = obj_branch.Update_CompBranch(branchName, lati, longi, address, distance, chkDist, compID);
    }
}