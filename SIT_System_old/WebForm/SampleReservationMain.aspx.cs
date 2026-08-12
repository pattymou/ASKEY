using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebForm_SampleReservationMain : System.Web.UI.Page
{
    public static string strRule;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["EmpNo"].ToString() == null) || (Session["EmpNo"].ToString() == ""))
        {

            linkCancel.Text = "[取消預約樣品]";
            linkDelay.Visible = false;
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");
            else if (Session["AppNo"].ToString() == "Public")
            {
                lblAdd.Visible = false;
                linkContinuous.Visible = false;
            }

            HttpCookie cookie_Rule = Request.Cookies["Rule"];
            if (cookie_Rule == null)
                Response.Redirect("~/WebForm/HomePage_N.aspx");

            strRule = Server.UrlDecode(cookie_Rule.Value);
            if (strRule == "")
                Response.Redirect("~/WebForm/HomePage_N.aspx");

        }
        else
        {

            linkCancel.Text = "[取消/歸還預約樣品]";
            linkDelay.Visible = true;

 
        }
        string strID;

        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
        {
            linkCancel.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadSampleQuery(txtSearch.Text, "0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        DataTable dt = clsData.UploadSampleQuery(txtSearch.Text, "0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
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
            Response.Write("<script>window.open('ReservationView_jq.aspx?ID=" + strID + "&Kind=2');</script>");

        }
        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('SampleView.aspx?ID=" + strID + "&Kind=0');</script>");
        }
        if (e.CommandName == "AddToCart2")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('SampleBorrowDetails.aspx?ID=" + strID + "');</script>");
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        if (linkDelay.Visible == false)
            Server.Transfer("~/WebForm/SampleDepartmentReservationCancel.aspx");
        else
            Server.Transfer("~/WebForm/SampleReservationCancel.aspx");
    }
    protected void lbtnDelay_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/DelaySample.aspx");
    }
    protected void lbtnContinuous_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SampleContinuous.aspx");
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SampleReservation.aspx");
    }
}
