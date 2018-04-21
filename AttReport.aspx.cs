using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

public partial class AttReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAttd_Click(object sender, EventArgs e)
    {
        DateTime date = Convert.ToDateTime(txtAttDate.Text);
        GetAttendance(date);

    }

    private void GetAttendance(DateTime AttDate)
    {
        AttendanceClass obj_Att = new AttendanceClass();
        DataTable dt = obj_Att.GetDailyAttendance(AttDate);
        //if (dt.Rows.Count > 0)
        //{
           
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        if (rows["CheckIn"].ToString() == "")
        //            rows["CheckIn"] = "Absent";
        //    }
        //}
        grdAttend.DataSource = dt;
        grdAttend.DataBind();

        if (grdAttend.Rows.Count > 0)
        {
            btnExport.Enabled = true;
        }
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdAttend.AllowPaging = false;
            //this.BindGrid();

            grdAttend.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdAttend.HeaderRow.Cells)
            {
                cell.BackColor = grdAttend.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdAttend.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdAttend.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdAttend.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdAttend.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}