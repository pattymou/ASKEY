using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using DayPilot.Web.Ui;
//using DayPilot.Web.Ui.Recurrence;




/// <summary>
/// clsScheduler 的摘要描述
/// </summary>
public class clsScheduler
{
    #region 取得系統連線字串

    private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    #endregion

    #region 找尋InfoProject
    public static DataTable UploadInfoProject(string strI,string strID,string strList)
    {
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select Project.Name as projectname,ProjectCase.Project_ID +'-'+ replace(str(ProjectCase.ID),' ','') as projectid, ProjectCase.Name,ProjectCase.Assign,ProjectCase.Start_Date,ProjectCase.End_Date,ProjectCase.Explain_Case from Project,ProjectCase where Project.ID = ProjectCase.Project_ID and ProjectCase.Start_Date != '1900-01-01' and ProjectCase.End_Date != '1900-01-01'");
        //strSQL.Append("from InfoData ");
        //strSQL.AppendFormat("WHERE Kind = '{0}' Order by Name", intrKind);
        if (strI == "1")
            strSQL.AppendFormat("and project.id = '{0}'", strID);
        else if (strI == "2")
            strSQL.AppendFormat("and ProjectCase.Assign = '{0}' and Project.Kind = '{1}'", strID, strList);
        else
            strSQL.AppendFormat("and Project.Kind = '{0}'", strList);

        strSQL.AppendFormat("and ProjectCase.Status != 'Close'", strList);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        return dt;
    }
    #endregion
}
