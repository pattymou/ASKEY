<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="SearchDashBoard.aspx.cs" Inherits="WebForm_SearchDashBoard" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table3" class="one" width="100%" >
            <tr>
                <td colspan =2>
                    
                    <asp:Label ID="Label30" runat="server" Text="地點："></asp:Label>
                                        <asp:RadioButton ID="rdoLocal" runat="server" GroupName="6" Text="台北" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoLocal1" runat="server" GroupName="6" Text="吳江" />

                </td>
                
            </tr> 
            <tr>
                <td colspan =2>    
                    
                    <asp:Label ID="Label1" runat="server" Text="日期區間："></asp:Label>
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
                <td style="width:120px">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoTeam" runat="server" Text="Team : " GroupName="1" 
                                oncheckedchanged="rdoTeam_CheckedChanged" AutoPostBack="True" />
                <td>
                    <asp:DropDownList ID="ddlTeam" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlTeam_SelectedIndexChanged">
                    </asp:DropDownList>                                
                    <asp:DropDownList ID="ddlEmp" runat="server"> </asp:DropDownList>                                             
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoProject" runat="server" Text="Project  " GroupName="1"  /> 
                                                   
                </td>
                <td>                               
                    <asp:DropDownList ID="ddlProject" runat="server"> </asp:DropDownList>                                             
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
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="名稱" ReadOnly="True" SortExpression="Name">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:TemplateField HeaderText="Open" SortExpression="Open">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoard.aspx?ID="+Eval("Name")+"&Status=Open" %>'
                                        Target="_blank" Text='<%# Bind("Open") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Close" SortExpression="Close">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoard.aspx?ID="+Eval("Name")+"&Status=Close" %>'
                                        Target="_blank" Text='<%# Bind("Close") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Hold" SortExpression="Hold">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoard.aspx?ID="+Eval("Name")+"&Status=Hold" %>'
                                        Target="_blank" Text='<%# Bind("Hold") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Delay" SortExpression="Delay">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoard.aspx?ID="+Eval("Name")+"&Status=Delay" %>'
                                        Target="_blank" Text='<%# Bind("Delay") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>  
                            <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                                                                           
                            <%--<asp:BoundField DataField="Upload_Date" HeaderText="上傳時間" ReadOnly="True" SortExpression="Upload_Date">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>  
                            <asp:BoundField DataField="Upload_Emp" HeaderText="上傳者" ReadOnly="True" SortExpression="Upload_Emp">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>                                                         
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                           
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align =center>
                    
                    <asp:Button ID="btnExcel1" runat="server" Text="將表格匯出至Excel" 
                        onclick="btnExcel1_Click" />                    
                </td>
            </tr>
        </table> 
                    
    </fieldset> 
</asp:Content>

