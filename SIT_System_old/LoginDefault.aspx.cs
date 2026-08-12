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

public partial class LoginDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //hlTP.NavigateUrl = "http://10.1.7.121/SIT_System/ApplicationDefault.aspx";
        //hlWJ.NavigateUrl = "http://10.7.5.88/SIT_System/ApplicationDefault.aspx";
    }

    protected void link1_Click(object sender, EventArgs e)
    {
        HttpCookie cookie_Authority = new HttpCookie("Authority");
        cookie_Authority.Value = Server.UrlEncode("0");
        Response.Cookies.Add(cookie_Authority);
        Response.Redirect("http://10.1.7.121/SIT_System/ApplicationDefault.aspx");
    }
    protected void Link2_Click(object sender, EventArgs e)
    {
        HttpCookie cookie_Authority = new HttpCookie("Authority");
        cookie_Authority.Value = Server.UrlEncode("1");
        Response.Cookies.Add(cookie_Authority);
        Response.Redirect("http://10.7.5.88/SIT_System/ApplicationDefault.aspx");
    }
}
