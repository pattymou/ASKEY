using System;
using System.Data;
using System.Text;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ApplicationDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtAccount.Attributes.Add("value", "Public");
        //txtPassword.Attributes.Add("value", "Public");
        HttpCookie cookie_Rule_A = new HttpCookie("Rule_A");
        cookie_Rule_A.Value = Server.UrlEncode("");
        //cookie_Rule.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_Rule_A);
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //clsParameter.strAppNo = "";
        Session["AppNo"] = "";
        Session["EmpNo"] = "";
        Session["EmpName"] = "";
        string strAccount = this.txtAccount.Text.Trim();
        string strPassword = this.txtPassword.Text.Trim();
        DataTable dt = clsData.CheckAccountPwd_Dep1(strAccount, strPassword, "0");
        //DataTable dt = clsData.CheckAccountPwd_test(strAccount, strPassword, "0");
        if (dt.Rows.Count > 0)
        //if (intCount > 0)
        {
            //Session["sess_emp_name"] = dt.Rows[0]["Name_En"].ToString().Trim();
            //Session["sess_emp_no"] = dt.Rows[0]["ID"].ToString().Trim();
            //if ((dt.Rows[0]["Position"].ToString().Trim() == "高級工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "助理工程師"))
            //    Session["Position"] = "1";
            //else
            //    Session["Position"] = "2";
            //clsParameter.strAppName = dt.Rows[0]["Name_En"].ToString().Trim();
            //DataTable dt = clsData.CheckAccountPwd_Dep1(strAccount, strPassword, "0");
            Session["AppNo"] = dt.Rows[0]["ID"].ToString().Trim();
            Session["AppDep"] = dt.Rows[0]["Department"].ToString().Trim();
            //clsParameter.strAppNo = dt.Rows[0]["ID"].ToString().Trim();
            //clsParameter.strLocation_P = dt.Rows[0]["Location"].ToString().Trim();
            //clsParameter.strWrite = dt.Rows[0]["Write"].ToString().Trim();
            //if ((dt.Rows[0]["Position"].ToString().Trim() == "高級工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "助理工程師"))
            //    clsParameter.strEmpPosition = "1";
            //else
            //    clsParameter.strEmpPosition = "2";
            HttpCookie cookie_Authority = new HttpCookie("Authority");
            cookie_Authority.Value = Server.UrlEncode("1");
            Response.Cookies.Add(cookie_Authority);

            HttpCookie cookie_Rule = new HttpCookie("Rule");
            cookie_Rule.Value = Server.UrlEncode("");
            //cookie_Rule.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_Rule);

            //string strRule;

            //HttpCookie cookie_Rule = Request.Cookies["Rule"];
            //strRule = Server.UrlDecode(cookie_Rule.Value);

            //if (strRule == "1")
            Response.Redirect("~/WebForm/BulletinView.aspx");
            //else
                //Response.Redirect("~/WebForm/HomePage_N.aspx");

        }
        else
        {
            clsMsg.AlertMessage("帳號或密碼錯誤，不允許登入系統...", this.Page);
        }



    }

    protected void butLink_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/WebForm/AddNumber.aspx");
        Response.Redirect("~/WebForm/AddNumber.aspx");
    }
    protected void linkTaipei_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://10.1.7.121/SIT_System/ApplicationDefault.aspx");
    }
    protected void linkWJ_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://10.7.5.88/SIT_System/ApplicationDefault.aspx");
    }
    protected void linkPwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/GetPassWord.aspx");
    }
}
