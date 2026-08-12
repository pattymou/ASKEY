<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationReport.aspx.cs" Inherits="WebForm_ApplicationReport" Title="" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table3" class="one" width="100%" >
            <tr>
                <td colspan =2>    
                    <asp:RadioButton ID="rdoDate" runat="server" GroupName="2" Text="日期區間：" />
                    <%--<asp:Label ID="Label1" runat="server" Text="日期區間："></asp:Label>--%>
                    <asp:Label ID="Label6" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearS" runat="server" Width="75px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonthS" runat="server">
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
                    <asp:Label ID="Label2" runat="server" Text="月～(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearE" runat="server"　Width="75px"></asp:TextBox>
                    
                    <asp:Label ID="Label4" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonthE" runat="server">
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
                    <asp:Label ID="Label5" runat="server" Text="月"></asp:Label>     

                    
                       
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoAll" runat="server" GroupName="2" Text="部門人員" />
                    
                    <asp:DropDownList ID="ddlDepartmentP" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>        
            <tr>
                <td>
                    <asp:RadioButton ID="rdoApplicationID" runat="server" GroupName="2" Text="申請單號" />
                    <asp:TextBox ID="txtApplication_ID" runat="server"　Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoApplicationName" runat="server" GroupName="2" Text="機種名稱" />
                    <asp:TextBox ID="txtApplicationi_Name" runat="server"　Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="butOK" runat="server" Text="搜尋" 
                            onclick="butOK_Click" />
                </td>
            </tr>
        </table>
        <table id="Table2" border="0" cellpadding="5" cellspacing="5" width="100%">
            <tr>
                <td align ="center">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDataBound="gvwMain_RowDataBound" OnPreRender ="gvwMain_PreRender">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="申請單號" ReadOnly="True" SortExpression="ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="Application_Date" HeaderText="申請日期" ReadOnly="True" SortExpression="Application_Date">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="customer" HeaderText="客戶代碼" ReadOnly="True" SortExpression="customer">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="NPI" HeaderText="NPI" ReadOnly="True" SortExpression="NPI">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>                    
                            <asp:TemplateField HeaderText="機種名稱" SortExpression="Name">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ApplicationDefault.aspx?PID="+Eval("ID") %>'
                                        Target="_blank" Text='<%# Bind("Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="測試報告" SortExpression="file_tag">
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                        Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jira" SortExpression="file_tag">
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("Jira") %>'
                                        Target="_blank" Text='<%# Eval("Jira") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            
                           
                        </Columns>
                        <RowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table> 
                    
    </fieldset>

</asp:Content>

