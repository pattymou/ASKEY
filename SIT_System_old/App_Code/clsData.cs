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
public class clsData
{
    #region 取得系統連線字串

    private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    #endregion

    #region 找尋InfoData
    public static DataTable UploadInfoDataProductQuery(string strCustomer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ProductName as Name,value,ID ");
        strSQL.Append("from ProjectList ");
        strSQL.AppendFormat("WHERE Customer = '{0}' Order by ProductName", strCustomer);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋InfoData
    public static DataTable UploadInfoDataQuery(int intrKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * ");
        strSQL.Append("from InfoData ");
        strSQL.AppendFormat("WHERE Kind = '{0}' Order by Name", intrKind);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋WeeklyReportCase
    public static DataTable UploadWeeklyReportCase(string strPID, string strCID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select p.Name,(c.Kind + '--' + c.Name) as Item from Project as p,ProjectCase as c where p.ID=c.Project_ID and c.Project_ID ='{0}' and c.ID ='{1}'", strPID, strCID);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    //#region 找尋WeeklyReportCase
    //public static DataTable UploadWeeklyReportCase_Leader(string strPID)
    //{
    //    MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
    //    StringBuilder strSQL = new StringBuilder();

    //    strSQL.AppendFormat("select * from Project where ID ='{0}'", strPID);
    //    DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
    //    return dt;
    //}
    //#endregion


    #region 找尋InfoData
    public static DataTable UploadInfoData_Value(string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * ");
        strSQL.Append("from InfoData ");
        strSQL.AppendFormat("WHERE Name = '{0}'", strName);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion


    #region 找尋PR_Detail
    public static DataTable UploadPR_DetailQuery(int intKind, string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (intKind == 0)
            strSQL.AppendFormat("select * from pr_detail where pr_id = '{0}'", strID);
        else
            strSQL.AppendFormat("select (g.Name_CH + '-' + g.Name_En) as name,g.Part_No,g.Kind,d.* from PR_Detail as d,Goods as g where d.PR_ID = '{0}' and d.Goods_ID =g.ID ", strID);
        //strSQL.AppendFormat("select (g.Name_CH + '-' + g.Name_En) as name,g.Part_No,g.Kind,d.PR_ID,d.Goods_ID,d.Unit,d.Purchase_Quantity,rtrim(d.Demand_Team) as Demand_Team,d.Demand_Person ,d.Procurement_Staff,d.Currency,d.Estimated_Price,d.US_Price,d.Estimated_TotalPrice ,d.Arrival_Date,d.Check_Date,d.Status,d.Note from PR_Detail as d,Goods as g where d.PR_ID = '{0}' and d.Goods_ID =g.ID ", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPlanName
    public static DataTable UploadTestPlanNameQuery(string strKind, string strCustomer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select DISTINCT ProductName from TestPlan ");
        //strSQL.Append("from InfoData ");
        strSQL.AppendFormat("where Kind='{0}' and Customer ='{1}'", strKind, strCustomer);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Requirement
    public static DataTable UploadRequirementQuery(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Requirement ");
        //strSQL.Append("from InfoData ");
        strSQL.AppendFormat("where ID='{0}'", strID);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋PlanID
    public static DataTable UploadPlanIDQuery(string strKind, string strCustomer, string strP_Name, string strReview)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from TestPlan ");
        //strSQL.AppendFormat("where Kind='{0}' and Customer='{1}' and Product_Name='{2}'", strKind, strCustomer, strP_Name);
        int intI = 0;

        if (strKind != "ALL")
        {
            strSQL.AppendFormat("where Kind='{0}'", strKind);
            intI = 1;
        }

        if (intI == 0)
        {
            if (strCustomer != "ALL")
                strSQL.AppendFormat("where Customer='{0}'", strCustomer);
        }
        else
        {
            if (strCustomer != "ALL")
                strSQL.AppendFormat("and Customer='{0}'", strCustomer);
        }

        if (intI == 0)
        {
            if (strP_Name != "ALL")
                strSQL.AppendFormat("where  ProductName='{0}'", strP_Name);
        }
        else
        {
            if (strP_Name != "ALL")
                strSQL.AppendFormat("and ProductName='{0}'", strP_Name);
        }

        //if (intI == 0)
        //{
        //    if (strReview != "")
        //        strSQL.AppendFormat("where  Review='{0}'", strReview);

        //}
        //else
        //{
        //    if (strReview != "")
        //        strSQL.AppendFormat("and Review='{0}'", strReview);
        //}

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Summary
    public static DataTable UploadSummary1(string strNo)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select Requirement.Owner ,TestPlan.ID,TestPlan.Kind,TestPlan.Category,TestPlan.TestResult from TestPlan,Requirement where TestPlan.ID=Requirement .ID ");
        //strSQL.AppendFormat("where Kind='{0}' and Customer='{1}' and Product_Name='{2}'", strKind, strCustomer, strP_Name);
        int intI = 0;

        strSQL.AppendFormat("and TestPlan.ID='{0}'", strNo);

        //strSQL.AppendFormat(" group by t.Category,t.Kind ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Summary
    public static DataTable UploadVerification(string strID, string strRandom)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Number where ");
        strSQL.AppendFormat("ID='{0}' and Random='{1}'", strID, strRandom);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_TestTeam
    public static DataTable UploadApplication_TestTeam(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select Custodian_Team from testcase_kind where id = '{0}'", strID);
        //strSQL.AppendFormat("where Kind='{0}' and Customer='{1}' and Product_Name='{2}'", strKind, strCustomer, strP_Name);
        int intI = 0;


        //strSQL.AppendFormat(" group by t.Category,t.Kind ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_TestTeam
    public static DataTable Customer(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from InfoData where name like '%{0}%' and kind='1'", strID);
        //strSQL.AppendFormat("where Kind='{0}' and Customer='{1}' and Product_Name='{2}'", strKind, strCustomer, strP_Name);
        int intI = 0;


        //strSQL.AppendFormat(" group by t.Category,t.Kind ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion


    #region 找尋Summary
    public static DataTable UploadSummary(string strKind, string strCustomer, string strP_Name)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //strSQL.Append("select t.Category,(select r.Owner + '/' from Requirement as r where r.Kind =t.Kind for XML path('')) as owner,COUNT(t.Category) as testplan1  ,(select COUNT(t.TestResult) from TestPlan as t where t.TestResult ='Pass' ");

        //strSQL.AppendFormat("and t.Kind='{0}' and t.Customer='{1}' and t.ProductName='{2}') as Pass,(select COUNT(t.TestResult) from TestPlan as t where t.TestResult ='Fail' and t.Kind='{0}' and t.Customer='{1}' and t.ProductName='{2}') as Fail,(select COUNT(t.TestResult) from TestPlan as t where t.TestResult ='TBD' and t.Kind='{0}' and t.Customer='{1}' and t.ProductName='{2}') as TBD,(select COUNT(t.TestResult) from TestPlan as t where t.TestResult ='N/T' and t.Kind='{0}' and t.Customer='{1}' and t.ProductName='{2}') as NT,(select COUNT(t.TestResult) from TestPlan as t where t.TestResult ='N/A' and t.Kind='{0}' and t.Customer='{1}' and t.ProductName='{2}') as NA from TestPlan as t ", strKind, strCustomer, strP_Name);

        strSQL.Append("select * from TestPlan as t ");

        int intI = 0;

        if (strKind != "ALL")
        {
            strSQL.AppendFormat("where t.Kind='{0}'", strKind);
            intI = 1;
        }

        if (intI == 0)
        {
            if (strCustomer != "ALL")
                strSQL.AppendFormat("where t.Customer='{0}'", strCustomer);
        }
        else
        {
            if (strCustomer != "ALL")
                strSQL.AppendFormat("and t.Customer='{0}'", strCustomer);
        }

        if (intI == 0)
        {
            if (strP_Name != "ALL")
                strSQL.AppendFormat("where t.ProductName='{0}'", strP_Name);
        }
        else
        {
            if (strP_Name != "ALL")
                strSQL.AppendFormat("and t.ProductName='{0}'", strP_Name);
        }

        //if (intI == 0)
        //{
        //    if (strReview != "")
        //        strSQL.AppendFormat("where  Review='{0}'", strReview);

        //}
        //else
        //{
        //    if (strReview != "")
        //        strSQL.AppendFormat("and Review='{0}'", strReview);
        //}

        //strSQL.AppendFormat(" group by t.Category,t.Kind ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion



    #region 找尋RequirementID
    public static DataTable UploadRequirementIDQuery(string strKind, string strCustomer, string strP_Name, string strReview)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Requirement ");
        //strSQL.AppendFormat("where Kind='{0}' and Customer='{1}' and Product_Name='{2}'", strKind, strCustomer, strP_Name);
        int intI = 0;

        if (strKind != "ALL")
        {
            strSQL.AppendFormat("where Kind='{0}'", strKind);
            intI = 1;
        }

        if (intI == 0)
        {
            if (strCustomer != "ALL")
                strSQL.AppendFormat("where Customer='{0}'", strCustomer);
        }
        else
        {
            if (strCustomer != "ALL")
                strSQL.AppendFormat("and Customer='{0}'", strCustomer);
        }

        if (intI == 0)
        {
            if (strP_Name != "ALL")
                strSQL.AppendFormat("where  Product_Name='{0}'", strP_Name);
        }
        else
        {
            if (strP_Name != "ALL")
                strSQL.AppendFormat("and Product_Name='{0}'", strP_Name);
        }

        if (intI == 0)
        {
            if (strReview != "")
                strSQL.AppendFormat("where  Review='{0}'", strReview);

        }
        else
        {
            if (strReview != "")
                strSQL.AppendFormat("and Review='{0}'", strReview);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Traceibility
    public static DataTable UploadTraceibilityQuery(string strRequirement_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select r.*,(t.Kind+t.Customer+t.ProductName+'-'+t.Number) as TestCaseID,t.ID as PlanID from Requirement as r,TestPlan as t ");
        strSQL.AppendFormat("where r.Requirement_ID =  t.RequirementID and r.Requirement_ID like '%{0}%'", strRequirement_ID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋BenchmarkLos
    public static DataTable UploadBenchmarkLos(string strNo, string strKind, string strChannel, string strType)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("where Kind='{0}' and Customer='{1}' and Product_Name='{2}'", strKind, strCustomer, strP_Name);
        int intI = 0;

        if (strKind == "Distance")
            strSQL.AppendFormat("select DISTINCT distance from bm_los_data where Info_ID ='{0}' order by distance", strNo);
        else if (strKind == "Attenuation")
            strSQL.AppendFormat("select DISTINCT Attenuation from bm_los_data where Info_ID ='{0}' order by Attenuation", strNo);
        else
            strSQL.AppendFormat("select data.*,info.Name from BM_Los_Data as data,BM_Los_Info as info where data.Info_ID='{0}' and data.Channel ='{1}' and data.type='{2}' and data.Info_ID = info.ID order by data.Distance ,data.Attenuation ", strNo, strChannel, strType);
        //strSQL.AppendFormat(" group by t.Category,t.Kind ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPlan
    public static DataTable UploadTestPlanQuery1(string strKind, string strCustomer, string strCategory, string strSearch, string strP_Name, string strRequirementCheck)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from TestPlan ");


        strSQL.AppendFormat("WHERE (SubCategory like '%{0}%' or Purpose like '%{0}%' or TestSteps like '%{0}%' or ExpectedResults like '%{0}%') ", strSearch);

        if ((strKind != "ALL") && (strKind != ""))
            strSQL.AppendFormat("and Kind = '{0}' ", strKind);

        if ((strCustomer != "ALL") && (strCustomer != ""))
            strSQL.AppendFormat("and Customer = '{0}' ", strCustomer);

        if ((strCategory != "ALL") && (strCategory != ""))
            strSQL.AppendFormat("and Category = '{0}' ", strCategory);

        if ((strP_Name != "ALL") && (strP_Name != ""))
            strSQL.AppendFormat("and ProductName = '{0}' ", strP_Name);

        if (strRequirementCheck == "Y")
            strSQL.AppendFormat("and RequirementID = '' ");

        strSQL.AppendFormat("order by ID");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPool
    public static DataTable UploadTestPool(string strKind, string strCustomer, string strCategory, string strSearch, string strP_Name)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from TestPool ");


        strSQL.AppendFormat("WHERE (SubCategory like '%{0}%' or Purpose like '%{0}%' or TestSteps like '%{0}%' or ExpectedResults like '%{0}%') ", strSearch);

        if ((strKind != "ALL") && (strKind != ""))
            strSQL.AppendFormat("and Kind = '{0}' ", strKind);

        if ((strCustomer != "ALL") && (strCustomer != ""))
            strSQL.AppendFormat("and Customer = '{0}' ", strCustomer);

        if ((strCategory != "ALL") && (strCategory != ""))
            strSQL.AppendFormat("and Category = '{0}' ", strCategory);

        if ((strP_Name != "ALL") && (strP_Name != ""))
            strSQL.AppendFormat("and ProductName = '{0}' ", strP_Name);


        strSQL.AppendFormat("order by ID");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPlan
    public static DataTable UploadTestPool1(int intKind, string strSearch, string strTestPlan, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from TestPool ");

        if (intKind == 0)
            strSQL.AppendFormat("WHERE (Category like '%{0}%' or Headline like '%{0}%' or Engineer like '%{0}%') ", strSearch);
        else if (intKind == 1)
            strSQL.AppendFormat("WHERE Category like '%{0}%' ", strSearch);
        else if (intKind == 2)
            strSQL.AppendFormat("WHERE Headline like '%{0}%' ", strSearch);
        else if (intKind == 3)
            strSQL.AppendFormat("WHERE Engineer like '%{0}%' ", strSearch);
        else if (intKind == 4)
            strSQL.AppendFormat("WHERE ID = '{0}' ", strSearch);
        else
            strSQL.AppendFormat("WHERE TestPlanName = '{0}' ", strSearch);

        if ((strTestPlan != "ALL") && (strTestPlan != ""))
            strSQL.AppendFormat("and TestPlanName = '{0}' ", strTestPlan);

        if ((strKind != "ALL") && (strKind != ""))
            strSQL.AppendFormat("and Kind = '{0}' ", strKind);

        strSQL.AppendFormat("order by ID");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPlanRequirement
    public static DataTable UploadTestPlanRequirement(StringBuilder strSQL)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        //StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select * from TestPlan ");
        //strSQL.AppendFormat("WHERE Purpose ='{0}' and TestSteps='{1}' and ExpectedResults='{2}' ", strP_Name, strKind, Customer);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPlan
    public static DataTable UploadTestPlanQuery2(string strP_Name, string strKind, string Customer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from TestPlan ");
        strSQL.AppendFormat("WHERE ProductName ='{0}' and Kind='{1}' and Customer='{2}' ", strP_Name, strKind, Customer);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestPlan
    public static DataTable UploadTestPlanQuery(int intKind, string strSearch, string strTestPlan, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from TestPlan ");

        if (intKind == 0)
            strSQL.AppendFormat("WHERE (Category like '%{0}%' or Headline like '%{0}%' or Engineer like '%{0}%') ", strSearch);
        else if (intKind == 1)
            strSQL.AppendFormat("WHERE Category like '%{0}%' ", strSearch);
        else if (intKind == 2)
            strSQL.AppendFormat("WHERE Headline like '%{0}%' ", strSearch);
        else if (intKind == 3)
            strSQL.AppendFormat("WHERE Engineer like '%{0}%' ", strSearch);
        else if (intKind == 4)
            strSQL.AppendFormat("WHERE ID = '{0}' ", strSearch);
        else
            strSQL.AppendFormat("WHERE TestPlanName = '{0}' ", strSearch);

        if ((strTestPlan != "ALL") && (strTestPlan != ""))
            strSQL.AppendFormat("and TestPlanName = '{0}' ", strTestPlan);

        if ((strKind != "ALL") && (strKind != ""))
            strSQL.AppendFormat("and Kind = '{0}' ", strKind);

        strSQL.AppendFormat("order by ID");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Project (使用申請單編號)
    public static DataTable UploadProjectIDQuery(string strNumber, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID,Name from project ");
        strSQL.AppendFormat("WHERE ID = '{0}' or A_Name ='{1}' and Status =''", strNumber, strName);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Project (使用申請單編號)
    public static DataTable UploadProjectIDQuery1(string strTeam, string strKind, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select ID,Name,kind,team from project where ");
        if (strTeam != "ALL")
            strSQL.AppendFormat("team = '{0}' and ", strTeam);
        if (strKind != "ALL")
            strSQL.AppendFormat("kind = '{0}' and ", strKind);

        strSQL.AppendFormat("name like '%{0}%'", strName);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application
    public static DataTable UploadApplicationIDQuery(string strDep, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID,Name from project ");
        strSQL.AppendFormat("WHERE A_Department = '{0}' and A_Name ='{1}' and Kind='驗証申請' and Status =''", strDep, strName);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application
    public static DataTable UploadApplication_TemporarilyIDQuery(string strDep, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID,Name,Kind from Project_Temporarily ");
        strSQL.AppendFormat("WHERE A_Department = '{0}' and A_Name ='{1}' and (Kind='驗証申請' or Kind='認証申請') and Status =''", strDep, strName);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application
    public static DataTable UploadApplication_TemporarilyIDQuery_A()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Project_Temporarily ");
        strSQL.AppendFormat("WHERE (Kind='驗証申請' or Kind='認証申請') and Status =''");
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusStatus
    public static DataTable UploadApparatusStatus(string strNumber, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select Status from Reservation where ");
        strSQL.AppendFormat("Apparatus_ID ='{0}' and EndDate <='{1}' order by ID desc", strNumber, strDate);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋FilePath
    public static DataTable UploadFilePathQuery(string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select TestCase from FilePath_TestCase as a,filepath_kind as b ");

        strSQL.AppendFormat("where a.id=b.FilePath_TestCase_ID and b.file_kind = '{0}'", strName);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectMessage
    public static DataTable UploadProjectMessageQuery(string strID, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select m.*,p.name from ProjectMessage as m,Project as p ");

        if (strKind == "")
            strSQL.AppendFormat("where m.Project_ID = '{0}' and m.project_id=p.id order by m.MessageTime desc", strID);
        else
            strSQL.AppendFormat("where m.Project_ID = '{0}' and m.project_id=p.id and m.kind='{1}' order by m.MessageTime desc", strID, strKind);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Employees
    public static DataTable UploadEmployeesQuery(string strID, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Employees ");

        if (strID != "")
        {
            if (strID == "Order by")
                strSQL.AppendFormat(" where Department ='{0}' order by team", strLocal);
            else
                strSQL.AppendFormat("where id = '{0}'", strID);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Employees
    public static DataTable UploadTeamEmp(string strTeam, string strLocal)
    {
        string strKind = "0";

        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Employees ");

        if (strTeam != "ALL")
        {
            strKind = "1";
            strSQL.AppendFormat("where Team = '{0}'", strTeam);
        }

        if (strLocal != "")
        {
            if (strKind == "0")
                strSQL.AppendFormat(" where Department = '{0}'", strLocal);
            else
                strSQL.AppendFormat(" and Department = '{0}'", strLocal);
        }


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Number
    public static DataTable UploadNumber(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Number ");

        if (strID != "")
            strSQL.AppendFormat("where id = '{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Project
    public static DataTable UploadProjectDateRange(string strStartDate, string strEndDate, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Project ");

        //if (strKind == "ALL")
        //    strSQL.AppendFormat("where Start_Date >='{0}' and End_Date <='{1}'", strStartDate, strEndDate);
        //else
        //    strSQL.AppendFormat("where Start_Date >='{0}' and End_Date <='{1}' and kind='{2}'", strStartDate, strEndDate, strKind);

        if (strKind != "ALL")
            strSQL.AppendFormat("where kind='{2}'", strStartDate, strEndDate, strKind);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Project
    public static DataTable UploadProjectDateRange1(string strStartDate, string strEndDate, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Project ");

        if (strKind == "ALL")
            strSQL.AppendFormat("where Start_Date >='{0}' and End_Date <='{1}'", strStartDate, strEndDate);
        else
            strSQL.AppendFormat("where Start_Date >='{0}' and End_Date <='{1}' and kind='{2}'", strStartDate, strEndDate, strKind);

        //if (strKind != "ALL")
        strSQL.AppendFormat(" order by name");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Project
    public static DataTable UploadProjectDateRange2(string strStartDate, string strEndDate, string strKind, string strLocal)
    {
        string strKind1 = "0";

        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Project ");

        if (strKind != "ALL")
        {
            //strSQL.AppendFormat("where Start_Date >='{0}' and End_Date <='{1}'", strStartDate, strEndDate);
            //else
            strSQL.AppendFormat("where kind='{2}'", strStartDate, strEndDate, strKind);
            strKind1 = "1";
        }

        if (strLocal != "")
        {
            if (strKind1 == "1")
                strSQL.AppendFormat(" and Accepted_Team ='{0}'", strLocal);
            else
                strSQL.AppendFormat(" where Accepted_Team ='{0}'", strLocal);
        }

        //if (strKind != "ALL")
        strSQL.AppendFormat(" order by name");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DashBoard
    public static DataTable UploadDashBoardQuery(string strKind, string strSearchKind, string strStatus, string strAssign, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();



        //if (strSearchKind == "Personal")
        //{

        if (strKind == "1")
            strSQL.Append("select COUNT(id) as CountCase from ProjectCase ");
        else
            strSQL.Append("select * from ProjectCase ");

        if (strStatus == "Open")
            strSQL.AppendFormat("where Status = 'Open' and CHARINDEX('{1}',Assign)>0 and Start_Date >='{2}' and End_Date <='{3}' and CONVERT(varchar(10),End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        else if (strStatus == "Delay")
            strSQL.AppendFormat("where Status = 'Open' and CHARINDEX('{1}',Assign)>0 and Start_Date >='{2}' and End_Date <='{3}' and CONVERT(varchar(10),End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        else if (strStatus == "Total")
            strSQL.AppendFormat("where CHARINDEX('{0}',Assign)>0 and Start_Date >='{1}' and End_Date <='{2}'", strAssign, strStartDate, strEndDate);
        else
            strSQL.AppendFormat("where Status = '{0}' and CHARINDEX('{1}',Assign)>0 and Start_Date >='{2}' and End_Date <='{3}'", strStatus, strAssign, strStartDate, strEndDate);
        //}
        //else  //Project
        //{
        //    if (strKind == "1")
        //        strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
        //    else
        //        strSQL.Append("select c.*,p.Name from ProjectCase as c,Project as p ");

        //    if (strStatus == "Open")
        //        strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        //    else if (strStatus == "Delay")
        //        strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        //    else if (strStatus == "Total")
        //        strSQL.AppendFormat("where  p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID", strAssign, strStartDate, strEndDate);

        //    else 
        //        strSQL.AppendFormat("where c.Status = '{0}' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID", strStatus, strAssign, strStartDate, strEndDate);
        //}

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectStatistics1
    public static DataTable UploadProjectStatistics1(string strKind, string strStatus, string strID, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        if (strKind == "1")
            strSQL.Append("select COUNT(id) as CountCase from ProjectCase ");
        else
            strSQL.Append("select * from ProjectCase ");

        if (strStatus == "Open")
            strSQL.AppendFormat("where Status = 'Open' and Project_id='{0}' and End_Date >='{1}' and CONVERT(varchar(10),End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strID, strEndDate);
        else if (strStatus == "Delay")
            strSQL.AppendFormat("where Status = 'Open' and Project_id ='{0}' and End_Date <='{1}' and CONVERT(varchar(10),End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strID, strEndDate);
        else if (strStatus == "Total")
            strSQL.AppendFormat("where Project_id ='{0}'", strID);
        else
            strSQL.AppendFormat("where Status = '{0}' and Project_id ='{1}'", strStatus, strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryList
    public static DataTable UploadDashBoardSummaryList(string strKind, string strName, string strNPI, string strFunction, string strItem, string strCustomer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.AppendFormat("select * from Project where Name = '{0}' and Kind = '驗証申請' and Customer = '{1}' and status !='' ", strName, strCustomer);
        else if (strKind == "1")
            strSQL.AppendFormat("select Kind,Name from ProjectCase where {0} and Kind != 'Project Information' group by kind,name order by Kind ", strName);
        else if (strKind == "2")
            strSQL.AppendFormat("select distinct p.ID,p.PCB_Version,p.FW_Version,p.End_Date from Project as p ,ProjectCase as c where p.NPI ='{0}' and p.Name ='{1}' and p.ID=c.Project_ID and p.Kind = '驗証申請' and c.Kind != 'Project Information' and p.Customer = '{2}' and p.status !='' order by p.id desc", strNPI, strName, strCustomer);
        else
            strSQL.AppendFormat("select c.Result from ProjectCase as c,Project as p where c.Project_ID = '{0}' and c.Kind = '{1}' and c.Name = '{2}' and p.ID=c.Project_ID and p.NPI ='{3}' and p.Customer = '{4}' ", strName, strFunction, strItem, strNPI, strCustomer);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDQADashBoard
    public static DataTable UploadDQADashBoard(string strKind, string strName, string strNPI, string strFunction, string strItem, string strCustomer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.AppendFormat("select * from Project where Name = '{0}' and Project_Kind = 'NPI驗証' and Customer = '{1}' and NPI='{2}' and Status='Close' ", strName, strCustomer, strNPI);
        else if (strKind == "1")
            strSQL.AppendFormat("select Kind,Name from ProjectCase where {0} and Kind != 'Project Information' group by kind,name order by Kind ", strName);
        else if (strKind == "2")
            strSQL.AppendFormat("select distinct p.ID,p.PCB_Version,p.FW_Version,p.End_Date from Project as p ,ProjectCase as c where p.NPI ='{0}' and p.Name ='{1}' and p.ID=c.Project_ID and p.Project_Kind = 'NPI驗証' and c.Kind != 'Project Information' and p.Customer = '{2}' order by p.id desc", strNPI, strName, strCustomer);
        else if (strKind == "3")
            strSQL.AppendFormat("select c.Result from ProjectCase as c,Project as p where c.Project_ID = '{0}' and c.Kind = '{1}' and c.Name = '{2}' and p.ID=c.Project_ID and p.NPI ='{3}' and p.Customer = '{4}' ", strName, strFunction, strItem, strNPI, strCustomer);
        else
        {
            strSQL.AppendFormat("select max(p.Project_ID) as id,p.Kind,p.Name,p.Result from ProjectCase as p inner join (select max(p.Project_ID) as id , Kind,Name from (select * from ProjectCase where {0} and Kind != 'Project Information') as p group by kind,name) as p1 on p.Project_ID =p1.id where p.Kind != 'Project Information' group by p.Kind,p.Name,p.Result", strName);

        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryList
    public static DataTable UploadApparatusReport(string strKind, string strSearch, string strSearch1, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a,Reservation as r where a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}' ", strStartDate, strEndDate);

        if (strKind == "0")
        {
            if (strSearch != "ALL")
                strSQL.AppendFormat(" and r.Department='{0}'", strSearch);

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and r.Custodian='{0}'", strSearch1);
            //strSQL.AppendFormat(" and r.Department='{0}' and a.Custodian='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "1")
        {
            if (strSearch != "ALL")
                strSQL.AppendFormat(" and r.Department='{0}'", strSearch);

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and a.Kind = '{0}'", strSearch1);

            //strSQL.AppendFormat(" and r.Department='{0}' and a.Kind='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "2")
            strSQL.AppendFormat(" and a.Products_ID='{0}'", strSearch);
        else if (strKind == "3")
        {
            if (strSearch != "ALL")
                strSQL.AppendFormat(" and a.Kind='{0}'", strSearch);
        }

        strSQL.AppendFormat(" order by a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.Period ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadApparatusReport
    public static DataTable UploadApparatusReport_New(string strStartDate,string strEndDate,string strProduct_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select *,(coalesce([01],0)+coalesce([02],0)+coalesce([03],0)+coalesce([04],0)+coalesce([05],0)+coalesce([06],0)+coalesce([07],0)+coalesce([08],0)+coalesce([09],0)+coalesce([10],0)+coalesce([11],0)+coalesce([12],0)) as total from( ");
        strSQL.AppendFormat("select b.M,b.Department,round((CAST(SUM(b.UseTime) as float) / 60),2) as UseTime from ");
        strSQL.AppendFormat("(select MONTH(startdate) as M,a.Name,r.Department, ");
        strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}') then (DATEDIFF(minute,r.StartDate,r.EndDate)) ", strStartDate, strEndDate);
        strSQL.AppendFormat("when (r.StartDate <= '{0}' and r.EndDate >= '{1}') then (DATEDIFF(minute,'{0}','{1}')) ", strStartDate, strEndDate);
        strSQL.AppendFormat("when (r.EndDate >= '{0}' and r.EndDate <= '{1}') then DATEDIFF(minute,'{0}',r.EndDate) ", strStartDate, strEndDate);
        strSQL.AppendFormat("when (r.StartDate >= '{0}' and r.StartDate <= '{1}') then (DATEDIFF(minute,r.StartDate,'{1}')) end)+1 as UseTime ", strStartDate, strEndDate);
        strSQL.AppendFormat("from Reservation as r,Apparatus as a where a.ID=r.Apparatus_ID and a.Products_ID ='{0}' and ", strProduct_ID);
        strSQL.AppendFormat("((EndDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}' and ReturnDate = '1900-01-01 00:00:00') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("((StartDate >= '{0}' and StartDate <= '{1}'))and  ((ReturnDate >= '{0}' and ReturnDate <= '{1}')) or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}')) ) as b ", strStartDate, strEndDate);
        strSQL.AppendFormat("group by b.Department ,b.M) as v ");
        strSQL.AppendFormat("pivot (sum(usetime) for m in ([01],[02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12])) as v1 ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadAChart_View
    public static DataTable UploadAChart_View(string strKind, string strSearch, string strSearch1, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select * from Apparatus where (ReservationStatus <> '採購中' and ReservationStatus <> '不可借用') ");

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and Custodian='{0}'", strSearch1);
            //strSQL.AppendFormat(" and r.Department='{0}' and a.Custodian='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "1")
        {
            strSQL.AppendFormat("select * from Apparatus where (ReservationStatus <> '採購中' and ReservationStatus <> '不可借用') ");

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and Kind = '{0}'", strSearch1);

            //strSQL.AppendFormat(" and r.Department='{0}' and a.Kind='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "2")
        {
            strSQL.AppendFormat("select * from Apparatus where (ReservationStatus <> '採購中' and ReservationStatus <> '不可借用') ");
            strSQL.AppendFormat(" and Products_ID='{0}'", strSearch);
        }
        else if (strKind == "3")
        {
            strSQL.AppendFormat("select * from Apparatus where (ReservationStatus <> '採購中' and ReservationStatus <> '不可借用') ");
            strSQL.AppendFormat(" and Kind='{0}'", strSearch);
        }

        strSQL.AppendFormat(" and Custodian_Department ='{0}'", strLocal);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadAChart_View1
    public static DataTable UploadAChart_View1(string strKind, string strSearch, string strSearch1, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select * from Apparatus where ReservationStatus <> '採購中' ");

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and Custodian='{0}'", strSearch1);
            //strSQL.AppendFormat(" and r.Department='{0}' and a.Custodian='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "1")
        {
            strSQL.AppendFormat("select * from Apparatus where ReservationStatus <> '採購中' ");

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and Kind = '{0}'", strSearch1);

            //strSQL.AppendFormat(" and r.Department='{0}' and a.Kind='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "2")
        {
            strSQL.AppendFormat("select * from Apparatus where ReservationStatus <> '採購中' ");
            strSQL.AppendFormat(" and Products_ID='{0}'", strSearch);
        }
        else if (strKind == "3")
        {
            strSQL.AppendFormat("select * from Apparatus where ID='{0}'", strSearch);
            //strSQL.AppendFormat("select * from Apparatus where ReservationStatus <> '採購中' ");
            //strSQL.AppendFormat(" and Name='{0}'", strSearch);
            //strSQL.AppendFormat(" and Kind='{0}'", strSearch);
        }

        strSQL.AppendFormat(" and Custodian_Department ='{0}'", strLocal);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadApparatusReportM
    public static DataTable UploadApparatusReportM(string strKind, string strSearch, string strID, string strStartDate, string strEndDate, string strSelect, string strDay, string strKindUse)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strSelect == "0")
        {
            if ((strKind == "0") || (strKind == "1"))
            {
                if (strSearch != "ALL")
                    strSQL.AppendFormat("select distinct Department from Reservation where StartDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and (Status = 'Y' or Status ='E') ", strStartDate, strEndDate, strID, strSearch);
                else
                    strSQL.AppendFormat("select distinct Department from Reservation where StartDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and (Status = 'Y' or Status ='E') ", strStartDate, strEndDate, strID);
            }
            else
                strSQL.AppendFormat("select distinct Department from Reservation where StartDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and (Status = 'Y' or Status ='E') ", strStartDate, strEndDate, strID);
        }
        else
        {
            if ((strKind == "0") || (strKind == "1"))
            {

                if (strSearch != "ALL")
                    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where Department = '{0}',StartDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Period='{4}' and UseKind='{5}' and (Status = 'Y' or Status ='E') group by Department,Period,UseKind order by Department,Period ", strSearch, strStartDate, strEndDate, strID, strDay, strKindUse);
                else
                    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where StartDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and Period='{4}'  and UseKind='{5}' and (Status = 'Y' or Status ='E') group by Department,Period,UseKind order by Department,Period ", strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);

            }
            else
            {
                strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where StartDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and Period='{4}'  and UseKind='{5}' and (Status = 'Y' or Status ='E') group by Department,Period,UseKind order by Department,Period ", strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
            }
        }


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadApparatusReportM1
    public static DataTable UploadApparatusReportM1(string strKind, string strSearch, string strID, string strStartDate, string strEndDate, string strSelect, string strDay, string strKindUse)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();



        if ((strKind == "0") || (strKind == "1"))
        {

            if (strSearch != "ALL")
            {
                //if (strSelect == "0")
                //    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where Department = '{0}'and EndDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Period='{4}' and UseKind='{5}' group by Department,Period,UseKind order by Department,Period ", strSearch, strStartDate, strEndDate, strID, strDay, strKindUse);
                //else
                strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,case when startdate<'{0}' then '{0}' else startdate end),CONVERT(datetime,case when EndDate <'{3}' then '{3}' else EndDate end))+1) as daycount from Reservation where Department = '{1}'and EndDate >='{2}' and EndDate <='{3}' and Apparatus_ID ='{4}' and Period='{5}' and UseKind='{6}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strSearch, strStartDate, strEndDate, strID, strDay, strKindUse);
            }
            else
            {
                //if (strSelect == "0")
                //    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where EndDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and Period='{4}'  and UseKind='{5}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
                //else
                strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,case when startdate<'{0}' then '{0}' else startdate end),CONVERT(datetime,case when EndDate <'{2}' then '{2}' else EndDate end))+1) as daycount from Reservation where EndDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Department ='{4}' and Period='{5}'  and UseKind='{6}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
            }


        }
        else
        {
            //if (strSelect == "0")
            //    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where EndDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and Period='{4}'  and UseKind='{5}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
            //else
            strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,case when startdate<'{0}' then '{0}' else startdate end),CONVERT(datetime,case when EndDate >'{2}' then '{2}' else EndDate end))+1) as daycount from Reservation where EndDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Department ='{4}' and Period='{5}'  and UseKind='{6}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
        }



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadApparatusReportM2
    public static DataTable UploadApparatusReportM2(string strKind, string strSearch, string strID, string strStartDate, string strEndDate, string strSelect)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();



        if ((strKind == "0") || (strKind == "1"))
        {

            if (strSearch != "ALL")
            {
                //if (strSelect == "0")
                //    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where Department = '{0}'and EndDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Period='{4}' and UseKind='{5}' group by Department,Period,UseKind order by Department,Period ", strSearch, strStartDate, strEndDate, strID, strDay, strKindUse);
                //else
                strSQL.AppendFormat("select Department,SUM( DATEDIFF (day,CONVERT(datetime,case when startdate<'{0}' then '{0}' else startdate end),CONVERT(datetime,case when EndDate <'{3}' then '{3}' else EndDate end))+1) as daycount from Reservation where Department = '{1}'and EndDate >='{2}' and EndDate <='{3}' and Apparatus_ID ='{4}' group by Department,Period,UseKind order by Department ", strStartDate, strSearch, strStartDate, strEndDate, strID);
            }
            else
            {
                //if (strSelect == "0")
                //    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where EndDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and Period='{4}'  and UseKind='{5}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
                //else
                strSQL.AppendFormat("select Department,SUM( DATEDIFF (day,CONVERT(datetime,case when startdate<'{0}' then '{0}' else startdate end),CONVERT(datetime,case when EndDate <'{2}' then '{2}' else EndDate end))+1) as daycount from Reservation where EndDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Department ='{4}' and Period='{5}'  and UseKind='{6}' group by Department order by Department ", strStartDate, strStartDate, strEndDate, strID, strSearch);
            }


        }
        else
        {
            //if (strSelect == "0")
            //    strSQL.AppendFormat("select Department,Period,UseKind,SUM( DATEDIFF (day,CONVERT(datetime,StartDate),CONVERT(datetime,EndDate))+1) as daycount from Reservation where EndDate >='{0}' and EndDate <='{1}' and Apparatus_ID ='{2}' and Department ='{3}' and Period='{4}'  and UseKind='{5}' group by Department,Period,UseKind order by Department,Period ", strStartDate, strEndDate, strID, strSearch, strDay, strKindUse);
            //else
            strSQL.AppendFormat("select Department,SUM( DATEDIFF (day,CONVERT(datetime,case when startdate<'{0}' then '{0}' else startdate end),CONVERT(datetime,case when EndDate >'{2}' then '{2}' else EndDate end))+1) as daycount from Reservation where EndDate >='{1}' and EndDate <='{2}' and Apparatus_ID ='{3}' and Department ='{4}'  group by Department,Period,UseKind order by Department ", strStartDate, strStartDate, strEndDate, strID, strSearch);
        }



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryList
    public static DataTable UploadApparatusReport1(string strKind, string strSearch, string strSearch1, string strStartDate, string strEndDate, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();



        //if (strKind == "0")
        //{

        //    if (strSearch != "ALL")
        //        strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}' and r.Department='{2}'  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strStartDate, strEndDate, strSearch);
        //    else
        //        strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}'  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strStartDate, strEndDate);

        //    if (strSearch1 != "ALL")
        //        strSQL.AppendFormat(" and a.Custodian='{0}'", strSearch1);
        //    //strSQL.AppendFormat(" and r.Department='{0}' and a.Custodian='{1}'", strSearch, strSearch1);
        //}
        //else if (strKind == "1")
        //{
        //    if (strSearch != "ALL")
        //        strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}' and r.Department='{2}'  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strStartDate, strEndDate, strSearch);
        //    else
        //        strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}'  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strStartDate, strEndDate);


        //    if (strSearch1 != "ALL")
        //        strSQL.AppendFormat(" and a.Kind = '{0}'", strSearch1);

        //    //strSQL.AppendFormat(" and r.Department='{0}' and a.Kind='{1}'", strSearch, strSearch1);
        //}
        //else if (strKind == "2")
        //{
        //    strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}'  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" and a.Products_ID='{0}'", strSearch);
        //}
        //else if (strKind == "3")
        //{
        //    strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}'  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" and a.Kind='{0}'", strSearch);
        //    //if (strSearch != "ALL")
        //    //    strSQL.AppendFormat(" and a.Kind='{0}'", strSearch);
        //}

        if (strKind == "0")
        {

            if (strSearch != "ALL")
            {
                strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID ");
                strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

                strSQL.AppendFormat(" and r.Department='{0}' and (r.Status = 'Y' or r.Status ='E')  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strSearch);
            }
            else
            {
                strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID ");
                strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

                strSQL.AppendFormat(" and (r.Status = 'Y' or r.Status ='E')  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ");
            }

            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and a.Custodian='{0}'", strSearch1);
            //strSQL.AppendFormat(" and r.Department='{0}' and a.Custodian='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "1")
        {
            if (strSearch != "ALL")
            {
                strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID ");

                strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

                strSQL.AppendFormat(" and r.Department='{0}' and (r.Status = 'Y' or r.Status ='E')  where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ", strSearch);
            }
            else
            {
                strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID ");

                strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
                strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

                strSQL.AppendFormat(" and (r.Status = 'Y' or r.Status ='E') where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用')  ");

            }


            if (strSearch1 != "ALL")
                strSQL.AppendFormat(" and a.Kind = '{0}'", strSearch1);

            //strSQL.AppendFormat(" and r.Department='{0}' and a.Kind='{1}'", strSearch, strSearch1);
        }
        else if (strKind == "2")
        {
            strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID ");
            strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
            strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
            strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
            strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

            strSQL.AppendFormat(" and (r.Status = 'Y' or r.Status ='E') where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用') and a.Products_ID='{0}'", strSearch);
        }
        else if (strKind == "3")
        {
            strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a left join Reservation as r on a.ID=r.Apparatus_ID ");

            strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
            strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
            strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
            strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

            if (strSearch == "DA40-LTE Team")
                strSQL.AppendFormat(" and (r.Status = 'Y' or r.Status ='E') where (a.ReservationStatus <> '採購中') and a.Kind='{0}'", strSearch);
            else
                strSQL.AppendFormat(" and (r.Status = 'Y' or r.Status ='E') where (a.ReservationStatus <> '採購中' and a.ReservationStatus <> '不可借用') and a.Kind='{0}'", strSearch);
            //if (strSearch != "ALL")
            //    strSQL.AppendFormat(" and a.Kind='{0}'", strSearch);
        }

        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        strSQL.AppendFormat(" order by a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.Period ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryList
    public static DataTable UploadApparatusReportM(string strID, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select a.ID,a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.StartDate,r.EndDate,r.Period,r.UseKind  from Apparatus as a,Reservation as r where a.ID=r.Apparatus_ID and r.StartDate >= '{0}' and r.EndDate <='{1}' and r.Apparatus_ID ='{2}' ", strID, strStartDate, strEndDate);

        //if (strKind == "0")
        //{
        //    if (strSearch != "ALL")
        //        strSQL.AppendFormat(" and r.Department='{0}'", strSearch);

        //    if (strSearch1 != "ALL")
        //        strSQL.AppendFormat(" and r.Custodian='{0}'", strSearch1);
        //    //strSQL.AppendFormat(" and r.Department='{0}' and a.Custodian='{1}'", strSearch, strSearch1);
        //}
        //else if (strKind == "1")
        //{
        //    if (strSearch != "ALL")
        //        strSQL.AppendFormat(" and r.Department='{0}'", strSearch);

        //    if (strSearch1 != "ALL")
        //        strSQL.AppendFormat(" and a.Kind = '{0}'", strSearch1);

        //    //strSQL.AppendFormat(" and r.Department='{0}' and a.Kind='{1}'", strSearch, strSearch1);
        //}
        //else if (strKind == "2")
        //    strSQL.AppendFormat(" and a.Products_ID='{0}'", strSearch);
        //else if (strKind == "3")
        //{
        //    if (strSearch != "ALL")
        //        strSQL.AppendFormat(" and a.Kind='{0}'", strSearch);
        //}

        strSQL.AppendFormat(" order by a.Name,a.Products_ID,r.Department,r.Customer,r.GName,r.Period ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryName
    public static DataTable UploadDashBoardSummaryName(string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select distinct Name from Project where Name like '%{0}%'", strName);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryDetail
    public static DataTable UploadDashBoardSummaryDetail(string strName, string strNPI, string strFunction, string strItem, string strCustomer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select p.ID,p.PCB_Version,p.FW_Version,p.End_Date,c.Result,f.File_Name,f.File_Path from Project as p,ProjectCase as c,Attachmen_File_Case as f where p.Name='{0}' and p.NPI='{1}' and c.Kind ='{2}' and c.Name ='{3}' and p.ID=c.Project_ID and f.Project_ID =p.ID and f.ProjectCase_ID =c.ID and (f.File_Name like '%xls%' or f.File_Name like '%pdf%' or f.File_Name like '%doc%' or f.File_Name like '%docx%') and f.File_Name like 'SIT%' and p.Customer = '{4}'", strName, strNPI, strFunction, strItem, strCustomer);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadDashBoardSummaryDetail
    public static DataTable UploadDashBoardSummaryDetail1(string strName, string strNPI, string strFunction, string strItem, string strCustomer)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //strSQL.AppendFormat("select p.ID,p.PCB_Version,p.FW_Version,p.End_Date,c.Result,f.File_Name,f.File_Path from Project as p,ProjectCase as c,Attachmen_File_Case as f where p.Name='{0}' and p.NPI='{1}' and c.Kind ='{2}' and c.Name ='{3}' and p.ID=c.Project_ID and f.Project_ID =p.ID and f.ProjectCase_ID =c.ID and f.File_Name like '%pdf%' and f.File_Name like 'SIT%' and p.Customer = '{4}'", strName, strNPI, strFunction, strItem, strCustomer);

        strSQL.AppendFormat("select p.ID,p.PCB_Version,p.FW_Version,p.End_Date,c.Result,f.File_Name,f.File_Path from Project as p,ProjectCase as c,Attachmen_File_Case as f where p.Name='{0}' and c.Kind ='{2}' and c.Name ='{3}' and p.ID=c.Project_ID and f.Project_ID =p.ID and f.ProjectCase_ID =c.ID and f.File_Name like '%pdf%' and f.File_Name like 'SIT%' and p.Customer = '{4}'", strName, strNPI, strFunction, strItem, strCustomer);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DashBoard
    public static DataTable UploadDashBoardList(string strStatus, string strAssign, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");


        if (strStatus == "Open")
            strSQL.AppendFormat("where c.Status = 'Open' and CHARINDEX('{0}',c.Assign)>0 and c.Start_Date >='{1}' and c.End_Date <='{2}' and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126) and p.ID=c.Project_ID ", strAssign, strStartDate, strEndDate);
        else if (strStatus == "Delay")
            strSQL.AppendFormat("where c.Status = 'Open' and CHARINDEX('{0}',c.Assign)>0 and c.Start_Date >='{1}' and c.End_Date <='{2}' and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126) and p.ID=c.Project_ID ", strAssign, strStartDate, strEndDate);
        else if (strStatus == "Total")
            strSQL.AppendFormat("where CHARINDEX('{0}',c.Assign)>0 and c.Start_Date >='{1}' and c.End_Date <='{2}' and p.ID=c.Project_ID", strAssign, strStartDate, strEndDate);
        else
            strSQL.AppendFormat("where c.Status = '{0}' and CHARINDEX('{1}',c.Assign)>0 and c.Start_Date >='{2}' and c.End_Date <='{3}' and p.ID=c.Project_ID", strStatus, strAssign, strStartDate, strEndDate);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DashBoardProject
    public static DataTable UploadDashBoardProject(string strKind, string strSearchKind, string strStatus, string strAssign, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strSearchKind == "ALL")
        {
            if (strKind == "1")
                strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
            else
                strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");

            if (strStatus == "Open")
                strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
            else if (strStatus == "Delay")
                strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
            else if (strStatus == "Total")
                strSQL.AppendFormat("where  p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID", strAssign, strStartDate, strEndDate);

            else
                strSQL.AppendFormat("where c.Status = '{0}' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID", strStatus, strAssign, strStartDate, strEndDate);
        }
        else
        {
            if (strKind == "1")
                strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
            else
                strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");

            if (strStatus == "Open")
                strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126) and p.Kind='{4}'", strStatus, strAssign, strStartDate, strEndDate, strSearchKind);
            else if (strStatus == "Delay")
                strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126) and p.Kind='{4}'", strStatus, strAssign, strStartDate, strEndDate, strSearchKind);
            else if (strStatus == "Total")
                strSQL.AppendFormat("where  p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID and p.Kind='{3}'", strAssign, strStartDate, strEndDate, strSearchKind);

            else
                strSQL.AppendFormat("where c.Status = '{0}' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and p.Kind='{4}'", strStatus, strAssign, strStartDate, strEndDate, strSearchKind);

        }


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DashBoardProject
    public static DataTable UploadProjectStatistics2(string strKind, string strStatus, string strID, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //if (strSearchKind == "ALL")
        //{
        if (strKind == "1")
            strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
        else
            strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");

        if (strStatus == "Open")
            strSQL.AppendFormat("where c.Project_ID = p.id and c.Status = 'Open' and c.Project_id='{0}' and c.End_Date >='{1}' and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strID, strEndDate);
        else if (strStatus == "Delay")
            strSQL.AppendFormat("where c.Project_ID = p.id and c.Status = 'Open' and c.Project_id ='{0}' and c.End_Date <='{1}' and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strID, strEndDate);
        else if (strStatus == "Total")
            strSQL.AppendFormat("where c.Project_ID = p.id and c.Project_id ='{0}'", strID);
        else
            strSQL.AppendFormat("where c.Project_ID = p.id and c.Status = '{0}' and c.Project_id ='{1}'", strStatus, strID);
        //}
        //else
        //{
        //    if (strKind == "1")
        //        strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
        //    else
        //        strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");

        //    if (strStatus == "Open")
        //        strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126) and p.Kind='{4}'", strStatus, strAssign, strStartDate, strEndDate, strSearchKind);
        //    else if (strStatus == "Delay")
        //        strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126) and p.Kind='{4}'", strStatus, strAssign, strStartDate, strEndDate, strSearchKind);
        //    else if (strStatus == "Total")
        //        strSQL.AppendFormat("where  p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID and p.Kind='{3}'", strAssign, strStartDate, strEndDate, strSearchKind);

        //    else
        //        strSQL.AppendFormat("where c.Status = '{0}' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and p.Kind='{4}'", strStatus, strAssign, strStartDate, strEndDate, strSearchKind);

        //}


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DashBoardProject
    public static DataTable UploadDashBoardProjectList(string strStatus, string strAssign, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //if (strSearchKind == "ALL")
        //{
        //    if (strKind == "1")
        //        strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
        //    else
        //        strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");

        //    if (strStatus == "Open")
        //        strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        //    else if (strStatus == "Delay")
        //        strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        //    else if (strStatus == "Total")
        //        strSQL.AppendFormat("where  p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID", strAssign, strStartDate, strEndDate);

        //    else
        //        strSQL.AppendFormat("where c.Status = '{0}' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID", strStatus, strAssign, strStartDate, strEndDate);
        //}
        //else
        //{
        //if (strKind == "1")
        //    strSQL.Append("select COUNT(c.id) as CountCase from ProjectCase as c,Project as p ");
        //else
        strSQL.Append("select p.ID as PID,c.ID as CID,p.Name,c.Kind,c.Name as CaseName from ProjectCase as c,Project as p ");

        if (strStatus == "Open")
            strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) >=  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        else if (strStatus == "Delay")
            strSQL.AppendFormat("where c.Status = 'Open' and p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID and CONVERT(varchar(10),c.End_Date,126) <  CONVERT(varchar(10),getdate(),126)", strStatus, strAssign, strStartDate, strEndDate);
        else if (strStatus == "Total")
            strSQL.AppendFormat("where  p.Name ='{0}' and c.Start_Date >='{1}' and c.End_Date <='{2}' and c.Project_ID = p.ID", strAssign, strStartDate, strEndDate);

        else
            strSQL.AppendFormat("where c.Status = '{0}' and p.Name ='{1}' and c.Start_Date >='{2}' and c.End_Date <='{3}' and c.Project_ID = p.ID", strStatus, strAssign, strStartDate, strEndDate);

        //}


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestItem_File
    public static DataTable UploadTestItem_File(string strKind_ID, string strFunction_ID, string strItem)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("Select * from TestCase_Item where Kind_ID = '{0}' and Function_ID = '{1}' and Item = '{2}' and disable <>'Y' ", strKind_ID, strFunction_ID, strItem);

        //if (strID == "")
        //    strSQL.AppendFormat("where id = '{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestItem_File1
    public static DataTable UploadTestItem_File1(string strKind_ID, string strFunction_ID, string strItem)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("Select * from TestCase_Item where Kind_ID = '{0}' and Function_ID = '{1}' and id = '{2}' and disable <>'Y' ", strKind_ID, strFunction_ID, strItem);

        //if (strID == "")
        //    strSQL.AppendFormat("where id = '{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestItem_File
    public static DataTable UploadExplanation_File(string strKind_ID, string strItem)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("Select * from Explanation_Item where Kind_ID = '{0}' and Item = '{1}' ", strKind_ID, strItem);

        //if (strID == "")
        //    strSQL.AppendFormat("where id = '{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusReportChart
    public static DataTable UploadApparatusReportChart(string strStart, string strEnd, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select r.Apparatus_ID from Reservation as r,Apparatus as a where r.StartDate >='{0}' and r.EndDate <='{1}' and a.ID=r.Apparatus_ID and a.Kind ='{2}'  group by r.Apparatus_ID ", strStart, strEnd, strKind);
        strSQL.AppendFormat("select ID from Apparatus where Kind='{0}' and (ReservationStatus !='採購中' and ReservationStatus !='不可借用')", strKind);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusReportChart
    public static DataTable UploadApparatusReportColumnChart(string strStart, string strEnd, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select a.Products_ID, a.Name from Reservation as r,Apparatus as a where r.StartDate >='{0}' and r.EndDate <='{1}' and a.ID=r.Apparatus_ID and a.Kind ='{2}' group by a.Products_ID ,a.Name ", strStart, strEnd, strKind);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusReportChart
    public static DataTable UploadReservationCaseCount(string strStart, string strEnd, string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select r.GName,COUNT(r.GName) as total from Reservation as r,Apparatus as a where a.Products_ID ='{0}' and r.Apparatus_ID = a.ID and ((EndDate >= '{1}' and EndDate <= '{2}' and ReturnDate = '1900-01-01 00:00:00') or (StartDate >= '{1}' and EndDate <= '{2}' and ReturnDate = '1900-01-01 00:00:00') or (StartDate <= '{1}' and EndDate >= '{2}' and ReturnDate = '1900-01-01 00:00:00') or ((StartDate >= '{1}' and StartDate <= '{2}'))and  ((ReturnDate >= '{1}' and ReturnDate <= '{2}')) or (StartDate >= '{1}' and StartDate <= '{2}')) group by r.GName ", strID, strStart, strEnd);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyReport1
    public static DataTable UploadWeeklyReport1(string strNumber, string strEmp, string strYear)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from WeeklyReport where WeekNumber = '{0}' and Employees ='{1}' and year(Report_Date) ='{2}'", strNumber, strEmp, strYear);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyReport2
    public static DataTable UploadWeeklyReport2(string strNumber, string strEmp, string strProject, string strItem, string strYear)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from WeeklyReport where WeekNumber = '{0}' and Employees ='{1}' and Project ='{2}' and Item ='{3}' and year(Report_Date) ='{4}'", strNumber, strEmp, strProject, strItem, strYear);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadTimeReport
    public static DataTable UploadTimeReport(string strDateS, string strDateE, string strEmp)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select p.A_Department,p.customer, case when ((w.Project != 'Meeting') and (w.Project != 'Day off')) then p.name else w.Project end as name ,STUFF((	SELECT ','+ c1.Name FROM WeeklyReport as w1 left join ProjectCase as c1 on w1.item = c1.ID and w1.Project = c1.Project_ID where w1.Project =w.Project for xml path('')),1,1,'') AS Item,SUM(CONVERT(float,w.Hours)) as hours from WeeklyReport as w left join Project as p on w.Project = p.ID where w.Employees ='{0}' and (w.Report_Date >='{1}' and w.Report_Date <='{2}')  group by p.name ,w.Project,p.A_Department,p.Customer", strEmp, strDateS, strDateE);
        strSQL.AppendFormat("select p.A_Department,p.customer, case when ((w.Project != 'Meeting') and (w.Project != 'Day off') and (w.Project != 'Other')) then p.name else w.Project end as name ,STUFF((	SELECT ','+ c1.Name FROM WeeklyReport as w1 left join ProjectCase as c1 on w1.item = c1.ID and w1.Project = c1.Project_ID where w1.Employees ='{0}' and w1.Project=w.Project and len(w1.item) < '10' group by w1.Project,c1.Name for xml path('')),1,1,'') AS Item,SUM(CONVERT(float,w.Hours)) as hours from WeeklyReport as w left join Project as p on w.Project = p.ID where w.Employees ='{0}' and (w.Report_Date >='{1}' and w.Report_Date <='{2}') group by p.name ,w.Project,p.A_Department,p.Customer,w.Item", strEmp, strDateS, strDateE);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadTimeReport
    public static DataTable UploadTimeReport_N(string strDateS, string strDateE, string strEmp, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select p.A_Department,p.customer, case when ((w.Project != 'Meeting') and (w.Project != 'Day off')) then p.name else w.Project end as name ,STUFF((	SELECT ','+ c1.Name FROM WeeklyReport as w1 left join ProjectCase as c1 on w1.item = c1.ID and w1.Project = c1.Project_ID where w1.Project =w.Project for xml path('')),1,1,'') AS Item,SUM(CONVERT(float,w.Hours)) as hours from WeeklyReport as w left join Project as p on w.Project = p.ID where w.Employees ='{0}' and (w.Report_Date >='{1}' and w.Report_Date <='{2}')  group by p.name ,w.Project,p.A_Department,p.Customer", strEmp, strDateS, strDateE);
        strSQL.AppendFormat("select w.* from WeeklyReport as w,Project as p where w.Project =p.ID and w.Report_Date >= '{0}' and w.Report_Date <= '{1}' and w.Employees ='{2}' and LEN(w.project) >= '14'", strDateS, strDateE, strEmp);
        if (strKind == "驗証申請")
            strSQL.AppendFormat(" and p.Kind ='驗証申請' order by w.Project,Item ");
        else
            strSQL.AppendFormat(" and p.Kind !='驗証申請' order by w.Project,Item ");


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadTimeReport
    public static DataTable UploadTimeReport_N1(string strPID, string strCID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select p.A_Department,p.customer, case when ((w.Project != 'Meeting') and (w.Project != 'Day off')) then p.name else w.Project end as name ,STUFF((	SELECT ','+ c1.Name FROM WeeklyReport as w1 left join ProjectCase as c1 on w1.item = c1.ID and w1.Project = c1.Project_ID where w1.Project =w.Project for xml path('')),1,1,'') AS Item,SUM(CONVERT(float,w.Hours)) as hours from WeeklyReport as w left join Project as p on w.Project = p.ID where w.Employees ='{0}' and (w.Report_Date >='{1}' and w.Report_Date <='{2}')  group by p.name ,w.Project,p.A_Department,p.Customer", strEmp, strDateS, strDateE);
        strSQL.AppendFormat("select p.Name as Name1,p.A_Department,p.A_Department2,p.Customer,p.Kind,p.Progress as Progress1,p.Progress_LastWeek as Progress_LastWeek1,c.* from Project as p,ProjectCase as c where p.ID=c.Project_ID and p.ID='{0}' ", strPID);
        if (strCID != "")
            strSQL.AppendFormat(" and c.ID='{0}' ", strCID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadTimeReport
    public static DataTable UploadTimeReport_N2(string strPID, string strCID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select p.A_Department,p.customer, case when ((w.Project != 'Meeting') and (w.Project != 'Day off')) then p.name else w.Project end as name ,STUFF((	SELECT ','+ c1.Name FROM WeeklyReport as w1 left join ProjectCase as c1 on w1.item = c1.ID and w1.Project = c1.Project_ID where w1.Project =w.Project for xml path('')),1,1,'') AS Item,SUM(CONVERT(float,w.Hours)) as hours from WeeklyReport as w left join Project as p on w.Project = p.ID where w.Employees ='{0}' and (w.Report_Date >='{1}' and w.Report_Date <='{2}')  group by p.name ,w.Project,p.A_Department,p.Customer", strEmp, strDateS, strDateE);
        strSQL.AppendFormat("select p.Name as Name1,p.A_Department,p.A_Department2,p.Customer,p.Kind as Kind2,p.Progress as Progress1,p.Progress_LastWeek as Progress_LastWeek1,c.* from Project as p,ProjectCase as c where p.ID=c.Project_ID and p.ID='{0}' ", strPID);
        if (strCID != "")
            strSQL.AppendFormat(" and c.ID='{0}' ", strCID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadTimeReport2
    public static DataTable UploadTimeReport2_N(string strDateS, string strDateE, string strEmp, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select c.Project_ID,c.ID as CaseID,w.* from Project as p,ProjectCase as c left join WeeklyReport as w on c.Project_ID = w.Project and (w.Project = w.Item or convert(varchar,c.ID) = w.Item) and w.Employees ='{0}' and w.Report_Date > '{1}' and w.Report_Date <= '{2}'", strEmp, strDateS, strDateE);
        strSQL.AppendFormat(" where((c.Start_Date >='{0}' and c.End_Date <= '{1}') or (c.Start_Date <='{2}' and c.End_Date > '{3}') or (c.Start_Date >='{4}' and  c.Start_Date < '{5}'))", strDateS, strDateE, strDateS, strDateS, strDateS, strDateE);
        strSQL.AppendFormat(" and c.Project_ID =p.ID and CHARINDEX('{0}',c.Assign)>0", strEmp);
        if (strKind == "驗証申請")
            strSQL.AppendFormat(" and p.Kind ='驗証申請' order by c.Project_ID  ");
        else
            strSQL.AppendFormat(" and p.Kind !='驗証申請' order by c.Project_ID  ");


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion  

    #region UploadTimeReport2
    public static DataTable UploadTimeReport2_N2(string strDateS, string strDateE, string strPID, string strCID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select p.A_Department,p.customer, case when ((w.Project != 'Meeting') and (w.Project != 'Day off')) then p.name else w.Project end as name ,STUFF((	SELECT ','+ c1.Name FROM WeeklyReport as w1 left join ProjectCase as c1 on w1.item = c1.ID and w1.Project = c1.Project_ID where w1.Project =w.Project for xml path('')),1,1,'') AS Item,SUM(CONVERT(float,w.Hours)) as hours from WeeklyReport as w left join Project as p on w.Project = p.ID where w.Employees ='{0}' and (w.Report_Date >='{1}' and w.Report_Date <='{2}')  group by p.name ,w.Project,p.A_Department,p.Customer", strEmp, strDateS, strDateE);
        strSQL.AppendFormat("select p.Name as Name1,p.A_Department,p.A_Department2,p.Customer,p.Kind as Kind2,p.Progress as Progress1,p.Progress_LastWeek as Progress_LastWeek1,c.* from Project as p,ProjectCase as c where p.ID=c.Project_ID and p.ID='{0}' ", strPID);
        strSQL.AppendFormat(" and ((c.Start_Date >='{0}' and c.End_Date <= '{1}') or (c.Start_Date <='{2}' and c.End_Date > '{3}') or (c.Start_Date >='{4}' and  c.Start_Date < '{5}'))", strDateS, strDateE, strDateS, strDateS, strDateS, strDateE);
         
        if (strCID != "")
            strSQL.AppendFormat(" and c.ID='{0}' ", strCID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadManpowerReport
    public static DataTable UploadManpowerReport(string strWeek, string strEmployees)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from (select WeekNumber,Project,CAST(Hours AS decimal(18, 1)) as hours from WeeklyReport where Employees ='{0}') as a pivot (sum(hours) for weeknumber in ({1})) as p", strEmployees, strWeek);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadManpowerReport
    public static DataTable UploadManpowerReport_Month(string strEmployees, string strDateS, string strDateE, string strMonths)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from (SELECT project,(cast(YEAR(Report_Date) AS varchar) +'/'+ cast(Month(Report_Date) as varchar)) as Months,sum(CAST(Hours AS decimal(18, 2))) as hours ");
        strSQL.AppendFormat("From WeeklyReport where Employees ='{0}' and Report_Date >= '{1}'and Report_Date <='{2}' Group By project,(cast(YEAR(Report_Date) AS varchar) +'/'+ cast(Month(Report_Date) as varchar))) as a pivot (sum(hours) for Months in ({3})) as p", strEmployees, strDateS, strDateE, strMonths);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadManpowerReport
    public static DataTable UploadManpowerReport1_Month_O(string strEmployees, string strDateS, string strDateE, string strMonths)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from (SELECT Project,(cast(YEAR(Report_Date) AS varchar) +'/'+ cast(Month(Report_Date) as varchar)) as Months,sum(CAST(Hours AS decimal(18, 2))) as hours ");
        strSQL.AppendFormat("From WeeklyReport where Employees ='{0}' and Report_Date >= '{1}'and Report_Date <='{2}' and LEN(project) < '14' Group By project,(cast(YEAR(Report_Date) AS varchar) +'/'+ cast(Month(Report_Date) as varchar))) as a pivot (sum(hours) for Months in ({3})) as p", strEmployees, strDateS, strDateE, strMonths);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadManpowerReport
    public static DataTable UploadManpowerReport1_Month(string strEmployees, string strDateS, string strDateE, string strMonths)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from (SELECT p.Kind,(cast(YEAR(Report_Date) AS varchar) +'/'+ cast(Month(Report_Date) as varchar)) as Months,sum(CAST(Hours AS decimal(18, 2))) as hours ");
        strSQL.AppendFormat("From WeeklyReport,Project as p where WeeklyReport.Project=p.ID and Employees ='{0}' and Report_Date >= '{1}'and Report_Date <='{2}' Group By p.Kind,(cast(YEAR(Report_Date) AS varchar) +'/'+ cast(Month(Report_Date) as varchar))) as a pivot (sum(hours) for Months in ({3})) as p1", strEmployees, strDateS, strDateE, strMonths);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadPR_Statistics
    public static DataTable UploadPR_Statistics(string strDateS, string strDateE, string strMonths, string strLocal, string strKind, string strStatus)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "1")
        {
            strSQL.AppendFormat("select * from (SELECT '設備購置' as kind,(cast(YEAR(d.Arrival_Date) AS varchar) +'/'+ cast(Month(d.Arrival_Date) as varchar)) as Months,sum(CAST(d.Estimated_TotalPrice AS money)) as total ");
            strSQL.AppendFormat("from PurchasingRequisition as p,PR_Detail as d where p.ID = d.PR_ID and CHARINDEX('A',d.Goods_ID) >0 and d.Arrival_Date >= '{0}'and d.Arrival_Date <='{1}' and Accepted_Team ='{2}' and d.Status ='{3}' group by (cast(YEAR(d.Arrival_Date) AS varchar) +'/'+ cast(Month(d.Arrival_Date) as varchar))) as a pivot (sum(total) for Months in ({4})) as p1", strDateS, strDateE, strLocal, strStatus, strMonths);

        }
        else if (strKind == "0")
        {
            strSQL.AppendFormat("select * from (SELECT g.Kind ,(cast(YEAR(d.Arrival_Date) AS varchar) +'/'+ cast(Month(d.Arrival_Date) as varchar)) as Months,sum(CAST(d.Estimated_TotalPrice AS money)) as total ");
            strSQL.AppendFormat("from PurchasingRequisition as p,PR_Detail as d,Goods as g where p.ID = d.PR_ID and d.Goods_ID = g.ID and CHARINDEX('G',d.Goods_ID) >0 and d.Arrival_Date >= '{0}'and d.Arrival_Date <='{1}' and Accepted_Team ='{2}' and d.Status ='{3}' and g.Kind <>'設備購置'  group by g.kind,(cast(YEAR(d.Arrival_Date) AS varchar) +'/'+ cast(Month(d.Arrival_Date) as varchar))) as a pivot (sum(total) for Months in ({4})) as p1", strDateS, strDateE, strLocal, strStatus, strMonths);
        }
        else
        {
            strSQL.AppendFormat("select * from (SELECT g.Kind ,(cast(YEAR(d.Arrival_Date) AS varchar) +'/'+ cast(Month(d.Arrival_Date) as varchar)) as Months,sum(CAST(d.Estimated_TotalPrice AS money)) as total ");
            strSQL.AppendFormat("from PurchasingRequisition as p,PR_Detail as d,Goods as g where p.ID = d.PR_ID and d.Goods_ID = g.ID and CHARINDEX('G',d.Goods_ID) >0 and d.Arrival_Date >= '{0}'and d.Arrival_Date <='{1}' and Accepted_Team ='{2}' and d.Status ='{3}' and g.Kind ='設備購置'  group by g.kind,(cast(YEAR(d.Arrival_Date) AS varchar) +'/'+ cast(Month(d.Arrival_Date) as varchar))) as a pivot (sum(total) for Months in ({4})) as p1", strDateS, strDateE, strLocal, strStatus, strMonths);

        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadPR_Statistics Open
    public static DataTable UploadPR_Statistics_open(string strDateS, string strDateE, string strMonths, string strLocal, string strKind, string strStatus)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "1")
        {
            strSQL.AppendFormat("select * from (SELECT '設備購置' as kind,(cast(YEAR(p.PR_Date) AS varchar) +'/'+ cast(Month(p.PR_Date) as varchar)) as Months,sum(CAST(d.Estimated_TotalPrice AS money)) as total ");
            strSQL.AppendFormat("from PurchasingRequisition as p,PR_Detail as d where p.ID = d.PR_ID and CHARINDEX('A',d.Goods_ID) >0 and p.PR_Date >= '{0}'and p.PR_Date <='{1}' and Accepted_Team ='{2}' and d.Status ='{3}' group by (cast(YEAR(p.PR_Date) AS varchar) +'/'+ cast(Month(p.PR_Date) as varchar))) as a pivot (sum(total) for Months in ({4})) as p1", strDateS, strDateE, strLocal, strStatus, strMonths);

        }
        else if (strKind == "0")
        {
            strSQL.AppendFormat("select * from (SELECT g.Kind ,(cast(YEAR(p.PR_Date) AS varchar) +'/'+ cast(Month(p.PR_Date) as varchar)) as Months,sum(CAST(d.Estimated_TotalPrice AS money)) as total ");
            strSQL.AppendFormat("from PurchasingRequisition as p,PR_Detail as d,Goods as g where p.ID = d.PR_ID and d.Goods_ID = g.ID and CHARINDEX('G',d.Goods_ID) >0 and p.PR_Date >= '{0}'and p.PR_Date <='{1}' and Accepted_Team ='{2}' and d.Status ='{3}' and g.Kind <>'設備購置'  group by g.kind,(cast(YEAR(p.PR_Date) AS varchar) +'/'+ cast(Month(p.PR_Date) as varchar))) as a pivot (sum(total) for Months in ({4})) as p1", strDateS, strDateE, strLocal, strStatus, strMonths);
        }
        else
        {
            strSQL.AppendFormat("select * from (SELECT g.Kind ,(cast(YEAR(p.PR_Date) AS varchar) +'/'+ cast(Month(p.PR_Date) as varchar)) as Months,sum(CAST(d.Estimated_TotalPrice AS money)) as total ");
            strSQL.AppendFormat("from PurchasingRequisition as p,PR_Detail as d,Goods as g where p.ID = d.PR_ID and d.Goods_ID = g.ID and CHARINDEX('G',d.Goods_ID) >0 and p.PR_Date >= '{0}'and p.PR_Date <='{1}' and Accepted_Team ='{2}' and d.Status ='{3}' and g.Kind ='設備購置'  group by g.kind,(cast(YEAR(p.PR_Date) AS varchar) +'/'+ cast(Month(p.PR_Date) as varchar))) as a pivot (sum(total) for Months in ({4})) as p1", strDateS, strDateE, strLocal, strStatus, strMonths);

        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyPlan
    public static DataTable UploadWeekPlan(string strNumber, string strEmp, string strYear)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from WeekPlan where Week_Number = '{0}' and Employees ='{1}' and Plan_Year ='{2}' order by Week_Name", strNumber, strEmp, strYear);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadExplanation_Item
    public static DataTable UploadExplanation_Item(string strKind_ID, string strItem)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("Select * from Explanation_Item where Kind_ID = '{0}' and Item = '{1}' ", strKind_ID, strItem);

        //if (strID == "")
        //    strSQL.AppendFormat("where id = '{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusMaster
    public static DataTable UploadApparatusMasterQuery(string strKind, string strSearch)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        if (strSearch == "1")
        {
            strSQL.Append("select e.* from InfoData as i,Employees as e ");
            strSQL.AppendFormat("where i.Kind = '{0}' and (i.Name = e.Name_En)", strKind);
        }
        else
        {
            strSQL.Append("select * from InfoData ");

            strSQL.AppendFormat("where kind = '{0}'", strKind);
        }


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋BorrowGoods
    public static DataTable UploadBorrowGoodsQuery(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select sum(cast(BorrowedQuantity as int)) as Count_ID from Reservation where ");
        strSQL.AppendFormat("Apparatus_ID ='{0}' and Status ='Y'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Leader
    public static DataTable UploadLeader(string strKind, string strLocation, string strTeam)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select Name_En,Email from Employees where teamleader = 'Y'");
        else if (strKind == "1")
        {
            strSQL.Append("select Name_En,Email from Employees where Manager = 'Y' ");
            strSQL.AppendFormat("and Location = '{0}'", strLocation);
        }
        else if (strKind == "3")
        {
            strSQL.Append("select Name_En,Email from Employees where teamleader = 'Y' ");
            strSQL.AppendFormat("and Department = '{0}'", strTeam);
        }
        else if (strKind == "4")
        {
            strSQL.Append("select Name_En,Email from Employees where ID = '1'");
        }
        else
        {
            strSQL.Append("select Name_En,Email from Employees where teamleader = 'Y' ");
            strSQL.AppendFormat("and Location = '{0}' and Team = '{1}'", strLocation, strTeam);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyReport
    public static DataTable UploadWeeklyReport(string strName, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select p.Name ,(c.Kind + '--' + c.Name) as Item ,c.ID,c.Project_ID   from ProjectCase as c,Project as p ");
        //strSQL.AppendFormat("where c.Project_ID =p.ID and  c.Assign = '{0}' and (c.Status ='Open' or c.End_Date >='{1}')", strName, strDate);
        //strSQL.AppendFormat("where c.Project_ID =p.ID and  c.Assign = '{0}' and  c.End_Date >='{1}'", strName, strDate);
        strSQL.AppendFormat("where c.Project_ID =p.ID and  CHARINDEX('{0}', c.Assign)>0  and  c.End_Date >='{1}'", strName, strDate);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyReport
    public static DataTable UploadWeeklyReport_Leader1(string strName, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select p.Name ,(c.Kind + '--' + c.Name) as Item ,c.ID,c.Project_ID   from ProjectCase as c,Project as p ");
        //strSQL.AppendFormat("where c.Project_ID =p.ID and  c.Assign = '{0}' and (c.Status ='Open' or c.End_Date >='{1}')", strName, strDate);
        //strSQL.AppendFormat("where c.Project_ID =p.ID and  c.Assign = '{0}' and  c.End_Date >='{1}'", strName, strDate);
        strSQL.AppendFormat("where c.Project_ID =p.ID and  CHARINDEX('{0}', c.Assign)>0  and  c.End_Date >='{1}' and p.Kind != '驗証申請'", strName, strDate);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyReport
    public static DataTable UploadWeeklyReport_Leader(string strName, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select ID, Name from Project where Kind='驗証申請' ");
        strSQL.AppendFormat(" and Assign ='{0}' and  End_Date >='{1}' ", strName, strDate);
        strSQL.AppendFormat("union select Project_ID, p.Name from ProjectCase as c,Project as p where c.Project_ID =p.ID and  CHARINDEX('{0}', c.Assign)>0  and  c.End_Date >='{1}' and p.Kind='驗証申請'", strName, strDate);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region UploadWeeklyReport
    public static DataTable UploadWeeklyReport_Manager(string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select * from Project where Kind='驗証申請' ");
        strSQL.AppendFormat(" and End_Date >='{0}' ", strDate);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋PR
    public static DataTable UploadPRQuery(string strSearch, string strStart, string strEnd, string strID, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from PurchasingRequisition where ");
        if (strSearch == "0")
        {

            strSQL.AppendFormat("(Application_Date >= '{0}' and Application_Date <= '{1}') and Accepted_Team ='{2}'", strStart, strEnd, strLocal);
        }
        else
        {
            strSQL.AppendFormat("(PR_No = '{0}' or Signed_ID = '{0}') and Accepted_Team ='{1}'", strID, strLocal);
        }
        strSQL.AppendFormat(" and Status = 'Close'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋WorkTime
    public static DataTable UploadWorkTimeQuery(string strSearch)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select * from InfoData where kind = 'A1'");

        strSQL.AppendFormat("select * from InfoData where kind = '{0}'", strSearch);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋FileName
    public static DataTable UploadFileNameQuery(string strTestCase_c)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from FilePath_Kind,FilePath_TestCase where FilePath_Kind .FilePath_TestCase_ID = FilePath_TestCase .ID and ");

        strSQL.AppendFormat("FilePath_TestCase.TestCase = '{0}'", strTestCase_c);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋FilePath_TestCase
    public static DataTable UploadFilePathTestCaseQuery()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from FilePath_TestCase");

        //strSQL.AppendFormat("FilePath_TestCase.TestCase = '{0}'", strTestCase_c);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
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

    #region 找尋ProjectTemporarily (使用ID搜尋)
    public static DataTable UploadProjectTemporarilyQuery(string strID, string strTable)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from ");
        //strSQL.Append("from " );


        if (strTable == "Project_Temporarily")
            strSQL.AppendFormat("{0} WHERE ID = '{1}'", strTable, strID);
        else
            strSQL.AppendFormat("{0} WHERE  Project_ID = '{1}'", strTable, strID);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectTestCase (使用ID搜尋)
    public static DataTable UploadProjectCase(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Application_TestCase ");
        strSQL.AppendFormat("where project_id='{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion


    #region 找尋ProjectFile
    public static DataTable UploadProjectFileQuery(string strProjectID, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select Project_ID,File_Name,File_Path from Attachmen_File ");

        if ((strKind == "驗証申請") || (strKind == "認証申請"))
            strSQL.AppendFormat("WHERE Project_ID = '{0}' and (ProjectCase_Kind = '驗証申請' or ProjectCase_Kind = '認証申請')", strProjectID, strKind);
        else
            strSQL.AppendFormat("WHERE Project_ID = '{0}' and ProjectCase_Kind = '{1}'", strProjectID, strKind);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectFile
    public static DataTable UploadProjectFile(string strProjectName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select File_Name,File_Path from Attachmen_File_Project "); ;
        strSQL.AppendFormat("WHERE ProjectName = '{0}'", strProjectName);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectCaseFile
    public static DataTable UploadProjectCaseFileQuery(string strCaseID, string strProjectID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select Project_ID,File_Name,File_Path,Upload_Date,Upload_Emp from Attachmen_File_Case ");
        strSQL.AppendFormat("WHERE  ProjectCase_ID = '{0}' and Project_ID = '{1}'", strCaseID, strProjectID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadSampleFileQuery
    public static DataTable UploadSampleFileQuery(string strSampleID, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Sample_File ");
        strSQL.AppendFormat("WHERE  Sample_ID = '{0}' ", strSampleID);

        if (strKind == "1")
            strSQL.AppendFormat(" and (File_Name not like '%jpg' and File_Name not like '%gif' and File_Name not like '%png' and File_Name not like '%bmp') ");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋FilePath_TestCase
    public static DataTable UploadFilePathCaseQuery(string strCaseID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from FilePath_TestCase ");
        strSQL.AppendFormat("WHERE  TestCase = '{0}' ", strCaseID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋FilePath_Kind
    public static DataTable UploadFilePathKindQuery(string strCaseID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from FilePath_TestCase as a,FilePath_Kind as b ");
        strSQL.AppendFormat("where b.File_Kind ='{0}' and a.ID = b.FilePath_TestCase_ID", strCaseID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion


    #region ProjectView用
    public static DataTable getDataTable(string strSQL)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        //StringBuilder strSQL = new StringBuilder();

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        //Adapter = new OracleDataAdapter(sql, conn);
        //DataTable dataTable = new DataTable();
        //Adapter.Fill(dataTable);
        //conn.Close();
        return dt;
    }
    #endregion

    #region getProjectList(ProjectApplication用)
    public static DataTable getProjectList(string strAssign, string strKind, string strStatus, string strLocation, string strList)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "1")
        {
            strSQL.Append("select kind,Name,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,ID,Accepted_Team from project ");
            strSQL.AppendFormat("where status ='{0}' and (kind='{1}' or kind='認証申請') ", strAssign, strList);
        }
        else if (strKind == "2")
        {
            strSQL.Append("select Team,kind,Name,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,ID,Accepted_Team from project ");
            strSQL.AppendFormat("where status ='{0}' and assign ='{1}' and kind='{2}'", strStatus, strAssign, strList);
        }
        else
        {
            //strSQL.Append("select Team,kind,Accepted_Team,Name,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,ID from project ");
            //strSQL.AppendFormat("where status ='{0}'", strStatus);
            strSQL.Append("select Team,Name,Customer,NPI,PCB_Version,FW_Version,HW_Engineer,SW_Engineer,PM,ID,Accepted_Team from project ");
            strSQL.AppendFormat("where status ='{0}' and kind='{1}' ", strStatus, strList);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getProjectList_App(ProjectView用)
    public static DataTable getProjectList_App(string strAssign, string strKind, string strStatus, string strLocation, string strList, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "1")
        {
            if (strList == "驗証申請")
            {
                strSQL.Append("select kind,Name,Project_Kind,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,ID,Accepted_Team from project ");
                strSQL.AppendFormat("where status ='{0}' and (kind='{1}' or kind ='認証申請') and Name='{2}'", strAssign, strList, strName);
            }
            else
            {
                strSQL.Append("select kind,Name,Project_Kind,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,ID,Accepted_Team from project ");
                strSQL.AppendFormat("where status ='{0}' and kind='{1}' and Name='{2}'", strAssign, strList, strName);
            }
        }
        else if (strKind == "2")
        {
            if (strList == "驗証申請")
            {
                strSQL.Append("select Team,kind,Name,Project_Kind,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,ID,Accepted_Team from project ");
                strSQL.AppendFormat("where status ='{0}' and Accepted_Team='{1}' and (kind='{2}' or kind ='認証申請') and Name='{3}'", strStatus, strAssign, strList, strName);
            }
            else
            {
                strSQL.Append("select Team,kind,Name,Project_Kind,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,ID,Accepted_Team from project ");
                strSQL.AppendFormat("where status ='{0}' and Accepted_Team='{1}' and kind='{2}' and Name='{3}'", strStatus, strAssign, strList, strName);
            }
        }
        else
        {
            //strSQL.Append("select Team,kind,Accepted_Team,Name,Customer,NPI,HW_Engineer,PCB_Version,SW_Engineer,FW_Version,A_Department,Chipset,DSP_Model,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,ID from project ");
            //strSQL.AppendFormat("where status ='{0}'", strStatus);
            if (strList == "驗証申請")
            {
                strSQL.Append("select Team,Name,Project_Kind,Customer,NPI,PCB_Version,FW_Version,HW_Engineer,SW_Engineer,PM,ID,Accepted_Team from project ");
                strSQL.AppendFormat("where status ='{0}' and (kind='{1}' or kind ='認証申請') and Name='{2}' ", strStatus, strList, strName);
            }
            else
            {
                strSQL.Append("select Team,Name,Project_Kind,Customer,NPI,PCB_Version,FW_Version,HW_Engineer,SW_Engineer,PM,ID,Accepted_Team from project ");
                strSQL.AppendFormat("where status ='{0}' and kind='{1}' and Name='{2}' ", strStatus, strList, strName);
            }
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getProjectMain(ProjectMain用)
    public static DataTable getProjectMain(string strStatus, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.AppendFormat("select distinct name,Accepted_Team from Project where (Kind='{0}' or Kind='認証申請') and Status='{1}' ", strKind, strStatus);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region 以特定Function_No 找其子節點的record(Masterpage使用)
    public static DataTable CreateMenuView(string strID, string strAuthority)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        if (strAuthority == "0")
        {
            strSQL.Append("select Function_List.*,authority.value1 from Function_List,authority where function_list.function_no = authority.function_no and Parent_Function_No = '0'and ");
            strSQL.AppendFormat("authority.login_id = '{0}' and authority.Parent_Function = 'Y' order by Sequence ", strID);
        }
        else if (strAuthority == "1")
        {
            strSQL.Append("select * from Function_List_N where Expand = 'Y' order by Sequence");
        }
        else
            strSQL.Append("select * from Function_List where Expand = 'Y'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 以特定Function_No 找其子節點的record(Masterpage使用)
    public static DataTable CreateChildMenuView(string iItem, string fun_no, string strLogin, string strAuthority)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strAuthority == "0")
        {
            if (iItem == "2")
            {
                strSQL.Append("select Function_No,Function_name,function_url from function_list ");
                strSQL.AppendFormat("where parent_function_no = '{0}' order by Function_No", fun_no);
            }
            else
            {
                strSQL.Append("select function_list.Function_No,function_list.Function_name,function_list.function_url,Authority.value1 from Function_List,Authority ");
                strSQL.AppendFormat("where Authority.login_id = '{0}' and function_list.parent_function_no = '{1}' and function_list.function_no = authority.function_no and Authority.Parent_Function='' order by Function_No", strLogin, fun_no);
            }
        }
        else
        {
            strSQL.Append("select Function_No,Function_name,function_url from Function_List_N ");
            strSQL.AppendFormat("where parent_function_no = '{0}' and Disable <>'Y' order by Function_No", fun_no);

        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 驗證登入者資料(Default使用)
    public static DataTable CheckAccountPwd(string empNo, string empPW)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("SELECT ID,Name_En,Department,Position,Location,Team,Write ");
        strSQL.AppendFormat(" FROM Employees WHERE Name_En = '{0}' AND password = '{1}'", empNo, empPW);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 驗證登入者資料(ApplicationDefault使用)
    public static DataTable CheckAccountPwd_Dep(string empNo, string empPW)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("SELECT * ");
        strSQL.AppendFormat(" FROM departmentaccount WHERE id = '{0}' AND password = '{1}'", empNo, empPW);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 驗證登入者資料(ApplicationDefault使用)
    public static DataTable CheckAccountPwd_Dep1(string empNo, string empPW, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("SELECT * FROM Number ");
        strSQL.AppendFormat("WHERE id = '{0}'", empNo);

        if (strKind == "0")
            strSQL.AppendFormat("AND PassWord = '{0}'", empPW);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 驗證登入者資料(ApplicationDefault使用)
    public static DataTable CheckAccountPwd_test(string empNo, string empPW, string strKind)
    {
        //string connectionString = @"myConnectionString";
        var id = empNo;
        var password = empPW;
        SqlConnection cn = new SqlConnection(connStr);
        //int count;
        //using (SqlConnection cn = new SqlConnection(connStr))
        //{
        cn.Open();
        //MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        string sqlStatement = @"SELECT * FROM Number  Where ID= @id AND PassWord= @password";
        SqlCommand sqlCommand = new SqlCommand(sqlStatement, cn);

        ////定義parameter型別
        sqlCommand.Parameters.Add("@id", SqlDbType.Int);
        sqlCommand.Parameters["@id"].Value = id;

        ////讓ADO.NET自行判斷型別轉換
        sqlCommand.Parameters.AddWithValue("@password", password);

        //count = (int)sqlCommand.ExecuteScalar();

        //}
        DataTable dt = new DataTable();
        dt.Load(sqlCommand.ExecuteReader());

        return dt;
        //return count;
        //this.Result.Text = (count > 0) ? "Pass" : "帳號或密碼錯誤";
    }
    #endregion

    #region getSystemList(AddUser用)
    public static DataTable getSystemList()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //strSQL.Append("select Function_No,Function_Name from function_list where Parent_Function_No != '0' order by Parent_Function_No ");        
        strSQL.Append("select item.Function_No,list.Function_Name as PN,item.Function_Name as LN from (select * from Function_List where Parent_Function_No = '0') as List,(select * from Function_List where Parent_Function_No <> '0') as Item where list.Function_No = item.Parent_Function_No order by list.Sequence");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getFunction_Name(Mean用)
    public static DataTable getFunction_Name(string strNumber)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select * from function_list where Function_No = '{0}' and expand='Y'", strNumber);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getEmployees(UserView,AddUser用)
    public static DataTable getEmployees(string strKind, string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "1")
        {
            strSQL.Append("select * from Employees ");
            strSQL.AppendFormat("where Name_En = '{0}'", strID);
        }
        else if (strKind == "0")
            strSQL.Append("select Name_En,Name_CH from Employees ");
        else
        {
            strSQL.Append("select * from Employees ");
            strSQL.AppendFormat("where ID = '{0}'", strID);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getNumber(UserView,AddUser用)
    public static DataTable getNumber(string strKind, string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if ((strID == "") || (strID == "ALL"))
            strKind = "0";

        if (strKind == "1")
        {
            strSQL.Append("select * from Employees ");
            strSQL.AppendFormat("where Name_En = '{0}'", strID);
        }
        else if (strKind == "0")
            strSQL.Append("select * from Number order by Department");
        else if (strKind == "2")
        {
            strSQL.Append("select * from Number ");
            strSQL.AppendFormat("where Department = '{0}'", strID);
        }
        else
        {
            strSQL.Append("select * from Number ");
            strSQL.AppendFormat("where ID = '{0}'", strID);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getDepartmentAccount(DepartmentAccountView用)
    public static DataTable getDepartmentAccount()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from infodata where kind='3' ");


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getReservationAssign(ReservationAssign用)
    public static DataTable getReservationAssign(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");
        strSQL.AppendFormat("Status ='' and Department ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getDepartmentAccount(UserView,AddUser用)
    public static DataTable getDepartmentAccount(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select * from DepartmentAccount ");
        strSQL.AppendFormat("where id = '{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getAuthority(AddUser用)
    public static DataTable getAuthority(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select * from Authority ");
        strSQL.AppendFormat("where Login_ID = '{0}'", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getProjectCase(PrjectDetail用)
    public static DataTable getProjectCase(string strID, string strKind, string strLogin)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "1")
        {
            //strSQL.Append("select distinct Kind,(Project_ID + '-' + convert(varchar,ID)) as ID1 from ProjectCase ");
            //strSQL.AppendFormat("where Project_ID = '{0}'", strID);

            //strSQL.Append("select distinct Kind from ProjectCase ");
            //strSQL.AppendFormat("where Project_ID = '{0}'", strID);

            strSQL.AppendFormat("select distinct c.Kind,(select count(c1.id) from ProjectCase as c1 where (c1.Status ='Open' or c1.Status ='') and c1.Project_ID = '{0}' and c.Kind =c1.Kind) as Total from ProjectCase as c where c.Project_ID = '{0}'", strID);
        }
        else
        {
            strSQL.Append("select (Project_ID + '-' + convert(varchar,ID)) as ID1,distinct Kind from ProjectCase ");
            strSQL.AppendFormat("where Project_ID = '{0}' and assign = '{1}'", strID, strLogin);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getProjectItem(PrjectCase用)
    public static DataTable getProjectItem(string strID, string strKind, string strLogin, string strCase, string strStatus)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strCase == "1")
        {
            //strSQL.Append("select distinct Kind,(Project_ID + '-' + convert(varchar,ID)) as ID1 from ProjectCase ");
            //strSQL.AppendFormat("where Project_ID = '{0}'", strID);

            if (strStatus == "Open")
            {
                strSQL.Append("select Name,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,Result,Status,Progress,ID from ProjectCase ");
                strSQL.AppendFormat("where Kind = '{0}' and Project_ID = '{1}' and (Status = 'Open' or Status = '')", strKind, strID);
            }
            else
            {
                strSQL.Append("select Name,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,Result,Status,Progress,ID from ProjectCase ");
                strSQL.AppendFormat("where Kind = '{0}' and Project_ID = '{1}' and Status = '{2}'", strKind, strID, strStatus);

            }
        }
        else
        {
            strSQL.Append("select Name,Assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,Result,Status,Progress,ID from ProjectCase ");
            strSQL.AppendFormat("where Kind = '{0}' and Project_ID = '{1}' and assign = '{2}'and Status = '{2}'", strKind, strID, strLogin, strStatus);
        }

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

    #region 找尋ProjectCase
    public static DataTable UploadProjectCase(string strID, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select explain_kind from ProjectCase where ");


        strSQL.AppendFormat("Project_ID = '{0}' and Kind ='{1}'", strID, strKind);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectMessage
    public static DataTable UploadProjectMessage(string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID,Name,Customer from Project where ");


        strSQL.AppendFormat("Name like '%{0}%'", strName);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectTask
    public static DataTable UploadProjectTask(string strID, string strCaseID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select id,name,assign,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,Result,progress,explain_case,status,Sub_PU,Model_Name,Lab,Quoted,Reimburse from ProjectCase where ");


        //strSQL.AppendFormat("Project_ID = '{0}' and Kind ='{1}' and Name ='{2}' and ID='{3}'", strID, strKind, strName, strCaseID);
        strSQL.AppendFormat("Project_ID = '{0}' and ID='{1}'", strID, strCaseID);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectTask
    public static DataTable UploadProjectTask_DB(string strID, string strCaseID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select id,name,assign,kind,Cast(Start_Date as date)as Start_Date1,Cast(End_Date as date)as End_Date1,Result,progress,explain_case,status,sub_pu,model_name from ProjectCase where ");


        //strSQL.AppendFormat("Project_ID = '{0}' and Kind ='{1}' and Name ='{2}' and ID='{3}'", strID, strKind, strName, strCaseID);
        strSQL.AppendFormat("Project_ID = '{0}' and ID='{1}'", strID, strCaseID);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion


    #region 找尋ProjectCaseID
    public static DataTable UploadProjectCaseID(string strID, string strKind, string strMethod, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID from ProjectCase where ");

        //if (strKind != "")
        //    strSQL.AppendFormat("Project_ID = '{0}' and kind = '{1}' order by ID", strID,strKind);
        //else
        //    strSQL.AppendFormat("Project_ID = '{0}' order by ID", strID);

        if (strMethod == "0")
            //strSQL.AppendFormat("Project_ID = '{0}' and name = '{1}'", strID, strKind);
            strSQL.AppendFormat("Project_ID = '{0}' and kind = '{1}' and name = '{2}'", strID, strKind, strName);
        else if (strMethod == "1")
            strSQL.AppendFormat("Project_ID = '{0}' and kind = '{1}' order by ID", strID, strKind);
        else if (strMethod == "2")
            strSQL.AppendFormat("Project_ID = '{0}' order by ID", strID);
        //else if (strMethod == "3")
        //    strSQL.AppendFormat("Project_ID = '{0}' and kind = '{1}' and name = '{2}'", strID,strKind,strName);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectTaskID
    public static DataTable UploadProjectTaskID(string strID, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID from ProjectCase where ");

        //if (strKind.IndexOf("DSL") != -1)
        //    strSQL.AppendFormat("Project_ID = '{0}' and Kind like  '%DSL%' order by ID", strID);
        //else
        //    strSQL.AppendFormat("Project_ID = '{0}' and Kind = '{1}' order by ID", strID, strKind);

        //if (strKind.IndexOf("DSL") != -1)
        //    strSQL.AppendFormat("Project_ID = '{0}' and Kind like  '%DSL%' order by ID", strID);
        //else
        strSQL.AppendFormat("Project_ID = '{0}' order by ID", strID, strKind);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋TestCaseID
    public static DataTable UploadTestCaseID(string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select ID from FilePath_TestCase where ");


        strSQL.AppendFormat("TestCase = '{0}'", strName);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_TestFunction最後一筆
    public static DataTable UploadApplication_TestFunctionMaxID(string strKindID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select MAX(ID) as ID from TestCase_Function where Kind_ID = '{0}'", strKindID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Explanation_Item最後一筆
    public static DataTable UploadExplanation_ItemMaxID(string strKindID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select MAX(ID) as ID from Explanation_Item where Kind_ID = '{0}'", strKindID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadApplication_TestItemMaxID最後一筆
    public static DataTable UploadApplication_TestItemMaxID(string strKindID, string strFunctionID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select MAX(ID) as ID from TestCase_Item where Kind_ID = '{0}' and Function_ID = '{1}'", strKindID, strFunctionID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Apparatus最後一筆
    public static DataTable UploadApparatusLastIDQuery()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select MAX(ID) as ID from Apparatus");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Apparatus
    public static DataTable UploadApparatusQuery(string strSearch, string strKind, string strSearchKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Apparatus where ");
        if (strKind == "0")
        {
            if (strSearchKind != "")
                strSQL.AppendFormat("Kind = '{0}' and ", strSearchKind);
            strSQL.AppendFormat("(Products_ID like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("Name like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("Brand like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("Model like '%{0}%') ", strSearch);
            strSQL.AppendFormat(" order by ReservationStatus desc");
        }
        else
        {
            strSQL.AppendFormat("ID = '{0}'", strSearch);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Apparatus
    public static DataTable UploadAContinuousQuery(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select a.ID ,a.Name ,a.kind ,a.Products_ID ,a.Brand ,a.Model ,a.Custodian ,a.Agent ,a.Note ,a.Custodian_Department ,a.Note ,r.Mission ,r.GName ,r.StartDate ,r.EndDate ,r.Borrower ,r.Department, r.Ext, r.Email, r.Customer, r.Period,r.UseKind,r.ContinuousDate from Apparatus as a,Reservation as r where a.ID = r.Apparatus_ID ");

        strSQL.AppendFormat("and r.ID ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Goods
    public static DataTable UploadGContinuousQuery(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select g.* ,r.Mission ,r.GName ,r.BorrowedQuantity ,r.StartDate ,r.EndDate ,r.Borrower , r.Ext, r.Email,r.ContinuousCount from Goods as g,GoodsReservation as r where g.ID = r.Goods_ID ");

        strSQL.AppendFormat("and r.ID ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Apparatus
    public static DataTable UploadSContinuousQuery(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select s.* ,r.Mission ,r.GName ,r.StartDate ,r.EndDate ,r.Borrower ,r.Department, r.Ext, r.Email from Sample_New as s,Reservation as r where s.ID = r.Apparatus_ID ");

        strSQL.AppendFormat("and r.ID ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Apparatus
    public static DataTable UploadApparatusStatus(string strSearch, string strKind, string strSearchKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select a.*,(case r.status when 'Y' then '借用中' else '閒置中' end) as status from Apparatus as a,Reservation as r where r.Apparatus_ID =a.ID and ");
        if (strKind == "0")
        {
            if (strSearchKind != "")
                strSQL.AppendFormat("a.Kind = '{0}' and ", strSearchKind);
            strSQL.AppendFormat("(a.Products_ID like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("a.Name like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("a.Brand like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("a.Model like '%{0}%') ", strSearch);
        }
        //else
        //{
        //    strSQL.AppendFormat("ID = '{0}'", strSearch);
        //}

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Goods
    public static DataTable UploadGoodsQuery(string strSearch, string strKind, string strSearchKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.Append("select * from Goods where ");
            if (strSearchKind != "")
                strSQL.AppendFormat("Kind = '{0}' and ", strSearchKind);
            //strSQL.AppendFormat("(Products_ID like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("(Name_En like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("Name_CH like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("MF_CH like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("MF_EN like '%{0}%') ", strSearch);
        }
        else if (strKind == "1")
        {
            strSQL.Append("select * from Goods where ");
            strSQL.AppendFormat("ID = '{0}'", strSearch);
        }
        else if (strKind == "2")
        {
            strSQL.Append("select ID,Part_No,Kind,(Name_En + '-' + Name_CH) as name,(MF_EN + '-' + MF_CH) as MF from Goods where ");
            if (strSearchKind != "")
                strSQL.AppendFormat("Kind = '{0}' and ", strSearchKind);
            //strSQL.AppendFormat("(Products_ID like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("(Name_En like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("Name_CH like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("MF_CH like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("MF_EN like '%{0}%') ", strSearch);
        }
        else
        {
            strSQL.Append("select ID,Part_No,Kind,(Name_En + '-' + Name_CH) as name,(MF_EN + '-' + MF_CH) as MF from Goods where ");
            strSQL.AppendFormat("ID = '{0}'", strSearch);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Sample
    public static DataTable UploadSampleQuery(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.Append("select * from Sample_New ");
            if (strSearch != "")
            {
                strSQL.AppendFormat("where Number like '%{0}%' or ", strSearch);
                strSQL.AppendFormat("ModelName like '%{0}%'", strSearch);
            }

        }
        else if (strKind == "1")
        {
            strSQL.Append("select * from Sample_New where ");
            strSQL.AppendFormat("ID = '{0}'", strSearch);
        }
        else if (strKind == "2")
        {
            strSQL.Append("select ID,Part_No,Kind,(Name_En + '-' + Name_CH) as name,(MF_EN + '-' + MF_CH) as MF from Goods where ");

            strSQL.AppendFormat("(Name_En like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("Name_CH like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("MF_CH like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("MF_EN like '%{0}%') ", strSearch);
        }
        else
        {
            strSQL.Append("select ID,Part_No,Kind,(Name_En + '-' + Name_CH) as name,(MF_EN + '-' + MF_CH) as MF from Goods where ");
            strSQL.AppendFormat("ID = '{0}'", strSearch);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Goods
    public static DataTable UploadRepeatGoods(string strGoods_ID, string strPR_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from PR_Detail where ");
        strSQL.AppendFormat("Goods_ID = '{0}' and PR_ID = '{1}'", strGoods_ID, strPR_ID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_TestCase
    public static DataTable UploadApplication_TestCase(string strDepartment)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,i.File_Path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' order by k.Kind");
        strSQL.AppendFormat("select i.id as id1,i.Kind_ID,i.Function_ID,k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,(i.File_Path + '\\' + i.File_Name) as file_path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note,i.Cost from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Department='{0}' and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' order by k.Kind", strDepartment);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_TestCase
    public static DataTable UploadApplication_TestCase_Temp(string strDepartment)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,i.File_Path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' order by k.Kind");
        strSQL.AppendFormat("select i.id as id1,i.Kind_ID,i.Function_ID,k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,(i.File_Path + '\\' + i.File_Name) as file_path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note,i.Cost from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Department='{0}' and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' order by k.Kind", strDepartment);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_TestCase
    public static DataTable UploadApplication_TestCaseN(string strDepartment,string strKind,string strAKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,i.File_Path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' order by k.Kind");

        if (strKind == "Certification")
            strSQL.AppendFormat("select i.id as id1,i.Kind_ID,i.Function_ID,k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,(i.File_Path + '\\' + i.File_Name) as file_path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note,i.Cost from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Department='{0}' and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' and Kind='{1}' order by id", strDepartment, strKind);
        else
        {
            if (strAKind == "")
                strSQL.AppendFormat("select i.id as id1,i.Kind_ID,i.Function_ID,k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,(i.File_Path + '\\' + i.File_Name) as file_path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note,i.Cost from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Department='{0}' and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' and Kind<>'Certification' order by id", strDepartment, strAKind);
            else
                strSQL.AppendFormat("select i.id as id1,i.Kind_ID,i.Function_ID,k.Kind,f.Name,i.Item,(i.Kind_ID + '-' + i.Function_ID +'-'+ REPLACE(str(i.ID),' ','')) as ID,i.File_Name,(i.File_Path + '\\' + i.File_Name) as file_path,i.File_Name1,i.File_Path1,i.Level1,i.Level2,i.Note,i.Cost from TestCase_Kind as k,TestCase_Function as f,TestCase_Item as i where k.ID=f.Kind_ID and f.Kind_ID = i.Kind_ID and f.ID = i.Function_ID and k.Department='{0}' and k.Disable <> 'Y' and f.Disable <>'Y' and i.Disable <>'Y' and Kind<>'Certification' and Application_Kind = '{1}' order by id", strDepartment, strAKind);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Certification_Wifi
    public static DataTable UploadCertification_Wifi(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from Certification_Wifi where project_id = '{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Certification_BT
    public static DataTable UploadCertification_BT(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from Certification_BT where project_id = '{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Certification_GCF
    public static DataTable UploadCertification_GCF(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from Certification_GCF where project_id = '{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Certification_PTCRB
    public static DataTable UploadCertification_PTCRB(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.AppendFormat("select * from Certification_PTCRB where project_id = '{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadExplanationViewFile
    public static DataTable UploadExplanationViewFile()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select k.Kind,i.Item,(i.Kind_ID + '-' + REPLACE(str(i.ID),' ','')) as ID,i.File_Name,i.File_Path from Explanation_Kind as k,Explanation_Item as i where k.ID=i.Kind_ID order by k.id");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Sample
    public static DataTable UploadSample1(string strSearch, string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from sample where ");
        if (strID != "")
            strSQL.AppendFormat("ID = '{0}' ", strID);
        else
            strSQL.AppendFormat("Name like '%{0}%' ", strSearch);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Sample
    public static DataTable UploadSample_N(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from sample_new ");
        if (strKind == "0")
        {
            if (strSearch != "")
            {
                strSQL.AppendFormat("where (Number like '%{0}%' or ", strSearch);
                strSQL.AppendFormat("ModelName like '%{0}%')  ", strSearch);
            }

        }
        else
        {
            strSQL.AppendFormat("where ID = '{0}'", strSearch);
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    //#region 找尋SampleRelease
    //public static DataTable UploadSampleRelease(string strID)
    //{
    //    MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
    //    StringBuilder strSQL = new StringBuilder();
    //    strSQL.Append("select * from sample_release where ");
    //    strSQL.AppendFormat("sample_id = '{0}'", strID);

    //    DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
    //    return dt;
    //}
    //#endregion

    #region 找尋ApparatusReservation
    public static DataTable UploadApparatusReservation(string strSearch, string strStartDate, string strEndDate, string strKind, string strKindS, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select r.id ,a.Kind ,a.Products_ID ,a.Brand ,a.Model ,a.Number ,a.Name ,r.StartDate ,r.EndDate ,r.Borrower ,a.Custodian_Department ,a.Custodian ,r.Department from Reservation as r,Apparatus as a where ");

        if (strKind == "0")
        {
            strSQL.AppendFormat("r.Apparatus_ID = a.ID and r.StartDate >= '{0}' and r.EndDate <= '{1}' and r.Status ='Y' and ", strStartDate, strEndDate);
            strSQL.AppendFormat("a.Custodian_Department = '{0}' and", strLocal);
        }
        else if (strKind == "1")
            strSQL.AppendFormat("r.Apparatus_ID = a.ID and r.StartDate > '{0}' and r.Department ='{1}' and r.Status <>'C' and ", strStartDate, strEndDate);
        else
        {
            strSQL.AppendFormat("r.Apparatus_ID = a.ID and r.Status ='Y' and ");
            strSQL.AppendFormat("a.Custodian_Department = '{0}' and", strLocal);
        }
        strSQL.AppendFormat("(Products_ID like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("Name like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("Brand like '%{0}%' or ", strSearch);
        if (strKind == "2")
            strSQL.AppendFormat("r.Borrower like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("Model like '%{0}%' ) and ", strSearch);
        strSQL.AppendFormat("r.Apparatus_ID like 'A%' ", strSearch);

        if (strKindS != "")
            strSQL.AppendFormat("and a.Kind = '{0}'", strKindS);

        strSQL.AppendFormat("order by r.EndDate");


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusReservation
    public static DataTable UploadApparatusReservation1(string strSearch, string strStartDate, string strEndDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select r.id ,a.Kind ,a.Products_ID ,a.Custodian ,a.Brand ,a.Model ,a.Number ,a.Name ,r.StartDate ,r.EndDate ,r.Borrower ,a.Custodian_Department ,a.Custodian ,r.Department from Reservation as r,Apparatus as a where ");

        strSQL.AppendFormat("r.Apparatus_ID = a.ID and r.StartDate >= '{0}' and r.EndDate <= '{1}' and r.Status ='Y' and a.Custodian='{2}'", strStartDate, strEndDate, strSearch);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Application_LTE
    public static DataTable UploadApplication_LTE(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Application_LTE where ");

        strSQL.AppendFormat("project_id= '{0}' ", strID);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReservation
    public static DataTable UploadGoodsReservation(string strSearch, string strStartDate, string strEndDate, string strKind, string strKindS)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select g.Part_No,g.Products_ID,r.id ,r.Apparatus_ID ,g.Kind ,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,r.StartDate ,r.EndDate ,r.Borrower ,r.BorrowedQuantity ,g.Custodian ,r.Department from Reservation as r,Goods as g where ");

        if (strKind == "0")
            strSQL.AppendFormat("r.Apparatus_ID = g.ID and r.StartDate >= '{0}' and r.EndDate <= '{1}' and r.Status ='Y' and ", strStartDate, strEndDate);
        else
            strSQL.AppendFormat("r.Apparatus_ID = g.ID and r.StartDate > '{0}' and r.Department ='{1}' and r.Status <>'C' and ", strStartDate, strEndDate);
        strSQL.AppendFormat("(g.Name_En like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("g.Name_CH like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("g.MF_EN like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("g.MF_CH like '%{0}%' ) and ", strSearch);
        strSQL.AppendFormat("r.Apparatus_ID like 'G%' ", strSearch);
        if (strKindS != "")
            strSQL.AppendFormat("and g.Kind = '{0}'", strKindS);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReservation
    public static DataTable UploadGoodsReservation1(string strSearch, string strDate, string strKind, string strKindS, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select g.Part_No,g.Products_ID,r.id ,r.Goods_ID ,g.Kind ,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,r.StartDate ,r.EndDate ,r.Borrower ,r.BorrowedQuantity ,g.Custodian ,r.ContinuousCount ,r.ContinuousDate from GoodsReservation as r,Goods as g where ");

        if (strKind == "0")
            //strSQL.AppendFormat("r.Apparatus_ID = g.ID and r.EndDate >= '{0}' and r.Status ='Y' and ", strDate);
            strSQL.AppendFormat("r.Goods_ID = g.ID and r.Status ='Y' and r.Return_First='' and ");
        else
            strSQL.AppendFormat("r.Goods_ID = g.ID and r.Status ='Y' and r.ContinuousCount<>'' and ");
        //strSQL.AppendFormat("r.Goods_ID = g.ID and r.Status <>'C' and ");
        strSQL.AppendFormat("(g.Name_En like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("g.Name_CH like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("g.MF_EN like '%{0}%' or ", strSearch);
        strSQL.AppendFormat("g.MF_CH like '%{0}%' ) and ", strSearch);
        strSQL.AppendFormat("r.Goods_ID like 'G%' ", strSearch);
        if (strKindS != "")
            strSQL.AppendFormat("and g.Kind = '{0}'", strKindS);

        strSQL.AppendFormat("and g.Custodian_Department = '{0}'", strLocal);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadSampleReservation
    public static DataTable UploadSampleReservation(string strSearch, string strStartDate, string strEndDate, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select r.id ,r.Apparatus_ID ,r.StartDate ,r.EndDate ,r.Borrower ,s.Custodian ,r.Department ,s.* from Reservation as r,sample_new as s where ");

        if (strKind == "0")
            strSQL.AppendFormat("r.Apparatus_ID = s.ID and r.StartDate >= '{0}' and r.EndDate <= '{1}' and r.Status ='Y' ", strStartDate, strEndDate);
        else
            strSQL.AppendFormat("r.Apparatus_ID = s.ID and r.StartDate > '{0}' and r.Department ='{1}' and r.Status <>'C' ", strStartDate, strEndDate);

        if (strSearch != "")
        {
            strSQL.AppendFormat(" and (s.number like '%{0}%' or ", strSearch);
            strSQL.AppendFormat("s.modelname like '%{0}%') and ", strSearch);
            strSQL.AppendFormat("r.Apparatus_ID like 'S%' ", strSearch);
        }


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ReservationDailyReport
    public static DataTable UploadReservationDailyReport(string strSearch)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Reservation_DailyReport where ");

        strSQL.AppendFormat("Reservation_ID ='{0}'", strSearch);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋MaxReservation
    public static DataTable UploadMaxReservation()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select MAX(ID) as ID from Reservation");

        //strSQL.AppendFormat("r.Apparatus_ID =a.ID and r.Department ='{0}'", strSearch);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DepartmentReservation
    public static DataTable UploadDepartmentReservation(string strSearch, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select a.Products_ID ,a.Name ,a.Custodian ,r.StartDate ,r.EndDate ,r.Period ,r.Borrower ,r.ID  from Reservation as r,Apparatus as a where ");

        strSQL.AppendFormat("r.Apparatus_ID =a.ID and a.Custodian ='{0}' and (r.StartDate >= '{1}' or r.EndDate >= '{1}') and r.Status !='C'", strSearch, strDate);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Apparatus
    public static DataTable UploadApparatusProjectListQuery(string strSearch)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select DISTINCT Apparatus.ID,Apparatus.Name from Reservation,Apparatus where ");

        strSQL.AppendFormat("Project_ID = '{0}'", strSearch);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApparatusFile
    public static DataTable UploadApparatusFileQuery(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        if (strKind == "0")
            strSQL.Append("select (File_Path + '\\' + File_Name) as file_path,File_Name from Apparatus_File where ");
        else if (strKind == "1")
            strSQL.Append("select File_Name from Apparatus_File where ");
        else
            strSQL.Append("select Apparatus_ID,File_Name from Apparatus_File where ");

        strSQL.AppendFormat("Apparatus_ID = '{0}'", strSearch);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋SampleFile
    public static DataTable UploadSampleFileQuery1(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        if (strKind == "0")
            strSQL.Append("select (File_Path + '\\' + File_Name) as file_path,File_Name from Sample_File where ");
        else if (strKind == "1")
            strSQL.Append("select File_Name from Sample_File where ");
        else
            strSQL.Append("select Sample_ID,File_Name from Sample_File where ");

        strSQL.AppendFormat("Sample_ID = '{0}' and (File_Name like '%jpg' or File_Name like '%gif' or File_Name like '%png' or File_Name like '%bmp')", strSearch);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsFile
    public static DataTable UploadGoodsFileQuery(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        if (strKind == "0")
            strSQL.Append("select (File_Path + '\\' + File_Name) as file_path,File_Name from Goods_File where ");
        else if (strKind == "1")
            strSQL.Append("select File_Name from Goods_File where ");
        else
            strSQL.Append("select Goods_ID,File_Name from Goods_File where ");

        strSQL.AppendFormat("Goods_ID = '{0}'", strSearch);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadReservationDateQuery
    public static DataTable UploadReservationDateQuery(string strStartDate, string strEndDate, string strID, string strPeriod)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");


        //strSQL.AppendFormat("(( StartDate <='{0}' and EndDate >='{1}' ) or ( StartDate<='{2}' and EndDate>='{3}' ) or ( StartDate>='{4}' and EndDate<='{5}' ) or ( StartDate<='{6}' and EndDate>='{7}' )) and Status <>'N' and Status <> 'C' and Status <> 'E' and Apparatus_ID = '{8}' and Period ='{9}' order by enddate desc", strStartDate, strEndDate, strEndDate, strEndDate, strStartDate, strEndDate, strStartDate, strStartDate, strID, strPeriod);
        strSQL.AppendFormat("(( StartDate <='{0}' and EndDate >='{1}' ) or ( StartDate<='{2}' and EndDate>='{3}' ) or ( StartDate>='{4}' and EndDate<='{5}' ) ) and Status <>'N' and Status <> 'C' and Status <> 'E' and Apparatus_ID = '{6}' and Period ='{7}' order by enddate desc", strStartDate, strEndDate, strEndDate, strEndDate, strStartDate, strEndDate, strID, strPeriod);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadReservationDateAssign
    public static DataTable UploadReservationDateAssign(string strStartDate, string strEndDate, string strID, string strPeriod)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");


        strSQL.AppendFormat("(( StartDate <='{0}' and EndDate >='{1}' ) or ( StartDate<='{2}' and EndDate>='{3}' ) or ( StartDate>='{4}' and EndDate<='{5}' ) or ( StartDate<='{6}' and EndDate>='{7}' )) and Status <>'N' and Status <> 'C' and Status <> 'E' and Status <> '' and Apparatus_ID = '{8}' and Period='{9}' order by enddate desc", strStartDate, strEndDate, strEndDate, strEndDate, strStartDate, strEndDate, strStartDate, strStartDate, strID, strPeriod);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadReservationRepeat
    public static DataTable UploadReservationRepeat(string strApparatus_ID, string strDate, string strDepartment, string strPeriod)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");


        strSQL.AppendFormat("EndDate > '{0}' and Apparatus_ID = '{1}' and Department = '{2}' and Period ='{3}' and Status <> 'C' and Status <> 'N' and Status <> 'E'", strDate, strApparatus_ID, strDepartment, strPeriod);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadReservationAID
    public static DataTable UploadReservationAID(string strReservation_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");


        strSQL.AppendFormat("ID = '{0}'", strReservation_ID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadReservationUsing
    public static DataTable UploadReservationUsing(string strReservation_ID, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");


        strSQL.AppendFormat("Status ='Y' and Apparatus_ID ='{0}' and EndDate >='{1}' order by ID desc", strReservation_ID, strDate);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadReservationUsing1
    public static DataTable UploadReservationUsing1(string strReservation_ID, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Reservation where ");


        strSQL.AppendFormat("Status ='' and Apparatus_ID ='{0}' and EndDate >='{1}' order by ID desc", strReservation_ID, strDate);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadBulletin
    public static DataTable UploadBulletin()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Bulletin where id='1'");



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Reservation
    public static DataTable UploadReservationQuery(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select r.*,a.* from Reservation as r,Apparatus as a where ");

        if (strKind == "0")
            strSQL.AppendFormat("r.Borrower ='{0}' and r.Apparatus_ID = a.ID ", strSearch);
        else
            strSQL.AppendFormat("r.id ='{0}' and r.Apparatus_ID = a.ID ", strSearch);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Reservation
    public static DataTable UploadGoodsReservationQuery(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select g.id,g.Part_No,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF,r.* from GoodsReservation as r,Goods as g where ");

        if (strKind == "0")
            strSQL.AppendFormat("r.Borrower ='{0}' and r.Goods_ID = g.ID ", strSearch);
        else if (strKind == "1")
            strSQL.AppendFormat("r.id ='{0}' and r.Goods_ID = g.ID ", strSearch);
        else
            strSQL.AppendFormat("r.Goods_ID ='{0}' and r.Status ='Y' and r.Goods_ID =g.ID ", strSearch);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Reservation
    public static DataTable UploadSampleReservationQuery(string strSearch, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select r.*,s.* from Reservation as r,Sample_New as s where ");

        if (strKind == "0")
            strSQL.AppendFormat("r.Borrower ='{0}' and r.Apparatus_ID = s.ID ", strSearch);
        else if (strKind == "1")
            strSQL.AppendFormat("r.id ='{0}' and r.Apparatus_ID = s.ID ", strSearch);
        else
            strSQL.AppendFormat("r.Apparatus_ID ='{0}' and r.Status ='Y' and r.Apparatus_ID =s.ID ", strSearch);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region getGoodsReservationList(GoodsReservationList用)
    public static DataTable getGoodsReservationList(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select g.Part_No,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,Cast(r.StartDate as date)as StartDate1,r.ID,(case  when g.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from GoodsReservation as r,Goods as g where r.goods_ID = g.ID and r.Status='' order by r.StartDate");
        else
            strSQL.Append("select g.Part_No,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,Cast(r.StartDate as date)as StartDate1,r.ID,(case  when g.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from GoodsReservation as r,Goods as g where r.goods_ID = g.ID and r.Status='T' order by r.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getSampleReservationList(SampleReservationList用)
    public static DataTable getSampleReservationList(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select s.Number,s.Kind,s.Function_Name,s.Item,s.ModelName ,r.StartDate,r.EndDate,r.ID from Reservation as r,Sample_New as s where r.Apparatus_ID = s.ID and r.Status='' order by r.StartDate");
        else
            strSQL.Append("select s.Number,s.Kind,s.Function_Name,s.Item,s.ModelName ,r.StartDate,r.EndDate,r.ID from Reservation as r,Sample_New as s where r.Apparatus_ID = s.ID and r.Status='T' order by r.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getReservationList(ReservationList用)
    public static DataTable getReservationList(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ID from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate");
        else
            strSQL.Append("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ID from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='T' order by Reservation.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getReservationList(ReservationList用)
    public static DataTable getReservationList1(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' and Apparatus.Custodian='{0}'  and Reservation.Custodian_Check='' order by Reservation.StartDate", strCustodian);
        else if (strKind == "1")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' and Reservation.Custodian_Check='' and Apparatus.Agent='{0}'  order by Reservation.StartDate", strCustodian);
        else if (strKind == "2")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate");
            //strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' and Reservation.Custodian_Check='Y'  order by Reservation.StartDate");
        else if (strKind == "3")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' and Reservation.Custodian_Check='T'  order by Reservation.StartDate");
        else
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' and Reservation.Custodian_Check=''  order by Reservation.StartDate");
        //else
        //    strSQL.Append("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ID from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='T' order by Reservation.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getContinuousList(ProjectApplication用)
    public static DataTable getContinuousList1(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' and Apparatus.Custodian ='{0}' and Reservation.Custodian_Check='' order by Reservation.StartDate", strCustodian);
        else if (strKind == "1")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' and Reservation.Custodian_Check='' and Apparatus.Agent='{0}' order by Reservation.StartDate", strCustodian);
        else if (strKind == "2")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' and Reservation.Custodian_Check='' order by Reservation.StartDate");
            //strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' and Reservation.Custodian_Check='Y' order by Reservation.StartDate");
        else if (strKind == "3")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' and Reservation.Custodian_Check='T' order by Reservation.StartDate");
        else
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' and Reservation.Custodian_Check='' order by Reservation.StartDate");
        //else
        //    strSQL.Append("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.ID from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='T' order by Reservation.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getContinuousList(ProjectApplication用)
    public static DataTable getContinuousList(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.ContinuousDate <>'' order by Reservation.StartDate");
        else
            strSQL.AppendFormat("select Apparatus.Products_ID,Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ContinuousDate,Reservation.Period,Reservation.ID,(case  when Apparatus.Custodian_Department='DA40' then '台北' else '吳江' end) as Custodian_Department from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='T' order by Reservation.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getGoodsContinuousList(ProjectApplication用)
    public static DataTable getGoodsContinuousList(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select g.Part_No,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,Cast(r.StartDate as date)as StartDate1,r.ID from GoodsReservation as r,Goods as g where r.Goods_ID = g.ID and r.ContinuousStatus ='Y' order by r.StartDate");
        else
            strSQL.Append("select g.Part_No,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,Cast(r.StartDate as date)as StartDate1,r.ID from GoodsReservation as r,Goods as g where r.Goods_ID = g.ID and r.Status='T' order by r.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getSampleContinuousList(ProjectApplication用)
    public static DataTable getSampleContinuousList(string strCustodian, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select s.Number,s.Kind,s.Function_Name,s.Item,s.ModelName ,r.StartDate,r.EndDate,r.ContinuousDate,r.ID from Reservation as r,Sample_New as s where r.Apparatus_ID = s.ID and r.ContinuousDate <>'' order by r.StartDate");
        else
            strSQL.Append("select s.Number,s.Kind,s.Function_Name,s.Item,s.ModelName ,r.StartDate,r.EndDate,r.ContinuousDate,r.ID from Reservation as r,Sample_New as s where r.Apparatus_ID = s.ID and r.Status='T' order by r.StartDate");
        //strSQL.AppendFormat("Apparatus.Custodian ='{0}' and Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='' order by Reservation.StartDate", strCustodian);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getGoodsList(ProjectApplication用)
    public static DataTable getGoodsList(string strID, string strStatus)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strID == "")
            strSQL.AppendFormat("select Application_Date,PR_No,PR_Date,Signed_ID,Note,ID,Accepted_Team from PurchasingRequisition where Status = '{0}'", strStatus);
        else
            strSQL.AppendFormat("select * from PurchasingRequisition where ID = '{0}'", strID);
        //else
        //    strSQL.Append("select Apparatus.Name,Apparatus.Brand,Apparatus.Model,Reservation.StartDate,Reservation.EndDate,Reservation.ID from Reservation,Apparatus where Reservation.Apparatus_ID = Apparatus.ID and Reservation.Status='T' order by Reservation.StartDate");




        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getGoodsCash(GoodsList用)
    public static DataTable getGoodsCash(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select sum(convert(float,REPLACE(Estimated_TotalPrice,',',''))) as a  from PR_Detail where PR_ID ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getDelayApparatusList(DelayApparatus用)
    public static DataTable getDelayApparatusList(string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select a.Name ,a.Products_ID ,a.Brand ,a.Model ,r.StartDate ,r.EndDate ,r.Borrower , r.Ext ,r.Agent ,r.AgentExt ,r.ID,a.Custodian_Department  from Reservation as r,Apparatus as a where ");
        //strSQL.AppendFormat("r.EndDate < '{0}' and r.Status <> 'Y' and r.Status <> 'E'", strDate);
        strSQL.AppendFormat("r.EndDate <= '{0}' and r.Status = 'Y' and r.Apparatus_ID =a.ID and r.Apparatus_ID like 'A%'", strDate);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getDelayGoodsList(DelayApparatus用)
    public static DataTable getDelayGoodsList(string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select g.Part_No,(g.Name_En + '-' + g.Name_CH) as Name ,(g.MF_EN + '-' + g.MF_CH) as MF ,r.StartDate ,r.EndDate ,r.Borrower , r.BorrowedQuantity ,r.Agent ,r.ID  from Reservation as r,Goods as g where ");
        //strSQL.AppendFormat("r.EndDate < '{0}' and r.Status <> 'Y' and r.Status <> 'E'", strDate);
        strSQL.AppendFormat("r.EndDate < '{0}' and r.Status = 'Y' and r.Apparatus_ID =g.ID and r.Apparatus_ID like 'G%'", strDate);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getDelaySampleList(DelaySample用)
    public static DataTable getDelaySampleList(string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select s.Number,s.Kind,s.Function_Name,s.Item,s.ModelName ,r.StartDate ,r.EndDate ,r.Borrower , r.Ext ,r.Agent ,r.AgentExt ,r.ID  from Reservation as r,Sample_New as s where ");
        //strSQL.AppendFormat("r.EndDate < '{0}' and r.Status <> 'Y' and r.Status <> 'E'", strDate);
        strSQL.AppendFormat("r.EndDate < '{0}' and r.Status = 'Y' and r.Apparatus_ID =s.ID and r.Apparatus_ID like 'S%'", strDate);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getContinuousApparatusList(DelayApparatus用)
    public static DataTable getContinuousApparatusList(string strDate, string strDepartment, string strNumber)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        //strSQL.Append("select a.Name ,a.Products_ID ,a.kind ,a.Brand ,a.Model ,r.StartDate ,r.EndDate ,r.Borrower , r.Ext ,r.ID  from Reservation as r,Apparatus as a where ");
        //strSQL.AppendFormat("r.Status = 'Y' and r.Apparatus_ID =a.ID ");
        strSQL.Append("select a.Name ,a.Products_ID ,a.kind ,a.Brand ,a.Model ,r.StartDate ,(Case when (r.ContinuousDate <= '1900-01-01 00:00:00.000') then r.EndDate when (r.ContinuousDate > '1900-01-01 00:00:00.000') then r.ContinuousDate when r.ContinuousDate IS NULL then r.EndDate end) as EndDate ,r.Borrower , r.Ext ,r.ID  from Reservation as r,Apparatus as a where ");
        strSQL.AppendFormat("r.Status = 'Y' and r.Apparatus_ID =a.ID ");
        //strSQL.AppendFormat("r.EndDate >= '{0}' and r.Status = 'Y' and r.Apparatus_ID =a.ID ", strDate);

        //if ((strDepartment != "") && (strDepartment != null) && (strDepartment != "ASKEY"))
        //{
        //    strSQL.AppendFormat("and r.Department ='{0}'", strDepartment);
        //}

        if (strDepartment != "ASKEY")
        {
            if ((strNumber != "") && (strNumber != null))
            {
                strSQL.AppendFormat("and r.Borrower ='{0}'", strNumber);
            }
        }
        strSQL.AppendFormat("order by r.EndDate");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getAutoTestLog(TestLogView用)
    public static DataTable getAutoTestLog(string strDep)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select *  from Auto_TestLog ");
        if ((strDep !="DA40") && (strDep != "DA40-SIT"))
            strSQL.AppendFormat("where A_Department >= '{0}' ", strDep);

        //if ((strDepartment != "") && (strDepartment != null) && (strDepartment != "ASKEY"))
        //{
        //    strSQL.AppendFormat("and r.Department ='{0}'", strDepartment);
        //}

        //if (strDepartment != "ASKEY")
        //{
        //    if ((strNumber != "") && (strNumber != null))
        //    {
        //        strSQL.AppendFormat("and r.Borrower ='{0}'", strNumber);
        //    }
        //}
        //strSQL.AppendFormat("order by r.EndDate");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getContinuousGoodsList(DelayGoods用)
    public static DataTable getContinuousGoodsList(string strDate, string strDepartment, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select r.StartDate ,r.EndDate ,r.Borrower , r.Ext ,r.ID,g.*  from GoodsReservation as r,Goods as g where ");
        if (strKind == "0")
            strSQL.AppendFormat("r.EndDate > '{0}' and r.Status = 'Y' and r.Goods_ID =g.ID ", strDate);
        else
            strSQL.AppendFormat("r.EndDate < '{0}' and r.Status = 'Y' and r.Goods_ID =g.ID ", strDate);

        if ((strDepartment != "") && (strDepartment != null) && (strDepartment != "ASKEY"))
        {
            strSQL.AppendFormat("and r.Department ='{0}'", strDepartment);
        }
        strSQL.AppendFormat(" order by r.EndDate");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getContinuousSampleList(DelayApparatus用)
    public static DataTable getContinuousSampleList(string strDate, string strDepartment)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select r.StartDate ,r.EndDate ,r.Borrower , r.Ext ,r.ID,s.*  from Reservation as r,Sample_New as s where ");
        strSQL.AppendFormat("r.EndDate > '{0}' and r.Status = 'Y' and r.Apparatus_ID =s.ID ", strDate);

        if ((strDepartment != "") && (strDepartment != null) && (strDepartment != "ASKEY"))
        {
            strSQL.AppendFormat("and r.Department ='{0}'", strDepartment);
        }


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getReservationView(ReservationView用)
    public static DataTable getReservationView(string strID, string strDate)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select * from Reservation where ");
        strSQL.AppendFormat("Apparatus_ID ='{0}' and startdate >= '{1}'", strID, strDate);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getReservationView(ReservationView用)
    public static DataTable getReservationView1(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select id,StartDate,EndDate,borrower from Reservation where ");
        strSQL.AppendFormat("Apparatus_ID ='{0}'", strID);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region getHistoryReservation(History_R_Detail用)
    public static DataTable getHistoryReservation(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select id,StartDate,(Case when (ContinuousDate <= '1900-01-01 00:00:00.000') then EndDate when (ContinuousDate > '1900-01-01 00:00:00.000') then ContinuousDate when ContinuousDate IS NULL then EndDate end) as EndDate,Borrower,Department,Ext,Status from Reservation where ");
        strSQL.AppendFormat("Apparatus_ID ='{0}' order by id desc", strID);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

        return dt;
    }
    #endregion

    #region 找尋UploadProjectCount
    public static DataTable UploadProjectCount(string strStartDate, string strStartDate1, string strDepartment, string strAssign)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from Project where ");

        strSQL.AppendFormat("(Start_Date >= '{0}' and Start_Date <= '{1}')", strStartDate, strStartDate1);

        if (strDepartment != "ALL")
            strSQL.AppendFormat(" and A_Department = '{0}'", strDepartment);

        if (strAssign != "ALL")
            strSQL.AppendFormat(" and Assign = '{0}'", strAssign);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadTestCaseName
    public static DataTable UploadTestCaseName(string strID, string strKind_ID, string strFunction_ID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select * from TestCase_Item where ");

        strSQL.AppendFormat("kind_id = '{0}' and function_id='{1}' and id='{2}'", strKind_ID, strFunction_ID, strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadTestCaseKind
    public static DataTable UploadTestCaseKind(string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("Select * from TestCase_Kind where ID = '{0}' ", strKind);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋UploadTestCaseMail
    public static DataTable UploadTestCaseMail(string strKind, string strLocation)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select t.*,e.Name_En,e.Email from TestCase_Kind as t,Employees as e where ");

        strSQL.AppendFormat("t.ID='{0}' and t.Custodian_Team = e.Team and TeamLeader ='Y' and e.Location ='{1}'", strKind, strLocation);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DepartmentReport
    public static DataTable UploadReportQuery(string strStartDate, string strEndDate, string strDepartment)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.AppendFormat("select Borrower,Department,(SELECT dbo.GetWorkMinute(StartDate,EndDate)) as usetime,(select dbo.GetWorkMinute('{0}','{1}')) as workday,(SELECT dbo.GetHolidayMinute(StartDate,EndDate)) as useholiday,(SELECT   dbo.GetHolidayMinute('{0}','{1}')) as holiday from Reservation", strStartDate, strEndDate);

        //strSQL.AppendFormat("where a.id=b.FilePath_TestCase_ID and b.file_kind = '{0}'", strName);
        if (strDepartment == "ALL")
            strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,a.Custodian_Department, r.Borrower,r.Department,(SELECT dbo.GetWorkMinute(r.StartDate,r.EndDate)) as UseTime,(select dbo.GetWorkMinute('{0}','{1}')) as WorkTime,(SELECT dbo.GetHolidayMinute(r.StartDate,r.EndDate)) as UseHoliday,(SELECT   dbo.GetHolidayMinute('{0}','{1}')) as Holiday from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.StartDate >= '{0}' and r.EndDate <= '{1}') order by a.id", strStartDate, strEndDate);
        else
            strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,a.Custodian_Department, r.Borrower,r.Department,(SELECT dbo.GetWorkMinute(r.StartDate,r.EndDate)) as UseTime,(select dbo.GetWorkMinute('{0}','{1}')) as WorkTime,(SELECT dbo.GetHolidayMinute(r.StartDate,r.EndDate)) as UseHoliday,(SELECT   dbo.GetHolidayMinute('{0}','{1}')) as Holiday from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.StartDate >= '{0}' and r.EndDate <= '{1}') and a.Custodian_Department = '{2}' order by a.id", strStartDate, strEndDate, strDepartment);
        //strSQL.AppendFormat("");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DepartmentReport
    public static DataTable UploadApparatusReportDep(string strStartDate, string strEndDate, string strDepartment, string strKind, string strCustodian, string strRKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(day,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(day,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        if (strDepartment != "ALL")
            strSQL.AppendFormat("and r.Department ='{0}' ", strDepartment);

        if ((strCustodian != "ALL") && (strCustodian != ""))
            strSQL.AppendFormat("and a.Custodian ='{0}' ", strCustodian);

        if ((strRKind != "ALL") && (strRKind != ""))
            strSQL.AppendFormat("and a.Kind ='{0}' ", strRKind);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}' and ReturnDate = '1900-01-01 00:00:00') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat("and  ((ReturnDate >= '{0}' and ReturnDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and ReturnDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and ReturnDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
        {
            if (strDepartment == "ALL")
                strSQL.AppendFormat("order by r.Department, a.Products_ID");
            else
                strSQL.AppendFormat("order by a.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DepartmentReport_Day
    public static DataTable UploadApparatusReportDep_Day(string strStartDate, string strEndDate, string strDepartment, string strKind, string strCustodian, string strRKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(day,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(day,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        if (strDepartment != "ALL")
            strSQL.AppendFormat("and r.Department ='{0}' ", strDepartment);

        if ((strCustodian != "ALL") && (strCustodian != ""))
            strSQL.AppendFormat("and a.Custodian ='{0}' ", strCustodian);

        if ((strRKind != "ALL") && (strRKind != ""))
            strSQL.AppendFormat("and a.Kind ='{0}' ", strRKind);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}' and ReturnDate = '1900-01-01 00:00:00') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat("and  ((ReturnDate >= '{0}' and ReturnDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and ReturnDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and ReturnDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
        {
            if (strDepartment == "ALL")
                strSQL.AppendFormat("order by r.Department, a.Products_ID");
            else
                strSQL.AppendFormat("order by a.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DepartmentReport_Hour
    public static DataTable UploadApparatusReportDep_Hour(string strStartDate, string strEndDate, string strDepartment, string strKind, string strCustodian, string strRKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //if (strKind == "0")
        //{
        //    strSQL.AppendFormat("select a.id,a.price_use,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
        //    strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,r.EndDate))");
        //    strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" then (DATEDIFF(hour,'{0}','{1}'))", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" then DATEDIFF(hour,'{0}',r.EndDate)", strStartDate);
        //    strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
        //    strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,'{0}')) end)+1 as UseTime ", strEndDate);
        //}
        ////strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(minute,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(minute,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(minute,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(minute,r.StartDate,'{0}')) end)+1 as UseTime ", strEndDate);
        }
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(hour,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        if (strDepartment != "ALL")
            strSQL.AppendFormat("and r.Department ='{0}' ", strDepartment.Replace(" ",""));

        if ((strCustodian != "ALL") && (strCustodian != ""))
            strSQL.AppendFormat("and a.Custodian ='{0}' ", strCustodian);

        if ((strRKind != "ALL") && (strRKind != ""))
            strSQL.AppendFormat("and a.Kind ='{0}' ", strRKind);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}' and ReturnDate = '1900-01-01 00:00:00') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}' and ReturnDate = '1900-01-01 00:00:00') or", strStartDate, strEndDate);
        strSQL.AppendFormat("((StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat("and  ((ReturnDate >= '{0}' and ReturnDate <= '{1}')) or ", strStartDate, strEndDate);
        //strSQL.AppendFormat("(StartDate >= '{0}' and ReturnDate <= '{1}') or", strStartDate, strEndDate);
        //strSQL.AppendFormat("(StartDate <= '{0}' and ReturnDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
        {
            if (strDepartment == "ALL")
                strSQL.AppendFormat("order by r.Department, a.Products_ID");
            else
                strSQL.AppendFormat("order by a.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋KindReport
    public static DataTable UploadApparatusReportKind(string strStartDate, string strEndDate, string strSearchKind, string strKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,a.Kind,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(day,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(day,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        if (strSearchKind != "ALL")
            strSQL.AppendFormat("and a.Kind ='{0}' ", strSearchKind);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
        {
            if (strSearchKind == "ALL")
                strSQL.AppendFormat("order by a.Kind, a.Products_ID");
            else
                strSQL.AppendFormat("order by a.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋KindReport_Hour
    public static DataTable UploadApparatusReportKind_Hour(string strStartDate, string strEndDate, string strSearchKind, string strKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,a.Kind,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(hour,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(hour,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        if (strSearchKind != "ALL")
            strSQL.AppendFormat("and a.Kind ='{0}' ", strSearchKind);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
        {
            if (strSearchKind == "ALL")
                strSQL.AppendFormat("order by a.Kind, a.Products_ID");
            else
                strSQL.AppendFormat("order by a.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReport
    public static DataTable UploadGoodsReportDep(string strStartDate, string strEndDate, string strDepartment, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select g.id,g.Products_ID,(g.Name_En + '-' + g.Name_CH) as Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(hour,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(hour,r.StartDate,r.EndDate)) as total,COUNT(g.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Goods as g where r.Apparatus_ID=g.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'G%' ");
        if (strDepartment != "ALL")
            strSQL.AppendFormat("and r.Department ='{0}' ", strDepartment);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

        if (strKind == "0")
        {
            if (strDepartment == "ALL")
                strSQL.AppendFormat("order by r.Department, g.Products_ID");
            else
                strSQL.AppendFormat("order by g.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReport
    public static DataTable UploadGoodsReportKind(string strStartDate, string strEndDate, string strSearchKind, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
        {
            strSQL.AppendFormat("select g.id,g.Products_ID,(g.Name_En + '-' + g.Name_CH) as Name,r.Mission ,r.GName , r.Borrower,g.Kind,r.Status,r.StartDate,r.EndDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(hour,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(hour,r.StartDate,r.EndDate)) as total,COUNT(g.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Goods as g where r.Apparatus_ID=g.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'G%' ");
        if (strSearchKind != "ALL")
            strSQL.AppendFormat("and g.Kind ='{0}' ", strSearchKind);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);

        if (strKind == "0")
        {
            if (strSearchKind == "ALL")
                strSQL.AppendFormat("order by g.Kind, g.Products_ID");
            else
                strSQL.AppendFormat("order by g.Products_ID");
        }

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReport
    public static DataTable UploadGoodsReportKind1(string strStartDate, string strEndDate, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select g.ID,(g.Name_En + '-' + g.Name_CH) as Name,g.Kind,r.Borrower,r.BorrowedQuantity,r.StartDate,r.EndDate,r.ContinuousCount,r.ContinuousDate from GoodsReservation as r,Goods as g where (r.StartDate >='{0}' and r.StartDate <='{1}') and r.Goods_ID =g.ID ", strStartDate, strEndDate);
        if (strKind != "ALL")
            strSQL.AppendFormat("and g.kind='{0}'  ", strKind);

        strSQL.AppendFormat(" order by g.Kind,Name", strKind);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReport
    public static DataTable UploadGoodsReportStock(string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select ID,Part_No,Kind,(Name_En + '-' + Name_CH) as name,(MF_EN + '-' + MF_CH) as MF,Quantity_Stock from Goods  ");

        if (strKind != "ALL")
            strSQL.AppendFormat(" where kind='{0}'  ", strKind);

        strSQL.AppendFormat(" order by Kind");
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProductReport
    public static DataTable UploadProductReportQuery(string strStartDate, string strEndDate, string strProductsID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,a.Custodian_Department, r.Borrower,r.Department,(SELECT dbo.GetWorkMinute(r.StartDate,r.EndDate)) as UseTime,(select dbo.GetWorkMinute('{0}','{1}')) as WorkTime,(SELECT dbo.GetHolidayMinute(r.StartDate,r.EndDate)) as UseHoliday,(SELECT   dbo.GetHolidayMinute('{0}','{1}')) as Holiday from Reservation as r,Apparatus as a where (r.StartDate >= '{0}' and r.EndDate <= '{1}') and a.Products_ID ='{2}' and r.Apparatus_ID=a.id", strStartDate, strEndDate, strProductsID);
        //strSQL.AppendFormat("");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApplicationReport
    public static DataTable UploadApplicationReport(string strStartDate, string strEndDate, string strDep, string strApplicationID, string strName, string strDepP)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select p.id,p.Application_Date,p.name,p.customer,p.npi,p.A_Department,f.File_Path,f.File_Name,p.jira from Project as p,Attachmen_File_Case as f where p.A_Department ='{0}' and p.ID=f.Project_ID and p.Kind ='驗証申請' and (f.File_Name like 'SIT-%' and f.File_Name like '%.pdf') ", strDep);

        if (strStartDate != "")
            strSQL.AppendFormat(" and (p.Application_Date >='{0}' and p.Application_Date <= '{1}') ", strStartDate, strEndDate);
        if (strApplicationID != "")
            strSQL.AppendFormat(" and p.id='{0}'", strApplicationID);
        if (strName != "")
            strSQL.AppendFormat(" and p.name like '%{0}%'", strName);
        if (strDepP != "")
        {
            if (strDepP != "ALL")
                strSQL.AppendFormat(" and p.A_Name ='{0}'", strDepP);
        }

        strSQL.AppendFormat(" order by p.id desc");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApplicationReport1
    public static DataTable UploadApplicationReport1(string strStartDate, string strEndDate, string strDep, string strApplicationID, string strName, string strDepP)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select p.id,p.Application_Date,p.name,p.customer,p.npi,p.A_Department,f.File_Path,f.File_Name,p.jira from Project as p,Attachmen_File_Case as f where p.ID=f.Project_ID and p.Kind ='驗証申請' and (f.File_Name like 'SIT-%' and f.File_Name like '%.pdf') ", strDep);

        if (strStartDate != "")
            strSQL.AppendFormat(" and (p.Application_Date >='{0}' and p.Application_Date <= '{1}') ", strStartDate, strEndDate);
        if (strApplicationID != "")
            strSQL.AppendFormat(" and p.id='{0}'", strApplicationID);
        if (strName != "")
            strSQL.AppendFormat(" and p.name like '%{0}%'", strName);
        if (strDepP != "")
        {
            if (strDepP != "ALL")
                strSQL.AppendFormat(" and p.A_Department ='{0}'", strDepP);
        }

        strSQL.AppendFormat(" order by p.id desc");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋PRReport
    public static DataTable UploadPRReportQuery(string strKind, string strStart, string strEnd, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select p.*,d.* from PurchasingRequisition as p,PR_Detail as d where p.ID=d.PR_ID ");
        //strSQL.AppendFormat("select p.*,d.*,(g.Name_CH + '-' + g.Name_En) as g_name,g.part_no from PurchasingRequisition as p,PR_Detail as d,Goods as g where p.ID=d.PR_ID and d.Goods_ID =g.ID ");

        if (strKind == "1")
            strSQL.AppendFormat("and p.Status ='Close' and (p.Application_Date >= '{0}' and p.Application_Date <='{1}' and p.Accepted_Team ='{2}')", strStart, strEnd, strLocal);
        else
            strSQL.AppendFormat(" and p.Status ='Open' and p.Accepted_Team ='{0}'", strLocal);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋PRReport
    public static DataTable UploadPRReportQuery1(string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.AppendFormat("select p.*,d.* from PurchasingRequisition as p,PR_Detail as d where p.ID=d.PR_ID and p.Status ='Open' and p.Accepted_Team ='{0}'", strLocal);

        //if (strKind == "1")
        //    strSQL.AppendFormat("and p.Status ='Close' and (p.Application_Date >= '{0}' and p.Application_Date <='{1}')", strStart, strEnd);
        //else
        //    strSQL.AppendFormat(" and p.Status ='Open'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectPerson
    public static DataTable UploadProjectPerson(string strName, string strDep)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.AppendFormat("select * from Project where A_Name ='{0}' and A_Department ='{1}' and Status <> 'Close'", strName, strDep);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectPerson1
    public static DataTable UploadProjectPerson1(string strName, string strDep, string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.AppendFormat("select * from Project as p,Attachmen_File_Case as f where p.A_Name ='{0}' and p.A_Department ='{1}' and p.ID='{2}' and p.ID=f.Project_ID  and (f.File_Name like 'SIT-%' and f.File_Name like '%.pdf')", strName, strDep, strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectPerson
    public static DataTable UploadProjectPerson2(string strName, string strDep)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.AppendFormat("select * from Project where A_Name ='{0}' and A_Department ='{1}' order by ID desc", strName, strDep);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProductReport
    public static DataTable UploadProductReport(string strStartDate, string strEndDate, string strPID, string strKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(day,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(day,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(day,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        strSQL.AppendFormat("and a.Products_ID ='{0}' ", strPID);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
            strSQL.AppendFormat("order by r.Department");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProductReport_ByHour
    public static DataTable UploadProductReport_ByHour(string strStartDate, string strEndDate, string strPID, string strKind, string strLocal)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        if (strKind == "0")
        {
            strSQL.AppendFormat("select a.id,a.price_use,a.Products_ID,a.Name,r.Customer,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,r.Apparatus_Price,r.Period,r.ReturnDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(hour,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(hour,r.StartDate,r.EndDate)) as total,COUNT(a.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Apparatus as a where r.Apparatus_ID=a.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'A%' ");
        strSQL.AppendFormat("and a.Products_ID ='{0}' ", strPID);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}'))", strStartDate, strEndDate);
        strSQL.AppendFormat(" and a.Custodian_Department ='{0}'", strLocal);

        if (strKind == "0")
            strSQL.AppendFormat("order by r.Department");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋GoodsReport
    public static DataTable UploadGoodsReport(string strStartDate, string strEndDate, string strGID, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        if (strKind == "0")
        {
            strSQL.AppendFormat("select g.id,(g.Name_En + '-' + g.Name_CH) as Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,");
            strSQL.AppendFormat("(case when (r.StartDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,r.EndDate))");
            strSQL.AppendFormat(" when (r.StartDate <= '{0}' and r.EndDate >= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,'{0}','{1}'))", strStartDate, strEndDate);
            strSQL.AppendFormat(" when (r.EndDate >= '{0}' and r.EndDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then DATEDIFF(hour,'{0}',r.EndDate)", strStartDate);
            strSQL.AppendFormat(" when (r.StartDate >= '{0}' and r.StartDate <= '{1}')", strStartDate, strEndDate);
            strSQL.AppendFormat(" then (DATEDIFF(hour,r.StartDate,'{0}')) end) as UseTime ", strEndDate);
        }
        //strSQL.AppendFormat("select a.id,a.Products_ID,a.Name,r.Mission ,r.GName , r.Borrower,r.Department,r.Status,r.StartDate,r.EndDate,DATEDIFF(hour,r.StartDate,r.EndDate) as UseTime ");
        else
            strSQL.AppendFormat("select SUM(DATEDIFF(hour,r.StartDate,r.EndDate)) as total,COUNT(g.id) as tcount ");

        strSQL.AppendFormat("from Reservation as r,Goods as g where r.Apparatus_ID=g.id and (r.Status = 'Y' or r.Status ='E') and r.Apparatus_ID like 'G%' ");
        strSQL.AppendFormat("and ((g.Name_En like '%{0}%' or g.Name_CH like '%{0}%') ", strGID);

        strSQL.AppendFormat("and  ((EndDate >= '{0}' and EndDate <= '{1}') or ", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and EndDate <= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate <= '{0}' and EndDate >= '{1}') or", strStartDate, strEndDate);
        strSQL.AppendFormat("(StartDate >= '{0}' and StartDate <= '{1}')))", strStartDate, strEndDate);

        if (strKind == "0")
            strSQL.AppendFormat("order by r.Department");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectCase_Kind
    public static DataTable SelectProjectCase_Kind(string strProjectID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from dbo.Attachmen_File "); ;
        strSQL.AppendFormat("WHERE Project_ID = '{0}'", strProjectID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Employee(Team)
    public static DataTable UploadTeamEmployee(string strTeam)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select p.Assign,COUNT(p.id) as total from ProjectCase as p,Employees as e where p.Assign = e.Name_En "); ;

        if (strTeam != "ALL")
            strSQL.AppendFormat(" and e.Team ='{0}'", strTeam);

        strSQL.AppendFormat(" group by p.Assign");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApplicationTestCase
    public static DataTable UploadApplicationTestCase(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Application_TestCase "); ;

        strSQL.AppendFormat(" where Project_ID ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApplicationTestCase
    public static DataTable UploadApplicationTestCase_Temp(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Application_TestCase_Temporarily "); ;

        strSQL.AppendFormat(" where Project_ID ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ApplicationTestCase
    public static DataTable UploadCustomerTestCase(string strID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select TestCase from Customer_TestCase "); ;

        strSQL.AppendFormat(" where Customer ='{0}'", strID);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋StatusCase
    public static DataTable UploadStatusCase(string strStatus, string strName)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select COUNT(id) as case1 from ProjectCase "); ;

        strSQL.AppendFormat("where Status ='{0}' and Assign = '{1}'", strStatus, strName);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Team
    public static DataTable UploadTeam()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select COUNT(id) as case1 from ProjectCase "); ;

        strSQL.AppendFormat("Select Name from InfoData Where Kind = '4'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Department
    public static DataTable UploadDepartment()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select COUNT(id) as case1 from ProjectCase "); ;

        strSQL.AppendFormat("Select Name from InfoData Where Kind = '3'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Goods
    public static DataTable UploadGoodsKind()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select COUNT(id) as case1 from ProjectCase "); ;

        strSQL.AppendFormat("Select Name from InfoData Where Kind = '10'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Goods
    public static DataTable UploadApparatusKind()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        //strSQL.Append("select COUNT(id) as case1 from ProjectCase "); ;

        strSQL.AppendFormat("Select Name from InfoData Where Kind = '7'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 計算部門案件數量
    public static DataTable UploadTeamCase(string strTeam, string strStart, string strEnd, string strFunction)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        strSQL.Append("select COUNT(projectcase.id) as total from Project,ProjectCase "); ;

        strSQL.AppendFormat("where Team = '{0}' and project.ID=projectcase.Project_ID and (Project.Start_Date >='{1}' and Project.End_Date <='{2}' and Project.Kind = '{3}')", strTeam, strStart, strEnd, strFunction);


        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Sample
    public static DataTable UploadSample()
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from sample");

        //strSQL.AppendFormat("and TestPlan.ID='{0}'", strNo);



        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋SampleRelease
    public static DataTable UploadSampleRelease(string strID, string strSID)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select * from Sample_Release");

        //strSQL.AppendFormat("and TestPlan.ID='{0}'", strNo);
        if (strID != "")
            strSQL.AppendFormat(" where id ='{0}' and sample_id='{1}'", strID, strSID);
        else
            strSQL.AppendFormat(" where sample_id ='{0}'", strSID);

        //if (strSID != "")
        //    strSQL.AppendFormat(" where sample_id ='{0}'", strSID);


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

    #region 找尋InfoData
    public static DataTable UploadFunction_List(string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strKind == "0")
            strSQL.Append("select MAX(Parent_Function_No) as ID from Function_List ");
        else
            strSQL.Append("select MAX(Sequence) as ID from Function_List where Function_Name <> '系統設定'");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectStatistics
    public static DataTable UploadProjectStatistics(string strDepartment, string strStartDate, string strEndDate, string strTeam, string strProject, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //strSQL.AppendFormat("select (CASE When p.A_Department2 IS NULL Then p.A_Department Else p.A_Department2 End )as Department,p.Name,COUNT(c.ID) as Total,p.ID from Project as p, ProjectCase as c where p.ID=c.Project_ID and (p.Kind='驗証申請' or p.Kind='專案支援')");
        strSQL.AppendFormat("select (CASE When p.A_Department2 IS NULL Then p.A_Department Else p.A_Department2 End )as Department,p.Name,COUNT(c.ID) as Total,p.ID from Project as p, ProjectCase as c where p.ID=c.Project_ID ");

        if (strDepartment != "")
            strSQL.AppendFormat(" and p.A_Department ='{0}' ", strDepartment);

        if (strStartDate != "")
            strSQL.AppendFormat(" and p.Start_Date>='{0}' and p.End_Date <='{1}' ", strStartDate, strEndDate);

        if (strTeam != "")
            strSQL.AppendFormat(" and p.Accepted_Team ='{0}' ", strTeam);

        if (strProject != "")
            strSQL.AppendFormat(" and p.Name ='{0}' ", strProject);

        if (strKind != "")
        {
            if (strKind == "驗証申請")
                strSQL.AppendFormat(" and (p.Kind='驗証申請' or p.Kind='專案支援') ");
            else
                strSQL.AppendFormat(" and p.Kind ='{0}' ", strKind);
        }
        else
        {
            strSQL.AppendFormat(" and (p.Kind='驗証申請' or p.Kind='認証申請' or p.Kind='專案支援') ");
        }

        strSQL.AppendFormat("group by (CASE When p.A_Department2 IS NULL Then p.A_Department Else p.A_Department2 End ),p.name,p.ID order by Department");


        //strSQL.AppendFormat("select p.A_Department,p.Name,COUNT(c.ID) as Total,p.ID from Project as p,ProjectCase as c where p.ID=c.Project_ID and p.Kind='驗証申請'");

        //if (strDepartment != "")
        //    strSQL.AppendFormat(" and p.A_Department ='{0}' ", strDepartment);

        //if (strStartDate != "")
        //    strSQL.AppendFormat(" and p.Start_Date>='{0}' and p.End_Date <='{1}' ", strStartDate, strEndDate);

        //if (strTeam != "")
        //    strSQL.AppendFormat(" and p.Accepted_Team ='{0}' ", strTeam);

        //if (strProject != "")
        //    strSQL.AppendFormat(" and p.Name ='{0}' ", strProject);

        //strSQL.AppendFormat(" group by p.A_Department,p.name,p.ID order by p.A_Department");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectStatistics
    public static DataTable UploadProjectStatistics1(string strDepartment, string strStartDate, string strEndDate, string strTeam, string strProject, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();


        //strSQL.AppendFormat("select (CASE When A_Department2 IS NULL Then A_Department Else A_Department2 End )as Department,COUNT(name) as Total from Project where (Kind='驗証申請' or Kind='專案支援')");
        strSQL.AppendFormat("select (CASE When A_Department2 IS NULL Then A_Department Else A_Department2 End )as Department,COUNT(name) as Total from Project where ");

        if (strKind != "")
        {
            if (strKind == "驗証申請")
                strSQL.AppendFormat(" (Kind='驗証申請' or Kind='專案支援') ");
            else
                strSQL.AppendFormat(" Kind ='{0}' ", strKind);
        }
        else
        {
            strSQL.AppendFormat(" (Kind='驗証申請' or Kind='認証申請' or Kind='專案支援') ");
        }

        if (strDepartment != "")
            strSQL.AppendFormat(" and A_Department ='{0}' ", strDepartment);


        if (strStartDate != "")
            strSQL.AppendFormat(" and Start_Date>='{0}' and End_Date <='{1}' ", strStartDate, strEndDate);


        if (strTeam != "")
            strSQL.AppendFormat(" and Accepted_Team ='{0}' ", strTeam);


        if (strProject != "")
            strSQL.AppendFormat(" and Name ='{0}' ", strProject);

        


        strSQL.AppendFormat(" group by (CASE When A_Department2 IS NULL Then A_Department Else A_Department2 End ) order by Department");

        //strSQL.AppendFormat("select A_Department,COUNT(name) as Total from Project where Kind='驗証申請'");

        //if (strDepartment != "")
        //    strSQL.AppendFormat(" and A_Department ='{0}' ", strDepartment);


        //if (strStartDate != "")
        //    strSQL.AppendFormat(" and Start_Date>='{0}' and End_Date <='{1}' ", strStartDate, strEndDate);


        //if (strTeam != "")
        //    strSQL.AppendFormat(" and Accepted_Team ='{0}' ", strTeam);


        //if (strProject != "")
        //    strSQL.AppendFormat(" and Name ='{0}' ", strProject);


        //strSQL.AppendFormat(" group by A_Department order by A_Department");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectCaseStatistics
    public static DataTable UploadProjectCaseStatistics(string strStartDate, string strEndDate, string strTeam)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select c.Kind ,c.name ,COUNT(c.ID) as Total from ProjectCase as c,Project as p where p.ID=c.Project_ID and p.Kind='驗証申請'");

        if (strStartDate != "")
            strSQL.AppendFormat(" and p.Start_Date>='{0}' and p.End_Date <='{1}' ", strStartDate, strEndDate);

        if (strTeam != "")
            strSQL.AppendFormat(" and p.Accepted_Team ='{0}' ", strTeam);

        strSQL.AppendFormat("group by c.Kind,c.name order by c.Kind,c.name");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋ProjectCaseKind
    public static DataTable UploadProjectCaseKind(string strName, string strTeam)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select * from TestCase_Function as f,TestCase_Kind as k where (k.Kind +' '+ f.Name)='{0}' and k.ID=f.Kind_ID ", strName);
        if (strTeam != "")
            strSQL.AppendFormat(" and k.Department='{0}'", strTeam);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion


    #region 找尋StatisticsCReport
    public static DataTable UploadStatisticsCReport(string strDepartment, string strStartDate, string strEndDate, string strKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        //strSQL.AppendFormat("select (CASE When p.A_Department2 IS NULL Then p.A_Department Else p.A_Department2 End )as Department,p.Name,COUNT(c.ID) as Total,p.ID from Project as p, ProjectCase as c where p.ID=c.Project_ID and (p.Kind='驗証申請' or p.Kind='專案支援')");
        strSQL.AppendFormat("select (CASE When p.A_Department2 IS NULL Then p.A_Department Else p.A_Department2 End )as Department,Replace(c.Kind,'Certification ','') as Kind,p.Name,c.Lab,(CONVERT(varchar, p.Start_Date, 111) + '~' + CONVERT(varchar, p.End_Date, 111)) as DateRange,sum(cast(Replace(c.Quoted,',','') AS int)) as Quoted,sum(cast(Replace(c.Reimburse,',','') AS int)) as Reimburse,p.ID from Project as p, ProjectCase as c where p.ID=c.Project_ID and p.Kind='認証申請' ");

        if (strKind != "ALL")
            strSQL.AppendFormat(" and c.Kind like '%{0}%' ", strKind);

        if (strDepartment != "ALL")
            strSQL.AppendFormat(" and p.A_Department ='{0}' ", strDepartment);

        if (strStartDate != "")
            strSQL.AppendFormat(" and p.Start_Date>='{0}' and p.End_Date <='{1}' ", strStartDate, strEndDate);



        strSQL.AppendFormat("group by (CASE When p.A_Department2 IS NULL Then p.A_Department Else p.A_Department2 End ),p.name,p.ID,c.Kind,c.Lab,p.Start_Date, p.End_Date order by Department");


        //strSQL.AppendFormat("select p.A_Department,p.Name,COUNT(c.ID) as Total,p.ID from Project as p,ProjectCase as c where p.ID=c.Project_ID and p.Kind='驗証申請'");

        //if (strDepartment != "")
        //    strSQL.AppendFormat(" and p.A_Department ='{0}' ", strDepartment);

        //if (strStartDate != "")
        //    strSQL.AppendFormat(" and p.Start_Date>='{0}' and p.End_Date <='{1}' ", strStartDate, strEndDate);

        //if (strTeam != "")
        //    strSQL.AppendFormat(" and p.Accepted_Team ='{0}' ", strTeam);

        //if (strProject != "")
        //    strSQL.AppendFormat(" and p.Name ='{0}' ", strProject);

        //strSQL.AppendFormat(" group by p.A_Department,p.name,p.ID order by p.A_Department");

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    //=================================debbie SIT Benchmark 20180503====================================
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
    //=================================debbie SIT Benchmark 20180503====================================

    #region 利用名字找尋員工 mail
    public static DataTable UploadEmployeeMail(string Name)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select Email from Employees where Name_CH='{0}' or Name_En='{0}'", Name);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋DQA mail
    public static DataTable UploadDQA(string Code_No, string Name)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("Select Name, Mail from Number Where Department = '{0}' and Name = '{1}'", Code_No, Name);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Attachmen_File_Case是否重複
    public static DataTable UploadAttachmenFileCase(string strName, string strPath1)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        strSQL.AppendFormat("select * from Attachmen_File_Case where File_Name ='{0}' and File_Path ='{1}' ", strName, strPath1);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Certification_Wifi
    public static DataTable UploadCertification_Wifi_Data(string strID , string strSearchKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strSearchKind == "0")
            strSQL.AppendFormat("select * from Certification_Wifi_Data where id = '{0}' ", strID);
        else
            strSQL.AppendFormat("select ID,Name from Certification_Wifi_Data where Kind = '{0}' and disable <>'Y' ", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion

    #region 找尋Certification_BT
    public static DataTable UploadCertification_BT_Data(string strID, string strSearchKind)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strSearchKind == "0")
            strSQL.AppendFormat("select * from Certification_BT_Data where id = '{0}' ", strID);
        else
            strSQL.AppendFormat("select ID,Name from Certification_BT_Data where Kind = '{0}' and disable <>'Y' ", strID);

        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion
}
