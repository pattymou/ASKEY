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

public partial class WebForm_AddPath : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadTestCase(this.ddlTestCase);
            DataTable dt = clsData.UploadFilePathTestCaseQuery();
            this.gvwMain1.DataSource = dt;
            this.DataBind();
        }
    }
    protected void btnAddTestCase_Click(object sender, EventArgs e)
    {
        string strName;

        strName = txtTestCase.Text.Trim();

        if (strName == "")
        {
            clsMsg.AlertMessage("請輸入TestCase！", this.Page);
        }
        else
        {
            DataTable dt = clsData.UploadFilePathCaseQuery(strName);
            if (dt.Rows.Count == 0)
            {
                if (clsTransaction.InsertTestCase(strName) == true)
                {
                    clsMsg.AlertMessage("新增成功....", this.Page);
                    ddlTestCase.Items.Clear();
                    loadTestCase(this.ddlTestCase);

                    dt = clsData.UploadFilePathTestCaseQuery();
                    this.gvwMain1.DataSource = dt;
                    this.DataBind();
                }
                else
                    clsMsg.AlertMessage("新增失敗....", this.Page);
            }
            else
                clsMsg.AlertMessage("此Function已重覆....", this.Page);

            txtTestCase.Text = "";
        }

    }

    #region loadTestCase
    protected void loadTestCase(DropDownList DDL)
    {
        clsDropDownList.ddlTestCaseFunction(DDL);
    }
    #endregion
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        string strTestCase,strID,strName;

        if (txtItem.Text.Trim() == "")
        {
            clsMsg.AlertMessage("請輸入項目！", this.Page);
        }
        else
        {
            strTestCase = ddlTestCase.Text;
            DataTable dt = clsData.UploadTestCaseID(strTestCase);
            strID = dt.Rows[0]["id"].ToString();
            strName = txtItem.Text.Trim();

            dt = clsData.UploadFilePathKindQuery(strName);
            if (dt.Rows.Count == 0)
            {
                if (clsTransaction.InsertTestCaseItem(strID, strName) == true)
                {
                    clsMsg.AlertMessage("新增成功....", this.Page);
                    dt = clsData.UploadFileNameQuery(ddlTestCase.Text);
                    this.gvwMain.DataSource = dt;
                    this.DataBind();
                    txtItem.Text = "";
                }
                else
                    clsMsg.AlertMessage("新增失敗....", this.Page);
            }
            else
                clsMsg.AlertMessage("此TestCase已重覆....", this.Page);
        }


    }
    protected void ddlTestCase_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadFileNameQuery(ddlTestCase.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //GvQuery(true);
        DataTable dt = clsData.UploadFileNameQuery(ddlTestCase.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //GvQuery(true);
        DataTable dt = clsData.UploadFilePathTestCaseQuery();
        this.gvwMain1.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string strPath, strPath1;
        string strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[1].FindControl("lblGVSeq")).Text;
        string strID = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblID")).Text;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        if (clsTransaction.DelFilePath_Kind(strID, strName,"0") == true)
        {
            
                //File.Delete(path);
                ((GridView)sender).SelectedIndex = -1;
                ((GridView)sender).EditIndex = -1;
                DataTable dt = clsData.UploadFileNameQuery(ddlTestCase.Text);
                this.gvwMain.DataSource = dt;
                this.DataBind();

        }
        else
        {
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        }


    }
    #endregion

    #region gvwMain1_RowDeleting
    protected void gvwMain1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string strPath, strPath1;
        //string strName = ((Label)this.gvwMain1.Rows[e.RowIndex].Cells[1].FindControl("lblGVSeq")).Text;
        string strID = ((Label)this.gvwMain1.Rows[e.RowIndex].Cells[2].FindControl("lblID")).Text;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        if (clsTransaction.DelFilePath_TestCase(strID) == true)
        {
            clsTransaction.DelFilePath_Kind(strID, "", "1");
            //File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            DataTable dt = clsData.UploadFilePathTestCaseQuery();
            this.gvwMain1.DataSource = dt;
            this.DataBind();
            loadTestCase(this.ddlTestCase);

        }
        else
        {
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadFilePathTestCaseQuery();
        this.gvwMain1.DataSource = dt;
        this.DataBind();
    }
}
