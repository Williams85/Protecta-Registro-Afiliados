<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RepVISANET.aspx.vb" Inherits="RptVISANET" 
    Title="Reporte Control de Pagos CAFAE" StylesheetTheme="Tema" EnableEventValidation = "false"%>
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
        .auto-style1 {
            height: 35px;
        }
    </style>
    <script type="text/javascript" src="JS/jquery-1.7.2.min.js"></script>
    <script language="javascript" type="text/javascript">
    </script>
    <table id="TABLE1" style="width: 100%; height: 100%; border-top-style: groove; border-right-style: groove;
        border-left-style: groove; border-bottom-style: groove">
        <tr style="height:10px">
            <td colspan="5">
            </td>
        </tr>
        <tr style="height:20px">
            <td colspan="5" align="center">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="15pt"
                    ForeColor="#D3343D"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" style="height: 20px">
            </td>
            <td align="right" style="height: 20px">
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
            <td colspan="2" style="width: 600px; height: 35px">
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
            <td colspan="2" style="width: 600px; height: 35px">
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
                    Text="Código de Comercio:"></asp:Label></td>
            <td class="auto-style1" colspan="2">
                <asp:DropDownList ID="ddlComercio" runat="server" AutoPostBack="True" Width="354px" Height="16px">
                </asp:DropDownList></td>
            <td colspan="1" style="width: 200px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px">
            </td>
            <td style="width: 200px; height: 35px">
                <asp:Label ID="LblFecha" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Fecha de Abono:"></asp:Label></td>
            <td style="width: 600px;height: 35px" colspan="2" valign="middle" >
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
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 23px;">
            </td>
            <td align="center" colspan="3" style="width: 600px; height: 23px">
                <asp:ImageButton ID="imbAceptar1" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" style="height: 22px" />
                &nbsp; &nbsp;
                <asp:ImageButton ID="imbCancelar" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
            <td style="width: 200px; height: 23px">
            </td>
        </tr>
        <tr>
            <td style="height: 10px;" colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" >
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
                                    <asp:Panel ID="PanelReport" runat="server" BackColor="#F7F6F3" Font-Size="Small" Width="100%" ScrollBars="Horizontal">
                                        <asp:GridView ID="gvwRepVisanet" runat="server" SkinID="SampleGridView" AutoGenerateColumns="False" 
                                            Width="100%" Font-Name="Arial" Font-Size="0.8em" AllowPaging="True" Font-Names="Arial" >
                                            <PagerStyle HorizontalAlign = "Right" CssClass="GridPager" BackColor="White" ForeColor="Black"/>
                                            <PagerSettings Mode="NumericFirstLast"/>
                                            <Columns>
                                                <asp:BoundField DataField="PRODUCTO" HeaderText="Producto" SortExpression="PRODUCTO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="POLIZA" HeaderText="Póliza" SortExpression="POLIZA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="COD. COMERCIO" HeaderText="Cod. Comercio" SortExpression="COD. COMERCIO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NOMBRE COMERCIAL" HeaderText="Nombre Comercial" SortExpression="NOMBRE COMERCIAL">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FEC. DE ABONO" HeaderText="Fecha Abono" SortExpression="FEC. DE ABONO" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FEC. DE TRANSACCIÓN" HeaderText="Fecha Transacción" SortExpression="FEC. DE TRANSACCIÓN" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TIPO OPERACION" HeaderText="Tipo de Operación" SortExpression="TIPO OPERACION" ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ORIGEN TARJETA" HeaderText="Ori. Tarjeta" SortExpression="ORIGEN TARJETA" ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TIPO TARJETA" HeaderText="Tipo Tarjeta" SortExpression="TIPO TARJETA" ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda" SortExpression="MONEDA" ItemStyle-HorizontalAlign="center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IMPORTE TRANSACCION" HeaderText="Imp. Transacción" SortExpression="IMPORTE TRANSACCION" DataFormatString = "{0:0.0000}" ItemStyle-HorizontalAlign="center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="COMISION TOTAL" HeaderText="Comisión Total" SortExpression="COMISION TOTAL" DataFormatString = "{0:0.0000}" ItemStyle-HorizontalAlign="center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="COMISION GRAVABLE" HeaderText="Comisión Gravable" SortExpression="COMISION GRAVABLE" ItemStyle-HorizontalAlign="left"  ItemStyle-Width="60px" DataFormatString="{0:0.0000}">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IMPORTE IGV" HeaderText="Importe IGV" SortExpression="IMPORTE IGV" ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" DataFormatString="{0:0.0000}">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NETO A ABONAR" HeaderText="Neto a Abonar" SortExpression="NETO A ABONAR" DataFormatString = "{0:0.0000}" ItemStyle-HorizontalAlign="center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" ItemStyle-HorizontalAlign="center" ItemStyle-Width="100px">
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BANCO" HeaderText="Banco" SortExpression="BANCO" ItemStyle-HorizontalAlign="center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RECIBO" HeaderText="Recibo" SortExpression="RECIBO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="left" Width="80px" />
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FACTURA" HeaderText="Factura" SortExpression="FACTURA" ItemStyle-HorizontalAlign="center" ItemStyle-Width="100px">
                                                <ItemStyle HorizontalAlign="center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FEC. FACTURA" HeaderText="Fec. Factura" SortExpression="FEC. FACTURA" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NUM. OPERACIÓN BANCO" HeaderText="Número de Operación" SortExpression="NUM. OPERACIÓN BANCO"  ItemStyle-HorizontalAlign="center" ItemStyle-Width="60px">
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
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
                            <asp:ImageButton ID="ImprimirVISANET" runat="server" ImageUrl="~/Imagenes/Imprimir.gif" style="height: 22px"/>
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