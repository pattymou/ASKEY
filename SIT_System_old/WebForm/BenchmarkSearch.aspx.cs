using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebForm_BenchmarkSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["EmpNo"] == null)
        //    Response.Redirect("~/Default.aspx");
        //Panel1.Visible = false  ;
        if (!IsPostBack)
        {
            btnOK.Visible = false;

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlTestCaseKind(DDL, strKind1);
    }
    #endregion

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strID="";
        //Response.Write("<script>window.open('DepartmentDailyReport.aspx?Value=0&ID=" + strID + "');</script>");
        for (int ii = 0; ii < this.gvwMain.Rows.Count; ii++)
        {
            if (((CheckBox)gvwMain.Rows[ii].FindControl("CheckBox2")).Checked)
            {
                if (strID == "")
                    strID = ((Label)this.gvwMain.Rows[ii].Cells[3].FindControl("lblGVSeq")).Text;
                else
                    strID = strID + "," + ((Label)this.gvwMain.Rows[ii].Cells[3].FindControl("lblGVSeq")).Text;
            }
        }

        HttpCookie cookie_BenchmarkID = new HttpCookie("BenchmarkID");
        cookie_BenchmarkID.Value = Server.UrlEncode(strID);
        //cookie_BenchmarkID.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_BenchmarkID);

        getChannel();
        Response.Write("<script>window.open('BenchmarkStatistics.aspx');</script>");
    }

    private void getChannel()
    {
        int intI;
        string strA="";
        string strB = "";
        string strG = "";
        string strN22 = "";
        string strN24 = "";
        string strN52 = "";
        string strN54 = "";
        string strAC2 = "";
        string strAC4 = "";
        string strAC8 = "";
        CheckBox[] chkA = new CheckBox[] { this.chkA1, this.chkA2, this.chkA3, this.chkA4, this.chkA5, this.chkA6, this.chkA7, this.chkA8, this.chkA9, this.chkA10, this.chkA11, this.chkA12, this.chkA13, this.chkA14, this.chkA15, this.chkA16, this.chkA17, this.chkA18, this.chkA19, this.chkA20, this.chkA21, this.chkA22, this.chkA23, this.chkA24, this.chkA25 };
        CheckBox[] chkB = new CheckBox[] { this.chkB1, this.chkB2, this.chkB3, this.chkB4, this.chkB5, this.chkB6, this.chkB7, this.chkB8, this.chkB9, this.chkB10, this.chkB11, this.chkB12, this.chkB13 };
        CheckBox[] chkG = new CheckBox[] { this.chkG1, this.chkG2, this.chkG3, this.chkG4, this.chkG5, this.chkG6, this.chkG7, this.chkG8, this.chkG9, this.chkG10, this.chkG11, this.chkG12, this.chkG13 };
        CheckBox[] chkN22 = new CheckBox[] { this.chkN221, this.chkN222, this.chkN223, this.chkN224, this.chkN225, this.chkN226, this.chkN227, this.chkN228, this.chkN229, this.chkN2210, this.chkN2211, this.chkN2212, this.chkN2213 };
        CheckBox[] chkN24 = new CheckBox[] { this.chkN241, this.chkN242, this.chkN243, this.chkN244, this.chkN245, this.chkN246, this.chkN247, this.chkN248 };
        CheckBox[] chkN52 = new CheckBox[] { this.chkN521, this.chkN522, this.chkN523, this.chkN524, this.chkN525, this.chkN526, this.chkN527, this.chkN528, this.chkN529, this.chkN5210, this.chkN5211, this.chkN5212, this.chkN5213, this.chkN5214, this.chkN5215, this.chkN5216, this.chkN5217, this.chkN5218, this.chkN5219, this.chkN5220, this.chkN5221, this.chkN5222, this.chkN5223, this.chkN5224, this.chkN5225 };
        CheckBox[] chkN54 = new CheckBox[] { this.chkN541, this.chkN542, this.chkN543, this.chkN544, this.chkN545, this.chkN546, this.chkN547, this.chkN548, this.chkN549, this.chkN5410, this.chkN5411, this.chkN5412 };
        CheckBox[] chkAC2 = new CheckBox[] { this.chkAC21, this.chkAC22, this.chkAC23, this.chkAC24, this.chkAC25, this.chkAC26, this.chkAC27, this.chkAC28, this.chkAC29, this.chkAC210, this.chkAC211, this.chkAC212, this.chkAC213, this.chkAC214, this.chkAC215, this.chkAC216, this.chkAC217, this.chkAC218, this.chkAC219, this.chkAC220, this.chkAC221, this.chkAC222, this.chkAC223, this.chkAC224, this.chkAC225 };
        CheckBox[] chkAC4 = new CheckBox[] { this.chkAC41, this.chkAC42, this.chkAC43, this.chkAC44, this.chkAC45, this.chkAC46, this.chkAC47, this.chkAC48, this.chkAC49, this.chkAC410, this.chkAC411, this.chkAC412 };
        CheckBox[] chkAC8 = new CheckBox[] { this.chkAC81, this.chkAC82, this.chkAC83, this.chkAC84, this.chkAC85 };


        for (intI = 0; intI < 25; intI++)
        {
            if (chkA[intI].Checked == true)
            {
                if (strA == "")
                    strA = chkA[intI].Text;
                else
                    strA = strA + "," + chkA[intI].Text;
            }
        }

        for (intI = 0; intI < 13; intI++)
        {
            if (chkB[intI].Checked == true)
            {
                if (strB == "")
                    strB = chkB[intI].Text;
                else
                    strB = strB + "," + chkB[intI].Text;
            }
        }

        for (intI = 0; intI < 13; intI++)
        {
            if (chkG[intI].Checked == true)
            {
                if (strG == "")
                    strG = chkG[intI].Text;
                else
                    strG = strG + "," + chkG[intI].Text;
            }
        }

        for (intI = 0; intI < 13; intI++)
        {
            if (chkN22[intI].Checked == true)
            {
                if (strN22 == "")
                    strN22 = chkN22[intI].Text;
                else
                    strN22 = strN22 + "," + chkN22[intI].Text;
            }
        }

        for (intI = 0; intI < 8; intI++)
        {
            if (chkN24[intI].Checked == true)
            {
                if (strN24 == "")
                    strN24 = chkN24[intI].Text;
                else
                    strN24 = strN24 + "," + chkN24[intI].Text;
            }
        }

        for (intI = 0; intI < 25; intI++)
        {
            if (chkN52[intI].Checked == true)
            {
                if (strN52 == "")
                    strN52 = chkN52[intI].Text;
                else
                    strN52 = strN52 + "," + chkN52[intI].Text;
            }
        }


        for (intI = 0; intI < 12; intI++)
        {
            if (chkN54[intI].Checked == true)
            {
                if (strN54 == "")
                    strN54 = chkN54[intI].Text;
                else
                    strN54 = strN54 + "," + chkN54[intI].Text;
            }
        }

        for (intI = 0; intI < 25; intI++)
        {
            if (chkAC2[intI].Checked == true)
            {
                if (strAC2 == "")
                    strAC2 = chkAC2[intI].Text;
                else
                    strAC2 = strAC2 + "," + chkAC2[intI].Text;
            }
        }

        for (intI = 0; intI < 12; intI++)
        {
            if (chkAC4[intI].Checked == true)
            {
                if (strAC4 == "")
                    strAC4 = chkAC4[intI].Text;
                else
                    strAC4 = strAC4 + "," + chkAC4[intI].Text;
            }
        }

        for (intI = 0; intI < 5; intI++)
        {
            if (chkAC8[intI].Checked == true)
            {
                if (strAC8 == "")
                    strAC8 = chkAC8[intI].Text;
                else
                    strAC8 = strAC8 + "," + chkAC8[intI].Text;
            }
        }

        HttpCookie cookie_11A = new HttpCookie("11A");
        cookie_11A.Value = Server.UrlEncode(strA);
        //cookie_11A.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11A);

        HttpCookie cookie_11B = new HttpCookie("11B");
        cookie_11B.Value = Server.UrlEncode(strB);
        //cookie_11B.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11B);

        HttpCookie cookie_11G = new HttpCookie("11G");
        cookie_11G.Value = Server.UrlEncode(strG);
        //cookie_11G.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11G);

        HttpCookie cookie_11N22 = new HttpCookie("11N22");
        cookie_11N22.Value = Server.UrlEncode(strN22);
        //cookie_11N22.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11N22);

        HttpCookie cookie_11N24 = new HttpCookie("11N24");
        cookie_11N24.Value = Server.UrlEncode(strN24);
        //cookie_11N24.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11N24);

        HttpCookie cookie_11N52 = new HttpCookie("11N52");
        cookie_11N52.Value = Server.UrlEncode(strN52);
        //cookie_11N52.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11N52);

        HttpCookie cookie_11N54 = new HttpCookie("11N54");
        cookie_11N54.Value = Server.UrlEncode(strN54);
        //cookie_11N54.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11N54);

        HttpCookie cookie_11AC2 = new HttpCookie("11AC2");
        cookie_11AC2.Value = Server.UrlEncode(strAC2);
        //cookie_11AC2.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11AC2);

        HttpCookie cookie_11AC4 = new HttpCookie("11AC4");
        cookie_11AC4.Value = Server.UrlEncode(strAC4);
        //cookie_11AC4.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11AC4);

        HttpCookie cookie_11AC8 = new HttpCookie("11AC8");
        cookie_11AC8.Value = Server.UrlEncode(strAC8);
        //cookie_11AC8.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_11AC8);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strKind;


        DataTable dt = clsData.UploadLosInfoQuery(ddlBenchmark.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();

        btnOK.Visible = true;
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strKind;

        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //if (ddlKind.Text == "ALL")
        //    strKind = "";
        //else
        //    strKind = ddlKind.Text;
        //DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", strKind);
        //this.gvwMain.DataSource = dt;
        //this.DataBind();
    }
    #endregion
}
