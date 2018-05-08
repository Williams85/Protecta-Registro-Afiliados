<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProcesoMinibatchSusalud.aspx.vb" Inherits="ProcesoMinibatchSusalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <table id="TABLE1" style="width: 100%; border-top-style: groove; border-right-style: groove;
        border-left-style: groove; border-bottom-style: groove" onclick="return TABLE1_onclick()" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" colspan="3" style="height: 30px">
                <asp:Label ID="txtTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt"
                    ForeColor="Green">Carga Minibatch</asp:Label></td>
            <td align="right" colspan="3" style="height: 30px">
                &nbsp; &nbsp;&nbsp;
                </td>
            <td align="right" colspan="1" style="width: 678px; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 35px">
                &nbsp;</td>
            <td style="width: 282px; height: 35px">
                &nbsp;</td>
            <td colspan="4" style="height: 35px">
                <asp:CheckBox ID="ckbGenerarArchivo" runat="server" Text="Generar todos los productos" AutoPostBack="True" ViewStateMode="Enabled" />
                </td>
            <td colspan="1" style="width: 678px; height: 35px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 517px; height: 35px">
            </td>
            <td style="width: 282px; height: 35px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Canal :"></asp:Label></td>
            <td colspan="4" style="height: 35px">
                <asp:DropDownList ID="ddlCanal" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" style="width: 678px; height: 35px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 37px;">
            </td>
            <td style="width: 282px; height: 37px;">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Producto :"></asp:Label></td>
            <td colspan="4" style="height: 37px" rowspan="">
                <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" rowspan="" style="width: 678px; height: 37px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 33px">
            </td>
            <td style="width: 282px; height: 33px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Póliza:"></asp:Label></td>
            <td style="height: 33px; width: 104px;">
                <asp:DropDownList ID="ddlPoliza" runat="server" AutoPostBack="True" Width="120px">
                </asp:DropDownList></td>
            <td colspan="3" rowspan="2">
                <br />
                <br />
            </td>
            <td colspan="1" rowspan="2" style="width: 678px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 20px;">
            </td>
            <td style="width: 282px; height: 20px;">
                </td>
            <td style="width: 104px; height: 20px;">
                </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
            </td>
            <td align="center" colspan="4" style="height: 23px">
                &nbsp;<asp:Button ID="BtnGeneraTrama" runat="server" Text="Generar Trama X12N" Width="138px" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            </td>
            <td style="width: 678px; height: 23px;">
                &nbsp;</td>
            <td style="width: 678px; height: 23px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 13px;">
            </td>
            <td align="center" colspan="4" style="height: 13px">
                </td>
            <td style="width: 678px; height: 13px;">
            </td>
            <td style="width: 678px; height: 13px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 42px;">
            </td>
            <td align="center" style="height: 42px; width: 1px;">
                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Archivo:"></asp:Label></td>
            <td align="center" colspan="2" style="height: 42px">
                                    <asp:TextBox ID="txtRutaArchivo" runat="server" Width="295px" ReadOnly="True"></asp:TextBox></td>
            <td align="center" colspan="2" style="height: 42px">
                </td>
            <td style="width: 678px; height: 42px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 36px;">
            </td>
            <td align="center" style="height: 36px; width: 1px;">
                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Comentario:"></asp:Label></td>
            <td align="center" colspan="2" style="height: 36px">
                                    <asp:TextBox ID="txtComentario0" runat="server" MaxLength="80" TabIndex="4" Width="291px" Height="22px"></asp:TextBox></td>
            <td align="center" colspan="2" style="height: 36px">
                </td>
            <td style="width: 678px; height: 36px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 16px;">
            </td>
            <td align="center" colspan="4" style="height: 16px">
                </td>
            <td style="width: 678px; height: 16px;">
            </td>
            <td style="width: 678px; height: 16px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
                &nbsp;</td>
            <td align="center" colspan="4" style="height: 23px">
                <asp:Button ID="BtnEnviarTrama" runat="server" Text="Enviar Trama X12N" Width="138px" />
            </td>
            <td style="width: 678px; height: 23px;">
                &nbsp;</td>
            <td style="width: 678px; height: 23px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
                &nbsp;</td>
            <td align="center" colspan="4" style="height: 23px">
                &nbsp;</td>
            <td style="width: 678px; height: 23px;">
                &nbsp;</td>
            <td style="width: 678px; height: 23px">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>

