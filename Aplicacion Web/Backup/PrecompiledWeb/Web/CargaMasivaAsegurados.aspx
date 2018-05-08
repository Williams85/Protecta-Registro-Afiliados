<%@ page language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" inherits="CargaMasivaAsegurados, App_Web_pdp4hctj" title="Carga Masiva de Asegurados" theme="Tema" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="Javascript"> 
 

       function Seleccionar(control)
       {
              var valor =document.getElementById('<%=FileUpload1.ClientId%>').value;

              //Guardamos en un hidden
              document.getElementById('<%=HiddenField1.ClientId%>').value = valor;
              //onclick="return TABLE1_onclick()"
        } 
function TABLE1_onclick() {

}

    </script>

    &nbsp;<asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="TABLE1" style="width: 100%; border-top-style: groove; border-right-style: groove;
        border-left-style: groove; height: 100%; border-bottom-style: groove" onclick="return TABLE1_onclick()">
        <tr>
            <td align="left" style="height: 36px" >
                <asp:Label ID="Label" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt"
                    ForeColor="MidnightBlue">CARGA MASIVA DE ASEGURADOS</asp:Label></td>
            <td align="right" style="height: 36px" >
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/b1r.png" /><asp:Image
                    ID="Image2" runat="server" ImageUrl="~/Imagenes/b2r.png" /><asp:Image ID="Image3"
                        runat="server" ImageUrl="~/Imagenes/b3r.png" /></td>
        </tr>
        <tr>
            <td align="left" style="height: 28px">
                <asp:Label ID="txtTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10.5pt"
                    ForeColor="Green"></asp:Label></td>
            <td align="right" style="height: 28px">
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 200px">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%; height: 100%; background-color: gainsboro">
                            <tr>
                                <td style="width: 121px; height: 10px">
                                </td>
                                <td style="width: 82px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                                <td style="width: 61px; height: 21px">
                                </td>
                                <td style="width: 191px; height: 21px">
                                </td>
                                <td style="width: 104px; height: 21px">
                                </td>
                                <td style="height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 121px; height: 27px">
                                </td>
                                <td style="width: 82px; height: 27px">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Archivo :" Width="85px"></asp:Label></td>
                                <td colspan="3" style="height: 27px">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="303px" onChange="Seleccionar(this);" />&nbsp;<br />
                                    <asp:TextBox ID="txtRuta" runat="server" Width="295px"></asp:TextBox></td>
                                <td style="width: 104px; height: 27px">
                                    &nbsp;<asp:Button ID="btnProcesar" runat="server" TabIndex="5" Text="Procesar" Width="127px" /></td>
                                <td style="height: 27px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 121px; height: 26px;">
                                </td>
                                <td style="width: 82px; height: 26px;">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Cabecera :"></asp:Label></td>
                                <td style="width: 100px; height: 26px;">
                                    <asp:TextBox ID="txtCabecera" runat="server" TabIndex="1" Width="43px"></asp:TextBox></td>
                                <td style="width: 61px; height: 26px;">
                                </td>
                                <td style="width: 191px; height: 26px;">
                                </td>
                                <td style="width: 104px; height: 26px;">
                                    <asp:Button ID="btnSiguiente" runat="server" TabIndex="6" Text="Siguiente" Width="127px" /></td>
                                <td style="height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 121px; height: 26px;">
                                </td>
                                <td style="width: 82px; height: 26px;">
                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Año :"></asp:Label></td>
                                <td style="width: 100px; height: 26px;">
                                    <asp:TextBox ID="txtAño" runat="server" TabIndex="2" Width="43px"></asp:TextBox></td>
                                <td style="width: 61px; height: 26px;">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Mes :" Width="91px"></asp:Label></td>
                                <td colspan="1" style="width: 191px; height: 26px;">
                                    &nbsp;<asp:DropDownList ID="ddlMeses" runat="server" TabIndex="3">
                                        <asp:ListItem Value="01">Enero</asp:ListItem>
                                        <asp:ListItem Value="02">Febrero</asp:ListItem>
                                        <asp:ListItem Value="03">Marzo</asp:ListItem>
                                        <asp:ListItem Value="04">Abril</asp:ListItem>
                                        <asp:ListItem Value="05">Mayo</asp:ListItem>
                                        <asp:ListItem Value="06">Junio</asp:ListItem>
                                        <asp:ListItem Value="07">Julio</asp:ListItem>
                                        <asp:ListItem Value="08">Agosto</asp:ListItem>
                                        <asp:ListItem Value="09">Septiembre</asp:ListItem>
                                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="width: 104px; height: 26px;">
                                    <asp:Button ID="btnProblemas" runat="server" Text="Error de Carga" Width="128px" /></td>
                                <td style="height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 121px; height: 26px">
                                </td>
                                <td style="width: 82px; height: 26px">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Comentario :" Width="84px"></asp:Label></td>
                                <td colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtComentario" runat="server" MaxLength="80" TabIndex="4" Width="279px"></asp:TextBox></td>
                                <td style="width: 104px; height: 26px">
                                    <asp:Button ID="btnCerrar" runat="server" TabIndex="7" Text="Cerrar" Width="127px" /></td>
                                <td style="height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 121px; height: 20px">
                                </td>
                                <td style="width: 82px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 191px">
                                </td>
                                <td style="width: 104px">
                                    </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 121px">
                                </td>
                                <td style="width: 82px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 191px">
                                </td>
                                <td style="width: 104px">
                                    </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="width: 100%; height: 100%; background-color: lightgrey">
                            <tr>
                                <td style="width: 115px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                                <td style="width: 62px; height: 21px">
                                </td>
                                <td style="width: 110px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px;">
                                </td>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Ciclo :" Width="52px"></asp:Label></td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtCiclo2" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="286px"></asp:TextBox></td>
                                <td style="width: 110px;" align="center">
                                    <asp:Button ID="btnProcesarPaso1" runat="server" Text="Procesar" Width="130px" /></td>
                                <td style="width: 100px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px">
                                </td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Año :"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtAño2" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="46px"></asp:TextBox></td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Mes :"></asp:Label></td>
                                <td style="width: 62px">
                                    <asp:TextBox ID="txtMes2" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="79px"></asp:TextBox></td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnSiguientePaso1" runat="server" Text="Siguiente" Width="130px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px;">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Registros a Procesar :" Width="141px"></asp:Label></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtReg2" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                        ReadOnly="True" Width="42px"></asp:TextBox></td>
                                <td style="width: 62px;">
                                </td>
                                <td style="width: 110px;" align="center">
                                    <asp:Button ID="btnErroresPaso1" runat="server" Text="Errores" Width="130px" /></td>
                                <td style="width: 100px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px;">
                                </td>
                                <td colspan="2">
                                </td>
                                <td colspan="2" rowspan="3">
                                    </td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnResumenPaso1" runat="server" Text="Resumen" Width="130px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnCerrarPaso1" runat="server" Text="Cerrar" Width="130px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px; height: 20px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 110px" align="center">
                                    </td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table style="width: 100%; height: 100%; background-color: lightgrey">
                            <tr>
                                <td style="width: 120px; height: 10px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 107px">
                                </td>
                                <td style="width: 97px">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px;">
                                </td>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Ciclo :"></asp:Label></td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtCiclo3" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="292px"></asp:TextBox></td>
                                <td style="width: 97px;">
                                    <asp:Button ID="btnProcesarPaso3" runat="server" Text="Procesar" Width="133px" /></td>
                                <td style="width: 100px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px">
                                </td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Año :"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtAño3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                        ReadOnly="True" Width="55px"></asp:TextBox></td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Mes :"></asp:Label></td>
                                <td style="width: 107px">
                                    <asp:TextBox ID="txtMes3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                        ReadOnly="True" Width="90px"></asp:TextBox></td>
                                <td style="width: 97px">
                                    <asp:Button ID="btnCorrectosPaso1" runat="server" Text="Transferidos" Width="130px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; height: 26px;">
                                </td>
                                <td colspan="2" style="height: 26px">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Registros Transferidos :"></asp:Label></td>
                                <td style="width: 100px; height: 26px;">
                                    <asp:TextBox ID="txtReg3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                        ReadOnly="True" Width="53px"></asp:TextBox></td>
                                <td style="width: 107px; height: 26px;">
                                </td>
                                <td style="width: 97px; height: 26px;">
                                    <asp:Button ID="btnCerrarPaso3" runat="server" Text="Cerrar" Width="133px" /></td>
                                <td style="width: 100px; height: 26px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; height: 20px">
                                </td>
                                <td colspan="2">
                                    </td>
                                <td style="width: 100px">
                                    </td>
                                <td style="width: 107px">
                                </td>
                                <td style="width: 97px">
                                    </td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td colspan="2">
                                    </td>
                                <td style="width: 97px" valign="top">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; height: 10px">
                                </td>
                                <td style="width: 100px; height: 10px">
                                </td>
                                <td style="width: 100px; height: 10px">
                                </td>
                                <td style="width: 100px; height: 10px">
                                </td>
                                <td style="width: 107px; height: 10px">
                                </td>
                                <td style="width: 97px; height: 10px">
                                </td>
                                <td style="width: 100px; height: 10px">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    &nbsp;
                </asp:MultiView></td>
        </tr>
    </table>

    <asp:HiddenField ID="HF_TIPO" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RUTA_MASIVA" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_SERVER_SECURITY" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_PASSINT" runat="server" />
    <asp:HiddenField ID="HF_RUTA_SQL" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RUTA_CARGA" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RUTA_TEMP" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_SERVER" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RUTA_EJECUTABLE" runat="server" Visible="False" />
    <asp:HiddenField ID="HF_RUTA_ARCHIVO" runat="server" />
    <asp:Panel ID="PanelCierre" runat="server" BackColor="White" BorderStyle="Solid"
        BorderWidth="2px" Height="143px" Width="469px" Visible="False">
        <table style="width: 464px">
            <tr>
                <td style="width: 100px; height: 10px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="font-weight: bold; font-size: small; color: dimgray;
                    font-family: Tahoma; height: 21px;">
                    Existen Ciclos para este N° de póliza que no están cerradas.</td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="font-weight: bold; font-size: small; height: 21px;
                    color: dimgray; font-family: Tahoma;">
                    Al crear un nuevo ciclo se cerrarán las demas.<br />
                    ¿Desea continuar con la generación del nuevo ciclo?</td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px; height: 10px;">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 34px;">
                </td>
                <td style="width: 100px; height: 34px;">
                    <asp:ImageButton ID="imbAcepCierre" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" /></td>
                <td style="width: 100px; height: 34px;">
                    <asp:ImageButton ID="imbCancelCierre" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
                <td style="width: 100px; height: 34px;">
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMensajeCierre" runat="server"></asp:Label></asp:Panel>
    <cc1:ModalPopupExtender ID="ModalMensajeCierre" runat="server" BackgroundCssClass="modalBackground"
        CacheDynamicResults="True" Drag="True" PopupControlID="PanelCierre" TargetControlID="lblMensajeCierre">
    </cc1:ModalPopupExtender>
    <cc1:MaskedEditExtender ID="MkeAño" runat="server" Mask="9999" MaskType="Number"
        TargetControlID="txtAño">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditExtender ID="Mkecab" runat="server" Mask="9" MaskType="Number" TargetControlID="txtCabecera">
    </cc1:MaskedEditExtender>
    &nbsp; &nbsp;&nbsp;&nbsp;
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    </asp:UpdatePanel>
    <br />
    <asp:UpdateProgress id="UpdateProgress1" runat="server" DisplayAfter="0">
        <progresstemplate>
        <DIV id="progressBackgroundFilter"></DIV><DIV style="TEXT-ALIGN: center" id="processMessage">Cargando...<BR /><IMG src="imagenes/progress_rojo.gif" /><BR /></DIV>
        </progresstemplate>
    </asp:UpdateProgress>
    <br />
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
                    <asp:Label ID="lblMensajeProceso" runat="server" Font-Names="Tahoma" Font-Size="9pt"
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
                    <asp:ImageButton ID="imbAceptarCarga" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" /></td>
                <td style="width: 100px">
                    <asp:ImageButton ID="imbCancelarCarga" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        <asp:Label ID="lblCarga" runat="server"></asp:Label></asp:Panel>
    <cc1:ModalPopupExtender ID="mpeCarga" runat="server" BackgroundCssClass="modalBackground"
        Drag="True" PopupControlID="PnelCarga" TargetControlID="lblCarga">
    </cc1:ModalPopupExtender>
</asp:Content>
