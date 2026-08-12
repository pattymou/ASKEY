<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;

public class Upload : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        HttpPostedFile uploads = context.Request.Files["upload"];

        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        string file = System.IO.Path.GetFileName(uploads.FileName);
        var t = DateTime.Now.ToString("yyyyMMdd_hhmmss") + file;
        var filename = context.Server.MapPath(@"../") + "MessageImg\\" + t;
        uploads.SaveAs(filename);
        string url = "../MessageImg/" + t;
        //string url = context.Server.MapPath(@"../") + "MessageImg\\" + t;
        context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        //context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + url + "\");</script>");
        context.Response.End();
    }
 
    public bool IsReusable 
    {
        get {
            return false;
        }
    }

}