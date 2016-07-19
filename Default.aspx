<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="defaultProject._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 106px;
        }
        .style2
        {
            font-size: xx-large;
        }
        .style3
        {
            width: 221px;
        }
        .style4
        {
            font-size: small;
        }
        .style11
        {
            width: 106px;
            height: 19px;
        }
        .style12
        {
            width: 221px;
            height: 19px;
        }
        .style13
        {
            height: 19px;
        }
        .style14
        {
            width: 106px;
            height: 11px;
        }
        .style15
        {
            width: 221px;
            height: 11px;
        }
        .style16
        {
            height: 11px;
        }
        .style17
        {
            width: 106px;
            height: 29px;
        }
        .style18
        {
            width: 221px;
            height: 29px;
        }
        .style19
        {
            height: 29px;
        }
        .style20
        {
            width: 106px;
            height: 30px;
        }
        .style21
        {
            width: 221px;
            height: 30px;
        }
        .style22
        {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span class="style2"><strong>廠內報工資料查詢<br />
        </strong></span>
        <table style="width: 100%;">
            <tr>
                <td bgcolor="#3399FF" class="style11">
                    完成日期(起)：
                </td>
                <td class="style12">
                    <asp:TextBox ID="txtDate1" runat="server"></asp:TextBox>
                    <asp:Button ID="btnDate1" runat="server" Height="23px" onclick="btnDate1_Click" 
                        Text="..." Width="31px" />
                    <br />
                    <span class="style4"><strong>可手動輸入Ex.20130516</strong></span></td>
                <td class="style13">
                    &nbsp;
                    <asp:Calendar ID="cldrDate1" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        onselectionchanged="cldrDate1_SelectionChanged" Visible="False" Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td bgcolor="#3399FF" class="style14">
                    完成日期(迄)：</td>
                <td class="style15">
                    <asp:TextBox ID="txtDate2" runat="server"></asp:TextBox>
                    <asp:Button ID="btnDate2" runat="server" Height="23px" onclick="btnDate2_Click" 
                        Text="..." Width="31px" />
                    <br />
                    <span class="style4"><strong>可手動輸入Ex.20130516</strong></span></td>
                <td class="style16">
                    &nbsp;
                    <asp:Calendar ID="cldrDate2" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        onselectionchanged="cldrDate2_SelectionChanged" Visible="False" Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td bgcolor="#3399FF" class="style17">
                    工單號碼</td>
                <td class="style18">
                    <asp:TextBox ID="txtProdOrderNum" runat="server"></asp:TextBox>
                </td>
                <td class="style19">
                </td>
            </tr>
            <tr>
                <td bgcolor="#3399FF" class="style17">
                    銷售訂單/項次</td>
                <td class="style18">
                    <asp:TextBox ID="txtSO" runat="server" Width="95px"></asp:TextBox>
&nbsp;
                    <asp:TextBox ID="txtSOITEM" runat="server" Width="55px"></asp:TextBox>
                    <br />
                    Ex. 2000123 / 0010</td>
                <td class="style19">
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#3399FF" class="style20">
                    廠內單位</td>
                <td class="style21">
                    <asp:RadioButtonList ID="rblDept" runat="server">
                        <asp:ListItem Selected="True" Value="0">製造</asp:ListItem>
                        <asp:ListItem Value="1">出貨</asp:ListItem>
                        <asp:ListItem Value="2">加工</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style22">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Button ID="btnQry" runat="server" OnClick="btnQry_Click" Text="查詢" Style="height: 26px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnClr" runat="server" OnClick="btnClr_Click" Text="重置" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                <td>
                    &nbsp;
                    <asp:Button ID="btnConvert" runat="server" onclick="btnConvert_Click" 
                        Text="轉EXCEL" Visible="False" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvData" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <br />
        <br />
    <asp:HyperLink ID="hl" runat="server" NavigateUrl="/sap/">回SAP報表畫面</asp:HyperLink>
    </div>
    </form>
</body>
</html>
