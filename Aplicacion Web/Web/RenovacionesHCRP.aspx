<%@ Page Language="VB" MasterPageFile="~/MasterPageProcesos.master" AutoEventWireup="false"
    CodeFile="RenovacionesHCRP.aspx.vb" Inherits="RenovacionesHCRP" Title="Procesos" Theme="Tema" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%--Proyecto: PRY010
Requerimiento: RQ2017-0100001
Autor: EFITEC-MCM
--------------------------------------------------------------------------------------------------%>
  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


    <script src="Script/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad() {
            //$("#<%=gvDetalleP.ClientID %> td:eq(8)").dblclick(function () {
            $("#<%=gvDetalleP.ClientID %> td").dblclick(function () {
              
                var col = $(this).parent().children().index($(this));
                var row = $(this).parent().parent().children().index($(this).parent());


                if (col == 8) {
                    var OriginalContent = $(this).text();

                    $(this).addClass("cellEditing");
                    $(this).html("<input type='text' maxlength='10' minlength='10' value='" + OriginalContent + "' />");
                    $(this).children().first().focus();


                    $(this).children().first().keypress(function (e) {
                        if (e.which == 13) {
                            var newContent = $(this).val();
                            $(this).parent().text(newContent);
                            $(this).parent().removeClass("cellEditing");
                            $('#<%=hidden2.ClientID %>').attr('value', newContent);
                            $('#<%=hidden3.ClientID %>').attr('value', row);
                            //                alert($('#<%=hidden2.ClientID %>').val());
                            $('#<%=BtnUpdate.ClientID %>').click();

                        }
                    });

                    $(this).children().first().blur(function () {
                        $(this).parent().text(OriginalContent);
                        $(this).parent().removeClass("cellEditing");
                    });
                }

            });

        };
        $(document).ready(function () {

        });


    </script>

    <script type="text/javascript" language="Javascript">

        function MostrarEspera() {
 
            $find('mpe').hide();
     
            var elem = document.getElementById("DivProcessMessage");
            elem.style.width = "100%";
            elem.style.height = "95%";
            elem.style.display = "block";
            return true;
        }
        function CerrarEspera() {
            var elem = document.getElementById("DivProcessMessage");
            elem.style.display = "none";
            return true;
        }

        function CerrarCancelar() {
            $find('mpe').hide();
            return true;
        }
    </script>
    
    <div id="midiv" style="position:absolute; display:none; left:0px;">

    </div>
    <asp:ScriptManager id="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
     <div style="position:absolute; display:none; left:0px; background-color: rgba(232, 221, 219, 0.4);" id="DivProcessMessage">
        <div style="TEXT-ALIGN: center; width: 700px; height: 100px; left: 0px; background-color:white; margin-left:20%;margin-top:15%; border: 1px; " ><b>Cargando...</b><br />
            <img src="imagenes/progress_rojo.gif" /><br />
        </div>
    </div>

