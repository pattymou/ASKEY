<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsBorrowDetails.aspx.cs" Inherits="WebForm_GoodsBorrowDetails" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <fieldset>
        <table id="Table1" class="one" width="100%">
            <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Size="X-Large" 
                ForeColor="Blue"></asp:Label> 
        </table> 
        <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td  align ="center">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Borrower" HeaderText="借用人" ReadOnly="True" SortExpression="Borrower">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Ext" HeaderText="借用人分機" ReadOnly="True" SortExpression="Ext">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>          
                                <asp:BoundField DataField="BorrowedQuantity" HeaderText="借用數量" ReadOnly="True" SortExpression="BorrowedQuantity">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                  
                                <asp:BoundField DataField="StartDate" HeaderText="借用日期" ReadOnly="True" SortExpression="StartDate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="歸還日期" ReadOnly="True" SortExpression="EndDate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>


                            
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
        </table> 
    </fieldset> 
</asp:Content>

