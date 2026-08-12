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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Report_rpt_ProductReport2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate, strStartDate1, strEndDate, strEndDate1, strID, strPID;
        int intUseTime, intJ, intBorrowingTimes, intUseTotal, intBorrowTotal;
        string strGName, strDep;
        double dTotal;

        strStartDate = Session["RDateS"].ToString() + " 00:00:00";
        strEndDate = Session["RDateE"].ToString() + " 23:59:59";
        strPID = Session["RPID"].ToString();
        strStartDate1 = Session["RDateS"].ToString();
        strEndDate1 = Session["RDateE"].ToString();


        DataTable dt_new1 = new DataTable("dt_new1");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new1.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Products_ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Products_ID";
        column2.DefaultValue = "0";
        dt_new1.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Name");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Name";
        column3.DefaultValue = "0";
        dt_new1.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Mission");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Mission";
        column4.DefaultValue = "0";
        dt_new1.Columns.Add(column4);

        DataColumn column5 = new DataColumn("GName");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "GName";
        column5.DefaultValue = "0";
        dt_new1.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Borrower");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Borrower";
        column6.DefaultValue = "0";
        dt_new1.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Department");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Department";
        column7.DefaultValue = "0";
        dt_new1.Columns.Add(column7);

        DataColumn column8 = new DataColumn("UseTime");
        column8.DataType = System.Type.GetType("System.Int32");
        column8.AllowDBNull = true;
        column8.Caption = "UseTime";
        column8.DefaultValue = "0";
        dt_new1.Columns.Add(column8);

        DataColumn column9 = new DataColumn("BorrowingTimes");
        column9.DataType = System.Type.GetType("System.Int32");
        column9.AllowDBNull = true;
        column9.Caption = "BorrowingTimes";
        column9.DefaultValue = "0";
        dt_new1.Columns.Add(column9);

        DataColumn column10 = new DataColumn("UsePercent");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "UsePercent";
        column10.DefaultValue = "0";
        dt_new1.Columns.Add(column10);

        DataColumn column11 = new DataColumn("BorrowingPercent");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "BorrowingPercent";
        column11.DefaultValue = "0";
        dt_new1.Columns.Add(column11);

        DataColumn column12 = new DataColumn("Customer");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "Customer";
        column12.DefaultValue = "0";
        dt_new1.Columns.Add(column12);

        DataColumn column13 = new DataColumn("Department1");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "Department1";
        column13.DefaultValue = "0";
        dt_new1.Columns.Add(column13);

        DataColumn column14 = new DataColumn("PU");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "PU";
        column14.DefaultValue = "0";
        dt_new1.Columns.Add(column14);

        DataColumn column15 = new DataColumn("DateRang");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "DateRang";
        column15.DefaultValue = "0";
        dt_new1.Columns.Add(column15);

        DataColumn column16 = new DataColumn("Days");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "Days";
        column16.DefaultValue = "0";
        dt_new1.Columns.Add(column16);

        DataColumn column17 = new DataColumn("Price");
        column17.DataType = System.Type.GetType("System.Int32");
        column17.AllowDBNull = true;
        column17.Caption = "Price";
        column17.DefaultValue = "0";
        dt_new1.Columns.Add(column17);
        string strDay = "2018/11/01";
       
        if (Convert.ToDateTime(strStartDate) >= Convert.ToDateTime(strDay))
        {
            CalculationByHour(dt_new1, 0, strPID, strStartDate, strEndDate, strEndDate1, strStartDate1);
        }
        else if ((Convert.ToDateTime(strStartDate) < Convert.ToDateTime(strDay)) && (Convert.ToDateTime(strEndDate) > Convert.ToDateTime(strDay)))
        {
            clsMsg.AlertMessage("日期區間請勿跨越2018年11月！", this.Page);
        }
        else if (Convert.ToDateTime(strEndDate) < Convert.ToDateTime(strDay))
        {
            CalculationByDay(dt_new1, 0, strPID, strStartDate, strEndDate, strEndDate1, strStartDate1);        
        }
    }

    #region CalculationByDay
    private void CalculationByDay(DataTable dt_new1, double dTotal, string strPID, string strStartDate, string strEndDate, string strEndDate1, string strStartDate1)
    {
        int intUseTime, intJ, intBorrowingTimes, intUseTotal, intBorrowTotal;
        string strGName;
        strGName = "";
        intUseTime = 0;
        intBorrowingTimes = 0;
        intUseTotal = 0;
        intBorrowTotal = 0;
        //if (Session["RDep"].ToString() == "ALL")
        //{

        DataTable dt2 = clsData.UploadProductReport(strStartDate, strEndDate, strPID, "1", Session["RLocal"].ToString());
        intBorrowTotal = Convert.ToInt32(dt2.Rows[0]["tcount"].ToString());

        dt2 = clsData.UploadProductReport(strStartDate, strEndDate, strPID, "0", Session["RLocal"].ToString());
        for (int x = 0; x < dt2.Rows.Count; x++)
        {
            intUseTotal = intUseTotal + Convert.ToInt32(dt2.Rows[x]["UseTime"].ToString());
        }


        //DataTable dt1 = clsData.UploadDepartment();

        //for (int i = 0; i < dt1.Rows.Count; i++)
        //{
        DataTable dt = clsData.UploadProductReport(strStartDate, strEndDate, strPID, "0", Session["RLocal"].ToString());
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            //if (dt.Rows[j]["Department"].ToString() == "D210")
            //    strStartDate1 = "0";
            strGName = dt.Rows[j]["Department"].ToString();

            if (j == dt.Rows.Count - 1)
            {
                intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                intBorrowingTimes = intBorrowingTimes + 1;

                DataRow dr = dt_new1.NewRow();
                dr["ID"] = dt.Rows[j]["ID"].ToString();
                dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                dr["Name"] = dt.Rows[j]["Name"].ToString();
                dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                dr["GName"] = dt.Rows[j]["GName"].ToString();
                dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                dr["Department"] = dt.Rows[j]["Department"].ToString();
                //if (intUseTime == 0)
                //{
                dr["UseTime"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) + 1).ToString();
                dr["UsePercent"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) / intUseTotal).ToString("#0.00");
                intUseTime = Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                //}
                //else
                //{
                //    dr["UseTime"] = intUseTime;
                //    dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(intUseTotal)) * 100;
                //    dr["UsePercent"] = dTotal.ToString("#0.00") + " %";
                //}
                dr["BorrowingTimes"] = intBorrowingTimes;
                dTotal = (Convert.ToDouble(intBorrowingTimes) / Convert.ToDouble(intBorrowTotal)) * 100;
                dr["BorrowingPercent"] = dTotal.ToString("#0.00") + " %";
                //dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                int intIndex, intIndex1;
                intIndex = dt.Rows[j]["Customer"].ToString().IndexOf("(");
                if (intIndex < 0)
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                else
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(0, intIndex);
                    //dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(1, intIndex - 1);

                string strDepartment2;
                //int intIndex, intIndex1;

                intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
                intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
                if (intIndex > 0)
                    strDepartment2 = dt.Rows[j]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                else
                    strDepartment2 = dt.Rows[j]["Department"].ToString();

                dr["Department1"] = strDepartment2;


                string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
                int intU = 0;
                foreach (string l in sArray)
                {
                    intU++;
                }
                if (intU == 2)
                    dr["PU"] = sArray[1].Replace("PU", "");
                else
                    dr["PU"] = sArray[0].Replace("PU", "");

                string strDate1, strDate2;
                DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());

                if (dt_Rang < Convert.ToDateTime(strStartDate))
                {
                    strDate1 = strStartDate1;
                }
                else
                    strDate1 = dt_Rang.ToString("yyyy/MM/dd");

                if (Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString()).Year.ToString() == "1900")
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());

                }
                else
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString());
                }
                //dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());

                strDate2 = dt_Rang.ToString("yyyy/MM/dd");
                if (dt_Rang > Convert.ToDateTime(strEndDate))
                    dr["DateRang"] = strDate1 + "~" + strEndDate1;
                else
                    dr["DateRang"] = strDate1 + "~" + strDate2;

                //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
                //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
                //TimeSpan Total = ETime.Subtract(STime); //日期相減

                //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
                //dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(24));
                //if (dt.Rows[j]["Period"].ToString() == "D")
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 9.5);
                //else
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 14.5);

                DateTime STime = DateTime.Parse(dt.Rows[j]["StartDate"].ToString()); //起始日
                DateTime ETime = DateTime.Parse(dt.Rows[j]["EndDate"].ToString()); //起始日
                DateTime RTime = DateTime.Parse(dt.Rows[j]["ReturnDate"].ToString()); //結束日

                if (Convert.ToDateTime(strStartDate) > STime)
                {
                    STime = DateTime.Parse(strStartDate); //起始日
                }

                if (Convert.ToDateTime(strEndDate) < ETime)
                {
                    ETime = DateTime.Parse(strEndDate); //起始日
                }


                TimeSpan Total, Total1;
                double dTotal1, dTotal2;

                if (RTime.Year.ToString() != "1900")
                {
                    if (dt.Rows[j]["Period"].ToString() == "D")
                    {

                        if (ETime > RTime)
                        {
                            string strEnd = ETime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 18:30:00";
                            string strEnd1 = RTime.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 18:30:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減
                            Total1 = RTime.Subtract(RTime1); //日期相減
                            dTotal1 = Convert.ToDouble(Total.Days.ToString()) * 9.5;
                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 9.5);
                            dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                            dTotal = dTotal + dTotal1 + dTotal2;
                        }
                        else
                        {
                            string strEnd = ETime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 18:30:00";
                            string strEnd1 = RTime.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 18:30:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減
                            Total1 = RTime.Subtract(RTime1); //日期相減
                            dTotal1 = Convert.ToDouble(Total.TotalHours.ToString());
                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 9.5);
                            //dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                            dTotal = dTotal + dTotal1;
                        }


                    }
                    else
                    {
                        if (ETime > RTime)
                        {
                            DateTime EndTime = ETime.AddDays(1);
                            string strEnd = EndTime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 09:00:00";
                            DateTime RTime2 = RTime.AddDays(1); //結束日
                            string strEnd1 = RTime2.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 09:00:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減
                            Total1 = RTime.Subtract(RTime1); //日期相減

                            //if (Convert.ToDouble(Total.Days.ToString()) <0)
                            //    dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) - 1) * 14.5);    
                            //else
                            //    dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) + 1) * 14.5);    
                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 14.5);

                            if ((Convert.ToInt32(RTime.Hour.ToString()) >= 9) && (Convert.ToInt32(RTime.Hour.ToString()) <= 18))
                            {
                                if (Convert.ToInt32(RTime.Hour.ToString()) == 18)
                                {
                                    if ((Convert.ToInt32(RTime.Minute.ToString()) >= 0) && (Convert.ToInt32(RTime.Minute.ToString()) < 30))
                                    {
                                        if (Convert.ToDouble(Total.Days.ToString()) < 0)
                                            dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) - 1) * 14.5);
                                        else
                                            dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) + 1) * 14.5);
                                        //dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                                        dTotal = dTotal + dTotal1;
                                    }
                                    else
                                    {

                                        dTotal1 = ((Convert.ToDouble(Total.Days.ToString())) * 14.5);

                                        dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                                        dTotal = dTotal + dTotal1 + dTotal2;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDouble(Total.Days.ToString()) < 0)
                                        dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) - 1) * 14.5);
                                    else
                                        dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) + 1) * 14.5);
                                    dTotal = dTotal + dTotal1;

                                }


                            }
                            else
                            {
                                dTotal1 = ((Convert.ToDouble(Total.Days.ToString())) * 14.5);

                                dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                                dTotal = dTotal + dTotal1 + dTotal2;
                            }
                        }
                        else
                        {
                            DateTime EndTime = ETime.AddDays(1);
                            string strEnd = EndTime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 09:00:00";
                            DateTime RTime2 = RTime.AddDays(1); //結束日
                            string strEnd1 = RTime2.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 09:00:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減

                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 14.5) + Convert.ToDouble(Total.TotalHours.ToString());


                        }

                    }
                }
                else
                {
                    if (dt.Rows[j]["Period"].ToString() == "D")
                        dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 9.5);
                    else
                        dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 14.5);

                }

                dr["Days"] = dTotal.ToString("#0.0");

                double dPrice;
                if (dt.Rows[j]["price_use"].ToString() != "")
                    dPrice = Convert.ToDouble(dt.Rows[j]["price_use"].ToString());
                else
                    dPrice = 0;
                //dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1));
                dPrice = dTotal * dPrice;

                dr["Price"] = dPrice.ToString("#0");

                dt_new1.Rows.Add(dr);

                intUseTime = 0;
                intBorrowingTimes = 0;
            }
            else
            {
                //if (strGName != dt.Rows[j + 1]["Department"].ToString())
                //{
                intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                intBorrowingTimes = intBorrowingTimes + 1;

                DataRow dr = dt_new1.NewRow();
                dr["ID"] = dt.Rows[j]["ID"].ToString();
                dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                dr["Name"] = dt.Rows[j]["Name"].ToString();
                dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                dr["GName"] = dt.Rows[j]["GName"].ToString();
                dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                dr["Department"] = dt.Rows[j]["Department"].ToString();
                //if (intUseTime == 0)
                //{
                dr["UseTime"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) + 1).ToString();
                dr["UsePercent"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) / intUseTotal).ToString("#0.00");
                intUseTime = Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                //}
                //else
                //{
                //    dr["UseTime"] = intUseTime;
                //    dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(intUseTotal)) * 100;
                //    dr["UsePercent"] = dTotal.ToString("#0.00") + " %";
                //}
                dr["BorrowingTimes"] = intBorrowingTimes;
                dTotal = (Convert.ToDouble(intBorrowingTimes) / Convert.ToDouble(intBorrowTotal)) * 100;
                dr["BorrowingPercent"] = dTotal.ToString("#0.00") + " %";
                //dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                int intIndex, intIndex1;
                intIndex = dt.Rows[j]["Customer"].ToString().IndexOf("(");
                if (intIndex < 0)
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                else
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(0, intIndex);
                    //dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(1, intIndex - 1);

                string strDepartment2;
                //int intIndex, intIndex1;

                intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
                intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
                if (intIndex > 0)
                    strDepartment2 = dt.Rows[j]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                else
                    strDepartment2 = dt.Rows[j]["Department"].ToString();

                dr["Department1"] = strDepartment2;


                string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
                int intU = 0;
                foreach (string l in sArray)
                {
                    intU++;
                }
                if (intU == 2)
                    dr["PU"] = sArray[1].Replace("PU", "");
                else
                    dr["PU"] = sArray[0].Replace("PU", "");

                string strDate1, strDate2;
                DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());
                if (dt_Rang < Convert.ToDateTime(strStartDate))
                {
                    strDate1 = strStartDate1;
                }
                else
                    strDate1 = dt_Rang.ToString("yyyy/MM/dd");

                if (Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString()).Year.ToString() == "1900")
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());

                }
                else
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString());
                }
                //dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());
                strDate2 = dt_Rang.ToString("yyyy/MM/dd");

                if (dt_Rang > Convert.ToDateTime(strEndDate))
                    dr["DateRang"] = strDate1 + "~" + strEndDate1;
                else
                    dr["DateRang"] = strDate1 + "~" + strDate2;

                //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
                //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
                //TimeSpan Total = ETime.Subtract(STime); //日期相減

                //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
                //dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(24));
                //if (dt.Rows[j]["Period"].ToString() == "D")
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 9.5);
                //else
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 14.5);
                DateTime STime = DateTime.Parse(dt.Rows[j]["StartDate"].ToString()); //起始日
                DateTime ETime = DateTime.Parse(dt.Rows[j]["EndDate"].ToString()); //起始日
                DateTime RTime = DateTime.Parse(dt.Rows[j]["ReturnDate"].ToString()); //結束日

                if (Convert.ToDateTime(strStartDate) > STime)
                {
                    STime = DateTime.Parse(strStartDate); //起始日
                }

                if (Convert.ToDateTime(strEndDate) < ETime)
                {
                    ETime = DateTime.Parse(strEndDate); //起始日
                }


                TimeSpan Total, Total1;
                double dTotal1, dTotal2;

                if (RTime.Year.ToString() != "1900")
                {
                    if (dt.Rows[j]["Period"].ToString() == "D")
                    {

                        if (ETime > RTime)
                        {
                            string strEnd = ETime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 18:30:00";
                            string strEnd1 = RTime.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 18:30:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減
                            Total1 = RTime.Subtract(RTime1); //日期相減
                            dTotal1 = Convert.ToDouble(Total.Days.ToString()) * 9.5;
                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 9.5);
                            dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                            dTotal = dTotal + dTotal1 + dTotal2;
                        }
                        else
                        {
                            string strEnd = ETime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 18:30:00";
                            string strEnd1 = RTime.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 18:30:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減
                            Total1 = RTime.Subtract(RTime1); //日期相減
                            dTotal1 = Convert.ToDouble(Total.TotalHours.ToString());
                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 9.5);
                            //dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                            dTotal = dTotal + dTotal1;
                        }


                    }
                    else
                    {
                        if (ETime > RTime)
                        {
                            DateTime EndTime = ETime.AddDays(1);
                            string strEnd = EndTime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 09:00:00";
                            DateTime RTime2 = RTime.AddDays(1); //結束日
                            string strEnd1 = RTime2.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 09:00:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減
                            Total1 = RTime.Subtract(RTime1); //日期相減

                            //if (Convert.ToDouble(Total.Days.ToString()) <0)
                            //    dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) - 1) * 14.5);    
                            //else
                            //    dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) + 1) * 14.5);    
                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 14.5);

                            if ((Convert.ToInt32(RTime.Hour.ToString()) >= 9) && (Convert.ToInt32(RTime.Hour.ToString()) <= 18))
                            {
                                if (Convert.ToInt32(RTime.Hour.ToString()) == 18)
                                {
                                    if ((Convert.ToInt32(RTime.Minute.ToString()) >= 0) && (Convert.ToInt32(RTime.Minute.ToString()) < 30))
                                    {
                                        if (Convert.ToDouble(Total.Days.ToString()) < 0)
                                            dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) - 1) * 14.5);
                                        else
                                            dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) + 1) * 14.5);
                                        //dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                                        dTotal = dTotal + dTotal1;
                                    }
                                    else
                                    {

                                        dTotal1 = ((Convert.ToDouble(Total.Days.ToString())) * 14.5);

                                        dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                                        dTotal = dTotal + dTotal1 + dTotal2;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDouble(Total.Days.ToString()) < 0)
                                        dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) - 1) * 14.5);
                                    else
                                        dTotal1 = ((Convert.ToDouble(Total.Days.ToString()) + 1) * 14.5);
                                    dTotal = dTotal + dTotal1;

                                }


                            }
                            else
                            {
                                dTotal1 = ((Convert.ToDouble(Total.Days.ToString())) * 14.5);

                                dTotal2 = Convert.ToDouble(Total1.TotalHours.ToString());
                                dTotal = dTotal + dTotal1 + dTotal2;
                            }
                        }
                        else
                        {
                            DateTime EndTime = ETime.AddDays(1);
                            string strEnd = EndTime.ToString("yyyy/MM/dd");
                            strEnd = strEnd + " 09:00:00";
                            DateTime RTime2 = RTime.AddDays(1); //結束日
                            string strEnd1 = RTime2.ToString("yyyy/MM/dd");
                            strEnd1 = strEnd1 + " 09:00:00";
                            DateTime ETime1 = Convert.ToDateTime(strEnd);
                            DateTime RTime1 = Convert.ToDateTime(strEnd1);
                            Total = RTime.Subtract(ETime1); //日期相減

                            dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 14.5) + Convert.ToDouble(Total.TotalHours.ToString());


                        }

                    }
                }
                else
                {
                    if (dt.Rows[j]["Period"].ToString() == "D")
                        dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 9.5);
                    else
                        dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1) * 14.5);

                }

                dr["Days"] = dTotal.ToString("#0.0");

                double dPrice;
                if (dt.Rows[j]["price_use"].ToString() != "")
                    dPrice = Convert.ToDouble(dt.Rows[j]["price_use"].ToString());
                else
                    dPrice = 0;
                //dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1));
                dPrice = dTotal * dPrice;

                dr["Price"] = dPrice.ToString("#0");

                dt_new1.Rows.Add(dr);
                //strGName = dt.Rows[i]["Products_ID"].ToString();
                intUseTime = 0;
                intBorrowingTimes = 0;
                //}
                //else
                //{
                //    intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                //    intBorrowingTimes = intBorrowingTimes + 1;
                //}
            }
            //intUseTotal = intUseTotal + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
            //intBorrowTotal = intBorrowTotal + 1;

        }
        //}
        //}
        //else
        //{
        //    intUseTotal = 0;
        //    intBorrowTotal = 0;
        //    DataTable dt = clsData.UploadApparatusReportDep(strStartDate, strEndDate, strDepartment, "0");
        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {

        //        strGName = dt.Rows[j]["Products_ID"].ToString();

        //        if (j == dt.Rows.Count - 1)
        //        {
        //            DataRow dr = dt_new1.NewRow();
        //            dr["ID"] = dt.Rows[j]["ID"].ToString();
        //            dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
        //            dr["Name"] = dt.Rows[j]["Name"].ToString();
        //            dr["Mission"] = dt.Rows[j]["Mission"].ToString();
        //            dr["GName"] = dt.Rows[j]["GName"].ToString();
        //            dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
        //            dr["Department"] = dt.Rows[j]["Department"].ToString();
        //            if (intUseTime == 0)
        //                dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
        //            else
        //                dr["UseTime"] = intUseTime;
        //            dr["BorrowingTimes"] = intBorrowingTimes;
        //            dr["Customer"] = dt.Rows[j]["Customer"].ToString();

        //            string strDepartment2;
        //            int intIndex, intIndex1;

        //            intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
        //            intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
        //            strDepartment2 = dt.Rows[j]["Department"].ToString();

        //            dr["Department1"] = strDepartment2;


        //            string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
        //            int intU = 0;
        //            foreach (string i in sArray)
        //            {
        //                intU++;
        //            }
        //            if (intU == 2)
        //                dr["PU"] = sArray[1];
        //            else
        //                dr["PU"] = sArray[0];

        //            string strDate1, strDate2;
        //            DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());
        //            if (dt_Rang < Convert.ToDateTime(strStartDate))
        //            {
        //                strDate1 = strStartDate;
        //            }
        //            else
        //                strDate1 = dt_Rang.ToString("yyyy/MM/dd");

        //            dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());
        //            strDate2 = dt_Rang.ToString("yyyy/MM/dd");

        //            if (dt_Rang > Convert.ToDateTime(strEndDate))
        //                dr["DateRang"] = strDate1 + "~" + strEndDate;
        //            else
        //                dr["DateRang"] = strDate1 + "~" + strDate2;

        //            //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
        //            //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
        //            //TimeSpan Total = ETime.Subtract(STime); //日期相減

        //            //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
        //            dr["Days"] = (intUseTime / 24).ToString();

        //            dt_new1.Rows.Add(dr);
        //        }
        //        else
        //        {
        //            if (strGName != dt.Rows[j + 1]["Products_ID"].ToString())
        //            {
        //                DataRow dr = dt_new1.NewRow();
        //                dr["ID"] = dt.Rows[j]["ID"].ToString();
        //                dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
        //                dr["Name"] = dt.Rows[j]["Name"].ToString();
        //                dr["Mission"] = dt.Rows[j]["Mission"].ToString();
        //                dr["GName"] = dt.Rows[j]["GName"].ToString();
        //                dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
        //                dr["Department"] = dt.Rows[j]["Department"].ToString();
        //                if (intUseTime == 0)
        //                    dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
        //                else
        //                    dr["UseTime"] = intUseTime;
        //                dr["BorrowingTimes"] = intBorrowingTimes;
        //                dr["Customer"] = dt.Rows[j]["Customer"].ToString();

        //                string strDepartment2;
        //                int intIndex, intIndex1;

        //                intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
        //                intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
        //                strDepartment2 = dt.Rows[j]["Department"].ToString();

        //                dr["Department1"] = strDepartment2;


        //                string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
        //                int intU = 0;
        //                foreach (string i in sArray)
        //                {
        //                    intU++;
        //                }
        //                if (intU == 2)
        //                    dr["PU"] = sArray[1];
        //                else
        //                    dr["PU"] = sArray[0];

        //                string strDate1, strDate2;
        //                DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());
        //                if (dt_Rang < Convert.ToDateTime(strStartDate))
        //                {
        //                    strDate1 = strStartDate;
        //                }
        //                else
        //                    strDate1 = dt_Rang.ToString("yyyy/MM/dd");

        //                dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());
        //                strDate2 = dt_Rang.ToString("yyyy/MM/dd");

        //                if (dt_Rang > Convert.ToDateTime(strEndDate))
        //                    dr["DateRang"] = strDate1 + "~" + strEndDate;
        //                else
        //                    dr["DateRang"] = strDate1 + "~" + strDate2;

        //                //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
        //                //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
        //                //TimeSpan Total = ETime.Subtract(STime); //日期相減

        //                //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
        //                dr["Days"] = (intUseTime / 24).ToString();

        //                dt_new1.Rows.Add(dr);
        //                //strGName = dt.Rows[i]["Products_ID"].ToString();
        //                intUseTime = 0;
        //                intBorrowingTimes = 1;
        //            }
        //            else
        //            {
        //                intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
        //                intBorrowingTimes = intBorrowingTimes + 1;
        //            }
        //        }
        //        intUseTotal = intUseTotal + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
        //        intBorrowTotal = intBorrowTotal + 1;

        //    }
        //}

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_ProductReport2.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt_new1);
        rptDoc.SetDataSource(ds);
        rptDoc.SetParameterValue("StartDate", Session["RDateS"].ToString());
        rptDoc.SetParameterValue("EndDate", Session["RDateE"].ToString());
        //rptDoc.SetParameterValue("UseTotal", intUseTotal);
        //rptDoc.SetParameterValue("BorrowTotal", intBorrowTotal);


        if (dt2.Rows.Count == 0)
        {
            rptDoc.SetParameterValue("ProductsID", "");
            rptDoc.SetParameterValue("ProductsName", "");
            clsMsg.AlertMessage("找不到資料！", this.Page);
        }
        else
        {
            rptDoc.SetParameterValue("ProductsID", dt2.Rows[0]["Products_ID"].ToString());
            rptDoc.SetParameterValue("ProductsName", dt2.Rows[0]["Name"].ToString());
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.DataBind();
        }
    }
    #endregion

    #region CalculationByHour
    private void CalculationByHour(DataTable dt_new1, double dTotal, string strPID, string strStartDate, string strEndDate, string strEndDate1, string strStartDate1)
    {
        int intUseTime, intJ, intBorrowingTimes, intUseTotal, intBorrowTotal;
        string strGName;
        strGName = "";
        intUseTime = 0;
        intBorrowingTimes = 0;
        intUseTotal = 0;
        intBorrowTotal = 0;
        //if (Session["RDep"].ToString() == "ALL")
        //{

        DataTable dt2 = clsData.UploadProductReport_ByHour(strStartDate, strEndDate, strPID, "1", Session["RLocal"].ToString());
        intBorrowTotal = Convert.ToInt32(dt2.Rows[0]["tcount"].ToString());

        dt2 = clsData.UploadProductReport_ByHour(strStartDate, strEndDate, strPID, "0", Session["RLocal"].ToString());
        for (int x = 0; x < dt2.Rows.Count; x++)
        {
            intUseTotal = intUseTotal + Convert.ToInt32(dt2.Rows[x]["UseTime"].ToString());
        }


        //DataTable dt1 = clsData.UploadDepartment();

        //for (int i = 0; i < dt1.Rows.Count; i++)
        //{
        DataTable dt = clsData.UploadProductReport_ByHour(strStartDate, strEndDate, strPID, "0", Session["RLocal"].ToString());
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            //if (dt.Rows[j]["Department"].ToString() == "D210")
            //    strStartDate1 = "0";
            strGName = dt.Rows[j]["Department"].ToString();

            if (j == dt.Rows.Count - 1)
            {
                intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                intBorrowingTimes = intBorrowingTimes + 1;

                DataRow dr = dt_new1.NewRow();
                dr["ID"] = dt.Rows[j]["ID"].ToString();
                dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                dr["Name"] = dt.Rows[j]["Name"].ToString();
                dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                dr["GName"] = dt.Rows[j]["GName"].ToString();
                dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                dr["Department"] = dt.Rows[j]["Department"].ToString();
                //if (intUseTime == 0)
                //{
                dr["UseTime"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) + 1).ToString();
                dr["UsePercent"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) / intUseTotal).ToString("#0.00");
                intUseTime = Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                //}
                //else
                //{
                //    dr["UseTime"] = intUseTime;
                //    dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(intUseTotal)) * 100;
                //    dr["UsePercent"] = dTotal.ToString("#0.00") + " %";
                //}
                dr["BorrowingTimes"] = intBorrowingTimes;
                dTotal = (Convert.ToDouble(intBorrowingTimes) / Convert.ToDouble(intBorrowTotal)) * 100;
                dr["BorrowingPercent"] = dTotal.ToString("#0.00") + " %";
                //dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                int intIndex, intIndex1;
                intIndex = dt.Rows[j]["Customer"].ToString().IndexOf("(");
                if (intIndex < 0)
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                else
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(0, intIndex);
                    //dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(1, intIndex - 1);

                string strDepartment2;
                //int intIndex, intIndex1;

                intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
                intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
                if (intIndex > 0)
                    strDepartment2 = dt.Rows[j]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                else
                    strDepartment2 = dt.Rows[j]["Department"].ToString();

                dr["Department1"] = strDepartment2;


                string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
                int intU = 0;
                foreach (string l in sArray)
                {
                    intU++;
                }
                if (intU == 2)
                    dr["PU"] = sArray[1].Replace("PU", "");
                else
                    dr["PU"] = sArray[0].Replace("PU", "");

                string strDate1, strDate2;
                DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());

                if (dt_Rang < Convert.ToDateTime(strStartDate))
                {
                    strDate1 = strStartDate1;
                }
                else
                    strDate1 = dt_Rang.ToString("yyyy/MM/dd");

                if (Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString()).Year.ToString() == "1900")
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());

                }
                else
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString());
                }
                //dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());

                strDate2 = dt_Rang.ToString("yyyy/MM/dd");
                if (dt_Rang > Convert.ToDateTime(strEndDate))
                    dr["DateRang"] = strDate1 + "~" + strEndDate1;
                else
                    dr["DateRang"] = strDate1 + "~" + strDate2;

                //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
                //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
                //TimeSpan Total = ETime.Subtract(STime); //日期相減

                //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
                //dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(24));
                //if (dt.Rows[j]["Period"].ToString() == "D")
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 9.5);
                //else
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 14.5);

                DateTime STime = DateTime.Parse(dt.Rows[j]["StartDate"].ToString()); //起始日
                DateTime ETime = DateTime.Parse(dt.Rows[j]["EndDate"].ToString()); //起始日
                DateTime RTime = DateTime.Parse(dt.Rows[j]["ReturnDate"].ToString()); //結束日

                if (Convert.ToDateTime(strStartDate) > STime)
                {
                    STime = DateTime.Parse(strStartDate); //起始日
                }

                if (Convert.ToDateTime(strEndDate) < ETime)
                {
                    ETime = DateTime.Parse(strEndDate); //起始日
                }


                TimeSpan Total, Total1;
                double dTotal1, dTotal2;

                if (RTime.Year.ToString() != "1900")
                {
                    Total = RTime.Subtract(ETime); //日期相減
                    dTotal1 = Convert.ToDouble(Total.TotalHours.ToString());
                    dTotal = (Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()));
                    dTotal = dTotal + dTotal1;
                }
                else
                {
                    dTotal = (Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()));
                }


                dr["Days"] = dTotal.ToString("#0.0");

                double dPrice;
                if (dt.Rows[j]["price_use"].ToString() != "")
                    dPrice = Convert.ToDouble(dt.Rows[j]["price_use"].ToString());
                else
                    dPrice = 0;
                //dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1));
                dPrice = dTotal * dPrice;

                dr["Price"] = dPrice.ToString("#0");

                dt_new1.Rows.Add(dr);

                intUseTime = 0;
                intBorrowingTimes = 0;
            }
            else
            {
                //if (strGName != dt.Rows[j + 1]["Department"].ToString())
                //{
                intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                intBorrowingTimes = intBorrowingTimes + 1;

                DataRow dr = dt_new1.NewRow();
                dr["ID"] = dt.Rows[j]["ID"].ToString();
                dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                dr["Name"] = dt.Rows[j]["Name"].ToString();
                dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                dr["GName"] = dt.Rows[j]["GName"].ToString();
                dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                dr["Department"] = dt.Rows[j]["Department"].ToString();
                //if (intUseTime == 0)
                //{
                dr["UseTime"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) + 1).ToString();
                dr["UsePercent"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) / intUseTotal).ToString("#0.00");
                intUseTime = Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                //}
                //else
                //{
                //    dr["UseTime"] = intUseTime;
                //    dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(intUseTotal)) * 100;
                //    dr["UsePercent"] = dTotal.ToString("#0.00") + " %";
                //}
                dr["BorrowingTimes"] = intBorrowingTimes;
                dTotal = (Convert.ToDouble(intBorrowingTimes) / Convert.ToDouble(intBorrowTotal)) * 100;
                dr["BorrowingPercent"] = dTotal.ToString("#0.00") + " %";
                //dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                int intIndex, intIndex1;
                intIndex = dt.Rows[j]["Customer"].ToString().IndexOf("(");
                if (intIndex < 0)
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString();
                else
                    dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(0, intIndex);
                    //dr["Customer"] = dt.Rows[j]["Customer"].ToString().Substring(1, intIndex - 1);

                string strDepartment2;
                //int intIndex, intIndex1;

                intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
                intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
                if (intIndex > 0)
                    strDepartment2 = dt.Rows[j]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                else
                    strDepartment2 = dt.Rows[j]["Department"].ToString();

                dr["Department1"] = strDepartment2;


                string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
                int intU = 0;
                foreach (string l in sArray)
                {
                    intU++;
                }
                if (intU == 2)
                    dr["PU"] = sArray[1].Replace("PU", "");
                else
                    dr["PU"] = sArray[0].Replace("PU", "");

                string strDate1, strDate2;
                DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());
                if (dt_Rang < Convert.ToDateTime(strStartDate))
                {
                    strDate1 = strStartDate1;
                }
                else
                    strDate1 = dt_Rang.ToString("yyyy/MM/dd");

                if (Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString()).Year.ToString() == "1900")
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());

                }
                else
                {
                    dt_Rang = Convert.ToDateTime(dt.Rows[j]["ReturnDate"].ToString());
                }
                //dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());
                strDate2 = dt_Rang.ToString("yyyy/MM/dd");

                if (dt_Rang > Convert.ToDateTime(strEndDate))
                    dr["DateRang"] = strDate1 + "~" + strEndDate1;
                else
                    dr["DateRang"] = strDate1 + "~" + strDate2;

                //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
                //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
                //TimeSpan Total = ETime.Subtract(STime); //日期相減

                //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
                //dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(24));
                //if (dt.Rows[j]["Period"].ToString() == "D")
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 9.5);
                //else
                //    dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString())+1) * 14.5);
                DateTime STime = DateTime.Parse(dt.Rows[j]["StartDate"].ToString()); //起始日
                DateTime ETime = DateTime.Parse(dt.Rows[j]["EndDate"].ToString()); //起始日
                DateTime RTime = DateTime.Parse(dt.Rows[j]["ReturnDate"].ToString()); //結束日

                if (Convert.ToDateTime(strStartDate) > STime)
                {
                    STime = DateTime.Parse(strStartDate); //起始日
                }

                if (Convert.ToDateTime(strEndDate) < ETime)
                {
                    ETime = DateTime.Parse(strEndDate); //起始日
                }


                TimeSpan Total, Total1;
                double dTotal1, dTotal2;

                if (RTime.Year.ToString() != "1900")
                {
                    Total = RTime.Subtract(ETime); //日期相減
                    dTotal1 = Convert.ToDouble(Total.TotalHours.ToString());
                    dTotal = (Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()));
                    dTotal = dTotal + dTotal1;
                }
                else
                {
                    dTotal = (Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()));
                }


                dr["Days"] = dTotal.ToString("#0.0");

                double dPrice;
                if (dt.Rows[j]["price_use"].ToString() != "")
                    dPrice = Convert.ToDouble(dt.Rows[j]["price_use"].ToString());
                else
                    dPrice = 0;
                //dTotal = ((Convert.ToDouble(dt.Rows[j]["UseTime"].ToString()) + 1));
                dPrice = dTotal * dPrice;

                dr["Price"] = dPrice.ToString("#0");

                dt_new1.Rows.Add(dr);
                //strGName = dt.Rows[i]["Products_ID"].ToString();
                intUseTime = 0;
                intBorrowingTimes = 0;
                //}
                //else
                //{
                //    intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                //    intBorrowingTimes = intBorrowingTimes + 1;
                //}
            }
            //intUseTotal = intUseTotal + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
            //intBorrowTotal = intBorrowTotal + 1;

        }
        //}
        //}
        //else
        //{
        //    intUseTotal = 0;
        //    intBorrowTotal = 0;
        //    DataTable dt = clsData.UploadApparatusReportDep(strStartDate, strEndDate, strDepartment, "0");
        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {

        //        strGName = dt.Rows[j]["Products_ID"].ToString();

        //        if (j == dt.Rows.Count - 1)
        //        {
        //            DataRow dr = dt_new1.NewRow();
        //            dr["ID"] = dt.Rows[j]["ID"].ToString();
        //            dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
        //            dr["Name"] = dt.Rows[j]["Name"].ToString();
        //            dr["Mission"] = dt.Rows[j]["Mission"].ToString();
        //            dr["GName"] = dt.Rows[j]["GName"].ToString();
        //            dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
        //            dr["Department"] = dt.Rows[j]["Department"].ToString();
        //            if (intUseTime == 0)
        //                dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
        //            else
        //                dr["UseTime"] = intUseTime;
        //            dr["BorrowingTimes"] = intBorrowingTimes;
        //            dr["Customer"] = dt.Rows[j]["Customer"].ToString();

        //            string strDepartment2;
        //            int intIndex, intIndex1;

        //            intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
        //            intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
        //            strDepartment2 = dt.Rows[j]["Department"].ToString();

        //            dr["Department1"] = strDepartment2;


        //            string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
        //            int intU = 0;
        //            foreach (string i in sArray)
        //            {
        //                intU++;
        //            }
        //            if (intU == 2)
        //                dr["PU"] = sArray[1];
        //            else
        //                dr["PU"] = sArray[0];

        //            string strDate1, strDate2;
        //            DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());
        //            if (dt_Rang < Convert.ToDateTime(strStartDate))
        //            {
        //                strDate1 = strStartDate;
        //            }
        //            else
        //                strDate1 = dt_Rang.ToString("yyyy/MM/dd");

        //            dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());
        //            strDate2 = dt_Rang.ToString("yyyy/MM/dd");

        //            if (dt_Rang > Convert.ToDateTime(strEndDate))
        //                dr["DateRang"] = strDate1 + "~" + strEndDate;
        //            else
        //                dr["DateRang"] = strDate1 + "~" + strDate2;

        //            //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
        //            //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
        //            //TimeSpan Total = ETime.Subtract(STime); //日期相減

        //            //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
        //            dr["Days"] = (intUseTime / 24).ToString();

        //            dt_new1.Rows.Add(dr);
        //        }
        //        else
        //        {
        //            if (strGName != dt.Rows[j + 1]["Products_ID"].ToString())
        //            {
        //                DataRow dr = dt_new1.NewRow();
        //                dr["ID"] = dt.Rows[j]["ID"].ToString();
        //                dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
        //                dr["Name"] = dt.Rows[j]["Name"].ToString();
        //                dr["Mission"] = dt.Rows[j]["Mission"].ToString();
        //                dr["GName"] = dt.Rows[j]["GName"].ToString();
        //                dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
        //                dr["Department"] = dt.Rows[j]["Department"].ToString();
        //                if (intUseTime == 0)
        //                    dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
        //                else
        //                    dr["UseTime"] = intUseTime;
        //                dr["BorrowingTimes"] = intBorrowingTimes;
        //                dr["Customer"] = dt.Rows[j]["Customer"].ToString();

        //                string strDepartment2;
        //                int intIndex, intIndex1;

        //                intIndex = dt.Rows[j]["Department"].ToString().IndexOf("(");
        //                intIndex1 = dt.Rows[j]["Department"].ToString().IndexOf(")");
        //                strDepartment2 = dt.Rows[j]["Department"].ToString();

        //                dr["Department1"] = strDepartment2;


        //                string[] sArray = dt.Rows[j]["Department"].ToString().Split('-');
        //                int intU = 0;
        //                foreach (string i in sArray)
        //                {
        //                    intU++;
        //                }
        //                if (intU == 2)
        //                    dr["PU"] = sArray[1];
        //                else
        //                    dr["PU"] = sArray[0];

        //                string strDate1, strDate2;
        //                DateTime dt_Rang = Convert.ToDateTime(dt.Rows[j]["StartDate"].ToString());
        //                if (dt_Rang < Convert.ToDateTime(strStartDate))
        //                {
        //                    strDate1 = strStartDate;
        //                }
        //                else
        //                    strDate1 = dt_Rang.ToString("yyyy/MM/dd");

        //                dt_Rang = Convert.ToDateTime(dt.Rows[j]["EndDate"].ToString());
        //                strDate2 = dt_Rang.ToString("yyyy/MM/dd");

        //                if (dt_Rang > Convert.ToDateTime(strEndDate))
        //                    dr["DateRang"] = strDate1 + "~" + strEndDate;
        //                else
        //                    dr["DateRang"] = strDate1 + "~" + strDate2;

        //                //DateTime STime = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()); //起始日
        //                //DateTime ETime = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()); //結束日
        //                //TimeSpan Total = ETime.Subtract(STime); //日期相減

        //                //dr["Days"] = ((int.Parse(Total.Minutes.ToString()) / 60) / 24).ToString() ;
        //                dr["Days"] = (intUseTime / 24).ToString();

        //                dt_new1.Rows.Add(dr);
        //                //strGName = dt.Rows[i]["Products_ID"].ToString();
        //                intUseTime = 0;
        //                intBorrowingTimes = 1;
        //            }
        //            else
        //            {
        //                intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
        //                intBorrowingTimes = intBorrowingTimes + 1;
        //            }
        //        }
        //        intUseTotal = intUseTotal + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
        //        intBorrowTotal = intBorrowTotal + 1;

        //    }
        //}

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_ProductReport2.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt_new1);
        rptDoc.SetDataSource(ds);
        rptDoc.SetParameterValue("StartDate", Session["RDateS"].ToString());
        rptDoc.SetParameterValue("EndDate", Session["RDateE"].ToString());
        //rptDoc.SetParameterValue("UseTotal", intUseTotal);
        //rptDoc.SetParameterValue("BorrowTotal", intBorrowTotal);


        if (dt2.Rows.Count == 0)
        {
            rptDoc.SetParameterValue("ProductsID", "");
            rptDoc.SetParameterValue("ProductsName", "");
            clsMsg.AlertMessage("找不到資料！", this.Page);
        }
        else
        {
            rptDoc.SetParameterValue("ProductsID", dt2.Rows[0]["Products_ID"].ToString());
            rptDoc.SetParameterValue("ProductsName", dt2.Rows[0]["Name"].ToString());
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.DataBind();
        }
    }
    #endregion
}
