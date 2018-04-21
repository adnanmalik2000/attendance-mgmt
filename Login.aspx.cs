using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        UsersClass obj_User = new UsersClass();
        dt = obj_User.CheckUserLogin(txtUsername.Text.Trim(),txtPassword.Text.Trim());

        if (dt.Rows.Count > 0)
        {
            Session["Role"] = dt.Rows[0]["RoleID"].ToString();
            Session["CompanyID"] = dt.Rows[0]["CompanyID"].ToString();
            Session["Username"] = txtUsername.Text;

            Response.Redirect("Company.aspx");
        }
        else
        {

        }

    }
}