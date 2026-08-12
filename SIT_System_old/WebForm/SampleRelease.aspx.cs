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

public partial class WebForm_SampleRelease : System.Web.UI.Page
{
    public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            strID = Request.QueryString["ID"];
            DataTable dt = clsData.UploadSample1("", strID);
            txtName.Text = dt.Rows[0]["Name"].ToString();
            getSampleRelease();
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim() == "")
        {
            clsMsg.AlertMessage("機種名稱不能為空白！", this.Page);
        }
        else
        {
            if (clsTransaction.UpdateSampleName(strID, txtName.Text) == true)
                clsMsg.AlertMessage("修改成功！", this.Page);
            else
                clsMsg.AlertMessage("修改失敗！", this.Page);
        }
    }

    private void getSampleRelease()
    {
        DataTable dt = clsData.UploadSampleRelease("",strID);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strPath, strPath1;
        string strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[5].FindControl("lblGVSeq")).Text;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        //if (clsTransaction.DelSample(strName) == true)
        //{
            if (clsTransaction.DelSampleRelease(strName,"1") != true)
            {
                clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
            }
        //}
        //else
        //{
        //    clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        //}
            DataTable dt = clsData.UploadSampleRelease("",strID);
            this.gvwMain.DataSource = dt;
            this.DataBind();
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadSampleRelease("",strID);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strSID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strSID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strSID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            //if (clsTransaction.UpDateReservation("C", strID, "", "1") == true)
            //    clsMsg.AlertMessage("取消成功！", this.Page);
            //else
            //    clsMsg.AlertMessage("取消失敗！", this.Page);

            //GvQuery();
            Server.Transfer("AddSampleRelease.aspx?SID=" + strID + "&ID=" + strSID);
        }
        
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddSampleRelease.aspx?SID=" + strID + "&ID=");
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SearchSample.aspx");
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strDate;
        DateTime dt;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            strDate = e.Row.Cells[6].Text;
            if (strDate != "")
            {
                dt = Convert.ToDateTime(strDate);
                e.Row.Cells[6].Text = dt.ToString("yyyy/MM/dd");
                if (e.Row.Cells[6].Text == "1900/01/01")
                    e.Row.Cells[6].Text = "";
            }

            strDate = e.Row.Cells[7].Text;
            if (strDate != "")
            {
                dt = Convert.ToDateTime(strDate);
                e.Row.Cells[7].Text = dt.ToString("yyyy/MM/dd");
                if (e.Row.Cells[7].Text == "1900/01/01")
                    e.Row.Cells[7].Text = "";
            }

        }
    }
}
