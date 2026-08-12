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
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Text;

public partial class WebForm_UploadReservation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            Session["Upload_Kind"] = "Reservation";
        }
    }

    protected void butOK_Click(object sender, EventArgs e)
    {

        ConvertToSQL();
        string Upload_Directory = @"d:\Reservation\";
        if (File.Exists(Upload_Directory))
            Directory.Delete(Upload_Directory, true);
    }

    private void ConvertToSQL()
    {
        string strPath = "";
        string strFile = "";
        string strFile_Name = "";
        int intFile;
        int intW, intW1;
        int intI;
        string strApparatus_ID = "", strStartDate = "", strEndDate = "", strBorrower = "", strDepartment = "", strExt = "", strEmail = "", strMission = "";
        string strGName = "", strReturnDate = "", strStatus = "E", strProject_ID = "", strContinuousDate = "", strBorrowedQuantity = "", strAgent = "", strAgentExt = "", strAgentEmail = "";
        string strCustomer = "", strApparatus_Price = "", strContinuousCount = "", strCustodian_Check = "Y", strAdmin_Check = "Y", strPeriod = "D", strUseKind = "";
        string strTime = "", strProduct_ID = "", strNote;
        DataTable dtInfo;
        string strDepartment1;

            if (Session["FileN"] != null)
            {
                strFile = Session["FileN"].ToString();


                if ((strFile != null) || (strFile != ""))
                {
                    string[] sArray = strFile.Split(',');
                    foreach (string i in sArray)
                    {
                        if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                        {
                            intFile = i.LastIndexOf('\\');
                            strPath = i.Substring(0, intFile);
                            strFile_Name = i.Substring(intFile + 1);
                        }
                    }

                    string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + "/" + strFile_Name + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
                    //string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Reservation\Book;Extended Properties='Excel 12.0 Xml;HDR=YES'";
                    OleDbConnection objConn = new OleDbConnection(strConn);

                    objConn.Open();
                    //objConn.Close();
                    try
                    {
                        int intI1 = 0;
                        string strSheetName = "";
                        DataRow[] sheetList = objConn.GetSchema("Tables").Select();
                        foreach (DataRow sheet in sheetList)
                        {
                            strSheetName = sheet["TABLE_NAME"].ToString();

                            if (!strSheetName.Contains("FilterDatabase"))
                            {
                                intW = 0;
                                string strExcel = "";
                                strExcel = "select * from [" + strSheetName + "]";
                                OleDbDataAdapter myCommand = null;
                                DataSet ds = null;

                                myCommand = new OleDbDataAdapter(strExcel, strConn);
                                ds = new DataSet();
                                myCommand.Fill(ds, "table2");

                                DataTable dt4 = ds.Tables["table2"];
                                if (dt4.Rows.Count != 0)
                                {
                                    strProduct_ID = dt4.Rows[0][5].ToString();
                                    dtInfo = clsData.UploadApparatusQuery(strProduct_ID, "0", "");
                                    if (dtInfo.Rows.Count > 0)
                                    {
                                        strApparatus_ID = dtInfo.Rows[0]["ID"].ToString();
                                        strApparatus_Price = dtInfo.Rows[0]["Price_Use"].ToString();

                                        DateTime thisDate = new DateTime();
                                        thisDate = Convert.ToDateTime(dt4.Rows[0][0].ToString());
                                        string strDay1 = thisDate.ToString("yyyy/MM/dd");
                                        string strDay2 = thisDate.ToString("yyyy/MM");
                                        string strDay3 = thisDate.AddMonths(1).ToString("yyyy/MM");
                                        string strMon1 = strDay2 + "/01";
                                        string strMon2 = strDay3 + "/01";

                                        clsTransaction.DelReservation_Date(strMon1, strMon2, strApparatus_ID);
                                    }
                                    else
                                    {
                                        objConn.Close();
                                        clsMsg.AlertMessage("系統未搜尋到此設備-財產編號:" + strProduct_ID , this.Page);
                                    }
                                }


                                for (intI = 0; intI < dt4.Rows.Count; intI++)
                                {
                                    if ((dt4.Rows[intI][0].ToString() != "") && (dt4.Rows[intI][1].ToString() != ""))
                                    {
                                        DateTime thisDate1 = new DateTime();
                                        thisDate1 = Convert.ToDateTime(dt4.Rows[intI][0].ToString().Trim());
                                        string strDay = thisDate1.ToString("yyyy/MM/dd");

                                        strTime = dt4.Rows[intI][7].ToString();
                                        if (dt4.Rows[intI][6].ToString().Trim() != "")
                                        {
                                            string[] sTime = strTime.Split('~');
                                            strStartDate = strDay + " " + sTime[0].Trim();
                                            if (sTime[1].Trim() == "24:00")
                                                strEndDate = strDay + " 23:59";
                                            else
                                                strEndDate = strDay + " " + sTime[1].Trim();
                                        }
                                        strDepartment = dt4.Rows[intI][1].ToString().Trim().Replace(" ","");
                                        
                                        strDepartment1 = checkDepartment(strDepartment);
                                        if (strDepartment1 != "")
                                        {
                                            strGName = dt4.Rows[intI][3].ToString().Trim();

                                            strCustomer = dt4.Rows[intI][2].ToString().Trim().PadLeft(3, '0');
                                            dtInfo = clsData.Customer(strCustomer);
                                            if (dtInfo.Rows.Count > 0)
                                            {
                                                strCustomer = dtInfo.Rows[0]["Name"].ToString().Trim();
                                                strBorrower = dt4.Rows[intI][6].ToString().Trim();
                                                strNote = dt4.Rows[intI][8].ToString().Trim();
                                                strUseKind = dt4.Rows[intI][9].ToString().Trim();
                                                if (strUseKind == "Manual")
                                                    strUseKind = "M";
                                                if (strUseKind == "Auto")
                                                    strUseKind = "A";
                                                if (strUseKind == "SemiAuto")
                                                    strUseKind = "S";
                                                //clsTransaction.DelReservation_Date(strStartDate, strApparatus_ID);
                                                if ((strStartDate != "") && (strEndDate != "") && (strBorrower != "") && (strDepartment != "") && (strGName != "") && (strCustomer != "") && (strBorrower != ""))
                                                    clsTransaction.InsertApparatusReservation(strApparatus_ID, strStartDate, strEndDate, strBorrower, strDepartment, strExt, strEmail, strMission, strGName, strReturnDate, strCustodian_Check, strStatus, strProject_ID, strBorrowedQuantity, strAgent, strAgentExt, strAgentEmail, strCustomer, strApparatus_Price, strContinuousCount, strPeriod, strUseKind, strNote, strCustodian_Check, strAdmin_Check);
                                            }
                                            else
                                            {
                                                objConn.Close();
                                                clsMsg.AlertMessage("系統未搜尋到-客戶代碼:" + strCustomer, this.Page);
                                            }
                                        }
                                        else
                                        {
                                            objConn.Close();
                                            clsMsg.AlertMessage("系統未搜尋到-部門代碼:" + strDepartment + "", this.Page);
                                        }
                                    }

                                }
                            }

                        }
                        objConn.Close();
                        clsMsg.AlertMessage("上傳成功！", this.Page);
                    }
                    catch
                    {
                        objConn.Close();
                        clsMsg.AlertMessage("上傳失敗！", this.Page);
                    }
                }

            }
            else
                clsMsg.AlertMessage("請上傳檔案！", this.Page);

    }

    private string checkDepartment(string strDepartment)
    {
        string strDepartment1 = "";
        DataTable dt = clsData.UploadDepartment();

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            if (strDepartment == dt.Rows[intI][0].ToString().Replace(" ",""))
                strDepartment1 = dt.Rows[intI][0].ToString();
        }

        return strDepartment1;
    }


}
