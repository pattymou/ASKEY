<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="WebForm_AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function SelectAllCheckboxes(spanChk)
    {
        elm=document.forms[0];
        for(i=0;i<= elm.length -1;i++)
        {
            if(elm[i].type=="checkbox" && elm[i].id!=spanChk.id)
            {
                if(elm.elements[i].checked!=spanChk.checked)
                    elm.elements[i].click();
            }
        }
    }
</script>

    <fieldset>
        <font face="verdana"color="0000DD"size="4" ><legend>新增使用者</legend></font>
        <hr size="5" width="100%" color="DDDDDD" style="height: 5px">
    
             
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        
<%--            <tr>
                <td align ="right">
                    <asp:Label ID="Label1" runat="server" Text="工號："></asp:Label>
                </td>
                <td style="COLOR: red">
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>                
                </td>
                
            </tr>--%>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label2" runat="server" Text="登入名稱："></asp:Label>
                
                </td>
                <td>
                
                    <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                    <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                    
                    <asp:Label ID="Label3" runat="server" Text="密碼："></asp:Label>
                    
                </td>
                <td>
                
                    <asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                    
                    <asp:Label ID="Label4" runat="server" Text="確認密碼："></asp:Label>
                    
                </td>
                <td>
                
                    <asp:TextBox ID="txtPwd_C" runat="server"></asp:TextBox>
                    <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                    
                    <asp:Label ID="Label5" runat="server" Text="姓名："></asp:Label>
                    
                </td>
                <td>
                
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:Label ID="Label16" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <%--<tr>
                <td align ="right">
                
                    <asp:Label ID="Label6" runat="server" Text="部門："></asp:Label>
                
                </td>
                <td style="COLOR: red">
                
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>*
                
                </td>
            </tr>--%>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label14" runat="server" Text="部門："></asp:Label>
                
                </td>
                <td>
                
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label17" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                    <asp:CheckBox ID="chkManager" runat="server" Text="部門最高主管" />
                
                </td>
            </tr>            
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label11" runat="server" Text="Team："></asp:Label>
                
                </td>
                <td>
                
                    <asp:DropDownList ID="ddlTeam" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                    <asp:CheckBox ID="chkLeader" runat="server" Text="Leader" />
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label7" runat="server" Text="職稱："></asp:Label>
                
                </td>
                
                <td>
                
                    <asp:DropDownList ID="ddlJob" runat="server">
                    </asp:DropDownList>
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label8" runat="server" Text="分機："></asp:Label>
                
                </td>
                <td>
                
                    <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label9" runat="server" Text="電話："></asp:Label>
                
                </td>
                <td>
                
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                
                </td>
            </tr>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label10" runat="server" Text="地址："></asp:Label>
                
                </td>
                <td>
                
                    <asp:TextBox ID="txtAddress" runat="server" Width="455px"></asp:TextBox>
                
                </td>
            </tr>
            <%--<tr>
                <td align ="right">
                
                    <asp:Label ID="Label11" runat="server" Text="生日："></asp:Label>
                
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlMonth" runat="server">
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
                    </asp:DropDownList>
                    
                    <asp:Label ID="Label14" runat="server" Text="月"></asp:Label>
                    <asp:DropDownList ID="ddalDate" runat="server">
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
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:Label ID="Label12" runat="server" Text="日"></asp:Label>
                    
                </td>

            </tr>--%>
            <tr>
                <td align ="right">
                
                    <asp:Label ID="Label13" runat="server" Text="E-Mail："></asp:Label>
                
                </td>
                <td>
                
                    <asp:TextBox ID="txtMail" runat="server" Width="344px"></asp:TextBox>
                    <asp:Label ID="Label19" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr> 
<%--            <tr>
                <td align ="right">
                    
                    <asp:Label ID="Label6" runat="server" Text="地點："></asp:Label>
                    
                </td>
                <td colspan=2 style="COLOR: red">
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdoAcceptT" runat="server" Text="台北" GroupName="5" 
                        ForeColor="Black" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoAcceptW" runat="server" Text="吳江" GroupName="5" 
                        ForeColor="Black" />&nbsp;&nbsp;&nbsp;*
                </td>
                
            </tr>--%> 
                <td align ="right">
                    <asp:Label ID="Label12" runat="server" Text="編輯權限："></asp:Label>
                </td>
                <td colspan=2>
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdoWrite_Y" runat="server" Text="是" GroupName="3" 
                        ForeColor="Black" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoWrite_N" runat="server" Text="否" GroupName="3" 
                        ForeColor="Black" />&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label20" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>                
            <tr>
                <td align="center" bgcolor="#dfe9f7" style="height: 27px" colspan=2>
                    <font face="新細明體" size="2">權限設定</font></td>
            </tr>
            <tr>
                <td align="center" colspan=2>
                    <font face="新細明體">
                    <asp:GridView ID="gvwMain" runat="server" 
             AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        Width="100%" OnPageIndexChanging="gvwMain_PageIndexChanging" 
             OnPreRender ="gvwMain_PreRender">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField>
<%--                            <headertemplate> 
                                <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="全選/取消" ToolTip="按一次全選，再按一次取消全選" /> 
                            </headertemplate>--%>
                            <itemtemplate> 
                                <asp:CheckBox ID="CheckBox2" runat="server"/> 
                            </itemtemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="PN" HeaderText="" ReadOnly="True" SortExpression="PN">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>                        
                        <asp:TemplateField HeaderText="系統名稱" SortExpression="file_tag">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("LN") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="系統編號" Visible="False">
                            <EditItemTemplate>
                                <asp:Label ID="lblFunction_NoGV" runat="server" Text='<%# Bind("Function_No") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFunction_NoGV" runat="server" Text='<%# Bind("Function_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                        &nbsp; </font>
            </td>
            </tr>
            
            
            
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />
                        *為必填欄位
                    <br />
                    <br />
                        <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />                          
                    <br />
                </td>
            </tr>  
            
                                                         
        </table>

        
    </fieldset> 
</asp:Content>

