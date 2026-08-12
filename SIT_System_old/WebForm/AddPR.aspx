<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddPR.aspx.cs" Inherits="WebForm_AddPR" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    <style>
        /* Adjust the jQuery UI widget font-size: */
        .ui-widget {
            font-size: 0.95em;
    }
    </style>    
    
    <fieldset>
        <font face="verdana"color="0000DD"size="4" ><legend>新增採購資訊</legend></font>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label30" runat="server" Text="地點"></asp:Label>
                    
                </td>
                <td colspan=3>
                    

                    
                    <asp:RadioButton ID="rdoLocal" runat="server" GroupName="1" Text="台北" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoLocal1" runat="server" GroupName="1" Text="吳江" />
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="開立PR日期"></asp:Label>
                
                </td>
                <td>
                    <input type="text" id="datepicker1" name = "date2">
                    
                     <script>
                         $(function() {
                         $("#datepicker1").datepicker();
                     });
                    
                     </script>                    
                </td>
            
            </tr>             
              
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="請購單號"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtPR_No" runat="server" Width="324px"></asp:TextBox>
                
                </td>
            
            </tr> 
            <%--<tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="採購類別"></asp:Label>
                
                </td>
                <td>                
                    <asp:DropDownList ID="ddlKind" runat="server">

                    </asp:DropDownList>
                
                </td>
            
            </tr>--%>
            <tr>
            
                <td>
                    <asp:Label ID="Label1" runat="server" Text="預計交貨日期"></asp:Label>
                
                </td>
                <td>
                    <input type="text" id="datepicker" name = "date1">
                    
                     <script>
                         $(function() {
                         $("#datepicker").datepicker();
                     });
                    
                     </script>                    
                </td>
            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="簽呈編號"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtSigned_ID" runat="server" Width="324px"></asp:TextBox>
                
                </td>
            
            </tr>               
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="需求人"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtDemand_Person" runat="server" Width="324px"></asp:TextBox>
                
                </td>
            
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtMail" runat="server" Width="324px"></asp:TextBox>
                
                </td>
            
            </tr>                       
            <tr>  
                <td>
                    
                    <asp:Label ID="Label33" runat="server" Text="需求原因"></asp:Label>
                    
                </td>
                <td>
                   
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
                
            </tr>    
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />
                    <br />
                        
                        <asp:Button ID="butOK" runat="server" Text="確定" 
                                onclick="butOK_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />                                
                        
                    <br />
                    <br />
                </td>
            </tr>                                                 
        </table> 
    </fieldset> 
</asp:Content>

