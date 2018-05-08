
<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProcesoReenvioSusalud.aspx.vb" Inherits="ProcesoReenvioSusalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="TABLE1" style="width: 100%; border-top-style: groove; border-right-style: groove;
        border-left-style: groove; border-bottom-style: groove" onclick="return TABLE1_onclick()" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" colspan="3" style="height: 30px">
                <asp:Label ID="txtTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="13pt"
                    ForeColor="Green">Reenvio:</asp:Label></td>
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
            <td colspan="4" style="height: 35px; font-weight: 700;">
                <asp:CheckBox ID="chkGenTodoReenvio" runat="server" Text="Generar todos los registros (Pendiente, Corregido)" AutoPostBack="True" ViewStateMode="Enabled" />
            </td>
            <td colspan="1" style="width: 678px; height: 35px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 517px; height: 35px">
                <br />
                <br />
                <br />
            </td>
            <td style="width: 282px; height: 35px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Fecha de Inicio Proceso:"></asp:Label></td>
            <td style="height: 35px; font-weight: 700;">
                <asp:TextBox ID="txFechaEfecto" runat="server" Width="93px"></asp:TextBox>
                <asp:ImageButton ID="imabtncalendario" runat="server" ImageUrl="~/Imagenes/calendario.jpg" />
                <asp:Calendar ID="Calendario" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px">
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </td>
            <td style="height: 35px; font-weight: 700;">
                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Fecha fin de Proceso:"></asp:Label>
            </td>
            <td colspan="2" style="height: 35px; font-weight: 700;">
                <asp:TextBox ID="txFechaEfectoRfin" runat="server" Width="93px"></asp:TextBox>
                <asp:ImageButton ID="imabtncalendarioRfin" runat="server" ImageUrl="~/Imagenes/calendario.jpg" />
                <asp:Calendar ID="CalendarioRfin" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px">
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </td>
            <td colspan="1" style="width: 678px; height: 35px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 37px;">
            </td>
            <td style="width: 282px; height: 37px;">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Estado :"></asp:Label></td>
            <td colspan="4" style="height: 37px" rowspan="">
                <asp:DropDownList ID="ddlEstadoEnvio" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" rowspan="" style="width: 678px; height: 37px">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="width: 678px">
            </td>
            <td style="width: 517px; ">
            </td>
            <td style="width: 149px; ">
                </td>
            <td colspan="3">
            </td>
            <td style="width: 104px; ">
                </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
            </td>
            <td align="center" colspan="4" style="height: 23px">
                &nbsp;<asp:Button ID="BtnGeneraTrama" runat="server" Text="Generar Trama X12N" Width="138px" />
            </td>
            <td style="width: 753px; height: 23px;">
                &nbsp;</td>
            <td style="width: 678px; height: 23px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 13px;">
            </td>
            <td align="center" colspan="4" style="height: 13px">
                </td>
            <td style="width: 753px; height: 13px;">
            </td>
            <td style="width: 678px; height: 13px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 30px;">
            </td>
            <td align="center" style="height: 30px; width: 1px;">
                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Archivo:"></asp:Label></td>
            <td align="center" colspan="2" style="height: 30px">
                                    <asp:TextBox ID="txtRuta" runat="server" Width="295px"></asp:TextBox></td>
            <td align="center" colspan="2" style="height: 30px">
                </td>
            <td style="width: 678px; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 36px;">
            </td>
            <td align="center" style="height: 36px; width: 1px;">
                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Comentario:"></asp:Label></td>
            <td align="center" colspan="2" style="height: 36px">
                                    <asp:TextBox ID="txtComentarioReenvio" runat="server" MaxLength="80" TabIndex="4" Width="293px" Height="28px" TextMode="MultiLine"></asp:TextBox></td>
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
            <td style="width: 753px; height: 16px;">
            </td>
            <td style="width: 678px; height: 16px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
                &nbsp;</td>
            <td align="center" colspan="4" style="height: 23px">
                <asp:Button ID="BtnGeneraTrama0" runat="server" Text="Enviar Trama X12N" Width="138px" />
            </td>
            <td style="width: 753px; height: 23px;">
                &nbsp;</td>
            <td style="width: 678px; height: 23px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
                </td>
            <td align="center" colspan="4" style="height: 23px">
                </td>
            <td style="width: 753px; height: 23px;">
                </td>
            <td style="width: 678px; height: 23px">
                </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
                &nbsp;</td>
            <td align="center" colspan="4" style="height: 23px">
                &nbsp;</td>
            <td style="width: 753px; height: 23px;">
                &nbsp;</td>
            <td style="width: 678px; height: 23px">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>

