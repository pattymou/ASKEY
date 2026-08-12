<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddInfo.aspx.cs" Inherits="WebForm_AddInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<asp:ScriptManager ID="ScriptManager1" Runat="Server" />--%>
    <link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
  <script src="../js/jquery-1.10.4.min.js"></script>
  
      <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../css/jquery.fileupload.css" />
    <link rel="stylesheet" href="../css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-noscript.css" /></noscript>
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-ui-noscript.css" /></noscript>  
  
  
  <script>
      $(function() {
          $("#tabs").tabs();
      });
  </script>
  
  <script>
      // Initialize the jQuery UI theme switcher:
    $('#theme-switcher1').change(function() {
        var theme = $('#theme');
        theme.prop(
        'href',
        theme.prop('href').replace(
            /[\w\-]+\/jquery-ui.css/,
            $(this).val() + '/jquery-ui.min.css'
        )
    );
    });
    $('#theme-switcher2').change(function() {
        var theme = $('#theme');
        theme.prop(
        'href',
        theme.prop('href').replace(
            /[\w\-]+\/jquery-ui.css/,
            $(this).val() + '/jquery-ui.min.css'
        )
    );
    });
  </script>
  
  
  
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>項目列表</legend></font>

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">各項參數設定</a></li>
    <li><a href="#tabs-2">系統項目設定</a></li>
    <li><a href="#tabs-3">申請單測試項目設定</a></li>
    <li><a href="#tabs-4">各項辦法及系統說明</a></li>
    <li><a href="#tabs-5">模組化網頁設定</a></li>
    <li><a href="#tabs-6">自動化程式相關設定</a></li>
  </ul>
  <div id="tabs-1">
        <table>
            <tr>
                <td>
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo1" runat="server" Text="Customer" GroupName="1" 
                        oncheckedchanged="rdoInfo1_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo2" runat="server" Text="NPI" GroupName="1" 
                        oncheckedchanged="rdoInfo2_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo3" runat="server" Text="部門" GroupName="1" 
                        oncheckedchanged="rdoInfo3_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo4" runat="server" Text="Team" GroupName="1" 
                        oncheckedchanged="rdoInfo4_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo5" runat="server" Text="職稱" GroupName="1" 
                        oncheckedchanged="rdoInfo5_CheckedChanged" AutoPostBack="True" />
                        
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo6" runat="server" Text="工作類別" GroupName="1" 
                        oncheckedchanged="rdoInfo6_CheckedChanged" AutoPostBack="True" />  
                        
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo7" runat="server" Text="設備類別" GroupName="1" 
                        oncheckedchanged="rdoInfo7_CheckedChanged" AutoPostBack="True" />   

                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo8" runat="server" Text="TestCase類別" GroupName="1" 
                        oncheckedchanged="rdoInfo8_CheckedChanged" AutoPostBack="True" />    
                        
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo9" runat="server" Text="機種名稱" GroupName="1" 
                        oncheckedchanged="rdoInfo9_CheckedChanged" AutoPostBack="True" />       
                <br />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoInfo10" runat="server" Text="貨品類別" GroupName="1" 
                        oncheckedchanged="rdoInfo10_CheckedChanged" AutoPostBack="True" />                                                                                        
                </td>
            </tr>
            <tr style="font-size: 9pt">
                <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                        <font face="新細明體" size="2">資訊列表</font></td>
            </tr>   
            <tr>
                <td>
                    
                    <asp:Label ID="lblCustomer" runat="server" Text="客戶："></asp:Label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True"
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>     
            <tr style="font-size: 9pt">
                <td align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>                
                        <asp:GridView ID="gvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BorderWidth="1px" CellPadding="3" DataKeyNames="Name"
                                    ForeColor="#333333" HorizontalAlign="Center" Width="95%" OnRowDeleting="gvList_RowDeleting" OnPageIndexChanging="gvList_PageIndexChanging" OnRowCancelingEdit="gvList_RowCancelingEdit" OnRowEditing="gvList_RowEditing" OnRowUpdating="gvList_RowUpdating" OnRowCreated ="gvList_RowCreated">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                        ImageUrl="~/images/WebForm/icon-save.gif" Text="更新" ValidationGroup="ChgCodeList" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                        ImageUrl="~/images/WebForm/icon-cancel.gif" Text="取消" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                        ImageUrl="~/images/WebForm/icon-edit.gif" Text="編輯" />
                                </ItemTemplate>
                            </asp:TemplateField>                    
                            <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("您確定要刪除此筆資料嗎？");'
                                        Text="刪除" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    &nbsp;
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TestCase" ShowHeader="False">
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" 
	　　                                    ImageUrl="~/images/WebForm/icon-testcase.png" 
	　　                                    NavigateUrl='<%# "AddTestCase.aspx?ID="+Eval("ID")+"&Name="+Eval("Name") %>'></asp:HyperLink> 
                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                    &nbsp;
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名稱" SortExpression="name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>                                    
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="單位主管" SortExpression="Value">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLeader" runat="server" Text='<%# Bind("Value") %>' TextMode="SingleLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLeader" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />                                
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Value" SortExpression="Value" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblLeader1" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                            </ItemTemplate>                                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
    <%--                        <asp:TemplateField HeaderText="名稱" SortExpression="name" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName1" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />                                
                            </asp:TemplateField>--%>                                             
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />                    
                    </asp:GridView>
                    </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="名稱："></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:TextBox ID="txtAdd" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    
                    
                </td>


            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLeader1" runat="server" Text="單位主管："></asp:Label>
                    <asp:TextBox ID="txtLeader1" runat="server"></asp:TextBox>
                </td>            
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="新增" onclick="btnAdd_Click" />
                </td>            
            </tr>
        </table> 
    </div>
    <div id="tabs-2">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="設備預約管理部門："></asp:Label>
                    <asp:DropDownList ID="ddlApparatusD" runat="server">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label1" runat="server" Text="設備預約管理人："></asp:Label>
                    <asp:DropDownList ID="ddlApparatusMaster" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label12" runat="server" Text="台北 - 設備預約代理人："></asp:Label>
                    <asp:DropDownList ID="ddlApparatusMaster1" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>  
            <tr>
                <td>

                    <asp:Label ID="Label35" runat="server" Text="吳江 - 設備預約代理人："></asp:Label>
                    <asp:DropDownList ID="ddlApparatusMaster2" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>          
