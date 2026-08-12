using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Configuration;
using System.Collections;
using System.Text;

public partial class WebForm_BenchmarkImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["EmpNo"] == null)
        //    Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadCustomer(this.ddlCustomer);
            loadKind(this.ddlKind, "0");
            loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
            loadNPI(this.ddlNPI);
        }
    }

    #region loadP_Name
    protected void loadP_Name(DropDownList DDL, string strCategory, string strKind1)
    {
        clsDropDownList.ddlP_Name(DDL, strCategory, strKind1);
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, "0");
    }
    #endregion

    #region loadKind
    protected void loadKind(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlTestCaseKind(DDL, strKind1);
    }
    #endregion

    #region loadNPI
    protected void loadNPI(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 2, "0");
    }
    #endregion

    #region ddlCustomer
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
    }
    #endregion

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strPath, strDr, strPath1;


        //ConvertLOS("");
        //strPath = "C:\\inetpub\\wwwroot\\SIT_System_patty\\Temporarily\\SIT-TR-WL-LOS-ANAC-RG8000W(RoHS)-DV-V10.xls";
        //ConvertLos(strPath);


        if (FileUpload1.HasFile)
        {
            if ((ddlKind.Text == "") || (ddlCustomer.Text == "") || (ddlP_Name.Text == "") || (ddlNPI.Text == ""))
                clsMsg.AlertMessage("請輸入類別、客戶、機種名稱及NPI....", this.Page);
            else
            {
                FileUpload xfileupload = new FileUpload();
                //strPath = FileUpload1.PostedFile.FileName;
                strDr = Request.PhysicalApplicationPath + @"Temporarily\";
                strPath = FileUpload1.FileName;
                //strPath = Path.GetFileName(FileUpload1.PostedFile.FileName);
                strPath1 = strDr + strPath;

                //if (strPath.IndexOf("LOS") < 0)

                    FileUpload1.SaveAs(strPath1);
                ConvertLos(strPath1);
            }
        }
        else
            clsMsg.AlertMessage("請選擇檔案....", this.Page);
    }

    private void ConvertLos(string strPath)
    {

        string strKind,strCustomer,strNPI;
        string strType = "";
        string strName, strP_Name,strMaxID;
        int intW, intW1;
        StringBuilder strSQL = new StringBuilder();
        DataTable dt1, dt2;

        strKind = ddlKind.Text;
        strP_Name = ddlP_Name.Text;
        strCustomer = ddlCustomer.Text;
        strNPI = ddlNPI.Text;


        if (clsTransaction.InsertLosToSQL(strKind, strCustomer, strP_Name, strNPI) == true)
        {
            DataTable dt3 = clsData.UploadLosInfoLastIDQuery();

            strMaxID = dt3.Rows[0]["ID"].ToString();
            string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            OleDbConnection objConn = new OleDbConnection(strConn);

            objConn.Open();
            //try
            //{
                int intI1 = 0;
                string strSheetName = "";
                DataRow[] sheetList = objConn.GetSchema("Tables").Select();
                foreach (DataRow sheet in sheetList)
                {
                    try
                    {
                        strSheetName = sheet["TABLE_NAME"].ToString();
                        intW = 0;
                        string strExcel = "";
                        if ((strSheetName.IndexOf("11ac-5G-20M") > 0) || (strSheetName.IndexOf("11ac-5G-40M") > 0) || (strSheetName.IndexOf("11ac-5G-80M") > 0))
                        {
                            strExcel = "select * from [" + strSheetName + "]";

                            if (strSheetName.IndexOf("11ac-5G-20M") > 0)
                            {
                                strKind = "11ac-5G-20M";
                                intW = 1;
                            }
                            if (strSheetName.IndexOf("11ac-5G-40M") > 0)
                            {
                                strKind = "11ac-5G-40M";
                                intW = 1;
                            }
                            if (strSheetName.IndexOf("11ac-5G-80M") > 0)
                            {
                                strKind = "11ac-5G-80M";
                                intW = 1;
                            }



                            OleDbDataAdapter myCommand = null;
                            DataSet ds = null;

                            myCommand = new OleDbDataAdapter(strExcel, strConn);
                            ds = new DataSet();
                            myCommand.Fill(ds, "table1");
                            strName = ddlCustomer.Text;
                            DataTable dt = ds.Tables["table1"];

                            int intI, intJ;
                            string strChannel;
                            string strAngle = "";
                            string strNumber;
                            string[] strAtt = new string[11];
                            string[] strDistance = new string[11];
                            string[] strThroughput = new string[11];


                            for (intI = 0; intI < dt.Rows.Count; intI++)
                            {
                                intW1 = 0;
                                if (dt.Rows[intI][0].ToString().IndexOf("Tx. Throughput") > 0)
                                {
                                    strType = "Tx";
                                    //intW1 = 1;
                                }
                                if ((dt.Rows[intI][0].ToString().IndexOf("5G - 20MHz  Rx. Throughput") > 0) || (dt.Rows[intI][0].ToString().IndexOf("5G - 40MHz  Rx. Throughput") > 0) || (dt.Rows[intI][0].ToString().IndexOf("5G - 80MHz  Rx. Throughput") > 0))
                                {
                                    strType = "Rx";
                                    //intW1 = 1;
                                }
                                if (dt.Rows[intI][0].ToString().IndexOf("Tx + Rx Throughput") > 0)
                                {
                                    strType = "TxRx";
                                    //intW1 = 1;
                                }

                                if (dt.Rows[intI][0].ToString().Trim() == "Attenuation (dB)")
                                {
                                    for (intJ = 0; intJ < 11; intJ++)
                                    {
                                        strAtt[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                    }
                                }

                                if (dt.Rows[intI][0].ToString().Trim() == "Distance (meter)")
                                {
                                    for (intJ = 0; intJ < 11; intJ++)
                                    {
                                        strDistance[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                    }
                                    //intI = intI + 1;
                                }

                                if (dt.Rows[intI][8].ToString().Trim() == "Best Angle：")
                                {

                                    strAngle = dt.Rows[intI][10].ToString();
                                    intI = intI + 1;
                                    intW1 = 1;
                                }

                                if ((intW == 1) && (intW1 == 1))
                                {
                                    while (dt.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                                    //if (dt.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                                    {
                                        strChannel = dt.Rows[intI][0].ToString() + dt.Rows[intI][1].ToString();
                                        for (intJ = 0; intJ < 11; intJ++)
                                        {
                                            //strThroughput[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                            strNumber = dt.Rows[intI][intJ + 2].ToString();
                                            if (strNumber != "N/S")
                                                clsTransaction.InsertLosDataToSQL(strMaxID, strKind, strType, strAtt[intJ], strDistance[intJ], strChannel, strAngle, strNumber);
                                            else
                                                intJ = 13;

                                        }
                                        intI++;
                                    }
                                }


                            }
                            intI1++;
                        }
                    }
                    catch (Exception ex) 
                    {
                    }
                }



                objConn.Close();
                //File.Delete(strPath);
                clsMsg.AlertMessage("轉檔成功！", this.Page);
            //}
            //catch (Exception ex)
            //{
            //    objConn.Close();
            //    //File.Delete(strPath);
            //    //clsMsg.AlertMessage("轉檔失敗！", this.Page);
            //}
        }

    }

    
}
