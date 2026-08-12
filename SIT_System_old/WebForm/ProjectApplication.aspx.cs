using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class WebForm_ProjectApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL,strValue,strStatus;

        DataTable dt;

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        //strValue = Request.QueryString["Value"];
        //if (strValue == "1")
        //{
        strStatus = "";
            //lblAdd.Visible = false;
        //}
        //else
        //{
        //    //strAssign = this.Page.Session["sess_emp_name"].ToString().Trim();
        //    strAssign = "patty_lu";
        //    lblAdd.Visible = true;
        //}
        //string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation.txt";
        //string strLocation_P;
        //HttpCookie cookie_Location_P = Request.Cookies["Location"];
        //strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

        dt = clsData.getProjectList(strStatus, "1", "", "", "驗証申請");
        string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_assign.txt";

        //dataTableToText(dt, 13, "C:\\inetpub\\wwwroot\\SIT_System\\ajax\\data\\arays_assign.txt");
        dataTableToText(dt, 14, strPath1);

        //dt = clsData.getProjectList(strStatus, "1", "", "吳江", "驗証申請");
        //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_assign_WJ.txt";

        ////dataTableToText(dt, 13, "C:\\inetpub\\wwwroot\\SIT_System\\ajax\\data\\arays_assign.txt");
        //dataTableToText(dt, 13, strPath1);
    }

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        DateTime dTime;
        string strDate;

        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    //if ((j == 12) || (j == 13))
                    //{
                    //    dTime = Convert.ToDateTime(dt.Rows[0]["end_date1"].ToString());
                    //    strDate = dTime.ToString("yyyy/MM/dd");
                    //    if (strDate != "1900/01/01")
                    //        strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                    //    else
                    //        strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    //}
                    //else
                        strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + ",\r\n";
                    //strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + ",\r\n";
                }
                else
                    strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + "\r\n";
            }
            if (i != dt.Rows.Count - 1)
                strSQLFile += "],\r\n";
            else
                strSQLFile += "]\r\n";
        }
        strSQLFile += "]" + "\r\n" + "}";
        strSQLFile = strSQLFile.Replace("/", "");
        strSQLFile = strSQLFile.Replace("\\", "");

        using (StreamWriter sw = new StreamWriter(DBPath))   //小寫TXT     
        {
            sw.Write(strSQLFile);
        }
    }
    #endregion

    //#region LogOut
    //protected void lbtnAdd_Click(object sender, EventArgs e)
    //{
    //    Server.Transfer("~/WebForm/ProjectEdit.aspx");
    //}
    //#endregion
}
