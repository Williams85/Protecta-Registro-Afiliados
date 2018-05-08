<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Session.aspx.vb" Inherits="Session" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
    var iStart = 0;
    var iMinute = <%= Session.TimeOut %>; //Obtengo el tiempo de session permitida
    function showTimer() { 
    iStart = 60; 
    iMinute -= 1
    lessMinutes(); 
    } 
    function lessMinutes()
    {
    //Busco mi elemento que uso para mostrar los minutos que le quedan (minutos y segundos)
    obj = document.getElementById('TimeLeft'); 
    if (iStart == 0) {
    iStart = 60 
    iMinute -= 1; 
    }
    iStart = iStart - 1;

    //Si minuto y segundo = 0 ya expiró la sesion 
    if (iMinute==0 && iStart==0) {
    alert("Su sesion ha expirado, sera redireccionado a la página principal");
    window.location.href = '<%= Request.ApplicationPath %>' + '/Default.aspx';
    }

    if (iStart < 10)
    obj.innerText = iMinute.toString() + ':0' + iStart.toString();
    else
    obj.innerText = iMinute.toString() + ':' + iStart.toString();

    //actualizo mi método cada segundo  
    window.setTimeout("lessMinutes();",1000)
    } 
    </script>
</head>
<body onload="InitializeTimer()">
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
    </form>
    <span id="TimeLeft"></span>

    <script type="text/javascript" language="javascript"> 
        showTimer();
</script>
</body>
</html>
