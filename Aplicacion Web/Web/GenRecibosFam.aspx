<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="GenRecibosFam.aspx.vb" Inherits="GenRecibosFam" Title="Generacion de Recibos Familiares" StylesheetTheme="Tema"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="3600"></cc1:ToolkitScriptManager>
    <script type="text/javascript" src="JS/jquery-1.7.2.min.js"></script>
    <script language="javascript" type="text/javascript">
    </script>
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
            width: 20px;
            height: 61px;
        }
        .auto-style2 {
            height: 61px;
        }
    </style>  
    <script type="text/javascript" language="Javascript">
        function MostrarEspera() {
            $find('mpe').hide();
            var elem = document.getElementById("DivProcessMessage");
            elem.style.width = "100%";
            elem.style.height = "150%";
            elem.style.display = "block";
            return true;
        }
        function MostrarEspera2() {
            var elem = document.getElementById("DivProcessMessage");
            elem.style.width = "100%";
            elem.style.height = "150%";
            elem.style.display = "block";
            return true;
        }

        function CerrarEspera() {
            var elem = document.getElementById("DivProcessMessage");
            elem.style.display = "none";
            return true;
        }
        </script>
     
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
            <div style="position:absolute; display:none; left:0px; background-color: rgba(232, 221, 219, 0.4);" id="DivProcessMessage">
                <div style="TEXT-ALIGN: center; width: 700px; height: 100px; left: 0px; background-color:white; margin-left:20%;margin-top:15%; border: 1px; " ><b>Cargando...</b><br />
                    <img src="imagenes/progress_rojo.gif" /><br /></div>
            </div>
            <input style="LEFT: 22px; WIDTH: 94px; POSITION: absolute; TOP: 1292px" id="hide" type="hidden" runat="server" />  
            <table>
                    <tr>
                        <td style="FONT-WEIGHT: bold; FONT-SIZE: large; FONT-FAMILY: Tahoma; HEIGHT: 88px; color: #000099;" align="center" colspan="9">Proceso de Generación de Recibos Individuales Familiares<br /> Facturación Serie 006</td></TR><TR><TD style="FONT-WEIGHT: bold; FONT-SIZE: small; FONT-FAMILY: Tahoma; HEIGHT: 26px" colSpan="2">&nbsp;
                        </td>
                        <td style="WIDTH: 371px; HEIGHT: 26px"></td>
                        <td style="FONT-WEIGHT: bold; FONT-SIZE: small; WIDTH: 100px; FONT-FAMILY: Tahoma; HEIGHT: 26px">Producto:</td>
                        <td style="WIDTH: 97px; HEIGHT: 26px">
                            <asp:DropDownList id="ddlProducto" runat="server" AutoPostBack="True" Width="264px">
                            </asp:DropDownList>
                        </td>
                        <td style="FONT-WEIGHT: bold; FONT-SIZE: small; WIDTH: 519px; FONT-FAMILY: Tahoma; HEIGHT: 26px" colspan="1" rowspan="1">&nbsp;</TD><TD style="WIDTH: 108px; HEIGHT: 26px">&nbsp;</TD><TD style="WIDTH: 69px; HEIGHT: 26px">&nbsp;</TD><TD style="WIDTH: 100px; HEIGHT: 26px"></TD></TR><tr><TD colSpan=2>&nbsp;</TD><TD style="FONT-WEIGHT: bold; FONT-SIZE: medium; WIDTH: 371px">&nbsp;</TD><TD style="WIDTH: 100px; font-weight: bold; font-size: small; font-family: Tahoma; height: 26px;">Póliza:</TD><td style="WIDTH: 97px">
                            <asp:DropDownList ID="ddlPoliza" runat="server" AutoPostBack="True" Width="159px">
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" rowspan="1" align="center">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="71px" OnClientClick="MostrarEspera2();"/>
                        </td>
                        <td style="WIDTH: 108px"></td>
                        <td style="WIDTH: 69px"></td><td style="WIDTH: 100px"></td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 21px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 27px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 371px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 100px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 97px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 519px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 108px; HEIGHT: 14px">&nbsp;</td>
                        <td style="WIDTH: 69px; HEIGHT: 14px"></td>
                        <td style="WIDTH: 100px; HEIGHT: 14px"></td>
                    </tr>
                    <tr>
                        <td colspan="9" align="center">
                            <div style=" WIDTH: 1023px; HEIGHT: 352px; BACKGROUND-COLOR: whitesmoke"> 
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table width="300px">
                                        <tr>
                                            <td style="width: 140px" align="right">
                                               
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 150px">
                                                <asp:Label runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Mostrar filas:" />&nbsp;
                                                <asp:DropDownList ID="ddlList" runat="server" AutoPostBack="true" Font-Size="11px">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15" />
                                                    <asp:ListItem Value="20" />
                                                    <asp:ListItem Value="25" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                            <asp:Panel ID="PanelReport" runat="server" BackColor="#F7F6F3" Font-Size="Small" Width="100%">
                            <asp:GridView id="gvDetalle"  runat="server" SkinID="SampleGridView" AutoGenerateColumns="False" Width="91%" Font-Name="Arial "
                            Font-Size="10pt" Font-Names="Arial" EmptyDataText="No se ha encontrado registros." AllowPaging="True">
                            <PagerSettings Mode="NumericFirstLast" />
                                <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox id="ChkSel" runat="server"></asp:CheckBox> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="Cantidad_Cert" HeaderText="Cantidad de Recibos">
                                    <ItemStyle Width="700px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="Asegurado" HeaderText="Asegurados">
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:BoundField>
                                    <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                    <ItemStyle HorizontalAlign="Left" Width="500px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="NETO" DataFormatString=" {0:0,0.00}" HeaderText="Prima Neta">
                                    <ItemStyle HorizontalAlign="Right" Width="600px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IGV" HeaderText="IGV" DataFormatString=" {0:0,0.00}">
                                    <ItemStyle HorizontalAlign="Right" Width="600px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DE" HeaderText="D.E." DataFormatString=" {0:0,0.00}">
                                    <ItemStyle HorizontalAlign="Right" Width="600px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Bruto" DataFormatString=" {0:0,0.00}" HeaderText="Prima Bruta">
                                    <ItemStyle HorizontalAlign="Right" Width="600px" />
                                </asp:BoundField>
                                </Columns>
                            <PagerStyle BackColor="White" CssClass="GridPager" ForeColor="Black" HorizontalAlign="Right" />
                            </asp:GridView>
                        </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
        </div>
        <br />
        </td>
