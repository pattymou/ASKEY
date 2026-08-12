<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ExplanationView.aspx.cs" Inherits="WebForm_ExplanationView" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional">
            <ContentTemplate> 
            <table id="Table2" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">

                <tr>
                    <td align ="center">
                                            
                        <asp:GridView ID="gvwMain" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                            Width="100%" OnPageIndexChanging="gvwMain_PageIndexChanging" 
                            OnPreRender ="gvwMain_PreRender">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>                 
                                <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="Name" HeaderText="功能" ReadOnly="True" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Item" HeaderText="項目" ReadOnly="True" SortExpression="Item">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                        
                                <asp:TemplateField HeaderText="項目說明" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                            Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>        
                                 
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
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
            </ContentTemplate>
                    

        </asp:UpdatePanel>
</asp:Content>

