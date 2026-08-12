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

public partial class WebForm_ReservationCancel : System.Web.UI.Page
{
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            DateTime FirstDay = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);

            strStart = FirstDay.ToString("yyyy/MM/dd");
            strStart1 = LastDay.ToString("yyyy/MM/dd");

            loadKind(this.ddlKind);
            ddlLocal.Visible = false;
        }
    }

    protected void ddlLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocal.Text == "台北")
            GvQuery("DA40");
        else
            GvQuery("DA40-WJ");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlLocal.Visible = true;
        
        GvQuery(Session["EmpDepartment"].ToString());
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7, "0");

    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID,strName;

        string strToday;

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            strName = ((Label)row.Cells[4].FindControl("lblName")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.UpDateReservation("C", strID, "", "1","","Apparatus") == true)
            {

                DataTable dt1 = clsData.UploadReservationAID(strID);

                string strApparatusID;
                strApparatusID = dt1.Rows[0]["Apparatus_ID"].ToString();
                //if (clsTransaction.UpDateApparatusStatus("閒置中", strApparatusID) == true)
                //{
                    MailData(strApparatusID, strName, strToday);
                    clsMsg.AlertMessage("取消成功！", this.Page);
                //}

            }
            else
                clsMsg.AlertMessage("取消失敗！", this.Page);


            GvQuery(Session["EmpDepartment"].ToString());
        }
        if (e.CommandName == "AddToCart1")
        {


            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            strName = ((Label)row.Cells[4].FindControl("lblName")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            //if (clsTransaction.UpDateReservation("E", strID, strToday, "2","","") == true)
            if (clsTransaction.UpDateReservation1("E", strID, strToday) == true)
            {
                DataTable dt1 = clsData.UploadReservationAID(strID);

                string strApparatusID;
                strApparatusID = dt1.Rows[0]["Apparatus_ID"].ToString();

                dt1 = clsData.UploadReservationUsing(strApparatusID, strToday);
                DataTable dt2;
                dt2 = clsData.UploadReservationUsing1(strApparatusID, strToday);
                if ((dt1.Rows.Count == 0) && (dt2.Rows.Count == 0))             
                {
                    //if (clsTransaction.UpDateApparatusStatus("閒置中", strApparatusID) == true)
                    //{
                        MailData(strApparatusID, strName, strToday);
                        clsMsg.AlertMessage("歸還成功！", this.Page);
                    //}
                }
                else
                {
                    MailData(strApparatusID, strName, strToday);
                    clsMsg.AlertMessage("歸還成功！", this.Page);
                }
            }
            else
                clsMsg.AlertMessage("歸還失敗！", this.Page);


            GvQuery(Session["EmpDepartment"].ToString());
        }
    }

    #region MailData
    private void MailData(string strID1,string strName1,string strToday1)
    {
        #region 宣告變數

        DateTime dt;


        #endregion

        #region mail config

        //mail標題
        string MailSubject = "設備預約通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_ApparatusReservation.txt");
        string strMailBody = myMailBody.ReadToEnd();


        #endregion

        #region 找資料塞到SendMail內

        string strDate="";
        DataTable dt1 = clsData.getReservationView(strID1, strToday1);
        string strMail;

        if (dt1.Rows.Count > 0)
        {

            strMail = dt1.Rows[0]["Email"].ToString();


            if (dt1.Rows[0]["startdate"].ToString() != "")
            {
                dt = Convert.ToDateTime(dt1.Rows[0]["startdate"].ToString());
                strDate = dt.ToString("yyyy/MM/dd");
            }

            if (strMail != "")
            {
                string strBody = string.Format(strMailBody, strDate, strName1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                clsTransaction.SendMail(strMail, MailSubject, strBody);

                myMailBody.Close();
                myMailBody.Dispose();
            }
            
        }

        #endregion
    }
    #endregion

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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime dt1 = Convert.ToDateTime(e.Row.Cells[5].Text);
            e.Row.Cells[5].Text = dt1.ToString("yyyy/MM/dd");
            dt1 = Convert.ToDateTime(e.Row.Cells[6].Text);
            e.Row.Cells[6].Text = dt1.ToString("yyyy/MM/dd");
        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        if (ddlLocal.Text == "台北")
            GvQuery("DA40");
        else
            GvQuery("DA40-WJ");
    }
    #endregion

    private void GvQuery(string strLocal)
    {
        DateTime dt1;
        DataTable dt;
        string strStartDate, strEndDate, strEndDate1;

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

        //dt = clsData.UploadApparatusReservation(txtSearch.Text, strStartDate, strEndDate, "0", ddlKind.Text, strLocal);
        dt = clsData.UploadApparatusReservation(txtSearch.Text, "", "", "2", ddlKind.Text, strLocal);


        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/ReservationMain.aspx");

    }
}
