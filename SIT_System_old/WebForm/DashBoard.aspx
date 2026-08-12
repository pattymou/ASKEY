<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="WebForm_DashBoard" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <fieldset>
        <table id="Table2" class="one" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Size="XX-Large" 
                        ForeColor="#3333FF"></asp:Label>        
                </td>
            </tr>
            
        </table> 
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333"
                        Width="100%" AllowPaging="True" 
                OnPageIndexChanging="gvwMain_PageIndexChanging" 
                OnPreRender ="gvwMain_PreRender" PageSize="20">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="專案名稱" ReadOnly="True" SortExpression="Name">
                        <ControlStyle Width="30px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="Kind" HeaderText="" ReadOnly="True" SortExpression="Kind">
                        <ControlStyle Width="30px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>                    
                    <asp:TemplateField HeaderText="任務名稱" SortExpression="CaseName">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemTemplate>
                            &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardDetail.aspx?PID="+Eval("PID")+"&CID="+Eval("CID") %>'
                                Target="_blank" Text='<%# Bind("CaseName") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="seq" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("PID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="seq" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblGVSeq1" runat="server" Text='<%# Bind("CID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                       
                   
                </Columns>
                <RowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </table> 
    </fieldset> 
</asp:Content>