<%--            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="設備預約管理部門："></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>                
                </td>
            </tr>--%>
            <tr>
                <td>

                    <asp:Label ID="Label7" runat="server" Text="台北-貨品預約/請購管理人："></asp:Label>
                    <asp:DropDownList ID="ddlGoods" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr> 
            <tr>
                <td>

                    <asp:Label ID="Label33" runat="server" Text="吳江-貨品預約/請購管理人："></asp:Label>
                    <asp:DropDownList ID="ddlGoodsW" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label16" runat="server" Text="台北-樣品預約管理人："></asp:Label>
                    <asp:DropDownList ID="ddlSample" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr> 
            <tr>
                <td>

                    <asp:Label ID="Label34" runat="server" Text="吳江-樣品預約管理人："></asp:Label>
                    <asp:DropDownList ID="ddlSampleW" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label32" runat="server" Text="部門名稱："></asp:Label>
                    <asp:TextBox ID="txtDepartmentName" runat="server"></asp:TextBox>
                    
                </td>
                    
            </tr>          
            <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="上班時間："></asp:Label>
                    <asp:DropDownList ID="ddlHourB" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label13" runat="server" ForeColor="Black" Text="："></asp:Label>
                    
                    <asp:DropDownList ID="ddlMinB" runat="server">
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="下班時間："></asp:Label>
                    <asp:DropDownList ID="ddlHourR" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label4" runat="server" ForeColor="Black" Text="："></asp:Label>
                    
                    <asp:DropDownList ID="ddlMinR" runat="server">
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>            
            
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />

                    <br />
                        <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
<%--                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" /> --%>                         
                    <br />
                    
                </td>
            </tr>            
        </table> 
    
    </div> 
    <div id="tabs-3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional">
                <ContentTemplate>    
        <table>
            <tr style="font-size: 9pt">
                <td align="center">
                
            <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="Kind" HeaderText="" ReadOnly="True" SortExpression="Kind">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="Name" HeaderText="" ReadOnly="True" SortExpression="Name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="Item" HeaderText="" ReadOnly="True" SortExpression="Item">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                                                                                               
                                                              

                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
