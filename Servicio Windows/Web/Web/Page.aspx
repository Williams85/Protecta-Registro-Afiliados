<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page.aspx.cs" Inherits="Web.Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btn_Insertar" runat="server" Text="Insertar" OnClick="btn_Insertar_Click" />
            <br />
            <asp:RadioButton ID="rbOk" runat="server" Text="Ok" GroupName="TipoResultado" />
            <asp:RadioButton ID="rbError" runat="server" Text="Error" GroupName="TipoResultado"/>

        </div>
        <br />
        <br />
        <div>
            <asp:Button ID="btn_Listar" runat="server" Text="Listar" OnClick="btn_Listar_Click" />
            <br />
            <asp:GridView ID="gridRespuesta" runat="server"></asp:GridView>
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
            <br />
            <asp:Button ID="btn_Exportar" runat="server" Text="Exportar Excel" OnClick="btn_Exportar_Click" />
        </div>
    </form>
</body>
</html>
