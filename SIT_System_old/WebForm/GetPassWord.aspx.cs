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
using System.IO;

public partial class WebForm_GetPassWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Visible = false;
        }
    }

    #region MailData
    private void MailData()
    {
        #region 宣告變數

        DateTime dt;


        #endregion

        #region mail config

        //mail標題
        string MailSubject = "SIT系統-忘記密碼";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_GetPwd.txt");
        string strMailBody = myMailBody.ReadToEnd();

        //預設標準時數
        //int defulDailyTime = Convert.ToInt16(WebConfigurationManager.AppSettings["checkDailyTime"]);

        #endregion

        #region 找資料塞到SendMail內

        StreamReader myIP = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_IP.txt");
        string strIP = myIP.ReadToEnd();


        string strMail = txtMail.Text.Trim();

        string strLink = "http://" + strIP + "/ApplicationDefault.aspx";
        //string strLink = "http://10.7.5.88/SIT_System/WebForm/Verification.aspx?C=" + strCode1+"&ID=" + txtID.Text.Trim();

        DataTable dt1 = clsData.CheckAccountPwd_Dep1(txtID.Text.Trim(), "", "");


        string strBody = string.Format(strMailBody, strLink, "<br>", txtID.Text.Trim(),dt1.Rows[0]["PassWord"].ToString());

        clsTransaction.SendMail(strMail, MailSubject, strBody);

        myMailBody.Close();
        myMailBody.Dispose();
        #endregion
    }
    #endregion

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ApplicationDefault.aspx");
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = true;
        MailData();
    }
}
