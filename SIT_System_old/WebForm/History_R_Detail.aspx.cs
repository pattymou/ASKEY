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

public partial class WebForm_History_R_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblName.Text = "";
        getApparatus();
        getHistoryDetail();
    }

    private void getApparatus()
    {
        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        //Session["ApparatusID"] = Request.QueryString["ID"].ToString();

        DataTable dt = clsData.UploadApparatusQuery(Request.QueryString["ID"].ToString(), "1", "");
        lblName.Text = dt.Rows[0]["name"].ToString();
    }

    private void getHistoryDetail()
    {
        //=====0217
        //DataTable dt = clsData.UploadApparatusStatus(txtSearch.Text, "0", "");
        DataTable dt = clsData.getHistoryReservation(Request.QueryString["ID"].ToString());
        //=====0217
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //=====0217
        //DataTable dt = clsData.UploadApparatusStatus(txtSearch.Text, "0", "");
        DataTable dt = clsData.getHistoryReservation(Request.QueryString["ID"].ToString());
        //=====0217
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //==========0217
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[5].Text == "E")
                e.Row.Cells[5].Text = "已歸還";

            if (e.Row.Cells[5].Text == "C")
                e.Row.Cells[5].Text = "已取消";

            if (e.Row.Cells[5].Text == "N")
                e.Row.Cells[5].Text = "Reject";

            if (e.Row.Cells[5].Text == "Y")
                e.Row.Cells[5].Text = "借用中";

            if ((e.Row.Cells[5].Text == "") || (e.Row.Cells[5].Text == " "))
                e.Row.Cells[5].Text = "已預約";

            DateTime dt1 = Convert.ToDateTime(e.Row.Cells[0].Text);
            e.Row.Cells[0].Text = dt1.ToString("yyyy/MM/dd");
            dt1 = Convert.ToDateTime(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = dt1.ToString("yyyy/MM/dd");

        }

        

    }
}
