using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Departments : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            GetCompanies();
            GetBranches(Convert.ToInt32(ddlcompany.SelectedValue));
            PopulateGrid(Convert.ToInt32(ddlBranch.SelectedValue));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DepartmentClass obj_dept = new DepartmentClass();
        string deptname = txtDepartment.Text;
      //  int compBranchID = Convert.ToInt32(ddlComBraAdd.SelectedValue);

        int compBranchID = Convert.ToInt32(hdbranchID.Value);

           string result = obj_dept.Insert_Departments(compBranchID,deptname);
        if (result.Equals("Exists"))
        {
            ExistMsg.Visible = true;
        }
        else
        {
            SuccessMsg.Visible = true;
            txtDepartment.Text = "";
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

        ddlCompAdd.DataSource = dt;
        ddlCompAdd.DataTextField = "CompanyName";
        ddlCompAdd.DataValueField = "CompanyID";
        ddlCompAdd.DataBind();
        ddlCompAdd.Items.Insert(0, new ListItem("---Select---", "---Select---"));


        if (role == "Admin")
        {
            string compID = Session["CompanyID"].ToString();
            ListItem itemToAdd = ddlcompany.Items.FindByValue(compID);
            ddlcompany.Items.Clear();
            ddlcompany.Items.Add(itemToAdd);

            ListItem itemToAdd1 = ddlCompAdd.Items.FindByValue(compID);
            ddlCompAdd.Items.Clear();
            ddlCompAdd.Items.Add(itemToAdd1);
            
            ddlCompAdd.Items.Insert(0, new ListItem("---Select---", "---Select---"));
           // ddlCompAdd.SelectedIndex = 0;
        }
    }

    private void GetBranches(int companyID)
    {
        CompBranchClass obj_branch = new CompBranchClass();
        DataTable dt = new DataTable();
        dt = obj_branch.GetCompBranchByCompID(companyID);
        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "CompBranchID";
        ddlBranch.DataBind();
    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranches(Convert.ToInt32(ddlcompany.SelectedValue));
        if (ddlBranch.Items.Count > 0)
            PopulateGrid(Convert.ToInt32(ddlBranch.SelectedValue));
        else
        {
            grdDept.DataSource = null;
            grdDept.DataBind();
        }
    }

    private void PopulateGrid(int BranchID)
    {
        DepartmentClass obj_Dept = new DepartmentClass();
        DataTable dt = new DataTable();
        dt = obj_Dept.GetBeptByBranch(BranchID);
        grdDept.DataSource = dt;
        grdDept.DataBind();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateGrid(Convert.ToInt32(ddlBranch.SelectedValue));
    }


    [WebMethod]
    public static List<ListItem> GetBranchesddl(int companyID)
    {
         
        CompBranchClass obj_branch = new CompBranchClass();
        DataTable dt = new DataTable();
        dt = obj_branch.GetCompBranchByCompID(companyID);
        List<ListItem> branches = new List<ListItem>();
        foreach (DataRow row in dt.Rows)
        {
            branches.Add(new ListItem
            {
                Enabled = true,
                Value = row["CompBranchID"].ToString(),
                Text = row["BranchName"].ToString()
            });
        }

        return branches;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DepartmentClass obj_dept = new DepartmentClass();
        string deptname = txtDepartment.Text;
        int deptID = Convert.ToInt32(txtDeptID.Text);

        string result = obj_dept.Update_Departments(deptname, deptID);
    }
}