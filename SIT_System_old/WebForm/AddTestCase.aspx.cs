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

public partial class WebForm_AddTestCase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            DataTable dt = clsData.UploadApplication_TestCase("DA40");
            this.gvwMain.DataSource = dt;
            this.DataBind();

            lblName.Text = Request.QueryString["Name"];
            getTestCase();
        }
    }

    private void getTestCase()
    {
        int intJ;
        string strCase;
        DataTable dt = clsData.UploadCustomerTestCase(Request.QueryString["ID"]);

        if (dt.Rows.Count > 0)
        {
            strCase = dt.Rows[0]["TestCase"].ToString();
            string[] sArray = strCase.Split(',');
            foreach (string i in sArray)
            {
                for (intJ = 0; intJ < this.gvwMain.Rows.Count; intJ++)
                {
                    string strFunction_No;

                    strFunction_No = ((Label)this.gvwMain.Rows[intJ].Cells[4].FindControl("lblGVSeq")).Text;
                    if (strFunction_No == i.ToString())
                    {
                        ((CheckBox)gvwMain.Rows[intJ].FindControl("CheckBox2")).Checked = true;
                    }

                }
            }
        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

    }
    #endregion

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {

        for (int intI = 1; intI < 3; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }

    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strID,strFunction,strItem,strKind;
        strID = "";
        strKind = "";
        int intI = 0;
        int intJ = 0;

        for (int i = 0; i < this.gvwMain.Rows.Count; i++)
        {
            if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
            {
                if (strID == "")
                    strID = ((Label)this.gvwMain.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;
                else
                    strID = strID + "," + ((Label)this.gvwMain.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;

                if (strKind != gvwMain.Rows[i].Cells[1].Text)
                {
                    intI = intI + 1;
                    intJ = intI * 10;
                }
                else
                    intJ = intJ + 1;

                strKind = gvwMain.Rows[i].Cells[1].Text;
                strFunction = gvwMain.Rows[i].Cells[2].Text;
                strItem = gvwMain.Rows[i].Cells[3].Text;

            }
        }

        DataTable dt = clsData.UploadCustomerTestCase(Request.QueryString["ID"]);

        if (dt.Rows.Count == 0)
        {
            if (clsTransaction.InsertCustomer_TestCase(Request.QueryString["ID"].ToString(), strID) == true)
            {
                clsMsg.AlertMessage("新增成功！", this.Page);
            }
            else
            {
                clsMsg.AlertMessage("新增失敗....", this.Page);
            }
        }
        else
        {
            if (clsTransaction.DelCustomerTestCase(lblName.Text) == true)
            {
                if (clsTransaction.InsertCustomer_TestCase(lblName.Text, strID) == true)
                {
                    clsMsg.AlertMessage("修改成功！", this.Page);
                }
                else
                {
                    clsMsg.AlertMessage("修改失敗....", this.Page);
                }
            }
            else
            {
                clsMsg.AlertMessage("修改失敗....", this.Page);
            }

        }

        DataTable dt1 = clsData.UploadApplication_TestCase("DA40");
        this.gvwMain.DataSource = dt1;
        this.DataBind();

        getTestCase();
    }
}
