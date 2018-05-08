﻿<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Menu.aspx.vb" Inherits="Menu" title="Untitled Page" Theme="Tema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 95%;">
        <tr>
            <td colspan="3" valign="top">
                <asp:Menu ID="MenuPrincipal" runat="server" DynamicHorizontalOffset="2"
                    Font-name="verdana" Font-Size="0.9em" Orientation="Horizontal" Width="300"
                    StaticSubMenuIndent="10px">
                    <StaticSelectedStyle BackColor="#D21F1B" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" ForeColor="White" BackColor="#D21F1B" Font-Bold="True" Font-Names="Arial" Font-Size="Small" Height="25" />
                    <StaticHoverStyle BackColor="#D21F1B" ForeColor="Black" />
                    <DynamicHoverStyle BackColor="#D21F1B" ForeColor="White" />
                    <DynamicMenuStyle BackColor="White" BorderStyle="Ridge" BorderWidth="1px" BorderColor="#D21F1B" />
                    <DynamicSelectedStyle BackColor="White" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" ForeColor="Black" />
                    <Items>
                        <asp:MenuItem Text="Productos VIDA" Value="VIDA">
                            <asp:MenuItem Text="Emisión Pólizas Vida Ley" Value="PolizaMatrizGrupal"></asp:MenuItem>
                            <asp:MenuItem Text="Carga de Asegurados" Value="mnuCargaMasivaAsegurados" ></asp:MenuItem>

                            <asp:MenuItem Text="Proceso de Carga" Value="Nuevo Proceso de Carga">
                                <asp:MenuItem Text="Validador de archivos" Value="mnuNuevoProcesoCarga"></asp:MenuItem>
                                <asp:MenuItem Text="Generación de recibos" Value="mnuNuevaGeneracionRecibos"></asp:MenuItem>
                            </asp:MenuItem>

                        </asp:MenuItem>
                        <asp:MenuItem Text="Productos SCTR" Value="SCTR">
                            <asp:MenuItem Text="Emisión Póliza Matriz" Value="PolizaMatriz"></asp:MenuItem>
                            <asp:MenuItem Text="Tipo de Movimiento" Value="DocEmisiones">
                                <asp:MenuItem Text="Emisión" Value="Emision"></asp:MenuItem>
                                <asp:MenuItem Text="Inclusión" Value="Inclusion"></asp:MenuItem>
                                <asp:MenuItem Text="Exclusión" Value="Exclusion"></asp:MenuItem>
                                <asp:MenuItem Text="Renovación" Value="Renovacion"></asp:MenuItem>
                                <asp:MenuItem Text="Inclusión por Exclusión" Value="IncxExclu"></asp:MenuItem>
                                <asp:MenuItem Text="Anulación de Movimientos" Value="Movimientos"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Actualización Datos de Asegurados" Value="Asegurados"></asp:MenuItem>
                            <asp:MenuItem Text="Carga Documentos" Value="DocContables">
                                <asp:MenuItem Text="Aviso de Cobranza" Value="Recibos"></asp:MenuItem>
                                <asp:MenuItem Text="Aviso de Cobranza Regula" Value="RecibosRegula"></asp:MenuItem>
                                <asp:MenuItem Text="Comisiones" Value="Comisiones"></asp:MenuItem>
                                <asp:MenuItem Text="Factura" Value="Factura"></asp:MenuItem>
                                <asp:MenuItem Text="Nota de Cr&eacute;dito" Value="NotaCredito"></asp:MenuItem>
                                <asp:MenuItem Text="Estado de Comisiones" Value="EstadoComisiones"></asp:MenuItem>
                                <asp:MenuItem Text="Estado de Documentos" Value="DocumentosEstados"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Reportes" Value="Reportes">
                                <asp:MenuItem Text="Movimientos" Value="RepEndosos"></asp:MenuItem>
                                <asp:MenuItem Text="Avisos de Cobranza" Value="RepRecibos"></asp:MenuItem>
                                <asp:MenuItem Text="Documentos Contables" Value="RepDC"></asp:MenuItem>
                                <asp:MenuItem Text="Comisiones" Value="RepCO"></asp:MenuItem>
                            </asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="Productos CAFAE" Value="CAFAE">
                            <asp:MenuItem Text="Carga de Tramas Vigentes" Value="CAFAEVigente"></asp:MenuItem>
                            <asp:MenuItem Text="Carga de Tramas Vencidas" Value="CAFAEVencido"></asp:MenuItem>
                            <asp:MenuItem Text="Generación de Comprobantes" Value="GenRecibosFam"></asp:MenuItem>
                            <asp:MenuItem Text="Liquidación de Comprobantes" Value="LiqRecibosFam"></asp:MenuItem>
                            <asp:MenuItem Text="Reporte Control de Pagos Asegurados CAFAE" Value="RepPagosCAFAE"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="Comisiones VISANET" Value="VISANET">
                            <asp:MenuItem Text="Carga Comisiones VISANET" Value="ComisionesVISANET"></asp:MenuItem>
                            <asp:MenuItem Text="Reporte de Comisiones" Value="ReporteVISANET"></asp:MenuItem>
                            <asp:MenuItem Text="Reporte de Fact. Pendientes" Value="ReporteDocPendientes"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="SETIAF - SUSALUD" Value="REGAFI">
                            <asp:MenuItem Text="Minibatch" Value="menMinibatch"></asp:MenuItem>
                            <asp:MenuItem Text="Reenvio" Value="menReenvio"></asp:MenuItem>
                            <asp:MenuItem Text="Actualización de asegurados" Value="menActuDatosAseg"></asp:MenuItem>
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td style="width: 100px"></td>
            <td style="width: 100px; padding-top: 100px"></td>
            <td style="width: 100px"></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 26px"></td>
            <td align="center" style="width: 100px; height: 26px"></td>
            <td style="width: 100px; height: 26px"></td>
        </tr>
        <tr>
            <td style="width: 100px"></td>
            <td align="center" style="width: 100px"></td>
            <td style="width: 100px"></td>
        </tr>
        <tr>
            <td style="width: 100px"></td>
            <td style="width: 100px; padding-top: 200px">&nbsp;</td>
            <td style="width: 100px; text-align: right;">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

