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

public partial class WebForm_Certification_PTCRB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["ApplicationID"] = "2234";
            Session["ApplicationID"] = Request.QueryString["ID"];
            txtRAT_2G.Enabled = false;
            txtRAT_3G.Enabled = false;
            txtRAT_4G.Enabled = false;
            txtRAT_5G.Enabled = false;
            txtCA_4G.Enabled = false;
            txtCA_5G.Enabled = false;
            txtMR.Enabled = false;
            //rdoCertification.Checked = true;
            //rdoSupport.Checked = true;
            //rdoBase.Checked = true;
            //rdoInherits.Checked = true;
            txtModuleNumber.Enabled = false;
            //rdoIMEI.Checked = true;

            //tInherits.Visible = false;
            //tRAT_2G.Visible = false;
            //tRAT_3G.Visible = false;
            //tRAT_4G.Visible = false;
            //tRAT_5G.Visible = false;
            //tCA_4G.Visible = false;
            //tCA_5G.Visible = false;
            //tMR.Visible = false;
            //tIMEI.Visible = false;
            //tIMEI1.Visible = false;

            getData();
        }
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strProjectID = Session["ApplicationID"].ToString();
        string strCertification, strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR, strIMEI;

        strCertification = "";
        strVoLTE = "";
        strCertifiedModule = "";
        strModuleNumber = "";
        strInherits = "";
        strRAT_2G = "";
        strRAT_3G = "";
        strRAT_4G = "";
        strRAT_5G = "";
        strCA_4G = "";
        strCA_5G = "";
        strSIMNumber = "";
        strMR = "";
        strIMEI = "";

        //if (rdoCertification.Checked == true)
        //{
        //    strCertification = "GCF";
        //    strIMEI = "";
        //}
        //if (rdoCertification1.Checked == true)
        //{
        //    strCertification = "PTCRB";
        strIMEI = "";
        if (rdoIMEI.Checked == true)
            strIMEI = "New";

        if (rdoIMEI1.Checked == true)
            strIMEI = "module";
        //}

        if (rdoSupport.Checked == true)
            strVoLTE = "No";

        if (rdoSupport1.Checked == true)
            strVoLTE = "Yes";

        if (rdoBase.Checked == true)
        {
            strCertifiedModule = "No";
            strModuleNumber = "";
            strInherits = "";
            if (chkRAT_2G.Checked == true)
                strRAT_2G = txtRAT_2G.Text;
            else
                strRAT_2G = "";

            if (chkRAT_3G.Checked == true)
                strRAT_3G = txtRAT_3G.Text;
            else
                strRAT_3G = "";

            if (chkRAT_4G.Checked == true)
                strRAT_4G = txtRAT_4G.Text;
            else
                strRAT_4G = "";

            if (chkRAT_5G.Checked == true)
                strRAT_5G = txtRAT_5G.Text;
            else
                strRAT_5G = "";

            strCA_4G = "";
            strCA_5G = "";
            strMR = "";
        }

        if (rdoBase1.Checked == true)
        {
            strCertifiedModule = "Yes";
            strModuleNumber = txtModuleNumber.Text;
            if (rdoInherits.Checked == true)
            {
                strInherits = "No";
                if (chkRAT_2G.Checked == true)
                    strRAT_2G = txtRAT_2G.Text;
                else
                    strRAT_2G = "";

                if (chkRAT_3G.Checked == true)
                    strRAT_3G = txtRAT_3G.Text;
                else
                    strRAT_3G = "";

                if (chkRAT_4G.Checked == true)
                    strRAT_4G = txtRAT_4G.Text;
                else
                    strRAT_4G = "";

                if (chkRAT_5G.Checked == true)
                    strRAT_5G = txtRAT_5G.Text;
                else
                    strRAT_5G = "";

                if (chkCA_4G.Checked == true)
                    strCA_4G = txtCA_4G.Text;
                else
                    strCA_4G = "";

                if (chkCA_5G.Checked == true)
                    strCA_5G = txtCA_5G.Text;
                else
                    strCA_5G = "";

                if (chkMR.Checked == true)
                    strMR = txtMR.Text;
                else
                    strMR = "";
            }

            if (rdoInherits1.Checked == true)
            {
                strInherits = "Yes";
                strRAT_2G = "";
                strRAT_3G = "";
                strRAT_4G = "";
                strRAT_5G = "";
                strCA_4G = "";
                strCA_5G = "";
                strMR = "";
            }

        }



        strSIMNumber = txtSIM.Text;






        DataTable dt1 = clsData.UploadCertification_PTCRB(strProjectID);
        //DataTable dt1 = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
        if (dt1.Rows.Count > 0)
        {
            if (clsTransaction.UpDateCertification_PTCRB(strProjectID, strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR, strIMEI) == true)
                clsMsg.AlertMessage("暫存成功....", this.Page);
            else
                clsMsg.AlertMessage("暫存失敗....", this.Page);
        }
        else
        {
            if (clsTransaction.InsertCertification_PTCRB(strProjectID, strVoLTE, strCertifiedModule, strModuleNumber, strInherits, strRAT_2G, strRAT_3G, strRAT_4G, strRAT_5G, strCA_4G, strCA_5G, strSIMNumber, strMR, strIMEI) == true)
                clsMsg.AlertMessage("暫存成功....", this.Page);
            else
                clsMsg.AlertMessage("暫存失敗....", this.Page);
        }

    }

    private void getData()
    {
        DataTable dt;

        dt = clsData.UploadCertification_PTCRB(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            //if (dt.Rows[0]["Certification"].ToString() == "GCF")
            //{
            //    rdoCertification.Checked = true;
            //    rdoIMEI.Enabled = false;
            //    rdoIMEI1.Enabled = false;
            //}
            //else
            //{
            //    rdoCertification1.Checked = true;
            //    rdoIMEI.Enabled = true;
            //    rdoIMEI1.Enabled = true;
            //    //tIMEI.Visible = true;

            if (dt.Rows[0]["IMEI"].ToString() == "New")
                rdoIMEI.Checked = true;
            else
                rdoIMEI1.Checked = true;
            //}

            if (dt.Rows[0]["VoLTE"].ToString() == "No")
                rdoSupport.Checked = true;
            else
                rdoSupport1.Checked = true;

            if (dt.Rows[0]["CertifiedModule"].ToString() == "No")
            {
                rdoBase.Checked = true;
                //tRAT_2G.Visible = true;
                //tRAT_3G.Visible = true;
                //tRAT_4G.Visible = true;
                //tRAT_5G.Visible = true;
                chkRAT_2G.Enabled = true;
                txtRAT_2G.Enabled = false;
                chkRAT_3G.Enabled = true;
                txtRAT_3G.Enabled = false;
                chkRAT_4G.Enabled = true;
                txtRAT_4G.Enabled = false;
                chkRAT_5G.Enabled = true;
                txtRAT_5G.Enabled = false;
                rdoInherits.Enabled = false;
                rdoInherits1.Enabled = false;
                if (dt.Rows[0]["RAT_2G"].ToString() != "")
                {
                    txtRAT_2G.Text = dt.Rows[0]["RAT_2G"].ToString();
                    chkRAT_2G.Checked = true;
                    txtRAT_2G.Enabled = true;
                }

                if (dt.Rows[0]["RAT_3G"].ToString() != "")
                {
                    txtRAT_3G.Text = dt.Rows[0]["RAT_3G"].ToString();
                    chkRAT_3G.Checked = true;
                    txtRAT_3G.Enabled = true;
                }

                if (dt.Rows[0]["RAT_4G"].ToString() != "")
                {
                    txtRAT_4G.Text = dt.Rows[0]["RAT_4G"].ToString();
                    chkRAT_4G.Checked = true;
                    txtRAT_4G.Enabled = true;
                }

                if (dt.Rows[0]["RAT_5G"].ToString() != "")
                {
                    txtRAT_5G.Text = dt.Rows[0]["RAT_5G"].ToString();
                    chkRAT_5G.Checked = true;
                    txtRAT_5G.Enabled = true;
                }
            }
            else
            {
                rdoBase1.Checked = true;
                txtModuleNumber.Enabled = true;
                txtModuleNumber.Text = dt.Rows[0]["ModuleNumber"].ToString();
                //tInherits.Visible = true;
                rdoInherits.Enabled = true;
                rdoInherits1.Enabled = true;
                if (dt.Rows[0]["Inherits"].ToString() == "No")
                {
                    rdoInherits.Checked = true;
                    //tRAT_2G.Visible = true;
                    //tRAT_3G.Visible = true;
                    //tRAT_4G.Visible = true;
                    //tRAT_5G.Visible = true;
                    //tCA_4G.Visible = true;
                    //tCA_5G.Visible = true;
                    //tMR.Visible = true;
                    chkRAT_2G.Enabled = true;
                    txtRAT_2G.Enabled = false;
                    chkRAT_3G.Enabled = true;
                    txtRAT_3G.Enabled = false;
                    chkRAT_4G.Enabled = true;
                    txtRAT_4G.Enabled = false;
                    chkRAT_5G.Enabled = true;
                    txtRAT_5G.Enabled = false;
                    chkCA_4G.Enabled = true;
                    txtCA_4G.Enabled = false;
                    chkCA_5G.Enabled = true;
                    txtCA_5G.Enabled = false;
                    chkMR.Enabled = true;
                    txtMR.Enabled = false;

                    if (dt.Rows[0]["RAT_2G"].ToString() != "")
                    {
                        txtRAT_2G.Text = dt.Rows[0]["RAT_2G"].ToString();
                        chkRAT_2G.Checked = true;
                        txtRAT_2G.Enabled = true;
                    }

                    if (dt.Rows[0]["RAT_3G"].ToString() != "")
                    {
                        txtRAT_3G.Text = dt.Rows[0]["RAT_3G"].ToString();
                        chkRAT_3G.Checked = true;
                        txtRAT_3G.Enabled = true;
                    }

                    if (dt.Rows[0]["RAT_4G"].ToString() != "")
                    {
                        txtRAT_4G.Text = dt.Rows[0]["RAT_4G"].ToString();
                        chkRAT_4G.Checked = true;
                        txtRAT_4G.Enabled = true;
                    }

                    if (dt.Rows[0]["RAT_5G"].ToString() != "")
                    {
                        txtRAT_5G.Text = dt.Rows[0]["RAT_5G"].ToString();
                        chkRAT_5G.Checked = true;
                        txtRAT_5G.Enabled = true;
                    }
                    if (dt.Rows[0]["CA_4G"].ToString() != "")
                    {
                        txtCA_4G.Text = dt.Rows[0]["CA_4G"].ToString();
                        chkCA_4G.Checked = true;
                        txtCA_4G.Enabled = true;
                    }

                    if (dt.Rows[0]["CA_5G"].ToString() != "")
                    {
                        txtCA_5G.Text = dt.Rows[0]["CA_5G"].ToString();
                        chkCA_5G.Checked = true;
                        txtCA_5G.Enabled = true;
                    }

                    if (dt.Rows[0]["MR"].ToString() != "")
                    {
                        txtMR.Text = dt.Rows[0]["MR"].ToString();
                        chkMR.Checked = true;
                        txtMR.Enabled = true;
                    }
                }
                else
                {
                    rdoInherits1.Checked = true;
                    chkRAT_2G.Enabled = false;
                    txtRAT_2G.Enabled = false;
                    chkRAT_3G.Enabled = false;
                    txtRAT_3G.Enabled = false;
                    chkRAT_4G.Enabled = false;
                    txtRAT_4G.Enabled = false;
                    chkRAT_5G.Enabled = false;
                    txtRAT_5G.Enabled = false;
                    chkCA_4G.Enabled = false;
                    txtCA_4G.Enabled = false;
                    chkCA_5G.Enabled = false;
                    txtCA_5G.Enabled = false;
                    chkMR.Enabled = false;
                    txtMR.Enabled = false;
                }
            }

            txtSIM.Text = dt.Rows[0]["SIMNumber"].ToString();




        }
    }

    protected void chkRAT_2G_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRAT_2G.Checked == true)
            txtRAT_2G.Enabled = true;
        else
            txtRAT_2G.Enabled = false;
    }

    protected void chkRAT_3G_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRAT_3G.Checked == true)
            txtRAT_3G.Enabled = true;
        else
            txtRAT_3G.Enabled = false;
    }

    protected void chkRAT_4G_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRAT_4G.Checked == true)
            txtRAT_4G.Enabled = true;
        else
            txtRAT_4G.Enabled = false;
    }

    protected void chkRAT_5G_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRAT_5G.Checked == true)
            txtRAT_5G.Enabled = true;
        else
            txtRAT_5G.Enabled = false;
    }

    protected void chkCA_4G_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCA_4G.Checked == true)
            txtCA_4G.Enabled = true;
        else
            txtCA_4G.Enabled = false;
    }

    protected void chkCA_5G_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCA_5G.Checked == true)
            txtCA_5G.Enabled = true;
        else
            txtCA_5G.Enabled = false;
    }

    protected void rdoBase1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoBase1.Checked == true)
        {
            txtModuleNumber.Enabled = true;
            //tInherits.Visible = true;
            //tRAT_2G.Visible = false;
            //tRAT_3G.Visible = false;
            //tRAT_4G.Visible = false;
            //tRAT_5G.Visible = false;
            //tCA_4G.Visible = false;
            //tCA_5G.Visible = false;
            //tMR.Visible = false;
            rdoInherits.Enabled = true;
            rdoInherits1.Enabled = true;
            chkRAT_2G.Enabled = false;
            txtRAT_2G.Enabled = false;
            chkRAT_3G.Enabled = false;
            txtRAT_3G.Enabled = false;
            chkRAT_4G.Enabled = false;
            txtRAT_4G.Enabled = false;
            chkRAT_5G.Enabled = false;
            txtRAT_5G.Enabled = false;
            chkCA_4G.Enabled = false;
            txtCA_4G.Enabled = false;
            chkCA_5G.Enabled = false;
            txtCA_5G.Enabled = false;
            chkMR.Enabled = false;
            txtMR.Enabled = false;
        }

    }
    protected void rdoBase_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoBase.Checked == true)
        {
            txtModuleNumber.Enabled = false;
            txtModuleNumber.Text = "";
            //tInherits.Visible = false;
            //tRAT_2G.Visible = true;
            //tRAT_3G.Visible = true;
            //tRAT_4G.Visible = true;
            //tRAT_5G.Visible = true;
            //tCA_4G.Visible = false;
            //tCA_5G.Visible = false;
            //tMR.Visible = false;

            rdoInherits.Enabled = false;
            rdoInherits1.Enabled = false;
            chkRAT_2G.Enabled = true;
            txtRAT_2G.Enabled = false;
            chkRAT_3G.Enabled = true;
            txtRAT_3G.Enabled = false;
            chkRAT_4G.Enabled = true;
            txtRAT_4G.Enabled = false;
            chkRAT_5G.Enabled = true;
            txtRAT_5G.Enabled = false;
            chkCA_4G.Enabled = true;
            txtCA_4G.Enabled = false;
            chkCA_5G.Enabled = true;
            txtCA_5G.Enabled = false;
            chkMR.Enabled = true;
            txtMR.Enabled = false;
            rdoInherits.Checked = false;
            rdoInherits1.Checked = false;
        }
    }
    protected void rdoInherits_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoInherits.Checked == true)
        {
            chkRAT_2G.Enabled = true;
            txtRAT_2G.Enabled = false;
            chkRAT_3G.Enabled = true;
            txtRAT_3G.Enabled = false;
            chkRAT_4G.Enabled = true;
            txtRAT_4G.Enabled = false;
            chkRAT_5G.Enabled = true;
            txtRAT_5G.Enabled = false;
            chkCA_4G.Enabled = true;
            txtCA_4G.Enabled = false;
            chkCA_5G.Enabled = true;
            txtCA_5G.Enabled = false;
            chkMR.Enabled = true;
            txtMR.Enabled = false;
            //tRAT_2G.Visible = true;
            //tRAT_3G.Visible = true;
            //tRAT_4G.Visible = true;
            //tRAT_5G.Visible = true;
            //tCA_4G.Visible = true;
            //tCA_5G.Visible = true;
            //tMR.Visible = true;
            //if (rdoCertification1.Checked == true)
            //{
            //    tIMEI.Visible = true;
            //    rdoIMEI.Checked = true;
            //    rdoIMEI1.Visible = false;
            //}
        }
    }

    protected void rdoInherits1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoInherits1.Checked == true)
        {
            chkRAT_2G.Enabled = false;
            txtRAT_2G.Enabled = false;
            chkRAT_3G.Enabled = false;
            txtRAT_3G.Enabled = false;
            chkRAT_4G.Enabled = false;
            txtRAT_4G.Enabled = false;
            chkRAT_5G.Enabled = false;
            txtRAT_5G.Enabled = false;
            chkCA_4G.Enabled = false;
            txtCA_4G.Enabled = false;
            chkCA_5G.Enabled = false;
            txtCA_5G.Enabled = false;
            chkMR.Enabled = false;
            txtMR.Enabled = false;

            chkRAT_2G.Checked = false;
            txtRAT_2G.Text = "";
            chkRAT_3G.Checked = false;
            txtRAT_3G.Text = "";
            chkRAT_4G.Checked = false;
            txtRAT_4G.Text = "";
            chkRAT_5G.Checked = false;
            txtRAT_5G.Text = "";
            chkCA_4G.Checked = false;
            txtCA_4G.Text = "";
            chkCA_5G.Checked = false;
            txtCA_5G.Text = "";
            chkMR.Checked = false;
            txtMR.Text = "";

            //tRAT_2G.Visible = false;
            //tRAT_3G.Visible = false;
            //tRAT_4G.Visible = false;
            //tRAT_5G.Visible = false;
            //tCA_4G.Visible = false;
            //tCA_5G.Visible = false;
            //tMR.Visible = false;
            //if (rdoCertification1.Checked == true)
            //{
            //    tIMEI.Visible = true;
            //    rdoIMEI1.Visible = true;
            //}
        }
    }

    protected void chkMR_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMR.Checked == true)
            txtMR.Enabled = true;
        else
            txtMR.Enabled = false;
    }
}
