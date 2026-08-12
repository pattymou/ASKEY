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
/// clsDropDownList 的摘要描述
/// </summary>
public static class clsDropDownList
{
        #region 取得系統連線字串

        private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        #endregion

        #region ddlInfoFunction
        public static object ddlInfoFunction(DropDownList FunDropDownList, int Code_No, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select Name from InfoData Where Kind = '{0}'", Code_No);
            if ((Code_No == 7) || (Code_No == 1))
                strSQL.AppendFormat(" order by Name");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //FunDropDownList.Items.Add(new ListItem("", ""));
            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlInfoFunction
        public static object ddlInfoKind_NPI(DropDownList FunDropDownList, int Code_No, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select Name from InfoData Where Kind = '{0}' and Name like '%NPI%'", Code_No);
            if ((Code_No == 7) || (Code_No == 1))
                strSQL.AppendFormat(" order by Name");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //FunDropDownList.Items.Add(new ListItem("", ""));
            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlNumberD
        public static object ddlNumberD(DropDownList FunDropDownList, string Code_No, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select Name from Number Where Department = '{0}'", Code_No);
            
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //FunDropDownList.Items.Add(new ListItem("", ""));
            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlInfoFunction
        public static object ddlInfoFunction1(DropDownList FunDropDownList, int Code_No, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select Name from InfoData Where Kind = '7' or kind ='10' order by Name", Code_No);
            //if (Code_No == 7)
            //    strSQL.AppendFormat(" order by Name");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //FunDropDownList.Items.Add(new ListItem("", ""));
            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString().Trim()));
            }
            return FunDropDownList;
        }
        #endregion        


