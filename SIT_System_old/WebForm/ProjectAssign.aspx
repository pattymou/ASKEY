<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProjectAssign.aspx.cs" Inherits="WebForm_ProjectAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <link rel="stylesheet" href="//apps.bdimg.com/libs/jqueryui/1.10.4/css/jquery-ui.min.css">
  <script src="//apps.bdimg.com/libs/jquery/1.10.2/jquery.min.js"></script>
  <script src="//apps.bdimg.com/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>--%>
    <link rel="stylesheet" href="../css/jquery-ui.min.css">

    <script src="../js/jquery_1.11.0.min.js"></script>

    <script>
        $(function() {
            $("#tabs1").tabs();
            $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
            $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
        });
    </script>

    <style>
        table.one
        {
            table-layout: automatic;
        }
    </style>
    <fieldset>
        <asp:Label ID="lblID" runat="server" ForeColor="#3333FF" Font-Bold="True"></asp:Label>
        <table id="Table1" class="one" style="border: 1px solid" cellpadding="5" cellspacing="5"
            frame="border" rules="all" width="100%">
            <tr>
                <td width="25%">
                    <asp:Label ID="Label7" runat="server" Text="申請人"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblName" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="Label10" runat="server" Text="部門"></asp:Label>
                </td>
                <td width="25%">
                    <%--<asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>--%>
                    <asp:Label ID="lblDepartment" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="分機"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExt" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Mail"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMail" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>                
                <td>
                    <asp:Label ID="Label16" runat="server" Text="機種所屬Sub-PU"></asp:Label>
                </td>
                <td>                 
                    <asp:Label ID="lblDepartment2" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="DQA負責人"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDQA" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="客戶"></asp:Label>
                </td>
                <td>
                    <%--<asp:DropDownList ID="ddlCustomer" runat="server">
                </asp:DropDownList>--%>
                    <asp:Label ID="lblCustomer" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="PM Sales"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPM" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="S/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="H/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Mechanical Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMechanical" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="DSP Model"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDSP" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="F/W Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Wireless Drive"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWireless" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Customer's Product Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProduct" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="NPI"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNPI" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="H/W Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHW_VR" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="Chipset"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblChipset" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="Sample MAC Address"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMAC" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="Utility Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUtility" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="預計Sample Ready日期"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblReady" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="期望完成日"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExpect" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr id ="Certification" runat ="server">
                <td>
                    <asp:Label ID="Label18" runat="server" Text="認証申請單"></asp:Label>
                </td>
                <td>
                    <asp:LinkButton ID="linkCertification_Wifi" runat="server" OnClick="lbtnWifi_Click">WIFI申請單連結</asp:LinkButton>
                    <asp:LinkButton ID="linkCertification_BT" runat="server" OnClick="lbtnBT_Click">BT申請單連結</asp:LinkButton>
                    <asp:LinkButton ID="linkCertification_GCF" runat="server" OnClick="lbtnGCF_Click">GCF申請單連結</asp:LinkButton>
                    <asp:LinkButton ID="linkCertification_PTCRB" runat="server" OnClick="lbtnPTCRB_Click">PTCRB申請單連結</asp:LinkButton>
                </td>
            </tr>
            <tr id="LTE" runat ="server">
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Application form for LTE TRP/TIS"></asp:Label>
                </td>
                <td colspan=3>
                    <asp:LinkButton ID="linkForm" runat="server" OnClick="lbtnForm_Click">LTE TRP/TIS連結</asp:LinkButton>
                </td>
                       
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="TestCase"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTestCase" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="579px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label35" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="578px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="附加檔案"></asp:Label>
                </td>
                <td align="center" colspan="5" colspan="3">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="1"
                        ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                        Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <%--                        <asp:TemplateField HeaderText="seq" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:RadioButton ID="rdoAccpt" runat="server" Text="Accept" GroupName="1" />
                    <%--<asp:RadioButton ID="rdoAssign" runat="server" Text="Assign : " GroupName="1" /> 
                <asp:DropDownList ID="ddlAssign" runat="server">
                </asp:DropDownList> 
                &nbsp;&nbsp;
                <asp:Label ID="Label12" runat="server" Text="預計完成日："></asp:Label>
                    
                <input type="text" id="datepicker" name = "date1">
                
                 <script>
                     $(function() {
                         $("#datepicker").datepicker();
                     });
                
                 </script>--%>
                </td>
                <td align="center" colspan="2">
                    <asp:RadioButton ID="rdoReject" runat="server" Text="Reject" GroupName="1" />
                </td>
            </tr>
        </table>
        <tr>
            <td align="center" colspan="2" style="color: red">
                <br />
                <br />
                <asp:Button ID="butOK" runat="server" Text="確定" OnClick="butOK_Click" />
                <br />
                <br />
            </td>
        </tr>
        <%--<div id="tabs1">
      <ul>
        <li><a href="#tabs-1">Nunc tincidunt</a></li>
        <li><a href="#tabs-2">Proin dolor</a></li>
        <li><a href="#tabs-3">Aenean lacinia</a></li>
      </ul>
      <div id="tabs-1">
            <div id="tabs">
              <ul>
                <li><a href="#tabs-a">Nunc tincidunt</a></li>
                <li><a href="#tabs-b">Proin dolor</a></li>
                <li><a href="#tabs-c">Aenean lacinia</a></li>
              </ul>
              <div id="tabs-a">
                <h2>内容标题 1</h2>
                <p>Proin elit arcu, rutrum commodo, vehicula tempus, commodo a, risus. Curabitur nec arcu. Donec sollicitudin mi sit amet mauris. Nam elementum quam ullamcorper ante. Etiam aliquet massa et lorem. Mauris dapibus lacus auctor risus. Aenean tempor ullamcorper leo. Vivamus sed magna quis ligula eleifend adipiscing. Duis orci. Aliquam sodales tortor vitae ipsum. Aliquam nulla. Duis aliquam molestie erat. Ut et mauris vel pede varius sollicitudin. Sed ut dolor nec orci tincidunt interdum. Phasellus ipsum. Nunc tristique tempus lectus.</p>
              </div>
              <div id="tabs-b">
                <h2>内容标题 2</h2>
                <p>Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p>
              </div>
              <div id="tabs-c">
                <h2>内容标题 3</h2>
                <p>Mauris eleifend est et turpis. Duis id erat. Suspendisse potenti. Aliquam vulputate, pede vel vehicula accumsan, mi neque rutrum erat, eu congue orci lorem eget lorem. Vestibulum non ante. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce sodales. Quisque eu urna vel enim commodo pellentesque. Praesent eu risus hendrerit ligula tempus pretium. Curabitur lorem enim, pretium nec, feugiat nec, luctus a, lacus.</p>
                <p>Duis cursus. Maecenas ligula eros, blandit nec, pharetra at, semper at, magna. Nullam ac lacus. Nulla facilisi. Praesent viverra justo vitae neque. Praesent blandit adipiscing velit. Suspendisse potenti. Donec mattis, pede vel pharetra blandit, magna ligula faucibus eros, id euismod lacus dolor eget odio. Nam scelerisque. Donec non libero sed nulla mattis commodo. Ut sagittis. Donec nisi lectus, feugiat porttitor, tempor ac, tempor vitae, pede. Aenean vehicula velit eu tellus interdum rutrum. Maecenas commodo. Pellentesque nec elit. Fusce in lacus. Vivamus a libero vitae lectus hendrerit hendrerit.</p>
              </div>
            </div>        
      </div>
      <div id="tabs-2">
        <p>Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p>
      </div>
      <div id="tabs-3">
        <p>Mauris eleifend est et turpis. Duis id erat. Suspendisse potenti. Aliquam vulputate, pede vel vehicula accumsan, mi neque rutrum erat, eu congue orci lorem eget lorem. Vestibulum non ante. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce sodales. Quisque eu urna vel enim commodo pellentesque. Praesent eu risus hendrerit ligula tempus pretium. Curabitur lorem enim, pretium nec, feugiat nec, luctus a, lacus.</p>
        <p>Duis cursus. Maecenas ligula eros, blandit nec, pharetra at, semper at, magna. Nullam ac lacus. Nulla facilisi. Praesent viverra justo vitae neque. Praesent blandit adipiscing velit. Suspendisse potenti. Donec mattis, pede vel pharetra blandit, magna ligula faucibus eros, id euismod lacus dolor eget odio. Nam scelerisque. Donec non libero sed nulla mattis commodo. Ut sagittis. Donec nisi lectus, feugiat porttitor, tempor ac, tempor vitae, pede. Aenean vehicula velit eu tellus interdum rutrum. Maecenas commodo. Pellentesque nec elit. Fusce in lacus. Vivamus a libero vitae lectus hendrerit hendrerit.</p>
      </div>
    </div> --%>
    </fieldset>
</asp:Content>
