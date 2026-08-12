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

public partial class WebForm_HomePage_A : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strRule;
        if (chkOK.Checked == true)
        {
            HttpCookie cookie_Rule_A = new HttpCookie("Rule_A");
            cookie_Rule_A.Value = Server.UrlEncode("1");
            //cookie_Rule.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_Rule_A);

            HttpCookie cookie_Rule = Request.Cookies["Rule_Kind"];

            strRule = Server.UrlDecode(cookie_Rule.Value);


            if (strRule == "Verification")
                Response.Redirect("~/WebForm/Application_N.aspx?Fun=" + Request.QueryString["Fun"]);

            else
                Response.Redirect("~/WebForm/Certification_Application.aspx");
        }
        else
            clsMsg.AlertMessage("請勾選「我已瞭解使用規則」", this.Page);
    }
}
