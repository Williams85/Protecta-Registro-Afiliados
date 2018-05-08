Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Data.OracleClient

Imports pe.gob.susalud.jr.transaccion.susalud.bean
Imports pe.gob.susalud.jr.transaccion.susalud.service.imp
Imports WinSCP
Imports System.Net.Security
Imports System.Net
Imports System.Security.Cryptography.X509Certificates


Partial Class ProcesoReenvioSusalud
    Inherits System.Web.UI.Page

    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet
    Dim RutaTramaX12N As String = ""
    Dim dtformato As DataSet
    Dim dtformatoAct As DataSet
    Dim dtArchivo As DataTable
    Dim rptainsertar As Boolean

    Dim vtipo As Integer = 0
    Dim vcanal As String = ""
    Dim vproducto As Integer = 0
    Dim vpoliza As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then
                Calendario.Visible = False
                CalendarioRfin.Visible = False
                txFechaEfecto.Text = DateTime.Today.ToString("dd-MM-yyyyy")
                txFechaEfectoRfin.Text = DateTime.Today.ToString("dd-MM-yyyyy")
                BtnGeneraTrama.Enabled = False

                Call CargarCombos()

            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub CargarCombos()

        dtCombos = Metodos.Lista_EstadosDeEnvios(Metodos.DB.ORACLE)

        With ddlEstadoEnvio
            .DataSource = dtCombos.Tables(0)
            .DataTextField = "DESCRIPCION"
            .DataValueField = "NCODIGO"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlEstadoEnvio)


    End Sub

    Protected Sub imabtncalendario_Click(sender As Object, e As ImageClickEventArgs) Handles imabtncalendario.Click

        If (Calendario.Visible) Then
            Calendario.Visible = False
        Else
            Calendario.Visible = True
        End If

    End Sub

    Protected Sub Calendario_SelectionChanged(sender As Object, e As EventArgs) Handles Calendario.SelectionChanged
        txFechaEfecto.Text = Calendario.SelectedDate.ToString("dd-MM-yyyy")
        Calendario.Visible = False

    End Sub

    Protected Sub imabtncalendarioVal_Click(sender As Object, e As ImageClickEventArgs)
        If (Calendario.Visible) Then
            Calendario.Visible = False
        Else
            Calendario.Visible = True
        End If
    End Sub


    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub
  
   Protected Sub BtnGeneraTrama_Click(sender As Object, e As EventArgs) Handles BtnGeneraTrama.Click

        If chkGenTodoReenvio.Checked Then
            vtipo = 4
            vcanal = "2007_" & DateTime.Today.ToString("yyyyMMdd")
            vproducto = 0
            vpoliza = 0
        Else
            If ddlEstadoEnvio.SelectedIndex = 0 Then
                AlertaScripts("Debe seleccionar el estado para seguir con el proceso de genearación de la trama SUSALUD.")
                Exit Sub
            End If

            vtipo = 3
            vcanal = ddlEstadoEnvio.SelectedValue
            vproducto = 0
            vpoliza = 0
        End If


        RutaTramaX12N = ConfigurationManager.AppSettings("RutaMasiva").ToString()

        Me.dtformato = Metodos.Lista_asegurado_trama_SUSALUD(Metodos.DB.ORACLE, vcanal, vproducto, vpoliza, vtipo)

        Session("DATOS_FORMATO_ACT") = dtformato


        Dim ourFile As FileInfo = New FileInfo(RutaTramaX12N & "Reenvio_" & vcanal & "POL" & vpoliza & ".txt")
        If ourFile.Exists Then
            My.Computer.FileSystem.DeleteFile(RutaTramaX12N & "Reenvio_" & vcanal & "POL" & vpoliza & ".txt")
        End If

        Dim theFileBat As FileStream
        'ruta_Bat = Server.MapPath("")

        Dim impl As RegafiUpdate271ServiceImpl = New RegafiUpdate271ServiceImpl()
        Dim dato As In271RegafiUpdate


        theFileBat = File.Open(RutaTramaX12N & "Reenvio_" & vcanal & "POL" & vpoliza & ".txt", FileMode.OpenOrCreate, FileAccess.Write)
        Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
        For Each row As DataRow In dtformato.Tables(0).Rows

            'dato = genera271RegafiUpdate("00")
            dato = genera271RegafiUpdateData(row.Item("tiOperacion").ToString(), row)

            Dim sx12n As String = impl.beanToX12N(dato)

            filaEscribebat.WriteLine(sx12n)

        Next

        txtRuta.Text = RutaTramaX12N & "Reenvio_" & vcanal & "POL" & vpoliza & ".txt"
        filaEscribebat.Close()
        theFileBat.Close()

        BtnGeneraTrama.Enabled = True



    End Sub


    Protected Sub chkGenTodoReenvio_CheckedChanged(sender As Object, e As EventArgs) Handles chkGenTodoReenvio.CheckedChanged
        If chkGenTodoReenvio.Checked Then
            txFechaEfecto.Text = ""
            txFechaEfectoRfin.Text = ""
            txFechaEfecto.Enabled = False
            txFechaEfectoRfin.Enabled = False
            ddlEstadoEnvio.Enabled = False
            BtnGeneraTrama.Enabled = True
        Else

            txFechaEfecto.Enabled = True
            txFechaEfectoRfin.Enabled = True
            ddlEstadoEnvio.Enabled = True
        End If
    End Sub

    Protected Sub ddlEstadoEnvio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEstadoEnvio.SelectedIndexChanged
        BtnGeneraTrama.Enabled = True
    End Sub

    Protected Sub BtnGeneraTrama0_Click(sender As Object, e As EventArgs) Handles BtnGeneraTrama0.Click

        If txtRuta.Text().Length() > 0 Then
            Dim usuREGAFI As String = ConfigurationManager.AppSettings("usuFTPS_SUSALUD").ToString()
            Dim pasREGAFI As String = ConfigurationManager.AppSettings("pasFTPS_SUSALUD").ToString()
            Dim hostname As String = ConfigurationManager.AppSettings("hostFTPS_SUSALUD").ToString()
            Dim puerto As Integer = CInt(ConfigurationManager.AppSettings("portFTPS_SUSALUD").ToString())
            Dim rutaformato As String = ConfigurationManager.AppSettings("rutaformatoFTPS_SUSALUD").ToString()

            Dim RutaArchivo As String = txtRuta.Text()

            'Envio al FTPS de SUSALUD el archivo generado(Trama X12N)
            'If CargarArchivoFTPS(usuREGAFI, pasREGAFI, hostname, puerto, RutaArchivo, RutaArchivo) = True Then
            If MoverArchivoFTP(RutaArchivo) = True Then

                ' Actualizar los registros de estado pendiente('P','C') en Proceso ('N') q

                dtformatoAct = Session("DATOS_FORMATO_ACT")
                Dim resultado As Integer

                For Each row As DataRow In dtformatoAct.Tables(0).Rows
                    resultado = Metodos.Actualizar_estado(Metodos.DB.ORACLE, CType(row.Item("idCorrelativo").ToString().Trim, Long))

                Next



                'Insertar datos para la tabla auditoría

                Dim fecha As String = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt")
                Dim tipoenvio As String = "Reeenvio: "


                rptainsertar = Metodos.Insertar_datos_auditoria(Metodos.DB.ORACLE, RutaArchivo, fecha, Session("CodUsuario"), tipoenvio & txtComentarioReenvio.Text.Trim)

                AlertaScripts("Se envió el archivo con éxito")

            Else
                AlertaScripts("Hubo problemas al enviar el archivo")
            End If

            ' Upload a un ftp normal
            'If DescargaFTP(usuREGAFI, pasREGAFI, RutaArchivo) =True Then
            'AlertaScripts("Se envió el archivo con éxito")
            'Else
            'AlertaScripts("Hubo problemas al enviar el archivo con éxito")
            'End If
        Else
            AlertaScripts("Para enviar primero debe realizar la genearión del archivo X12N ")
        End If


    End Sub

    Protected Sub imabtncalendarioRfin_Click(sender As Object, e As ImageClickEventArgs) Handles imabtncalendarioRfin.Click
        If (CalendarioRfin.Visible) Then
            CalendarioRfin.Visible = False
        Else
            CalendarioRfin.Visible = True
        End If
    End Sub


    Public Function genera271RegafiUpdateData(ByVal TipoOperacion As String, ByVal row As DataRow) As In271RegafiUpdate

        Dim In271RegafiUpdate As In271RegafiUpdate = New In271RegafiUpdate()
        Dim in271RegafiUpdateAfiliado As In271RegafiUpdateAfiliado = New In271RegafiUpdateAfiliado()


        If (TipoOperacion = "00") Then


            In271RegafiUpdate.setNoTransaccion(row.Item("noTransaccion").ToString())
            In271RegafiUpdate.setIdRemitente(row.Item("idRemitente").ToString())
            In271RegafiUpdate.setIdReceptor(row.Item("idReceptor").ToString())
            In271RegafiUpdate.setFeTransaccion(row.Item("feTransaccion").ToString())
            In271RegafiUpdate.setHoTransaccion(row.Item("hoTransaccion").ToString())
            In271RegafiUpdate.setIdCorrelativo(row.Item("idCorrelativo").ToString())
            In271RegafiUpdate.setIdTransaccion(row.Item("idTransaccion").ToString())
            In271RegafiUpdate.setTiFinalidad(row.Item("tiFinalidad").ToString())
            In271RegafiUpdate.setCaRemitente(row.Item("caRemitente").ToString())
            In271RegafiUpdate.setTiOperacion(row.Item("tiOperacion").ToString())


            in271RegafiUpdateAfiliado.setApPaternoAfiliado(row.Item("apPaternoAfiliado").ToString())
            in271RegafiUpdateAfiliado.setNoAfiliado1(row.Item("noAfiliado1").ToString())
            in271RegafiUpdateAfiliado.setNoAfiliado2("")
            in271RegafiUpdateAfiliado.setApMaternoAfiliado(row.Item("apMaternoAfiliado").ToString())
            in271RegafiUpdateAfiliado.setTiDocumentoAfil(row.Item("tiDocumentoAfil").ToString())
            in271RegafiUpdateAfiliado.setNuDocumentoAfil(row.Item("nuDocumentoAfil").ToString())
            in271RegafiUpdateAfiliado.setEstadoAfiliado(row.Item("estadoAfiliado").ToString())
            in271RegafiUpdateAfiliado.setTiDocumentoAct("")
            in271RegafiUpdateAfiliado.setNuDocumentoAct("")
            in271RegafiUpdateAfiliado.setApCasadaAfiliado("")
            in271RegafiUpdateAfiliado.setCoNacionalidad(row.Item("coNacionalidad").ToString())
            in271RegafiUpdateAfiliado.setUbigeo("")
            in271RegafiUpdateAfiliado.setFeNacimiento(row.Item("feNacimiento").ToString())
            in271RegafiUpdateAfiliado.setGenero(row.Item("genero").ToString())
            in271RegafiUpdateAfiliado.setCoPaisDoc(row.Item("coPaisDoc").ToString())
            in271RegafiUpdateAfiliado.setFefallecimiento("")
            in271RegafiUpdateAfiliado.setCoPaisEmisorDocAct("")
            in271RegafiUpdateAfiliado.setFeActPersonaxIafas("")
            in271RegafiUpdateAfiliado.setIdAfiliadoNombre(row.Item("idAfiliadoNombre").ToString())
            in271RegafiUpdateAfiliado.setTiDocTutor("")
            in271RegafiUpdateAfiliado.setNuDocTutor("")
            in271RegafiUpdateAfiliado.setTiVinculoTutor("")
            in271RegafiUpdateAfiliado.setFeValidacionReniec("")
            in271RegafiUpdateAfiliado.setCoPaisEmisorDocTutor("")

            Dim in271RegafiUpdateAfiliacion1 As In271RegafiUpdateAfiliacion = New In271RegafiUpdateAfiliacion()

            in271RegafiUpdateAfiliacion1.setTiDocTitular(row.Item("tiDocTitular").ToString())
            in271RegafiUpdateAfiliacion1.setNuDocTitular(row.Item("nuDocTitular").ToString())
            in271RegafiUpdateAfiliacion1.setFeNacimientoTitular(row.Item("feNacimientoTitular").ToString())
            in271RegafiUpdateAfiliacion1.setCoPaisEmisorDocTitular(row.Item("coPaisEmisorDocTitular").ToString())

            in271RegafiUpdateAfiliacion1.setTiContratante(row.Item("tiContratante").ToString())
            in271RegafiUpdateAfiliacion1.setApPaternoContratante("")
            in271RegafiUpdateAfiliacion1.setNoContratante1("")
            in271RegafiUpdateAfiliacion1.setNoContratante2("")
            in271RegafiUpdateAfiliacion1.setNoContratante3("")
            in271RegafiUpdateAfiliacion1.setNoContratante4("")
            in271RegafiUpdateAfiliacion1.setApMaternoContratante("")
            in271RegafiUpdateAfiliacion1.setTiDocContratante(row.Item("tiDocContratante").ToString())
            in271RegafiUpdateAfiliacion1.setNuDocContratante(row.Item("nuDocContratante").ToString())
            in271RegafiUpdateAfiliacion1.setApCasadaContratante("")
            in271RegafiUpdateAfiliacion1.setFeNacimientoContratante("")
            in271RegafiUpdateAfiliacion1.setCoPaisEmisorDocContratante(row.Item("coPaisEmisorDocContratante").ToString())

            in271RegafiUpdateAfiliacion1.setCoAfiliacion(row.Item("coAfiliacion").ToString())
            in271RegafiUpdateAfiliacion1.setCoContrato(row.Item("coContrato").ToString())
            in271RegafiUpdateAfiliacion1.setCoUnicoMultisectorial("")
            in271RegafiUpdateAfiliacion1.setTiregimen(row.Item("tiregimen").ToString())
            in271RegafiUpdateAfiliacion1.setEsAfiliacion(row.Item("esAfiliacion").ToString())
            in271RegafiUpdateAfiliacion1.setCoCausalBaja("")
            in271RegafiUpdateAfiliacion1.setTiPlanSalud(row.Item("tiPlanSalud").ToString())
            'in271RegafiUpdateAfiliacion1.setNoProductoSalud("PLAN PEAS Y COMPLEMENTARIO PRUEBA")
            in271RegafiUpdateAfiliacion1.setNoProductoSalud(row.Item("noProductoSalud").ToString().Replace(".", ""))
            in271RegafiUpdateAfiliacion1.setCoProducto(row.Item("coProducto").ToString())
            in271RegafiUpdateAfiliacion1.setParentesco(row.Item("parentesco").ToString())
            in271RegafiUpdateAfiliacion1.setCoRenipress("")
            in271RegafiUpdateAfiliacion1.setPkAfiliado(row.Item("pkAfiliado").ToString())
            in271RegafiUpdateAfiliacion1.setFeActEstado("")
            in271RegafiUpdateAfiliacion1.setFeIniAfiliacion(row.Item("feIniAfiliacion").ToString())
            in271RegafiUpdateAfiliacion1.setFeFinAfiliacion(row.Item("feFinAfiliacion").ToString()) ' Opcional
            in271RegafiUpdateAfiliacion1.setFeIniCobertura("")
            in271RegafiUpdateAfiliacion1.setFeFinCobertura("")
            in271RegafiUpdateAfiliacion1.setFeActOperacion("")
            in271RegafiUpdateAfiliacion1.setTiActOperacion("")
            in271RegafiUpdateAfiliacion1.setCoTiCobertura(row.Item("coTiCobertura").ToString())

            in271RegafiUpdateAfiliacion1.setIdAfiliacionNombre(row.Item("idAfiliacionNombre").ToString())
            In271RegafiUpdate.addDetalle(in271RegafiUpdateAfiliado)
            In271RegafiUpdate.addDetalle(in271RegafiUpdateAfiliacion1)



        End If

        Return In271RegafiUpdate

    End Function


    Public Function MoverArchivoFTP(ByVal rutaini As String) As Boolean
        Try
            ' File.Move(rutaini, "C:\FTP")
            System.IO.File.Copy(rutaini, "C:\FTP\20007_30082017.txt", True)
            Return True
        Catch ex As Exception
            Return False
        End Try



    End Function
End Class
