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

public partial class WebForm_Verification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strID,strCode;

        strID = Request.QueryString["ID"];
        strCode = Request.QueryString["C"];

        //strID = "123";
        //strCode = "3vDYCypI";

        DataTable dt = clsData.UploadVerification(strID, strCode);

        if (dt.Rows.Count > 0)
        {
            if (clsTransaction.UpDateVerification(strID, strCode) == true)
            {
                if (clsTransaction.UpDateVerification_PND(strID, strCode) == true)
                {
                    lblMsg.Text = "認證成功！";
                    //clsMsg.AlertMessage("認證成功！", this.Page);
                    //Server.Transfer("~/SystemDefault.aspx");
                }
            }
        }

    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/ApplicationDefault.aspx");
    }
}