<%--                    </ContentTemplate>
                        </asp:UpdatePanel>--%>
                </td>
            </tr> 
            
            <tr>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="部門："></asp:Label>
                    <asp:DropDownList ID="ddlDepartment_T" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlDepartment_T_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoNPI" runat="server" Text="一般驗證申請單" GroupName="3" 
                       oncheckedchanged="rdoNPI_CheckedChanged" AutoPostBack="True" />
                    <asp:RadioButton ID="rdoNPI1" runat="server" Text="NPI驗證申請單" GroupName="3" 
                       oncheckedchanged="rdoNPI1_CheckedChanged" AutoPostBack="True" />
                </td>

            </tr>
            <tr>   
                <td>
                <br />
                </td> 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="類別："></asp:Label>
                    <asp:DropDownList ID="ddlKind" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp
                    <asp:Button ID="btnDKind" runat="server" Text="刪除" onclick="btnDKind_Click" />
                    &nbsp;&nbsp
                    <asp:Button ID="btnHKind" runat="server" Text="隱藏" onclick="btnHKind_Click" AutoPostBack="True" />
                    &nbsp;&nbsp;&nbsp;&nbsp
                    <asp:Label ID="Label10" runat="server" Text="負責Team："></asp:Label>
                    <asp:DropDownList ID="ddlTeam" runat="server">
                    </asp:DropDownList>       
                    &nbsp;&nbsp
                    <asp:Button ID="btnTeam" runat="server" Text="修改" onclick="btnTeam_Click" />             
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:TextBox ID="txtKind" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAKind" runat="server" Text="新增" onclick="btnAKind_Click" />                    
                </td>
            </tr> 
            <tr>   
                <td>
                <br />
                </td> 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Function："></asp:Label>
                    <asp:DropDownList ID="ddlKind1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind1_SelectedIndexChanged">
                    </asp:DropDownList>
