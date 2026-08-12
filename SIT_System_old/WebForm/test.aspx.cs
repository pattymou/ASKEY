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
using ClosedXML.Excel;

public partial class WebForm_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        Response.Clear();
        //Response.Buffer = true;
        //Response.Charset = "";
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        //Response.ContentType = "application/ms-excel";
        //Response.AddHeader("Content-Disposition", "attachment,filename=YourExcelFileName.xls");

        Response.Buffer = true;
        Response.Charset = "BIG5";
        Response.AppendHeader("content-disposition", "attachment;filename=TestPlan.xls");
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("BIG5");
        Response.ContentType = "application/ms-excel";
        

        this.EnableViewState = false;

        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        System.IO.StringWriter objStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
        this.RenderControl(objHtmlTextWriter);
        Response.Write(objHtmlTextWriter.ToString());
        Response.End();
    }
}
