using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// AjaxCalendar 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
// [System.Web.Script.Services.ScriptService]
public class AjaxCalendar : System.Web.Services.WebService {

    private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    public AjaxCalendar () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    public void GetRecordNote(string start, string end)
    {
        //string sql = "select id as \"id\",GET_DEPANAME(t.depa) as  \"title\",to_char(t.rq,'yyyy-mm-dd') AS \"start\" from record_note t " +
        //    " where to_char(rq,'yyyyMMdd')>='" + TimeStamp.GetTime(start).ToString("yyyyMMdd") + "' and " +
        //    " to_char(rq,'yyyyMMdd')<='" + TimeStamp.GetTime(end).ToString("yyyyMMdd") + "' " +
        //    " order by t.depa";
        //DataTable dt = cfo.ReturnDataSet(sql, "RECORD_NOTE").Tables[0];
        //string data = JsonHelper.DataToJson(dt);
        //cfo.CloseConn();
        string strApparatusID_Cookie;
        HttpCookie cookie_ApparatusID = HttpContext .Current.Request.Cookies["ApparatusID"];
        strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select id,borrower,StartDate,EndDate from Reservation where ");
        strSQL.AppendFormat("Apparatus_ID ='{0}'", strApparatusID_Cookie);
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        string data = DataToJson(dt);
        Context.Response.Write(data);

    }

    private string DataToJson(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"Name\":\"" + dt.TableName + "\",\"Rows");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][j].ToString().Replace("\"", "\\\""));
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }
    
}

