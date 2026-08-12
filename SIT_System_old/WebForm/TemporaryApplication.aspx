<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="TemporaryApplication.aspx.cs" Inherits="WebForm_TemporaryApplication" Title="暫存申請單" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <font face="verdana"color="0000DD"size="4" ><legend>暫存申請單</legend></font>
    <fieldset>
        <table id="Table1" class="one" width="100%">
            <tr>
                <asp:GridView ID="gvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BorderWidth="1px" CellPadding="3" DataKeyNames="Name" ForeColor="#333333" HorizontalAlign="Center"
                    Width="95%" OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvwList_RowDataBound">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="申請單編號" ReadOnly="True" SortExpression="ID">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="專案名稱" ReadOnly="True" SortExpression="Name">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Kind" HeaderText="專案類別" ReadOnly="True" SortExpression="Kind">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="A_Department" HeaderText="申請部門" ReadOnly="True" SortExpression="A_Department">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="A_Name" HeaderText="申請人" ReadOnly="True" SortExpression="A_Name">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </tr>
        </table> 
    </fieldset>  
        
</asp:Content>