<%--                    <asp:DropDownList ID="ddlFunction" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlFunction" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;                   
                    <asp:Button ID="btnDFunction" runat="server" Text="刪除" onclick="btnDFunction_Click" />
                    &nbsp;&nbsp&nbsp;
                    <asp:Button ID="btnHFunction" runat="server" Text="隱藏" onclick="btnHFunction_Click" />
                    &nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="txtEFunction" runat="server"></asp:TextBox>
                     &nbsp;&nbsp;
                     <asp:Button ID="btnEFunction" runat="server" Text="修改" onclick="btnEFunction_Click" />
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKind2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind2_SelectedIndexChanged">
                    </asp:DropDownList>                    
                    <asp:TextBox ID="txtFunction" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAFunction" runat="server" Text="新增" onclick="btnAFunction_Click" />                    
                    
                </td>
            </tr>
            <tr>   
                <td>
                <br />
                </td> 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="項目："></asp:Label>
                    <asp:DropDownList ID="ddlKind3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind3_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction1_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp                                     
                    <asp:Button ID="btnDItem" runat="server" Text="刪除" onclick="btnDItem_Click" /> 
                    &nbsp;&nbsp
                    <asp:Button ID="btnHItem" runat="server" Text="隱藏" onclick="btnHItem_Click" />                   
                    &nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="txtEItem" runat="server"></asp:TextBox>
                     &nbsp;&nbsp;
                     <asp:Button ID="btnEItem" runat="server" Text="修改" onclick="btnEItem_Click" />
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKind4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind4_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:DropDownList ID="ddlFunction2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction2_SelectedIndexChanged">
                    </asp:DropDownList>                                                            
                    <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    
                    <asp:Button ID="btnAItem" runat="server" Text="新增" onclick="btnAItem_Click" />                    
                </td>
            </tr> 
            <tr>   
                <td>
                <br />
                </td> 
            </tr>
           <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="EV to DV NPI："></asp:Label>
                    <asp:DropDownList ID="ddlKind5" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind5_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction3_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem1_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlL1" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Option</asp:ListItem>
                        <asp:ListItem>Mandatory</asp:ListItem>
                        
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    <asp:Button ID="btnNPIL1" runat="server" Text="更新" onclick="btnNPIL1_Click" />
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="DV to PV NPI："></asp:Label>
                    <asp:DropDownList ID="ddlKind6" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind6_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction4_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem2_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlL2" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Option</asp:ListItem>
                        <asp:ListItem>Mandatory</asp:ListItem>
                        
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    <asp:Button ID="btnNPIL2" runat="server" Text="更新" onclick="btnNPIL2_Click" />
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label30" runat="server" Text="Lead Time："></asp:Label>
                    <asp:DropDownList ID="ddlKind8" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind8_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction6" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction6_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem4_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCost" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp 
                    <asp:Button ID="btnCost" runat="server" Text="更新" onclick="btnCost_Click" />

                </td>
            </tr>
            <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="測試內容說明："></asp:Label>
                    <asp:DropDownList ID="ddlKind7" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind7_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction5" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction5_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem3_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="578px"></asp:TextBox>  
                    &nbsp;&nbsp 
                    <asp:Button ID="btnNote" runat="server" Text="更新" onclick="btnNote_Click" />
                    
                </td>

                 
            </tr>
            <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="測試環境架構："></asp:Label>
                    <asp:DropDownList ID="ddlFileK" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileK_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFileF" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileF_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlFileI" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileI_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="檔案名稱："></asp:Label>                                    
                    <asp:Label ID="lblFileN" runat="server"></asp:Label>
                </td>            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="(限上傳一個檔案)" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table> 
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlKind" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDFunction" EventName="Click" />

                </Triggers>                    

        </asp:UpdatePanel>
        <table >
            <tr>
                <td>
                    <form style="visibility :hidden">
                        <label style="visibility :hidden" for="theme-switcher">Theme:</label>
                        <select style="visibility :hidden" id="theme-switcher" class="pull-right" >
                            <option value="dark-hive" selected>Dark Hive</option>
                        </select>
                    </form>
                    <form id="fileupload" action="UploadProgress.aspx" method="POST" enctype="multipart/form-data">

                        <div class="row fileupload-buttonbar">
                            <div id = "id1" class="col-lg-7">
                                <!-- The fileinput-button span is used to style the file input field as button -->
                                <span class="btn btn-success fileinput-button">
                                    <i class="glyphicon glyphicon-plus"></i>
                                    <span>選擇檔案</span>
                                    <input type="file" name="files[]" multiple>
                                
                                </span>
                            </div>
                            <!-- The global progress state -->
                        <div class="fileupload-progress fade" style="display:none">
                            <!-- The global progress bar -->
                            <div class="progress" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                            <!-- The extended global progress state -->
                            <div class="progress-extended">&nbsp;</div>
                        </div>
                        </div>
                        <!-- The table listing the files available for upload/download -->
                        <table role="presentation" class="table table-striped"><tbody class="files"></tbody></table>
                               
                    </form> 
                                   
                    <asp:Button ID="btnMFile" runat="server" Text="更新" onclick="btnMFile_Click" />
                </td>
            </tr>              
           </table>
               <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode ="Conditional">
            <ContentTemplate>              
           <table>

           <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>
           <tr>
                <td>

                    <asp:Label ID="Label20" runat="server" Text="文件下載："></asp:Label>
                    <asp:DropDownList ID="ddlFileK1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileK1_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFileF1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileF1_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlFileI1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileI1_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    
    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="檔案名稱："></asp:Label>                                    
                    <asp:Label ID="lblFileN1" runat="server"></asp:Label>
                </td>            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="(限上傳一個檔案)" ForeColor="Red"></asp:Label>
                </td>
            </tr>

        </table> 
                            </ContentTemplate>
                    
            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDepartment_T" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlKind" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDFunction" EventName="Click" />

                </Triggers>
        </asp:UpdatePanel>
        <table >
            <tr>
                <td>
                    <form style="visibility :hidden">
                        <label style="visibility :hidden" for="theme-switcher2">Theme:</label>
                        <select style="visibility :hidden" id="theme-switcher2" class="pull-right" >
                            <option value="dark-hive" selected>Dark Hive</option>
                        </select>
                    </form>
                    <form id="fileupload2" action="UploadProgress.aspx" method="POST" enctype="multipart/form-data">

                        <div class="row fileupload-buttonbar">
                            <div id = "Div2" class="col-lg-7">
                                <!-- The fileinput-button span is used to style the file input field as button -->
                                <span class="btn btn-success fileinput-button">
                                    <i class="glyphicon glyphicon-plus"></i>
                                    <span>選擇檔案</span>
                                    <input type="file" name="files[]" multiple>
                                
                                </span>
                            </div>
                            <!-- The global progress state -->
                        <div class="fileupload-progress fade" style="display:none">
                            <!-- The global progress bar -->
                            <div class="progress" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                            <!-- The extended global progress state -->
                            <div class="progress-extended">&nbsp;</div>
                        </div>
                        </div>
                        <!-- The table listing the files available for upload/download -->
                        <table role="presentation" class="table table-striped"><tbody class="files"></tbody></table>
                               
                    </form> 
                                   
                    <asp:Button ID="btnMFile1" runat="server" Text="更新" onclick="btnMFile1_Click" />
                </td>
            </tr>              
           </table>                                                    
    </div>
    <div id="tabs-4">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode ="Conditional">
            <ContentTemplate>    
        <table>
             
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="類別："></asp:Label>
                    <asp:DropDownList ID="ddlKind_E" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind_E_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp
                    <asp:Button ID="btnDKind_E" runat="server" Text="刪除" onclick="btnDKind_E_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:TextBox ID="txtKind_E" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAKind_E" runat="server" Text="新增" onclick="btnAKind_E_Click" />                    
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="項目："></asp:Label>
                    <asp:DropDownList ID="ddlKind1_E" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind1_E_SelectedIndexChanged">
                    </asp:DropDownList>
