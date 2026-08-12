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

public partial class WebForm_AddBulletin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["EmpNo"].ToString() == "") || (Session["EmpNo"].ToString() == null))
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            getBulletin();
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strNote;

        strNote = CKEditorControl1.Text;
        //strNote = strNote.Replace("<p>", "");
        //strNote = strNote.Replace("</p>", "");

        if (clsTransaction.UpDateBulletin(strNote) == true)
            clsMsg.AlertMessage("修改成功！", this.Page);
    }

    private void getBulletin()
    {
        DataTable dt = clsData.UploadBulletin();

        CKEditorControl1.Text = dt.Rows[0]["Note"].ToString();
    }
}