</tr>
<tr>
    <td style="WIDTH: 21px"></td>
    <td style="WIDTH: 21px"></td>
    <td style="WIDTH: 21px" colspan="3">
        <asp:RadioButtonList ID="rblmarcar" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" RepeatDirection="Horizontal" Width="256px" OnClientClick="MostrarEspera2();">
            <asp:ListItem Value="0">Marcar Todos</asp:ListItem>
            <asp:ListItem Value="1">Desmarcar Todos</asp:ListItem>
        </asp:RadioButtonList>
    </td>

</tr>
        <tr>
            <td class="auto-style1"></td>
            <td colspan="4" class="auto-style2">
                <table style="WIDTH: 304px">
                    <tbody>
                        <tr>
                            <td style="WIDTH: 108px">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" Text="Total de N° de registros :" Width="173px"></asp:Label>
                            </td>
                            <td style="WIDTH: 100px">
                                <asp:TextBox ID="txtNumReg" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 108px">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" Text="Total Prima:" Width="108px"></asp:Label>
                            </td>
                            <td style="WIDTH: 100px">
                                <asp:TextBox ID="txtTotalPrima" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td style="WIDTH: 21px"></td>
            <td style="WIDTH: 27px"></td>
            <td style="WIDTH: 371px">&nbsp;</td>
            <td colspan="2" align="right">
                &nbsp;</td>
            <td colspan="1" align="right">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" Text="Fecha de Facturación:"></asp:Label>
                </td>
            <td style="WIDTH: 519px">
                            <asp:TextBox ID="txtFactura" runat="server" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="imgCalendarFac" runat="server" ImageUrl="~/Imagenes/Calendario.jpg" />
                            <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgCalendarFac" runat="server" TargetControlID="txtFactura" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                </td>
            <td style="WIDTH: 108px">
                &nbsp;</td>
            <td style="WIDTH: 69px"></td>
        </tr>
        <tr>
            <td style="WIDTH: 21px">&nbsp;</td>
            <td style="WIDTH: 27px">&nbsp;</td>
            <td style="WIDTH: 371px">&nbsp;</td>
            <td align="right" colspan="2">&nbsp;</td>
            <td align="right" colspan="1">&nbsp;</td>
            <td style="WIDTH: 519px">&nbsp;</td>
            <td style="WIDTH: 108px">&nbsp;</td>
            <td style="WIDTH: 69px">&nbsp;</td>
        </tr>
        <tr>
            <td style="WIDTH: 21px; HEIGHT: 27px"></td>
            <td style="WIDTH: 27px; HEIGHT: 27px"></td>
            <td style="WIDTH: 371px; HEIGHT: 27px"></TD>
            <td style="WIDTH: 100px; HEIGHT: 27px"></td>
            <td align="center">
                <asp:Button id="btnGrabar" runat="server" Text="Grabar" Width="105px" OnClientClick="MostrarEspera2();"></asp:Button>
            </td>
            <td align="center">
                <asp:Button ID="btnSalir" runat="server" Text="Salir" Width="105px" />
            </td>
            <td style="WIDTH: 108px; HEIGHT: 27px"></td>
            <td style="WIDTH: 69px; HEIGHT: 27px"></td>
            <td style="WIDTH: 100px; HEIGHT: 27px"></td>
        </tr>
        <tr>
            <td style="WIDTH: 21px; HEIGHT: 20px"></td>
            <td style="WIDTH: 27px; HEIGHT: 20px"></td>
            <td style="WIDTH: 371px; HEIGHT: 20px"></td>
            <td style="WIDTH: 100px; HEIGHT: 20px"></td>
            <td style="WIDTH: 97px; HEIGHT: 20px"></td>
            <td style="WIDTH: 519px; HEIGHT: 20px"></td>
            <td style="WIDTH: 108px; HEIGHT: 20px"></td>
            <td style="WIDTH: 69px; HEIGHT: 20px"></td>
            <td style="WIDTH: 100px; HEIGHT: 20px"></td>
        </tr>
        <tr>
            <td colspan=" 10">
                
                <asp:TextBox ID="TxtUsuario" runat="server" Visible="False"></asp:TextBox>
                
            </td>
        </tr>
    </td>
