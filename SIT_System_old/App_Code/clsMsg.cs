using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

    /// <summary>
    /// clsMsg 的摘要描述
    /// 交易訊息及LOG檔均在此作業
    /// </summary>
    public static class clsMsg
    {
        #region AlertMessage
        public static void AlertMessage(string msg, System.Web.UI.Page alertPage)
        {
            msg = msg.Replace("'", "|").Replace('\n'.ToString(), ",,").Replace('\r'.ToString(), ",,");
            //alertPage.RegisterClientScriptBlock("_AlertMessage", "<script language='javascript'>window.alert('" + msg + "');</script>");
            string js = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(alertPage, typeof(string), "", js, true);
        }
        #endregion

       

        #region GetMessageandClosePageScript
        public static string GetMessageandClosePageScript(string msg)
        {
            return "<Script Language=\"JavaScript\"> \n" +
                   "alert('" + msg + "'); \n" +
                   "window.close(); \n" +
                   "</Script> \n";
        }
        #endregion
    }