<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentDailyReport.aspx.cs" Inherits="WebForm_DepartmentDailyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
    <hr size="5" width="100%" color="DDDDDD" style="height: 5px">
    <table id="Table5" class="one" width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1"  runat="server" Text="說明"></asp:Label>
                
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="2000" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
            </td>
        </tr>
        
    </table> 
        <%--<table id="Table5" class="one" width="100%">
            <tr>
                <td>
                    <asp:GridView ID="gvwMain"  OnDataBound ="gvwMain_DataBound" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="日期" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblReservationDate" runat="server" Text='<%# Bind("ReservationDate") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="早" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblMorning" runat="server" Text='<%# Bind("Morning") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMorning" runat="server" Text='<%# Bind("Morning") %>' MaxLength="500" Rows="5" TextMode="MultiLine" Width="380px"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="午" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAfternoon" runat="server" Text='<%# Bind("Afternoon") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAfternoon" runat="server" Text='<%# Bind("Afternoon") %>' MaxLength="500" Rows="5" TextMode="MultiLine" Width="380px"></asp:TextBox>
                                </EditItemTemplate>                                
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="晚" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvening" runat="server" Text='<%# Bind("Evening") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEvening" runat="server" Text='<%# Bind("Evening") %>' MaxLength="500" Rows="5" TextMode="MultiLine" Width="380px"></asp:TextBox>
                                </EditItemTemplate> 
                            </asp:TemplateField>                                                                                 
                                                        
                            
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("Reservation_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="Center" BackColor="#EFF3FB" />
                        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" Wrap="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#66CCFF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>    
                </td>
            </tr>

        </table>--%> 
        <table id="Table1" class="one" width="100%">
        <tr>
            <td align ="center" colspan = 3 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="確定" 
                            onclick="butOK_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;            
                <asp:Button ID="butReturn" runat="server" Text="上一頁" 
                            onclick="butReturn_Click" />                            
                    
                <br />
                <br />
            </td>
        </tr>  
        </table>       
    </fieldset> 
</asp:Content>

