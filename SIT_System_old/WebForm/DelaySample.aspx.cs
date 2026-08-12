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

public partial class WebForm_DelaySample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dt1;

        string strSQL, strValue, strStatus, strDate;

        DataTable dt;


        strStatus = "";
        strDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


        string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_DelayApparatus.txt";

        dt = clsData.getDelaySampleList(strDate);
        dataTableToText(dt, 12, strPath1);
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

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SampleReservationMain.aspx");
    }
}