<input style="LEFT: 22px; WIDTH: 94px; POSITION: absolute; TOP: 1292px" id="hide" type="hidden" runat="server" /> <cc1:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="ImgbFecha" PopupPosition="BottomRight" TargetControlID="txtFecha">
    </cc1:CalendarExtender> 
    <asp:HiddenField id="HF_TIPO_RECIBO" runat="server">
   </asp:HiddenField>&nbsp;
     <asp:HiddenField ID="HF_NUMPAGINA" runat="server" />
     <asp:HiddenField ID="HF_RUTA_MASIVA" runat="server" />  
    <asp:HiddenField ID="HF_PROSIGUE" runat="server" />   

             <table style="width: 50%; height: 70%; margin-left: 50px; margin-top: 0px;">
                 <tbody>
                     <tr><TD style="FONT-WEIGHT: bold; FONT-SIZE: large; FONT-FAMILY: Tahoma; HEIGHT:26px" align=center colSpan=9>Proceso de Generación de Recibos</TD></TR>
                     <TR><TD style="FONT-WEIGHT: bold; FONT-SIZE: small; FONT-FAMILY: Tahoma; HEIGHT: 26px" colSpan=2>Ramo:<asp:DropDownList id="ddlRamo" runat="server" AutoPostBack="True" Width="195px">
                </asp:DropDownList></td><td style="WIDTH: 371px; HEIGHT: 26px"></td><td style="FONT-WEIGHT: bold; FONT-SIZE: small; WIDTH: 100px; FONT-FAMILY: Tahoma; HEIGHT: 26px">Producto:</TD><TD style="WIDTH: 97px; HEIGHT: 26px"><asp:DropDownList id="ddlProducto" runat="server" AutoPostBack="True" Width="238px">
                </asp:DropDownList></td><td style="FONT-WEIGHT: bold; FONT-SIZE: small; WIDTH: 519px; FONT-FAMILY: Tahoma; HEIGHT: 26px"><asp:Label id="lblFechaPeriodo" runat="server" Text="Periodo de Proceso:"></asp:Label></TD><TD style="WIDTH: 108px; HEIGHT: 26px"><asp:TextBox id="txtFecha" runat="server" Width="86px" OnTextChanged="txtFecha_TextChanged"></asp:TextBox></TD><TD style="WIDTH: 69px; HEIGHT: 26px"><asp:ImageButton id="ImgbFecha" runat="server" ImageUrl="~/Imagenes/calendario.jpg"></asp:ImageButton></TD><TD style="WIDTH: 100px; HEIGHT: 26px"></TD></TR>
                     <TR><TD colSpan=2><asp:Label id="Label3" runat="server" Font-Size="10pt" Font-Names="Tahoma" Font-Bold="True" Text="Tipo de recibo:" Width="101px"></asp:Label> <asp:Label id="lblRecibo" runat="server" Font-Size="10pt" Font-Names="Tahoma" Font-Bold="True"></asp:Label></TD><TD style="FONT-WEIGHT: bold; FONT-SIZE: medium; WIDTH: 371px"><asp:Label id="lblPoliza" runat="server" Text="Póliza:"></asp:Label></TD><TD style="WIDTH: 100px"><asp:TextBox id="txtPoliza" runat="server" Width="65px"></asp:TextBox></TD><TD style="WIDTH: 97px"><asp:RadioButtonList id="rblTipo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Carga</asp:ListItem>
                    <asp:ListItem Value="1">Reemplazo</asp:ListItem>
                    <asp:ListItem Value="2">Prorroga</asp:ListItem>
                </asp:RadioButtonList></TD><TD style="WIDTH: 519px"></TD><TD style="WIDTH: 108px"></TD><TD style="WIDTH: 69px"></TD><TD style="WIDTH: 100px"></TD></TR>
                     <TR><TD style="WIDTH: 21px; HEIGHT: 14px"></TD><TD style="WIDTH: 27px; HEIGHT: 14px"></TD><TD style="WIDTH: 371px; HEIGHT: 14px"></TD><TD style="WIDTH: 100px; HEIGHT: 14px"></TD><TD style="WIDTH: 97px; HEIGHT: 14px"></TD><TD style="WIDTH: 519px; HEIGHT: 14px"></TD><TD style="WIDTH: 108px; HEIGHT: 14px"><asp:Button id="btnBuscar" runat="server" Text="Buscar" Width="71px"></asp:Button></TD><TD style="WIDTH: 69px; HEIGHT: 14px"></TD><TD style="WIDTH: 100px; HEIGHT: 14px"></TD></TR>
                     <TR>
                         <TD colSpan=9 style="height: 703px">
                             <DIV style="OVERFLOW: scroll; WIDTH: 850px; HEIGHT: 400px; BACKGROUND-COLOR: whitesmoke">
                                 <asp:GridView id="gvDetalle" runat="server" ForeColor="Black" Width="858px" PageSize="4000" AllowPaging="True" AutoGenerateColumns="False" GridLines="Vertical" BorderStyle="None" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="gvDetalle_PageIndexChanging">
                                <RowStyle BackColor="#F7F7DE" Font-Names="Tahoma" Font-Size="8pt"></RowStyle>
                                <Columns>
                                <asp:TemplateField><ItemTemplate>
                                <asp:CheckBox id="ChkSel" runat="server"></asp:CheckBox> 
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cantidad_Cert" HeaderText="Cant. Cert.">
                                <ItemStyle Width="700px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Asegurado" HeaderText="Asegurado">
                                <ControlStyle Width="3000px"></ControlStyle>

                                <ItemStyle Width="700px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Prima" DataFormatString=" {0:0,0.00}" HeaderText="Prima">
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                <ItemStyle HorizontalAlign="Right" Width="700px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Nro_Factura" HeaderText="Nro_Factura">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado_Cert" DataFormatString=" {0:0}" HeaderText="Estado_Cert" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="Estado_recibo" DataFormatString=" {0:0}" HeaderText="Estado_recibo" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="Tarea" DataFormatString=" {0:0}" HeaderText="Tarea"></asp:BoundField>
                                <asp:BoundField DataField="Estado_tarea" HeaderText="Estado_tarea"></asp:BoundField>
                                <asp:BoundField DataField="Descripcion_tarea" HeaderText="Descripci&oacute;n_tarea"></asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                <PagerStyle HorizontalAlign="Right" BackColor="#F7F7DE" ForeColor="Black"></PagerStyle>
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" ForeColor="White"></HeaderStyle>
                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                </asp:GridView> 
                                <asp:GridView id="gvDetalleP" runat="server" ForeColor="Black" Width="859px" PageSize="4000" AllowPaging="True" AutoGenerateColumns="False" GridLines="Vertical" BorderStyle="None" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanging="gvDetalleP_SelectedIndexChanging" >
                                <RowStyle BackColor="#F7F7DE" Font-Names="Tahoma" Font-Size="8pt"></RowStyle>
                                <Columns>
                                <asp:TemplateField><ItemTemplate>
                                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox> 
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Poliza" HeaderText="P&oacute;liza"></asp:BoundField>
                                <asp:BoundField DataField="Nro_Contratante" HeaderText="Nro_Contratante" Visible="False">
                                <ItemStyle Width="0px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Contratante" HeaderText="Contratante"></asp:BoundField>
                                <asp:BoundField DataField="Inicio_Vig" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Inicio_Vig"></asp:BoundField>
                                <asp:BoundField DataField="Fin_Vig" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fin_Vig"></asp:BoundField>
                                <asp:BoundField DataField="Prima" DataFormatString=" {0:0,0.00}" HeaderText="Prima">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Nro_Factura" HeaderText="Nro_Factura"></asp:BoundField>
                                <asp:BoundField DataField="Fecha_Renova" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha_Renova" ReadOnly = "true" >
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha_Fin_Renova" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha_Fin_Renova" ReadOnly = "true" >
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado_Cert" DataFormatString=" {0:0}" HeaderText="Estado_Cert" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="Estado_recibo" DataFormatString=" {0:0}" HeaderText="Estado_recibo" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="nintermed" DataFormatString=" {0:0}" HeaderText="nintermed"></asp:BoundField>
                                <asp:BoundField DataField="Tarea" DataFormatString=" {0:0}" HeaderText="Tarea"></asp:BoundField>
                                <asp:BoundField DataField="Estado_tarea" HeaderText="Estado_tarea"></asp:BoundField>
                                <asp:BoundField DataField="Descripcion_tarea" HeaderText="Descripci&oacute;n_tarea"></asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99"></FooterStyle>
                                <PagerStyle HorizontalAlign="Right" BackColor="#F7F7DE" ForeColor="Black"></PagerStyle>
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" ForeColor="White"></HeaderStyle>
                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                </asp:GridView>
                            </DIV>
                             <asp:RadioButtonList id="rblmarcar" runat="server" Font-Size="10pt" Font-Names="Tahoma" Font-Bold="True" AutoPostBack="True" Width="256px" RepeatDirection="Horizontal"><asp:ListItem Value="0">Marcar Todos</asp:ListItem>
                            <asp:ListItem Value="1">Desmarcar Todos</asp:ListItem>
                            </asp:RadioButtonList>
