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

public partial class Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //clsParameter.strEmpNo = "";
        Session["EmpNo"] = "";
        Session["EmpName"] = "";
        string strAccount = this.txtAccount.Text.Trim();
        string strPassword = this.txtPassword.Text.Trim();
        DataTable dt = clsData.CheckAccountPwd(strAccount, strPassword);
        if (dt.Rows.Count > 0)
        {
            //Session["sess_emp_name"] = dt.Rows[0]["Name_En"].ToString().Trim();
            //Session["sess_emp_no"] = dt.Rows[0]["ID"].ToString().Trim();
            //if ((dt.Rows[0]["Position"].ToString().Trim() == "高級工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "助理工程師"))
            //    Session["Position"] = "1";
            //else
            //    Session["Position"] = "2";

            Session["EmpName"] = dt.Rows[0]["Name_En"].ToString().Trim();
            Session["EmpNo"] = dt.Rows[0]["ID"].ToString().Trim();
            //clsParameter.strEmpName = dt.Rows[0]["Name_En"].ToString().Trim();
            //clsParameter.strEmpNo = dt.Rows[0]["ID"].ToString().Trim();

            //HttpCookie cookie_Location_P = new HttpCookie("Location");
            //cookie_Location_P.Value = Server.UrlEncode(dt.Rows[0]["Location"].ToString().Trim());
            ////cookie_Location_P.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_Location_P);
            Session["Location"] = dt.Rows[0]["Location"].ToString().Trim();
            Session["EmpDepartment"] = dt.Rows[0]["Department"].ToString().Trim();
            //clsParameter.strLocation_P = dt.Rows[0]["Location"].ToString().Trim();
            HttpCookie cookie_Write = new HttpCookie("Write");
            cookie_Write.Value = Server.UrlEncode(dt.Rows[0]["Write"].ToString().Trim());
            //cookie_Write.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_Write);
            
            //clsParameter.strWrite = dt.Rows[0]["Write"].ToString().Trim();
            Session["AppNo"] = dt.Rows[0]["Department"].ToString().Trim();
            //clsParameter.strAppNo = dt.Rows[0]["Department"].ToString().Trim();
            //if ((dt.Rows[0]["Position"].ToString().Trim() == "高級工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "工程師") || (dt.Rows[0]["Position"].ToString().Trim() == "助理工程師"))
            //    clsParameter.strEmpPosition = "1";
            //else
            //    clsParameter.strEmpPosition = "2";
            HttpCookie cookie_Authority = new HttpCookie("Authority");
            cookie_Authority.Value = Server.UrlEncode("0");
            Response.Cookies.Add(cookie_Authority);

            Response.Redirect("~/WebForm/BulletinView.aspx");
        }
        else
        {
            clsMsg.AlertMessage("帳號或密碼錯誤，不允許登入系統...", this.Page);
        }
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtAccount.Text = "";
        txtPassword.Text = "";
    }
}
