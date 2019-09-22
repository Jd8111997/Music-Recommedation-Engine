<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Beats1.admin1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:FileUpload ID="FileUpload1" runat="server" />
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <p>
            Track name<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            album<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>
            artist<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            genre<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </p>
        <p>
            duration<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </p>
    </form>
</body>
</html>
