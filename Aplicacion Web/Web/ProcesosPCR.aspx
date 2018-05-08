<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProcesosPCR.aspx.vb" Inherits="ProcesosPCR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--Proyecto: PRY010
Requerimiento: RQ2017-0100001
Autor: EFITEC-MCM
--------------------------------------------------------------------------------------------------%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" src="JS/custom.js"></script>
    <title></title>

     <style>
        /* style sheet for tab*/      
      
        .TabStyle .ajax__tab_header
        {
            cursor: pointer;
            background-color: #f1f1f1;
            font-size: 12px;
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            height: 36px;
            border-bottom: 1px solid #bebebe;
            width:100%;
        }
      
        .TabStyle .ajax__tab_active .ajax__tab_tab
        {
            border: 1px solid;
            border-color: #D21F1B #D21F1B #e1e1e1 #D21F1B;
            background-color: #D21F1B;
            color: #FFFFFF;
            padding: 10px;
            border-bottom: none;
        }
        .TabStyle .ajax__tab_active .ajax__tab_tab:hover
        {
            border: 1px solid;
            border-color: #bebebe #bebebe #e1e1e1 #bebebe;
            background-color: #1E90FF;
            padding: 10px;
            border-bottom: none;
        }
      
        .TabStyle .ajax__tab_tab
        {
            border: 1px solid;
            border-color: #e1e1e1 #e1e1e1 #bebebe #e1e1e1;
            background-color: #f1f1f1;
            color: #777777;
            cursor: pointer;
            text-decoration: none;
            padding: 10px;
        }
        .TabStyle .ajax__tab_tab:hover
        {
            border: 1px solid;
            border-color: #bebebe #bebebe #e1e1e1 #bebebe;
            background-color: #e1e1e1;
            color: #777777;
            cursor: pointer;
            text-decoration: none;
            padding: 10px;
            border-bottom: none;
        }
        .TabStyle .ajax__tab_active .ajax__tab_tab, .TabStyle .ajax__tab_tab, .TabStyle .ajax__tab_header .ajax__tab_tab
        {
         
            margin: 0px 0px 0px 0px;
        }
      
        .TabStyle .ajax__tab_body
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #bebebe;
            border-top: none;
            padding: 5px;
            background-color: #FFFFFF;
            width:99%;
        }

         .auto-style1 {
             height: 25px;
         }

         .auto-style2 {
             height: 19px;
             width: 99px;
         }
         .auto-style3 {
             height: 25px;
             width: 99px;
         }
         .auto-style4 {
             height: 300px;
             width: 99px;
         }

    </style>

<script language="javascript" type="text/javascript">

    function funcion() {
        document.getElementById("HF_CONTADOR").value = 0;
        var contador = 0;
        var limite = 0;
    }
    function AgregarNuevoTabPanel() {
        if (Number(document.getElementById("HF_CONTADOR").value) == Number(document.getElementById("HF_LIMITE").value)) {
            return;
        }
        contador = Number(document.getElementById("HF_CONTADOR").value);
        contador = contador + 1;
        document.getElementById("HF_CONTADOR").value = contador;
        if (Number(contador) == Number(document.getElementById("HF_LIMITE").value)) {
            alert("Llego al limite de cargas permitidas...");
            return;
        }

        var TabContainerMainHeader = $get("TabContainerMain_header");
        var TabContainerMainBody = $get("TabContainerMain_body");
        var newSPAN = document.createElement('span');

        newSPAN.setAttribute("id", "__tab_TabContainerMain_TabPanel" + contador);
        newSPAN.innerHTML = "Carga " + (Number(contador) + 1);
        TabContainerMainHeader.appendChild(newSPAN);

        var newDIV = document.createElement('div'); 
        newDIV.setAttribute("id", "TabContainerMain_TabPanel" + contador);
        newDIV.style.display = "none";

        var newFrame = document.createElement('iframe');
        newFrame.setAttribute("id", "TabContainerMain_TabPanel" + contador);
        newFrame.style.display = "none";        
        newFrame.setAttribute("scrolling", "no");
        newFrame.setAttribute("frameborder", "0");
        newFrame.setAttribute("src", "ProcesosHCR.aspx?nuevapag=" + (Number(contador) + 1));
        newFrame.setAttribute("width", "100%");
        newFrame.setAttribute("height", 480);
        TabContainerMainBody.appendChild(newFrame);
        $create(AjaxControlToolkit.TabPanel, { "headerTab": $get("__tab_TabContainerMain_TabPanel" + contador) }, null, { "owner": "TabContainerMain" }, $get("TabContainerMain_TabPanel" + contador));
    }

   