<%--                    <asp:DropDownList ID="ddlFunction" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlItem_E" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;                   
                    <asp:Button ID="btnDItem_E" runat="server" Text="刪除" onclick="btnDItem_E_Click" />
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKind2_E" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind2_E_SelectedIndexChanged">
                    </asp:DropDownList>                    
                    <asp:TextBox ID="txtItem_E" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAItem_E" runat="server" Text="新增" onclick="btnAItem_E_Click" />                    
                    
                </td>
            </tr>
             
           
            <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="項目："></asp:Label>
                    <asp:DropDownList ID="ddlFileK_E" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileK_E_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFileI_E" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileI_E_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="檔案名稱："></asp:Label>                                    
                    <asp:Label ID="lblFileN_E" runat="server"></asp:Label>
                </td>            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="(限上傳一個檔案)" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table> 
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlKind_E" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDItem_E" EventName="Click" />

                </Triggers>                    

        </asp:UpdatePanel>
        <table >
            <tr>
                <td>
                    <form style="visibility :hidden">
                        <label style="visibility :hidden" for="theme-switcher1">Theme:</label>
                        <select style="visibility :hidden" id="theme-switcher1" class="pull-right" >
                            <option value="dark-hive" selected>Dark Hive</option>
                        </select>
                    </form>
                    <form id="fileupload1" action="UploadProgress.aspx" method="POST" enctype="multipart/form-data">

                        <div class="row fileupload-buttonbar">
                            <div id = "Div1" class="col-lg-7">
                                <!-- The fileinput-button span is used to style the file input field as button -->
                                <span class="btn btn-success fileinput-button">
                                    <i class="glyphicon glyphicon-plus"></i>
                                    <span>選擇檔案</span>
                                    <input type="file" name="files[]" multiple>
                                
                                </span>
                            </div>
                            <!-- The global progress state -->
                        <div class="fileupload-progress fade" style="display:none">
                            <!-- The global progress bar -->
                            <div class="progress" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                            <!-- The extended global progress state -->
                            <div class="progress-extended">&nbsp;</div>
                        </div>
                        </div>
                        <!-- The table listing the files available for upload/download -->
                        <table role="presentation" class="table table-striped"><tbody class="files"></tbody></table>
                               
                    </form> 
                                   
                    <asp:Button ID="btnMFile_E" runat="server" Text="更新" onclick="btnMFile_E_Click" />
                </td>
            </tr>              
           </table>
    </div>  
    <div id="tabs-5">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="模組化網頁："></asp:Label>    
                    <asp:DropDownList ID="ddlModel" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp
                    <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAddModel" runat="server" Text="新增" 
                        onclick="btnAddModel_Click" />                    
                </td>
            </tr>
            
        </table>
    </div> 
    <div id="tabs-6">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label36" runat="server" Text="Los負責人："></asp:Label>
                    <asp:DropDownList ID="ddlLos" runat="server">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label37" runat="server" Text="Veriwave負責人："></asp:Label>
                    <asp:DropDownList ID="ddlVeriwave" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label38" runat="server" Text="Octoscope負責人："></asp:Label>
                    <asp:DropDownList ID="ddlOctoscope" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>  
            <tr>
                <td>

                    <asp:Label ID="Label39" runat="server" Text="Mesh負責人："></asp:Label>
                    <asp:DropDownList ID="ddlMesh" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr>          

            <tr>
                <td>

                    <asp:Label ID="Label40" runat="server" Text="AP Coverage負責人："></asp:Label>
                    <asp:DropDownList ID="ddlAP" runat="server">
                    </asp:DropDownList>
                </td>
                    
            </tr> 
                      
                        
            
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />

                    <br />
                        <asp:Button ID="btnOK_Auto" runat="server" Text="確定" onclick="btnOK_Auto_Click" />
