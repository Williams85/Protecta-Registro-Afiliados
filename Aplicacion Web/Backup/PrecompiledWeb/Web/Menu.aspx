<%@ page language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" inherits="Menu, App_Web_pdp4hctj" title="Untitled Page" theme="Tema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 99%; height: 90%">
        <tr>
            <td colspan="3">
                <asp:Menu ID="MenuPrincipal" runat="server" BackColor="#E3EAEB" DynamicHorizontalOffset="2"
                    Font-Names="Verdana" Font-Size="0.8em" ForeColor="#666666" Orientation="Horizontal"
                    StaticSubMenuIndent="10px">
                    <StaticSelectedStyle BackColor="#1C5E55" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
                    <DynamicMenuStyle BackColor="#E3EAEB" />
                    <DynamicSelectedStyle BackColor="#1C5E55" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle BackColor="#666666" ForeColor="White" />
                    <Items>
                        <asp:MenuItem Text="Validador de archivos" Value="mnuValArchivos"></asp:MenuItem>
                        <asp:MenuItem Text="Generaci&#243;n de recibos" Value="mnuProcesoRenovacion"></asp:MenuItem>
                        <asp:MenuItem Text="Carga de Asegurados" Value="mnuCargaMasivaAsegurados"></asp:MenuItem>
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

