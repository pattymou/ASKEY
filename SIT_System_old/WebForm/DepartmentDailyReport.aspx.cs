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

public partial class WebForm_DepartmentDailyReport : System.Web.UI.Page
{
    //public static string strID;
    public static string strToday;
    public static int intCount;
    public static string strValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strID;
        //DateTime dt1;
        //string strToday;
        //string strValue;

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //{
        //    if ((clsParameter.strAppNo == "") || (clsParameter.strAppNo == null))
        //        Response.Redirect("~/SystemDefault.aspx");
        //}
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            strID = Request.QueryString["ID"];
            //strValue = Request.QueryString["Value"];

            //if (strValue == "1")
            //{
            //    strToday = DateTime.Now.ToString("yyyy/MM/dd");

            //    //strID = "44";

            DataTable dt = clsData.UploadReservationDailyReport(Request.QueryString["ID"]);
            if (dt.Rows.Count == 0)
                txtNote.Text = "";
            else
                txtNote.Text = dt.Rows[0]["Note"].ToString();
            //    intCount = 0;

            //    for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
            //    {
            //        if (dt.Rows[intJ]["ReservationDate"].ToString().IndexOf(strToday) != -1)
            //            intCount = intJ;

            //    }

            //    gvwMain.EditIndex = intCount;
            //}
            //else
            //{
            //    butOK.Visible = false;
            //    butReturn.Visible = false;
            //}

            //GvQuery();
        }


    }

    //protected void gvwMain_DataBound(object sender, EventArgs e)
    //{
    //}

    //protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //}

    //protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    ((GridView)sender).PageIndex = e.NewPageIndex;
    //    ((GridView)sender).EditIndex = -1;
    //    ((GridView)sender).SelectedIndex = -1;
    //    GvQuery();
    //}

    //private void GvQuery()
    //{

    //    DataTable dt = clsData.UploadReservationDailyReport(Request.QueryString["ID"]);
    //    this.gvwMain.DataSource = dt;
    //    this.DataBind();
    //}

    protected void butOK_Click(object sender, EventArgs e)
    {

        //string strMorning = ((TextBox)gvwMain.Rows[intCount].Cells[1].FindControl("txtMorning")).Text;
        //string strAfternoon = ((TextBox)gvwMain.Rows[intCount].Cells[2].FindControl("txtAfternoon")).Text;
        //string strEvening = ((TextBox)gvwMain.Rows[intCount].Cells[3].FindControl("txtEvening")).Text;

        if (clsTransaction.UpDateDepartmentDailyReport(Request.QueryString["ID"],txtNote.Text) == true)
        {
            clsMsg.AlertMessage("新增成功！", this.Page);
        }
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        if (strValue == "1")
            Server.Transfer("~/WebForm/ApparatusDailyReport.aspx");
        else
            Server.Transfer("~/WebForm/AllDailyReport.aspx");
    }
}
