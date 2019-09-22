<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="play.aspx.cs" Inherits="Beats1.play" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
function callme() {
    var rating = parseInt(prompt("Please enter your ratings 10 being the maximum", "0"));
    var songid=<%=this.id%>;
    if (rating > 10 || rating <= 0)
    {
        alert("Enter proper value rating value");
        location.href="index.aspx"
    }
   location.href="saveratings.aspx?rating="+rating+"&songid="+songid;
}
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
    </div>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