<%--                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" /> --%>                         
                    <br />
                    
                </td>
            </tr>            
        </table> 
    
    </div>
   </div>            
</fieldset> 


<script id="template-upload" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-upload fade">
        <td>
            <span class="preview"></span>
        </td>
        <td>
            <p class="name">{%=file.name%}</p>
            <strong class="error"></strong>
        </td>
        <td>
            <p class="size">Processing...</p>
            <div class="progress"></div>
        </td>
        <td>
            {% if (!i && !o.options.autoUpload) { %}
                <button class="start" disabled>Start</button>
            {% } %}
            {% if (!i) { %}
                <button class="cancel">Cancel</button>
            {% } %}
        </td>
    </tr>
{% } %}
</script>
<!-- The template to display files available for download -->
<script id="template-download" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-download fade">
        <td>
            <span class="preview">
                {% if (file.thumbnailUrl) { %}
                    <a href="{%=file.url%}" title="{%=file.name%}" download="{%=file.name%}" data-gallery><img src="{%=file.thumbnailUrl%}"></a>
                {% } %}
            </span>
        </td>
        <td>
            <p class="name">
                <a href="{%=file.url%}" title="{%=file.name%}" download="{%=file.name%}" {%=file.thumbnailUrl?'data-gallery':''%}>{%=file.name%}</a>
            </p>
            {% if (file.error) { %}
                <div><span class="error">Error</span> {%=file.error%}</div>
            {% } %}
        </td>
        <td>
            <span class="size">{%=o.formatFileSize(file.size)%}</span>
        </td>
        <td>
            <button class="delete" data-type="{%=file.deleteType%}" data-url="{%=file.deleteUrl%}"{% if (file.deleteWithCredentials) { %} data-xhr-fields='{"withCredentials":true}'{% } %}>Delete</button>
            <input type="checkbox" name="delete" value="1" class="toggle">
        </td>
    </tr>
{% } %}
</script>   

<script src="../js/jquery.min.js"></script>
<script src="../js/jquery-ui.min.js"></script>
<!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
<!-- jquery.ui.widget.js 不能省略，否則已上傳及要上傳之清單會看不到-->
<script src="../js/vendor/jquery.ui.widget.js"></script>
<!-- The Templates plugin is included to render the upload/download listings -->
<script src="../js/tmpl.min.js"></script>
<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
<script src="../js/load-image.all.min.js"></script>
<!-- The Canvas to Blob plugin is included for image resizing functionality -->
<script src="../js/canvas-to-blob.min.js"></script>
<!-- blueimp Gallery script -->
<script src="../js/jquery.blueimp-gallery.min.js"></script>
<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
<script src="../js/jquery.iframe-transport.js"></script>
<!-- The basic File Upload plugin -->
<script src="../js/jquery.fileupload.js"></script>
<!-- The File Upload processing plugin -->
<script src="../js/jquery.fileupload-process.js"></script>
<!-- The File Upload image preview & resize plugin -->
<script src="../js/jquery.fileupload-image.js"></script>
<!-- The File Upload audio preview plugin -->
<script src="../js/jquery.fileupload-audio.js"></script>
<!-- The File Upload video preview plugin -->
<script src="../js/jquery.fileupload-video.js"></script>
<!-- The File Upload validation plugin -->
<script src="../js/jquery.fileupload-validate.js"></script>
<!-- The File Upload user interface plugin -->
<script src="../js/jquery.fileupload-ui.js"></script>
<!-- The File Upload jQuery UI plugin -->
<script src="../js/jquery.fileupload-jquery-ui.js"></script>
<script>    var fileuploadurl = "UploadProgress.aspx";</script>
<!-- The main application script -->
<script src="../js/main.js"></script>

</asp:Content>

