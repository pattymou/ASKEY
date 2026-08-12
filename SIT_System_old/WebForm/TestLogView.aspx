<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="TestLogView.aspx.cs" Inherits="WebForm_TestLogView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
<%--    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">

  
    <style>
        /* Adjust the jQuery UI widget font-size: */
        .ui-widget {
            font-size: 0.95em;
    }
    </style>--%>
    
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <style>
        @import url('https://fonts.googleapis.com/css?family=Lato');
        @import url('http://fonts.googleapis.com/earlyaccess/cwtexyen.css');
        @import url('https://fonts.googleapis.com/css?family=Do+Hyeon');
        .col-Item
        {
            padding: 10px;
        }
        .report-list
        {
            box-shadow: 0px 1px 0px rgb(0, 0, 0,.05);
            border-radius: 1px;
            background: White;
            overflow: hidden;
        }
        .gridview-show
        {
            border: 1px solid #D4D4D4;
            font-size: 16px;
            font-family: 'Lato' , sans-serif;
            margin: 5px 0;
        }
        .btn
        {
            border: 0px solid;
            font-size: 20px;
            font-family: 'cwTeXYen' , sans-serif;
            color: #212121;
            background-color: #bababa;
            margin: 2px 0;
        }
        .btn:hover
        {
            background-color: #2B2B2B;
        }
        .SearchList
        {
            font-size: 22px;
            font-family: 'cwTeXYen' , sans-serif;
            padding: 0 10px;
        }
        th
        {
            text-align: center;
        }
        .ddl
        {
            font-size: 18px;
            font-family: 'Lato' , sans-serif;
        }
        .label
        {
            font-size: 20px;
            font-family: 'cwTeXYen' , sans-serif;
        }
        .label-1
        {
            font-size: 25px;
            font-family: 'cwTeXYen' , sans-serif;
            color: Red;
            text-align: center;
        }
    </style>
    <%--<style type="text/css">
        table
        {
            table-layout: fixed;
            word-break: break-all;
        }
    </style>--%>
       
    <table width="100%">
        <tr>
            <td>
                <div id="div1" style="overflow: scroll; height: 500px; width: 100%;">
                    <asp:GridView ID="gvwMain" runat="server" class="gridview-show" AutoGenerateColumns="False" Width="100%"
                        ForeColor="#333333" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDeleting="gvwMain_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                            Text="刪除" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            <asp:TemplateField HeaderText="測試日期" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTestDate" runat="server" Text='<%# Bind("TestDate") %>'></asp:Label>
                                </ItemTemplate>  
                                <%--<ControlStyle Width="30px"></ControlStyle>--%>                              
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="測試類別" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTestKind" runat="server" Text='<%# Bind("TestKind") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>                         

                            <asp:BoundField DataField="ModelName" HeaderText="機種名稱" ReadOnly="True" SortExpression="ModelName"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            
                            
                            <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" SortExpression="Customer"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>   
                            <asp:BoundField DataField="ModelPU" HeaderText="機種Sub-PU" ReadOnly="True" SortExpression="ModelPU"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>                                                      

                            <asp:BoundField DataField="Applicant" HeaderText="申請人" ReadOnly="True" SortExpression="Applicant"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="A_Department" HeaderText="申請人Sub-PU" ReadOnly="True" SortExpression="A_Department"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center"  Wrap="False" />
                            </asp:BoundField>  
                            
                             <asp:BoundField DataField="Channel" HeaderText="Channel" ReadOnly="True" SortExpression="Channel"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Angle" HeaderText="Angle" ReadOnly="True" SortExpression="Angle"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="BandWidth" HeaderText="BandWidth" ReadOnly="True" SortExpression="BandWidth"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Attenuation" HeaderText="Attenuation" ReadOnly="True" SortExpression="Attenuation"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status"  >
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            

                            
                           
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnSearch" runat="server" 
                                  CommandName="AddToCart" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="TestLog" />
                              </ItemTemplate> 
                            </asp:TemplateField>
                            
                            
                            
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#E8E8E8" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#595959" Font-Bold="True" ForeColor="white" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        
        
        <%--<tr>
            <td align ="center">
                                    
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDeleting="gvwMain_RowDeleting" >
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                            Text="刪除" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="測試日期" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTestDate" runat="server" Text='<%# Bind("TestDate") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="測試類別" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTestKind" runat="server" Text='<%# Bind("TestKind") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>                         

                            <asp:BoundField DataField="ModelName" HeaderText="機種名稱" ReadOnly="True" SortExpression="ModelName">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            
                            <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" SortExpression="Customer">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>   
                            <asp:BoundField DataField="ModelPU" HeaderText="機種Sub-PU" ReadOnly="True" SortExpression="ModelPU">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                      

                            <asp:BoundField DataField="Applicant" HeaderText="申請人" ReadOnly="True" SortExpression="Applicant">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="A_Department" HeaderText="申請人Sub-PU" ReadOnly="True" SortExpression="A_Department">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>  

                            
                           
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnSearch" runat="server" 
                                  CommandName="AddToCart" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="TestLog" />
                              </ItemTemplate> 
                            </asp:TemplateField>
                            
                            
                            
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                </asp:GridView>           
            </td>
        </tr>--%>
    </table> 
</asp:Content>

