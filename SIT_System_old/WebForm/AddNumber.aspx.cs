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

public partial class WebForm_AddNumber : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["EmpName"] = null;
        Session["EmpNo"] = null;
        Session["AppNo"] = null;


        if (!IsPostBack)
        {
            Session["PicNumber"] = "";
            loadDepartment(this.ddlDepartment);


        }
            TextBox _textbox = this.txtPassWord;
            _textbox.Attributes.Add("value", _textbox.Text);

            TextBox _textbox1 = this.txtPassWord1;
            _textbox1.Attributes.Add("value", _textbox1.Text);
    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strMail,strCode;

        //txtPassWord.Text = Session["PassWord"].ToString();
        //txtPassWord1.Text = Session["PassWord1"].ToString();
        strMail = txtMail.Text.Trim();
        strMail = strMail.ToLower();
        if (Session["PicNumber"].ToString() == "")
            clsMsg.AlertMessage("請輸入驗證碼！", this.Page);
        else
        {
            if ((txtID.Text.Trim() == "") || (txtNumber.Text.Trim() == "") || (txtName.Text.Trim() == "") || (txtMail.Text.Trim() == "") || (txtExt.Text.Trim() == ""))
                clsMsg.AlertMessage("*為必填項目！", this.Page);
            else
            {
                DataTable dt = clsData.CheckAccountPwd_Dep1(txtID.Text.Trim(), "","1");
                if (dt.Rows.Count == 0)
                {
                    if (strMail.IndexOf("askey") == 0)
                        clsMsg.AlertMessage("請輸入ASKEY電子信箱！", this.Page);
                    else
                    {
                        if (txtPassWord.Text != txtPassWord1.Text)
                            clsMsg.AlertMessage("密碼不相同！", this.Page);
                        else
                        {
                            strCode = GetRandomString();
                            if (clsTransaction.InsertNumber(txtID.Text.Trim(), txtNumber.Text.Trim(), txtName.Text.Trim(), txtMail.Text.Trim(), ddlDepartment.Text, txtPassWord.Text.Trim(), txtCard.Text.Trim(), strCode, txtExt.Text) == true)
                            {
                                if (clsTransaction.InsertNumber_PND(txtID.Text.Trim(), txtNumber.Text.Trim(), txtName.Text.Trim(), txtMail.Text.Trim(), ddlDepartment.Text, txtPassWord.Text.Trim(), txtCard.Text.Trim(), strCode, txtExt.Text) == true)
                                {
                                    MailData(strCode);
                                    setEmpty();
                                    clsMsg.AlertMessage("新增成功！請至電子信箱進行驗證！", this.Page);
                                    //Server.Transfer("~/SystemDefault.aspx");
                                }
                            }
                        }
                    }
                }
                else
                    clsMsg.AlertMessage("此工號已註冊！", this.Page);
            }
        }

    }

    private void setEmpty()
    {
        txtID.Text = "";
        txtNumber.Text = "";
        txtName.Text = "";
        txtMail.Text = "";
        txtCard.Text = "";
        txtPassWord.Text = "";
        txtPassWord1.Text = "";

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //Response.Write("剛剛輸入的是" + txt_input.Text + "<hr/>");
        //Session["PassWord"] = txtPassWord.Text;
        //Session["PassWord1"] = txtPassWord1.Text;
        Session["PicNumber"] = txt_input.Text;
    }

    protected string GetRandomString()
    {
        Random r = new Random();

        string code = "";

        for (int i = 0; i < 8; ++i)
            switch (r.Next(0, 3))
            {
                case 0: code += r.Next(0, 10); break;
                case 1: code += (char)r.Next(65, 91); break;
                case 2: code += (char)r.Next(97, 122); break;
            }

        return code;
    }

    #region MailData
    private void MailData(string strCode1)
    {
        #region 宣告變數

        DateTime dt;


        #endregion

        #region mail config

        //mail標題
        string MailSubject = "SIT系統-電子信箱驗證信函";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_registered.txt");
        string strMailBody = myMailBody.ReadToEnd();

        //預設標準時數
        //int defulDailyTime = Convert.ToInt16(WebConfigurationManager.AppSettings["checkDailyTime"]);

        #endregion

        #region 找資料塞到SendMail內

        StreamReader myIP = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_IP.txt");
        string strIP = myIP.ReadToEnd();


        string strMail = txtMail.Text.Trim();

        string strLink = "http://" + strIP + "/WebForm/Verification.aspx?C=" + strCode1 + "&ID=" + txtID.Text.Trim();
        //string strLink = "http://10.7.5.88/SIT_System/WebForm/Verification.aspx?C=" + strCode1+"&ID=" + txtID.Text.Trim();

        string strBody = string.Format(strMailBody, strLink, "<br>");

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
}
