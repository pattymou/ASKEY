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

public partial class WebForm_ReservationList : System.Web.UI.Page
{
    //public static string strValue1;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL, strValue, strStatus;

        DataTable dt;

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        //clsParameter.strEmpName = "patty_lu";
        //strValue1 = Session["EmpDepartment"].ToString();
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

        DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "0");
        string strMaster = dt1.Rows[0]["Name"].ToString();
        //dt1 = clsData.UploadApparatusMasterQuery("A1P", "0");
        //strMaster = strMaster + "," + dt1.Rows[0]["Name"].ToString();
        dt1 = clsData.UploadApparatusMasterQuery("A1T", "0");
        strMaster = strMaster + "," + dt1.Rows[0]["Name"].ToString();
        dt1 = clsData.UploadApparatusMasterQuery("A1W", "0");
        strMaster = strMaster + "," + dt1.Rows[0]["Name"].ToString();
        dt1 = clsData.UploadLeader("1", "", "");
        //string strLeader = dt1.Rows[0]["Name_En"].ToString();
        string strLeader = "";
        for (int intI = 0; intI < dt1.Rows.Count; intI++)
        {
            strLeader = strLeader + "," + dt1.Rows[0]["Name_En"].ToString();
        }
        string strPath1;
        //string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_" + Session["EmpNo"].ToString() + ".txt";

        //dt = clsData.getReservationList1(Session["EmpName"].ToString(), "0");
        //dataTableToText(dt, 7, strPath1);

        strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_Agent" + Session["EmpNo"].ToString() + ".txt";
        dt = clsData.getReservationList1(Session["EmpName"].ToString(), "1");
        dataTableToText(dt, 9, strPath1);

        //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_" + Session["EmpNo"].ToString() + ".txt";
        //dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "0");
        //dataTableToText(dt, 8, strPath1);

        strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_Agent" + Session["EmpNo"].ToString() + ".txt";
        dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "1");
        dataTableToText(dt, 10, strPath1);

        if ((strMaster.IndexOf(Session["EmpName"].ToString()) != -1) || (Session["EmpName"].ToString() == "Patty_Lu"))
        {
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_" + Session["EmpNo"].ToString() + ".txt";

            dt = clsData.getReservationList1(Session["EmpName"].ToString(), "4");
            dataTableToText(dt, 9, strPath1);

            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_" + Session["EmpNo"].ToString() + ".txt";
            dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "4");
            dataTableToText(dt, 10, strPath1);

            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_Leader" + Session["EmpNo"].ToString() + ".txt";
            dt = clsData.getReservationList1(Session["EmpName"].ToString(), "2");
            dataTableToText(dt, 9, strPath1);

            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_Leader" + Session["EmpNo"].ToString() + ".txt";
            dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "2");
            dataTableToText(dt, 10, strPath1);
        }
        else
        {
            if (strLeader.IndexOf(Session["EmpName"].ToString()) != -1)
            {
                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_" + Session["EmpNo"].ToString() + ".txt";

                dt = clsData.getReservationList1(Session["EmpName"].ToString(), "4");
                dataTableToText(dt, 9, strPath1);

                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_" + Session["EmpNo"].ToString() + ".txt";
                dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "4");
                dataTableToText(dt, 10, strPath1);

                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_Leader" + Session["EmpNo"].ToString() + ".txt";
                dt = clsData.getReservationList1(Session["EmpName"].ToString(), "2");
                dataTableToText(dt, 9, strPath1);

                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_Leader" + Session["EmpNo"].ToString() + ".txt";
                dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "2");
                dataTableToText(dt, 10, strPath1);
            }
            else
            {
                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_" + Session["EmpNo"].ToString() + ".txt";

                dt = clsData.getReservationList1(Session["EmpName"].ToString(), "0");
                dataTableToText(dt, 9, strPath1);

                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_" + Session["EmpNo"].ToString() + ".txt";
                dt = clsData.getContinuousList1(Session["EmpName"].ToString(), "0");
                dataTableToText(dt, 10, strPath1);


                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Reservation_Leader" + Session["EmpNo"].ToString() + ".txt";
                dt = clsData.getReservationList1(Session["EmpName"].ToString(), "3");
                dataTableToText(dt, 9, strPath1);

                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Continuous_Leader" + Session["EmpNo"].ToString() + ".txt";
                dt = clsData.getContinuousList(Session["EmpName"].ToString(), "3");
                dataTableToText(dt, 10, strPath1);
            }
        }
    }

    public string strName1()
    {
        return Session["EmpNo"].ToString();
    }

    public string strValue1()
    {
        return Session["EmpDepartment"].ToString();
        //return "台北";
    }

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        DateTime dTime;
        string strDate,strValue;

        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    if (columnCount == 9)
                    {
                        if ((j == 5) || (j == 4))
                        {
                            dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
                            strDate = dTime.ToString("yyyy/MM/dd");
                            if (strDate != "1900/01/01")
                                strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                            else
                                strSQLFile += @"""" + "" + @"""" + ",\r\n";
                        }
                        else if (j == 6)
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
                    }
                    else
                    {
                        if ((j == 5) || (j == 6) || (j == 4))
                        {
                            dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
                            strDate = dTime.ToString("yyyy/MM/dd");
                            if (strDate != "1900/01/01")
                                strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                            else
                                strSQLFile += @"""" + "" + @"""" + ",\r\n";
                        }
                        else if (j == 7)
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
                    }




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
