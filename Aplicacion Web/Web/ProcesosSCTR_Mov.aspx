<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProcesosSCTR_Mov.aspx.vb" Inherits="ProcesosSCTR_Mov" 
    Title="Validador de archivos SCTR" Theme="Tema"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript" language="Javascript">
        function Seleccionar(control) {
            var valor = document.getElementById('<%=FileUpload1.ClientId%>').value;

            //Guardamos en un hidden
            document.getElementById('<%=HiddenField1.ClientId%>').value = valor;
            //onclick="return TABLE1_onclick()"
        }
        function TABLE1_onclick() {
        }
        function MostrarEspera() {
            $find('mpe').hide();
            var elem = document.getElementById("DivProcessMessage");
            elem.style.width = "100%";
            elem.style.height = "95%";
            elem.style.display = "block";
            return true;
        }

        function MostrarEspera2() {
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
    </script>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ScriptManager id="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div style="position:absolute; display:none; left:0px; background-color: rgba(232, 221, 219, 0.4);" id="DivProcessMessage">
        <div style="TEXT-ALIGN: center; width: 700px; height: 100px; left: 0px; background-color:white; margin-left:20%;margin-top:15%; border: 1px; " ><b>Cargando...</b><br />
            <img src="imagenes/progress_rojo.gif" /><br /></div>
    </div>
    <table id="TABLE1" style="width: 100%; border-top-style: groove; border-right-style: groove;
        border-left-style: groove; border-bottom-style: groove" onclick="return TABLE1_onclick()" cellpadding="0" cellspacing="0">
        <tr style="height:20px">
            <td colspan="6" style="text-align:center;">
                <asp:Label ID="lblTipoMovimiento" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt"
                    ForeColor="#D3343D"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" style="height: 30px">
            </td>
            <td align="right" colspan="2" style="height: 30px">
            </td>
            <td align="right" colspan="1" style="width: 678px; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 35px">
            </td>
            <td style="width: 282px; height: 35px;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Canal :"></asp:Label></td>
            <td colspan="3" style="height: 35px">
                <asp:DropDownList ID="ddlCanal" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" style="width: 678px; height: 35px">
            </td>
        </tr>
        <tr>
            <td style="width: 517px; height: 35px;">
            </td>
            <td style="width: 282px; height: 35px;" >
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                    Text="Producto :"></asp:Label></td>
            <td colspan="3" style="height: 35px" rowspan="">
                <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" Width="440px">
                </asp:DropDownList></td>
            <td colspan="1" rowspan="" style="width: 678px; height: 35px">
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
            <td colspan="2" style="height: 35px">
                <asp:Label ID="LblTit" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Small"
                    ForeColor="Crimson" Width="328px" Height="16px"></asp:Label><br />
                <asp:Label ID="LblCCarga" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Small" ForeColor="#0000C0" Height="32px" Width="280px" style="text-align:justify"></asp:Label>
            </td>
            <td style="height: 35px"></td>
        </tr>
        <tr>
            <td style="width: 517px; height: 43px;">
            </td>
            <td align="center" colspan="2" style="height: 43px">
                &nbsp;<asp:ImageButton ID="imbAceptar1" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" style="height: 22px" />
                <asp:ImageButton ID="imbCancelar" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
            <td style="width: 87px; height: 43px;">
            </td>
            <td style="width: 678px; height: 43px;">
            </td>
            <td style="width: 678px; height: 43px">
            </td>
        </tr>
        
        <tr runat="server" id="trIcons" visible="false" style="background-color: gainsboro">
            <td colspan="6" style="height: 20px" align="center" >
                <table style="width: 650px; height: 100%; background-color: gainsboro">
                    <tr>
                        <td style="width: 70%">
                            <asp:Label ID="txtTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt"
                                ForeColor="Green"></asp:Label>
                        </td>
                        <td style="width: 30%" align="center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/b1r.png" /><asp:Image
                                ID="Image2" runat="server" ImageUrl="~/Imagenes/b2r.png" /><asp:Image ID="Image3"
                                    runat="server" ImageUrl="~/Imagenes/b3r.png" /><asp:Image ID="Image4" runat="server"
                                        ImageUrl="~/Imagenes/b4r.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 200px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%; height: 100%; background-color: gainsboro;">
                            <tr>
                                <td>
                                    <table style="width: 650px; height: 100%; background-color: gainsboro; margin: 0 auto;">
                                        <tr>
                                            <td style="width: 20%; height: 21px"></td>
                                            <td style="width: 25%; height: 21px"></td>
                                            <td style="width: 7%; height: 21px"></td>
                                            <td style="width: 18%; height: 21px"></td>
                                            <td style="width: 30%; height: 21px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; height: 27px">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Archivo :"></asp:Label></td>
                                            <td colspan="3" style="height: 27px" valign="middle">
                                                <asp:FileUpload ID="FileUpload1" runat="server" onChange="Seleccionar(this);" Width="300px"/>
                                                <asp:TextBox ID="txtRuta" runat="server" Width="300px"></asp:TextBox></td>
                                            <td style="width: 30%; height: 27px" valign="middle">
                                                <asp:Button ID="btnProcesar" runat="server" TabIndex="5" Text="Procesar" Width="90%" OnClientClick="MostrarEspera2();"/></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; height: 26px;">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Cabecera :"></asp:Label></td>
                                            <td style="width: 25%; height: 26px;">
                                                <asp:TextBox ID="txtCabecera" runat="server" TabIndex="1" Width="80%"></asp:TextBox></td>
                                            <td style="width: 7%; height: 26px;"></td>
                                            <td style="width: 18%; height: 26px;"></td>
                                            <td style="width: 30%; height: 26px;">
                                                <asp:Button ID="btnSiguiente" runat="server" TabIndex="6" Text="Siguiente" Width="90%" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Año :"></asp:Label></td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtAño" runat="server" TabIndex="2" Width="80%"></asp:TextBox></td>
                                            <td style="width: 7%;">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Mes :"></asp:Label></td>
                                            <td colspan="1" style="width: 18%;">
                                                <asp:DropDownList ID="ddlMeses" runat="server" TabIndex="3">
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
                                            <td style="width: 30%;">
                                                <asp:Button ID="btnCerrar" runat="server" TabIndex="7" Text="Cerrar" Width="90%" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; height: 27px">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Comentario :"></asp:Label></td>
                                            <td colspan="3" style="height: 27px">
                                                <asp:TextBox ID="txtComentario" runat="server" MaxLength="80" TabIndex="4" Width="300px"></asp:TextBox></td>
                                            <td style="width: 30%; height: 27px">
                                                <asp:Button ID="btnResTotal" runat="server" TabIndex="8" Text="Resumen Total" Width="90%" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 30%;">
                                                <asp:Button ID="btnRegCorrectos" runat="server" TabIndex="9" Text="Registros Correctos" Width="90%" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 30%;">
                                                <asp:Button ID="btnProblemas" runat="server" Text="Error de Carga" Width="90%" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:View>

                    <asp:View ID="View2" runat="server">
                        <table style="width: 100%; height: 100%; background-color: gainsboro;">
                            <tr>
                                <td>
                                    <table style="width: 650px; height: 100%; background-color: gainsboro; margin: 0 auto;">
                                        <tr>
                                            <td style="width: 27%; height: 21px"></td>
                                            <td style="width: 18%; height: 21px"></td>
                                            <td style="width: 10%; height: 21px"></td>
                                            <td style="width: 15%; height: 21px"></td>
                                            <td style="width: 30%; height: 21px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%; height: 26px">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Ciclo :"></asp:Label>
                                            </td>
                                            <td colspan="3" style="height: 26px">
                                                <asp:TextBox ID="txtCiclo2" runat="server" BackColor="#E0E0E0" Font-Bold="True" 
                                                    Width="85%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 30%; height: 26px" align="center">
                                                <asp:Button ID="btnProcesarPaso1" runat="server" Text="Procesar" Width="90%" OnClientClick="MostrarEspera2();"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Año :"></asp:Label>
                                            </td>
                                            <td style="width: 18%">
                                                <asp:TextBox ID="txtAño2" runat="server" BackColor="#E0E0E0" Font-Bold="True" 
                                                    Width="90%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Mes :"></asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="txtMes2" runat="server" BackColor="#E0E0E0" Font-Bold="True" 
                                                    width="90%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 30%" align="center">
                                                <asp:Button ID="btnSiguientePaso1" runat="server" Text="Siguiente" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:27%">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Registros a Procesar :" ></asp:Label>
                                            </td>
                                            <td style="width: 18%">
                                                <asp:TextBox ID="txtReg2" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                                    width="90%" ReadOnly="True" ></asp:TextBox>
                                            </td>
                                            <td colspan="2"></td>
                                            <td style="width: 30%" align="center">
                                                <asp:Button ID="btnCerrarPaso1" runat="server" Text="Cerrar" Width="90%" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%">
                                                <asp:Label ID="lblFecEfecto0" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="8pt" Text="Fecha de Efecto"></asp:Label></td>
                                            <td style="width: 18%">
                                                <asp:TextBox ID="txtFecEfecto0" runat="server" Width="85%" Height="22px"></asp:TextBox>
                                            </td>
                                            <td colspan="2" rowspan="3"></td>
                                            <td style="width: 30%" align="center">
                                                <asp:Button ID="btnErroresPaso1" runat="server" Text="Errores" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%"></td>
                                            <td style="width: 18%"></td>
                                            <td style="width: 30%" align="center">
                                                <asp:Button ID="btnCorrectosPaso1" runat="server" Text="Transferidos" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%; height: 20px"></td>
                                            <td style="width: 18%"></td>
                                            <td style="width: 30%" align="center">
                                                <asp:Button ID="btnResumenPaso1" runat="server" Text="Resumen" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%"></td>
                                            <td style="width: 18%"></td>
                                            <td colspan="2" rowspan="1"></td>
                                            <td style="width: 30%" align="center"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%; height: 26px"></td>
                                            <td style="width: 18%; height: 26px"></td>
                                            <td colspan="2" rowspan="1" style="height: 26px"></td>
                                            <td align="center" style="width: 30%; height: 26px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </asp:View>
                    <asp:View ID="View3" runat="server">     
                        <table style="width: 100%; height: 100%; background-color: gainsboro;">
                            <tr>
                                <td>
                                    <table style="width: 650px; height: 100%; background-color: gainsboro; margin: 0 auto;">
                                        <tr>
                                            <td style="width: 27%"></td>
                                            <td style="width: 18%"></td>
                                            <td style="width: 10%"></td>
                                            <td style="width: 15%"></td>
                                            <td style="width: 30%"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%; height: 26px">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Ciclo :"></asp:Label></td>
                                            <td colspan="3" style="height: 26px">
                                                <asp:TextBox ID="txtCiclo3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Width="90%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 30%; height: 26px">
                                                <asp:Button ID="btnProcesarPaso3" runat="server" Text="Procesar" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Año :"></asp:Label></td>
                                            <td style="width: 18%">
                                                <asp:TextBox ID="txtAño3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                                    Width="85%" ReadOnly="True"></asp:TextBox></td>
                                            <td style="width: 10%">
                                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Mes :"></asp:Label></td>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="txtMes3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="14px"
                                                    Width="90%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 30%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                                    Text="Registros Transferidos :"></asp:Label></td>
                                            <td style="width: 18%">
                                                <asp:TextBox ID="txtReg3" runat="server" BackColor="#E0E0E0" Font-Bold="True" Height="15px"
                                                    Width="85%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td colspan="2"></td>
                                            <td style="width: 30%">
                                                <asp:Button ID="btnCerrarPaso3" runat="server" Text="Cerrar" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%">
                                                <asp:Label ID="lblFechaEfecto" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="8pt" Text="Fecha de Efecto"></asp:Label></td>
                                            <td style="width: 18%">
                                                <asp:TextBox ID="txtFechaDef" runat="server" Width="85%" Height="22px"></asp:TextBox>
                                            </td>
                                            <td colspan="2"></td>
                                            <td style="width: 30%">
                                                <asp:Button ID="btnRegxModPaso3" runat="server" Text="" Width="90%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%"></td>
                                            <td style="width: 18%"></td>
                                            <td colspan="2"></td>
                                            <td style="width: 30%" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 27%; height: 10px"></td>
                                            <td style="width: 18%; height: 10px"></td>
                                            <td style="width: 10%; height: 10px"></td>
                                            <td style="width: 15%; height: 10px"></td>
                                            <td style="width: 30%; height: 10px"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>                   
                        
                    </asp:View>                    
                </asp:MultiView>
            </td>
        </tr>
    </table>
    &nbsp; &nbsp;&nbsp;
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFechaDef" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFecEfecto0" UserDateFormat="DayMonthYear">
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
    <cc1:MaskedEditExtender ID="Mkecab" runat="server" Mask="9" MaskType="Number" TargetControlID="txtCabecera">
    </cc1:MaskedEditExtender>
    &nbsp; &nbsp;&nbsp;&nbsp;
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    </asp:UpdatePanel>
    <br />
    <%--<asp:UpdateProgress id="UpdateProgress1" runat="server" DisplayAfter="0">
        <progresstemplate>
        <DIV id="progressBackgroundFilter"></DIV><DIV style="TEXT-ALIGN: center" id="processMessage">Cargando...<BR /><IMG src="imagenes/progress_rojo.gif" /><BR /></DIV>
        </progresstemplate>
    </asp:UpdateProgress>--%>
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
                    <asp:ImageButton ID="imbAceptarCarga" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClientClick="MostrarEspera();"/></td>
                <td style="width: 100px">
                    <asp:ImageButton ID="imbCancelarCarga" runat="server" ImageUrl="~/Imagenes/Cancelar.gif" /></td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        <asp:Label ID="lblCarga" runat="server"></asp:Label></asp:Panel>
    <cc1:ModalPopupExtender ID="mpeCarga" runat="server" BackgroundCssClass="modalBackground"
        Drag="True" PopupControlID="PnelCarga" TargetControlID="lblCarga" BehaviorID="mpe">
    </cc1:ModalPopupExtender>
</asp:Content>
