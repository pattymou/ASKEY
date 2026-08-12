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
/// clsData 的摘要描述
/// </summary>
public class clsBM
{
    #region 取得系統連線字串

    private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    #endregion

    #region 找尋Project (使用ID搜尋)
    public static DataTable UploadProjectQuery(string strID, string strTable)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from ");
        //strSQL.Append("from " );


        if (strTable == "Project")
            strSQL.AppendFormat("{0} WHERE ID = '{1}'", strTable, strID);
        else
            strSQL.AppendFormat("{0} WHERE  Project_ID = '{1}'", strTable, strID);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region getProjectCase1(PrjectCase用)
    public static DataTable getProjectCase1(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from ProjectCase ");
        strSQL.AppendFormat("where Project_ID = '{0}'", strID);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region 找尋LosInfo最後一筆
    public static DataTable UploadLosInfoLastIDQuery()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select MAX(ID) as ID from BM_Los_Info");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋MeshInfo最後一筆
    public static DataTable UploadMeshInfoLastIDQuery()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select MAX(ID) as ID from BM_Mesh_Info");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋LosInfoID
    public static DataTable UploadLosInfoID(string Project_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select ID from BM_Los_Info where Project_ID='{0}'", Project_ID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋LosInfo
    public static DataTable UploadLosInfoQuery(string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from BM_Los_Info");

        strSQL.AppendFormat(" where kind ='{0}'", strKind);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region InsertInformationToSQL (Los轉檔)
    public static bool InsertLosInformationToSQL(string strID, string strAskModelName, string strLanMAC, string str24WLanMAC, string str5WLanMAC, string strMainChipset, string strChipsetNum, string strEthType, string strBootVersion, string str24Mimo, string str5Mimo, string strFrequencyBand, string strCusModelName, string strHWVersion, string strFWVersion, string strBOMVersion, string str24WLanChipset, string str5WLanChipset, string str24WLanChipsetNum, string str5WLanChipsetNum, string strReportNPI, string strDriverVersion, string strReportBand, string strReportBandwidth, string strLocation, string strBandMode, string strUploadDate, string strReportVersion, string strFileName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Los_Info values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}')", strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, strBOMVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strLocation, strBandMode, strUploadDate, strReportVersion, strFileName); bool isExist = false;
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

    #region InsertInformationToSQL (MeshRvR轉檔)
    public static bool InsertMeshInformationToSQL(string strID, string strAskModelName, string strLanMAC, string str24WLanMAC, string str5WLanMAC, string str5WLanMAC2, string strHWVersion, string strPCBVersion, string strBootVersion, string str24Mimo, string str5Mimo, string str5Mimo2, string strCusModelName, string strEthType, string strMainChipset, string str24WLanChipset, string str5WLanChipset, string str5WLanChipset2, string str24WLanChipsetNum, string str5WLanChipsetNum, string str5WLanChipsetNum2, string strReportNPI, string strDriverVersion, string strReportBand, string strFrequencyBand, string strReportBandwidth, string strLocation, string strToday, string strReportVersion, string strFile_Name)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Mesh_Info values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}')", strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, str5WLanMAC2, strHWVersion, strPCBVersion, strBootVersion, str24Mimo, str5Mimo, str5Mimo2, strCusModelName, strEthType, strMainChipset, str24WLanChipset, str5WLanChipset, str5WLanChipset2, str24WLanChipsetNum, str5WLanChipsetNum, str5WLanChipsetNum2, strReportNPI, strDriverVersion, strReportBand, strFrequencyBand, strReportBandwidth, strLocation, strToday, strReportVersion, strFile_Name); bool isExist = false;
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

    #region InsertMeshDataToSQL (MeshRvR轉檔)
    public static bool InsertMeshDataToSQL(string strID, string strProject_ID, string strSheet, string strMode, string strBand, string strBandwidth, string strDirection, string strDirectionName, string strAtt, string strChannel, string strThroughput)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Mesh_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", strID, strProject_ID, strSheet, strMode, strBand, strBandwidth, strDirection, strDirectionName, strAtt, strChannel, strThroughput); bool isExist = false;
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

    #region Delete MeshDataToSQL (MeshRvR轉檔)
    public static bool DelMeshDataToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Mesh_Data WHERE Project_id= '{0}'", strID); bool isExist = false;
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

    #region Delete MeshInformationToSQL (MeshRvR轉檔)
    public static bool DelMeshInformationToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Mesh_Info WHERE Project_id= '{0}'", strID); bool isExist = false;
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

    #region 找尋MeshInfoID
    public static DataTable UploadMeshInfoID(string Project_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select ID from BM_Mesh_Info where Project_ID='{0}'", Project_ID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋IndoorInfoID
    public static DataTable UploadIndoorInfoID(string Project_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select ID from BM_Indoor_Info where Project_ID='{0}'", Project_ID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region InsertIndoorInformationToSQL (Indoor轉檔)
    public static bool InsertIndoorInformationToSQL(string strID, string strAskModelName, string strLanMAC, string str24WLanMAC, string str5WLanMAC, string strMainChipset, string strChipsetNum, string strEthType, string strBootVersion, string str24Mimo, string str5Mimo, string strFrequencyBand, string strCusModelName, string strHWVersion, string strFWVersion, string strBOMVersion, string str24WLanChipset, string str5WLanChipset, string str24WLanChipsetNum, string str5WLanChipsetNum, string strReportNPI, string strDriverVersion, string strReportBand, string strReportBandwidth, string strLocation, string strBandMode, string strWLanCard, string strUploadDate, string strReportVersion, string strFileName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Indoor_Info values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}')", strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, strBOMVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strLocation, strBandMode, strWLanCard, strUploadDate, strReportVersion, strFileName); bool isExist = false;
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

    #region InsertIndoorDataToSQL (Indoor轉檔)
    public static bool InsertIndoorDataToSQL(string strID, string strProject_ID, string strMode, string strBand, string strBandwidth, string strChannel, string strDirection, string strTestPoint, string strRssi, string strAngle, string strThroughput)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_Indoor_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", strID, strProject_ID, strMode, strBand, strBandwidth, strChannel, strDirection, strTestPoint, strRssi, strAngle, strThroughput); bool isExist = false;
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

    #region Delete IndoorInformationToSQL (Indoor轉檔)
    public static bool DelIndoorInformationToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Indoor_Info WHERE Project_id= '{0}'", strID); bool isExist = false;
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

    #region Delete IndoorDataToSQL (Indoor轉檔)
    public static bool DelIndoorDataToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_Indoor_Data WHERE Project_id= '{0}'", strID); bool isExist = false;
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

    #region InsertOTAInformationToSQL (OTA轉檔)
    public static bool InsertOTAInformationToSQL(string strID, string strAskModelName, string strLanMAC, string str24MAC, string str5MAC, string strMainChipset, string strChipsetNum, string strEthType, string strBootVersion, string str24Mimo, string str5Mimo, string strFrequencyBand, string strCusModelName, string strHWVersion, string strFWVersion, string str24WLanChipset, string str5WLanChipset, string str24WLanChipsetNum, string str5WLanChipsetNum, string strReportNPI, string strDriverVersion, string strReportBand, string strReportBandwidth, string strLocation, string strBandMode, string strUploadDate, string strReportVersion, string strFileName)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_OTA_Info values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')", strID, strAskModelName, strLanMAC, str24MAC, str5MAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strLocation, strBandMode, strUploadDate, strReportVersion, strFileName); bool isExist = false;
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

    #region 找尋OTAInfoID
    public static DataTable UploadOTAInfoID(string Project_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select ID from BM_OTA_Info where Project_ID='{0}'", Project_ID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region InsertOTADataToSQL (Los轉檔)
    public static bool InsertOTADataToSQL(string strID, string strProject_ID, string strMode, string strBand, string strBandMode, string strBandwidth, string strDirection, string strFequency, string strChannel, string strChannel2, string strThroughput)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.Append("Insert into dbo.BM_OTA_Data values (");
        strSQL.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10})", strID, strProject_ID, strMode, strBand, strBandMode, strBandwidth, strDirection, strFequency, strChannel, strChannel2, strThroughput); bool isExist = false;
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

    #region Delete OTADataToSQL (MeshRvR轉檔)
    public static bool DelOTADataToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_OTA_Data WHERE Project_id= '{0}'", strID); bool isExist = false;
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

    #region Delete OTAInformationToSQL (MeshRvR轉檔)
    public static bool DelOTAInformationToSQL(string strID)
    {
        StringBuilder strSQL = new StringBuilder();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

        strSQL.AppendFormat("Delete dbo.BM_OTA_Info WHERE Project_id= '{0}'", strID); bool isExist = false;
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
}
