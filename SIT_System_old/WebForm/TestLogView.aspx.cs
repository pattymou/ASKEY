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


public partial class WebForm_TestLogView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataTable dt;


            Session["AppDep"] = "DA40";
            dt = clsData.getAutoTestLog(Session["AppDep"].ToString());
            this.gvwMain.DataSource = dt;
            this.DataBind();

            


        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;


        DataTable dt;




        dt = clsData.getAutoTestLog(Session["AppDep"].ToString());

        this.gvwMain.DataSource = dt;
        this.DataBind();
        //GvQuery();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            string strID = ((Label)row.Cells[6].FindControl("lblGVSeq")).Text.Trim();
            string strName;
            string strPath = @"D:\TestLog";

            DateTime dt1 = Convert.ToDateTime(((Label)row.Cells[1].FindControl("lblTestDate")).Text.Trim());
            strName = ((Label)row.Cells[1].FindControl("lblTestKind")).Text.Trim() + dt1.ToString("yyyyMMddHHmmss") + ".txt";
            //strName = "los.txt";

            Response.Redirect("filedownload.aspx?guid=" + strName + "&path=" + strPath);
            //Response.Redirect("~/WebForm/ProjectView.aspx?Fun=" + Session["Fun"].ToString());


        }


    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      
        string strPath, strPath1;
        string strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[5].FindControl("lblGVSeq")).Text;

        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        //if (clsTransaction.DelApparatus(strName) == true)
        //{
            if (clsTransaction.DelTestLogFile(strName) == true)
            {
                strPath = @"D:\TestLog\";
                //strPath1 = @"d:\Apparatus\" + strName;
                //Directory.Delete(strPath, true);
                
                //DateTime dt1 = Convert.ToDateTime(row.Cells[0].Text);
                DateTime dt1 = Convert.ToDateTime(((Label)this.gvwMain.Rows[e.RowIndex].Cells[0].FindControl("lblTestDate")).Text);
                strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblTestKind")).Text + dt1.ToString("yyyyMMddHHmmss") + ".txt";
                strPath = strPath + strName;
                System.IO.File.Delete(strPath);

                //DirectoryInfo DIFO = new DirectoryInfo(strPath);
                //FileInfo[] filelist = DIFO.GetFiles();
                //foreach (FileInfo fl in filelist)
                //{
                //    System.IO.File.Delete(fl.FullName);
                //}
                //Directory.Delete(strPath1, true);
                //clsTransaction.DelPR_Goods(strName);
                //File.Delete(path);
                ((GridView)sender).SelectedIndex = -1;
                ((GridView)sender).EditIndex = -1;
                DataTable dt = clsData.getAutoTestLog(Session["AppDep"].ToString());
                this.gvwMain.DataSource = dt;
                this.DataBind();
            }
            else
            {
                clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
            }
        //}
        //else
        //{
        //    clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        //}
    }
    #endregion


}
