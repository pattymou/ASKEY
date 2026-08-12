<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddWeeklyReport.aspx.cs" Inherits="WebForm_AddWeeklyReport" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <script >
        function NewFocus(obj){
       obj.focus();
       var oTextRange = document.createTextRange();
       oTextRange.collapse(false);
       oTextRange.select();
    }
    </script>
    <script >
        function thisFunc(obj){
 
        //判断从键盘输入值
        if(event.keyCode==32||(event.keyCode>48&&event.keyCode<90)){
            __doPostBack(obj.id,'','');
        }
    }
    </script>
    <fieldset>
        <table id="Table4" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblLastWeek" runat="server" OnClick="lbtnlblLastWeek_Click">[上週]</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkThisWeek" runat="server" OnClick="lbtnThisWeek_Click">[本週]</asp:LinkButton>
                    
                    
                </td>

            </tr>         
         </table> 
         <br />
        <table id="Table2" runat =server  width="100%">
            <%--<tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="客戶代碼："></asp:Label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>                
                            <asp:GridView ID="gvwMain" runat="server" Width="100%" ShowFooter="true"
                                AutoGenerateColumns="False" GridLines="None" 
                                OnRowDataBound="gvwMain_RowDataBound">                    
                                <Columns>
                                    <%--<asp:BoundField DataField="RowNumber" HeaderText="Row Number" />--%>
                                    <asp:BoundField DataField="Name" HeaderText="專案名稱" ReadOnly="True" SortExpression="Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Item" HeaderText="項目" ReadOnly="True" SortExpression="Item">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="說明" ItemStyle-HorizontalAlign ="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Width="200px" Text='<%#Eval("Detail") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign ="Center">
                                        <ControlStyle Width="50px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtW1" runat="server" OnTextChanged ="txtW1_TextChanged" Text='<%#Eval("W1") %>' AutoPostBack ="true" onkeyup="thisFunc(this)"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblW1" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign ="Center">
                                        <ControlStyle Width="50px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtW2" runat="server" OnTextChanged ="txtW2_TextChanged" Text='<%#Eval("W2") %>' AutoPostBack ="true" onkeyup="thisFunc(this)"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblW2" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign ="Center">
                                        <ControlStyle Width="50px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtW3" runat="server" OnTextChanged ="txtW3_TextChanged" Text='<%#Eval("W3") %>' AutoPostBack ="true" onkeyup="thisFunc(this)"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblW3" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign ="Center">
                                        <ControlStyle Width="50px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtW4" runat="server" OnTextChanged ="txtW4_TextChanged" Text='<%#Eval("W4") %>' AutoPostBack ="true" onkeyup="thisFunc(this)"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblW4" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign ="Center">
                                        <ControlStyle Width="50px"></ControlStyle>
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtW5" runat="server" OnTextChanged ="txtW5_TextChanged" Text='<%#Eval("W5") %>' AutoPostBack ="true" onkeyup="thisFunc(this)"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                         <%--<asp:Button ID="ButtonAdd" runat="server" Text="Add Row" 
                                                onclick="ButtonAdd_Click" />--%>
                                                <asp:Label ID="lblW5" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                         
                                    <asp:TemplateField HeaderText="Project_ID" SortExpression="Project_ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProject_ID" runat="server" Text='<%# Bind("Project_ID") %>'></asp:Label>
                                        </ItemTemplate>                                    
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Case_ID" SortExpression="Case_ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCase_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>                                    
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridviewScrollHeader" /> 
                                <RowStyle CssClass="GridviewScrollItem" /> 
                                <PagerStyle CssClass="GridviewScrollPager" />
                            </asp:GridView>
                        </ContentTemplate>
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtW1" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtW2" EventName="TextChanged" />
                                

                        </Triggers>--%>
                    </asp:UpdatePanel>  
                 </td> 
             </tr>
             <tr>
                <td align ="center">
                     <asp:Label ID="lblWeek" runat="server" Text="" Visible="False"></asp:Label>                   
                    &nbsp;</td>
            </tr>              
         </table>
         <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">

             <tr>
                <td align ="center" colspan=2>
                    <asp:Label ID="Label3" runat="server" Text="下週計劃" Font-Bold="True" 
                        Font-Size="Large" ForeColor="Blue"></asp:Label>
                </td>
             </tr>
             <tr>
                <td align ="center">
                    <asp:Label ID="Label1" runat="server" Text="Mon" Width="300px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt1" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                </td>
             </tr>
             <tr>
                <td align ="center">
                    <asp:Label ID="Label2" runat="server" Text="Tue" Width="300px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt2" runat="server" TextMode="MultiLine"  Width="500px"></asp:TextBox>
                </td>
             </tr>
             <tr>
                <td align ="center">
                    <asp:Label ID="Label4" runat="server" Text="Wed" Width="300px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt3" runat="server" TextMode="MultiLine"  Width="500px"></asp:TextBox>
                </td>
             </tr>
             <tr>
                <td align ="center">
                    <asp:Label ID="Label5" runat="server" Text="Thu" Width="300px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt4" runat="server" TextMode="MultiLine"  Width="500px"></asp:TextBox>
                </td>
             </tr>
             <tr>
                <td align ="center">
                    <asp:Label ID="Label6" runat="server" Text="Fri" Width="300px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt5" runat="server" TextMode="MultiLine"  Width="500px"></asp:TextBox>
                </td>
             </tr>
             
        </table>
        <table id="Table3" runat =server  width="100%">
            <tr>
                <td align ="center">
                                        
                    &nbsp;</td>
            </tr>
             <tr>
                <td align =center>
                    
                    <asp:Button ID="btnSave" runat="server" Text="儲存" onclick="btnSave_Click" />
                    
                </td>
            </tr>
        </table> 
    </fieldset> 
</asp:Content>

