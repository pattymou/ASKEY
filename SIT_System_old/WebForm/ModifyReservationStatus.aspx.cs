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
using System.Diagnostics;

public partial class WebForm_ModifyReservationStatus : System.Web.UI.Page
{
    //public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            getReservation();

            string strID1;
            //============0217
            strID1 = Session["EmpName"].ToString().Trim();
            //strID = "patty_lu";
            if ((strID1 == "") || (strID1 == null))
            {
                butOK.Visible = false;
            }
            //============0217
        }
    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        string strStatus, strToday;

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        //if (rdoAccpt.Checked == true)
        //    strStatus = "Y";
        //else
        //    strStatus = "N";

        if (ddlStatus.Text == "閒置中")
        {
            strStatus = "E";

            if (clsTransaction.UpDateReservation1(strStatus, Request.QueryString["ID"], strToday) == true)
            {
                //==========0217
                DataTable dt = clsData.UploadReservationAID(Request.QueryString["ID"]);
                string strApparatusID;
                strApparatusID = dt.Rows[0]["Apparatus_ID"].ToString();
                if (clsTransaction.UpDateApparatusStatus("閒置中", strApparatusID) == true)
                //==========0217
                {
                    MailData(strApparatusID, lblName.Text, strToday);
                    Response.Redirect("~/WebForm/DelayApparatus.aspx");
                }
            }
            else
                clsMsg.AlertMessage("更新失敗！", this.Page);
        }
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/DelayApparatus.aspx");
    }

    private void getReservation()
    {
        DateTime dTime;
        DataTable dt = clsData.UploadReservationQuery(Request.QueryString["ID"], "1");
        lblName.Text = dt.Rows[0]["Name"].ToString();
        lblProductID.Text = dt.Rows[0]["Products_ID"].ToString();
        //lblBrand.Text = dt.Rows[0]["Brand"].ToString();
        //lblModel.Text = dt.Rows[0]["Model"].ToString();
        DataTable dt1 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
        lblCustodian.Text = dt1.Rows[0]["Name_CH"].ToString();
        lblBorrower.Text = dt.Rows[0]["Borrower"].ToString();
        lblDepartment.Text = dt.Rows[0]["Department"].ToString();
        lblExt.Text = dt.Rows[0]["Ext"].ToString();
        lblMail.Text = dt.Rows[0]["Email"].ToString();
        dTime = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString().Trim());
        lblStartDate.Text = dTime.ToString("yyyy/MM/dd");
        //lblStartDate.Text = dt.Rows[0]["StartDate"].ToString();
        dTime = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
        lblEndDate.Text = dTime.ToString("yyyy/MM/dd");
        //txtNote.Text = dt.Rows[0]["Note"].ToString();
    }

    #region MailData
    private void MailData(string strID1, string strName1, string strToday1)
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

        string strDate = "";
        DataTable dt1 = clsData.getReservationView(strID1, strToday1);

        if (dt1.Rows.Count > 0)
        {
            string strMail = dt1.Rows[0]["Email"].ToString();
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
}
