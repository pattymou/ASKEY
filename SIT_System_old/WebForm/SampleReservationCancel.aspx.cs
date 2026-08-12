using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WebForm_SampleReservationCancel : System.Web.UI.Page
{
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
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

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;


        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateReservation("C", strID, "", "1", "", "Other") == true)
            {

                DataTable dt1 = clsData.UploadReservationAID(strID);

                string strSampleID;
                strSampleID = dt1.Rows[0]["Apparatus_ID"].ToString();
                if (clsTransaction.UpDateSampleStatus("閒置中", strSampleID) == true)
                    clsMsg.AlertMessage("取消成功！", this.Page);

            }
            else
                clsMsg.AlertMessage("取消失敗！", this.Page);

            GvQuery();
        }
        if (e.CommandName == "AddToCart1")
        {
            string strToday;

            strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateReservation("E", strID, strToday, "2", "", "Other") == true)
            {
                DataTable dt1 = clsData.UploadReservationAID(strID);

                string strSampleID;
                strSampleID = dt1.Rows[0]["Apparatus_ID"].ToString();
                if (clsTransaction.UpDateSampleStatus("閒置中", strSampleID) == true)
                    clsMsg.AlertMessage("歸還成功！", this.Page);

            }
            else
                clsMsg.AlertMessage("歸還失敗！", this.Page);

            GvQuery();
        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strID;

        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;

            }

        }
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

    private void GvQuery()
    {
        DateTime dt1;
        DataTable dt;
        string strStartDate, strEndDate, strEndDate1;

        strStartDate = Request["date1"].ToString();
        if (strStartDate != "")
        {
            dt1 = Convert.ToDateTime(strStartDate);
            strStartDate = dt1.ToString("yyyy/MM/dd");
        }

        strEndDate = Request["date2"].ToString();
        strEndDate1 = Request["date2"].ToString();
        if (strEndDate != "")
        {
            dt1 = Convert.ToDateTime(strEndDate);
            dt1 = dt1.AddDays(1);
            strEndDate = dt1.ToString("yyyy/MM/dd");
        }
        strStart = strStartDate;
        strStart1 = strEndDate1;

        dt = clsData.UploadSampleReservation(txtSearch.Text, strStartDate, strEndDate, "0");

        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SampleReservationMain.aspx");
    }
}
