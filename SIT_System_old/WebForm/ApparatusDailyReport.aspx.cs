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

public partial class WebForm_ApparatusDailyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL, strValue, strStatus;

        DataTable dt;
        if (!IsPostBack)
        {
            if (Session["EmpNo"] == null)
            {
                linkDelay.Visible = false;
                if (Session["AppNo"] == null)
                    Response.Redirect("~/SystemDefault.aspx");
            }
            else
                linkDelay.Visible = true;


            strStatus = Session["EmpName"].ToString();

            string strToday = DateTime.Now.ToString("yyyy/MM/dd");
            dt = clsData.UploadDepartmentReservation(strStatus, strToday);
            string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_DailyReport.txt";

            dataTableToText(dt, 8, strPath1);
        }
    }

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        DateTime dTime;
        string strDate, strValue;

        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    if ((j == 3) || (j == 4))
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
                        strDate = dTime.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                        else
                            strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    }
                    else if (j == 5)
                    {
                        if (dt.Rows[i][j].ToString().Trim() == "D")
                            strValue = "白天";
                        else if (dt.Rows[i][j].ToString().Trim() == "N")
                            strValue = "晚上";
                        else
                            strValue = "";

                        strSQLFile += @"""" + strValue + @"""" + ",\r\n";
                    }
                    else
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
        using (StreamWriter sw = new StreamWriter(DBPath))   //小寫TXT     
        {
            sw.Write(strSQLFile);
        }
    }
    #endregion

    protected void lbtnDelay_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AllDailyReport.aspx");
    }
}
