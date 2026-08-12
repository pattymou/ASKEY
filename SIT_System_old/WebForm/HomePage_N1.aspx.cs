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

public partial class WebForm_HomePage_N1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (chkOK.Checked == true)
        {
            HttpCookie cookie_Rule = new HttpCookie("Rule");
            cookie_Rule.Value = Server.UrlEncode("1");
            //cookie_Rule.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_Rule);

            HttpCookie cookie_Write = new HttpCookie("Write");
            cookie_Write.Value = Server.UrlEncode("N");
            //cookie_Rule.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_Write);

            Response.Redirect("~/WebForm/Homepage.aspx");
        }
        else
            clsMsg.AlertMessage("請勾選「我已瞭解使用規則」", this.Page);
    }
}
