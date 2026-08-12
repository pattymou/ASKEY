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

public partial class WebForm_ProjectMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL;
        DataTable dt1;

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");


        string strWrite;

        Session["Fun"] = "9";

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            lblAdd.Visible = false;
        }

        DataTable dt;

        string strPath1 = Server.MapPath(@"../") + @"/ajax/data/ProjectMain_Open.txt";
        dt = clsData.getProjectMain("Open", "驗証申請");
        dataTableToText(dt, 2, strPath1);

        strPath1 = Server.MapPath(@"../") + @"/ajax/data/ProjectMain_Close.txt";
        dt = clsData.getProjectMain("Close", "驗証申請");
        dataTableToText(dt, 2, strPath1);

        strPath1 = Server.MapPath(@"../") + @"/ajax/data/ProjectMain_Hold.txt";
        dt = clsData.getProjectMain("Hold", "驗証申請");
        dataTableToText(dt, 2, strPath1);

        ///////////////////////

        //strPath1 = Server.MapPath(@"../") + @"/ajax/data/ProjectMain_Open_WJ.txt";
        //dt = clsData.getProjectMain("Open", "吳江");
        //dataTableToText(dt, 1, strPath1);

        //strPath1 = Server.MapPath(@"../") + @"/ajax/data/ProjectMain_Close_WJ.txt";
        //dt = clsData.getProjectMain("Close", "吳江");
        //dataTableToText(dt, 1, strPath1);

        //strPath1 = Server.MapPath(@"../") + @"/ajax/data/ProjectMain_Hold_WJ.txt";
        //dt = clsData.getProjectMain("Hold", "吳江");
        //dataTableToText(dt, 1, strPath1);

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
                    if ((j == 14) || (j == 13))
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
                        strDate = dTime.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                        else
                            strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    }
                    else
                        strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + ",\r\n";
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
        using (StreamWriter sw = new StreamWriter(DBPath))   //小寫TXT     
        {
            sw.Write(strSQLFile);
        }
    }
    #endregion

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        string strID1 = "";

        Server.Transfer("~/WebForm/ProjectEdit.aspx?A=1");
    }

    public string strValue1()
    {
        return Session["EmpDepartment"].ToString();
        //return "台北";
    }
}
