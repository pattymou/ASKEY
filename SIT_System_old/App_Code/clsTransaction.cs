using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// clsTransaction 的摘要描述
/// 所有交易資料(Insert/Delete/Update)均在此作業
/// </summary>
public class clsTransaction
{


    #region 取得系統連線字串

    private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
    private static string connStr_PND = WebConfigurationManager.ConnectionStrings["connStr_PND"].ConnectionString;

    #endregion

    #region Insert Login
    public static bool InsertUser(int emp_no)
    {
        return true;
    }
    #endregion

    #region InsertInfo_Product (新增InfoData)
    public static bool InsertInfo_Product(string strKind, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.ProjectList values (");
        strSQL.AppendFormat("'{0}','{1}')", strKind, strName); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertInfo (新增InfoData)
    public static bool InsertInfo(string strKind, string strName, string strValue)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.InfoData values (");
        strSQL.AppendFormat("'{0}','{1}','{2}')", strKind, strName, strValue); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertInfo (新增InfoData)
    public static bool InsertInfo_PND(string strKind, string strName, string strValue)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr_PND);

        strSQL.Append("Insert into dbo.InfoData values (");
        strSQL.AppendFormat("'{0}','{1}','{2}')", strKind, strName, strValue); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertWeeklyReport (新增WeeklyReport)
    public static bool InsertWeeklyReport(string strEmp, string strWeek, string strProject, string strItem, string strDetail, string strDate, string strHours)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.WeeklyReport values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}')", strEmp, strWeek, strProject, strItem, strDetail, strDate, strHours); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddWeeklyReport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertWeeklyPlan (新增WeeklyPlan)
    public static bool InsertWeekPlan(string strEmp, string strWeek, string strWeekName, string strPlan, string strYear)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.WeekPlan values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", strEmp, strWeek, strWeekName, strPlan, strYear); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddWeeklyReport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_Kind (新增Application_Kind)
    public static bool InsertApplication_Kind(string strKind, string strTeam, string strDepartment,string strApplication_Kind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_Kind values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}')", strKind, strTeam, " ", strDepartment, strApplication_Kind, ""); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertExplanation_Kind (新增Explanation_Kind)
    public static bool InsertExplanation_Kind(string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Explanation_Kind values (");
        strSQL.AppendFormat("'{0}')", strKind); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_Function (新增Application_Function)
    public static bool InsertApplication_Function(string strID, string strKindID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_Function values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','')", strID, strKindID, strName, ""); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertExplanation_Item (新增Explanation_Item)
    public static bool InsertExplanation_Item(string strID, string strKindID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Explanation_Item values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", strID, strKindID, strName, "", ""); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_Item (新增Application_Item)
    public static bool InsertApplication_Item(string strID, string strKindID, string strFunctionID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_Item values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','','','','','','','')", strID, strKindID, strFunctionID, strName, "", "", ""); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestPlan (新增TestPlan)
    public static bool InsertTestPlan(string strKind, string strRequirementID, string strCategory, string strSubCategory, string strPurpose, string strEnvironmentSetup, string strTestSteps, string strExpectedResults, string strTestResult, string strBugTicketID, string strRDComment, string strCustomer, string strP_Name, string strRequirementB, string strNumber)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestPlan values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", strKind, strRequirementID, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name, strRequirementB, strNumber); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPlanItem", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCertification_Wifi (新增Certification_Wifi)
    public static bool InsertCertification_Wifi(string strProjectID, string strProductName, string strModelNumber, string strWirelessChipset, string strProductOperatingSystem, string strOSVersion, string strHardwareVersion_Product, string strFirmwareVersion_Product, string strHardwareVersion_WiFi, string strFirmwareVersion_WiFi, string strProductNotes, string strSearchable, string strPublish, string strPublish_Date, string strDeviceType, string strProductType, string strPrimaryProductCategory, string strSecondaryProductCategory, string strLeastOneBand, string strMandatoryProgram, string strOptionalProgram, string strSupportedSpatialStreams_Tx, string strSupportedSpatialStreams_Rx, string strAdditionalCapabilities, string strSecurityType, string strSpectrumAndRegulatoryFeatures, string strNOptionalFeature, string strACOptionalFeature)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Certification_Wifi values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')", strProjectID, strProductName, strModelNumber, strWirelessChipset, strProductOperatingSystem, strOSVersion, strHardwareVersion_Product, strFirmwareVersion_Product, strHardwareVersion_WiFi, strFirmwareVersion_WiFi, strProductNotes, strSearchable, strPublish, strPublish_Date, strDeviceType, strProductType, strPrimaryProductCategory, strSecondaryProductCategory, strLeastOneBand, strMandatoryProgram, strOptionalProgram, strSupportedSpatialStreams_Tx, strSupportedSpatialStreams_Rx, strAdditionalCapabilities, strSecurityType, strSpectrumAndRegulatoryFeatures, strNOptionalFeature, strACOptionalFeature); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_Wifi", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCertification_GCF (新增Certification_GCF)
    public static bool InsertCertification_GCF(string strProjectID, string strVoLTE, string strCertifiedModule, string strModuleNumber, string strInherits, string strRAT_2G, string strRAT_3G, string strRAT_4G, string strRAT_5G, string strCA_4G, string strCA_5G, string strSIMNumber, string strMR)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Certification_GCF values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", strProjectID, strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_GCF", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCertification_PTCRB (新增Certification_PTCRB)
    public static bool InsertCertification_PTCRB(string strProjectID, string strVoLTE, string strCertifiedModule, string strModuleNumber, string strInherits, string strRAT_2G, string strRAT_3G, string strRAT_4G, string strRAT_5G, string strCA_4G, string strCA_5G, string strSIMNumber, string strMR, string strIMEI)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Certification_PTCRB values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", strProjectID, strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR, strIMEI); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_PTCRB", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestPool (新增TestPool)
    public static bool InsertTestPool(string strKind, string strCategory, string strSubCategory, string strPurpose, string strEnvironmentSetup, string strTestSteps, string strExpectedResults, string strTestResult, string strBugTicketID, string strRDComment, string strCustomer, string strP_Name, string strNumber)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestPool values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", strKind, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name, strNumber); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPlanItem", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_LTE (新增Application_LTE)
    public static bool InsertApplication_LTE(string strProject_ID, string strBand, string strTRP1, string strTRP2, string strTRP3, string strTIS1, string strTIS2, string strTIS3)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Application_LTE values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", strProject_ID, strBand, strTRP1, strTRP2, strTRP3, strTIS1, strTIS2, strTIS3); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Application_LTE", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application  (SearchApplication使用)
    public static bool DelApplication(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.project WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application_Temporarily  (SearchApplication使用)
    public static bool DelApplication_Temporarily(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.project_Temporarily WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application TestCase  (ModifyApplication使用)
    public static bool DelApplicationTestCase(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Application_TestCase WHERE project_id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application TestCase_Temporarily  (ModifyApplication使用)
    public static bool DelApplicationTestCase_Temporarily(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Application_TestCase_Temporarily WHERE project_id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete WeeklyReport  (AddWeeklyReport使用)
    public static bool DelWeeklyReport(string strNumber, string strEmp, string strYear)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.WeeklyReport WHERE WeekNumber = '{0}' and Employees ='{1}' and year(Report_Date) ='{2}' ", strNumber, strEmp, strYear);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddWeeklyReport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete WeekPlan  (AddWeeklyReport使用)
    public static bool DelWeekPlan(string strNumber, string strEmp, string strYear)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.WeekPlan WHERE Week_Number = '{0}' and Employees ='{1}' and Plan_Year ='{2}' ", strNumber, strEmp, strYear);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddWeeklyReport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Customer TestCase  (AddTestCase使用)
    public static bool DelCustomerTestCase(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Customer_TestCase WHERE Customer= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddTestCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete PR  (PR_Detail使用)
    public static bool DelPR(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.PurchasingRequisition WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_PR_Detail", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Requirement  (RequirementView使用)
    public static bool DelRequirement(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Requirement WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_RequirementView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion


    #region Delete TestPlan  (ModifyPlan使用)
    public static bool DelTestPlan(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestPlan WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyPlan", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete TestPool  (TestPoolView使用)
    public static bool DelTestPool(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestPool WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_TestPoolView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application Bluetooth  (SearchApplication使用)
    public static bool DelApplication_Bluetooth(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestCase_Bluetooth WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application DSL  (SearchApplication使用)
    public static bool DelApplication_DSL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestCase_DSL WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    //#region Delete Application LTE  (SearchApplication使用)
    //public static bool DelApplication_LTE(string strID)
    //{
    //    StringBuilder strSQL = new StringBuilder();
    //    MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

    //    strSQL.AppendFormat("Delete dbo.TestCase_LTE WHERE Project_ID= '{0}' ", strID);
    //    bool isExist = false;
    //    try
    //    {
    //        sqlConn.openConnection();
    //        sqlConn.beginTransaction();
    //        sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
    //        sqlConn.commitTransaction();
    //        isExist = true;
    //        return isExist;
    //    }
    //    catch (System.Exception ex)
    //    {
    //        isExist = false;
    //        MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
    //        sqlConn.rollbackTransaction();
    //        return isExist;
    //    }
    //    finally
    //    {
    //        sqlConn.closeConnection();
    //    }
    //}
    //#endregion

    #region Delete Application USB  (SearchApplication使用)
    public static bool DelApplication_USB(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestCase_USB WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application WiFi  (SearchApplication使用)
    public static bool DelApplication_WiFi(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestCase_WiFi WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application Wireless  (SearchApplication使用)
    public static bool DelApplication_Wireless(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.TestCase_Wireless WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application File  (SearchApplication使用)
    public static bool DelApplication_File(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Attachmen_File WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Application_LTE  (Application_LTE使用)
    public static bool DelApplication_LTE(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Application_LTE WHERE Project_ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Application_LTE", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    //#region Delete Application ProjectCase  (SearchApplication使用)
    //public static bool DelApplication_ProjectCase(string strID)
    //{
    //    StringBuilder strSQL = new StringBuilder();
    //    MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

    //    strSQL.AppendFormat("Delete dbo.ProjectCase WHERE Project_ID= '{0}' ", strID);
    //    bool isExist = false;
    //    try
    //    {
    //        sqlConn.openConnection();
    //        sqlConn.beginTransaction();
    //        sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
    //        sqlConn.commitTransaction();
    //        isExist = true;
    //        return isExist;
    //    }
    //    catch (System.Exception ex)
    //    {
    //        isExist = false;
    //        MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApplication", ex.Message);
    //        sqlConn.rollbackTransaction();
    //        return isExist;
    //    }
    //    finally
    //    {
    //        sqlConn.closeConnection();
    //    }
    //}
    //#endregion

    #region Delete Data  (InfoData使用)
    public static bool DelInfo_Product(string strCustomer, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.ProjectList WHERE Customer= '{0}' and ProductName = '{1}' ", strCustomer, strName);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Del PR_Detail  (PR_Detail使用)
    public static bool DelPR_Detail(string strPR_ID, string strGoods_ID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.PR_Detail WHERE PR_ID= '{0}' and Goods_ID = '{1}' ", strPR_ID, strGoods_ID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_PR_Detail", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (InfoData使用)
    public static bool DelInfo(string strItem, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.InfoData WHERE name= '{0}' and id='{1}' ", strItem, strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (InfoData使用)
    public static bool DelInfo_PND(string strItem, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr_PND);

        strSQL.AppendFormat("Delete dbo.InfoData WHERE name= '{0}' and Kind='{1}' ", strItem, strKind);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (AddInfo使用)
    public static bool UpDateInfoData(string strName, string strUName, string strValue)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.InfoData Set ");
        strSQL.AppendFormat("Name = '{0}',Value = '{1}' ", strUName, strValue);
        //strSQL.AppendFormat("where Name= '{0}' ", strName);
        strSQL.AppendFormat("where id= '{0}' ", strName);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (AddInfo使用)
    public static bool UpDateInfoData_PND(string strKind, string strUName, string strValue, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr_PND);

        strSQL.Append("Update dbo.InfoData Set ");
        strSQL.AppendFormat("Name = '{0}',Value = '{1}' ", strUName, strValue);
        //strSQL.AppendFormat("where Name= '{0}' ", strName);
        strSQL.AppendFormat("where Kind= '{0}' and Name='{1}' ", strKind, strName);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Bulletin (AddBulletin使用)
    public static bool UpDateBulletin(string strNote)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Bulletin Set ");
        strSQL.AppendFormat("Note = '{0}' where id='1'", strNote);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateBulletin", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateDepartmentAccount (AddDepartmentAccount使用)
    public static bool UpDateDepartmentAccount(string strID, string strPwd)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.DepartmentAccount Set ");
        strSQL.AppendFormat("Password = '{0}' ", strPwd);
        strSQL.AppendFormat("where ID= '{0}' ", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateDepartmentAccount", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update TestPlan (TestPlan使用)
    public static bool UpDateTestPlan(string strID, string strKind, string strRequirementID, string strCategory, string strSubCategory, string strPurpose, string strEnvironmentSetup, string strTestSteps, string strExpectedResults, string strTestResult, string strBugTicketID, string strRDComment, string strCustomer, string strP_Name, string strRequirementB)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestPlan Set ");
        strSQL.AppendFormat("Kind = '{0}',RequirementID = '{1}',Category = '{2}',SubCategory = '{3}',Purpose = '{4}',EnvironmentSetup = '{5}',TestSteps = '{6}',ExpectedResults = '{7}',TestResult = '{8}',BugTicketID = '{9}',RDComment = '{10}',Customer = '{11}',ProductName='{12}',RequirementID_B='{13}' ", strKind, strRequirementID, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name, strRequirementB);
        strSQL.AppendFormat("where ID= '{0}' ", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPlanItem", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update TestPool (TestPool使用)
    public static bool UpDateTestPool(string strID, string strKind, string strCategory, string strSubCategory, string strPurpose, string strEnvironmentSetup, string strTestSteps, string strExpectedResults, string strTestResult, string strBugTicketID, string strRDComment, string strCustomer, string strP_Name)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestPool Set ");
        strSQL.AppendFormat("Kind = '{0}',Category = '{1}',SubCategory = '{2}',Purpose = '{3}',EnvironmentSetup = '{4}',TestSteps = '{5}',ExpectedResults = '{6}',TestResult = '{7}',BugTicketID = '{8}',RDComment = '{9}',Customer = '{10}',ProductName='{11}' ", strKind, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name);
        strSQL.AppendFormat("where ID= '{0}' ", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPlanItem", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateInfoSystemData (AddInfo使用)
    public static bool UpDateApparatusMasterData(string strName, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.InfoData Set ");
        strSQL.AppendFormat("Name = '{0}' where kind='{1}'", strName, strKind);
        //strSQL.AppendFormat("where Name= '{0}' ", strName);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateVerification (Verification使用)
    public static bool UpDateVerification(string strID, string strRandom)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Number Set ");
        strSQL.AppendFormat("Verification = 'Y' where ID='{0}' and Random='{1}'", strID, strRandom);
        //strSQL.AppendFormat("where Name= '{0}' ", strName);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Verification", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateVerification (Verification使用)
    public static bool UpDateVerification_PND(string strID, string strRandom)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr_PND);

        strSQL.Append("Update dbo.Number Set ");
        strSQL.AppendFormat("Verification = 'Y' where ID='{0}' and Random='{1}'", strID, strRandom);
        //strSQL.AppendFormat("where Name= '{0}' ", strName);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Verification", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDatePwd (ChangePwd使用)
    public static bool UpDatePwd(string strPwd, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Employees Set ");
        strSQL.AppendFormat("Password = '{0}' ", strPwd);
        strSQL.AppendFormat("where ID= '{0}' ", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDatePwd", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateDepartmentDailyReport (DepartmentDailyReport使用)
    public static bool UpDateDepartmentDailyReport(string strID, string strNote)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation_DailyReport Set ");
        strSQL.AppendFormat("Note = '{0}' ", strNote);
        strSQL.AppendFormat("where Reservation_ID= '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DepartmentDailyReport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateContinuousDate (ApparatusContinuous使用)
    public static bool UpDateContinuousDate(string strID, string strDate, string strCount)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation Set ");
        strSQL.AppendFormat("ContinuousDate = '{0}',ContinuousCount = '{1}' ,Custodian_Check='' ,Admin_Check='' where id='{2}'", strDate, strCount, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusContinuous", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateGoodsContinuousDate (GoodsContinuous使用)
    public static bool UpDateGoodsContinuousDate(string strID, string strDate, string strCount)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.GoodsReservation Set ");
        strSQL.AppendFormat("ContinuousDate = '{0}',ContinuousCount = '{1}',ContinuousStatus='Y' where id='{2}'", strDate, strCount, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusContinuous", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplication_TestKind (InfoData使用)
    public static bool UpDateApplication_TestKind(string strTeam, string strID, string strI)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Kind Set ");

        if (strI == "0")
            strSQL.AppendFormat("Custodian_Team = '{0}' where id = '{1}'", strTeam, strID);
        else if (strI == "1")
            strSQL.AppendFormat("Disable = 'Y' where id = '{1}'", strTeam, strID);
        else
            strSQL.AppendFormat("Disable = 'Y' , Hide = 'Y' where id = '{1}'", strTeam, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateCertification_Wifi (Certification_Wifi使用)
    public static bool UpDateCertification_Wifi(string strProjectID, string strProductName, string strModelNumber, string strWirelessChipset, string strProductOperatingSystem, string strOSVersion, string strHardwareVersion_Product, string strFirmwareVersion_Product, string strHardwareVersion_WiFi, string strFirmwareVersion_WiFi, string strProductNotes, string strSearchable, string strPublish, string strPublish_Date, string strDeviceType, string strProductType, string strPrimaryProductCategory, string strSecondaryProductCategory, string strLeastOneBand, string strMandatoryProgram, string strOptionalProgram, string strSupportedSpatialStreams_Tx, string strSupportedSpatialStreams_Rx, string strAdditionalCapabilities, string strSecurityType, string strSpectrumAndRegulatoryFeatures, string strNOptionalFeature, string strACOptionalFeature)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Certification_Wifi Set ");

        strSQL.AppendFormat("ProductName = '{0}',ModelNumber = '{1}',WirelessChipset = '{2}',ProductOperatingSystem = '{3}',OSVersion = '{4}',HardwareVersion_Product = '{5}',FirmwareVersion_Product = '{6}',HardwareVersion_WiFi = '{7}',FirmwareVersion_WiFi = '{8}',ProductNotes = '{9}',Searchable = '{10}',Publish = '{11}',Publish_Date='{12}',DeviceType='{13}',ProductType='{14}',PrimaryProductCategory='{15}',SecondaryProductCategory='{16}',LeastOneBand='{17}',MandatoryProgram='{18}',OptionalProgram='{19}',SupportedSpatialStreams_Tx='{20}',SupportedSpatialStreams_Rx='{21}',AdditionalCapabilities='{22}',SecurityType='{23}',SpectrumAndRegulatoryFeatures='{24}',NOptionalFeature='{25}',ACOptionalFeature='{26}' ", strProductName, strModelNumber, strWirelessChipset, strProductOperatingSystem, strOSVersion, strHardwareVersion_Product, strFirmwareVersion_Product, strHardwareVersion_WiFi, strFirmwareVersion_WiFi, strProductNotes, strSearchable, strPublish, strPublish_Date, strDeviceType, strProductType, strPrimaryProductCategory, strSecondaryProductCategory, strLeastOneBand, strMandatoryProgram, strOptionalProgram, strSupportedSpatialStreams_Tx, strSupportedSpatialStreams_Rx, strAdditionalCapabilities, strSecurityType, strSpectrumAndRegulatoryFeatures, strNOptionalFeature, strACOptionalFeature);
        strSQL.AppendFormat("where Project_ID= '{0}' ", strProjectID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_Wifi", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateCertification_BT (Certification_BT使用)
    public static bool UpDateCertification_BT(string strProjectID, string strBT_Version, string strCore_Mode, string strBriefly_Describe, string strApplication_Profiles, string strController_Vendor, string strController_DID, string strHost_Vendor, string strHost_DID, string strComponent_Vendor, string strComponent_DID, string strEnd_Vendor, string strEnd_DID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Certification_BT Set ");

        strSQL.AppendFormat("BT_Version = '{0}',Core_Mode = '{1}',Briefly_Describe = '{2}',Application_Profiles = '{3}',Controller_Vendor = '{4}',Controller_DID = '{5}',Host_Vendor = '{6}',Host_DID = '{7}',Component_Vendor = '{8}',Component_DID = '{9}',End_Vendor = '{10}',End_DID = '{11}'", strBT_Version, strCore_Mode, strBriefly_Describe, strApplication_Profiles, strController_Vendor, strController_DID, strHost_Vendor, strHost_DID, strComponent_Vendor, strComponent_DID, strEnd_Vendor, strEnd_DID);
        strSQL.AppendFormat("where Project_ID= '{0}' ", strProjectID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_BT", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateCertification_GCF (Certification_GCF使用)
    public static bool UpDateCertification_GCF(string strProjectID, string strVoLTE, string strCertifiedModule, string strModuleNumber, string strInherits, string strRAT_2G, string strRAT_3G, string strRAT_4G, string strRAT_5G, string strCA_4G, string strCA_5G, string strSIMNumber, string strMR)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Certification_GCF Set ");

        strSQL.AppendFormat("VoLTE = '{0}',CertifiedModule = '{1}',ModuleNumber = '{2}',Inherits = '{3}',RAT_2G = '{4}',RAT_3G = '{5}',RAT_4G = '{6}',RAT_5G = '{7}',CA_4G = '{8}',CA_5G = '{9}',SIMNumber = '{10}',MR = '{11}' ", strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR);
        strSQL.AppendFormat("where Project_ID= '{0}' ", strProjectID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_GCF", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateCertification_PTCRB (Certification_PTCRB使用)
    public static bool UpDateCertification_PTCRB(string strProjectID, string strVoLTE, string strCertifiedModule, string strModuleNumber, string strInherits, string strRAT_2G, string strRAT_3G, string strRAT_4G, string strRAT_5G, string strCA_4G, string strCA_5G, string strSIMNumber, string strMR, string strIMEI)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Certification_PTCRB Set ");

        strSQL.AppendFormat("VoLTE = '{0}',CertifiedModule = '{1}',ModuleNumber = '{2}',Inherits = '{3}',RAT_2G = '{4}',RAT_3G = '{5}',RAT_4G = '{6}',RAT_5G = '{7}',CA_4G = '{8}',CA_5G = '{9}',SIMNumber = '{10}',MR = '{11}' ", strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR, strIMEI);
        strSQL.AppendFormat("where Project_ID= '{0}' ", strProjectID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_PTCRB", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelExplanation_Kind (InfoData使用)
    public static bool DelExplanation_Kind(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("delete dbo.Explanation_Kind ");

        strSQL.AppendFormat("where id = '{0}'", strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelExplanation_Item (InfoData使用)
    public static bool DelExplanation_Item(string strID, string strKindID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("delete dbo.Explanation_Item ");

        strSQL.AppendFormat("where id = '{0}' and Kind_ID='{1}'", strID, strKindID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApparatusStatus
    public static bool UpDateApparatusStatus(string strStatus, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Apparatus Set ");
        strSQL.AppendFormat("ReservationStatus = '{0}' where id = '{1}'", strStatus, strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusStatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateGoodsQuantityStock
    public static bool UpDateGoodsQuantityStock(string strCount, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Goods Set ");
        strSQL.AppendFormat("Quantity_Stock = '{0}' where id = '{1}'", strCount, strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_GoodsReservation", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateGoodsCount
    public static bool UpDateGoodsCount(string strQuantity_Stock, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Goods Set ");
        strSQL.AppendFormat("Quantity_Stock = '{0}' where id = '{1}'", strQuantity_Stock, strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_GoodsReservationCancel", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateSampleStatus
    public static bool UpDateSampleStatus(string strStatus, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Sample_New Set ");
        strSQL.AppendFormat("ReservationStatus = '{0}' where id = '{1}'", strStatus, strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusStatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateNumber
    public static bool UpDateNumber(string strID, string strNumber, string strName, string strMail, string strDepartment, string strPassWord, string strCard, string strExt)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Number Set ");
        strSQL.AppendFormat("Number = '{0}',Name = '{1}',Mail = '{2}',Department = '{3}',PassWord = '{4}',CardNumber = '{5}',Ext = '{6}' where id = '{7}'", strNumber, strName, strMail, strDepartment, strPassWord, strCard, strExt, strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusStatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApparatusStatus
    public static bool UpDateGoodsStatus(string strStatus, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Goods Set ");
        strSQL.AppendFormat("Status = '{0}' where id = '{1}'", strStatus, strID);



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyGReservationStatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplication_TestFunction (InfoData使用)
    public static bool UpDateApplication_TestFunction(string strKindID, string strName, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Function Set ");

        if (strKind == "0")
            strSQL.AppendFormat("Disable = 'Y' where kind_id='{0}' and name='{1}'", strKindID, strName);
        else
            strSQL.AppendFormat("Disable = 'Y',Hide = 'Y' where kind_id='{0}' and name='{1}'", strKindID, strName);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplication_TestItem (InfoData使用)
    public static bool UpDateApplication_TestItem(string ID, string strKindID, string strFunctionID, string strName, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Item Set ");

        if (strKind == "0")
            strSQL.AppendFormat("Disable = 'Y' where id='{0}'and kind_id='{1}'and Function_id='{2}' and Item='{3}'", ID, strKindID, strFunctionID, strName);
        else
            strSQL.AppendFormat("Disable = 'Y',Hide = 'Y' where id='{0}'and kind_id='{1}'and Function_id='{2}' and Item='{3}'", ID, strKindID, strFunctionID, strName);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateCertification_Wifi_Data (Certification_Wifi_Data使用)
    public static bool UpDateCertification_Wifi_Data(string strID, string strModifyKind, string strNote)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Certification_Wifi_Data Set ");

        if (strModifyKind == "Content")
            strSQL.AppendFormat("Description = '{0}' where id = '{1}'", strNote, strID);
        else
            strSQL.AppendFormat("Disable = 'Y' where id = '{0}'", strID);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_Wifi", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateCertification_BT_Disable (Certification_BT_Data使用)
    public static bool UpDateCertification_BT_Disable(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Certification_BT_Data Set ");

        strSQL.AppendFormat("Disable = 'Y' where id = '{0}'", strID);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_Wifi", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCertification_Wifi_Data
    public static bool InsertCertification_Wifi_Data(string strKind, string strName, string strDescription, string strDisable)
    {
        //strKind- 0=Mandatory Program
        //         1=Optional Programs
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Certification_Wifi_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}')", strKind, strName, strDescription, strDisable);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddCertification", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCertification_BT_Data
    public static bool InsertCertification_BT_Data(string strKind, string strName, string strDisable)
    {
        //strKind- 0=Product Category
        //         1=BT Core Spec
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Certification_BT_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}')", strKind, strName, strDisable);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddCertification", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCertification_BT
    public static bool InsertCertification_BT(string strProjectID, string strBT_Version, string strCore_Mode, string strBriefly_Describe, string strApplication_Profiles, string strController_Vendor, string strController_DID, string strHost_Vendor, string strHost_DID, string strComponent_Vendor, string strComponent_DID, string strEnd_Vendor, string strEnd_DID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Certification_BT values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", strProjectID, strBT_Version, strCore_Mode, strBriefly_Describe, strApplication_Profiles, strController_Vendor, strController_DID, strHost_Vendor, strHost_DID, strComponent_Vendor, strComponent_DID, strEnd_Vendor, strEnd_DID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Certification_BT", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUser (新增User)
    public static bool InsertUser(string strNumber, string strLogin, string strName, string strDepartment, string strTeam, string strPosition, string strExt, string strPhone, string strAdd, string strEmail, string strPwd, string strLocation, string strWrite, string strLeader, string strManager)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Employees values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", strNumber, strLogin, strName, strDepartment, strTeam, strPosition, strExt, strPhone, strAdd, strEmail, strPwd, strLocation, strWrite, strLeader, strManager);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertDepartmentAccount (新增DepartmentAccount)
    public static bool InsertDepartmentAccount(string strID, string strPassword)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.DepartmentAccount values (");
        strSQL.AppendFormat("'{0}','{1}')", strID, strPassword);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddDepartmentAccount", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_Function (新增Application_TestFunction)
    public static bool InsertApplication_Function(string strID, string strPassword)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_Function values (");
        strSQL.AppendFormat("'{0}','{1}')", strID, strPassword);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddDepartmentAccount", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase (新增InsertTestCase,AddPath使用)
    public static bool InsertTestCase(string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.FilePath_TestCase values (");
        strSQL.AppendFormat("'{0}')", strName); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertTestCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCaseItem (新增InsertTestCaseItem,AddPath使用)
    public static bool InsertTestCaseItem(string strID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.FilePath_Kind values (");
        strSQL.AppendFormat("'{0}','{1}')", strID, strName); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertTestCaseItem", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertProject (新增Project)
    public static bool InsertProject(string strID, string strName, string strAccepted, string strCustomer, string strNPI, string strPM, string strHW, string strSW, string strMechanical, string FW, string strWireless, string strPCB, string strBOM, string strMac, string strUtility, string strPart, string strReady, string strProduct, string strExpect, string strA_Name, string strA_Department, string strA_Ext, string strA_mail, string strAssign, string strStart, string strEnd, string strToday, string strStatus, string strNote, string strKind, string strProgress2, string strProgress, string strResult, string strExplain, string strTeam, string strRelated, string strJira, string strDQA, string strDepartment, string strAKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Project values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}')", strID, strName, strAccepted, strCustomer, strNPI, strPM, strHW, strSW, strMechanical, FW, strWireless, strPCB, strBOM, strMac, strUtility, strPart, strReady, strProduct, strExpect, strA_Name, strA_Department, strA_Ext, strA_mail, strAssign, strStart, strEnd, strToday, strStatus, strNote, strKind, strProgress2, strProgress, strResult, strExplain, strTeam, strRelated, strJira, strDQA, strDepartment, strAKind); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertProject", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertProject_Temporarily (新增Project_Temporarily)
    public static bool InsertProject_Temporarily(string strID, string strName, string strAccepted, string strCustomer, string strNPI, string strPM, string strHW, string strSW, string strMechanical, string FW, string strWireless, string strPCB, string strBOM, string strMac, string strUtility, string strPart, string strReady, string strProduct, string strExpect, string strA_Name, string strA_Department, string strA_Ext, string strA_mail, string strAssign, string strStart, string strEnd, string strToday, string strStatus, string strNote, string strKind, string strProgress2, string strProgress, string strResult, string strExplain, string strTeam, string strRelated, string strJira, string strDQA, string strDepartment, string strAKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Project_Temporarily values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}')", strID, strName, strAccepted, strCustomer, strNPI, strPM, strHW, strSW, strMechanical, FW, strWireless, strPCB, strBOM, strMac, strUtility, strPart, strReady, strProduct, strExpect, strA_Name, strA_Department, strA_Ext, strA_mail, strAssign, strStart, strEnd, strToday, strStatus, strNote, strKind, strProgress2, strProgress, strResult, strExplain, strTeam, strRelated, strJira, strDQA, strDepartment, strAKind); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertProject", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertProject_Temporarily (新增Project_Temporarily)
    public static StringBuilder InsertProject_Temporarily1(string strID, string strName, string strAccepted, string strCustomer, string strNPI, string strPM, string strHW, string strSW, string strMechanical, string FW, string strWireless, string strPCB, string strBOM, string strMac, string strUtility, string strPart, string strReady, string strProduct, string strExpect, string strA_Name, string strA_Department, string strA_Ext, string strA_mail, string strAssign, string strStart, string strEnd, string strToday, string strStatus, string strNote, string strKind, string strProgress2, string strProgress, string strResult, string strExplain, string strTeam, string strRelated, string strJira, string strDQA, string strDepartment)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Project_Temporarily values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}')", strID, strName, strAccepted, strCustomer, strNPI, strPM, strHW, strSW, strMechanical, FW, strWireless, strPCB, strBOM, strMac, strUtility, strPart, strReady, strProduct, strExpect, strA_Name, strA_Department, strA_Ext, strA_mail, strAssign, strStart, strEnd, strToday, strStatus, strNote, strKind, strProgress2, strProgress, strResult, strExplain, strTeam, strRelated, strJira, strDQA, strDepartment); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return strSQL;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertProject", ex.Message);
            sqlConn.rollbackTransaction();
            return strSQL;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_TestCase (新增Application_TestCase)
    public static bool InsertApplication_TestCase(string strProjectID, string strID, string strDepartment, string strCustomer)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Application_TestCase values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}')", strProjectID, strID, strDepartment, strCustomer); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Application_N", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApplication_TestCase_Temporarily (新增Application_TestCase_Temporarily)
    public static bool InsertApplication_TestCase_Temporarily(string strProjectID, string strID, string strDepartment, string strCustomer)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Application_TestCase_Temporarily values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}')", strProjectID, strID, strDepartment, strCustomer); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Application_N", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertCustomer_TestCase (新增Application_TestCase)
    public static bool InsertCustomer_TestCase(string strProjectID, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Customer_TestCase values (");
        strSQL.AppendFormat("'{0}','{1}')", strProjectID, strID); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddTestCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUploadFile (新增Attachmen_File)
    public static bool InsertUploadFile(string strID, string strName, string strKind, string strPath1)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Attachmen_File values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}')", strID, strName, strKind, strPath1); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUploadFile", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertNumber (新增Number)
    public static bool InsertNumber(string strID, string strNumber, string strName, string strMail, string strDepartment, string strPassWord, string strCard, string strCode, string strExt)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Number values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", strID, strNumber, strName, strMail, strDepartment, strPassWord, strCard, "", strCode, strExt); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddNumber", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertNumber (新增Number)
    public static bool InsertNumber_PND(string strID, string strNumber, string strName, string strMail, string strDepartment, string strPassWord, string strCard, string strCode, string strExt)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr_PND);

        strSQL.Append("Insert into dbo.Number values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", strID, strNumber, strName, strMail, strDepartment, strPassWord, strCard, "", strCode, strExt); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddNumber", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUploadFile_Project (新增Attachmen_File)
    public static bool InsertUploadFile_Project(string strName, string strKind, string strPath1)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Attachmen_File_Project values (");
        strSQL.AppendFormat("'{0}','{1}','{2}')", strName, strKind, strPath1); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUploadFile", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUploadFile_Case (新增Attachmen_File_Case)
    public static bool InsertUploadFile_Case(string strID, string strProject_ID, string strName, string strPath1, string strDate, string strEmp)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Attachmen_File_Case values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}')", strID, strProject_ID, strName, strPath1, strDate, strEmp); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUploadFile_Case", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertProjectCase (新增ProjectCase)
    public static bool InsertProjectCase(string strNumber, string strID, string strKind, string strName, string strAssign, string strStart, string strEnd, string strResult, string strStatus, string strExplain, string strProgress2, string strProgress, string strExplain_Kind, string strPU, string strModel, string strLab, string strQuoted, string strReimburse)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.ProjectCase values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')", strNumber, strID, strKind, strName, strAssign, strStart, strEnd, strResult, strProgress, strProgress, strExplain_Kind, strExplain, strStatus, strPU, strModel, strLab, strQuoted, strReimburse); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertProjectCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertProjectCase_Temporarily (新增ProjectCase_Temporarily)
    public static bool InsertProjectCase_Temporarily(string strNumber, string strID, string strKind, string strName, string strAssign, string strStart, string strEnd, string strResult, string strStatus, string strExplain, string strProgress2, string strProgress, string strExplain_Kind, string strPU, string strModel)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.ProjectCase_Temporarily values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", strNumber, strID, strKind, strName, strAssign, strStart, strEnd, strResult, strProgress, strProgress, strExplain_Kind, strExplain, strStatus, strPU, strModel); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertProjectCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase_DSL (新增TestCase_DSL)
    public static bool InsertTestCase_DSL(string strID, string strAdsl1, string strAdsl2, string strAdsl3, string strPlus1, string strPlus2, string strPlus3, string strVdsl1, string strVdsl2, string strVdsl3, string strBonding, string strRemote1, string strRemote2, string strXdsl1, string strXdsl2, string strRouter1, string strRouter2, string strRouter3, string strRouter4, string strRouter5, string strRouter6, string strRouter7, string strRFC1, string strRFC2, string strRFC3, string strRFC4, string strL11, string strL12, string strL13)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_DSL values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}')", strID, strAdsl1, strAdsl2, strAdsl3, strPlus1, strPlus2, strPlus3, strVdsl1, strVdsl2, strVdsl3, strBonding, strRemote1, strRemote2, strXdsl1, strXdsl2, strRouter1, strRouter2, strRouter3, strRouter4, strRouter5, strRouter6, strRouter7, strRFC1, strRFC2, strRFC3, strRFC4, strL11, strL12, strL13);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_DSL", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase_Wireless (新增TestCase_Wireless)
    public static bool InsertTestCase_Wireless(string strID, string strWV1, string strWV2, string strWV3, string strWV4, string strWV5, string strWV6, string strM1, string strM2, string strVW_Channel1, string strVW_Channel2, string strVW_Channel3, string strVW_Channel4, string strVW_Channel5)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_Wireless values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", strID, strWV1, strWV2, strWV3, strWV4, strWV5, strWV6, strM1, strM2, strVW_Channel1, strVW_Channel2, strVW_Channel3, strVW_Channel4, strVW_Channel5);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_Wireless", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase_LTE (新增TestCase_LTE)
    public static bool InsertTestCase_LTE(string strID, string strLte1, string strLte2, string strLte3, string strLte4, string strLte5, string strLte6, string strLte7, string strLte8)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_LTE values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", strID, strLte1, strLte2, strLte3, strLte4, strLte5, strLte6, strLte7, strLte8);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_LTE", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase_Wifi (新增TestCase_Wifi)
    public static bool InsertTestCase_Wifi(string strID, string strWifi1, string strWifi2, string strWifi3, string strWifi4, string strWpa2_1, string strWpa2_2, string strBand1, string strBand2, string strWifiItem1, string strWifiItem2, string strWifiItem3, string strWifiItem4, string strWifiItem5, string strWifiItem6, string strCashY, string strCashN, string strAskey1, string strAskey2, string strMoney)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_WiFi values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", strID, strWifi1, strWifi2, strWifi3, strWifi4, strWpa2_1, strWpa2_2, strBand1, strBand2, strWifiItem1, strWifiItem2, strWifiItem3, strWifiItem4, strWifiItem5, strWifiItem6, strCashY, strCashN, strAskey1, strAskey2, strMoney);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_Wifi", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase_USB (新增TestCase_USB)
    public static bool InsertTestCase_USB(string strID, string strUsbCase1, string strUsbCase2, string strUsbCase3, string strCashY, string strCashN, string strAskey1, string strAskey2, string strMoney)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_USB values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", strID, strUsbCase1, strUsbCase2, strUsbCase3, strCashY, strCashN, strAskey1, strAskey2, strMoney);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_USB", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertTestCase_Bluetooth (新增TestCase_Bluetooth)
    public static bool InsertTestCase_Bluetooth(string strID, string strBlueCase1, string strBlueCase2, string strBlueCase3, string strBlueCase4, string strCashY, string strCashN, string strAskey1, string strAskey2, string strMoney)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestCase_Bluetooth values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", strID, strBlueCase1, strBlueCase2, strBlueCase3, strBlueCase4, strCashY, strCashN, strAskey1, strAskey2, strMoney);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_USB", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertAuthority (新增權限)
    public static bool InsertAuthority(string strNo, string strName, string strParent)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Authority values (");
        strSQL.AppendFormat("'{0}','{1}','{2}'", strName, strNo, strParent);
        strSQL.Append(",'Y')");

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertUser_USB", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (ApplicationForm使用)
    public static bool UpDateApplicationForm(string strStatus, string strID, string strCustomer)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Project Set ");
        //if (strStatus == "Reject")
        //if (strStatus == "Open")
        //    strSQL.AppendFormat("Status = '{0}',Customer = '{1}'  where id= '{2}' ", strStatus, strCustomer, strID);
        //else
        strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
        //else
        //    strSQL.AppendFormat("Assign = '{0}',Status = '{1}',End_Date = '{2}' where id= '{3}' ", strAssign, strStatus,strEnd, strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateApplicationForm", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (ApplicationFile使用)
    public static bool UpDateApplicationFile(string strPath, string strID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Attachmen_File Set ");
        //if (strStatus == "Reject")

        strSQL.AppendFormat("File_Path = '{0}' where project_id= '{1}' and file_name='{2}' ", strPath, strID, strName);
        //else
        //    strSQL.AppendFormat("Assign = '{0}',Status = '{1}',End_Date = '{2}' where id= '{3}' ", strAssign, strStatus,strEnd, strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelProjectData (Project使用)
    public static bool DelProjectData(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Project WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelProject", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelProjectCaseData (Project使用)
    public static bool DelProjectCaseData(string strID, string strKind, string strMethod)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        if (strMethod == "0")
            strSQL.AppendFormat("Delete dbo.ProjectCase WHERE project_id= '{0}' and kind = '{1}'", strID, strKind);
        else
            strSQL.AppendFormat("Delete dbo.ProjectCase WHERE project_id= '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelProjectCaseData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelProjectCaseData_Temporarily (Project使用)
    public static bool DelProjectCaseData_Temporarily(string strID, string strKind, string strMethod)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        if (strMethod == "0")
            strSQL.AppendFormat("Delete dbo.ProjectCase_Temporarily WHERE project_id= '{0}' and kind = '{1}'", strID, strKind);
        else
            strSQL.AppendFormat("Delete dbo.ProjectCase_Temporarily WHERE project_id= '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelProjectCaseData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelProjectTaskData (Project使用)
    public static bool DelProjectTaskData(string strID, string strKind, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.ProjectCase WHERE project_id= '{0}' and kind = '{1}' and name = '{2}'", strID, strKind, strName);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelProjectTaskData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelUploadFiles  (UploadFiles使用)
    public static bool DelUploadFiles(string strName, string strID, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Attachmen_File WHERE File_Name= '{0}' and Project_ID = '{1}' and ProjectCase_Kind = '{2}'", strName, strID, strKind);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelUploadFiles", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelUploadFiles  (UploadFiles使用)
    public static bool DelUploadProjectFiles(string strFileName, string strProjectName, string strPath)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Attachmen_File_Project WHERE File_Name= '{0}' and ProjectName = '{1}' and File_Path = '{2}'", strFileName, strProjectName, strPath);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelUploadFiles", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelUploadFilesCase  (ProjectTask使用)
    public static bool DelUploadFilesCase(string strName, string strPID, string strCaseID, string strMethod)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        if (strMethod == "0")
            strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE File_Name= '{0}'", strName);
        else if (strMethod == "1")
            strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE ProjectCase_ID= '{0}' and Project_ID = '{1}'", strCaseID, strPID);
        else
            strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE Project_ID = '{0}'", strPID);

        //if (strName != "")
        //    strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE File_Name= '{0}'", strName);
        //else
        //    strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE ProjectCase_ID= '{0}' and Project_ID = '{1}'", strCaseID, strPID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("DelUploadFilesCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelUploadFilesCase  (ProjectTask使用)
    public static bool DelUploadFilesCase1(string strName, string strPID, string strCaseID, string strMethod)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);


        strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE File_Name= '{0}' and Project_ID ='{1}'", strName, strPID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("DelUploadFilesCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelSampleFilesCase  (ProjectTask使用)
    public static bool DelSampleFilesCase(string strName, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Sample_File WHERE File_Name= '{0}' and Sample_ID ='{1}'", strName, strID);
        //if (strName != "")
        //    strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE File_Name= '{0}'", strName);
        //else
        //    strSQL.AppendFormat("Delete dbo.Attachmen_File_Case WHERE ProjectCase_ID= '{0}' and Project_ID = '{1}'", strCaseID, strPID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("DelUploadFilesCase", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelAuthority  (AddUser使用)
    public static bool DelAuthority(string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Authority WHERE Login_ID= '{0}'", strName);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DelAuthority", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateProjectFunctionData (Project使用)
    public static bool UpdateProjectFunctionData(string strID, string strName, string strKind, string strAssign, string strCustomer, string strPM, string strSW, string strHW, string strMechanical, string strA_Department, string strFW, string strWireless, string strProduct, string strNPI, string strPCB, string strChipset, string strMac, string strUtility, string strDSP, string strStart, string strEnd, string strResult, string strStatus, string strExplain, string strProgress, string strSample1, string strTeam, string strRelated, string strJira, string strDQA, string strLocal, string strA_Department2)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Project Set ");
        strSQL.AppendFormat("Name = '{0}',Kind = '{1}',Assign = '{2}',Customer = '{3}',PM = '{4}',SW_Engineer = '{5}',HW_Engineer = '{6}',Mechanical_Engineer = '{7}',A_Department = '{8}',FW_Version = '{9}',WirelessDrive = '{10}',Customer_Product_Name = '{11}',NPI = '{12}',PCB_Version = '{13}',Chipset = '{14}',Sample_Mac_address = '{15}',Utility_Version = '{16}',DSP_Model = '{17}',Start_Date = '{18}',End_Date = '{19}',Result = '{20}',Status = '{21}',Explain = '{22}',Progress = '{23}',Sample_Ready_Date='{24}',Team='{25}',Related='{26}',Jira='{27}' ,DQA='{28}' ,Accepted_Team='{29}' ,A_Department2 = '{30}'", strName, strKind, strAssign, strCustomer, strPM, strSW, strHW, strMechanical, strA_Department, strFW, strWireless, strProduct, strNPI, strPCB, strChipset, strMac, strUtility, strDSP, strStart, strEnd, strResult, strStatus, strExplain, strProgress, strSample1, strTeam, strRelated, strJira, strDQA, strLocal, strA_Department2);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpdateProjectFunctionData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateSampleName (SampleRelease使用)
    public static bool UpdateSampleName(string strID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Sample Set ");
        strSQL.AppendFormat("Name = '{0}'", strName);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SampleRelease", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateSample (ModifySample使用)
    public static bool UpdateSample(string strID, string strKind, string strFunction, string strItem, string strNumber, string strCategory, string strVendor, string strName, string strMAC, string strPHY, string strFirmware, string strPhysical, string strVoIP, string strCATV, string strUSB, string strLAN, string strWLAN, string strWPS, string strStatus, string strPlace, string strCustodian, string strNote, string strNameCode, string strAgent, string strLocal)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Sample_New Set ");
        strSQL.AppendFormat("Kind = '{0}' ,Function_Name = '{1}' ,Item = '{2}' ,Number = '{3}' ,Category = '{4}' ,Vendor = '{5}' ,ModelName = '{6}' ,MAC = '{7}' ,PHY = '{8}' ,Firmware = '{9}' ,Physical = '{10}' ,VoIP = '{11}' ,CATV = '{12}' ,USB = '{13}' ,LAN = '{14}' ,WLAN = '{15}' ,WPS = '{16}',ReservationStatus = '{17}',Place = '{18}',Custodian = '{19}',Note = '{20}',NameCode = '{21}',Agent = '{22}',Custodian_Department = '{23}'", strKind, strFunction, strItem, strNumber, strCategory, strVendor, strName, strMAC, strPHY, strFirmware, strPhysical, strVoIP, strCATV, strUSB, strLAN, strWLAN, strWPS, strStatus, strPlace, strCustodian, strNote, strNameCode, strAgent, strLocal);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifySample", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateProjectCaseFunctionData (ProjectCase使用)
    public static bool UpdateProjectCaseFunctionData(string strID, string strCaseID, string strName, string strAssign, string strStart, string strEnd, string strResult, string strStatus, string strExplain, string strProgress, string strPU, string strModel, string strLab, string strQuoted, string strReimburse)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.ProjectCase Set ");
        strSQL.AppendFormat("Name = '{0}',Assign = '{1}',Start_Date = '{2}',End_Date = '{3}',Result = '{4}',Status = '{5}',Explain_Case = '{6}',Progress = '{7}',Sub_PU = '{8}',Model_Name = '{9}',Lab = '{10}',Quoted = '{11}',Reimburse = '{12}'", strName, strAssign, strStart, strEnd, strResult, strStatus, strExplain, strProgress, strPU, strModel, strLab, strQuoted, strReimburse);
        strSQL.AppendFormat(" where Project_ID = '{0}' and ID = '{1}'", strID, strCaseID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpdateProjectCaseFunctionData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateDashBoardFunctionData (Project使用)
    public static bool UpdateDashBoardFunctionData(string strID, string strAssign, string strCustomer, string strPM, string strSW, string strHW, string strMechanical, string strFW, string strWireless, string strProduct, string strNPI, string strPCB, string strChipset, string strMac, string strUtility, string strDSP, string strStart, string strEnd, string strResult, string strStatus, string strExplain, string strProgress, string strSample1, string strTeam, string strA_Department2)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Project Set ");
        strSQL.AppendFormat("Assign = '{0}',Customer = '{1}',PM = '{2}',SW_Engineer = '{3}',HW_Engineer = '{4}',Mechanical_Engineer = '{5}',FW_Version = '{6}',WirelessDrive = '{7}',Customer_Product_Name = '{8}',NPI = '{9}',PCB_Version = '{10}',Chipset = '{11}',Sample_Mac_address = '{12}',Utility_Version = '{13}',DSP_Model = '{14}',Start_Date = '{15}',End_Date = '{16}',Result = '{17}',Status = '{18}',Explain = '{19}',Progress = '{20}',Sample_Ready_Date='{21}',Team='{22}',A_Department2='{23}'", strAssign, strCustomer, strPM, strSW, strHW, strMechanical, strFW, strWireless, strProduct, strNPI, strPCB, strChipset, strMac, strUtility, strDSP, strStart, strEnd, strResult, strStatus, strExplain, strProgress, strSample1, strTeam, strA_Department2);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyDashBoard", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateDashBoardCaseFunctionData (ModifyDashBoard使用)
    public static bool UpdateDashBoardCaseFunctionData(string strID, string strCaseID, string strAssign, string strStart, string strEnd, string strResult, string strStatus, string strExplain, string strProgress)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.ProjectCase Set ");
        strSQL.AppendFormat("Assign = '{0}',Start_Date = '{1}',End_Date = '{2}',Result = '{3}',Status = '{4}',Explain_Case = '{5}',Progress = '{6}'", strAssign, strStart, strEnd, strResult, strStatus, strExplain, strProgress);
        strSQL.AppendFormat(" where Project_ID = '{0}' and ID = '{1}'", strID, strCaseID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyDashBoard", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpdateEmployeesData (AddUser使用)
    public static bool UpdateEmployeesData(string strID, string strLogin, string strName, string strDepartment, string strTeam, string strPosition, string strExt, string strPhone, string strAddress, string strEmail, string strLocation, string strPwd, string strLeader, string strManager, string strWrite)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Employees Set ");
        strSQL.AppendFormat("Name_CH = '{0}',Department = '{1}',Team = '{2}',Position = '{3}',Extension = '{4}',PhoneNumber = '{5}',Address = '{6}',Email = '{7}',Location = '{8}',Password = '{9}', TeamLeader = '{10}', Manager = '{11}', Write = '{12}'", strName, strDepartment, strTeam, strPosition, strExt, strPhone, strAddress, strEmail, strLocation, strPwd, strLeader, strManager, strWrite);
        strSQL.AppendFormat(" where Name_En = '{0}'", strLogin);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpdateEmployeesData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (ProjectItem使用)
    public static bool UpDateProjectItemData(string strExplain, string strKind, string strID, string strNameNew)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.ProjectCase Set ");
        strSQL.AppendFormat("explain_kind = '{0}' , kind= '{1}'", strExplain, strNameNew);
        strSQL.AppendFormat("where kind= '{0}' and project_id = '{1}'", strKind, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UpDateProjectItemData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApparatus (新增User)
    public static bool InsertApparatus(string strID, string strProducts_ID, string strName, string strKind, string strPart_No, string strBrand, string strModel, string strNumber, string strIMEI, string strInspection, string strMaintenance, string strPlace, string strCustodian, string strCustodianD, string strFeature, string strSpec, string strNote, string strRStatus, string strOS, string strOS_VR, string strAgent, string strName_En, string strMF, string strProcurement_staff, string strMF_Number, string strCost, string strYears, string strDays, string strPrice)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Apparatus values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}')", strID, strProducts_ID, strName, strKind, strPart_No, strBrand, strModel, strNumber, strIMEI, strInspection, strMaintenance, strPlace, strCustodian, strCustodianD, strFeature, strSpec, strNote, strRStatus, strOS, strOS_VR, strAgent, strName_En, strMF, strProcurement_staff, strMF_Number, strCost, strYears, strDays, strPrice);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddAparatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertGoods (新增Goods)
    public static bool InsertGoods(string strID, string strName_EN, string strName_CH, string strKind, string strMF_EN, string strMF_CH, string strMF_Mail, string strCustodian, string strCheck_Date, string strQuantity_Stock, string strQuantity_Safety, string strPlace, string strStatus, string strNote, string strPart_No, string Products_ID, string strAgent, string strMF_Number, string strBrand, string strDep)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Goods values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", strID, strName_EN, strName_CH, strKind, strMF_EN, strMF_CH, strMF_Mail, strPart_No, strCustodian, strCheck_Date, strQuantity_Stock, strQuantity_Safety, strPlace, strStatus, strNote, Products_ID, strAgent, strMF_Number, strBrand, strDep);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddGoods", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertPR (新增PR)
    public static bool InsertPR(string strID, string strApplication_Date, string strPR_No, string strPR_Date, string strsigned_ID, string strNote, string strStatus, string strDemand_Person, string strEmail, string strNotification_Date, string strLocal)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.PurchasingRequisition values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", strID, strApplication_Date, strPR_No, strPR_Date, strsigned_ID, strNote, strStatus, strDemand_Person, strEmail, strNotification_Date, strLocal);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPR", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUploadFile_Apparatus (新增File)
    public static bool InsertUploadFile_Apparatus(string strID, string strName, string strPath1)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Apparatus_File values (");
        strSQL.AppendFormat("'{0}','{1}','{2}')", strID, strName, strPath1); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddApparatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUploadFile_Sample (新增File)
    public static bool InsertUploadFile_Sample(string strID, string strName, string strPath1, string strTime, string strEmp)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Sample_File values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", strID, strName, strPath1, strTime, strEmp); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddSample", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertUploadFile_Goods (新增File)
    public static bool InsertUploadFile_Goods(string strID, string strName, string strPath1)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Goods_File values (");
        strSQL.AppendFormat("'{0}','{1}','{2}')", strID, strName, strPath1); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_Goods", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertProjectMessage
    public static bool InsertProjectMessage(string strID, string strMessage, string strMessageTime, string strMessageUser, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.ProjectMessage values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", strID, strKind, strMessage, strMessageTime, strMessageUser); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectMessage", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (Apparatus使用)
    public static bool DelApparatus(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Apparatus WHERE id= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchApparatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (Goods使用)
    public static bool DelGoods(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Goods WHERE id= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchGoods", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (Sample使用)
    public static bool DelSample(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Sample WHERE id= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchSample", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (Sample使用)
    public static bool DelSample1(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Sample_New WHERE id= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchSample", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (SampleRelease使用)
    public static bool DelSampleRelease(string strItem, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        if (strKind == "0")
            strSQL.AppendFormat("Delete dbo.Sample_Release WHERE Sample_ID= '{0}' ", strItem);
        else
            strSQL.AppendFormat("Delete dbo.Sample_Release WHERE ID= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SearchSample", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete ApparatusFile  (ApparatusView使用)
    public static bool DelApparatusFile(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Apparatus_File WHERE Apparatus_ID= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete TestLogFile  (TestLogView使用)
    public static bool DelTestLogFile(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Auto_TestLog WHERE ID= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_TestLogView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion



    #region Delete GoodsFile  (GoodsView使用)
    public static bool DelGoodsFile(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Goods_File WHERE Goods_ID= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_GoodsView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete PR_Goods  (GoodsView使用)
    public static bool DelPR_Goods(string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.PR_Detail WHERE Goods_ID= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_GoodsView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApparatus (ModifyApparatus使用)
    public static bool UpDateApparatus(string strID, string strProducts_ID, string strName, string strKind, string strPart_No, string strBrand, string strModel, string strNumber, string strIMEI, string strInspection, string strMaintenance, string strPlace, string strCustodian, string strFeature, string strSpec, string strNote, string strRStatus, string strOS, string strOS_VR, string strAgent, string strName_En, string strMF, string strProcurement_staff, string strMF_Number, string strCost, string strYears, string strDays, string strPrice, string strDep)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Apparatus Set ");
        strSQL.AppendFormat("Products_ID = '{0}',Name = '{1}',Kind = '{2}',Part_No = '{3}',Brand = '{4}',Model = '{5}',Number = '{6}',IMEI = '{7}',InspectionDate = '{8}',MaintenanceDate = '{9}',Place = '{10}',Custodian = '{11}',Feature = '{12}',Spec = '{13}',Note = '{14}',ReservationStatus = '{15}',OS = '{16}',OS_VR = '{17}',Agent = '{18}',Name_En = '{19}',MF = '{20}',Procurement_staff = '{21}',MF_Number = '{22}',Cost_Price = '{23}',Years_Use = '{24}',Days_Use = '{25}',Price_Use = '{26}',Custodian_Department='{27}'", strProducts_ID, strName, strKind, strPart_No, strBrand, strModel, strNumber, strIMEI, strInspection, strMaintenance, strPlace, strCustodian, strFeature, strSpec, strNote, strRStatus, strOS, strOS_VR, strAgent, strName_En, strMF, strProcurement_staff, strMF_Number, strCost, strYears, strDays, strPrice, strDep);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyApparatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateGoods (ModifyGoods使用)
    public static bool UpDateGoods(string strID, string strName_EN, string strName_CH, string strKind, string strMF_EN, string strMF_CH, string strMF_Mail, string strCustodian, string strCheck_Date, string strQuantity_Stock, string strQuantity_Safety, string strPlace, string strStatus, string strNote, string strPart_No, string strAgent, string strMF_Number, string strBrand, string strDep)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Goods Set ");
        strSQL.AppendFormat("Name_EN = '{0}',Name_CH = '{1}',Kind = '{2}',MF_EN = '{3}',MF_CH = '{4}',Procurement_staff = '{5}',Custodian = '{6}',Check_Date = '{7}',Quantity_Stock = '{8}',Quantity_Safety = '{9}',Place = '{10}',Status = '{11}',Note = '{12}',Part_No = '{13}',Agent = '{14}',MF_Number = '{15}',Brand = '{16}',Custodian_Department='{17}'", strName_EN, strName_CH, strKind, strMF_EN, strMF_CH, strMF_Mail, strCustodian, strCheck_Date, strQuantity_Stock, strQuantity_Safety, strPlace, strStatus, strNote, strPart_No, strAgent, strMF_Number, strBrand, strDep);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyGoods", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateGoodsQuantity (PR_Detail使用)
    public static bool UpDateGoodsQuantity(string strID, string strQuantity)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Goods Set ");
        strSQL.AppendFormat("Quantity_Stock = '{0}'", strQuantity);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyGoods", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateSampleRelease (AddSampleRelease使用)
    public static bool UpDateSampleRelease(string strID, string strSID, string strMAC, string strNPI, string strTotal, string strCustodian, string strProvide, string strReceiveDate, string strReturnDate, string strNote)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Sample_Release Set ");
        strSQL.AppendFormat("MAC = '{0}',NPI = '{1}',Total = '{2}',Custodian = '{3}',Provide = '{4}',ReceiveDate = '{5}',ReturnDate = '{6}',Note = '{7}'", strMAC, strNPI, strTotal, strCustodian, strProvide, strReceiveDate, strReturnDate, strNote);
        strSQL.AppendFormat(" where ID = '{0}'and Sample_ID = '{1}'", strID, strSID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyApparatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateRequirement (AddRequirement使用)
    public static bool UpDateRequirement(string strID, string strRequirement_ID, string strDoc_Ver, string strRequirement_Date, string strDescription, string strRequirement_Table, string strFigure, string strOwner, string strPurposeKeyword, string strTestStepsKeyword, string strExpectedResultsKeyword, string strAssociate1, string strAssociate2, string strKind, string strCustomer, string strProduct_Name, string strNumber, string strCheck, string strReview)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Requirement Set ");
        strSQL.AppendFormat("Requirement_ID = '{0}',Doc_Ver = '{1}',Requirement_Date = '{2}',Description = '{3}',Requirement_Table = '{4}',Figure = '{5}',Owner = '{6}',PurposeKeyword = '{7}',TestStepsKeyword = '{8}',ExpectedResultsKeyword = '{9}',Associate1 = '{10}',Associate2 = '{11}',Kind = '{12}',Customer = '{13}',Product_Name = '{14}',Number = '{15}',Review = '{16}',Check_Requirement = '{17}'", strRequirement_ID, strDoc_Ver, strRequirement_Date, strDescription, strRequirement_Table, strFigure, strOwner, strPurposeKeyword, strTestStepsKeyword, strExpectedResultsKeyword, strAssociate1, strAssociate2, strKind, strCustomer, strProduct_Name, strNumber, strCheck, strReview);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddRequirement", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateTestPlanRequirement (AddRequirement使用)
    public static bool UpDateTestPlanRequirement(string strID, string strRequirementID_B)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestPlan Set ");
        strSQL.AppendFormat("RequirementID_B = '{0}'", strRequirementID_B);
        strSQL.AppendFormat(" where ID = '{0}'", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddRequirement", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelApparatusFiles (ModifyApparatus使用)
    public static bool DelApparatusFiles(string strName, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Apparatus_File WHERE File_Name= '{0}' and Apparatus_ID ='{1}'", strName, strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ModifyApparatus", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region DelReservation_Date (DelReservation_Date使用)
    public static bool DelReservation_Date(string strDate, string strDate1, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Reservation WHERE StartDate >= '{0}' and StartDate < '{1}' and Apparatus_ID ='{2}'", strDate, strDate1, strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UploadReservation", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertApparatusReservation (新增設備預約)
    public static bool InsertApparatusReservation(string strApparatus_ID, string strStartDate, string strEndDate, string strBorrower, string strDepartment, string strExt, string strEmail, string strMission, string strGName, string strReturnDate, string strCustodian, string strStatus, string strCaseID, string strBorrowedQuantity, string strAgent, string strAgentExt, string strAgentEmail, string strCustomer, string strPrice, string strContinuousCount, string strPeriod, string strUseKind, string strNote, string strCustodian_Check, string strAdmin_Check)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Reservation values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')", strApparatus_ID, strStartDate, strEndDate, strBorrower, strDepartment, strExt, strEmail, strMission, strGName, strReturnDate, strStatus, strCaseID, null, strBorrowedQuantity, strAgent, strAgentExt, strAgentEmail, strCustomer, strPrice, strContinuousCount, strCustodian_Check, strAdmin_Check, strPeriod, strUseKind, strNote); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusReservation", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertGoodsReservation (新增貨品預約)
    public static bool InsertGoodsReservation(string strGoods_ID, string strStartDate, string strEndDate, string strBorrower, string strExt, string strEmail, string strMission, string strGName, string strReturnDate, string strStatus, string strContinuousDate, string strBorrowedQuantity, string strContinuousBorrowed, string strAgent, string strAgentExt, string strAgentEmail, string strContinuousCount, string strSurplusCount, string strReturn_First, string strContinuousStatus)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.GoodsReservation values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", strGoods_ID, strStartDate, strEndDate, strBorrower, strExt, strEmail, strMission, strGName, strReturnDate, strStatus, strContinuousDate, strBorrowedQuantity, strContinuousBorrowed, strAgent, strAgentExt, strAgentEmail, strContinuousCount, strSurplusCount, strReturn_First, strContinuousStatus); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusReservation", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertPR_Detail (新增採購明細)
    public static bool InsertPR_Detail(string strPR_ID, string strGoodsID, string strUnit, string strPurchase_Quantity, string strDemand_Team, string strDemand_Person, string strProcurement_Staff, string strCurrency, string strEstimated_Price, string strUS_Price, string strEstimated_TotalPrice, string strArrival_Date, string strCheck_Date, string strStatus, string strNote, string strExchangeRate)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.PR_Detail values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", strPR_ID, strGoodsID, strUnit, strPurchase_Quantity, strDemand_Team, strDemand_Person, strProcurement_Staff, strCurrency, strEstimated_Price, strUS_Price, strEstimated_TotalPrice, strArrival_Date, strCheck_Date, strStatus, strNote, strExchangeRate); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_PR_Detail", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertRequirement (新增Requirement)
    public static bool InsertRequirement(string strRequirement_ID, string strDoc_Ver, string strRequirement_Date, string strDescription, string strRequirement_Table, string strFigure, string strOwner, string strPurposeKeyword, string strTestStepsKeyword, string strExpectedResultsKeyword, string strAssociate1, string strAssociate2, string strKind, string strCustomer, string strProduct_Name, string strNumber, string strCheck, string strReview)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Requirement values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')", strRequirement_ID, strDoc_Ver, strRequirement_Date, strDescription, strRequirement_Table, strFigure, strOwner, strPurposeKeyword, strTestStepsKeyword, strExpectedResultsKeyword, strAssociate1, strAssociate2, strKind, strCustomer, strProduct_Name, strNumber, strReview, strCheck); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddRequirement", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertReservationDate (新增設備預約)
    public static bool InsertReservationDate(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Reservation_DailyReport values (");
        strSQL.AppendFormat("'{0}','{1}')", strID, ""); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ApparatusReservation", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateReservation(string strStatus, string strID, string strEndDate, string strKind, string strStartDate, string strKind1)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation Set ");

        if (strKind1 == "Apparatus")
        {
            strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
        }
        else
        {
            if (strKind == "1")
                strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
            else if (strKind == "2")
                strSQL.AppendFormat("Status = '{0}',EndDate = '{1}' where id= '{2}' ", strStatus, strEndDate, strID);
            else
                strSQL.AppendFormat("Status = '{0}',StartDate = '{1}',EndDate = '{2}' where id= '{3}' ", strStatus, strStartDate, strEndDate, strID);
        }



        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateReservation1(string strStatus, string strID, string strEndDate)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation Set ");
        strSQL.AppendFormat("Status = '{0}' ,ReturnDate='{1}' where id= '{2}' ", strStatus, strEndDate, strID);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateReservation1(string strStatus, string strID, string strEndDate, string strKind, string strStartDate)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        //strSQL.Append("Update dbo.Reservation Set ");
        //if ((strKind == "0") || (strKind == "1"))
        //{
        //    if (strStatus == "N")
        //        strSQL.AppendFormat("Status = '{0}',Custodian_Check = 'Y' where id= '{1}' ", strStatus, strID);
        //    else
        //        strSQL.AppendFormat("Custodian_Check = 'Y' where id= '{0}' ", strID);
        //}
        //else if (strKind == "2")
        //    strSQL.AppendFormat("Status = '{0}',StartDate = '{1}',EndDate = '{2}' ,Admin_Check = 'Y' where id= '{3}' ", strStatus, strStartDate, strEndDate, strID);
        ////else
        ////    strSQL.AppendFormat("Status = '{0}',StartDate = '{1}',EndDate = '{2}' where id= '{3}' ", strStatus, strStartDate, strEndDate, strID);
        strSQL.Append("Update dbo.Reservation Set ");

        strSQL.AppendFormat("Status = '{0}' ,Admin_Check = 'Y' ,Custodian_Check = 'Y' where id= '{1}' ", strStatus, strID);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateGoodsReservation1(string strStatus, string strID, string strCount, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.GoodsReservation Set ");

        if (strKind == "0")
        {
            if ((strStatus == "E") || (strStatus == "C") || (strCount == "E"))
                strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
            else
                strSQL.AppendFormat("Return_First='Y' where id= '{0}' ", strID);
        }
        else
        {
            //if ((strStatus == "E") || (strStatus == "C") || (strCount == "E"))
            if (strStatus == "C")
                strSQL.AppendFormat("ContinuousDate = '',ContinuousCount='',Status = '{0}' where id= '{1}' ", strStatus, strID);
            else
                strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
            //else
            //    strSQL.AppendFormat("Return_First='Y' where id= '{0}' ", strID);

        }
        //if (strKind == "1")
        //    strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
        //else if (strKind == "2")
        //    strSQL.AppendFormat("Status = '{0}',EndDate = '{1}' where id= '{2}' ", strStatus, strEndDate, strID);
        //else
        //    strSQL.AppendFormat("Status = '{0}',StartDate = '{1}',EndDate = '{2}' where id= '{3}' ", strStatus, strStartDate, strEndDate, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateGoodsReservation(string strStatus, string strID, string strEndDate, string strKind, string strStartDate)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation Set ");
        if (strKind == "1")
            strSQL.AppendFormat("Status = '{0}' where id= '{1}' ", strStatus, strID);
        else if (strKind == "2")
            strSQL.AppendFormat("Status = '{0}',EndDate = '{1}' where id= '{2}' ", strStatus, strEndDate, strID);
        else
            strSQL.AppendFormat("Status = '{0}',StartDate = '{1}',EndDate = '{2}' where id= '{3}' ", strStatus, strStartDate, strEndDate, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (PR_Detail使用)
    public static bool UpDatePR_Detail(string strPR_ID, string strGoodsID, string strUnit, string strPurchase_Quantity, string strDemand_Team, string strDemand_Person, string strProcurement_Staff, string strCurrency, string strEstimated_Price, string strUS_Price, string strEstimated_TotalPrice, string strArrival_Date, string strCheck_Date, string strStatus, string strNote, string strExchangeRate)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.PR_Detail Set ");
        strSQL.AppendFormat("Unit = '{0}',Purchase_Quantity = '{1}',Demand_Team = '{2}',Demand_Person = '{3}',Procurement_Staff = '{4}',Currency = '{5}',Estimated_Price = '{6}',US_Price = '{7}',Estimated_TotalPrice = '{8}',Arrival_Date = '{9}',Check_Date = '{10}',Status = '{11}',Note = '{12}' ,ExchangeRate= '{13}' where PR_ID= '{14}' and Goods_ID= '{15}'  ", strUnit, strPurchase_Quantity, strDemand_Team, strDemand_Person, strProcurement_Staff, strCurrency, strEstimated_Price, strUS_Price, strEstimated_TotalPrice, strArrival_Date, strCheck_Date, strStatus, strNote, strExchangeRate, strPR_ID, strGoodsID);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_PR_Detail", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (PR_Detail使用)
    public static bool UpDatePR(string strID, string strApplication_Date, string strPR_No, string strPR_Date, string strSigned_ID, string strNote, string strDemand_Person, string strEmail, string strNotification_Date, string strStatus, string strAcceptedTeam)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.PurchasingRequisition Set ");
        strSQL.AppendFormat("Application_Date = '{0}',PR_No = '{1}',PR_Date = '{2}',Signed_ID = '{3}',Note = '{4}',Status = '{5}',Demand_Person = '{6}',Email = '{7}',Notification_Date = '{8}' ,Accepted_Team = '{9}' where ID= '{10}' ", strApplication_Date, strPR_No, strPR_Date, strSigned_ID, strNote, strStatus, strDemand_Person, strEmail, strNotification_Date, strAcceptedTeam, strID);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_PR_Detail", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplicationCaseFile (AddInfo使用)
    public static bool UpDateApplicationCaseFile(string strFile_Name, string strFile_Path, string strKind_ID, string strFunction_ID, string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Item Set ");
        strSQL.AppendFormat("File_Name = '{0}',File_Path = '{1}' where Kind_ID = '{2}' and Function_ID = '{3}' and Item = '{4}' ", strFile_Name, strFile_Path, strKind_ID, strFunction_ID, strItem);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplicationCaseFile (AddInfo使用)
    public static bool UpDateApplicationCaseFile1(string strFile_Name, string strFile_Path, string strKind_ID, string strFunction_ID, string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Item Set ");
        strSQL.AppendFormat("File_Name1 = '{0}',File_Path1 = '{1}' where Kind_ID = '{2}' and Function_ID = '{3}' and Item = '{4}' ", strFile_Name, strFile_Path, strKind_ID, strFunction_ID, strItem);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplicationCaseNPI (AddInfo使用)
    public static bool UpDateApplicationCaseNPI(string strNPI, string strKind_ID, string strFunction_ID, string strItem, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Item Set ");

        if (strKind == "0")
            strSQL.AppendFormat("Level1 = '{0}' where Kind_ID = '{1}' and Function_ID = '{2}' and Item = '{3}' ", strNPI, strKind_ID, strFunction_ID, strItem);
        else if (strKind == "1")
            strSQL.AppendFormat("Level2 = '{0}' where Kind_ID = '{1}' and Function_ID = '{2}' and Item = '{3}' ", strNPI, strKind_ID, strFunction_ID, strItem);
        else if (strKind == "2")
            strSQL.AppendFormat("Note = '{0}' where Kind_ID = '{1}' and Function_ID = '{2}' and Item = '{3}' ", strNPI, strKind_ID, strFunction_ID, strItem);
        else
            strSQL.AppendFormat("Cost = '{0}' where Kind_ID = '{1}' and Function_ID = '{2}' and Item = '{3}' ", strNPI, strKind_ID, strFunction_ID, strItem);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateExplanationFile (AddInfo使用)
    public static bool UpDateExplanationFile(string strFile_Name, string strFile_Path, string strKind_ID, string strItem)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Explanation_Item Set ");
        strSQL.AppendFormat("File_Name = '{0}',File_Path = '{1}' where Kind_ID = '{2}' and Item = '{3}' ", strFile_Name, strFile_Path, strKind_ID, strItem);


        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateReservationContinuous(string strID, string strEndDate, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation Set ");

        if (strKind == "0")
            strSQL.AppendFormat("EndDate = '{0}',ContinuousDate = null where id= '{1}' ", strEndDate, strID);
        else
            strSQL.AppendFormat("ContinuousDate = null where id= '{0}' ", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (Reservation使用)
    public static bool UpDateReservationContinuous1(string strID, string strEndDate, string strKind, string strEmp)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Reservation Set ");

        if (strKind == "0")
        {
            if ((strEmp == "0") || (strEmp == "1"))
                strSQL.AppendFormat("Custodian_Check='Y',Admin_Check='Y' where id= '{1}' ", strEndDate, strID);
                //strSQL.AppendFormat("Custodian_Check='Y' where id= '{1}' ", strEndDate, strID);
            else
                strSQL.AppendFormat("EndDate = '{0}',ContinuousDate = null,Admin_Check='Y' where id= '{1}' ", strEndDate, strID);
        }
        else
        {
            if ((strEmp == "0") || (strEmp == "1"))
                strSQL.AppendFormat("ContinuousDate = null,Custodian_Check='Y',Admin_Check='Y' where id= '{0}' ", strID);
                //strSQL.AppendFormat("ContinuousDate = null,Custodian_Check='Y' where id= '{0}' ", strID);
            else
                strSQL.AppendFormat("ContinuousDate = null,Admin_Check='Y' where id= '{0}' ", strID);
        }

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Data (GoodsReservation使用)
    public static bool UpDateGoodsReservationContinuous(string strID, string strEndDate, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.GoodsReservation Set ");

        if (strKind == "0")
            strSQL.AppendFormat("ContinuousStatus='' where id= '{0}' ", strID);
        else
            strSQL.AppendFormat("ContinuousCount = null,ContinuousStatus='' where id= '{0}' ", strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Data  (InfoData使用)
    public static bool DelReservation(string strProjectID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Reservation WHERE Project_ID= '{0}' ", strProjectID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete User  (UserView1使用)
    public static bool DelEmployees(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Employees WHERE Name_En= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UserView1", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete User  (UserView1使用)
    public static bool DelEmployees_PND(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr_PND);

        strSQL.AppendFormat("Delete dbo.Employees WHERE Name_En= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UserView1", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete Number  (UserView1使用)
    public static bool DelNumber(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Number WHERE id= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UserView1", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete User  (UserView1使用)
    public static bool DelEmployees_Authority(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.Authority WHERE Name_En= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_UserView1", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete FilePath_Kind  (UserView1使用)
    public static bool DelFilePath_Kind(string strID, string strName, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        if (strKind == "0")
            strSQL.AppendFormat("Delete dbo.FilePath_Kind WHERE FilePath_TestCase_ID= '{0}' and File_Kind ='{1}' ", strID, strName);
        else
            strSQL.AppendFormat("Delete dbo.FilePath_Kind WHERE FilePath_TestCase_ID= '{0}'", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPath", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete FilePath_TestCase  (UserView1使用)
    public static bool DelFilePath_TestCase(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.FilePath_TestCase WHERE ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddPath", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Delete DepartmentAccount  (DepartmentAccountView使用)
    public static bool DelDepartmentAccount(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.DepartmentAccount WHERE ID= '{0}' ", strID);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_DepartmentAccountView", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertExcelToSQL (Excel)
    public static bool InsertExcelToSQL(string strKind, string strRequirementID, string strCategory, string strSubCategory, string strPurpose, string strEnvironmentSetup, string strTestSteps, string strExpectedResults, string strTestResult, string strBugTicketID, string strRDComment, string strCustomer, string strP_Name, string strNumber)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.TestPlan values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", strKind, strRequirementID, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name, "", strNumber);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddDepartmentAccount", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertExcelToSQL (Excel)
    public static bool InsertExcelToSQL1(string strKind, string strCategory, string strSubCategory, string strPurpose, string strEnvironmentSetup, string strTestSteps, string strExpectedResults, string strTestResult, string strBugTicketID, string strRDComment, string strCustomer, string strP_Name, string strNumber)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strKind = strKind.Replace("'", @"""");
        strCategory = strCategory.Replace("'", @"""");
        strSubCategory = strSubCategory.Replace("'", @"""");
        strPurpose = strPurpose.Replace("'", @"""");
        strEnvironmentSetup = strEnvironmentSetup.Replace("'", @"""");
        strTestSteps = strTestSteps.Replace("'", @"""");
        strBugTicketID = strBugTicketID.Replace("'", @"""");
        strRDComment = strRDComment.Replace("'", @"""");
        strCustomer = strCustomer.Replace("'", @"""");
        strTestResult = strTestResult.Replace("'", @"""");
        strExpectedResults = strExpectedResults.Replace("'", @"""");


        strSQL.Append("Insert into dbo.TestPool values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", strKind, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name, strNumber);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddDepartmentAccount", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region Update Path (ProjectEdit使用)
    public static bool UpDatePath(string strPathnew, string strPathold, string strName, string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.Attachmen_File Set ");
        if (strName == " ")
            strSQL.AppendFormat("File_Path = '{0}' where File_Path= '{1}' ", strPathnew, strPathold);
        else
            strSQL.AppendFormat("File_Path = '{0}',ProjectCase_Kind='{1}' where File_Path= '{2}' and project_id='{3}' ", strPathnew, strName, strPathold, strID);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ReservationAssign", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region 發送MAIL
    public static bool SendMail(string strTo, string strSubject, string strBody)
    {
        #region 舊的發MAIL方式
        //MailMessage mail = new MailMessage();
        //string strSmtpHost = WebConfigurationManager.AppSettings["SmtpHost"];
        //mail.IsBodyHtml = true;
        ////mail.BodyEncoding = System.Text.Encoding.Default 
        //mail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        //mail.SubjectEncoding = System.Text.Encoding.Default;

        //mail.From = new MailAddress(strFrom, strEmpName, Encoding.Default);

        //#region 多筆收件人
        ////if (strTo != null)
        ////{
        ////    for (int i = 0; i <= strTo.Length - 1; i++)
        ////    {
        ////        mail.To.Add(new MailAddress(strTo(i)));
        ////    }
        ////}        
        //#endregion

        ////單一收件人
        //mail.To.Add(new MailAddress(strTo));

        //mail.Subject = strSubject;
        //mail.Body = strBody;
        #endregion

        bool isExist = false;
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("EXEC msdb.dbo.sp_send_dbmail ");
        strSQL.AppendFormat("@profile_name = 'SIT',@recipients='{0}',@body='{1}',@body_format='HTML',@subject='{2}'", strTo, strBody, strSubject);

        //SqlParameter[] para;
        //strSQL.Append("EXEC sp_SendMail @sp_profile_name,@sp_recipients,@sp_body,@sp_subject,@sp_body_format ;");
        //para = new SqlParameter[]
        //            {
        //                new SqlParameter("@sp_profile_name", "SIT"),
        //                new SqlParameter("@sp_recipients", strTo),
        //                new SqlParameter("@sp_body", strBody),
        //                new SqlParameter("@sp_subject", strSubject),
        //                new SqlParameter("@sp_body_format", "HTML"),
        //            };
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (Exception ex)
        {
            MicroSovaComponent.Log.LogObject.writeLog("Web_SendMail", ex.Message);
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertSample (新增樣品)
    public static bool InsertSample(string strID, string strName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Sample values (");
        strSQL.AppendFormat("'{0}','{1}')", strID, strName); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SampleRelease", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertSample_N (新增樣品)
    public static bool InsertSample_N(string strID, string strKind, string strFunction, string strItem, string strNumber, string strCategory, string strVendor, string strName, string strMAC, string strPHY, string strFirmware, string strPhysical, string strVoIP, string strCATV, string strUSB, string strLAN, string strWLAN, string strWPS, string strStatus, string strPlace, string strCustodian, string strNote, string strCode, string strAgent, string strLocal)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Sample_New values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')", strID, strKind, strFunction, strItem, strNumber, strCategory, strVendor, strName, strMAC, strPHY, strFirmware, strPhysical, strVoIP, strCATV, strUSB, strLAN, strWLAN, strWPS, strStatus, strPlace, strCustodian, strNote, strCode, strAgent, strLocal); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddSample", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertSampleRelease (新增樣品)
    public static bool InsertSampleRelease(string strID, string strSID, string strMAC, string strNPI, string strTotal, string strCustodian, string strProvide, string strReceiveDate, string strReturnDate, string strNote)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Sample_Release values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", strID, strSID, strMAC, strNPI, strTotal, strCustodian, strProvide, strReceiveDate, strReturnDate, strNote); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_SampleRelease", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertBenchmarkImport (Los轉檔)
    public static bool InsertLosToSQL(string strKind, string strCustomer, string strName, string strNPI)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Los_Info values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}')", strKind, strCustomer, strName, strNPI); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_BenchmarkImport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertLosDataToSQL (Los轉檔)
    public static bool InsertLosDataToSQL(string strID, string strKind, string strType, string strAtt, string strDistance, string strChannel, string strAngle, string strThroughput)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Los_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", strID, strKind, strType, strAtt, strDistance, strChannel, strAngle, strThroughput); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_BenchmarkImport", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion

    #region Delete ModelWeb  (AddInfo使用)
    public static bool DelModelWeb(string strItem, string strKind)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        if (strKind == "0")
            strSQL.AppendFormat("Delete dbo.Function_List WHERE Function_No= '{0}' and Parent_Function_No='0' ", strItem);
        else
            strSQL.AppendFormat("Delete dbo.Function_List WHERE Parent_Function_No= '{0}' ", strItem);
        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region InsertModelWeb (新增ModelWeb)
    public static bool InsertModelWeb(string strID, string strParentID, string strName, string strUrl, string strExpand, string strSequence, string strModel)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.Function_List values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}')", strID, strParentID, strName, strUrl, strExpand, strSequence, strModel); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_InsertInfoData", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    //=================================debbie SIT Benchmark 20180503====================================
    #region InsertInformationToSQL (Los轉檔)
    public static bool InsertLosInformationToSQL(string strID, string strAskModelName, string strLanMAC, string str24WLanMAC, string str5WLanMAC, string strMainChipset, string strChipsetNum, string strEthType, string strBootVersion, string str24Mimo, string str5Mimo, string strFrequencyBand, string strCusModelName, string strHWVersion, string strFWVersion, string strBOMVersion, string str24WLanChipset, string str5WLanChipset, string str24WLanChipsetNum, string str5WLanChipsetNum, string strReportNPI, string strDriverVersion, string strReportBand, string strReportBandwidth, string strUploadDate, string strReportVersion, string strFileName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Los_Info values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}')", strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, strBOMVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strUploadDate, strReportVersion, strFileName); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion

    #region InsertLosDataToSQL (Los轉檔)
    public static bool InsertLosDataToSQL(string strID, string strProject_ID, string strProtocol, string strBand, string strBandwidth, string strType, string strAtt, string strDistance, string strFequency, string strChannel, string strAngle, string strThroughput)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Los_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", strID, strProject_ID, strProtocol, strBand, strBandwidth, strType, strAtt, strDistance, strFequency, strChannel, strAngle, strThroughput); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion

    #region InsertLosAngleToSQL (Los轉檔)
    public static bool InsertLosAngleToSQL(string strID, string strProject_ID, string strProtocol, string strBand, string strBandwidth, string strType, string strAtt, string strFequency, string strChannel, string strAngle, string strThroughput)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Los_Angle values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", strID, strProject_ID, strProtocol, strBand, strBandwidth, strType, strAtt, strFequency, strChannel, strAngle, strThroughput); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion

    #region Delete InformationToSQL (Los轉檔)
    public static bool DelInformationToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Los_Info WHERE Project_id= '{0}'", strID); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion

    #region Delete LosAngleToSQL (Los轉檔)
    public static bool DelLosAngleToSQL(string strProject_ID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Los_Angle WHERE Project_id= '{0}'", strProject_ID); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion

    #region Delete LosDataToSQL (Los轉檔)
    public static bool DelLosDataToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Los_Data WHERE Project_id= '{0}'", strID); bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_ProjectTask", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
        //return true;
    }
    #endregion
    //=================================debbie SIT Benchmark 20180503====================================

    #region UpDateApplication_Item (AddInfo使用)
    public static bool UpDateApplication_Item(string strItem, string strKind_ID, string strFunction_ID, string strItem_New, int intKind, int intKind1, string path1, string path2)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Item Set ");
        strSQL.AppendFormat("Item = '{0}'", strItem_New);
        if (intKind == 1)
        {
            strSQL.AppendFormat(", File_Path = '{0}'", path1);
        }
        if (intKind1 == 1)
        {
            strSQL.AppendFormat(", File_Path1 = '{0}'", path2);
        }
        strSQL.AppendFormat(" where disable <>'Y' and Kind_ID = '{0}' and Function_ID = '{1}' and Item = '{2}' ", strKind_ID, strFunction_ID, strItem);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }
    #endregion

    #region UpDateApplication_Function (AddInfo使用)
    public static bool UpDateApplication_Function(string strFunction, string strKind_ID, string strFunction_New)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Update dbo.TestCase_Function Set ");
        strSQL.AppendFormat("Name = '{0}'", strFunction_New);
        strSQL.AppendFormat(" where disable <>'Y' and Kind_ID = '{0}' and Name = '{1}' ", strKind_ID, strFunction);

        bool isExist = false;
        try
        {
            sqlConn.openConnection();
            sqlConn.beginTransaction();
            sqlConn.executeSql(strSQL.ToString(), null, CommandType.Text);
            sqlConn.commitTransaction();
            isExist = true;
            return isExist;
        }
        catch (System.Exception ex)
        {
            isExist = false;
            MicroSovaComponent.Log.LogObject.writeLog("Web_AddInfo", ex.Message);
            sqlConn.rollbackTransaction();
            return isExist;
        }
        finally
        {
            sqlConn.closeConnection();
        }
    }    
    #endregion
}