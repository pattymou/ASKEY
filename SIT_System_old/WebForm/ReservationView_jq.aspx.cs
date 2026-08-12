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

public partial class WebForm_ReservationView_jq : System.Web.UI.Page
{
    //public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //strID = Request.QueryString["ID"];
        lblName.Text = "";
        string strKind;

        strKind = Request.QueryString["Kind"].ToString();

        Session["Calendar"] = Request.QueryString["ID"].ToString();

        if (strKind == "0")
        {
            //HttpCookie cookie_ApparatusID = new HttpCookie("ApparatusID");
            //cookie_ApparatusID.Value = Server.UrlEncode(Request.QueryString["ID"].ToString());
            ////cookie_ApparatusID.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_ApparatusID);
            //Session["ApparatusID"] = Request.QueryString["ID"].ToString();
            getApparatus();
        }
        else if (strKind == "1")
        {
            //Session["Goods_ID"] = Request.QueryString["ID"].ToString();
            getGoods();
        }
        else
            getSample();


        //clsParameter.strApparatusID = Request.QueryString["ID"];
        //clsParameter.strApparatusID = "4";

    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        string strKind;

        strKind = Request.QueryString["Kind"].ToString();

        if (strKind == "0")
        {
            Server.Transfer("~/WebForm/ReservationMain.aspx");
        }
        else if (strKind == "1")
            Server.Transfer("~/WebForm/GoodsReservationMain.aspx");
        else
            Server.Transfer("~/WebForm/SampleReservationMain.aspx");
    }

    private void getApparatus()
    {
        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        //Session["ApparatusID"] = Request.QueryString["ID"].ToString();

        DataTable dt = clsData.UploadApparatusQuery(Request.QueryString["ID"].ToString(), "1", "");
        lblName.Text = dt.Rows[0]["name"].ToString();
    }

    private void getGoods()
    {

        DataTable dt = clsData.UploadGoodsQuery(Request.QueryString["ID"].ToString(), "1", "");
        lblName.Text = dt.Rows[0]["Name_En"].ToString() + '-' + dt.Rows[0]["Name_CH"].ToString();
    }

    private void getSample()
    {

        DataTable dt = clsData.UploadSampleQuery(Request.QueryString["ID"].ToString(), "1");
        lblName.Text = dt.Rows[0]["Number"].ToString() + '-' + dt.Rows[0]["ModelName"].ToString();
    }
}
