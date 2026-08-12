using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;


public partial class WebForm_ProjectView : System.Web.UI.Page
{
    //public static string strFun;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL;
        DataTable dt1;

        Session["Fun"] = Request.QueryString["Fun"];
        dt1 = clsData.getFunction_Name(Session["Fun"].ToString());

        Session["Upload_Project_Kind"] = dt1.Rows[0]["Function_Name"].ToString();


        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");


        string strWrite;

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            lblAdd.Visible = false;
        }

        DataTable dt;

        if (Session["Fun"].ToString() == "9")
        {
            string strName;

            strName = Request.QueryString["ID"];
            string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Open_" + Session["EmpNo"].ToString() + ".txt";
            dt = clsData.getProjectList_App("", "3", "Open", "", dt1.Rows[0]["Function_Name"].ToString(), strName);
            dataTableToText(dt, 11, strPath1);

            dt = clsData.getProjectList_App("", "3", "Close", "", dt1.Rows[0]["Function_Name"].ToString(), strName);
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Close_" + Session["EmpNo"].ToString() + ".txt";
            dataTableToText(dt, 11, strPath1);

            dt = clsData.getProjectList_App("", "3", "Hold", "", dt1.Rows[0]["Function_Name"].ToString(), strName);
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Hold_" + Session["EmpNo"].ToString() + ".txt";
            dataTableToText(dt, 11, strPath1);
            /////////////////////////
            
            //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Open_WJ.txt";
            //dt = clsData.getProjectList_App("", "3", "Open", "吳江", dt1.Rows[0]["Function_Name"].ToString(), strName);
            //dataTableToText(dt, 10, strPath1);

            //dt = clsData.getProjectList_App("", "3", "Close", "吳江", dt1.Rows[0]["Function_Name"].ToString(), strName);
            //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Close_WJ.txt";
            //dataTableToText(dt, 10, strPath1);

            //dt = clsData.getProjectList_App("", "3", "Hold", "吳江", dt1.Rows[0]["Function_Name"].ToString(), strName);
            //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Hold_WJ.txt";
            //dataTableToText(dt, 10, strPath1);
        }
        else
        {
            string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Open_" + Session["EmpNo"].ToString() + ".txt";
            dt = clsData.getProjectList("", "3", "Open", "", dt1.Rows[0]["Function_Name"].ToString());
            dataTableToText(dt, 11, strPath1);

            dt = clsData.getProjectList("", "3", "Close", "", dt1.Rows[0]["Function_Name"].ToString());
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Close_" + Session["EmpNo"].ToString() + ".txt";
            dataTableToText(dt, 11, strPath1);

            dt = clsData.getProjectList("", "3", "Hold", "", dt1.Rows[0]["Function_Name"].ToString());
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Hold_" + Session["EmpNo"].ToString() + ".txt";
            dataTableToText(dt, 11, strPath1);
            ///////////////////////

            //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Open_WJ.txt";
            //dt = clsData.getProjectList("", "3", "Open", "吳江", dt1.Rows[0]["Function_Name"].ToString());
            //dataTableToText(dt, 10, strPath1);

            //dt = clsData.getProjectList("", "3", "Close", "吳江", dt1.Rows[0]["Function_Name"].ToString());
            //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Close_WJ.txt";
            //dataTableToText(dt, 10, strPath1);

            //dt = clsData.getProjectList("", "3", "Hold", "吳江", dt1.Rows[0]["Function_Name"].ToString());
            //strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Hold_WJ.txt";
            //dataTableToText(dt, 10, strPath1);
        }

    }

    #region GvQuery
    private void GvQuery(Boolean IsPage)
    {
        //if (IsPage != true)
        //    this.gvwMain.PageIndex = 0;

        //DataTable dt = clsData.ViewFilesQuery(this.txtDateS.Text, this.txtDateE.Text, int.Parse(this.Page.Session["sess_emp_no"].ToString().Trim()), this.ddlDept.SelectedValue);
        //this.gvwMain.DataSource = dt;
        //this.DataBind();
    }
    #endregion

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        DateTime dTime;
        string strDate;

        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    if ((j == 14) || (j == 13))
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
                        strDate = dTime.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                        else
                            strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    }
                    else
                        strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + ",\r\n";
                }
                else
                    strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + "\r\n";
            }
            if (i != dt.Rows.Count - 1)
                strSQLFile += "],\r\n";
            else
                strSQLFile += "]\r\n";
        }
        strSQLFile += "]" + "\r\n" + "}";
        using (StreamWriter sw = new StreamWriter(DBPath))   //小寫TXT     
        {
            sw.Write(strSQLFile);
        }
    }
    #endregion

    
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        string strID1 = "";

        //HttpCookie cookie_Customer = new HttpCookie("Project");
        //cookie_Customer.Values.Add("Customer", "");
        //cookie_Customer.Values.Add("Department", "");
        //cookie_Customer.Values.Add("ID", "");
        //cookie_Customer.Values.Add("Fun", strFun);
        //cookie_Customer.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(cookie_Customer);

        Server.Transfer("~/WebForm/ProjectEdit.aspx?A=1");
    }

    public string strValue1()
    {
        return Session["EmpDepartment"].ToString();
        //return "台北";
    }

    public string strName1()
    {
        return Session["EmpNo"].ToString();
    }
    
}
