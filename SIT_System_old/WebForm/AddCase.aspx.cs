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

public partial class WebForm_AddCase : System.Web.UI.Page
{
    //public static string strProjectID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        //strProjectID = Request.QueryString["ID"];
        //strProjectID = "20141218154008";
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strProjectID = cookie_Customer.Values["ID"];
    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        AddProjectCase();
    }

    #region AddProjectCase
    private void AddProjectCase()
    {
        string strID, strKind, strName, strExplain;

        double intNumber = 0;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strProjectID = cookie_Customer.Values["ID"];
        if ((txtCase.Text.Trim() == "") || (txtTask.Text.Trim() == ""))
        {
            clsMsg.AlertMessage("請輸入專案名稱及子任務名稱....", this.Page);
        }
        else
        {
            string strLast;
            strKind = txtCase.Text;
            strName = txtTask.Text;
            strExplain = txtNote.Text;

            DataTable dt = clsData.UploadProjectCaseID(Session["ID"].ToString(), "", "2", "");

            foreach (DataRow dr in dt.Rows)
            {
                intNumber = Convert.ToInt32(dr["ID"].ToString());
            }


            //if (strLast == "0")
            //    strID = ((Math.Ceiling(intNumber / 10) * 10)+1).ToString();
            //else
            strID = (Math.Ceiling(intNumber / 10) * 10).ToString();
            if (strID == intNumber.ToString())
                strID = (intNumber + 10).ToString();

            strLast = strID.Substring(1);
            if (strLast == "0")
                strID = (Convert.ToInt32(strID) + 1).ToString();


            if (strID == "0")
                strID = "11";




            if (clsTransaction.InsertProjectCase(strID, Session["ID"].ToString(), strKind, strName, "", "", "", "", "", "", "", "", strExplain, "", "","","","") == true)
            {
                clsMsg.AlertMessage("新增成功....", this.Page);

                //HttpCookie cookie_Customer = Request.Cookies["Project"];
                Server.Transfer("~/WebForm/ProjectDetail.aspx");
                //Server.Transfer("~/WebForm/ProjectDetail.aspx");
            }
            else
                clsMsg.AlertMessage("新增失敗....", this.Page);
        }

    }
    #endregion
}
