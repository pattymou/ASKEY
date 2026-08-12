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

public partial class WebForm_GoodsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL, strValue, strStatus;

        

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        strStatus = "";
        loadDataTable("Open");
        loadDataTable("Hold");

        
    }

    public string strValue1()
    {
        return Session["EmpDepartment"].ToString();
        //return "台北";
    }

    private void loadDataTable(string strStatus)
    {
        string strPath1;
        DataTable dt;
        DataTable dt1 = clsData.UploadApparatusMasterQuery("A3T", "0");
        string strMaster = dt1.Rows[0]["Name"].ToString();
        dt1 = clsData.UploadApparatusMasterQuery("A3W", "0");
        strMaster = strMaster + "," + dt1.Rows[0]["Name"].ToString();

        if (strStatus == "Open")
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_PR.txt";
        else
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_PR_Hold.txt";

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("PR_Date");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "PR_Date";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("PR_No");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "PR_No";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Application_Date");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Application_Date";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Signed_ID");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Signed_ID";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Note");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Note";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Total");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Total";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("ID");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "ID";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("Local");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "Local";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        if ((strMaster.IndexOf(Session["EmpName"].ToString()) != -1) || (Session["EmpName"].ToString() == "Patty_Lu"))
        {

            dt = clsData.getGoodsList("", strStatus);
            //dataTableToText(dt, 7, strPath1);
        }
        else
        {
            dt = clsData.getGoodsList("AAA", strStatus);
            //dataTableToText(dt, 7, strPath1);
        }

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            DataRow dr = dt_new.NewRow();
            dt1 = clsData.getGoodsCash(dt.Rows[intI]["ID"].ToString());

            dr["PR_Date"] = dt.Rows[intI]["PR_Date"].ToString();
            dr["PR_No"] = dt.Rows[intI]["PR_No"].ToString();
            dr["Application_Date"] = dt.Rows[intI]["Application_Date"].ToString();
            dr["Signed_ID"] = dt.Rows[intI]["Signed_ID"].ToString();
            dr["Note"] = dt.Rows[intI]["Note"].ToString();
            dr["Total"] = dt1.Rows[0]["a"].ToString();
            dr["ID"] = dt.Rows[intI]["ID"].ToString();
            dr["Local"] = dt.Rows[intI]["Accepted_Team"].ToString();

            dt_new.Rows.Add(dr);
        }


        dataTableToText(dt_new, 8, strPath1);
    }

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        DateTime dTime;
        string strDate,strLine;


        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    if ((j == 0) || (j == 2))
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
                        strDate = dTime.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                        else
                            strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    }
                    else if (j == 4)
                    {
                        strLine = dt.Rows[i][j].ToString().Trim().Replace("\n", " ").Replace("\r", " ");
                        strSQLFile += @"""" + strLine + @"""" + ",\r\n";
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

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddPR.aspx");
    }

    protected void lbtnHistorical_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/PR_HistoricalRecord.aspx");
    }

}