</table>
    
    <%--<asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div id="progressBackgroundFilter"></div>
            <div id="processMessage" style="TEXT-ALIGN: center">
                Procesando...<br /><img src="imagenes/progress_rojo.gif" /><br /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <input id="hidden2" runat="server" type="hidden" />
    <input id="hidden3" runat="server" type="hidden" />
<div style="visibility:hidden">
</div>

<asp:Panel ID="PnelCarga" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px"
Height="114px" Width="326px">
    <table>
        <tr>
            <td align="center" colspan="4" style="font-weight: bold; font-size: small; font-family: Tahoma;
                height: 25px;">
            </td>
        </tr>
        <tr>
            <td style="width: 147px">
            </td>
            <td align="center" colspan="2" rowspan="2" valign="top">
                <asp:Label ID="lblMensajeProceso" runat="server" Font-Names="Tahoma" Font-Size="8pt"
                    ForeColor="#404040"></asp:Label></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 147px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 147px">
            </td>
            <td style="width: 100px">
                <asp:ImageButton ID="imbAceptarCarga" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClientClick="MostrarEspera();"/></td>
            <td style="width: 100px">
                <asp:ImageButton ID="imbCancelarCarga" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
    <asp:Label ID="lblCarga" runat="server"> </asp:Label>
</asp:Panel>
    <cc1:ModalPopupExtender ID="mpeCarga" runat="server" BackgroundCssClass="modalBackground"
    Drag="True" PopupControlID="PnelCarga" TargetControlID="lblCarga" BehaviorID="mpe">
    </cc1:ModalPopupExtender>

</contenttemplate>
    <triggers>
    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click"></asp:AsyncPostBackTrigger>
    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"></asp:AsyncPostBackTrigger>
    </triggers>
</asp:UpdatePanel>
</asp:Content>