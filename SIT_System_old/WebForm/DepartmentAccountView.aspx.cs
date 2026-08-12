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

public partial class WebForm_DepartmentAccountView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            GvQuery();
        }
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            //if (clsTransaction.UpDateReservation("C", strID, "", "1") == true)
            //    clsMsg.AlertMessage("取消成功！", this.Page);
            //else
            //    clsMsg.AlertMessage("取消失敗！", this.Page);

            //GvQuery();
            Server.Transfer("AddDepartmentAccount.aspx?ID=" + strID);
        }
        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //string strToday;

            //strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            //if (clsTransaction.DelInfo(strID) == true)
            //{
            //    if (clsTransaction.DelDepartmentAccount(strID) == true)
            //        clsMsg.AlertMessage("刪除成功！", this.Page);
            //}
            //else
            //    clsMsg.AlertMessage("刪除失敗！", this.Page);

            GvQuery();
        }
    }

    private void GvQuery()
    {
        //DateTime dt1;
        //string strStartDate, strEndDate;

        //strStartDate = Request["date1"].ToString();
        //if (strStartDate != "")
        //{
        //    dt1 = Convert.ToDateTime(strStartDate);
        //    strStartDate = dt1.ToString("yyyy/MM/dd");
        //}

        //strEndDate = Request["date2"].ToString();
        //if (strEndDate != "")
        //{
        //    dt1 = Convert.ToDateTime(strEndDate);
        //    dt1 = dt1.AddDays(1);
        //    strEndDate = dt1.ToString("yyyy/MM/dd");
        //}
        //DataTable dt = clsData.UploadApparatusReservation(txtSearch.Text, strStartDate, strEndDate, "0");
        DataTable dt;


        dt = clsData.getDepartmentAccount();
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    //protected void lbtnAdd_Click(object sender, EventArgs e)
    //{
    //    Server.Transfer("~/WebForm/AddDepartmentAccount.aspx");
    //}

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion
}
