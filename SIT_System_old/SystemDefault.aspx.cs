using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void link1_Click(object sender, EventArgs e)
    {
        HttpCookie cookie_Authority = new HttpCookie("Authority");
        cookie_Authority.Value = Server.UrlEncode("0");
        Response.Cookies.Add(cookie_Authority);
        Response.Redirect("~/Default.aspx");
    }
    protected void Link2_Click(object sender, EventArgs e)
    {
        HttpCookie cookie_Authority = new HttpCookie("Authority");
        cookie_Authority.Value = Server.UrlEncode("1");
        Response.Cookies.Add(cookie_Authority);
        Response.Redirect("~/ApplicationDefault.aspx");
    }
    protected void Link3_Click(object sender, EventArgs e)
    {
        HttpCookie cookie_Authority = new HttpCookie("Authority");
        cookie_Authority.Value = Server.UrlEncode("2");
        Response.Cookies.Add(cookie_Authority);
        Response.Redirect("http://10.1.7.121/SIT_Benchmark/");
    }

    protected void Link4_Click(object sender, EventArgs e)
    {
        HttpCookie cookie_Authority = new HttpCookie("Authority");
        cookie_Authority.Value = Server.UrlEncode("3");
        Response.Cookies.Add(cookie_Authority);
        Response.Redirect("http://10.1.7.121/SIT_Reservation/");
    }
}
