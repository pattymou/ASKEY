using System;
using System.Data;
using System.Text;
using System.Web.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MicroSovaComponent.Database;

public partial class WebForm_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if ((clsParameter.strEmpName != null) || (clsParameter.strEmpNo != null) || (clsParameter.strAppNo != null))
            //    getMenuData();
            if ((Session["EmpName"] != null) || (Session["EmpNo"] != null) || (Session["AppNo"] != null))
            {
                getMenuData();
                getMenuData1();
            }
        }
    }

    private void getMenuData()
    {
        string strID, strAuthority;
        string strValue;

        strAuthority = "0";
        mnuTopMenu.Items.Clear();
        //mnuTopMenu1.Items.Clear();

        //strID = this.Page.Session["sess_emp_name"].ToString().Trim();
        //strID = clsParameter.strEmpName;
        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
            strAuthority = "1";

        DataTable dt = clsData.CreateMenuView(strID, strAuthority);

        foreach (DataRow dr in dt.Rows)
        {
            MenuItem MainMenu = new MenuItem();
            if (Convert.ToInt16(dr["Sequence"].ToString()) < 9)
            {
                strValue = dr["Function_Name"].ToString();
                MainMenu.Value = dr["Function_No"].ToString();
                MainMenu.Text = strValue.Trim();
                MainMenu.NavigateUrl = dr["Function_Url"].ToString();


                DataTable dt1 = clsData.CreateChildMenuView("1", dr["Function_No"].ToString(), strID, strAuthority);
                //加入子節點
                foreach (DataRow dr1 in dt1.Rows)
                {
                    MenuItem aNewItem = new MenuItem();
                    aNewItem.Value = dr1["Function_No"].ToString();
                    aNewItem.Text = dr1["Function_Name"].ToString();


                    aNewItem.NavigateUrl = dr1["Function_Url"].ToString();

                    MainMenu.ChildItems.Add(aNewItem);
                }
                mnuTopMenu.Items.Add(MainMenu);
                mnuTopMenu.StaticEnableDefaultPopOutImage = false;
            }

        }

    }
    private void getMenuData1()
    {
        string strID, strAuthority;
        string strValue;

        strAuthority = "0";
        mnuTopMenu1.Items.Clear();
        //strID = this.Page.Session["sess_emp_name"].ToString().Trim();
        //strID = clsParameter.strEmpName;
        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
            strAuthority = "1";

        DataTable dt = clsData.CreateMenuView(strID, strAuthority);

        foreach (DataRow dr in dt.Rows)
        {
            MenuItem MainMenu = new MenuItem();
            if (Convert.ToInt16(dr["Sequence"].ToString()) > 8)
            {
                strValue = dr["Function_Name"].ToString();
                MainMenu.Value = dr["Function_No"].ToString();
                MainMenu.Text = strValue.Trim();
                MainMenu.NavigateUrl = dr["Function_Url"].ToString();


                DataTable dt1 = clsData.CreateChildMenuView("1", dr["Function_No"].ToString(), strID, strAuthority);
                //加入子節點
                foreach (DataRow dr1 in dt1.Rows)
                {
                    MenuItem aNewItem = new MenuItem();
                    aNewItem.Value = dr1["Function_No"].ToString();
                    aNewItem.Text = dr1["Function_Name"].ToString();


                    aNewItem.NavigateUrl = dr1["Function_Url"].ToString();

                    MainMenu.ChildItems.Add(aNewItem);

                }
                mnuTopMenu1.Items.Add(MainMenu);
                mnuTopMenu1.StaticEnableDefaultPopOutImage = false;
            }

        }

    }
    protected void linkLogout_Click(object sender, EventArgs e)
    {
        //clsParameter.strEmpName = null;
        //clsParameter.strPath = null;
        //clsParameter.strCustomer = null;
        //clsParameter.strDepartment = null;
        //clsParameter.strFileName = null;
        //clsParameter.strUpload_Kind = null;
        //clsParameter.strApplicationID = null;
        //clsParameter.strEmpNo = null;
        ////clsParameter.strEmpPosition = null;
        //clsParameter.strLocation_P = null;
        //clsParameter.strAuthority = null;
        //clsParameter.strWrite = null;
        //clsParameter.strApparatusID = null;
        //clsParameter.strAppNo = null;

        Response.Redirect("~/SystemDefault.aspx");
        //Response.Redirect("~/Default.aspx");

    }
    protected void linkHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/BulletinView.aspx");
    }
}
