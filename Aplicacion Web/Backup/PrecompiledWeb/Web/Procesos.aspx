<%@ page language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" inherits="Procesos, App_Web_pdp4hctj" title="Validador de archivos" theme="Tema" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="Javascript"> 
 //'

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
            <td align="left" colspan="3" style="height: 30px">
                <asp:Label ID="txtTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt"
                    ForeColor="Green"></asp:Label></td>
            <td align="right" colspan="2" style="height: 30px">
                &nbsp; &nbsp;&nbsp;
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/b1r.png" /><asp:Image
                    ID="Image2" runat="server" ImageUrl="~/Imagenes/b2r.png" /><asp:Image ID="Image3"
                        runat="server" ImageUrl="~/Imagenes/b3r.png" /><asp:Image ID="Image4" runat="server"
                            ImageUrl="~/Imagenes/b4r.png" /></td>
            <td align="right" colspan="1" style="width: 678px; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 35px">
            </td>
            <td style="width: 282px; height: 35px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Canal :"></asp:Label></td>
            <td colspan="3" style="height: 35px">
                <asp:DropDownList ID="ddlCanal" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" style="width: 678px; height: 35px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 37px;">
            </td>
            <td style="width: 282px; height: 37px;">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Producto :"></asp:Label></td>
            <td colspan="3" style="height: 37px" rowspan="">
                <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" rowspan="" style="width: 678px; height: 37px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 33px">
            </td>
            <td style="width: 282px; height: 33px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Poliza:"></asp:Label></td>
            <td style="height: 33px">
                <asp:DropDownList ID="ddlPoliza" runat="server" AutoPostBack="True" Width="120px">
                </asp:DropDownList></td>
            <td colspan="2" rowspan="2">
                <asp:Label ID="LblTit" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Small"
                    ForeColor="Crimson" Width="328px" Height="16px"></asp:Label><br />
                <asp:Label ID="LblCCarga" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Small" ForeColor="#0000C0" Height="32px" Width="280px" style="text-align:justify"></asp:Label>
                <br />
                <asp:Label ID="LblCRenova" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Overline="False" Font-Size="X-Small" ForeColor="#0000C0" Height="32px" Width="280px" style="text-align:justify"></asp:Label>
            </td>
            <td colspan="1" rowspan="2" style="width: 678px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 34px;">
            </td>
            <td style="width: 282px; height: 34px;">
                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Ciclo :"></asp:Label></td>
            <td style="width: 1px; height: 34px;">
                <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True" Width="120px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 517px; height: 20px">
            </td>
            <td style="width: 282px; height: 20px">
            </td>
            <td style="width: 1px; height: 20px">
            </td>
            <td colspan="2" rowspan="1" style="height: 20px">
            </td>
            <td style="width: 678px; height: 20px" rowspan="">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 23px;">
            </td>
            <td align="center" colspan="2" style="height: 23px">
                &nbsp;<asp:ImageButton ID="imbAceptar1" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" />
                <asp:ImageButton ID="imbCancelar" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
            <td style="width: 87px; height: 23px;">
            </td>
            <td style="width: 678px; height: 23px;">
            </td>
            <td style="width: 678px; height: 23px">
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 200px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%; height: 100%; background-color: gainsboro">
                            <tr>
                                <td style="width: 109px; height: 10px">
                                </td>
                                <td style="width: 82px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                                <td style="width: 61px; height: 21px">
                                </td>
                                <td style="width: 114px; height: 21px">
                                </td>
                                <td style="width: 86px; height: 21px">
                                </td>
                                <td style="height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px; height: 27px">
                                </td>
                                <td style="width: 82px; height: 27px">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Archivo :" Width="85px"></asp:Label></td>
                                <td colspan="3" style="height: 27px">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="303px" onChange="Seleccionar(this);" />&nbsp;<br />
                                    <asp:TextBox ID="txtRuta" runat="server" Width="295px"></asp:TextBox></td>
                                <td style="width: 86px; height: 27px">
                                    &nbsp;<asp:Button ID="btnProcesar" runat="server" TabIndex="5" Text="Procesar" Width="127px" /></td>
                                <td style="height: 27px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px; height: 26px;">
                                </td>
                                <td style="width: 82px; height: 26px;">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Cabecera :"></asp:Label></td>
                                <td style="width: 100px; height: 26px;">
                                    <asp:TextBox ID="txtCabecera" runat="server" TabIndex="1" Width="43px"></asp:TextBox></td>
                                <td style="width: 61px; height: 26px;">
                                </td>
                                <td style="width: 114px; height: 26px;">
                                </td>
                                <td style="width: 86px; height: 26px;">
                                    <asp:Button ID="btnSiguiente" runat="server" TabIndex="6" Text="Siguiente" Width="127px" /></td>
                                <td style="height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px">
                                </td>
                                <td style="width: 82px">
                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Año :"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtAño" runat="server" TabIndex="2" Width="43px"></asp:TextBox></td>
                                <td style="width: 61px">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Mes :" Width="91px"></asp:Label></td>
                                <td colspan="1" style="width: 114px">
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
                                <td style="width: 86px">
                                    <asp:Button ID="btnCerrar" runat="server" TabIndex="7" Text="Cerrar" Width="127px" /></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px; height: 12px">
                                </td>
                                <td style="width: 82px; height: 12px">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Comentario :" Width="84px"></asp:Label></td>
                                <td colspan="3" style="height: 12px">
                                    <asp:TextBox ID="txtComentario" runat="server" MaxLength="80" TabIndex="4" Width="279px"></asp:TextBox></td>
                                <td style="width: 86px; height: 12px">
                                    <asp:Button ID="btnResTotal" runat="server" TabIndex="8" Text="Resumen Total" Width="127px" /></td>
                                <td style="height: 12px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px; height: 20px">
                                </td>
                                <td style="width: 82px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 114px">
                                </td>
                                <td style="width: 86px">
                                    <asp:Button ID="btnRegCorrectos" runat="server" TabIndex="9" Text="Registros Correctos"
                                        Width="128px" /></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px">
                                </td>
                                <td style="width: 82px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 114px">
                                </td>
                                <td style="width: 86px">
                                    <asp:Button ID="btnProblemas" runat="server" Text="Error de Carga" Width="128px" /></td>
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
                                <td style="width: 60px; height: 21px">
                                </td>
                                <td style="width: 110px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px; height: 26px">
                                </td>
                                <td style="width: 100px; height: 26px">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Ciclo :" Width="52px"></asp:Label></td>
                                <td colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtCiclo2" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="286px"></asp:TextBox></td>
                                <td style="width: 110px; height: 26px" align="center">
                                    <asp:Button ID="btnProcesarPaso1" runat="server" Text="Procesar" Width="130px" /></td>
                                <td style="width: 100px; height: 26px">
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
                                <td style="width: 60px">
                                    <asp:TextBox ID="txtMes2" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="79px"></asp:TextBox></td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnSiguientePaso1" runat="server" Text="Siguiente" Width="130px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Registros a Procesar :" Width="141px"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtReg2" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                        ReadOnly="True" Width="42px"></asp:TextBox></td>
                                <td style="width: 60px">
                                </td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnCerrarPaso1" runat="server" Text="Cerrar" Width="130px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px; height: 20px">
                                </td>
                                <td colspan="2">
                                </td>
                                <td colspan="2" rowspan="3">
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                        Font-Size="8pt" Width="158px">
                                        <asp:ListItem Value="0">Remplazo de N&#243;mina</asp:ListItem>
                                        <asp:ListItem Value="1">Carga Masiva</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnErroresPaso1" runat="server" Text="Errores" Width="130px" /></td>
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
                                    <asp:Button ID="btnCorrectosPaso1" runat="server" Text="Transferidos" Width="130px" /></td>
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
                                    <asp:Button ID="btnResumenPaso1" runat="server" Text="Resumen" Width="130px" /></td>
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
                                <td colspan="2" rowspan="1">
                                </td>
                                <td style="width: 110px" align="center">
                                    <asp:Button ID="btnRptAgrobanco" runat="server" Text="Resumen Total" Width="130px"
                                        Enabled="False" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 115px; height: 26px">
                                </td>
                                <td style="width: 100px; height: 26px">
                                </td>
                                <td style="width: 100px; height: 26px">
                                </td>
                                <td colspan="2" rowspan="1" style="height: 26px">
                                </td>
                                <td align="center" style="width: 110px; height: 26px">
                                    <asp:Button ID="BtnPermanencia" runat="server" Text="Exceden permanencia" Width="144px"
                                        Enabled="False" Height="40px" Visible="False" /></td>
                                <td style="width: 100px; height: 26px">
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
                                <td style="width: 116px">
                                </td>
                                <td style="width: 97px">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; height: 26px">
                                </td>
                                <td style="width: 100px; height: 26px">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Ciclo :"></asp:Label></td>
                                <td colspan="3" style="height: 26px">
                                    <asp:TextBox ID="txtCiclo3" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="292px"></asp:TextBox></td>
                                <td style="width: 97px; height: 26px">
                                    <asp:Button ID="btnProcesarPaso3" runat="server" Text="Procesar" Width="133px" /></td>
                                <td style="width: 100px; height: 26px">
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
                                <td style="width: 116px">
                                    <asp:TextBox ID="txtMes3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                        ReadOnly="True" Width="90px"></asp:TextBox></td>
                                <td style="width: 97px">
                                    <asp:Button ID="btnSiguientePaso3" runat="server" Text="Siguiente" Width="133px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Registros Transferidos :"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtReg3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                        ReadOnly="True" Width="53px"></asp:TextBox></td>
                                <td style="width: 116px">
                                </td>
                                <td style="width: 97px">
                                    <asp:Button ID="btnCerrarPaso3" runat="server" Text="Cerrar" Width="133px" /></td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; height: 20px">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblFechaEfecto" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                        Font-Size="8pt" Text="Fecha de Efecto"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtFechaDef" runat="server" Width="80px"></asp:TextBox></td>
                                <td style="width: 116px">
                                </td>
                                <td style="width: 97px">
                                    <asp:Button ID="btnRegxModPaso3" runat="server" Text="Registros por Módulo" Width="134px" /></td>
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
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                        Font-Size="8pt" Width="158px" BackColor="LightGray" BorderColor="White" ForeColor="Black">
                                        <asp:ListItem Value="0">Remplazo de N&#243;mina</asp:ListItem>
                                        <asp:ListItem Value="1">Carga Masiva</asp:ListItem>
                                    </asp:RadioButtonList></td>
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
                                <td style="width: 116px; height: 10px">
                                </td>
                                <td style="width: 97px; height: 10px">
                                </td>
                                <td style="width: 100px; height: 10px">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <table style="width: 100%; height: 100%; background-color: lightgrey">
                            <tr>
                                <td style="width: 101px">
                                </td>
                                <td style="width: 89px">
                                </td>
                                <td style="width: 47px">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Ciclo :"></asp:Label></td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtCiclo4" runat="server" BackColor="#E0E0E0" Font-Bold="True" ReadOnly="True"
                                        Width="288px"></asp:TextBox></td>
                                <td style="width: 116px">
                                </td>
                                <td style="width: 169px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px; height: 24px">
                                </td>
                                <td style="width: 89px; height: 24px">
                                </td>
                                <td style="width: 47px; height: 24px">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Año :"></asp:Label></td>
                                <td style="width: 100px; height: 24px">
                                    <asp:TextBox ID="txtAño4" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                        ReadOnly="True" Width="41px"></asp:TextBox></td>
                                <td style="width: 100px; height: 24px">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Mes :"></asp:Label></td>
                                <td style="width: 100px; height: 24px">
                                    <asp:TextBox ID="txtMes4" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                        ReadOnly="True" Width="83px"></asp:TextBox></td>
                                <td style="width: 116px; height: 24px">
                                </td>
                                <td style="width: 169px; height: 24px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px">
                                </td>
                                <td style="width: 89px">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Registros a Procesar :"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtReg4" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                        ReadOnly="True" Width="56px"></asp:TextBox></td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 116px">
                                </td>
                                <td style="width: 169px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px; height: 10px">
                                </td>
                                <td style="width: 89px">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                        Text="Fecha de efecto :"></asp:Label></td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtFechaEfecto" runat="server" Width="79px"></asp:TextBox></td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 116px">
                                </td>
                                <td style="width: 169px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="1" style="font-weight: bold; font-size: small; width: 101px;
                                    font-family: Tahoma; height: 18px">
                                </td>
                                <td align="center" colspan="6" style="font-weight: bold; font-size: small; font-family: Tahoma;
                                    height: 18px">
                                    Procesos Generados</td>
                                <td align="center" colspan="1" style="font-weight: bold; font-size: small; font-family: Tahoma;
                                    height: 18px; width: 169px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px; height: 100px">
                                </td>
                                <td colspan="6">
                                    <asp:GridView ID="gvProcesos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                        Height="81px" Width="507px">
                                        <RowStyle BackColor="#E7E7FF" Font-Names="Tahoma" Font-Size="8pt" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="NMODULEC" HeaderText="M&#243;dulo">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad Titular">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKEY_T" HeaderText="Proceso Titular">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CANTIDAD_BENE" HeaderText="Cant_Ben">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKEY_B" HeaderText="Proceso Beneficiario">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                            ForeColor="#F7F7F7" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                    </asp:GridView>
                                </td>
                                <td style="width: 169px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="8" style="height: 46px">
                                    <asp:Button ID="btnProcesarPaso4" runat="server" Text="Procesar" Width="84px" /><asp:Button
                                        ID="btnRepFinalPaso4" runat="server" Text="Reporte Final" Width="91px" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
    &nbsp; &nbsp;&nbsp;
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFechaDef" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
    <br />
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
    <cc1:MaskedEditExtender ID="MkeFechaEfecto" runat="server" Mask="99/99/9999" MaskType="Date"
        TargetControlID="txtFechaEfecto" UserDateFormat="DayMonthYear">
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
