<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationDetail.aspx.cs" Inherits="WebForm_ApplicationDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<fieldset>
    <asp:Label ID="lblID" runat="server" ForeColor="#3333FF" Font-Bold="True"></asp:Label>
    <table id="tb1" runat="server" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
    
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
                <asp:Label ID="Label2" runat="server" Text="客戶"></asp:Label>
                
            </td>
            <td>
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
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="TestCase"></asp:Label>
                
            </td>
            <td colspan=3>
                    <asp:Label ID="lblTestCase" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtTestCase" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="579px"></asp:TextBox>
                   
            </td>
           
        </tr>        
        <tr>
            <td>
                <asp:Label ID="Label35" runat="server" Text="備註"></asp:Label>
                
            </td>
            <td colspan=3>
                   <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="578px"></asp:TextBox>
                   
            </td>
           
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="附加檔案"></asp:Label>
                
            </td>

                <td align ="center" colspan="5" >
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
<%--                        <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                            <ItemTemplate>
                                &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                    Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="File_Name" HeaderText="項目" ReadOnly="True" SortExpression="File_Name">
                            <%--<ControlStyle Width="30px"></ControlStyle>--%>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        
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
      
        
                         
    </table>
        <tr>
        <td align ="center" colspan = 2 style="COLOR: red">
            <br />
            <br />
                
            <asp:Button ID="butOK" runat="server" Text="匯出Excel" 
                    onclick="butOK_Click" />
                
            <br />
            <br />
        </td>
    </tr> 
      
</fieldset> 

</asp:Content>

