using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class WebForm_AllDailyReport : System.Web.UI.Page
{
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //{
        //    Response.Redirect("~/SystemDefault.aspx");
        //}
        if (!IsPostBack)
        {
            if (Session["EmpNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");

            DateTime FirstDay = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);

            strStart = FirstDay.ToString("yyyy/MM/dd");
            strStart1 = LastDay.ToString("yyyy/MM/dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GvQuery();
    }

    private void GvQuery()
    {
        DateTime dt1;
        string strStartDate, strEndDate;

        strStartDate = Request["date1"].ToString();
        if (strStartDate != "")
        {
            dt1 = Convert.ToDateTime(strStartDate);
            strStartDate = dt1.ToString("yyyy/MM/dd");
        }

        strEndDate = Request["date2"].ToString();
        if (strEndDate != "")
        {
            dt1 = Convert.ToDateTime(strEndDate);
            dt1 = dt1.AddDays(1);
            strEndDate = dt1.ToString("yyyy/MM/dd");
        }
        DataTable dt = clsData.UploadApparatusReservation1(Session["EmpName"].ToString(), strStartDate, strEndDate);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;

        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('DepartmentDailyReport.aspx?Value=0&ID=" + strID + "');</script>");

        }
    }

}
