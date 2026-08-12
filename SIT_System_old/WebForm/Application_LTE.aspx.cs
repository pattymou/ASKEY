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

public partial class WebForm_Application_LTE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["ApplicationID"] = "2234";
            Session["ApplicationID"] = Request.QueryString["ID"];

            set1(false);
            set2(false);
            set3(false);
            set4(false);
            set5(false);
            set6(false);
            set7(false);
            set8(false);
            set9(false);
            set10(false);

            set11(false);
            set12(false);
            set13(false);
            set14(false);
            set17(false);
            set18(false);
            set19(false);
            set20(false);

            set21(false);
            set22(false);
            set23(false);
            set24(false);
            set25(false);
            set26(false);
            set27(false);
            set28(false);
            set30(false);

            set31(false);
            set33(false);
            set34(false);
            set35(false);
            set36(false);
            set37(false);
            set38(false);
            set39(false);
            set40(false);

            set41(false);
            set42(false);
            set43(false);
            set44(false);
            set45(false);
            set46(false);
            set47(false);
            set48(false);
            set50(false);
            set51(false);

            set65(false);
            set66(false);
            set68(false);
            set70(false);

            set71(false);
            set72(false);
            set74(false);

            getData();
        }
    }

    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        if (chk1.Checked == true)
            set1(true);
        else
        {
            set1(false);
        }
    }

    protected void chk2_CheckedChanged(object sender, EventArgs e)
    {
        if (chk2.Checked == true)
            set2(true);
        else
            set2(false);
    }

    protected void chk3_CheckedChanged(object sender, EventArgs e)
    {
        if (chk3.Checked == true)
            set3(true);
        else
            set3(false);
    }

    protected void chk4_CheckedChanged(object sender, EventArgs e)
    {
        if (chk4.Checked == true)
            set4(true);
        else
            set4(false);
    }

    protected void chk5_CheckedChanged(object sender, EventArgs e)
    {
        if (chk5.Checked == true)
            set5(true);
        else
            set5(false);
    }

    protected void chk6_CheckedChanged(object sender, EventArgs e)
    {
        if (chk6.Checked == true)
            set6(true);
        else
            set6(false);
    }

    protected void chk7_CheckedChanged(object sender, EventArgs e)
    {
        if (chk7.Checked == true)
            set7(true);
        else
            set7(false);
    }

    protected void chk8_CheckedChanged(object sender, EventArgs e)
    {
        if (chk8.Checked == true)
            set8(true);
        else
            set8(false);
    }

    protected void chk9_CheckedChanged(object sender, EventArgs e)
    {
        if (chk9.Checked == true)
            set9(true);
        else
            set9(false);
    }

    protected void chk10_CheckedChanged(object sender, EventArgs e)
    {
        if (chk10.Checked == true)
            set10(true);
        else
            set10(false);
    }

    protected void chk11_CheckedChanged(object sender, EventArgs e)
    {
        if (chk11.Checked == true)
            set11(true);
        else
            set11(false);
    }

    protected void chk12_CheckedChanged(object sender, EventArgs e)
    {
        if (chk12.Checked == true)
            set12(true);
        else
            set12(false);
    }

    protected void chk13_CheckedChanged(object sender, EventArgs e)
    {
        if (chk13.Checked == true)
            set13(true);
        else
            set13(false);
    }

    protected void chk14_CheckedChanged(object sender, EventArgs e)
    {
        if (chk14.Checked == true)
            set14(true);
        else
            set14(false);
    }

    protected void chk17_CheckedChanged(object sender, EventArgs e)
    {
        if (chk17.Checked == true)
            set17(true);
        else
            set17(false);
    }

    protected void chk18_CheckedChanged(object sender, EventArgs e)
    {
        if (chk18.Checked == true)
            set18(true);
        else
            set18(false);
    }

    protected void chk19_CheckedChanged(object sender, EventArgs e)
    {
        if (chk19.Checked == true)
            set19(true);
        else
            set19(false);
    }

    protected void chk20_CheckedChanged(object sender, EventArgs e)
    {
        if (chk20.Checked == true)
            set20(true);
        else
            set20(false);
    }

    protected void chk21_CheckedChanged(object sender, EventArgs e)
    {
        if (chk21.Checked == true)
            set21(true);
        else
            set21(false);
    }

    protected void chk22_CheckedChanged(object sender, EventArgs e)
    {
        if (chk22.Checked == true)
            set22(true);
        else
            set22(false);
    }

    protected void chk23_CheckedChanged(object sender, EventArgs e)
    {
        if (chk23.Checked == true)
            set23(true);
        else
            set23(false);
    }

    protected void chk24_CheckedChanged(object sender, EventArgs e)
    {
        if (chk24.Checked == true)
            set24(true);
        else
            set24(false);
    }

    protected void chk25_CheckedChanged(object sender, EventArgs e)
    {
        if (chk25.Checked == true)
            set25(true);
        else
            set25(false);
    }

    protected void chk26_CheckedChanged(object sender, EventArgs e)
    {
        if (chk26.Checked == true)
            set26(true);
        else
            set26(false);
    }

    protected void chk27_CheckedChanged(object sender, EventArgs e)
    {
        if (chk27.Checked == true)
            set27(true);
        else
            set27(false);
    }

    protected void chk28_CheckedChanged(object sender, EventArgs e)
    {
        if (chk28.Checked == true)
            set28(true);
        else
            set28(false);
    }

    protected void chk30_CheckedChanged(object sender, EventArgs e)
    {
        if (chk30.Checked == true)
            set30(true);
        else
            set30(false);
    }

    protected void chk31_CheckedChanged(object sender, EventArgs e)
    {
        if (chk31.Checked == true)
            set31(true);
        else
            set31(false);
    }

    protected void chk33_CheckedChanged(object sender, EventArgs e)
    {
        if (chk33.Checked == true)
            set33(true);
        else
            set33(false);
    }

    protected void chk34_CheckedChanged(object sender, EventArgs e)
    {
        if (chk34.Checked == true)
            set34(true);
        else
            set34(false);
    }

    protected void chk35_CheckedChanged(object sender, EventArgs e)
    {
        if (chk35.Checked == true)
            set35(true);
        else
            set35(false);
    }

    protected void chk36_CheckedChanged(object sender, EventArgs e)
    {
        if (chk36.Checked == true)
            set36(true);
        else
            set36(false);
    }

    protected void chk37_CheckedChanged(object sender, EventArgs e)
    {
        if (chk37.Checked == true)
            set37(true);
        else
            set37(false);
    }

    protected void chk38_CheckedChanged(object sender, EventArgs e)
    {
        if (chk38.Checked == true)
            set38(true);
        else
            set38(false);
    }

    protected void chk39_CheckedChanged(object sender, EventArgs e)
    {
        if (chk39.Checked == true)
            set39(true);
        else
            set39(false);
    }

    protected void chk40_CheckedChanged(object sender, EventArgs e)
    {
        if (chk40.Checked == true)
            set40(true);
        else
            set40(false);
    }

    protected void chk41_CheckedChanged(object sender, EventArgs e)
    {
        if (chk41.Checked == true)
            set41(true);
        else
            set41(false);
    }

    protected void chk42_CheckedChanged(object sender, EventArgs e)
    {
        if (chk42.Checked == true)
            set42(true);
        else
            set42(false);
    }

    protected void chk43_CheckedChanged(object sender, EventArgs e)
    {
        if (chk43.Checked == true)
            set43(true);
        else
            set43(false);
    }

    protected void chk44_CheckedChanged(object sender, EventArgs e)
    {
        if (chk44.Checked == true)
            set44(true);
        else
            set44(false);
    }

    protected void chk45_CheckedChanged(object sender, EventArgs e)
    {
        if (chk45.Checked == true)
            set45(true);
        else
            set45(false);
    }

    protected void chk46_CheckedChanged(object sender, EventArgs e)
    {
        if (chk46.Checked == true)
            set46(true);
        else
            set46(false);
    }

    protected void chk47_CheckedChanged(object sender, EventArgs e)
    {
        if (chk47.Checked == true)
            set47(true);
        else
            set47(false);
    }

    protected void chk48_CheckedChanged(object sender, EventArgs e)
    {
        if (chk48.Checked == true)
            set48(true);
        else
            set48(false);
    }

    protected void chk50_CheckedChanged(object sender, EventArgs e)
    {
        if (chk50.Checked == true)
            set50(true);
        else
            set50(false);
    }

    protected void chk51_CheckedChanged(object sender, EventArgs e)
    {
        if (chk51.Checked == true)
            set51(true);
        else
            set51(false);
    }
    protected void chk65_CheckedChanged(object sender, EventArgs e)
    {
        if (chk65.Checked == true)
            set65(true);
        else
            set65(false);
    }

    protected void chk66_CheckedChanged(object sender, EventArgs e)
    {
        if (chk66.Checked == true)
            set66(true);
        else
            set66(false);
    }

    protected void chk68_CheckedChanged(object sender, EventArgs e)
    {
        if (chk68.Checked == true)
            set68(true);
        else
            set68(false);
    }

    protected void chk70_CheckedChanged(object sender, EventArgs e)
    {
        if (chk70.Checked == true)
            set70(true);
        else
            set70(false);
    }

    protected void chk71_CheckedChanged(object sender, EventArgs e)
    {
        if (chk71.Checked == true)
            set71(true);
        else
            set71(false);
    }

    protected void chk72_CheckedChanged(object sender, EventArgs e)
    {
        if (chk72.Checked == true)
            set72(true);
        else
            set72(false);
    }

    protected void chk74_CheckedChanged(object sender, EventArgs e)
    {
        if (chk74.Checked == true)
            set74(true);
        else
            set74(false);
    }

    private void set1(bool bTF)
    {
        txtTRP_1_1.Enabled = bTF;
        txtTRP_1_2.Enabled = bTF;
        txtTRP_1_3.Enabled = bTF;
        txtTIS_1_1.Enabled = bTF;
        txtTIS_1_2.Enabled = bTF;
        txtTIS_1_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_1_1.Text = "";
            txtTRP_1_2.Text = "";
            txtTRP_1_3.Text = "";
            txtTIS_1_1.Text = "";
            txtTIS_1_2.Text = "";
            txtTIS_1_3.Text = "";
        }
    }

    private void set2(bool bTF)
    {
        txtTRP_2_1.Enabled = bTF;
        txtTRP_2_2.Enabled = bTF;
        txtTRP_2_3.Enabled = bTF;
        txtTIS_2_1.Enabled = bTF;
        txtTIS_2_2.Enabled = bTF;
        txtTIS_2_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_2_1.Text = "";
            txtTRP_2_2.Text = "";
            txtTRP_2_3.Text = "";
            txtTIS_2_1.Text = "";
            txtTIS_2_2.Text = "";
            txtTIS_2_3.Text = "";
        }
    }

    private void set3(bool bTF)
    {
        txtTRP_3_1.Enabled = bTF;
        txtTRP_3_2.Enabled = bTF;
        txtTRP_3_3.Enabled = bTF;
        txtTIS_3_1.Enabled = bTF;
        txtTIS_3_2.Enabled = bTF;
        txtTIS_3_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_3_1.Text = "";
            txtTRP_3_2.Text = "";
            txtTRP_3_3.Text = "";
            txtTIS_3_1.Text = "";
            txtTIS_3_2.Text = "";
            txtTIS_3_3.Text = "";
        }
    }

    private void set4(bool bTF)
    {
        txtTRP_4_1.Enabled = bTF;
        txtTRP_4_2.Enabled = bTF;
        txtTRP_4_3.Enabled = bTF;
        txtTIS_4_1.Enabled = bTF;
        txtTIS_4_2.Enabled = bTF;
        txtTIS_4_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_4_1.Text = "";
            txtTRP_4_2.Text = "";
            txtTRP_4_3.Text = "";
            txtTIS_4_1.Text = "";
            txtTIS_4_2.Text = "";
            txtTIS_4_3.Text = "";
        }
    }

    private void set5(bool bTF)
    {
        txtTRP_5_1.Enabled = bTF;
        txtTRP_5_2.Enabled = bTF;
        txtTRP_5_3.Enabled = bTF;
        txtTIS_5_1.Enabled = bTF;
        txtTIS_5_2.Enabled = bTF;
        txtTIS_5_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_5_1.Text = "";
            txtTRP_5_2.Text = "";
            txtTRP_5_3.Text = "";
            txtTIS_5_1.Text = "";
            txtTIS_5_2.Text = "";
            txtTIS_5_3.Text = "";
        }
    }

    private void set6(bool bTF)
    {
        txtTRP_6_1.Enabled = bTF;
        txtTIS_6_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_6_1.Text = "";
            txtTIS_6_1.Text = "";
        }
    }

    private void set7(bool bTF)
    {
        txtTRP_7_1.Enabled = bTF;
        txtTRP_7_2.Enabled = bTF;
        txtTRP_7_3.Enabled = bTF;
        txtTIS_7_1.Enabled = bTF;
        txtTIS_7_2.Enabled = bTF;
        txtTIS_7_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_7_1.Text = "";
            txtTRP_7_2.Text = "";
            txtTRP_7_3.Text = "";
            txtTIS_7_1.Text = "";
            txtTIS_7_2.Text = "";
            txtTIS_7_3.Text = "";
        }
    }

    private void set8(bool bTF)
    {
        txtTRP_8_1.Enabled = bTF;
        txtTRP_8_2.Enabled = bTF;
        txtTRP_8_3.Enabled = bTF;
        txtTIS_8_1.Enabled = bTF;
        txtTIS_8_2.Enabled = bTF;
        txtTIS_8_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_8_1.Text = "";
            txtTRP_8_2.Text = "";
            txtTRP_8_3.Text = "";
            txtTIS_8_1.Text = "";
            txtTIS_8_2.Text = "";
            txtTIS_8_3.Text = "";
        }
    }

    private void set9(bool bTF)
    {
        txtTRP_9_1.Enabled = bTF;
        txtTRP_9_2.Enabled = bTF;
        txtTRP_9_3.Enabled = bTF;
        txtTIS_9_1.Enabled = bTF;
        txtTIS_9_2.Enabled = bTF;
        txtTIS_9_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_9_1.Text = "";
            txtTRP_9_2.Text = "";
            txtTRP_9_3.Text = "";
            txtTIS_9_1.Text = "";
            txtTIS_9_2.Text = "";
            txtTIS_9_3.Text = "";
        }
    }

    private void set10(bool bTF)
    {
        txtTRP_10_1.Enabled = bTF;
        txtTRP_10_2.Enabled = bTF;
        txtTRP_10_3.Enabled = bTF;
        txtTIS_10_1.Enabled = bTF;
        txtTIS_10_2.Enabled = bTF;
        txtTIS_10_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_10_1.Text = "";
            txtTRP_10_2.Text = "";
            txtTRP_10_3.Text = "";
            txtTIS_10_1.Text = "";
            txtTIS_10_2.Text = "";
            txtTIS_10_3.Text = "";
        }
    }

    private void set11(bool bTF)
    {
        txtTRP_11_1.Enabled = bTF;
        txtTRP_11_2.Enabled = bTF;
        txtTRP_11_3.Enabled = bTF;
        txtTIS_11_1.Enabled = bTF;
        txtTIS_11_2.Enabled = bTF;
        txtTIS_11_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_11_1.Text = "";
            txtTRP_11_2.Text = "";
            txtTRP_11_3.Text = "";
            txtTIS_11_1.Text = "";
            txtTIS_11_2.Text = "";
            txtTIS_11_3.Text = "";
        }
    }

    private void set12(bool bTF)
    {
        txtTRP_12_1.Enabled = bTF;
        txtTRP_12_2.Enabled = bTF;
        txtTRP_12_3.Enabled = bTF;
        txtTIS_12_1.Enabled = bTF;
        txtTIS_12_2.Enabled = bTF;
        txtTIS_12_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_12_1.Text = "";
            txtTRP_12_2.Text = "";
            txtTRP_12_3.Text = "";
            txtTIS_12_1.Text = "";
            txtTIS_12_2.Text = "";
            txtTIS_12_3.Text = "";
        }
    }

    private void set13(bool bTF)
    {
        txtTRP_13_1.Enabled = bTF;
        txtTIS_13_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_13_1.Text = "";
            txtTIS_13_1.Text = "";
        }
    }

    private void set14(bool bTF)
    {
        txtTRP_14_1.Enabled = bTF;
        txtTIS_14_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_14_1.Text = "";
            txtTIS_14_1.Text = "";
        }
    }

    private void set17(bool bTF)
    {
        txtTRP_17_1.Enabled = bTF;
        txtTRP_17_2.Enabled = bTF;
        txtTRP_17_3.Enabled = bTF;
        txtTIS_17_1.Enabled = bTF;
        txtTIS_17_2.Enabled = bTF;
        txtTIS_17_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_17_1.Text = "";
            txtTRP_17_2.Text = "";
            txtTRP_17_3.Text = "";
            txtTIS_17_1.Text = "";
            txtTIS_17_2.Text = "";
            txtTIS_17_3.Text = "";
        }
    }

    private void set18(bool bTF)
    {
        txtTRP_18_1.Enabled = bTF;
        txtTRP_18_2.Enabled = bTF;
        txtTRP_18_3.Enabled = bTF;
        txtTIS_18_1.Enabled = bTF;
        txtTIS_18_2.Enabled = bTF;
        txtTIS_18_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_18_1.Text = "";
            txtTRP_18_2.Text = "";
            txtTRP_18_3.Text = "";
            txtTIS_18_1.Text = "";
            txtTIS_18_2.Text = "";
            txtTIS_18_3.Text = "";
        }
    }

    private void set19(bool bTF)
    {
        txtTRP_19_1.Enabled = bTF;
        txtTRP_19_2.Enabled = bTF;
        txtTRP_19_3.Enabled = bTF;
        txtTIS_19_1.Enabled = bTF;
        txtTIS_19_2.Enabled = bTF;
        txtTIS_19_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_19_1.Text = "";
            txtTRP_19_2.Text = "";
            txtTRP_19_3.Text = "";
            txtTIS_19_1.Text = "";
            txtTIS_19_2.Text = "";
            txtTIS_19_3.Text = "";
        }
    }

    private void set20(bool bTF)
    {
        txtTRP_20_1.Enabled = bTF;
        txtTRP_20_2.Enabled = bTF;
        txtTRP_20_3.Enabled = bTF;
        txtTIS_20_1.Enabled = bTF;
        txtTIS_20_2.Enabled = bTF;
        txtTIS_20_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_20_1.Text = "";
            txtTRP_20_2.Text = "";
            txtTRP_20_3.Text = "";
            txtTIS_20_1.Text = "";
            txtTIS_20_2.Text = "";
            txtTIS_20_3.Text = "";
        }
    }

    private void set21(bool bTF)
    {
        txtTRP_21_1.Enabled = bTF;
        txtTRP_21_2.Enabled = bTF;
        txtTRP_21_3.Enabled = bTF;
        txtTIS_21_1.Enabled = bTF;
        txtTIS_21_2.Enabled = bTF;
        txtTIS_21_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_21_1.Text = "";
            txtTRP_21_2.Text = "";
            txtTRP_21_3.Text = "";
            txtTIS_21_1.Text = "";
            txtTIS_21_2.Text = "";
            txtTIS_21_3.Text = "";
        }
    }

    private void set22(bool bTF)
    {
        txtTRP_22_1.Enabled = bTF;
        txtTRP_22_2.Enabled = bTF;
        txtTRP_22_3.Enabled = bTF;
        txtTIS_22_1.Enabled = bTF;
        txtTIS_22_2.Enabled = bTF;
        txtTIS_22_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_22_1.Text = "";
            txtTRP_22_2.Text = "";
            txtTRP_22_3.Text = "";
            txtTIS_22_1.Text = "";
            txtTIS_22_2.Text = "";
            txtTIS_22_3.Text = "";
        }
    }

    private void set23(bool bTF)
    {
        txtTRP_23_1.Enabled = bTF;
        txtTRP_23_2.Enabled = bTF;
        txtTRP_23_3.Enabled = bTF;
        txtTIS_23_1.Enabled = bTF;
        txtTIS_23_2.Enabled = bTF;
        txtTIS_23_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_23_1.Text = "";
            txtTRP_23_2.Text = "";
            txtTRP_23_3.Text = "";
            txtTIS_23_1.Text = "";
            txtTIS_23_2.Text = "";
            txtTIS_23_3.Text = "";
        }
    }

    private void set24(bool bTF)
    {
        txtTRP_24_1.Enabled = bTF;
        txtTRP_24_2.Enabled = bTF;
        txtTRP_24_3.Enabled = bTF;
        txtTIS_24_1.Enabled = bTF;
        txtTIS_24_2.Enabled = bTF;
        txtTIS_24_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_24_1.Text = "";
            txtTRP_24_2.Text = "";
            txtTRP_24_3.Text = "";
            txtTIS_24_1.Text = "";
            txtTIS_24_2.Text = "";
            txtTIS_24_3.Text = "";
        }
    }

    private void set25(bool bTF)
    {
        txtTRP_25_1.Enabled = bTF;
        txtTRP_25_2.Enabled = bTF;
        txtTRP_25_3.Enabled = bTF;
        txtTIS_25_1.Enabled = bTF;
        txtTIS_25_2.Enabled = bTF;
        txtTIS_25_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_25_1.Text = "";
            txtTRP_25_2.Text = "";
            txtTRP_25_3.Text = "";
            txtTIS_25_1.Text = "";
            txtTIS_25_2.Text = "";
            txtTIS_25_3.Text = "";
        }
    }

    private void set26(bool bTF)
    {
        txtTRP_26_1.Enabled = bTF;
        txtTRP_26_2.Enabled = bTF;
        txtTRP_26_3.Enabled = bTF;
        txtTIS_26_1.Enabled = bTF;
        txtTIS_26_2.Enabled = bTF;
        txtTIS_26_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_26_1.Text = "";
            txtTRP_26_2.Text = "";
            txtTRP_26_3.Text = "";
            txtTIS_26_1.Text = "";
            txtTIS_26_2.Text = "";
            txtTIS_26_3.Text = "";
        }
    }

    private void set27(bool bTF)
    {
        txtTRP_27_1.Enabled = bTF;
        txtTRP_27_2.Enabled = bTF;
        txtTRP_27_3.Enabled = bTF;
        txtTIS_27_1.Enabled = bTF;
        txtTIS_27_2.Enabled = bTF;
        txtTIS_27_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_27_1.Text = "";
            txtTRP_27_2.Text = "";
            txtTRP_27_3.Text = "";
            txtTIS_27_1.Text = "";
            txtTIS_27_2.Text = "";
            txtTIS_27_3.Text = "";
        }
    }

    private void set28(bool bTF)
    {
        txtTRP_28_1.Enabled = bTF;
        txtTRP_28_2.Enabled = bTF;
        txtTRP_28_3.Enabled = bTF;
        txtTIS_28_1.Enabled = bTF;
        txtTIS_28_2.Enabled = bTF;
        txtTIS_28_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_28_1.Text = "";
            txtTRP_28_2.Text = "";
            txtTRP_28_3.Text = "";
            txtTIS_28_1.Text = "";
            txtTIS_28_2.Text = "";
            txtTIS_28_3.Text = "";
        }
    }

    private void set30(bool bTF)
    {
        txtTRP_30_1.Enabled = bTF;
        txtTIS_30_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_30_1.Text = "";
            txtTIS_30_1.Text = "";
        }
    }

    private void set31(bool bTF)
    {
        txtTRP_31_1.Enabled = bTF;
        txtTIS_31_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_31_1.Text = "";
            txtTIS_31_1.Text = "";
        }
    }

    private void set33(bool bTF)
    {
        txtTRP_33_1.Enabled = bTF;
        txtTRP_33_2.Enabled = bTF;
        txtTRP_33_3.Enabled = bTF;
        txtTIS_33_1.Enabled = bTF;
        txtTIS_33_2.Enabled = bTF;
        txtTIS_33_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_33_1.Text = "";
            txtTRP_33_2.Text = "";
            txtTRP_33_3.Text = "";
            txtTIS_33_1.Text = "";
            txtTIS_33_2.Text = "";
            txtTIS_33_3.Text = "";
        }
    }

    private void set34(bool bTF)
    {
        txtTRP_34_1.Enabled = bTF;
        txtTRP_34_2.Enabled = bTF;
        txtTRP_34_3.Enabled = bTF;
        txtTIS_34_1.Enabled = bTF;
        txtTIS_34_2.Enabled = bTF;
        txtTIS_34_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_34_1.Text = "";
            txtTRP_34_2.Text = "";
            txtTRP_34_3.Text = "";
            txtTIS_34_1.Text = "";
            txtTIS_34_2.Text = "";
            txtTIS_34_3.Text = "";
        }
    }

    private void set35(bool bTF)
    {
        txtTRP_35_1.Enabled = bTF;
        txtTRP_35_2.Enabled = bTF;
        txtTRP_35_3.Enabled = bTF;
        txtTIS_35_1.Enabled = bTF;
        txtTIS_35_2.Enabled = bTF;
        txtTIS_35_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_35_1.Text = "";
            txtTRP_35_2.Text = "";
            txtTRP_35_3.Text = "";
            txtTIS_35_1.Text = "";
            txtTIS_35_2.Text = "";
            txtTIS_35_3.Text = "";
        }
    }

    private void set36(bool bTF)
    {
        txtTRP_36_1.Enabled = bTF;
        txtTRP_36_2.Enabled = bTF;
        txtTRP_36_3.Enabled = bTF;
        txtTIS_36_1.Enabled = bTF;
        txtTIS_36_2.Enabled = bTF;
        txtTIS_36_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_36_1.Text = "";
            txtTRP_36_2.Text = "";
            txtTRP_36_3.Text = "";
            txtTIS_36_1.Text = "";
            txtTIS_36_2.Text = "";
            txtTIS_36_3.Text = "";
        }
    }

    private void set37(bool bTF)
    {
        txtTRP_37_1.Enabled = bTF;
        txtTRP_37_2.Enabled = bTF;
        txtTRP_37_3.Enabled = bTF;
        txtTIS_37_1.Enabled = bTF;
        txtTIS_37_2.Enabled = bTF;
        txtTIS_37_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_37_1.Text = "";
            txtTRP_37_2.Text = "";
            txtTRP_37_3.Text = "";
            txtTIS_37_1.Text = "";
            txtTIS_37_2.Text = "";
            txtTIS_37_3.Text = "";
        }
    }

    private void set38(bool bTF)
    {
        txtTRP_38_1.Enabled = bTF;
        txtTRP_38_2.Enabled = bTF;
        txtTRP_38_3.Enabled = bTF;
        txtTIS_38_1.Enabled = bTF;
        txtTIS_38_2.Enabled = bTF;
        txtTIS_38_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_38_1.Text = "";
            txtTRP_38_2.Text = "";
            txtTRP_38_3.Text = "";
            txtTIS_38_1.Text = "";
            txtTIS_38_2.Text = "";
            txtTIS_38_3.Text = "";
        }
    }

    private void set39(bool bTF)
    {
        txtTRP_39_1.Enabled = bTF;
        txtTRP_39_2.Enabled = bTF;
        txtTRP_39_3.Enabled = bTF;
        txtTIS_39_1.Enabled = bTF;
        txtTIS_39_2.Enabled = bTF;
        txtTIS_39_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_39_1.Text = "";
            txtTRP_39_2.Text = "";
            txtTRP_39_3.Text = "";
            txtTIS_39_1.Text = "";
            txtTIS_39_2.Text = "";
            txtTIS_39_3.Text = "";
        }
    }

    private void set40(bool bTF)
    {
        txtTRP_40_1.Enabled = bTF;
        txtTRP_40_2.Enabled = bTF;
        txtTRP_40_3.Enabled = bTF;
        txtTIS_40_1.Enabled = bTF;
        txtTIS_40_2.Enabled = bTF;
        txtTIS_40_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_40_1.Text = "";
            txtTRP_40_2.Text = "";
            txtTRP_40_3.Text = "";
            txtTIS_40_1.Text = "";
            txtTIS_40_2.Text = "";
            txtTIS_40_3.Text = "";
        }
    }

    private void set41(bool bTF)
    {
        txtTRP_41_1.Enabled = bTF;
        txtTRP_41_2.Enabled = bTF;
        txtTRP_41_3.Enabled = bTF;
        txtTIS_41_1.Enabled = bTF;
        txtTIS_41_2.Enabled = bTF;
        txtTIS_41_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_41_1.Text = "";
            txtTRP_41_2.Text = "";
            txtTRP_41_3.Text = "";
            txtTIS_41_1.Text = "";
            txtTIS_41_2.Text = "";
            txtTIS_41_3.Text = "";
        }
    }

    private void set42(bool bTF)
    {
        txtTRP_42_1.Enabled = bTF;
        txtTRP_42_2.Enabled = bTF;
        txtTRP_42_3.Enabled = bTF;
        txtTIS_42_1.Enabled = bTF;
        txtTIS_42_2.Enabled = bTF;
        txtTIS_42_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_42_1.Text = "";
            txtTRP_42_2.Text = "";
            txtTRP_42_3.Text = "";
            txtTIS_42_1.Text = "";
            txtTIS_42_2.Text = "";
            txtTIS_42_3.Text = "";
        }
    }

    private void set43(bool bTF)
    {
        txtTRP_43_1.Enabled = bTF;
        txtTRP_43_2.Enabled = bTF;
        txtTRP_43_3.Enabled = bTF;
        txtTIS_43_1.Enabled = bTF;
        txtTIS_43_2.Enabled = bTF;
        txtTIS_43_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_43_1.Text = "";
            txtTRP_43_2.Text = "";
            txtTRP_43_3.Text = "";
            txtTIS_43_1.Text = "";
            txtTIS_43_2.Text = "";
            txtTIS_43_3.Text = "";
        }
    }

    private void set44(bool bTF)
    {
        txtTRP_44_1.Enabled = bTF;
        txtTRP_44_2.Enabled = bTF;
        txtTRP_44_3.Enabled = bTF;
        txtTIS_44_1.Enabled = bTF;
        txtTIS_44_2.Enabled = bTF;
        txtTIS_44_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_44_1.Text = "";
            txtTRP_44_2.Text = "";
            txtTRP_44_3.Text = "";
            txtTIS_44_1.Text = "";
            txtTIS_44_2.Text = "";
            txtTIS_44_3.Text = "";
        }
    }

    private void set45(bool bTF)
    {
        txtTRP_45_1.Enabled = bTF;
        txtTRP_45_2.Enabled = bTF;
        txtTRP_45_3.Enabled = bTF;
        txtTIS_45_1.Enabled = bTF;
        txtTIS_45_2.Enabled = bTF;
        txtTIS_45_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_45_1.Text = "";
            txtTRP_45_2.Text = "";
            txtTRP_45_3.Text = "";
            txtTIS_45_1.Text = "";
            txtTIS_45_2.Text = "";
            txtTIS_45_3.Text = "";
        }
    }

    private void set46(bool bTF)
    {
        txtTRP_46_1.Enabled = bTF;
        txtTRP_46_2.Enabled = bTF;
        txtTRP_46_3.Enabled = bTF;
        txtTIS_46_1.Enabled = bTF;
        txtTIS_46_2.Enabled = bTF;
        txtTIS_46_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_46_1.Text = "";
            txtTRP_46_2.Text = "";
            txtTRP_46_3.Text = "";
            txtTIS_46_1.Text = "";
            txtTIS_46_2.Text = "";
            txtTIS_46_3.Text = "";
        }
    }

    private void set47(bool bTF)
    {
        txtTRP_47_1.Enabled = bTF;
        txtTRP_47_2.Enabled = bTF;
        txtTRP_47_3.Enabled = bTF;
        txtTIS_47_1.Enabled = bTF;
        txtTIS_47_2.Enabled = bTF;
        txtTIS_47_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_47_1.Text = "";
            txtTRP_47_2.Text = "";
            txtTRP_47_3.Text = "";
            txtTIS_47_1.Text = "";
            txtTIS_47_2.Text = "";
            txtTIS_47_3.Text = "";
        }
    }

    private void set48(bool bTF)
    {
        txtTRP_48_1.Enabled = bTF;
        txtTRP_48_2.Enabled = bTF;
        txtTRP_48_3.Enabled = bTF;
        txtTIS_48_1.Enabled = bTF;
        txtTIS_48_2.Enabled = bTF;
        txtTIS_48_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_48_1.Text = "";
            txtTRP_48_2.Text = "";
            txtTRP_48_3.Text = "";
            txtTIS_48_1.Text = "";
            txtTIS_48_2.Text = "";
            txtTIS_48_3.Text = "";
        }
    }

    private void set50(bool bTF)
    {
        txtTRP_50_1.Enabled = bTF;
        txtTRP_50_2.Enabled = bTF;
        txtTRP_50_3.Enabled = bTF;
        txtTIS_50_1.Enabled = bTF;
        txtTIS_50_2.Enabled = bTF;
        txtTIS_50_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_50_1.Text = "";
            txtTRP_50_2.Text = "";
            txtTRP_50_3.Text = "";
            txtTIS_50_1.Text = "";
            txtTIS_50_2.Text = "";
            txtTIS_50_3.Text = "";
        }
    }

    private void set51(bool bTF)
    {
        txtTRP_51_1.Enabled = bTF;
        txtTIS_51_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_51_1.Text = "";
            txtTIS_51_1.Text = "";
        }
    }

    private void set65(bool bTF)
    {
        txtTRP_65_1.Enabled = bTF;
        txtTRP_65_2.Enabled = bTF;
        txtTRP_65_3.Enabled = bTF;
        txtTIS_65_1.Enabled = bTF;
        txtTIS_65_2.Enabled = bTF;
        txtTIS_65_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_65_1.Text = "";
            txtTRP_65_2.Text = "";
            txtTRP_65_3.Text = "";
            txtTIS_65_1.Text = "";
            txtTIS_65_2.Text = "";
            txtTIS_65_3.Text = "";
        }
    }

    private void set66(bool bTF)
    {
        txtTRP_66_1.Enabled = bTF;
        txtTRP_66_2.Enabled = bTF;
        txtTRP_66_3.Enabled = bTF;
        txtTIS_66_1.Enabled = bTF;
        txtTIS_66_2.Enabled = bTF;
        txtTIS_66_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_66_1.Text = "";
            txtTRP_66_2.Text = "";
            txtTRP_66_3.Text = "";
            txtTIS_66_1.Text = "";
            txtTIS_66_2.Text = "";
            txtTIS_66_3.Text = "";
        }
    }

    private void set68(bool bTF)
    {
        txtTRP_68_1.Enabled = bTF;
        txtTRP_68_2.Enabled = bTF;
        txtTRP_68_3.Enabled = bTF;
        txtTIS_68_1.Enabled = bTF;
        txtTIS_68_2.Enabled = bTF;
        txtTIS_68_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_68_1.Text = "";
            txtTRP_68_2.Text = "";
            txtTRP_68_3.Text = "";
            txtTIS_68_1.Text = "";
            txtTIS_68_2.Text = "";
            txtTIS_68_3.Text = "";
        }
    }

    private void set70(bool bTF)
    {
        txtTRP_70_1.Enabled = bTF;
        txtTIS_70_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_70_1.Text = "";
            txtTIS_70_1.Text = "";
        }
    }

    private void set71(bool bTF)
    {
        txtTRP_71_1.Enabled = bTF;
        txtTRP_71_2.Enabled = bTF;
        txtTRP_71_3.Enabled = bTF;
        txtTIS_71_1.Enabled = bTF;
        txtTIS_71_2.Enabled = bTF;
        txtTIS_71_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_71_1.Text = "";
            txtTRP_71_2.Text = "";
            txtTRP_71_3.Text = "";
            txtTIS_71_1.Text = "";
            txtTIS_71_2.Text = "";
            txtTIS_71_3.Text = "";
        }
    }

    private void set72(bool bTF)
    {
        txtTRP_72_1.Enabled = bTF;
        txtTIS_72_1.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_72_1.Text = "";
            txtTIS_72_1.Text = "";
        }
    }

    private void set74(bool bTF)
    {
        txtTRP_74_1.Enabled = bTF;
        txtTRP_74_2.Enabled = bTF;
        txtTRP_74_3.Enabled = bTF;
        txtTIS_74_1.Enabled = bTF;
        txtTIS_74_2.Enabled = bTF;
        txtTIS_74_3.Enabled = bTF;
        if (bTF == false)
        {
            txtTRP_74_1.Text = "";
            txtTRP_74_2.Text = "";
            txtTRP_74_3.Text = "";
            txtTIS_74_1.Text = "";
            txtTIS_74_2.Text = "";
            txtTIS_74_3.Text = "";
        }
    }

    private string checkBand1()
    {
        string strI = "0";

        if (chk1.Checked == true)
        {
            if (txtTRP_1_1.Text == "")
                strI = "1";
            if (txtTRP_1_2.Text == "")
                strI = "1";
            if (txtTRP_1_3.Text == "")
                strI = "1";
            if (txtTIS_1_1.Text == "")
                strI = "1";
            if (txtTIS_1_2.Text == "")
                strI = "1";
            if (txtTIS_1_3.Text == "")
                strI = "1";
        }

        return strI;
    }

    private string checkBand2()
    {
        string strI = "0";

        if (chk2.Checked == true)
        {
            if (txtTRP_2_1.Text == "")
                strI = "2";
            if (txtTRP_2_2.Text == "")
                strI = "2";
            if (txtTRP_2_3.Text == "")
                strI = "2";
            if (txtTIS_2_1.Text == "")
                strI = "2";
            if (txtTIS_2_2.Text == "")
                strI = "2";
            if (txtTIS_2_3.Text == "")
                strI = "2";
        }

        return strI;
    }

    private string checkBand3()
    {
        string strI = "0";

        if (chk3.Checked == true)
        {
            if (txtTRP_3_1.Text == "")
                strI = "3";
            if (txtTRP_3_2.Text == "")
                strI = "3";
            if (txtTRP_3_3.Text == "")
                strI = "3";
            if (txtTIS_3_1.Text == "")
                strI = "3";
            if (txtTIS_3_2.Text == "")
                strI = "3";
            if (txtTIS_3_3.Text == "")
                strI = "3";
        }

        return strI;
    }

    private string checkBand4()
    {
        string strI = "0";

        if (chk4.Checked == true)
        {
            if (txtTRP_4_1.Text == "")
                strI = "4";
            if (txtTRP_4_2.Text == "")
                strI = "4";
            if (txtTRP_4_3.Text == "")
                strI = "4";
            if (txtTIS_4_1.Text == "")
                strI = "4";
            if (txtTIS_4_2.Text == "")
                strI = "4";
            if (txtTIS_4_3.Text == "")
                strI = "4";
        }

        return strI;
    }

    private string checkBand5()
    {
        string strI = "0";

        if (chk5.Checked == true)
        {
            if (txtTRP_5_1.Text == "")
                strI = "5";
            if (txtTRP_5_2.Text == "")
                strI = "5";
            if (txtTRP_5_3.Text == "")
                strI = "5";
            if (txtTIS_5_1.Text == "")
                strI = "5";
            if (txtTIS_5_2.Text == "")
                strI = "5";
            if (txtTIS_5_3.Text == "")
                strI = "5";
        }

        return strI;
    }

    private string checkBand6()
    {
        string strI = "0";

        if (chk6.Checked == true)
        {
            if (txtTRP_6_1.Text == "")
                strI = "6";
            if (txtTIS_6_1.Text == "")
                strI = "6";
        }

        return strI;
    }

    private string checkBand7()
    {
        string strI = "0";

        if (chk7.Checked == true)
        {
            if (txtTRP_7_1.Text == "")
                strI = "7";
            if (txtTRP_7_2.Text == "")
                strI = "7";
            if (txtTRP_7_3.Text == "")
                strI = "7";
            if (txtTIS_7_1.Text == "")
                strI = "7";
            if (txtTIS_7_2.Text == "")
                strI = "7";
            if (txtTIS_7_3.Text == "")
                strI = "7";
        }

        return strI;
    }

    private string checkBand8()
    {
        string strI = "0";

        if (chk8.Checked == true)
        {
            if (txtTRP_8_1.Text == "")
                strI = "8";
            if (txtTRP_8_2.Text == "")
                strI = "8";
            if (txtTRP_8_3.Text == "")
                strI = "8";
            if (txtTIS_8_1.Text == "")
                strI = "8";
            if (txtTIS_8_2.Text == "")
                strI = "8";
            if (txtTIS_8_3.Text == "")
                strI = "8";
        }

        return strI;
    }

    private string checkBand9()
    {
        string strI = "0";

        if (chk9.Checked == true)
        {
            if (txtTRP_9_1.Text == "")
                strI = "9";
            if (txtTRP_9_2.Text == "")
                strI = "9";
            if (txtTRP_9_3.Text == "")
                strI = "9";
            if (txtTIS_9_1.Text == "")
                strI = "9";
            if (txtTIS_9_2.Text == "")
                strI = "9";
            if (txtTIS_9_3.Text == "")
                strI = "9";
        }

        return strI;
    }

    private string checkBand10()
    {
        string strI = "0";

        if (chk10.Checked == true)
        {
            if (txtTRP_10_1.Text == "")
                strI = "10";
            if (txtTRP_10_2.Text == "")
                strI = "10";
            if (txtTRP_10_3.Text == "")
                strI = "10";
            if (txtTIS_10_1.Text == "")
                strI = "10";
            if (txtTIS_10_2.Text == "")
                strI = "10";
            if (txtTIS_10_3.Text == "")
                strI = "10";
        }

        return strI;
    }

    private string checkBand11()
    {
        string strI = "0";

        if (chk11.Checked == true)
        {
            if (txtTRP_11_1.Text == "")
                strI = "11";
            if (txtTRP_11_2.Text == "")
                strI = "11";
            if (txtTRP_11_3.Text == "")
                strI = "11";
            if (txtTIS_11_1.Text == "")
                strI = "11";
            if (txtTIS_11_2.Text == "")
                strI = "11";
            if (txtTIS_11_3.Text == "")
                strI = "11";
        }

        return strI;
    }

    private string checkBand12()
    {
        string strI = "0";

        if (chk12.Checked == true)
        {
            if (txtTRP_12_1.Text == "")
                strI = "12";
            if (txtTRP_12_2.Text == "")
                strI = "12";
            if (txtTRP_12_3.Text == "")
                strI = "12";
            if (txtTIS_12_1.Text == "")
                strI = "12";
            if (txtTIS_12_2.Text == "")
                strI = "12";
            if (txtTIS_12_3.Text == "")
                strI = "12";
        }

        return strI;
    }

    private string checkBand13()
    {
        string strI = "0";

        if (chk13.Checked == true)
        {
            if (txtTRP_13_1.Text == "")
                strI = "13";
            if (txtTIS_13_1.Text == "")
                strI = "13";
        }

        return strI;
    }

    private string checkBand14()
    {
        string strI = "0";

        if (chk14.Checked == true)
        {
            if (txtTRP_14_1.Text == "")
                strI = "14";
            if (txtTIS_14_1.Text == "")
                strI = "14";
        }

        return strI;
    }

    private string checkBand17()
    {
        string strI = "0";

        if (chk17.Checked == true)
        {
            if (txtTRP_17_1.Text == "")
                strI = "17";
            if (txtTRP_17_2.Text == "")
                strI = "17";
            if (txtTRP_17_3.Text == "")
                strI = "17";
            if (txtTIS_17_1.Text == "")
                strI = "17";
            if (txtTIS_17_2.Text == "")
                strI = "17";
            if (txtTIS_17_3.Text == "")
                strI = "17";
        }

        return strI;
    }

    private string checkBand18()
    {
        string strI = "0";

        if (chk18.Checked == true)
        {
            if (txtTRP_18_1.Text == "")
                strI = "18";
            if (txtTRP_18_2.Text == "")
                strI = "18";
            if (txtTRP_18_3.Text == "")
                strI = "18";
            if (txtTIS_18_1.Text == "")
                strI = "18";
            if (txtTIS_18_2.Text == "")
                strI = "18";
            if (txtTIS_18_3.Text == "")
                strI = "18";
        }

        return strI;
    }

    private string checkBand19()
    {
        string strI = "0";

        if (chk19.Checked == true)
        {
            if (txtTRP_19_1.Text == "")
                strI = "19";
            if (txtTRP_19_2.Text == "")
                strI = "19";
            if (txtTRP_19_3.Text == "")
                strI = "19";
            if (txtTIS_19_1.Text == "")
                strI = "19";
            if (txtTIS_19_2.Text == "")
                strI = "19";
            if (txtTIS_19_3.Text == "")
                strI = "19";
        }

        return strI;
    }

    private string checkBand20()
    {
        string strI = "0";

        if (chk20.Checked == true)
        {
            if (txtTRP_20_1.Text == "")
                strI = "20";
            if (txtTRP_20_2.Text == "")
                strI = "20";
            if (txtTRP_20_3.Text == "")
                strI = "20";
            if (txtTIS_20_1.Text == "")
                strI = "20";
            if (txtTIS_20_2.Text == "")
                strI = "20";
            if (txtTIS_20_3.Text == "")
                strI = "20";
        }

        return strI;
    }

    private string checkBand21()
    {
        string strI = "0";

        if (chk21.Checked == true)
        {
            if (txtTRP_21_1.Text == "")
                strI = "21";
            if (txtTRP_21_2.Text == "")
                strI = "21";
            if (txtTRP_21_3.Text == "")
                strI = "21";
            if (txtTIS_21_1.Text == "")
                strI = "21";
            if (txtTIS_21_2.Text == "")
                strI = "21";
            if (txtTIS_21_3.Text == "")
                strI = "21";
        }

        return strI;
    }

    private string checkBand22()
    {
        string strI = "0";

        if (chk22.Checked == true)
        {
            if (txtTRP_22_1.Text == "")
                strI = "22";
            if (txtTRP_22_2.Text == "")
                strI = "22";
            if (txtTRP_22_3.Text == "")
                strI = "22";
            if (txtTIS_22_1.Text == "")
                strI = "22";
            if (txtTIS_22_2.Text == "")
                strI = "22";
            if (txtTIS_22_3.Text == "")
                strI = "22";
        }

        return strI;
    }

    private string checkBand23()
    {
        string strI = "0";

        if (chk23.Checked == true)
        {
            if (txtTRP_23_1.Text == "")
                strI = "23";
            if (txtTRP_23_2.Text == "")
                strI = "23";
            if (txtTRP_23_3.Text == "")
                strI = "23";
            if (txtTIS_23_1.Text == "")
                strI = "23";
            if (txtTIS_23_2.Text == "")
                strI = "23";
            if (txtTIS_23_3.Text == "")
                strI = "23";
        }

        return strI;
    }

    private string checkBand24()
    {
        string strI = "0";

        if (chk24.Checked == true)
        {
            if (txtTRP_24_1.Text == "")
                strI = "24";
            if (txtTRP_24_2.Text == "")
                strI = "24";
            if (txtTRP_24_3.Text == "")
                strI = "24";
            if (txtTIS_24_1.Text == "")
                strI = "24";
            if (txtTIS_24_2.Text == "")
                strI = "24";
            if (txtTIS_24_3.Text == "")
                strI = "24";
        }

        return strI;
    }

    private string checkBand25()
    {
        string strI = "0";

        if (chk25.Checked == true)
        {
            if (txtTRP_25_1.Text == "")
                strI = "25";
            if (txtTRP_25_2.Text == "")
                strI = "25";
            if (txtTRP_25_3.Text == "")
                strI = "25";
            if (txtTIS_25_1.Text == "")
                strI = "25";
            if (txtTIS_25_2.Text == "")
                strI = "25";
            if (txtTIS_25_3.Text == "")
                strI = "25";
        }

        return strI;
    }

    private string checkBand26()
    {
        string strI = "0";

        if (chk26.Checked == true)
        {
            if (txtTRP_26_1.Text == "")
                strI = "26";
            if (txtTRP_26_2.Text == "")
                strI = "26";
            if (txtTRP_26_3.Text == "")
                strI = "26";
            if (txtTIS_26_1.Text == "")
                strI = "26";
            if (txtTIS_26_2.Text == "")
                strI = "26";
            if (txtTIS_26_3.Text == "")
                strI = "26";
        }

        return strI;
    }

    private string checkBand27()
    {
        string strI = "0";

        if (chk27.Checked == true)
        {
            if (txtTRP_27_1.Text == "")
                strI = "27";
            if (txtTRP_27_2.Text == "")
                strI = "27";
            if (txtTRP_27_3.Text == "")
                strI = "27";
            if (txtTIS_27_1.Text == "")
                strI = "27";
            if (txtTIS_27_2.Text == "")
                strI = "27";
            if (txtTIS_27_3.Text == "")
                strI = "27";
        }

        return strI;
    }

    private string checkBand28()
    {
        string strI = "0";

        if (chk28.Checked == true)
        {
            if (txtTRP_28_1.Text == "")
                strI = "28";
            if (txtTRP_28_2.Text == "")
                strI = "28";
            if (txtTRP_28_3.Text == "")
                strI = "28";
            if (txtTIS_28_1.Text == "")
                strI = "28";
            if (txtTIS_28_2.Text == "")
                strI = "28";
            if (txtTIS_28_3.Text == "")
                strI = "28";
        }

        return strI;
    }

    private string checkBand30()
    {
        string strI = "0";

        if (chk30.Checked == true)
        {
            if (txtTRP_30_1.Text == "")
                strI = "30";
            if (txtTIS_30_1.Text == "")
                strI = "30";
        }

        return strI;
    }

    private string checkBand31()
    {
        string strI = "0";

        if (chk31.Checked == true)
        {
            if (txtTRP_31_1.Text == "")
                strI = "31";
            if (txtTIS_31_1.Text == "")
                strI = "31";
        }

        return strI;
    }

    private string checkBand33()
    {
        string strI = "0";

        if (chk33.Checked == true)
        {
            if (txtTRP_33_1.Text == "")
                strI = "33";
            if (txtTRP_33_2.Text == "")
                strI = "33";
            if (txtTRP_33_3.Text == "")
                strI = "33";
            if (txtTIS_33_1.Text == "")
                strI = "33";
            if (txtTIS_33_2.Text == "")
                strI = "33";
            if (txtTIS_33_3.Text == "")
                strI = "33";
        }

        return strI;
    }

    private string checkBand34()
    {
        string strI = "0";

        if (chk34.Checked == true)
        {
            if (txtTRP_34_1.Text == "")
                strI = "34";
            if (txtTRP_34_2.Text == "")
                strI = "34";
            if (txtTRP_34_3.Text == "")
                strI = "34";
            if (txtTIS_34_1.Text == "")
                strI = "34";
            if (txtTIS_34_2.Text == "")
                strI = "34";
            if (txtTIS_34_3.Text == "")
                strI = "34";
        }

        return strI;
    }

    private string checkBand35()
    {
        string strI = "0";

        if (chk35.Checked == true)
        {
            if (txtTRP_35_1.Text == "")
                strI = "35";
            if (txtTRP_35_2.Text == "")
                strI = "35";
            if (txtTRP_35_3.Text == "")
                strI = "35";
            if (txtTIS_35_1.Text == "")
                strI = "35";
            if (txtTIS_35_2.Text == "")
                strI = "35";
            if (txtTIS_35_3.Text == "")
                strI = "35";
        }

        return strI;
    }

    private string checkBand36()
    {
        string strI = "0";

        if (chk36.Checked == true)
        {
            if (txtTRP_36_1.Text == "")
                strI = "36";
            if (txtTRP_36_2.Text == "")
                strI = "36";
            if (txtTRP_36_3.Text == "")
                strI = "36";
            if (txtTIS_36_1.Text == "")
                strI = "36";
            if (txtTIS_36_2.Text == "")
                strI = "36";
            if (txtTIS_36_3.Text == "")
                strI = "36";
        }

        return strI;
    }

    private string checkBand37()
    {
        string strI = "0";

        if (chk37.Checked == true)
        {
            if (txtTRP_37_1.Text == "")
                strI = "37";
            if (txtTRP_37_2.Text == "")
                strI = "37";
            if (txtTRP_37_3.Text == "")
                strI = "37";
            if (txtTIS_37_1.Text == "")
                strI = "37";
            if (txtTIS_37_2.Text == "")
                strI = "37";
            if (txtTIS_37_3.Text == "")
                strI = "37";
        }

        return strI;
    }

    private string checkBand38()
    {
        string strI = "0";

        if (chk38.Checked == true)
        {
            if (txtTRP_38_1.Text == "")
                strI = "38";
            if (txtTRP_38_2.Text == "")
                strI = "38";
            if (txtTRP_38_3.Text == "")
                strI = "38";
            if (txtTIS_38_1.Text == "")
                strI = "38";
            if (txtTIS_38_2.Text == "")
                strI = "38";
            if (txtTIS_38_3.Text == "")
                strI = "38";
        }

        return strI;
    }

    private string checkBand39()
    {
        string strI = "0";

        if (chk39.Checked == true)
        {
            if (txtTRP_39_1.Text == "")
                strI = "39";
            if (txtTRP_39_2.Text == "")
                strI = "39";
            if (txtTRP_39_3.Text == "")
                strI = "39";
            if (txtTIS_39_1.Text == "")
                strI = "39";
            if (txtTIS_39_2.Text == "")
                strI = "39";
            if (txtTIS_39_3.Text == "")
                strI = "39";
        }

        return strI;
    }

    private string checkBand40()
    {
        string strI = "0";

        if (chk40.Checked == true)
        {
            if (txtTRP_40_1.Text == "")
                strI = "40";
            if (txtTRP_40_2.Text == "")
                strI = "40";
            if (txtTRP_40_3.Text == "")
                strI = "40";
            if (txtTIS_40_1.Text == "")
                strI = "40";
            if (txtTIS_40_2.Text == "")
                strI = "40";
            if (txtTIS_40_3.Text == "")
                strI = "40";
        }

        return strI;
    }

    private string checkBand41()
    {
        string strI = "0";

        if (chk41.Checked == true)
        {
            if (txtTRP_41_1.Text == "")
                strI = "41";
            if (txtTRP_41_2.Text == "")
                strI = "41";
            if (txtTRP_41_3.Text == "")
                strI = "41";
            if (txtTIS_41_1.Text == "")
                strI = "41";
            if (txtTIS_41_2.Text == "")
                strI = "41";
            if (txtTIS_41_3.Text == "")
                strI = "41";
        }

        return strI;
    }

    private string checkBand42()
    {
        string strI = "0";

        if (chk42.Checked == true)
        {
            if (txtTRP_42_1.Text == "")
                strI = "42";
            if (txtTRP_42_2.Text == "")
                strI = "42";
            if (txtTRP_42_3.Text == "")
                strI = "42";
            if (txtTIS_42_1.Text == "")
                strI = "42";
            if (txtTIS_42_2.Text == "")
                strI = "42";
            if (txtTIS_42_3.Text == "")
                strI = "42";
        }

        return strI;
    }

    private string checkBand43()
    {
        string strI = "0";

        if (chk43.Checked == true)
        {
            if (txtTRP_43_1.Text == "")
                strI = "43";
            if (txtTRP_43_2.Text == "")
                strI = "43";
            if (txtTRP_43_3.Text == "")
                strI = "43";
            if (txtTIS_43_1.Text == "")
                strI = "43";
            if (txtTIS_43_2.Text == "")
                strI = "43";
            if (txtTIS_43_3.Text == "")
                strI = "43";
        }

        return strI;
    }

    private string checkBand44()
    {
        string strI = "0";

        if (chk44.Checked == true)
        {
            if (txtTRP_44_1.Text == "")
                strI = "44";
            if (txtTRP_44_2.Text == "")
                strI = "44";
            if (txtTRP_44_3.Text == "")
                strI = "44";
            if (txtTIS_44_1.Text == "")
                strI = "44";
            if (txtTIS_44_2.Text == "")
                strI = "44";
            if (txtTIS_44_3.Text == "")
                strI = "44";
        }

        return strI;
    }

    private string checkBand45()
    {
        string strI = "0";

        if (chk45.Checked == true)
        {
            if (txtTRP_45_1.Text == "")
                strI = "45";
            if (txtTRP_45_2.Text == "")
                strI = "45";
            if (txtTRP_45_3.Text == "")
                strI = "45";
            if (txtTIS_45_1.Text == "")
                strI = "45";
            if (txtTIS_45_2.Text == "")
                strI = "45";
            if (txtTIS_45_3.Text == "")
                strI = "45";
        }

        return strI;
    }

    private string checkBand46()
    {
        string strI = "0";

        if (chk46.Checked == true)
        {
            if (txtTRP_46_1.Text == "")
                strI = "46";
            if (txtTRP_46_2.Text == "")
                strI = "46";
            if (txtTRP_46_3.Text == "")
                strI = "46";
            if (txtTIS_46_1.Text == "")
                strI = "46";
            if (txtTIS_46_2.Text == "")
                strI = "46";
            if (txtTIS_46_3.Text == "")
                strI = "46";
        }

        return strI;
    }

    private string checkBand47()
    {
        string strI = "0";

        if (chk47.Checked == true)
        {
            if (txtTRP_47_1.Text == "")
                strI = "47";
            if (txtTRP_47_2.Text == "")
                strI = "47";
            if (txtTRP_47_3.Text == "")
                strI = "47";
            if (txtTIS_47_1.Text == "")
                strI = "47";
            if (txtTIS_47_2.Text == "")
                strI = "47";
            if (txtTIS_47_3.Text == "")
                strI = "47";
        }

        return strI;
    }

    private string checkBand48()
    {
        string strI = "0";

        if (chk48.Checked == true)
        {
            if (txtTRP_48_1.Text == "")
                strI = "48";
            if (txtTRP_48_2.Text == "")
                strI = "48";
            if (txtTRP_48_3.Text == "")
                strI = "48";
            if (txtTIS_48_1.Text == "")
                strI = "48";
            if (txtTIS_48_2.Text == "")
                strI = "48";
            if (txtTIS_48_3.Text == "")
                strI = "48";
        }

        return strI;
    }

    private string checkBand50()
    {
        string strI = "0";

        if (chk50.Checked == true)
        {
            if (txtTRP_50_1.Text == "")
                strI = "50";
            if (txtTRP_50_2.Text == "")
                strI = "50";
            if (txtTRP_50_3.Text == "")
                strI = "50";
            if (txtTIS_50_1.Text == "")
                strI = "50";
            if (txtTIS_50_2.Text == "")
                strI = "50";
            if (txtTIS_50_3.Text == "")
                strI = "50";
        }

        return strI;
    }

    private string checkBand51()
    {
        string strI = "0";

        if (chk51.Checked == true)
        {
            if (txtTRP_51_1.Text == "")
                strI = "51";
            if (txtTIS_51_1.Text == "")
                strI = "51";
        }

        return strI;
    }

    private string checkBand65()
    {
        string strI = "0";

        if (chk65.Checked == true)
        {
            if (txtTRP_65_1.Text == "")
                strI = "65";
            if (txtTRP_65_2.Text == "")
                strI = "65";
            if (txtTRP_65_3.Text == "")
                strI = "65";
            if (txtTIS_65_1.Text == "")
                strI = "65";
            if (txtTIS_65_2.Text == "")
                strI = "65";
            if (txtTIS_65_3.Text == "")
                strI = "65";
        }

        return strI;
    }

    private string checkBand66()
    {
        string strI = "0";

        if (chk66.Checked == true)
        {
            if (txtTRP_66_1.Text == "")
                strI = "66";
            if (txtTRP_66_2.Text == "")
                strI = "66";
            if (txtTRP_66_3.Text == "")
                strI = "66";
            if (txtTIS_66_1.Text == "")
                strI = "66";
            if (txtTIS_66_2.Text == "")
                strI = "66";
            if (txtTIS_66_3.Text == "")
                strI = "66";
        }

        return strI;
    }

    private string checkBand68()
    {
        string strI = "0";

        if (chk68.Checked == true)
        {
            if (txtTRP_68_1.Text == "")
                strI = "68";
            if (txtTRP_68_2.Text == "")
                strI = "68";
            if (txtTRP_68_3.Text == "")
                strI = "68";
            if (txtTIS_68_1.Text == "")
                strI = "68";
            if (txtTIS_68_2.Text == "")
                strI = "68";
            if (txtTIS_68_3.Text == "")
                strI = "68";
        }

        return strI;
    }

    private string checkBand70()
    {
        string strI = "0";

        if (chk70.Checked == true)
        {
            if (txtTRP_70_1.Text == "")
                strI = "70";
            if (txtTIS_70_1.Text == "")
                strI = "70";
        }

        return strI;
    }

    private string checkBand71()
    {
        string strI = "0";

        if (chk71.Checked == true)
        {
            if (txtTRP_71_1.Text == "")
                strI = "71";
            if (txtTRP_71_2.Text == "")
                strI = "71";
            if (txtTRP_71_3.Text == "")
                strI = "71";
            if (txtTIS_71_1.Text == "")
                strI = "71";
            if (txtTIS_71_2.Text == "")
                strI = "71";
            if (txtTIS_71_3.Text == "")
                strI = "71";
        }

        return strI;
    }

    private string checkBand72()
    {
        string strI = "0";

        if (chk72.Checked == true)
        {
            if (txtTRP_72_1.Text == "")
                strI = "72";
            if (txtTIS_72_1.Text == "")
                strI = "72";
        }

        return strI;
    }

    private string checkBand74()
    {
        string strI = "0";

        if (chk74.Checked == true)
        {
            if (txtTRP_74_1.Text == "")
                strI = "74";
            if (txtTRP_74_2.Text == "")
                strI = "74";
            if (txtTRP_74_3.Text == "")
                strI = "74";
            if (txtTIS_74_1.Text == "")
                strI = "74";
            if (txtTIS_74_2.Text == "")
                strI = "74";
            if (txtTIS_74_3.Text == "")
                strI = "74";
        }

        return strI;
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strCheck = "";
        string strCheck1 = "";
        string strProjectID = Session["ApplicationID"].ToString();

        strCheck = checkBand1();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand2();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand3();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand4();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand5();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand6();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand7();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand8();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand9();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand10();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand11();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand12();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand13();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand14();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand17();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand18();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand19();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand20();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand21();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand22();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand23();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand24();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand25();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand26();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand27();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand28();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand30();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand31();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand33();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand34();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand35();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand36();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand37();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand38();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand39();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand40();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand41();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand42();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand43();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand44();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand45();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand46();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand47();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand48();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand50();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand51();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand65();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand66();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand68();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand70();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand71();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand72();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        strCheck = checkBand74();
        if (strCheck != "0")
            strCheck1 = strCheck1 + strCheck + ",";

        if (strCheck1 != "")
            clsMsg.AlertMessage("Band" + strCheck1 + "尚未填寫完成", this.Page);
        else
        {
            if (clsTransaction.DelApplication_LTE(strProjectID) == true)
            {

                if (chk1.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "1", txtTRP_1_1.Text, txtTRP_1_2.Text, txtTRP_1_3.Text, txtTIS_1_1.Text, txtTIS_1_2.Text, txtTIS_1_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band1暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk2.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "2", txtTRP_2_1.Text, txtTRP_2_2.Text, txtTRP_2_3.Text, txtTIS_2_1.Text, txtTIS_2_2.Text, txtTIS_2_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band2暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk3.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "3", txtTRP_3_1.Text, txtTRP_3_2.Text, txtTRP_3_3.Text, txtTIS_3_1.Text, txtTIS_3_2.Text, txtTIS_3_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band3暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk4.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "4", txtTRP_4_1.Text, txtTRP_4_2.Text, txtTRP_4_3.Text, txtTIS_4_1.Text, txtTIS_4_2.Text, txtTIS_4_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band4暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk5.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "5", txtTRP_5_1.Text, txtTRP_5_2.Text, txtTRP_5_3.Text, txtTIS_5_1.Text, txtTIS_5_2.Text, txtTIS_5_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band5暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk6.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "6", txtTRP_6_1.Text, "", "", txtTIS_6_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band6暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk7.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "7", txtTRP_7_1.Text, txtTRP_7_2.Text, txtTRP_7_3.Text, txtTIS_7_1.Text, txtTIS_7_2.Text, txtTIS_7_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band7暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk8.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "8", txtTRP_8_1.Text, txtTRP_8_2.Text, txtTRP_8_3.Text, txtTIS_8_1.Text, txtTIS_8_2.Text, txtTIS_8_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band8暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk9.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "9", txtTRP_9_1.Text, txtTRP_9_2.Text, txtTRP_9_3.Text, txtTIS_9_1.Text, txtTIS_9_2.Text, txtTIS_9_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band9暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk10.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "10", txtTRP_10_1.Text, txtTRP_10_2.Text, txtTRP_10_3.Text, txtTIS_10_1.Text, txtTIS_10_2.Text, txtTIS_10_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band10暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk11.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "11", txtTRP_11_1.Text, txtTRP_11_2.Text, txtTRP_11_3.Text, txtTIS_11_1.Text, txtTIS_11_2.Text, txtTIS_11_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band11暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk12.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "12", txtTRP_12_1.Text, txtTRP_12_2.Text, txtTRP_12_3.Text, txtTIS_12_1.Text, txtTIS_12_2.Text, txtTIS_12_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band12暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk13.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "13", txtTRP_13_1.Text, "", "", txtTIS_13_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band13暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk14.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "14", txtTRP_14_1.Text, "", "", txtTIS_14_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band14暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk17.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "17", txtTRP_17_1.Text, txtTRP_17_2.Text, txtTRP_17_3.Text, txtTIS_17_1.Text, txtTIS_17_2.Text, txtTIS_17_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band17暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk18.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "18", txtTRP_18_1.Text, txtTRP_18_2.Text, txtTRP_18_3.Text, txtTIS_18_1.Text, txtTIS_18_2.Text, txtTIS_18_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band18暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk19.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "19", txtTRP_19_1.Text, txtTRP_19_2.Text, txtTRP_19_3.Text, txtTIS_19_1.Text, txtTIS_19_2.Text, txtTIS_19_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band19暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk20.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "20", txtTRP_20_1.Text, txtTRP_20_2.Text, txtTRP_20_3.Text, txtTIS_20_1.Text, txtTIS_20_2.Text, txtTIS_20_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band20暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk21.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "21", txtTRP_21_1.Text, txtTRP_21_2.Text, txtTRP_21_3.Text, txtTIS_21_1.Text, txtTIS_21_2.Text, txtTIS_21_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band21暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk22.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "22", txtTRP_22_1.Text, txtTRP_22_2.Text, txtTRP_22_3.Text, txtTIS_22_1.Text, txtTIS_22_2.Text, txtTIS_22_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band22暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk23.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "23", txtTRP_23_1.Text, txtTRP_23_2.Text, txtTRP_23_3.Text, txtTIS_23_1.Text, txtTIS_23_2.Text, txtTIS_23_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band23暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk24.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "24", txtTRP_24_1.Text, txtTRP_24_2.Text, txtTRP_24_3.Text, txtTIS_24_1.Text, txtTIS_24_2.Text, txtTIS_24_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band24暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk25.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "25", txtTRP_25_1.Text, txtTRP_25_2.Text, txtTRP_25_3.Text, txtTIS_25_1.Text, txtTIS_25_2.Text, txtTIS_25_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band25暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk26.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "26", txtTRP_26_1.Text, txtTRP_26_2.Text, txtTRP_26_3.Text, txtTIS_26_1.Text, txtTIS_26_2.Text, txtTIS_26_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band26暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk27.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "27", txtTRP_27_1.Text, txtTRP_27_2.Text, txtTRP_27_3.Text, txtTIS_27_1.Text, txtTIS_27_2.Text, txtTIS_27_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band27暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk28.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "28", txtTRP_28_1.Text, txtTRP_28_2.Text, txtTRP_28_3.Text, txtTIS_28_1.Text, txtTIS_28_2.Text, txtTIS_28_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band28暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk30.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "30", txtTRP_30_1.Text, "", "", txtTIS_30_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band30暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk31.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "31", txtTRP_31_1.Text, "", "", txtTIS_31_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band31暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk33.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "33", txtTRP_33_1.Text, txtTRP_33_2.Text, txtTRP_33_3.Text, txtTIS_33_1.Text, txtTIS_33_2.Text, txtTIS_33_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band33暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk34.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "34", txtTRP_34_1.Text, txtTRP_34_2.Text, txtTRP_34_3.Text, txtTIS_34_1.Text, txtTIS_34_2.Text, txtTIS_34_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band34暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk35.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "35", txtTRP_35_1.Text, txtTRP_35_2.Text, txtTRP_35_3.Text, txtTIS_35_1.Text, txtTIS_35_2.Text, txtTIS_35_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band35暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk36.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "36", txtTRP_36_1.Text, txtTRP_36_2.Text, txtTRP_36_3.Text, txtTIS_36_1.Text, txtTIS_36_2.Text, txtTIS_36_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band36暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk37.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "37", txtTRP_37_1.Text, txtTRP_37_2.Text, txtTRP_37_3.Text, txtTIS_37_1.Text, txtTIS_37_2.Text, txtTIS_37_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band37暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk38.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "38", txtTRP_38_1.Text, txtTRP_38_2.Text, txtTRP_38_3.Text, txtTIS_38_1.Text, txtTIS_38_2.Text, txtTIS_38_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band38暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk39.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "39", txtTRP_39_1.Text, txtTRP_39_2.Text, txtTRP_39_3.Text, txtTIS_39_1.Text, txtTIS_39_2.Text, txtTIS_39_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band39暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk40.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "40", txtTRP_40_1.Text, txtTRP_40_2.Text, txtTRP_40_3.Text, txtTIS_40_1.Text, txtTIS_40_2.Text, txtTIS_40_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band40暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk41.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "41", txtTRP_41_1.Text, txtTRP_41_2.Text, txtTRP_41_3.Text, txtTIS_41_1.Text, txtTIS_41_2.Text, txtTIS_41_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band41暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk42.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "42", txtTRP_42_1.Text, txtTRP_42_2.Text, txtTRP_42_3.Text, txtTIS_42_1.Text, txtTIS_42_2.Text, txtTIS_42_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band42暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk43.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "43", txtTRP_43_1.Text, txtTRP_43_2.Text, txtTRP_43_3.Text, txtTIS_43_1.Text, txtTIS_43_2.Text, txtTIS_43_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band43暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk44.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "44", txtTRP_44_1.Text, txtTRP_44_2.Text, txtTRP_44_3.Text, txtTIS_44_1.Text, txtTIS_44_2.Text, txtTIS_44_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band44暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk45.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "45", txtTRP_45_1.Text, txtTRP_45_2.Text, txtTRP_45_3.Text, txtTIS_45_1.Text, txtTIS_45_2.Text, txtTIS_45_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band45暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk46.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "46", txtTRP_46_1.Text, txtTRP_46_2.Text, txtTRP_46_3.Text, txtTIS_46_1.Text, txtTIS_46_2.Text, txtTIS_46_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band46暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk47.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "47", txtTRP_47_1.Text, txtTRP_47_2.Text, txtTRP_47_3.Text, txtTIS_47_1.Text, txtTIS_47_2.Text, txtTIS_47_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band47暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk48.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "48", txtTRP_48_1.Text, txtTRP_48_2.Text, txtTRP_48_3.Text, txtTIS_48_1.Text, txtTIS_48_2.Text, txtTIS_48_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band48暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk50.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "50", txtTRP_50_1.Text, txtTRP_50_2.Text, txtTRP_50_3.Text, txtTIS_50_1.Text, txtTIS_50_2.Text, txtTIS_50_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band50暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk51.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "51", txtTRP_51_1.Text, "", "", txtTIS_51_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band51暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk65.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "65", txtTRP_65_1.Text, txtTRP_65_2.Text, txtTRP_65_3.Text, txtTIS_65_1.Text, txtTIS_65_2.Text, txtTIS_65_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band65暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk66.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "66", txtTRP_66_1.Text, txtTRP_66_2.Text, txtTRP_66_3.Text, txtTIS_66_1.Text, txtTIS_66_2.Text, txtTIS_66_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band66暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk68.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "68", txtTRP_68_1.Text, txtTRP_68_2.Text, txtTRP_68_3.Text, txtTIS_68_1.Text, txtTIS_68_2.Text, txtTIS_68_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band68暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk70.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "70", txtTRP_70_1.Text, "", "", txtTIS_70_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band70暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk71.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "71", txtTRP_71_1.Text, txtTRP_71_2.Text, txtTRP_71_3.Text, txtTIS_71_1.Text, txtTIS_71_2.Text, txtTIS_71_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band71暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk72.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "72", txtTRP_72_1.Text, "", "", txtTIS_72_1.Text, "", "") == false)
                    {
                        clsMsg.AlertMessage("Band72暫存失敗....", this.Page);
                        return;
                    }
                }

                if (chk74.Checked == true)
                {
                    if (clsTransaction.InsertApplication_LTE(strProjectID, "74", txtTRP_74_1.Text, txtTRP_74_2.Text, txtTRP_74_3.Text, txtTIS_74_1.Text, txtTIS_74_2.Text, txtTIS_74_3.Text) == false)
                    {
                        clsMsg.AlertMessage("Band74暫存失敗....", this.Page);
                        return;
                    }
                }
            }
            else
            {
                clsMsg.AlertMessage("暫存失敗....", this.Page);
                return;
            }


            clsMsg.AlertMessage("暫存成功....", this.Page);
        }
    }

    private void getData()
    {
        DataTable dt;
        string strProjectID = Session["ApplicationID"].ToString();

        dt = clsData.UploadApplication_LTE(strProjectID);

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            if (dt.Rows[intI]["Band"].ToString() == "1")
            {
                chk1.Checked = true;
                set1(true);
                txtTRP_1_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_1_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_1_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_1_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_1_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_1_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "2")
            {
                chk2.Checked = true;
                set2(true);
                txtTRP_2_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_2_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_2_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_2_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_2_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_2_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "3")
            {
                chk3.Checked = true;
                set3(true);
                txtTRP_3_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_3_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_3_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_3_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_3_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_3_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "4")
            {
                chk4.Checked = true;
                set4(true);
                txtTRP_4_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_4_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_4_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_4_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_4_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_4_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "5")
            {
                chk5.Checked = true;
                set5(true);
                txtTRP_5_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_5_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_5_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_5_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_5_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_5_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "6")
            {
                chk6.Checked = true;
                set6(true);
                txtTRP_6_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_6_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "7")
            {
                chk7.Checked = true;
                set7(true);
                txtTRP_7_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_7_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_7_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_7_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_7_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_7_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "8")
            {
                chk8.Checked = true;
                set8(true);
                txtTRP_8_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_8_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_8_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_8_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_8_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_8_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "9")
            {
                chk9.Checked = true;
                set9(true);
                txtTRP_9_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_9_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_9_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_9_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_9_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_9_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "10")
            {
                chk10.Checked = true;
                set10(true);
                txtTRP_10_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_10_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_10_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_10_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_10_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_10_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "11")
            {
                chk11.Checked = true;
                set11(true);
                txtTRP_11_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_11_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_11_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_11_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_11_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_11_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "12")
            {
                chk12.Checked = true;
                set12(true);
                txtTRP_12_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_12_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_12_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_12_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_12_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_12_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "13")
            {
                chk13.Checked = true;
                set13(true);
                txtTRP_13_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_13_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "14")
            {
                chk14.Checked = true;
                set14(true);
                txtTRP_14_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_14_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "17")
            {
                chk17.Checked = true;
                set17(true);
                txtTRP_17_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_17_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_17_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_17_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_17_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_17_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "18")
            {
                chk18.Checked = true;
                set18(true);
                txtTRP_18_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_18_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_18_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_18_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_18_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_18_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "19")
            {
                chk19.Checked = true;
                set19(true);
                txtTRP_19_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_19_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_19_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_19_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_19_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_19_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "20")
            {
                chk20.Checked = true;
                set20(true);
                txtTRP_20_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_20_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_20_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_20_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_20_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_20_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "21")
            {
                chk21.Checked = true;
                set21(true);
                txtTRP_21_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_21_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_21_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_21_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_21_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_21_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "22")
            {
                chk22.Checked = true;
                set22(true);
                txtTRP_22_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_22_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_22_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_22_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_22_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_22_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "23")
            {
                chk23.Checked = true;
                set23(true);
                txtTRP_23_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_23_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_23_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_23_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_23_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_23_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "24")
            {
                chk24.Checked = true;
                set24(true);
                txtTRP_24_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_24_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_24_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_24_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_24_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_24_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "25")
            {
                chk25.Checked = true;
                set25(true);
                txtTRP_25_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_25_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_25_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_25_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_25_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_25_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "26")
            {
                chk26.Checked = true;
                set26(true);
                txtTRP_26_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_26_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_26_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_26_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_26_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_26_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "27")
            {
                chk27.Checked = true;
                set27(true);
                txtTRP_27_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_27_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_27_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_27_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_27_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_27_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "28")
            {
                chk28.Checked = true;
                set28(true);
                txtTRP_28_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_28_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_28_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_28_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_28_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_28_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "30")
            {
                chk30.Checked = true;
                set30(true);
                txtTRP_30_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_30_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "31")
            {
                chk31.Checked = true;
                set31(true);
                txtTRP_31_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_31_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "33")
            {
                chk33.Checked = true;
                set33(true);
                txtTRP_33_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_33_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_33_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_33_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_33_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_33_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "34")
            {
                chk34.Checked = true;
                set34(true);
                txtTRP_34_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_34_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_34_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_34_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_34_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_34_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "35")
            {
                chk35.Checked = true;
                set35(true);
                txtTRP_35_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_35_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_35_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_35_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_35_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_35_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "36")
            {
                chk36.Checked = true;
                set36(true);
                txtTRP_36_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_36_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_36_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_36_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_36_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_36_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "37")
            {
                chk37.Checked = true;
                set37(true);
                txtTRP_37_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_37_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_37_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_37_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_37_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_37_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "38")
            {
                chk38.Checked = true;
                set38(true);
                txtTRP_38_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_38_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_38_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_38_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_38_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_38_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "39")
            {
                chk39.Checked = true;
                set39(true);
                txtTRP_39_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_39_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_39_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_39_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_39_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_39_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "40")
            {
                chk40.Checked = true;
                set40(true);
                txtTRP_40_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_40_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_40_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_40_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_40_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_40_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "41")
            {
                chk41.Checked = true;
                set41(true);
                txtTRP_41_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_41_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_41_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_41_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_41_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_41_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "42")
            {
                chk42.Checked = true;
                set42(true);
                txtTRP_42_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_42_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_42_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_42_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_42_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_42_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "43")
            {
                chk43.Checked = true;
                set43(true);
                txtTRP_43_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_43_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_43_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_43_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_43_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_43_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "44")
            {
                chk44.Checked = true;
                set44(true);
                txtTRP_44_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_44_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_44_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_44_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_44_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_44_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "45")
            {
                chk45.Checked = true;
                set45(true);
                txtTRP_45_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_45_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_45_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_45_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_45_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_45_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "46")
            {
                chk46.Checked = true;
                set46(true);
                txtTRP_46_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_46_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_46_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_46_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_46_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_46_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "47")
            {
                chk47.Checked = true;
                set47(true);
                txtTRP_47_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_47_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_47_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_47_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_47_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_47_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "48")
            {
                chk48.Checked = true;
                set48(true);
                txtTRP_48_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_48_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_48_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_48_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_48_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_48_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "50")
            {
                chk50.Checked = true;
                set50(true);
                txtTRP_50_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_50_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_50_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_50_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_50_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_50_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "51")
            {
                chk51.Checked = true;
                set51(true);
                txtTRP_51_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_51_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "65")
            {
                chk65.Checked = true;
                set65(true);
                txtTRP_65_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_65_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_65_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_65_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_65_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_65_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "66")
            {
                chk66.Checked = true;
                set66(true);
                txtTRP_66_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_66_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_66_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_66_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_66_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_66_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "68")
            {
                chk68.Checked = true;
                set68(true);
                txtTRP_68_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_68_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_68_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_68_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_68_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_68_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "70")
            {
                chk70.Checked = true;
                set70(true);
                txtTRP_70_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_70_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "71")
            {
                chk71.Checked = true;
                set71(true);
                txtTRP_71_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_71_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_71_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_71_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_71_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_71_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "72")
            {
                chk72.Checked = true;
                set72(true);
                txtTRP_72_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTIS_72_1.Text = dt.Rows[intI]["TIS_1"].ToString();
            }

            if (dt.Rows[intI]["Band"].ToString() == "74")
            {
                chk74.Checked = true;
                set74(true);
                txtTRP_74_1.Text = dt.Rows[intI]["TRP_1"].ToString();
                txtTRP_74_2.Text = dt.Rows[intI]["TRP_2"].ToString();
                txtTRP_74_3.Text = dt.Rows[intI]["TRP_3"].ToString();
                txtTIS_74_1.Text = dt.Rows[intI]["TIS_1"].ToString();
                txtTIS_74_2.Text = dt.Rows[intI]["TIS_2"].ToString();
                txtTIS_74_3.Text = dt.Rows[intI]["TIS_3"].ToString();
            }






        }

    }


}
