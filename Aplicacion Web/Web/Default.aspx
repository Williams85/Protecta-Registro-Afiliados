<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" Title="Sistema Integrador de Archivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 1000px; height: 90%; margin: 0 auto;">
                <tr>
                    <td style="height: 47px" align="left" colspan="10">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Logo-transicion.jpg" Height="48px" Width="192px" />
                    </td>
                    <td style="width: 2%"></td>
                    <td style="width: 2%"></td>
                </tr>
                <tr>
                    <td colspan="10"></td>
                    <td style="width: 2%"></td>
                    <td style="width: 2%"></td>
                </tr>
                <tr>
                    <td colspan="10" style="font-weight: bold; font-size: x-large; color: #c80000; font-family: Tahoma; height: 31px;">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/nOMBRE-DEL-SISTEMA.png" /></td>
                    <td style="font-weight: bold; font-size: x-large; width: 2%; color: #c80000; font-family: Tahoma; height: 31px"></td>
                    <td style="font-weight: bold; font-size: x-large; width: 2%; color: #c80000; font-family: Tahoma; height: 31px"></td>
                </tr>
                <tr>
                    <td colspan="10" style="height: 195px">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/SIA-01.png"
                            Width="1000px" Height="200px" /></td>
                    <td colspan="1" style="width: 2%; height: 195px"></td>
                    <td colspan="1" style="width: 2%; height: 195px"></td>
                </tr>
                <tr>
                    <td colspan="12" style="width: 15px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%;"></td>
                    <td style="width: 10%"></td>
                    <td style="width: 4%"></td>
                    <td style="width: 5%"></td>
                    <td style="width: 5%"></td>
                    <td style="width: 5%"></td>
                    <td style="width: 35%" align="right">
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="8pt"
                            Text="Usuario :"></asp:Label></td>
                    <td colspan="2" style="width: 20%">
                        <asp:TextBox ID="txtUsuario" runat="server" Width="146px"></asp:TextBox></td>
                    <td style="width: 2%"></td>
                    <td style="width: 2%"></td>
                    <td style="width: 2%"></td>
                </tr>
                <tr>
                    <td style="width: 10%; height: 26px;"></td>
                    <td style="width: 10%; height: 26px;"></td>
                    <td style="width: 4%; height: 26px;"></td>
                    <td style="width: 5%; height: 26px;"></td>
                    <td style="width: 5%; height: 26px;"></td>
                    <td style="width: 5%; height: 26px;"></td>
                    <td style="width: 35%; height: 26px;" align="right">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="8pt"
                            Text="Contraseña :"></asp:Label></td>
                    <td colspan="2" style="width: 20%; height: 26px;">
                        <asp:TextBox ID="txtClave" runat="server" Width="145px" TextMode="Password"></asp:TextBox></td>
                    <td style="width: 2%; height: 26px;"></td>
                    <td style="width: 2%; height: 26px;"></td>
                    <td style="width: 2%; height: 26px;"></td>
                </tr>
                <tr>
                    <td colspan="12" style="height: 12px;"></td>
                </tr>
                <tr>
                    <td style="width: 10%;"></td>
                    <td style="width: 10%;"></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 5%;"></td>
                    <td style="width: 5%;"></td>
                    <td style="width: 5%;"></td>
                    <td style="width: 35%;" align="right">
                        &nbsp;
                    </td>
                    <td colspan="2" style="width: 20%;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Boton-aceptar_chico.png" Width="82px" />
                    </td>
                    <td style="width: 2%;"></td>
                    <td style="width: 2%;"></td>
                    <td style="width: 2%;"></td>
                </tr>
                <tr>
                    <td style="width: 10%; height: 23px;"></td>
                    <td style="width: 10%; height: 23px;"></td>
                    <td style="width: 4%; height: 23px;"></td>
                    <td style="width: 5%; height: 23px;"></td>
                    <td style="width: 5%; height: 23px;"></td>
                    <td style="width: 5%; height: 23px;"></td>
                    <td style="width: 35%; height: 23px;" align="right">
                        &nbsp;
                    </td>
                    <td colspan="2" style="width: 20%; height: 23px;">
                        &nbsp;
                    </td>
                    <td style="width: 2%; height: 23px;"></td>
                    <td style="width: 2%; height: 23px;"></td>
                    <td style="width: 2%; height: 23px;"></td>
                </tr>
                <tr>
                    <td colspan="10" style="height: 80px">
                        <asp:Image ID="Image3" runat="server" Height="94px" ImageUrl="~/Imagenes/franja-roja-caratula.png"
                            Width="984px" /></td>
                    <td colspan="1" style="width: 2%; height: 80px"></td>
                    <td colspan="1" style="width: 2%; height: 80px"></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
