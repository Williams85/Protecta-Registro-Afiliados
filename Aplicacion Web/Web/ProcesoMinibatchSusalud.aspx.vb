Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Data.OracleClient
' Inicio Modificación SUSSALUD 08/08/2017
Imports pe.gob.susalud.jr.transaccion.susalud.bean
Imports pe.gob.susalud.jr.transaccion.susalud.service.imp
Imports WinSCP
Imports System.Net


' Fin de modificación
Partial Class ProcesoMinibatchSusalud
    Inherits System.Web.UI.Page

    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet
    Dim dtformato As DataSet
    Dim dtformatoAct As DataSet
    Dim dtArchivo As DataTable
    Dim sNroArchivo As Integer = 0
    Dim pro As New Process
    Dim dtCarga As DataSet
    Dim RutaTramaX12N As String = ""

    Dim vtipo As Integer = 0
    Dim vcanal As String = ""
    Dim vproducto As Integer = 0
    Dim vpoliza As Integer = 0
    Dim rptainsertar As Boolean



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then

                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")
                Call CargarCombos()
                BtnGeneraTrama.Enabled = False


            End If

        Catch ex As Exception
            AlertaScripts("No es posible cargar datos iniciales")
        End Try
    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Private Sub CargarCombos()
        dtCombos = Metodos.Lista_Datos_Combos_SUSALUD(Metodos.DB.ORACLE)

        With ddlCanal
            .DataSource = dtCombos.Tables(0)
            .DataTextField = "DES_CANAL"
            .DataValueField = "CANAL"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlCanal)

        ddlProducto.Enabled = False
        ddlPoliza.Enabled = False
        Session("dtCombos") = dtCombos

        With ddlProducto
            .DataSource = dtCombos.Tables(1)
            .DataTextField = "NPRODUCT"
            .DataValueField = "NPRODUCT"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlProducto)

        With ddlPoliza
            .DataSource = dtCombos.Tables(2)
            .DataTextField = "NPOLICY"
            .DataValueField = "NPOLICY"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlPoliza)

        ddlCanal.SelectedIndex = 0
        ddlPoliza.SelectedIndex = 0
        ddlProducto.SelectedIndex = 0

    End Sub
    Protected Sub ddlCanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCanal.SelectedIndexChanged
        Try
            dtCombos = Session("dtCombos")
            Dim arr As New ArrayList
            For Each fila As DataRow In dtCombos.Tables(1).Select("CANAL='" & ddlCanal.SelectedValue & "'")
                arr.Add(New Listado(fila("NPRODUCT"), fila("SDESCRIPT")))
            Next

            With ddlProducto
                .DataSource = arr
                .DataTextField = "Descripcion"
                .DataValueField = "Codigo"
                .DataBind()
            End With
            Global.Metodos.AgregarItemCombo(ddlProducto)
            ddlProducto.Enabled = True
            ddlPoliza.SelectedIndex = 0
            ddlPoliza.Enabled = False

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnGeneraTrama_Click(sender As Object, e As EventArgs) Handles BtnGeneraTrama.Click
        Try
          
            BtnGeneraTrama.Enabled = False

            RutaTramaX12N = ConfigurationManager.AppSettings("RutaMasiva").ToString()
            If Me.ckbGenerarArchivo.Checked Then
                vtipo = 2
                vcanal = "2007_" & DateTime.Today.ToString("yyyyMMdd")
                vproducto = 0
                vpoliza = 0
            Else

                If ddlPoliza.SelectedIndex = 0 Then
                    AlertaScripts("Debe seleccionar un canal, producto y poliza para realizar el proceso de generarción de tramas a SUSALUD")
                    Exit Sub
                End If
                vtipo = 1
                vcanal = ddlCanal.SelectedValue
                vproducto = ddlProducto.SelectedValue
                vpoliza = ddlPoliza.SelectedValue
            End If

            Me.dtformato = Metodos.Lista_asegurado_trama_SUSALUD(Metodos.DB.ORACLE, vcanal, vproducto, vpoliza, vtipo)

            Session("DATOS_FORMATO_ACT") = dtformato

            Dim ourFile As FileInfo = New FileInfo(RutaTramaX12N & vcanal & "POL" & vpoliza & ".txt")
            If ourFile.Exists Then
                My.Computer.FileSystem.DeleteFile(RutaTramaX12N & vcanal & "POL" & vpoliza & ".txt")
            End If

            Dim theFileBat As FileStream
            'ruta_Bat = Server.MapPath("")

            Dim impl As RegafiUpdate271ServiceImpl = New RegafiUpdate271ServiceImpl()
            Dim dato As In271RegafiUpdate


            theFileBat = File.Open(RutaTramaX12N & vcanal & "POL" & vpoliza & ".txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            For Each row As DataRow In dtformato.Tables(0).Rows

                'dato = genera271RegafiUpdate("00")
                dato = genera271RegafiUpdateData(row.Item("tiOperacion").ToString(), row)

                Dim sx12n As String = impl.beanToX12N(dato)

                filaEscribebat.WriteLine(sx12n)

            Next

            txtRutaArchivo.Text = RutaTramaX12N & vcanal & "POL" & vpoliza & ".txt"
            filaEscribebat.Close()
            theFileBat.Close()

            BtnGeneraTrama.Enabled = True

        Catch ex As Exception

        End Try
    End Sub


    Public Function genera271RegafiUpdate(ByVal TipoOperacion As String) As In271RegafiUpdate


        Dim In271RegafiUpdate As In271RegafiUpdate = New In271RegafiUpdate()
        Dim in271RegafiUpdateAfiliado As In271RegafiUpdateAfiliado = New In271RegafiUpdateAfiliado()


        If (TipoOperacion = "00") Then


            In271RegafiUpdate.setNoTransaccion("271_REGAFI_UPDATE")
            In271RegafiUpdate.setIdRemitente("20007")
            In271RegafiUpdate.setIdReceptor("SUSALUD")
            In271RegafiUpdate.setFeTransaccion("20170721")
            In271RegafiUpdate.setHoTransaccion("093900")
            In271RegafiUpdate.setIdCorrelativo("000000001")
            In271RegafiUpdate.setIdTransaccion("271")
            In271RegafiUpdate.setTiFinalidad("13")
            In271RegafiUpdate.setCaRemitente("1")
            In271RegafiUpdate.setTiOperacion("00")


            in271RegafiUpdateAfiliado.setApPaternoAfiliado("QUIPUSCO")
            in271RegafiUpdateAfiliado.setNoAfiliado1("JAIME")
            in271RegafiUpdateAfiliado.setNoAfiliado2("")
            in271RegafiUpdateAfiliado.setApMaternoAfiliado("ESCOBEDO")
            in271RegafiUpdateAfiliado.setTiDocumentoAfil("1")
            in271RegafiUpdateAfiliado.setNuDocumentoAfil("06588686")
            in271RegafiUpdateAfiliado.setEstadoAfiliado("1")
            in271RegafiUpdateAfiliado.setTiDocumentoAct("")
            in271RegafiUpdateAfiliado.setNuDocumentoAct("")
            in271RegafiUpdateAfiliado.setApCasadaAfiliado("")
            in271RegafiUpdateAfiliado.setCoNacionalidad("PER")
            in271RegafiUpdateAfiliado.setUbigeo("140137")
            in271RegafiUpdateAfiliado.setFeNacimiento("19660221")
            in271RegafiUpdateAfiliado.setGenero("1")
            in271RegafiUpdateAfiliado.setCoPaisDoc("PER")
            in271RegafiUpdateAfiliado.setFefallecimiento("")
            in271RegafiUpdateAfiliado.setCoPaisEmisorDocAct("")
            in271RegafiUpdateAfiliado.setFeActPersonaxIafas("")
            in271RegafiUpdateAfiliado.setIdAfiliadoNombre("0")
            in271RegafiUpdateAfiliado.setTiDocTutor("")
            in271RegafiUpdateAfiliado.setNuDocTutor("")
            in271RegafiUpdateAfiliado.setTiVinculoTutor("")
            in271RegafiUpdateAfiliado.setFeValidacionReniec("")
            in271RegafiUpdateAfiliado.setCoPaisEmisorDocTutor("")

            Dim in271RegafiUpdateAfiliacion1 As In271RegafiUpdateAfiliacion = New In271RegafiUpdateAfiliacion()

            in271RegafiUpdateAfiliacion1.setTiDocTitular("1")
            in271RegafiUpdateAfiliacion1.setNuDocTitular("06588686")
            in271RegafiUpdateAfiliacion1.setFeNacimientoTitular("19660221")
            in271RegafiUpdateAfiliacion1.setCoPaisEmisorDocTitular("PER")

            in271RegafiUpdateAfiliacion1.setTiContratante("1")
            in271RegafiUpdateAfiliacion1.setApPaternoContratante("")
            in271RegafiUpdateAfiliacion1.setNoContratante1("")
            in271RegafiUpdateAfiliacion1.setNoContratante2("")
            in271RegafiUpdateAfiliacion1.setNoContratante3("")
            in271RegafiUpdateAfiliacion1.setNoContratante4("")
            in271RegafiUpdateAfiliacion1.setApMaternoContratante("")
            in271RegafiUpdateAfiliacion1.setTiDocContratante("1")
            in271RegafiUpdateAfiliacion1.setNuDocContratante("06588686")
            in271RegafiUpdateAfiliacion1.setApCasadaContratante("")
            in271RegafiUpdateAfiliacion1.setFeNacimientoContratante("")
            in271RegafiUpdateAfiliacion1.setCoPaisEmisorDocContratante("PER")

            in271RegafiUpdateAfiliacion1.setCoAfiliacion("COD0001")
            in271RegafiUpdateAfiliacion1.setCoContrato("CONTRATO0002")
            in271RegafiUpdateAfiliacion1.setCoUnicoMultisectorial("")
            in271RegafiUpdateAfiliacion1.setTiregimen("1")
            in271RegafiUpdateAfiliacion1.setEsAfiliacion("1")
            in271RegafiUpdateAfiliacion1.setCoCausalBaja("")
            in271RegafiUpdateAfiliacion1.setTiPlanSalud("2")
            in271RegafiUpdateAfiliacion1.setNoProductoSalud("PLAN PEAS Y COMPLEMENTARIO PRUEBA")
            in271RegafiUpdateAfiliacion1.setCoProducto("1")
            in271RegafiUpdateAfiliacion1.setParentesco("1")
            in271RegafiUpdateAfiliacion1.setCoRenipress("")
            in271RegafiUpdateAfiliacion1.setPkAfiliado("PER106588686")
            in271RegafiUpdateAfiliacion1.setFeActEstado("")
            in271RegafiUpdateAfiliacion1.setFeIniAfiliacion("20170701")
            in271RegafiUpdateAfiliacion1.setFeFinAfiliacion("")
            in271RegafiUpdateAfiliacion1.setFeIniCobertura("")
            in271RegafiUpdateAfiliacion1.setFeFinCobertura("")
            in271RegafiUpdateAfiliacion1.setFeActOperacion("20170730")
            in271RegafiUpdateAfiliacion1.setTiActOperacion("121000")
            in271RegafiUpdateAfiliacion1.setCoTiCobertura("2")

            in271RegafiUpdateAfiliacion1.setIdAfiliacionNombre("000")
            In271RegafiUpdate.addDetalle(in271RegafiUpdateAfiliado)
            In271RegafiUpdate.addDetalle(in271RegafiUpdateAfiliacion1)



        End If

        Return In271RegafiUpdate

    End Function

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

    Protected Sub ddlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try

            If ddlProducto.SelectedIndex = 0 Then
                ddlPoliza.SelectedIndex = 0
                ddlPoliza.Enabled = False

            Else
                ddlPoliza.Enabled = True

                dtCombos = Session("dtCombos")
                Dim arr As New ArrayList
                For Each fila As DataRow In dtCombos.Tables(2).Select("CANAL='" & ddlCanal.SelectedValue & "' AND NPRODUCT=" & ddlProducto.SelectedValue & "")
                    arr.Add(New Listado(fila("NPOLICY"), fila("NPOLICY")))
                Next

                With ddlPoliza
                    .DataSource = arr
                    .DataTextField = "Descripcion"
                    .DataValueField = "Codigo"
                    .DataBind()
                End With
                Global.Metodos.AgregarItemCombo(ddlPoliza)


            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPoliza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPoliza.SelectedIndexChanged
        BtnGeneraTrama.Enabled = True
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        If ddlCanal.SelectedIndex > 0 Then
            ddlProducto.SelectedIndex = 0
        End If
        Call CargarCombos()
        Call EstadoCombos()
        ddlCanal.SelectedIndex = 0
        ddlCanal.Enabled = True
        ddlPoliza.SelectedIndex = 0
        ddlPoliza.Enabled = False
        ddlProducto.Enabled = False

    End Sub

    Private Sub EstadoCombos()
        ddlCanal.Enabled = Not ddlCanal.Enabled
        ddlProducto.Enabled = Not ddlProducto.Enabled
        ddlPoliza.Enabled = Not ddlPoliza.Enabled
    End Sub

    Protected Sub ckbGenerarArchivo_CheckedChanged(sender As Object, e As EventArgs) Handles ckbGenerarArchivo.CheckedChanged
        If ckbGenerarArchivo.Checked Then
            ddlCanal.Enabled = False
            Me.ddlProducto.Enabled = False
            Me.ddlPoliza.Enabled = False
            Me.BtnGeneraTrama.Enabled = True
        Else
            ddlCanal.Enabled = True
            Me.ddlProducto.Enabled = True
            Me.ddlPoliza.Enabled = True
        End If
    End Sub

    Protected Sub BtnEnviarTrama_Click(sender As Object, e As EventArgs) Handles BtnEnviarTrama.Click

        If txtRutaArchivo.Text().Length() > 0 Then
            Dim usuREGAFI As String = ConfigurationManager.AppSettings("usuFTPS_SUSALUD").ToString()
            Dim pasREGAFI As String = ConfigurationManager.AppSettings("pasFTPS_SUSALUD").ToString()
            Dim hostname As String = ConfigurationManager.AppSettings("hostFTPS_SUSALUD").ToString()
            Dim puerto As Integer = CInt(ConfigurationManager.AppSettings("portFTPS_SUSALUD").ToString())
            Dim rutaformato As String = ConfigurationManager.AppSettings("rutaformatoFTPS_SUSALUD").ToString()

            Dim RutaArchivo As String = txtRutaArchivo.Text()

            'Envio al FTPS de SUSALUD el archivo generado(Trama X12N)
            'If CargarArchivoFTPS(usuREGAFI, pasREGAFI, hostname, puerto, RutaArchivo, RutaArchivo) = True Then
            If MoverArchivoFTP(RutaArchivo) = True Then

                ' Actualizar los registros de estado pendiente('P','C') en Proceso ('N') q

                dtformatoAct = Session("DATOS_FORMATO_ACT")
                Dim resultado As Integer

                For Each row As DataRow In dtformatoAct.Tables(0).Rows
                    resultado = Metodos.Actualizar_estado(Metodos.DB.ORACLE, CType(row.Item("idCorrelativo").ToString().Trim, Long))

                Next


                ''Insertar datos para la tabla auditoría

                'Dim fecha As String = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt")
                'Dim tipoenvio As String = "Reeenvio: "


                'rptainsertar = Metodos.Insertar_datos_auditoria(Metodos.DB.ORACLE, RutaArchivo, fecha, Session("CodUsuario"), tipoenvio & txtComentarioReenvio.Text.Trim)


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


    Public Function MoverArchivoFTP(ByVal rutaini As String) As Boolean
        Try
            ' File.Move(rutaini, "C:\FTP")
            System.IO.File.Copy(rutaini, "C:\FTP\20007_30082017.txt", True)
            Return True
        Catch ex As Exception
            Return False
        End Try

       

    End Function
    Private Function CargarArchivoFTPS(usuario As String, password As String, hostname As String, puerto As Integer, RutaArchivo As String, RutaFTPSFormato As String) As Boolean
        Try


            ' Enable FTPS in explicit mode
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Ftp
                .PortNumber = 61400
                .HostName = hostname
                .UserName = usuario
                .Password = password
                .FtpSecure = FtpSecure.Explicit
                .GiveUpSecurityAndAcceptAnyTlsHostCertificate = True
            End With
            'With sessionOptions
            '    .Protocol = Protocol.Ftp
            '    .PortNumber = 61400
            '    .HostName = "apprx.susalud.gob.pe"
            '    .UserName = usuario
            '    .Password = password
            '    .FtpSecure = FtpSecure.Explicit
            '    .GiveUpSecurityAndAcceptAnyTlsHostCertificate = True
            'End With


            Using session As New WinSCP.Session()
                ' Connect

                ' Your code
                session.Open(sessionOptions)
                session.PutFiles(RutaArchivo.Replace("'/", "'\"), RutaFTPSFormato).Check()
                'session.PutFiles("C:\Masiva\testfile4.txt", "\Archivo\\testfile4.txt").Check()
                'session.MoveFile("/home/user/myfile.dat.filepart", "/home/user/myfile.dat");
                Return True
            End Using



        Catch e As Exception

            Return False
        End Try

    End Function

    Private Function DescargaFTP(usuario As String, password As String, nombrefoto As String) As Boolean
        Try
            Dim estado As Boolean = False

            Dim displayRequest As FtpWebRequest = DirectCast(WebRequest.Create(Convert.ToString("ftp://app11.susalud.gob.pe/home/ftp/IAFAS_Prueba//v5" + "/") & "testfile.txt"), FtpWebRequest)

            'displayRequest.Method = WebRequestMethods.Ftp.DownloadFile
            displayRequest.Method = WebRequestMethods.Ftp.UploadFile

            displayRequest.Credentials = New NetworkCredential(usuario, password)

            'Dim displayResponse As FtpWebResponse = DirectCast(displayRequest.GetResponse(), FtpWebResponse)

            Dim sourceStream As StreamReader = New StreamReader("C:\Masiva\testfile.txt")

            Dim fileContents As Byte() = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd())
            sourceStream.Close()
            displayRequest.ContentLength = fileContents.Length

            'Dim responsestream As Stream = displayResponse.GetResponseStream()

            Dim requestStream As Stream = displayRequest.GetRequestStream()
            requestStream.Write(fileContents, 0, fileContents.Length)
            requestStream.Close()

            Dim response As FtpWebResponse = DirectCast(displayRequest.GetResponse(), FtpWebResponse)

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription)

            response.Close()


            Return estado

        Catch e As Exception

            Return False
        End Try

    End Function

End Class
