using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExistMsg.Visible = false;
        SuccessMsg.Visible = false;

        if (!IsPostBack)
        {
            GetCompanies();
            GetUserbyCompID(Convert.ToInt32(ddlCompSearch.SelectedValue));
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

        ddlCompSearch.DataSource = dt;
        ddlCompSearch.DataTextField = "CompanyName";
        ddlCompSearch.DataValueField = "CompanyID";
        ddlCompSearch.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UsersClass obj_user = new UsersClass();

        string result = obj_user.Insert_Users(txtUsername.Text,txtPassword.Text,Convert.ToInt32(ddlcompany.SelectedValue),ddlRole.SelectedItem.Text,Convert.ToBoolean(1));
        if (result.Equals("Exists"))
        {
            ExistMsg.Visible = true;
        }
        else
        {
            SuccessMsg.Visible = true;
        }
       
        GetUserbyCompID(Convert.ToInt32(ddlCompSearch.SelectedValue));
    }


    protected void ddlCompSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetUserbyCompID(Convert.ToInt32(ddlCompSearch.SelectedValue));
    }

    private void GetUserbyCompID(int CompID)
    {
        UsersClass obj_User = new UsersClass();
        DataTable dt = new DataTable();
        dt = obj_User.GetUserByCompID(CompID);

        grdUsers.DataSource = dt;
        grdUsers.DataBind();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UsersClass obj_user = new UsersClass();
        Boolean status = Convert.ToBoolean(chkStatus.Checked);
        string result = obj_user.Update_Users(txtPassword.Text, Convert.ToInt32(txtUserID.Text),status);
        if (result.Equals("1"))
        {
            SuccessMsg.Visible = true;
        }
        

        GetUserbyCompID(Convert.ToInt32(ddlCompSearch.SelectedValue));
    }
}