<BR />
<TABLE style="WIDTH: 304px">
<TBODY>
<TR>
<TD style="WIDTH: 108px">
<asp:Label id="Label1" runat="server" Font-Size="10pt" Font-Names="Tahoma" Font-Bold="True" Text="Total de N° de registros :" Width="173px"></asp:Label>
</TD>
<TD style="WIDTH: 100px">
    <asp:TextBox id="txtNumReg" runat="server" Width="78px" ReadOnly="True"></asp:TextBox>
</TD>
</TR>
<TR>
<TD style="WIDTH: 108px; height: 26px;">
<asp:Label id="Label2" runat="server" Font-Size="10pt" Font-Names="Tahoma" Font-Bold="True" Text="Total Prima:" Width="82px"></asp:Label>
</TD>
<TD style="WIDTH: 100px; height: 26px;">
<asp:TextBox id="txtTotalPrima" runat="server" Width="78px" ReadOnly="True"></asp:TextBox>
</TD>
</TR>
</TBODY>
</TABLE>
</TD>
</TR>
<TR>
<TD style="WIDTH: 21px"></TD>
<TD style="WIDTH: 27px"></TD>
<TD style="WIDTH: 371px"></TD>
<TD style="WIDTH: 100px"></TD>
<TD style="WIDTH: 97px"></TD>
<TD style="WIDTH: 519px"></TD>
<TD style="WIDTH: 108px"></TD>
<TD style="WIDTH: 69px"></TD>
<TD style="WIDTH: 100px"></TD>
</TR>
<TR>
<TD style="WIDTH: 21px; HEIGHT: 26px"></TD>
<TD style="WIDTH: 27px; HEIGHT: 26px"></TD>
<TD style="WIDTH: 371px; HEIGHT: 26px"></TD>

