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

public partial class WebForm_ImageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strPath1 = Server.MapPath(".") + @"\pic";
        string strPath2;
        //string strFilePath;

        string strID = Request.QueryString["id"];
        string strKID = Request.QueryString["kid"];
        string strFID = Request.QueryString["fid"];

        if (!Directory.Exists(strPath1))  // 若目錄不存在則建立之
        {
            Directory.CreateDirectory(strPath1);
        }
        else
        {
            DirectoryInfo DIFO = new DirectoryInfo(strPath1);
            FileInfo[] filelist = DIFO.GetFiles();
            foreach (FileInfo fl in filelist)
            {
                System.IO.File.Delete(fl.FullName);
            }
            //Directory.Delete(strPath1, true);
            //System.Threading.Thread.Sleep(1000);
            //Directory.CreateDirectory(strPath1);
        }
        //System.Threading.Thread.Sleep(1000);
        //foreach (DataRow dr in dt.Rows)
        //{
        DataTable dt = clsData.UploadTestItem_File1(strKID, strFID, strID);

        strPath1 = Server.MapPath(".") + @"\pic";
        strPath2 = dt.Rows[0]["File_Path"].ToString() + @"\" + dt.Rows[0]["File_Name"].ToString();
        strPath1 = strPath1 + @"\" + dt.Rows[0]["File_Name"].ToString();
        File.Copy(strPath2, strPath1, true);
        Image1.ImageUrl = "pic/" + dt.Rows[0]["File_Name"].ToString();

        //    intI = intI + 1;
        //}
    }
}
