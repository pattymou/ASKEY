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


public partial class WebForm_SampleReservationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL, strValue, strStatus;

        DataTable dt;

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        strStatus = "";

        DataTable dt1 = clsData.UploadApparatusMasterQuery("A4T", "0");
        string strMaster = dt1.Rows[0]["Name"].ToString();
        dt1 = clsData.UploadApparatusMasterQuery("A4W", "0");
        strMaster = strMaster + "," + dt1.Rows[0]["Name"].ToString();
        dt1 = clsData.UploadLeader("1", "", "");
        //string strLeader = dt1.Rows[0]["Name_En"].ToString();
        string strLeader = "";
        for (int intI = 0; intI < dt1.Rows.Count; intI++)
        {
            strLeader = strLeader + "," + dt1.Rows[0]["Name_En"].ToString();
        }
        //DataTable dt1 = clsData.UploadApparatusMasterQuery("A4", "0");
        //string strMaster = dt1.Rows[0]["Name"].ToString();
        //dt1 = clsData.UploadLeader("1", "", "");
        ////string strLeader = dt1.Rows[0]["Name_En"].ToString();
        //string strLeader = "";
        //for (int intI = 0; intI < dt1.Rows.Count; intI++)
        //{
        //    strLeader = strLeader + "," + dt1.Rows[0]["Name_En"].ToString();
        //}

        string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation.txt";
        if (Session["EmpName"].ToString() == strMaster)
        {

            dt = clsData.getSampleReservationList(Session["EmpName"].ToString(), "0");
            dataTableToText(dt, 8, strPath1);
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous.txt";
            dt = clsData.getSampleContinuousList(Session["EmpName"].ToString(), "0");
            dataTableToText(dt, 9, strPath1);
        }
        else
        {
            if (strLeader.IndexOf(Session["EmpName"].ToString()) != -1)
            {
                dt = clsData.getSampleReservationList(Session["EmpName"].ToString(), "0");
                dataTableToText(dt, 8, strPath1);
                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous.txt";
                dt = clsData.getSampleContinuousList(Session["EmpName"].ToString(), "0");
                dataTableToText(dt, 9, strPath1);
            }
            else
            {
                dt = clsData.getSampleReservationList(Session["EmpName"].ToString(), "1");
                dataTableToText(dt, 9, strPath1);
                dt = clsData.getSampleContinuousList(Session["EmpName"].ToString(), "1");
                dataTableToText(dt, 10, strPath1);
            }
        }
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
        using (StreamWriter sw = new StreamWriter(DBPath))   //小寫TXT     
        {
            sw.Write(strSQLFile);
        }
    }
    #endregion
}
