<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Beats1.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 34px;
        }
        .auto-style4 {
            width: 234px;
        }
        .auto-style5 {
            height: 26px;
        }
        .auto-style6 {
            width: 234px;
            height: 26px;
        }
        .auto-style7 {
            width: 34px;
            height: 26px;
        }
        .btn1:hover {
          background-color:lightblue
              color :white
        }
    </style>
</head>
    <body>
        
    <form id="form1" runat="server">
    <div>
    
    </div>
        <table class="auto-style1">
            
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="account.aspx?mode=history">History</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:LoginStatus ID="LoginStatus1" runat="server" OnLoggingOut="LoginStatus1_LoggingOut" />
                </td>
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style6"></td>
                <td class="auto-style5"></td>
                <td class="auto-style7"></td>
                <td class="auto-style5">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="account.aspx?mode=update">Update Password</asp:HyperLink>
                </td>
                <td class="auto-style5"></td>
                <td class="auto-style5">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="channels.aspx">Channels</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style4">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="0">----NONE----</asp:ListItem>
                        <asp:ListItem Value="1">Artist</asp:ListItem>
                        <asp:ListItem Value="2">Genre</asp:ListItem>
                        <asp:ListItem Value="3">Track</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style1" Width="509px" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" class="btn1" runat="server" OnClick="Button1_Click" Text="Search!!" />
                </td>
                <td>&nbsp;</td>
                <td>
                <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
                </asp:Login>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:LoginView ID="LoginView2" runat="server" OnViewChanged="LoginView2_ViewChanged">
                        <AnonymousTemplate>
                            Want to Join us?<br />
                            <asp:HyperLink ID="link1" runat="server" NavigateUrl="register.aspx">Register here!!</asp:HyperLink>
                            &nbsp;<ahref ="Register.aspx/">
                        </AnonymousTemplate>
                    </asp:LoginView>
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="forgotpassword.aspx">Forgot Password</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style4">
                    <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="3">--SORT--</asp:ListItem>
                        <asp:ListItem Value="1">Time</asp:ListItem>
                        <asp:ListItem Value="0">Ratings</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="recommendme.aspx">Songs Recommended for me</asp:HyperLink>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnSelecting="SqlDataSource1_Selecting" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [track], [artist], [duration], [album], [song_rating], [song_id] FROM [song]"></asp:SqlDataSource>
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
            <asp:ListItem Value="0">Play</asp:ListItem>
            <asp:ListItem Value="1">Download</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Play/Download" ShowHeader="True" Text="select" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        
            
        <asp:LoginView ID="LoginView1" runat="server">
        </asp:LoginView>
        
            
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" DataKeyNames="song_id"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="track" HeaderText="track" SortExpression="track" />
                <asp:BoundField DataField="artist" HeaderText="artist" SortExpression="artist" />
                <asp:BoundField DataField="duration" HeaderText="duration" SortExpression="duration" />
                <asp:BoundField DataField="album" HeaderText="album" SortExpression="album" />
                <asp:BoundField DataField="song_rating" HeaderText="Song Rating" SortExpression="song_rating" />
                <asp:BoundField DataField="song_id" HeaderText="SongID" InsertVisible="False" ReadOnly="True" SortExpression="song_id" />
                <asp:CommandField ButtonType="Button" HeaderText="Play/Download" ShowHeader="True" ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
