using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class WebForm_filedownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DownLoadFile();
    }

    private void DownLoadFile()
    {
        string strName, strPath;
        string FilePath;
        //將虛擬路徑轉換成實體路徑
        //string FilePath = Request.QueryString["path"] + @"\" + Request.QueryString["guid"] as string;
        if (Session["fileupload_Name"] == null)
        {
            FilePath = Request.QueryString["path"] + @"\" + Request.QueryString["guid"] as string;
        }
        else
        {

            strName = Session["fileupload_Name"].ToString();
            strPath = Session["fileupload_Path"].ToString();
            //將虛擬路徑轉換成實體路徑
            FilePath = strPath + @"\" + strName as string;

            Session["fileupload_Name"] = null;
            Session["fileupload_Path"] = null;
        }


        if (FilePath.Split('\\').Length != 0)
        {
            string FileName = FilePath.Split('\\')[FilePath.Split('\\').Length - 1];

            //中文檔名作轉換
            //FileName = HttpUtility.UrlEncode(FileName, Encoding.UTF8);

            FileStream fr = new FileStream(FilePath, FileMode.Open,FileAccess.Read,FileShare.Read);
            Byte[] buf = new Byte[fr.Length];

            fr.Read(buf, 0, Convert.ToInt32(fr.Length));
            fr.Close();
            fr.Dispose();

            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = true;
            //轉換文字檔編碼格式用，但本次輸出無文字檔，故註解此段
            //Response.ContentEncoding = parEncoding;
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName);

            Response.BinaryWrite(buf);
            Response.End();
        }
    }
}
