<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RepEndosos.aspx.vb" Inherits="RepEndosos" 
        Title="Reporte de Endosos" StylesheetTheme="Tema" EnableEventValidation = "false" %>

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
        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=gvwRepEndosos.ClientID%>');
            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
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
            <td colspan="3" style="width: 600px; height: 35px" rowspan="">
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
                    <asp:ListItem Text="Activo" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Bloqueado" Value="B"></asp:ListItem>
                    <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
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
                <asp:Label ID="lblRuc" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Ruc:"></asp:Label></td>
            <td colspan="3" style="width: 600px; height: 35px" rowspan="">
                <asp:TextBox ID="txtRuc" runat="server" Width="150px" MaxLength="11" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </td> 
            <td style="width: 200px">
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 35px">
            </td>
            <td style="width: 200px; height: 35px">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Fecha Ini. Vig. :"></asp:Label></td>
            <td style="width: 600px;height: 35px" colspan="3" valign="middle"  >
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Desde"></asp:Label>
                &nbsp; &nbsp;
                <asp:TextBox ID="txtFecEfectoIni" runat="server" Width="80px"></asp:TextBox>
                <asp:ImageButton ID="imgCalendarIni" runat="server" ImageUrl="~/Imagenes/Calendario.jpg"/>
                <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgCalendarIni" runat="server" TargetControlID="txtFecEfectoIni" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                &nbsp; &nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Hasta"></asp:Label>
                &nbsp; &nbsp;
                <asp:TextBox ID="txtFecEfectoFin" runat="server" Width="80px"></asp:TextBox>
                <asp:ImageButton ID="imgCalendarFin" runat="server" ImageUrl="~/Imagenes/Calendario.jpg"/>
                <cc1:CalendarExtender ID="Calendar2" PopupButtonID="imgCalendarFin" runat="server" TargetControlID="txtFecEfectoFin" Format="dd/MM/yyyy">
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
                <table style="width: 100%; height: 100%" runat="server" id="Tab_ReporteEndoso">
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
                            <asp:UpdatePanel ID="udpLisEndosos" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <table width="300px">
                                        <tr>
                                            <td style="width:140px">
                                                <asp:Label runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                Text="Total Registro:"></asp:Label>&nbsp;
                                                <asp:Label ID="lblNumListEndosos" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                Text="0"></asp:Label> 
                                            </td>
                                            <td style="width:10px"></td>
                                            <td style="width:150px">
                                                <asp:Label runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" Text="Mostrar filas:" />&nbsp;
                                                <asp:DropDownList ID="ddlListEndosos" runat="server" AutoPostBack="true" Font-Size="11px">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15" />
                                                    <asp:ListItem Value="20" />
                                                    <asp:ListItem Value="25" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="PanelReport" runat="server" BackColor="#F7F6F3" Font-Size="Small" Width="100%">
                                        <asp:GridView ID="gvwRepEndosos" runat="server" SkinID="SampleGridView" AutoGenerateColumns="False" Width="100%" Font-Name="Arial" 
                                            AllowPaging="True" Font-Size="0.8em" Font-Names="Arial" >
                                            <PagerSettings Mode="NumericFirstLast"/>
                                            <PagerStyle HorizontalAlign = "Right" CssClass="GridPager" BackColor="White" ForeColor="Black"/>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="SuppliersGroup" onclick = "checkRadioBtn(this);"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="POLIZA" HeaderText="P&oacute;liza" SortExpression="POLIZA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCLIENT" HeaderText="RUC" SortExpression="SCLIENT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCLIENAME" HeaderText="Raz&oacute;n Social" SortExpression="SCLIENAME" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="120px">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RENOVACION" HeaderText="Renovaci&oacute;n" SortExpression="RENOVACION" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="ENDOSO" HeaderText="Endoso" SortExpression="ENDOSO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>--%>
                                                <asp:BoundField DataField="TIPO_ENDOSO" HeaderText="Tipo Endoso" SortExpression="TIPO_ENDOSO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="220px">
                                                <ItemStyle HorizontalAlign="Center" Width="220px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CERTIFICADOS" HeaderText="Certificados" SortExpression="CERTIFICADOS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda" SortExpression="MONEDA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PREMIUM" HeaderText="Prima Neta" SortExpression="PRIMA" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DEFFECDATE" HeaderText="Fecha Efecto" SortExpression="FECEFECTO" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DSTARTDATE" HeaderText="Fecha Inicio" SortExpression="FECINICIO" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DEXPIRDAT" HeaderText="Fecha Expiraci&oacute;n" SortExpression="FECEXPIR" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NUM_MOV" HeaderText="Movimiento" SortExpression="NUM_MOV" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESTADO_MOV" HeaderText="Estado Movimiento" SortExpression="ESTADO_MOV" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                            <asp:ImageButton ID="ImprimirEndosos" runat="server" ImageUrl="~/Imagenes/Imprimir.gif" style="height: 22px"/>
                        </td>
                        <td style="height: 23px;width: 50px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px">
                        </td>
                        <td class="degradado" valign="top" style="width:900px">
                                <asp:UpdatePanel ID="udpLisDetEndosos" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <table width="300">
                                            <tr>
                                                <td style="width:140px">
                                                    <asp:Label ID="lblTotalDetEndosos" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Total Registro:" Visible="false" ></asp:Label>&nbsp;
                                                    <asp:Label ID="lblNumDetEndosos" runat="server" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="0" Visible="false" ></asp:Label> 
                                                </td>
                                                <td style="width:10px"></td>
                                                <td style="width:150px">
                                                    <asp:Label ID="lblFilDetEndosos" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" 
                                                        Text="Mostrar filas:" Visible="false" />&nbsp;
                                                    <asp:DropDownList ID="ddlListDetEndosos" runat="server" AutoPostBack="true" Font-Size="11px" Visible="false" >
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15" />
                                                        <asp:ListItem Value="20" />
                                                        <asp:ListItem Value="25" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    <asp:Panel ID="PanelReport2" runat="server" BackColor="#F7F6F3" Font-Size="Small" Width="100%">
                                        <asp:GridView ID="gvwRepEndososDet" runat="server" SkinID="SampleGridView" AutoGenerateColumns="False" Width="100%" 
                                            AllowPaging="True" Font-Name="Arial" Font-Size="0.8em" Font-Names="Arial">
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <PagerStyle HorizontalAlign = "Right" CssClass="GridPager" BackColor="White" ForeColor="Black"/>
                                            <Columns>
                                                <asp:BoundField DataField="CERTIFICADO" HeaderText="Certificados" SortExpression="CERTIFICADOS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ASEGURADO" HeaderText="Asegurados" SortExpression="ASEGURADOS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="220px">
                                                <ItemStyle HorizontalAlign="Left" Width="220px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SALARIO" HeaderText="Salario" SortExpression="SALARIO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.00}" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MODULO" HeaderText="M&oacute;dulo" SortExpression="MODULO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TASA" HeaderText="Tasa" SortExpression="TASA" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0000}" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRIMA" HeaderText="Prima Neta" SortExpression="PRIMA" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.00}" ItemStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INIVIGENCIA" HeaderText="Inicio Vigencia" SortExpression="INICIOVIGENCIA" ItemStyle-HorizontalAlign="Center" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-Width="70px">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FINVIGENCIA" HeaderText="Fin Vigencia" SortExpression="FINVIGENCIA" ItemStyle-HorizontalAlign="Center" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-Width="70px">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INIEXCLUSION" HeaderText="Fecha Exclusi&oacute;n" SortExpression="INIEXCLUSION" ItemStyle-HorizontalAlign="Center" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-Width="70px">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PROFORMA" HeaderText="Nro. Aviso Cobranza" SortExpression="PROFORMA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PREEXISTENCIA" HeaderText="Pre-Existencia" SortExpression="PREEXISTENCIA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NCERTIF_EXC" HeaderText="Cert. Excluidos" SortExpression="NCERTIF_EXC" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" Visible="False">
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
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:ImageButton ID="ImprimirDetEndosos" runat="server" ImageUrl="~/Imagenes/Imprimir.gif" style="height: 22px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
        MaskType="Date" TargetControlID="txtFecEfectoIni" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFecEfectoFin" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
    <asp:HiddenField ID="HF_POLIZA" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RUC" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RAZONSOCIAL" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RENOVACION" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_NUM_MOVIMIENTO" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_TIPOMOV" runat="server" Visible="False" />
</asp:Content>