</script>

<script type="text/javascript" language="Javascript">
    //'
    function preguntar() {
        var respuesta = window.confirm(Mensaje);
        if (respuesta) { document.Renovaciones.hide.value = "SI"; }
        else { document.Renovaciones.hide.value = "NO"; }
    }
</script>


</head>
    
<body onload="funcion();">
    <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    <form id="form2" runat="server">
        <div>
            <table style="width: 90%; height: 90%; margin-left: 50px; margin-top: 0px;">
                <tr>
                    <td colspan="3" style="height: 30px; background-color: #d9213c; background-attachment: fixed;"></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 19px">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                            Font-Size="8pt" ForeColor="#D3343D">Menú Inicio</asp:LinkButton></td>
                    <td align="center" colspan="1" class="auto-style2">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                            Font-Size="8pt" ForeColor="#D3343D">Cerrar Sesión</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Logo-transicion.jpg" Height="40px" Width="168px" />
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                            ForeColor="Gray" Text="Usuario :"></asp:Label>
                        <asp:Label ID="lblNomUsuario" runat="server" Font-Bold="True" Font-Names="Tahoma"
                            Font-Size="8pt" ForeColor="Gray"></asp:Label></td>
                    <td align="center" colspan="1" class="auto-style3"></td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 15px"></td>
                </tr>
                <tr>
                    <td style="width: 95px; height: 300px;"></td>
                    <td style="width: 1000px; height: 300px;" colspan="">
                         <asp:ScriptManager ID="ScriptManager1" runat="server" />
                            <div>
                            <table style="width: 100%">
                             <tr>
                                 <td></td>
                                  <td valign="top">
                                      
                                      &nbsp;</td>
                             </tr>
                             <tr>
                                 <td valign="top"><img alt="" id="imgAgregar" src="Imagenes/Add.png" onclick="javascript: AgregarNuevoTabPanel();" height="24" width="24" title="Agregar nueva carga..." onmouseover="" style="cursor: pointer;"/><br />
                                     <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                                 </td>
                              <td style="width: 100%">
                               <cc1:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0" CssClass="TabStyle">
                                <cc1:TabPanel ID="TabPanelMainHome" runat="server" HeaderText="Carga 1" >
                                   <ContentTemplate>
                                            
                                             <iframe id="iframe0" name="iframePag0" src="ProcesosHCR.aspx?nuevapag=1" frameborder="0" width="100%" height="480px" scrolling="no"></iframe>
                                   </ContentTemplate>
                                </cc1:TabPanel>
                               </cc1:TabContainer>
                              </td>
                             </tr>
                            </table>
                            <asp:HiddenField ID="HF_LIMITE" runat="server"  />
                            <asp:HiddenField ID="HF_CONTADOR" runat="server"  />
                            <asp:HiddenField ID="HF_RUTA_MASIVA" runat="server" Visible="False" />
                            </div>
                       
                    </td>
                    <td colspan="" class="auto-style4">
                        <br />
                        <asp:ListBox ID="lbxSession" runat="server" Visible="False"></asp:ListBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 5px">

                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 30px; background-color: #d21f1b"></td>
                </tr>
            </table>
            <br />

 
        </div>
    </form>
</body>
</html>



