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

public partial class WebForm_GoodsReservationCancel : System.Web.UI.Page
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

            loadKind(this.ddlKind);
            //ddlLocal.Visible = false;
        }
    }

    protected void ddlLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocal.Text == "台北")
        {
            GvQuery("DA40");
            GvQuery1("DA40");
        }
        else
        {
            GvQuery("DA40-WJ");
            GvQuery1("DA40-WJ");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlLocal.Visible = true;

        if (ddlLocal.Text == "台北")
        {
            GvQuery("DA40");
            GvQuery1("DA40");
        }
        else
        {
            GvQuery("DA40-WJ");
            GvQuery1("DA40-WJ");
        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;
        string strCount;

        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[10].FindControl("lblGVSeq")).Text.Trim();
            //strCount = ((Label)row.Cells[3].FindControl("lblBorrowedQuantity")).Text.Trim();
           
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateGoodsReservation1("C", strID, "","0") == true)
            {

                //DataTable dt1 = clsData.UploadReservationAID(strID);
                
                //string strGoodsID;
                //strGoodsID = dt1.Rows[0]["Apparatus_ID"].ToString();
                //DataTable dt = clsData.UploadGoodsQuery(strGoodsID,"1","");
                //int intQuantity_Stock2 = 0;
                //intQuantity_Stock2 = Convert.ToInt16(dt.Rows[0]["Quantity_Stock"].ToString());
                //if (clsTransaction.UpDateGoodsCount("閒置中", strGoodsID) == true)
                    clsMsg.AlertMessage("取消成功！", this.Page);

            }
            else
                clsMsg.AlertMessage("取消失敗！", this.Page);

            if (ddlLocal.Text == "台北")
            {
                GvQuery("DA40");
                //GvQuery1(Session["EmpDepartment"].ToString());
            }
            else
            {
                GvQuery("DA40-WJ");
                //GvQuery1(Session["EmpDepartment"].ToString());
            }
        }
        if (e.CommandName == "AddToCart1")
        {
            string strToday, strCount1, strCount2, strCount3, strStatus;

            strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[10].FindControl("lblGVSeq")).Text.Trim();
            strCount1 = row.Cells[4].Text;
            strCount2 = row.Cells[3].Text;
            strCount3 = (Convert.ToInt16(strCount2) - Convert.ToInt16(strCount1)).ToString();
            if (strCount3 == "0")
                strStatus = "E";
            else
                strStatus = "";
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateGoodsReservation1(strStatus, strID, strCount3,"0") == true)
            {
                //DataTable dt1 = clsData.UploadReservationAID(strID);

                //string strGoodsID;
                //strGoodsID = dt1.Rows[0]["Apparatus_ID"].ToString();
                //if (clsTransaction.UpDateApparatusStatus("閒置中", strGoodsID) == true)
                clsMsg.AlertMessage("歸還成功！", this.Page);

            }
            else
                clsMsg.AlertMessage("歸還失敗！", this.Page);

            if (ddlLocal.Text == "台北")
            {
                GvQuery("DA40");
                //GvQuery1(Session["EmpDepartment"].ToString());
            }
            else
            {
                GvQuery("DA40-WJ");
                //GvQuery1(Session["EmpDepartment"].ToString());
            }
        }
    }

    protected void gvwMain1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;
        string strCount;

        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[9].FindControl("lblGVSeq")).Text.Trim();
            //strCount = ((Label)row.Cells[3].FindControl("lblBorrowedQuantity")).Text.Trim();

            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateGoodsReservation1("C", strID, "","1") == true)
            {

                //DataTable dt1 = clsData.UploadReservationAID(strID);

                //string strGoodsID;
                //strGoodsID = dt1.Rows[0]["Apparatus_ID"].ToString();
                //DataTable dt = clsData.UploadGoodsQuery(strGoodsID,"1","");
                //int intQuantity_Stock2 = 0;
                //intQuantity_Stock2 = Convert.ToInt16(dt.Rows[0]["Quantity_Stock"].ToString());
                //if (clsTransaction.UpDateGoodsCount("閒置中", strGoodsID) == true)
                clsMsg.AlertMessage("取消成功！", this.Page);

            }
            else
                clsMsg.AlertMessage("取消失敗！", this.Page);

            if (ddlLocal.Text == "台北")
            {
                //GvQuery("DA40");
                GvQuery1("DA40");
            }
            else
            {
                //GvQuery("DA40-WJ");
                GvQuery1("DA40-WJ");
            }
        }
        if (e.CommandName == "AddToCart1")
        {
            string strToday, strCount1, strCount2, strCount3, strStatus;

            strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[9].FindControl("lblGVSeq")).Text.Trim();
            //strCount1 = row.Cells[4].Text;
            //strCount2 = row.Cells[3].Text;
            //strCount3 = (Convert.ToInt16(strCount2) - Convert.ToInt16(strCount1)).ToString();
            //if (strCount3 == "0")
                strStatus = "E";
            //else
            //    strStatus = "";
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateGoodsReservation1(strStatus, strID, "","1") == true)
            {
                //DataTable dt1 = clsData.UploadReservationAID(strID);

                //string strGoodsID;
                //strGoodsID = dt1.Rows[0]["Apparatus_ID"].ToString();
                //if (clsTransaction.UpDateApparatusStatus("閒置中", strGoodsID) == true)
                clsMsg.AlertMessage("歸還成功！", this.Page);

            }
            else
                clsMsg.AlertMessage("歸還失敗！", this.Page);

            if (ddlLocal.Text == "台北")
            {
                //GvQuery("DA40");
                GvQuery1("DA40");
            }
            else
            {
                //GvQuery("DA40-WJ");
                GvQuery1("DA40-WJ");
            }
        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strID, strDate;
        DateTime dTime;

        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;

                //dTime = Convert.ToDateTime(e.Row.Cells[4].ToString());
                //strDate = dTime.ToString("yyyy/MM/dd");
                //if (strDate != "1900/01/01")
                //    e.Row.Cells[4].Text = strDate;
                //else
                //    e.Row.Cells[4].Text = "";
                dTime = Convert.ToDateTime(e.Row.Cells[5].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[5].Text = strDate;
                else
                    e.Row.Cells[5].Text = "";

                dTime = Convert.ToDateTime(e.Row.Cells[6].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[6].Text = strDate;
                else
                    e.Row.Cells[6].Text = "";

                if (e.Row.Cells[3].Text == "")
                    e.Row.Cells[3].Text = "0";

                if ((e.Row.Cells[4].Text == "") || (e.Row.Cells[4].Text == "&nbsp;"))
                    e.Row.Cells[4].Text = "0";

                int intCount;

                intCount = Convert.ToInt16(e.Row.Cells[3].Text) - Convert.ToInt16(e.Row.Cells[4].Text);
                e.Row.Cells[4].Text = intCount.ToString();

            }

        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                dTime = Convert.ToDateTime(e.Row.Cells[5].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[5].Text = strDate;
                else
                    e.Row.Cells[5].Text = "";


                dTime = Convert.ToDateTime(e.Row.Cells[6].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[6].Text = strDate;
                else
                    e.Row.Cells[6].Text = "";

                if (e.Row.Cells[3].Text == "")
                    e.Row.Cells[3].Text = "0";

                if ((e.Row.Cells[4].Text == "") || (e.Row.Cells[4].Text == "&nbsp;"))
                    e.Row.Cells[4].Text = "0";

                int intCount;

                intCount = Convert.ToInt16(e.Row.Cells[3].Text) - Convert.ToInt16(e.Row.Cells[4].Text);
                e.Row.Cells[4].Text = intCount.ToString();

            }
        }
    }

    protected void gvwMain1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strID, strDate;
        DateTime dTime;

        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;

                //dTime = Convert.ToDateTime(e.Row.Cells[4].ToString());
                //strDate = dTime.ToString("yyyy/MM/dd");
                //if (strDate != "1900/01/01")
                //    e.Row.Cells[4].Text = strDate;
                //else
                //    e.Row.Cells[4].Text = "";
                dTime = Convert.ToDateTime(e.Row.Cells[4].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[4].Text = strDate;
                else
                    e.Row.Cells[4].Text = "";

                dTime = Convert.ToDateTime(e.Row.Cells[5].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[5].Text = strDate;
                else
                    e.Row.Cells[5].Text = "";

                if (e.Row.Cells[3].Text == "")
                    e.Row.Cells[3].Text = "0";

                //if ((e.Row.Cells[4].Text == "") || (e.Row.Cells[4].Text == "&nbsp;"))
                //    e.Row.Cells[4].Text = "0";

                //int intCount;

                //intCount = Convert.ToInt16(e.Row.Cells[3].Text) - Convert.ToInt16(e.Row.Cells[4].Text);
                //e.Row.Cells[4].Text = intCount.ToString();

            }

        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                dTime = Convert.ToDateTime(e.Row.Cells[4].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[4].Text = strDate;
                else
                    e.Row.Cells[4].Text = "";


                dTime = Convert.ToDateTime(e.Row.Cells[5].Text);
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    e.Row.Cells[5].Text = strDate;
                else
                    e.Row.Cells[5].Text = "";

                if (e.Row.Cells[3].Text == "")
                    e.Row.Cells[3].Text = "0";

                //if ((e.Row.Cells[4].Text == "") || (e.Row.Cells[4].Text == "&nbsp;"))
                //    e.Row.Cells[4].Text = "0";

                //int intCount;

                //intCount = Convert.ToInt16(e.Row.Cells[3].Text) - Convert.ToInt16(e.Row.Cells[4].Text);
                //e.Row.Cells[4].Text = intCount.ToString();

            }
        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        if (ddlLocal.Text == "台北")
        {
            GvQuery("DA40");
            //GvQuery1(Session["EmpDepartment"].ToString());
        }
        else
        {
            GvQuery("DA40-WJ");
            //GvQuery1(Session["EmpDepartment"].ToString());
        }
    }
    #endregion

    #region gvwMain1_PageIndexChanging
    protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        if (ddlLocal.Text == "台北")
        {
            //GvQuery("DA40");
            GvQuery1("DA40");
        }
        else
        {
            //GvQuery("DA40-WJ");
            GvQuery1("DA40-WJ");
        }
    }
    #endregion

    private void GvQuery(string strLocal)
    {
        DateTime dt1;
        DataTable dt;
        string strStartDate, strEndDate, strEndDate1;

        strStartDate = DateTime.Now.ToString("yyyy/MM/dd");
        //strStartDate = Request["date1"].ToString();
        //if (strStartDate != "")
        //{
        //    dt1 = Convert.ToDateTime(strStartDate);
        //    strStartDate = dt1.ToString("yyyy/MM/dd");
        //}

        //strEndDate = Request["date2"].ToString();
        //strEndDate1 = Request["date2"].ToString();
        //if (strEndDate != "")
        //{
        //    dt1 = Convert.ToDateTime(strEndDate);
        //    dt1 = dt1.AddDays(1);
        //    strEndDate = dt1.ToString("yyyy/MM/dd");
        //}
        //strStart = strStartDate;
        //strStart1 = strEndDate1;

        dt = clsData.UploadGoodsReservation1(txtSearch.Text, strStartDate, "0", ddlKind.Text, strLocal);

        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    private void GvQuery1(string strLocal)
    {
        DateTime dt1;
        DataTable dt;
        string strStartDate, strEndDate, strEndDate1;

        strStartDate = DateTime.Now.ToString("yyyy/MM/dd");
        //strStartDate = Request["date1"].ToString();
        //if (strStartDate != "")
        //{
        //    dt1 = Convert.ToDateTime(strStartDate);
        //    strStartDate = dt1.ToString("yyyy/MM/dd");
        //}

        //strEndDate = Request["date2"].ToString();
        //strEndDate1 = Request["date2"].ToString();
        //if (strEndDate != "")
        //{
        //    dt1 = Convert.ToDateTime(strEndDate);
        //    dt1 = dt1.AddDays(1);
        //    strEndDate = dt1.ToString("yyyy/MM/dd");
        //}
        //strStart = strStartDate;
        //strStart1 = strEndDate1;

        dt = clsData.UploadGoodsReservation1(txtSearch.Text, strStartDate, "1", ddlKind.Text, strLocal);

        this.gvwMain1.DataSource = dt;
        this.DataBind();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/GoodsReservationMain.aspx");
    }
}
