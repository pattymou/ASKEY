<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddSampleRelease.aspx.cs" Inherits="WebForm_AddSampleRelease" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
     
  


    <fieldset>
        
        <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label27" runat="server" Text="留存編號"></asp:Label>
                    
                </td>
                 <td colspan=3>

                     <asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>

                </td>               

            </tr>        
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="專案名稱"></asp:Label>
                    
                </td>
                 <td colspan=3>

                     <asp:Label ID="lblName" runat="server" Text=""></asp:Label>

                </td>               

            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="MAC"></asp:Label>
                    
                </td>
                <td colspan=3>
                    

                    
                    <asp:TextBox ID="txtMAC" runat="server"></asp:TextBox>
                    

                    
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="數量"></asp:Label>
                </td>
                <td>
                    

                    
                    <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                    

                    
                </td>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="NPI"></asp:Label>
                    
                </td>
                <td>
                    

                    
                    <asp:DropDownList ID="ddlNPI" runat="server">
                    </asp:DropDownList>
                    

                    
                </td>                
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label9" runat="server" Text="負責人"></asp:Label>
                    
                </td>
                <td>
                    

                    
                    <asp:TextBox ID="txtCustodian" runat="server"></asp:TextBox>
                    

                    
                </td>
                <td>
                    
                    <asp:Label ID="Label10" runat="server" Text="樣品提供人"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtProvide" runat="server"></asp:TextBox>
                    
                </td>
            </tr>  
            
            
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="收到日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                     <script>
                         $(function() {
                             $("#datepicker").datepicker();
                         });
                     </script>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="歸還日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker1" name = "date2" value = "<%=strEnd%>">
                     <script>
                         $(function() {
                             $("#datepicker1").datepicker();
                         });
                     </script>
                </td>
            </tr>
                        
            
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtExplain" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                    
                </td>                
            </tr>
            
        
        </table> 

    <tr>
        <td align ="center" colspan = 2 style="COLOR: red">
            <%--<br />--%>
<%--            <br />--%>
                
            <asp:Button ID="butOK" runat="server" Text="確定" 
                    onclick="butOK_Click" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />   
            <br />
            <br />
        </td>
    </tr>

    </fieldset>
</asp:Content>

