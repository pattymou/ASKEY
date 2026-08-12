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

public partial class WebForm_SampleDepartmentReservationCancel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
        {
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");

        }
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
                clsMsg.AlertMessage("取消成功！", this.Page);
            else
                clsMsg.AlertMessage("取消失敗！", this.Page);

            GvQuery();
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
        string strDepartment;
        DataTable dt;


        strDepartment = Session["AppNo"].ToString();

        string strToday = DateTime.Now.ToString("yyyy/MM/dd");

        dt = clsData.UploadSampleReservation(txtSearch.Text, strToday, strDepartment, "1");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GvQuery();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SampleReservationMain.aspx");
    }
}
