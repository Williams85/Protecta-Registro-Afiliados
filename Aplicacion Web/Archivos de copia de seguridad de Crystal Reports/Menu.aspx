<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Menu.aspx.vb" Inherits="Menu" title="Untitled Page" Theme="Tema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 99%; height: 90%">
        <tr>
            <td colspan="3">
                <asp:Menu ID="MenuPrincipal" runat="server" BackColor="#E3EAEB" DynamicHorizontalOffset="2"
                    Font-name="verdana" Font-Size="0.9em" ForeColor="#666666" Orientation="Horizontal"
                    StaticSubMenuIndent="10px">
                    <StaticSelectedStyle BackColor="#E3EAEB" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicHoverStyle BackColor="#D3343D" ForeColor="White" />
                    <DynamicMenuStyle BackColor="White"/>
                    <DynamicSelectedStyle BackColor="White" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle BackColor="#D3343D" ForeColor="White" />
                    <Items>
                        <asp:MenuItem Text="Productos VIDA" Value="VIDA">
                            <asp:MenuItem Text="Validador de archivos" Value="mnuValArchivos" NavigateUrl="Procesos.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Generaci&#243;n de recibos" Value="mnuProcesoRenovacion" NavigateUrl="Renovaciones.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Carga de Asegurados" Value="mnuCargaMasivaAsegurados" NavigateUrl="CargaMasivaAsegurados.aspx"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="Productos SCTR" Value="SCTR">
                            <asp:MenuItem Text="Tipo de Movimiento/Endosos">
                                <asp:MenuItem Text="Emisi&#243;n" Value="Emision" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=EM"></asp:MenuItem>
                                <asp:MenuItem Text="Inclusi&#243;n" Value="Inclusion" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=EM"></asp:MenuItem>
                                <asp:MenuItem Text="Exclusi&#243;n" Value="Exclusion" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=EX"></asp:MenuItem>
                                <asp:MenuItem Text="Renovaci&#243;n" Value="Renovacion" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=EM"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Actualizaci&#243;n de datos de asegurados" Value="Asegurados" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=AS"></asp:MenuItem>
                            <asp:MenuItem Text="Tarifario" Value=""></asp:MenuItem>
                            <asp:MenuItem Text="Carga Documentos" Value="">
                                <asp:MenuItem Text="Proformas" Value="Proformas" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=PR"></asp:MenuItem>
                                <asp:MenuItem Text="Facturas/Nota de credito" Value="Facturas" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=DC"></asp:MenuItem>
                                <asp:MenuItem Text="Comisiones" Value="Comisiones" NavigateUrl="ProcesosSCTR.aspx?MovSTRC=CO"></asp:MenuItem>
                            </asp:MenuItem>
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px; padding-top: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 26px">
            </td>
            <td align="center" style="width: 100px; height: 26px">
                </td>
            <td style="width: 100px; height: 26px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td align="center" style="width: 100px">
                </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px; padding-top: 200px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
   
    
   
</asp:Content>