<td style="WIDTH: 100px; HEIGHT: 26px">
<asp:Button id="btnGrabar" runat="server" Text="Grabar" Width="70px"></asp:Button>
</td>
<TD style="WIDTH: 97px; HEIGHT: 26px">
<asp:Button id="btnSalir" runat="server" Text="Salir" Width="79px"></asp:Button>
</TD><TD style="WIDTH: 519px; HEIGHT: 26px"></TD><TD style="WIDTH: 108px; HEIGHT: 26px"></TD><TD style="WIDTH: 69px; HEIGHT: 26px"></TD><TD style="WIDTH: 100px; HEIGHT: 26px"></TD></TR><TR><TD style="WIDTH: 21px; HEIGHT: 20px"></TD><TD style="WIDTH: 27px; HEIGHT: 20px"></TD><TD style="WIDTH: 371px; HEIGHT: 20px"></TD><TD style="WIDTH: 100px; HEIGHT: 20px"></TD><TD style="WIDTH: 97px; HEIGHT: 20px"></TD><TD style="WIDTH: 519px; HEIGHT: 20px"></TD><TD style="WIDTH: 108px; HEIGHT: 20px"></TD><TD style="WIDTH: 69px; HEIGHT: 20px"></TD><TD style="WIDTH: 100px; HEIGHT: 20px"></TD></TR></TBODY></table>
<div id="prueba1"> 
 
</div>
<cc1:maskededitextender id="MaskedEditFecha" runat="server" TargetControlID="txtFecha" Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear"></cc1:maskededitextender> <BR />

<input id="hidden2" runat="server" type="hidden" />
<input id="hidden3" runat="server" type="hidden" />
<div style="visibility:hidden">
<asp:Button id="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" />
</div>

 <asp:UpdateProgress id="UpdateProgress1" runat="server" DisplayAfter="0">
        <progresstemplate>
        <DIV id="progressBackgroundFilter"></DIV><DIV style="TEXT-ALIGN: center" id="processMessage">Cargando...<BR /><IMG src="imagenes/progress_rojo.gif" /><BR /></DIV>
        </progresstemplate>
    </asp:UpdateProgress>
    <br />


<asp:Panel ID="PnelCarga" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px"
        Height="114px" Width="326px">
        <table >
                        <tr>
                <td align="center" colspan="4" style="font-weight: bold; font-size: small; font-family: Tahoma;
                    height: 25px;"></td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="font-weight: bold; font-size: small; font-family: Tahoma;
                    height: 25px;">
                    Desea procesar&nbsp; la generación de recibos?¿Confirme por favor...?</td>
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
                    <asp:ImageButton ID="imbCancelarCarga" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" OnClientClick="CerrarCancelar();"/></td>
                <td style="width: 100px">
                </td>
            </tr>
             <tr>
                <td style="width: 147px">
                </td>
                <td style="width: 100px">
                    </td>
                <td style="width: 100px">
                    </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        <asp:Label ID="lblCarga" runat="server"></asp:Label></asp:Panel>

    <cc1:ModalPopupExtender ID="mpeCarga" runat="server" BackgroundCssClass="modalBackground"
        Drag="True" PopupControlID="PnelCarga" TargetControlID="lblCarga" BehaviorID="mpe">
    </cc1:ModalPopupExtender>
</asp:Content>