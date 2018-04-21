using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

public partial class Eployees : System.Web.UI.Page
{
    string role;
    protected void Page_Load(object sender, EventArgs e)
    {
      
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
            GetBranches(Convert.ToInt32(ddlcompany.SelectedValue));
            if (ddlBranch.Items.Count > 0)
            GetDepartments(Convert.ToInt32(ddlBranch.SelectedValue));
            if (ddlDept.Items.Count > 0)
                GetEmployees(Convert.ToInt32(ddlDept.SelectedValue));
        }
    }

    private void GetDepartments(int BranchID)
    {
        DepartmentClass obj_dept = new DepartmentClass();
        DataTable dt = new DataTable();
        dt = obj_dept.GetBeptByBranch(BranchID);

        ddlDept.DataSource = dt;
        ddlDept.DataTextField = "DeptName";
        ddlDept.DataValueField = "DeptID";
        ddlDept.DataBind();
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

    [WebMethod]
    public static List<ListItem> GetDeptddl(int BranchID)
    {
        DepartmentClass obj_dept = new DepartmentClass();
        DataTable dt = new DataTable();
        dt = obj_dept.GetBeptByBranch(BranchID);

        List<ListItem> depts = new List<ListItem>();
        foreach (DataRow row in dt.Rows)
        {
            depts.Add(new ListItem
            {
                Enabled = true,
                Value = row["DeptID"].ToString(),
                Text = row["DeptName"].ToString()
            });
        }

        return depts;
    }

    private void GetEmployees(int DeptID)
    {
        EmpClass obj_Emp = new EmpClass();
        DataTable dt = new DataTable();
        dt = obj_Emp.GetEmployeesByDeptID(DeptID);

        grdEmployee.DataSource = dt;
        grdEmployee.DataBind();
    }

    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranches(Convert.ToInt32(ddlcompany.SelectedValue));
        if(ddlBranch.Items.Count > 0)
         GetDepartments(Convert.ToInt32(ddlBranch.SelectedValue));
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepartments(Convert.ToInt32(ddlBranch.SelectedValue));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmpClass obj_emp = new EmpClass();

        int EmpID = obj_emp.GetMaxEmpID();

        string logoname = "";
        string logoPath = "";
        string logoType = "";

        string path = @"~\CompEmpImages\";
        if (FileUploadInsert.HasFile)
        {
            logoType = Path.GetExtension(FileUploadInsert.FileName);
            logoname = EmpID + "-" + "Emp" + logoType;
            logoPath = path + "\\" + logoname;
            FileUploadInsert.SaveAs(Server.MapPath(path + "\\" + logoname));
        }


        string result = obj_emp.Insert_Employee(txtFName.Text, txtLName.Text, txtUserName.Text, txtPassword.Text, Convert.ToInt32(ddlDept.SelectedValue),Convert.ToInt32(EmpID),logoname,logoPath,logoType);

        if (ddlDept.Items.Count > 0)
            GetEmployees(Convert.ToInt32(ddlDept.SelectedValue));
    }


    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmployees(Convert.ToInt32(ddlDept.SelectedValue));
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string fname = txtupFname.Text;
        string lname = txtupLname.Text;
        string username = txtupUname.Text;
        string password = txtupPass.Text;
        int status=0;
        if (chkStatus.Checked)
            status = 1;


        double lati = Convert.ToDouble(txtupLati.Text);
        double longi = Convert.ToDouble(txtupLong.Text);
        decimal distance = Convert.ToDecimal(txtupDistance.Text);

        string address = txtupAddress.Text;
        Boolean chkDist = Convert.ToBoolean(chkDistance.Checked);
        Boolean UploadImage = Convert.ToBoolean(chkScreenshots.Checked);

        int empID = Convert.ToInt32(txtEmpID.Text);

        int haslogo = 0;

        string logoname = "";
        string logoPath = "";
        string logoType = "";

        string path = @"~\CompEmpImages\";
        if (FileUploadUpdate.HasFile)
        {
            haslogo = 1;
            logoType = Path.GetExtension(FileUploadUpdate.FileName);
            logoname = empID + "-" + "Emp" + logoType;
            logoPath = path + "\\" + logoname;
            FileUploadUpdate.SaveAs(Server.MapPath(path + "\\" + logoname));
        }


        EmpClass obj_emp = new EmpClass();
        string result = obj_emp.Update_Employee(fname, lname, password, status, lati, longi, address, distance, chkDist, empID, UploadImage,haslogo,logoname,logoPath,logoType);
        if (result.Equals("1"))
        {
            SuccessMsg.Visible = true;
        }

        if (ddlDept.Items.Count > 0)
            GetEmployees(Convert.ToInt32(ddlDept.SelectedValue));

    }
}