<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RepRecibos.aspx.vb" Inherits="RepRecibos" 
        Title="Reporte de Recibos" StylesheetTheme="Tema" EnableEventValidation = "false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </cc1:ToolkitScriptManager>
    <style type="text/css">
        .GridPager a, .GridPager span
        {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }
        .GridPager a
        {
            background-color: #f5f5f5;
            color: white;
            border: 1px solid #969696;
        }
        .GridPager span
        {
            background-color: #D21F1B;
            color: white;
            border: 0px solid #3AC0F2;
        }
    </style>
    <script type="text/javascript" src="JS/jquery-1.7.2.min.js"></script>
    <script language="javascript" type="text/javascript">
    </script>
    <table id="TABLE1" style="width: 100%; height: 100%; border-top-style: groove; border-right-style: groove;
        border-left-style: groove; border-bottom-style: groove">
        <tr style="height:10px">
            <td colspan="6">
            </td>
        </tr>
        <tr style="height:20px">
            <td colspan="6" align="center">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="15pt"
                    ForeColor="#D3343D"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" style="height: 20px">
            </td>
            <td align="right" colspan="2" style="height: 20px">
            </td>
            <td align="right" colspan="1" style="width: 678px; height: 20px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px">
            </td>
            <td style="width: 200px; height: 35px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Canal :"></asp:Label></td>
            <td colspan="3" style="width: 600px; height: 35px">
                <asp:DropDownList ID="ddlCanal" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td style="width: 200px; height: 35px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px;">
            </td>
            <td style="width: 200px; height: 35px;">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Producto :"></asp:Label></td>
            <td colspan="3" style="width: 600px; height: 35px">
                <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" rowspan="" style="width: 200px; height: 35px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px">
            </td>
            <td style="width: 200px; height: 35px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="P&oacute;liza:"></asp:Label></td>
            <td style="width: 200px; height: 35px">
                <asp:DropDownList ID="ddlPoliza" runat="server" AutoPostBack="True" Width="120px">
                </asp:DropDownList></td>
            <td colspan="2" style="Width:400px; height: 35px">
            </td>
            <td colspan="1" style="width: 200px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px">
            </td>
            <td style="width: 200px; height: 35px">
                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Estado:"></asp:Label></td>
            <td style="width: 200px; height: 35px">
                <asp:DropDownList ID="ddlEstado" runat="server" Width="120px">
                    <asp:ListItem Text="Generado" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Abonado" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Anulado" Value="7"></asp:ListItem>
                    <asp:ListItem Value="33">Modificado</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" style="Width:400px; height: 35px">
            </td>
            <td style="width: 200px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px">
            </td>
            <td style="width: 200px; height: 35px">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Fecha Ini. Vig.:"></asp:Label></td>
            <td style="width: 600px;height: 35px" colspan="3" valign="middle" >
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Desde"></asp:Label>
                &nbsp; &nbsp;
                <asp:TextBox ID="txtFecInicio" runat="server" Width="80px"></asp:TextBox>
                <asp:ImageButton ID="imgCalendarIni" runat="server" ImageUrl="~/Imagenes/Calendario.jpg"/>
                <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgCalendarIni" runat="server" TargetControlID="txtFecInicio" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                &nbsp; &nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Hasta"></asp:Label>
                &nbsp; &nbsp;
                <asp:TextBox ID="txtFecFin" runat="server" Width="80px"></asp:TextBox>
                <asp:ImageButton ID="imgCalendarFin" runat="server" ImageUrl="~/Imagenes/Calendario.jpg"/>
                <cc1:CalendarExtender ID="Calendar2" PopupButtonID="imgCalendarFin" runat="server" TargetControlID="txtFecFin" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td style="width: 200px">
            </td>
        </tr>
        <tr style="height:10px">
            <td colspan="6">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 23px;">
            </td>
            <td align="center" colspan="4" style="width: 600px; height: 23px">
                <asp:ImageButton ID="imbAceptar1" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" style="height: 22px" />
                &nbsp; &nbsp;
                <asp:ImageButton ID="imbCancelar" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
            <td style="width: 200px; height: 23px">
            </td>
        </tr>
        <tr>
            <td style="height: 10px;" colspan="6">
            </td>
        </tr>
        <tr>
            <td colspan="6" >
                <table style="width: 100%; height: 100%" runat="server" id="Tab_ReporteRecibos">
                    <tr>
                        <td style="width: 50px; height: 20px;">
                        </td>
                        <td style="height: 20px">
                        </td>
                        <td style="height: 20px;width: 50px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px">
                        </td>
                        <td class="degradado" valign="top" style="width:900px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="300px">
                                        <tr>
                                            <td style="width:140px">
                                                <asp:Label runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                Text="Total Registro:"></asp:Label>&nbsp;
                                                <asp:Label ID="lblNumListRecibos" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                Text="0"></asp:Label> 
                                            </td>
                                            <td style="width:10px"></td>
                                            <td style="width:150px">
                                                <asp:Label runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Mostrar filas:" />&nbsp;
                                                <asp:DropDownList ID="ddlListRecibos" runat="server" AutoPostBack="true" Font-Size="11px">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15" />
                                                    <asp:ListItem Value="20" />
                                                    <asp:ListItem Value="25" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="PanelReport" runat="server" BackColor="#F7F6F3" Font-Size="Small" Width="100%">
                                        <asp:GridView ID="gvwRepRecibos" runat="server" SkinID="SampleGridView" AutoGenerateColumns="False" 
                                            Width="100%" Font-Name="Arial" Font-Size="0.8em" AllowPaging="True" Font-Names="Arial" >
                                            <PagerStyle HorizontalAlign = "Right" CssClass="GridPager" BackColor="White" ForeColor="Black"/>
                                            <PagerSettings Mode="NumericFirstLast"/>
                                            <Columns>
                                                <asp:BoundField DataField="NTYPE" HeaderText="Tipo Recibo" SortExpression="TIPO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SPROFORMA" HeaderText="Nro. Aviso Cobranza" SortExpression="PROFORMA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NPOLICY" HeaderText="Póliza" SortExpression="POLIZA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCLIENT" HeaderText="Cliente" SortExpression="CLIENT" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="240px">
                                                <ItemStyle HorizontalAlign="Left" Width="240px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DEFFECDATE" HeaderText="Fecha Efecto" SortExpression="FECHAEFECTO" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DSTARTDATE" HeaderText="Fecha Inicio" SortExpression="FECHAINICIO" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="DEXPIRDAT" HeaderText="Fecha Vencimiento" SortExpression="FECHAFIN" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>--%>
                                                <asp:BoundField DataField="NCURRENCY" HeaderText="Moneda" SortExpression="MONEDA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NNETO_PROF" HeaderText="Monto Neto" SortExpression="NETO" DataFormatString="{0:0.0000}" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NDE_PROF" HeaderText="D.E." SortExpression="DE" DataFormatString="{0:0.0000}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NIGV_PROF" HeaderText="I.G.V." SortExpression="IGV" DataFormatString="{0:0.0000}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NBRUTA_PROF" HeaderText="Monto Bruto" SortExpression="BRUTA" DataFormatString="{0:0.0000}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BROKER" HeaderText="Broker" SortExpression="BROKER" ItemStyle-HorizontalAlign="left" ItemStyle-Width="120px">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 50px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px; height: 23px;">
                        </td>
                        <td align="center" style="height: 23px">
                            <asp:ImageButton ID="ImprimirRecibos" runat="server" ImageUrl="~/Imagenes/Imprimir.gif" style="height: 22px"/>
                        </td>
                        <td style="height: 23px;width: 50px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="3">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFecInicio" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFecFin" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
</asp:Content>