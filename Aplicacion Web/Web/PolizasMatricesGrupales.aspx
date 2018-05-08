<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PolizasMatricesGrupales.aspx.vb" Inherits="PolizasMatricesGrupales" 
    MasterPageFile="~/MasterPage.master"   Theme="Tema" Title="Generación de Pólizas Matrices Grupales" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>

   <div style="position:absolute; display:none; left:0px; background-color: rgba(232, 221, 219, 0.4);" id="DivProcessMessage">
        <div style="TEXT-ALIGN: center; width: 700px; height: 100px; left: 0px; background-color:white; margin-left:28%;margin-top:5%; border: 1px; " ><b>Cargando...</b><br />
            <img src="imagenes/progress_rojo.gif" /><br /></div>
    </div>
      <center>
          <table style="width: 70%; height: 100%;" border="0">
              <tr>
                  <td colspan="6">
                      <asp:Label ID="txtTitulo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt"
                          Text="Generación de Pólizas Matrices Grupales" ForeColor="Green"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td colspan="6"></td>
              </tr>
              <tr>
                  <td colspan="6"></td>
              </tr>
              <tr>
                  <td colspan="6">
                      
                  </td>
              </tr>
              <tr>
                  <td colspan="3"></td>
                  <td colspan="3">
                      <table style="width: 90%; height: 100%; background-color: gainsboro" border="0">
                          <tr>
                              <td style="width: 30%">
                                  <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                      Text="Canal :"></asp:Label>
                              </td>
                              <td style="width: 50%">
                                  <asp:DropDownList ID="ddlCanal" runat="server" AutoPostBack="True" Width="90%" TabIndex="1"></asp:DropDownList>
                              </td>
                              <td style="width: 20%; text-align: center;">
                                  <asp:Button ID="btnValidar" runat="server" TabIndex="5" Text="Validar" Width="90%" OnClientClick="MostrarEspera();"/>
                              </td>
                          </tr>
                          <tr>
                              <td style="width: 30%">
                                  <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                      Text="Producto :"></asp:Label>
                              </td>
                              <td style="width: 50%">
                                  <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" Width="90%" TabIndex="1"></asp:DropDownList>
                              </td>
                              <td style="width: 20%; text-align: center;">
                                  <asp:Button ID="btnProcesar" runat="server" TabIndex="6" Text="Procesar" Width="90%" Enabled="False" OnClientClick="MostrarEspera();" />
                              </td>
                          </tr>
                          <tr>
                              <td style="width: 30%">
                                  <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                      Text="Archivo :"></asp:Label>
                              </td>
                              <td style="width: 50%">
                                  <asp:Label ID="lblruta" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="8pt"></asp:Label>
                                 <asp:FileUpload ID="FileUpload1" runat="server" Width="80%" TabIndex="2" BackColor="Gainsboro" onChange="Seleccionar(this);"/>
                              </td>
                              <td style="width: 20%; text-align: center;">
                                  <asp:Button ID="btnError" runat="server" TabIndex="7" Text="Errores" Width="90%" Enabled="False" />
                              </td>
                          </tr>
                          <tr>
                              <td style="width: 30%">
                                  <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                      Text="Cabecera :"></asp:Label>
                              </td>
                              <td style="width: 50%">
                                  <asp:TextBox ID="txtCabecera" runat="server" TabIndex="3" Width="10%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                              </td>
                              <td style="width: 20%; text-align: center;">
                                  <asp:Button ID="btnResumen" runat="server" TabIndex="8" Text="Resumen" Width="90%" Enabled="False" />
                              </td>
                          </tr>
                          <tr>
                              <td style="width: 30%">
                                  <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                      Text="Comentario :"></asp:Label>
                              </td>
                              <td style="width: 50%">
                                  <asp:TextBox ID="txtComentario" runat="server" TabIndex="4" Width="90%"></asp:TextBox>
                              </td>
                              <td style="width: 20%; text-align: center;">
                                  <br />
                                  <asp:Button ID="btnCerrar" runat="server" TabIndex="9" Text="Cerrar" Width="90%" />
                              </td>
                          </tr>

                      </table>
                  </td>
              </tr>
              <caption>
                  <br/>                  
                  <tr>
                      <td colspan="6">
                          <br/>
                          <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Underline="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Gray" Text="Módulos configurados para la emisión de pólizas matrices"></asp:Label>                          
                      </td>
                  </tr>
                  <tr>
                      <td colspan="3"></td>
                      <td colspan="3">
                          <br/>                         
                          <asp:GridView ID="gbModulo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="311px">
                              <RowStyle BackColor="#F7F7DE" Font-Names="Tahoma" Font-Size="8pt" />
                              <Columns>
                                  <asp:TemplateField HeaderText="Sel."><ItemTemplate>
                                  <asp:CheckBox id="ChkSel" runat="server"></asp:CheckBox></ItemTemplate></asp:TemplateField>
                                  <asp:BoundField DataField="COD_MODUL" HeaderText="Módulo " />
                                  <asp:BoundField DataField="SMODUL" HeaderText="Descripción" />
                                  <asp:BoundField DataField="NTAX" HeaderText="Tasa" />
                              </Columns>
                              <FooterStyle BackColor="#CCCC99" />
                              <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                              <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                              <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt" ForeColor="White" />
                              <AlternatingRowStyle BackColor="White" />
                              <SortedAscendingCellStyle BackColor="#F7F7F7" />
                              <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                              <SortedDescendingCellStyle BackColor="#E5E5E5" />
                              <SortedDescendingHeaderStyle BackColor="#242121" />
                          </asp:GridView>
                          <br />
                          <asp:RadioButtonList id="rblmarcar" runat="server" Font-Size="10pt" Font-Names="Tahoma" Font-Bold="True" AutoPostBack="True" Width="356px" RepeatDirection="Horizontal">
                              <asp:ListItem Value="0">Marcar Todos</asp:ListItem>
                              <asp:ListItem Value="1">Desmarcar Todos</asp:ListItem>
                          </asp:RadioButtonList>
                      </td>
                  </tr>
              </caption>
          </table>

      </center>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="HF_RUTA_MASIVA" runat="server" />
    <asp:HiddenField ID="HF_CADENA" runat="server" />
    <asp:HiddenField ID="hdIDArchivoProcesador" runat="server" />
    <asp:HiddenField ID="idejcucion" runat="server" />
    <asp:HiddenField ID="herrores" runat="server" />
    <asp:HiddenField ID="hejecutados" runat="server" />
    <asp:HiddenField ID="hRuta" runat="server" />
    <script language="Javascript">



        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function Seleccionar(control) {

            var file = control.files[0];
            var filename = file.name;

            var valor = document.getElementById('<%=FileUpload1.ClientId%>').value;

            document.getElementById('<%=HiddenField1.ClientId%>').value = filename;

            document.getElementById('<%=hRuta.ClientID%>').value = filename;
        }
        function MostrarEspera() {
            var elem = document.getElementById("DivProcessMessage");
            elem.style.width = "100%";
            elem.style.height = "95%";
            elem.style.display = "block";
            return true;
        }
    </script>



</asp:Content>