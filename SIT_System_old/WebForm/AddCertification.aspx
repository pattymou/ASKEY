<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddCertification.aspx.cs" Inherits="WebForm_AddCertification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/jquery-ui.min.css" />
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
  
  
    <script>
      $(function() {
          $("#tabs").tabs();
          $("#tabs1").tabs();
          $("#tabs2").tabs();
      });
    </script>

    

    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Wi-Fi</a></li>
            <li><a href="#tabs-2">BT</a></li>

            
        </ul>
            <div id="tabs-1">
                <div id="tabs1">
                <ul>
                    <li><a href="#tabs1-1">Mandatory Program</a></li>
                    <li><a href="#tabs1-2">Optional Programs</a></li>
                    
                </ul>
                <div id="tabs1-1">
                    <asp:updatepanel id="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Wi-Fi CERTIFIED ac & n"></asp:Label>
                                <asp:TextBox ID="txtac" runat="server" Width ="100%" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Wi-Fi CERTIFIED n"></asp:Label>
                                <asp:TextBox ID="txtN" runat="server" Width ="100%" TextMode="MultiLine"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Wi-Fi CERTIFIED 6"></asp:Label>
                                <asp:TextBox ID="txt6" runat="server" Width ="100%" TextMode="MultiLine"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr align ="center">
                            <td>
                                <br />
                                <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnOK" EventName="Click" />

                    </Triggers>
                </asp:updatepanel>
                    
                </div>
                <div id="tabs1-2" >
                    <asp:updatepanel id="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <table Width = "30%">
                        <tr>
                            <td >
                                <asp:Label ID="Label4" runat="server" Text="Option"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOption" runat="server" Width ="100%"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlAOptional" runat="server" >
                                </asp:DropDownList>--%>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="說明"></asp:Label>
                            </td>
                             <td>
                                
                                <asp:TextBox ID="txtOptionA" runat="server" Width ="60%" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;&nbsp
                                <asp:Button ID="btnAdd" runat="server" Text="新增" onclick="btnAdd_Click" />                                
                            </td>                           
                        </tr>
                        <tr>
                            <td colspan =2>
                                ***************************************************
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Label ID="Label6" runat="server" Text="Option"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAOptional" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlAOptional_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="說明"></asp:Label>
                            </td>
                             <td>
                                
                                <asp:TextBox ID="txtOptionM" runat="server" Width ="60%" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnModify" runat="server" Text="修改" onclick="btnModify_Click" />                                
                            </td>                           
                        </tr>
                        <tr>
                            <td colspan =2>
                                ***************************************************
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Label ID="Label9" runat="server" Text="Option"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAOptionalD" runat="server" >
                                </asp:DropDownList>
                                <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click" /> 
                            </td>
                            <%--<td>
                                                               
                            </td>--%>

                        </tr>
                        <%--<tr>
                             <td>
                                <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click" />                                
                            </td>                           
                        </tr>--%>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="ddlAOptional" EventName="SelectedIndexChanged" />--%>

                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" />
                        

                    </Triggers>
                </asp:updatepanel>
                </div>

            </div>
            
            
        </div>
        <div id="tabs-2">
            <div id="tabs2">
            <ul>
                <li><a href="#tabs2-1">BT Version</a></li>
                <li><a href="#tabs2-2">Core Mode</a></li>
                
            </ul>
            
            <div id="tabs2-1">
                <asp:updatepanel id="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <table Width = "30%">
                        <tr>
                            <td >
                                <asp:Label ID="Label10" runat="server" Text="BT Version"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBT_CS" runat="server" Width ="100%"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlAOptional" runat="server" >
                                </asp:DropDownList>--%>
                            </td>
                            <td>
                            &nbsp&nbsp
                                <asp:Button ID="btnABT_CS" runat="server" Text="新增" onclick="btnABT_CS_Click" />                                
                            </td>

                        </tr>
                        
                        <tr>
                            <td colspan =3>
                                ***************************************************
                            </td>
                        </tr>
                                               
                        <tr>
                            <td >
                                <asp:Label ID="Label11" runat="server" Text="BT Version"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBT_CS" runat="server" >
                                </asp:DropDownList>
                                <asp:Button ID="btnDBT_CS" runat="server" Text="刪除" onclick="btnDBT_CS_Click" /> 
                            </td>
                            <%--<td>
                                                               
                            </td>--%>

                        </tr>
                        <%--<tr>
                             <td>
                                <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click" />                                
                            </td>                           
                        </tr>--%>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="ddlAOptional" EventName="SelectedIndexChanged" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnABT_CS" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDBT_CS" EventName="Click" />
                        

                    </Triggers>
                </asp:updatepanel>
            </div>
            <div id="tabs2-2">
                <asp:updatepanel id="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <table Width = "30%">
                        <tr>
                            <td >
                                <asp:Label ID="Label8" runat="server" Text="Core Mode"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBT_CoreMode" runat="server" Width ="100%"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlAOptional" runat="server" >
                                </asp:DropDownList>--%>
                            </td>
                            <td>
                            &nbsp&nbsp
                                <asp:Button ID="btnAddBT_CoreMode" runat="server" Text="新增" onclick="btnAddBT_CoreMode_Click" />                                
                            </td>

                        </tr>
                        
                        <tr>
                            <td colspan =3>
                                ***************************************************
                            </td>
                        </tr>
                                               
                        <tr>
                            <td >
                                <asp:Label ID="Label13" runat="server" Text="Core Mode"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDBT_CoreMode" runat="server" >
                                </asp:DropDownList>
                                <asp:Button ID="btnDBT_CoreMode" runat="server" Text="刪除" onclick="btnDBT_CoreMode_Click" /> 
                            </td>
                            <%--<td>
                                                               
                            </td>--%>

                        </tr>
                        <%--<tr>
                             <td>
                                <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click" />                                
                            </td>                           
                        </tr>--%>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="ddlAOptional" EventName="SelectedIndexChanged" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnAddBT_CoreMode" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDBT_CoreMode" EventName="Click" />
                        

                    </Triggers>
                </asp:updatepanel>
                
                
            </div>
</div>
        </div>

    </div>

</asp:Content>

