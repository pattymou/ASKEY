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


public partial class WebForm_Import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadCustomer(this.ddlCustomer);
            loadKind(this.ddlKind, "0");
            loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strPath, strDr, strPath1;

        if (FileUpload1.HasFile)
        {
            if ((ddlKind.Text == "") || (ddlCustomer.Text == ""))
                clsMsg.AlertMessage("請輸入類別及客戶....", this.Page);
            else
            {
                FileUpload xfileupload = new FileUpload();
                //strPath = FileUpload1.PostedFile.FileName;
                strDr = Request.PhysicalApplicationPath + @"Temporarily\";
                strPath = FileUpload1.FileName;
                //strPath = Path.GetFileName(FileUpload1.PostedFile.FileName);
                strPath1 = strDr + strPath;
                FileUpload1.SaveAs(strPath1);
                ConvertSQL(strPath1);
            }
        }
        else
            clsMsg.AlertMessage("請選擇檔案....", this.Page);
        //strDr = Request.PhysicalApplicationPath + @"Temporarily\MS4 - O2 TC.xlsx";
        //Convert(strDr);

    }

    private void ConvertSQL(string strPath)
    {
        
        string strKind;
        string strName,strP_Name;
        StringBuilder strSQL = new StringBuilder();
        DataTable dt1,dt2;

        strKind = ddlKind.Text;
        strP_Name = ddlP_Name.Text;


        string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
        //Excel.Application objExcel = new Excel.ApplicationClass();
        OleDbConnection objConn = new OleDbConnection(strConn);

        objConn.Open();
        //try
        //{
            int intI1 = 0;
            string strSheetName="";
            DataRow[] sheetList = objConn.GetSchema("Tables").Select();
            foreach (DataRow sheet in sheetList)
            {
                //if (intI1 != 0)
                //{
                    strSheetName = sheet["TABLE_NAME"].ToString();



                    string strExcel = "";
                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;
                    strExcel = "select * from [" + strSheetName + "]";
                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table1");
                    //return ds;
                    strName = ddlCustomer.Text;
                    DataTable dt = ds.Tables["table1"];

                    int intI;
                    string strPlanDate;
                    string strNumber;
                    //DateTime dt1;


                    for (intI = 0; intI < dt.Rows.Count; intI++)
                    {
                        //if (dt.Rows[intI][4].ToString() != "")
                        //{
                        //    DateTime dt1 = Convert.ToDateTime(dt.Rows[intI][10].ToString());
                        //    strPlanDate = dt1.ToString("yyyy/MM/dd");
                        //    //if (strPlanDate == "1900/01/01")
                        //    //    strPlanDate = "";
                        //    //else
                        //    //    strPlanDate = strPlanDate;
                        //}
                        //else
                        //    strPlanDate = "";
                        strNumber = (intI + 1).ToString();
                        if ((dt.Rows[intI][4].ToString() != "") && (dt.Rows[intI][1].ToString() != "") && (dt.Rows[intI][2].ToString() != "") && (dt.Rows[intI][3].ToString() != ""))
                            clsTransaction.InsertExcelToSQL1(strKind, dt.Rows[intI][1].ToString(), dt.Rows[intI][2].ToString(), dt.Rows[intI][3].ToString(), dt.Rows[intI][4].ToString(), dt.Rows[intI][5].ToString(), dt.Rows[intI][6].ToString(), dt.Rows[intI][7].ToString(), dt.Rows[intI][8].ToString(), dt.Rows[intI][9].ToString(), strName, strP_Name, strNumber);
                            //clsTransaction.InsertExcelToSQL(strKind, dt.Rows[intI][0].ToString(), dt.Rows[intI][1].ToString(), dt.Rows[intI][2].ToString(), dt.Rows[intI][3].ToString(), dt.Rows[intI][4].ToString(), dt.Rows[intI][5].ToString(), dt.Rows[intI][6].ToString(), dt.Rows[intI][7].ToString(), dt.Rows[intI][8].ToString(), strName, strP_Name, strNumber);



                        

                    }
                //}
                intI1++;
            }

            strSQL.Append("select * from Requirement");

            dt1 = clsData.UploadTestPlanRequirement(strSQL);

            string strRequirementB, strTestPlanID, strRequirement_ID;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                strRequirement_ID = dt1.Rows[i]["Requirement_ID"].ToString();
                strSQL.Length = 0;
                strSQL.Append("select * from TestPlan ");
                strSQL.AppendFormat("WHERE Purpose like '%{0}%' {1} TestSteps like '%{2}%' {3} ExpectedResults like '%{4}%' and Kind = '{5}' and Customer = '{6}' and ProductName = '{7}' ", dt1.Rows[i]["PurposeKeyword"].ToString(), dt1.Rows[i]["Associate1"].ToString(), dt1.Rows[i]["TestStepsKeyword"].ToString(), dt1.Rows[i]["Associate2"].ToString(), dt1.Rows[i]["ExpectedResultsKeyword"].ToString(),ddlKind.Text,ddlCustomer.Text,ddlP_Name.Text);

                dt2 = clsData.UploadTestPlanRequirement(strSQL);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    strRequirementB = dt2.Rows[j]["RequirementID_B"].ToString();
                    strTestPlanID = dt2.Rows[j]["ID"].ToString();
                    if (strRequirementB != "")
                        strRequirementB = strRequirementB + "," + strRequirement_ID;
                    else
                        strRequirementB = strRequirement_ID;

                    if (clsTransaction.UpDateTestPlanRequirement(strTestPlanID, strRequirementB) == false)
                        clsMsg.AlertMessage("Requirement修改失敗！", this.Page);
                }

            }

            objConn.Close();
            File.Delete(strPath);
            clsMsg.AlertMessage("轉檔成功！", this.Page);
        //}
        //catch (Exception ex)
        //{
        //    objConn.Close();
        //    File.Delete(strPath);
        //    clsMsg.AlertMessage("轉檔失敗！", this.Page);
        //}
        
    }


    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
    }
}
