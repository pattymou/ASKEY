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

public partial class WebForm_SearchGoods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["EmpNo"] == null)
        //    Response.Redirect("~/Default.aspx");

        string strWrite;

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            lblAdd.Visible = false;
        }

        if (!IsPostBack)
        {
            loadKind(this.ddlKind);

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion 

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", ddlKind.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        string strWrite;

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            e.Row.Cells[0].Visible = false;
        }

    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //==========0217
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            DataTable dt = clsData.getEmployees("1", e.Row.Cells[5].Text);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Name_CH"].ToString() != "")
                {
                    e.Row.Cells[5].Text = dt.Rows[0]["Name_CH"].ToString();
                    e.Row.Cells[6].Text = dt.Rows[0]["Extension"].ToString();
                }
            }
            else
            {
                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";
            }

        }
    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strPath, strPath1;
        string strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[5].FindControl("lblGVSeq")).Text;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        if (clsTransaction.DelGoods(strName) == true)
        {
            if (clsTransaction.DelGoodsFile(strName) == true)
            {
                strPath = @"d:\Goods\" + strName + @"\";
                strPath1 = @"d:\Goods\" + strName;
                //Directory.Delete(strPath, true);

                DirectoryInfo DIFO = new DirectoryInfo(strPath);
                FileInfo[] filelist = DIFO.GetFiles();
                foreach (FileInfo fl in filelist)
                {
                    System.IO.File.Delete(fl.FullName);
                }
                Directory.Delete(strPath1, true);
                clsTransaction.DelPR_Goods(strName);
                //File.Delete(path);
                ((GridView)sender).SelectedIndex = -1;
                ((GridView)sender).EditIndex = -1;
                DataTable dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", "");
                this.gvwMain.DataSource = dt;
                this.DataBind();
            }
            else
            {
                clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
            }
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        }
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", "");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddGoods.aspx");
    }
}