        #region ddlApplication_TestCase_Kind
        public static object ddlApplication_TestCase_Kind(DropDownList FunDropDownList,string strDepartment,string strApplication_Kind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select ID,Kind from TestCase_Kind where disable<>'Y' and Department ='{0}' and Application_Kind ='{1}'", strDepartment, strApplication_Kind);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));
            //if (strKind == "0")
            //    FunDropDownList.Items.Add(new ListItem("", ""));
            //else
            //    FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Kind"].ToString(), dr["ID"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlApparatusKind
        public static object ddlApparatusKind(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select ID,Name from Apparatus where Kind ='{0}'", strKind);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));
            //if (strKind == "0")
            //    FunDropDownList.Items.Add(new ListItem("", ""));
            //else
            //    FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlExplanation_Kind
        public static object ddlExplanation_Kind(DropDownList FunDropDownList)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select ID,Kind from Explanation_Kind");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));
            //if (strKind == "0")
            //    FunDropDownList.Items.Add(new ListItem("", ""));
            //else
            //    FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Kind"].ToString(), dr["ID"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlApplication_TestCase_Function
        public static object ddlApplication_TestCase_Function(DropDownList FunDropDownList, string strID)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select ID,Name from TestCase_Function where Kind_ID = '{0}' and disable <>'Y'",strID);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlExplanation_Item
        public static object ddlExplanation_Item(DropDownList FunDropDownList, string strID)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select ID,Item from Explanation_Item where Kind_ID = '{0}'", strID);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Item"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlApplication_TestCase_Function
        public static object ddlApplication_TestCase_Function1(DropDownList FunDropDownList, string strID)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat ("Select ID,Name from TestCase_Function where Kind_ID = '{0}' and disable <>'Y'", strID);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlApplication_TestCase_Item
        public static object ddlApplication_TestCase_Item(DropDownList FunDropDownList, string strID, string strFunctionID)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select ID,Item from TestCase_Item where Kind_ID = '{0}' and Function_ID = '{1}' and disable <>'Y'", strID, strFunctionID);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Item"].ToString(), dr["ID"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlTestCaseFunction
        public static object ddlTestCaseFunction(DropDownList FunDropDownList)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("Select TestCase from FilePath_TestCase");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["TestCase"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlDepartment
        public static object ddlDepartment(DropDownList FunDropDownList,string strDepartment,string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select Name from InfoData where kind='3' order by Name");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            //FunDropDownList.Items.Add(new ListItem("", ""));
            //FunDropDownList.Items.Add(new ListItem(strDepartment));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlTeam
        public static object ddlTeam(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select Name from InfoData where kind='4'");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            //FunDropDownList.Items.Add(new ListItem("", ""));
            //FunDropDownList.Items.Add(new ListItem(strDepartment));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlEmployees
        public static object ddlEmployees(DropDownList FunDropDownList,string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("Select Name_En from Employees");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name_En"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlEmployees
        public static object ddlEmployees_CH(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("Select Name_CH,Name_En from Employees");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name_CH"].ToString(), dr["Name_En"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlFunction
        public static object ddlDashBoardFunction(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select Function_Name from Function_List where Expand ='Y' and Model ='Y'");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));

            FunDropDownList.Items.Add(new ListItem("驗証申請"));
            FunDropDownList.Items.Add(new ListItem("實驗室管理"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Function_Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlFunction
        public static object ddlStatisticsProject(DropDownList FunDropDownList, string strKind, string strDepartment)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("select distinct Name   from Project where Kind='驗証申請' and A_Department ='{0}'", strDepartment);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlTeamEmployees
        public static object ddlTeamEmployees(DropDownList FunDropDownList, string strKind, string strTeam)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);


            if (strTeam == "ALL")
                strSQL.AppendFormat("Select Name_En from Employees ");
            else
                strSQL.AppendFormat("Select Name_En from Employees where Team = '{0}' ",strTeam);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name_En"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlDepartmentNumber
        public static object ddlDepartmentNumber(DropDownList FunDropDownList, string strKind, string strDepartment)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("Select Name from Number where Department = '{0}' ", strDepartment);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlApparatusDepartment
        public static object ddlApparatusDepartment(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("Select DISTINCT Custodian_Department from Apparatus");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Custodian_Department"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlTestPlan
        public static object ddlTestPlan(DropDownList FunDropDownList)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select DISTINCT Customer from TestPlan");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //if (strKind == "0")
            //    FunDropDownList.Items.Add(new ListItem("", ""));
            //else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Customer"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlTestCaseKind
        public static object ddlTestCaseKind(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select Name from InfoData where Kind='8'");
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlCategory
        public static object ddlCategory(DropDownList FunDropDownList,string strKind,string strCustomer)
        {
            int intI = 0;
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select DISTINCT Category from TestPlan ");

            if (strKind != "ALL")
            {
                strSQL.AppendFormat("where Kind = '{0}' ", strKind);
                intI = 1;
            }

            if (strCustomer != "ALL")
            {
                if (intI==0)
                    strSQL.AppendFormat("where Customer = '{0}'", strCustomer);
                else
                    strSQL.AppendFormat("and Customer = '{0}'", strCustomer);
            }
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //if (strKind == "0")
            //    FunDropDownList.Items.Add(new ListItem("", ""));
            //else
            FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Category"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlRequirementOwner
        public static object ddlRequirementOwner(DropDownList FunDropDownList,string strID)
        {
            //int intI = 0;
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select DISTINCT owner from Requirement ");


            strSQL.AppendFormat("where Requirement_ID like '%{0}%' ", strID);



            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            //if (strKind == "0")
            //    FunDropDownList.Items.Add(new ListItem("", ""));
            //else
            FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["owner"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region loadP_Name
        public static object ddlP_Name(DropDownList FunDropDownList, string strCustomer, string strKind1)
        {
            int intI = 0;
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select DISTINCT ProductName from ProjectList ");

            if (strCustomer != "ALL")
            {
                if (intI == 0)
                    strSQL.AppendFormat("where Customer = '{0}'", strCustomer);
                else
                    strSQL.AppendFormat("and Customer = '{0}'", strCustomer);
            }
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            if (strKind1 == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));
            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["ProductName"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlModelWeb
        public static object ddlModelWeb(DropDownList FunDropDownList)
        {
            int intI = 0;
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.Append("select Function_Name,Function_No from Function_List where Model = 'Y'");


            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);


            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Function_Name"].ToString(), dr["Function_No"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlProjectName
        public static object ddlProjectName(DropDownList FunDropDownList, string strID)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("select DISTINCT Name from Project where Customer ='{0}' and Kind='驗証申請' ", strID);
            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);

            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["Name"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlCertification_Wifi_Optional
        public static object ddlCertification_Wifi_Optional(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("select id,name from Certification_Wifi_Data where Kind = '{0}' and disable <> 'Y'", strKind);


            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);


            FunDropDownList.Items.Add(new ListItem("", ""));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlCertification_BT
        public static object ddlCertification_BT(DropDownList FunDropDownList, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("select id,name from Certification_BT_Data where Kind = '{0}' and disable <> 'Y'", strKind);


            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);


            FunDropDownList.Items.Add(new ListItem("", ""));


            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

        #region ddlCertification_Kind
        public static object ddlCertification_Kind(DropDownList FunDropDownList, string strNumber, string strKind)
        {
            FunDropDownList.Items.Clear();

            StringBuilder strSQL = new StringBuilder();
            MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);

            strSQL.AppendFormat("select * from TestCase_Function where Kind_ID ='{0}' and Disable <>'Y'", strNumber);


            DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);


            //FunDropDownList.Items.Add(new ListItem("", ""));
            if (strKind == "0")
                FunDropDownList.Items.Add(new ListItem("", ""));
            else
                FunDropDownList.Items.Add(new ListItem("ALL"));

            foreach (DataRow dr in dt.Rows)
            {
                FunDropDownList.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
            return FunDropDownList;
        }
        #endregion

	
}
