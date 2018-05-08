Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Data.OracleClient

Partial Class ActualizaciondatosSusalud
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet
    Dim dtformato As DataSet
    Dim dtArchivo As DataTable
    Dim sNroArchivo As Integer = 0
    Dim pro As New Process
    Dim dtCarga As DataSet


    Protected Property MaxRequestLength() As Integer
        Get

        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

              
                CalendarioInicio.Visible = False
                CalendarioFin.Visible = False
                txFechaEfectoInicio.Text = DateTime.Today.ToString("dd-MM-yyyy")
                txFechaEfectoFin.Text = DateTime.Today.ToString("dd-MM-yyyy")


                FileUpload1.Attributes.Add("onChange", "Seleccionar(this);")
                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")
                Call inicia()
                Call EstadoPasosNext(0)

                HF_SERVER.Value = ConfigurationManager.AppSettings("Server").ToString()
                HF_RUTA_MASIVA.Value = ConfigurationManager.AppSettings("RutaMasiva").ToString()
                HF_RUTA_CARGA.Value = ConfigurationManager.AppSettings("RutaCarga").ToString()
                HF_RUTA_SQL.Value = ConfigurationManager.AppSettings("RutaSql").ToString()
                HF_RUTA_TEMP.Value = ConfigurationManager.AppSettings("RutaTemp").ToString()
                HF_RUTA_EJECUTABLE.Value = ConfigurationManager.AppSettings("RutaEjecutables").ToString()
                HF_PASSINT.Value = ConfigurationManager.AppSettings("PasswordUserInt").ToString()
                HF_SERVER_SECURITY.Value = ConfigurationManager.AppSettings("ServerSecurity").ToString()

                PanelCierre.Visible = False
                PnelCarga.Visible = False
            End If

        Catch ex As Exception
            AlertaScripts("No es posible cargar datos iniciales")
        End Try
    End Sub

    Private Sub EstadoPasosNext(ByVal paso As Int16)
        Select Case paso
            Case 0
                Image1.Visible = False
                Image2.Visible = False
                Image3.Visible = False

            Case 1
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1a.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2r.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"

            Case 2
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2a.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"

            Case 3
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3a.png"

            Case 4
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"

            Case 5
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"

        End Select
    End Sub
    Private Sub EstadoPasosProc(ByVal paso As Int16)
        Select Case paso
            Case 1
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2r.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"

            Case 2
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"

            Case 3
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"

            Case 4
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"

        End Select
    End Sub

    Protected Sub inicia()
        Try
            FileUpload1.Visible = True
            txtRuta.Visible = False

            MultiView1.Visible = True
            MultiView1.ActiveViewIndex = 0
            txtRuta.Text = ""
            txtCabecera.Text = ""
            txtAño.Text = ""
            ddlMeses.SelectedIndex = 0
            txtComentario.Text = ""

            btnProblemas.Enabled = False
            txtTitulo.Text = "Carga de Archivo"
            ddlMeses.SelectedValue = Date.Now.Month.ToString.PadLeft(2, "0")
            txtAño.Text = Date.Now.Year

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub


    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim RutaBat As String = ""
        Dim sPeriodo As String = ""
        Dim sFormato As String = ""
        Dim sCiclo As Integer = 0
        Dim sSecPol As Integer = 0

        Try

            If FileUpload1.PostedFile.FileName.ToString <> "" And txtCabecera.Text.Trim <> "" Then
                'Dim sArchivoOrigen As String = HiddenField1.Value

                Dim sArchivoOrigen As String = FileUpload1.PostedFile.FileName
                Metodos.Abre_Ciclo(Metodos.DB.ORACLE, "ACTUA_ASEGURADOS", 0, 0, txtAño.Text.Trim & ddlMeses.Text, txtComentario.Text.Trim, sArchivoOrigen, txtCabecera.Text, txtComentario.Text.Trim)
                System.Threading.Thread.Sleep(500)
                dtArchivo = Metodos.Lista_Datos_Archivo(Metodos.DB.ORACLE, "ACTUA_ASEGURADOS", 0, 0, txtAño.Text.Trim & ddlMeses.Text)
                sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
                sSecPol = dtArchivo.Rows(0).Item("SEC_POLIZA").ToString
                sNroArchivo = dtArchivo.Rows(0).Item("Num_Archivo").ToString

                dtformato = Metodos.Lista_Datos_Formato(Metodos.DB.ORACLE, "ACTUA_ASEGURADOS", 0, 0, txtAño.Text.Trim & ddlMeses.Text, sCiclo)

                Session("DATOS_ARCHIVO") = dtArchivo
                Session("DATOS_FORMATO") = dtformato


                sFormato = dtformato.Tables(0).Rows(0).Item("FORMATO").ToString
                RutaBat = Server.MapPath("").Replace("\", "/")
                Dim ourFileGen As FileInfo = New FileInfo(RutaBat & "/bat/" & "ACTUA_ASEGURADOS" & "POL" & 0 & ".bat")
                If ourFileGen.Exists Then
                    My.Computer.FileSystem.DeleteFile(RutaBat & "/bat/" & "ACTUA_ASEGURADOS" & "POL" & 0 & ".bat")
                End If
                'crea carpeta de trabajo
                sPeriodo = txtAño.Text.Trim & ddlMeses.SelectedValue & "-" & sCiclo.ToString.PadLeft(3, "0")
                Call CreaDirectorioTrabajo(sPeriodo)

                ' Mover el fichero.si existe lo sobreescribe   
                Dim sRutaDestino As String = HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".TXT"

                FileUpload1.SaveAs(sRutaDestino)
                HF_RUTA_ARCHIVO.Value = sRutaDestino
                'Elimina archivos
                Dim ourFile As FileInfo = New FileInfo(HF_RUTA_EJECUTABLE.Value & "SALIDA.CTL")
                If ourFile.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_EJECUTABLE.Value & "SALIDA.CTL")
                End If

                Dim ourFileCTL As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL")
                If ourFileCTL.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL")
                End If

                Dim ourFileLOG As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG")
                If ourFileLOG.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG")
                End If

                Dim ourFileBAD As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".BAD")
                If ourFileBAD.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".BAD")
                End If

                Dim ourFileDSC As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".DSC")
                If ourFileDSC.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".DSC")
                End If

                Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".txt")
                If ourFiletxt.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".txt")
                End If
                'CREA ARCHIVO .CTL Y .BAT 
                Call CreaArchivoCTL(sPeriodo, sSecPol)
                System.Threading.Thread.Sleep(1000)

                Call CreaBat(sPeriodo, sSecPol)
                'borra tablas asociadas
                Metodos.Elimina_Archivo_Asociados(Metodos.DB.ORACLE, sNroArchivo)
                'genera secuencia
                Metodos.Genera_Secuencia(Metodos.DB.ORACLE, sFormato)
                'SQL*LOADER y LO EJECUTA
                RutaBat = Server.MapPath("").Replace("\", "/")

                Call EjecutaHilo()

                btnProblemas.Enabled = True
                txtRuta.Text = sArchivoOrigen
                FileUpload1.Visible = False
                txtRuta.Visible = True
                btnProcesar.Enabled = False

                Call EstadoPasosProc(1)
                Call VerificaProceso()
                'Actualiza estado
                Metodos.Archivo_Estado(Metodos.DB.ORACLE, sNroArchivo, "R")

                AlertaScripts("Proceso concluido")
            Else
                If FileUpload1.PostedFile.FileName.ToString = "" Then
                    AlertaScripts("Seleccione un archivo")
                ElseIf txtCabecera.Text.Trim = "" Then
                    AlertaScripts("Indicar número de cabecera")
                End If
            End If

        Catch ex As Exception
            'Throw ex
            AlertaScripts("No se pudo Procesar")
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            theFileBat = File.Open(ruta_Bat & "\bat\error1.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try

    End Sub

    Private Sub CreaDirectorioTrabajo(ByVal sPeriodo As String)
        Dim di As DirectoryInfo
        Dim rUTA As String = ""
        Try

            rUTA = HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "\PROD" & 0 & "\" & sPeriodo
            'Try
            If Directory.Exists(rUTA) = False Then
                di = Directory.CreateDirectory(rUTA)
            End If
            'Finally
            'End Try
        Catch ex As Exception
            AlertaScripts("No se pudo crear la carpeta de trabajo")

        End Try
    End Sub

    Private Sub CreaArchivoCTL(ByVal sPeriodo As String, ByVal sSecPol As Integer)
        Dim vComplemento As String
        Dim rut As String
        Dim texto As String = ""
        Dim caracter As String = "("
        Dim str As New System.Text.StringBuilder
        Dim caracter2 As String = """"

        Try
            'AlertaScripts("ctl -00")

            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")

            Dim vSkip As Integer = dtArchivo.Rows(0).Item("num_reg_skip")
            Dim vArchivo As String = dtArchivo.Rows(0).Item("archivo")
            Dim vTabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim vTipoFormato As String = dtformato.Tables(0).Rows(0).Item("Tip_formato").ToString
            Dim vDelimitador As String = "" & dtformato.Tables(0).Rows(0).Item("Delimitador").ToString
            Dim vFormato As String = "" & dtformato.Tables(0).Rows(0).Item("Formato").ToString
            'vArchivo = vArchivo.ToString.Replace("D:", HF_SERVER.Value)
            System.Threading.Thread.Sleep(500)
            'AlertaScripts("ctl -0")

            str.AppendLine("OPTIONS ( SKIP=" & vSkip & ")")
            str.AppendLine("LOAD DATA")
            str.AppendLine("INFILE '" & vArchivo & ".txt'")
            str.AppendLine("BADFILE '" & vArchivo & ".bad'")
            str.AppendLine("DISCARDFILE '" & vArchivo & ".dsc'")
            str.AppendLine("TRUNCATE")
            str.AppendLine("INTO TABLE """ & vTabla & """")
            If vTipoFormato = "D" Then
                str.AppendLine("FIELDS TERMINATED BY '" & vDelimitador & "' OPTIONALLY ENCLOSED BY '" & caracter2 & "' TRAILING NULLCOLS")
            End If

            For Each fila As DataRow In dtformato.Tables(3).Select("")
                str.AppendLine(caracter)
                If vTipoFormato = "F" Then
                    vComplemento = " POSITION (" & fila("INICIO") & ":" & fila("FIN") & ") CHAR"
                Else
                    vComplemento = ""
                End If
                str.AppendLine("   " & fila("COLUMNA") & vComplemento)
                caracter = ","
            Next
            str.AppendLine(",")
            str.AppendLine("   NUM_REGISTRO    ""seq_trx_in_" & vFormato & ".nextval""")
            str.AppendLine(")")
            texto = str.ToString

            'AlertaScripts("ctl 0")
            Dim theFile As FileStream
            rut = HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL"
            'AlertaScripts("ctl 0" & rut)

            theFile = File.Open(rut, FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribe As StreamWriter = New StreamWriter(theFile)
            filaEscribe.WriteLine(texto)
            filaEscribe.Close()
            theFile.Close()

        Catch ex As Exception
            AlertaScripts("error en ctl ")
        End Try
    End Sub

    Private Sub EjecutaHilo()
        Dim arrThreads(2) As Thread

        arrThreads(0) = New Thread(New ThreadStart(AddressOf CorreProceso))
        'arrThreads(1) = New Thread(New ThreadStart(AddressOf VerificaProceso))

        arrThreads(0).Start()
        'arrThreads(1).Start()
    End Sub
    Private Sub CorreProceso()

        Dim RutaBat As String = Server.MapPath("") '.Replace("\", "/")
        pro.StartInfo.WorkingDirectory = RutaBat & "\Bat\"

        pro.StartInfo.FileName = "ACTUA_ASEGURADOS" & "POL" & 0 & ".bat"
        'pro.WaitForInputIdle()
        pro.Start()



    End Sub
    Private Sub VerificaProceso()


        'Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("cmd")
        'For Each p As Process In pProcess
        '    p.Kill()
        'Next

        Try

salto:
            System.Threading.Thread.Sleep(2000)
            If (pro.HasExited = False) Then
                If (pro.Responding) Then
                    'El proceso estaba respondiendo; cerrar la ventana principal.
                    'pro.CloseMainWindow()
                    GoTo salto
                Else
                    'El proceso no estaba respondiendo; forzar el cierre del proceso.
                    pro.Kill()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub suma(ByVal z As Integer)
        Dim y As Integer = z
    End Sub
    Private Sub CreaBat(ByVal Periodo As String, ByVal SecPol As Integer)
        Dim tex As String = ""
        Try
            Dim ruta As String = ""
            Dim ruta_Bat As String = ""
            Dim vBaseDatos As String = ConfigurationManager.AppSettings("BD").ToString()
            Dim vPasswordBD As String = ConfigurationManager.AppSettings("PasswordBD").ToString()
            Dim vUserBD As String = ConfigurationManager.AppSettings("UserBD").ToString()

            ruta = HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "\PROD" & 0 & "\" & Periodo & "\POL" & 0 & "-" & SecPol.ToString.PadLeft(2, "0")

            Dim s As New System.Text.StringBuilder
            s.AppendLine("@echo off")
            s.AppendLine("sqlldr " & vUserBD & "/" & vPasswordBD & "@" & vBaseDatos & " control=" & ruta & ".CTL")
            s.AppendLine("move POL" & 0 & "-" & SecPol.ToString.PadLeft(2, "0") & ".LOG  " & ruta & ".LOG")
            's.AppendLine("pause")
            tex = s.ToString

            Dim theFileBat As FileStream
            ruta_Bat = Server.MapPath("")

            theFileBat = File.Open(ruta_Bat & "\bat\" & "ACTUA_ASEGURADOS" & "POL" & 0 & ".bat", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(tex)
            filaEscribebat.Close()
            theFileBat.Close()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguiente.Click
        If btnProcesar.Enabled = True Then
            AlertaScripts("Tiene que procesar para ir al siguiente paso")
            Exit Sub
        End If
        Try

            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")

            Dim dt As DataTable
            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            dt = Metodos.Lista_Tabla_Canal(Metodos.DB.ORACLE, vtabla)
            Dim vCantCorrec As Int64 = dt.Compute("count(NUM_REGISTRO)", "")

            If (CantRegArchivo() - txtCabecera.Text) - vCantCorrec > 0 Then
                AlertaScripts("No han pasado todos los registros. Verifique en el segundo paso de validación.")
            End If

            btnErroresPaso1.Enabled = False
            btnResumenPaso1.Enabled = False
            txtAño2.Text = txtAño.Text
            txtMes2.Text = ddlMeses.SelectedItem.Text
            txtCiclo2.Text = dtArchivo.Rows(0).Item("Periodo").ToString & dtArchivo.Rows(0).Item("Ciclo").ToString

            txtReg2.Text = vCantCorrec
            If txtReg2.Text = "0" Then
                AlertaScripts("No se pudo cargar los datos")
                btnProcesar.Enabled = True
            Else
                MultiView1.ActiveViewIndex = 1
                Call EstadoPasosNext(2)
                txtTitulo.Text = "Validación y Transferencia"
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Function CantRegArchivo() As Int64
        Try
            Dim x As Int64 = 0
            Dim rdr As StreamReader = File.OpenText(HF_RUTA_ARCHIVO.Value)

            While Not rdr.EndOfStream
                Dim line As String = rdr.ReadLine()
                x = x + 1
            End While
            rdr.Close()
            Return x
        Catch ex As Exception
            Throw
        End Try

    End Function

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Try

            inicia()
            Call EstadoPasosNext(0)
            btnProcesar.Enabled = True

        Catch ex As Exception
            AlertaScripts("No es posible Cerrar Paso 1")
        End Try
    End Sub

    Protected Sub btnProblemas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProblemas.Click

        Dim x(7) As String

        Try
            dtArchivo = Session("DATOS_ARCHIVO")

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo")
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = txtAño.Text.Trim & ddlMeses.SelectedValue

            x(0) = "Canal;" & "ACTUA_ASEGURADOS"
            x(1) = "Producto;" & ""
            x(2) = "Poliza;" & ""
            x(3) = "ciclo;" & ""
            x(4) = "Archivo;" & vArchivo
            x(5) = "NomEmpresa;Protecta Compañia de Seguros"
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;Proceso de actualización"

            Session("TABLA_REPORTE") = TablaProblemasCargaInicial()
            Session("RPT") = "Paso1_Carga.rpt"
            Session("NombreRPT") = "Carga"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Private Function TablaProblemasCargaInicial() As DataTable

        Try
            Dim dtr As DataRow
            Dim dt As New DataTable
            Dim errores As String = ""
            Dim exist As Integer = 0
            Dim exist2 As Integer = 0
            Dim dtArchivo As DataTable = Session("DATOS_ARCHIVO")
            Dim sCiclo As String = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = txtAño.Text.Trim & ddlMeses.SelectedValue & "-" & sCiclo.ToString.PadLeft(3, "0")
            Dim sSecPol As String = dtArchivo.Rows(0).Item("SEC_POLIZA").ToString
            Dim theFile As FileStream
            Dim ruta As String = HF_RUTA_MASIVA.Value & "ACTUA_ASEGURADOS" & "/PROD" & 0 & "/" & sPeriodo & "/POL" & 0 & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG"

            'defino columnas
            dt.Columns.Add("ID", Type.GetType("System.Int64"))
            dt.Columns.Add("Descripcion", Type.GetType("System.String"))


            'modo de apertura de archivo
            theFile = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.Read)

            Using filaLee As StreamReader = New StreamReader(theFile)
                Dim line As String
                Dim varid As Int64 = 0
                Dim var_frace As String
                'recorro linea por linea y grabo en datatable a partir q encuentra la palabra record  hasta q encuentre la palabra table '' o maximum
                Do
                    If exist > 0 Then
                        line = filaLee.ReadLine()
                        var_frace = Mid(line, 1, 6)
                        exist2 = InStr(var_frace, "Table ", CompareMethod.Text)
                        If exist2 > 0 Then
                            Exit Do
                        Else
                            var_frace = Mid(line, 1, 7)
                            exist2 = InStr(var_frace, "Maximum", CompareMethod.Text)
                            If exist2 > 0 Then
                                Exit Do
                            Else
                                dtr = dt.NewRow
                                dtr(0) = varid + 1
                                dtr(1) = line
                                dt.Rows.Add(dtr)
                            End If
                        End If

                    Else
                        line = filaLee.ReadLine()
                        var_frace = Mid(line, 1, 6)
                        exist = InStr(var_frace, "Record", CompareMethod.Text)
                        If exist > 0 Then
                            dtr = dt.NewRow
                            dtr(0) = varid + 1
                            dtr(1) = line
                            dt.Rows.Add(dtr)
                        End If
                    End If
                Loop Until line Is Nothing
                filaLee.Close()
            End Using

            theFile.Close()

            TablaProblemasCargaInicial = dt

        Catch ex As Exception
            AlertaScripts("No se pudo abrir el archivo log")
        End Try
    End Function

    Protected Sub btnProcesarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso1.Click
        Try
            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")
            Dim vConsistencia As String = dtformato.Tables(0).Rows(0).Item("CONSISTENCIA").ToString
            Dim vTranslada As String = dtformato.Tables(0).Rows(0).Item("TRASLADA_REG").ToString
            'inserta en la tabla de errores TBL_TRX_ERROR
            Dim NumArchivo As Integer = dtArchivo.Rows(0).Item("num_archivo")
            Metodos.Inserta_errores(Metodos.DB.ORACLE, NumArchivo, vConsistencia)

            Call EstadoPasosProc(2)

            'inserta en tabla tbl_trx_exclusion y tbl_trx_pol#poliza
            Metodos.Traslado_Registros(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), vTranslada)
            'ACTUALIZA ESTADO
            Metodos.Archivo_Estado(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), "C")
            System.Threading.Thread.Sleep(500)

            AlertaScripts("Proceso concluido")
            btnProcesarPaso1.Enabled = False
            btnErroresPaso1.Enabled = True
            btnCorrectosPaso1.Enabled = True
            btnResumenPaso1.Enabled = True

        Catch ex As Exception
            AlertaScripts(Err.Description)
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            theFileBat = File.Open(ruta_Bat & "\bat\error2.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try
    End Sub

    Protected Sub btnSiguientePaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguientePaso1.Click
        If btnErroresPaso1.Enabled = False Then
            AlertaScripts("Tiene que procesar para ir al siguiente paso")
            Exit Sub
        End If

        Try


            txtAño3.Text = txtAño2.Text
            txtMes3.Text = txtMes2.Text
            txtCiclo3.Text = txtCiclo2.Text
            txtReg3.Text = Metodos.Lista_Tabla_ACTUALIZARASEG_SUSALUD(Metodos.DB.ORACLE).Compute("count(NUM_REGISTRO)", "")
            btnCorrectosPaso1.Enabled = False

            If txtReg3.Text = "0" Then
                AlertaScripts("No se pudo cargar los datos. Revise el reporte de errores.")
                btnProcesarPaso1.Enabled = True
            Else
                MultiView1.ActiveViewIndex = 2
                Call EstadoPasosNext(3)
                txtTitulo.Text = "Generación"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnErroresPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnErroresPaso1.Click
        Try
            Dim dt As DataTable
            dtArchivo = Session("DATOS_ARCHIVO")
            Dim sNroArchivo As Integer = dtArchivo.Rows(0).Item("Num_Archivo").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString

            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & "ACTUA_ASEGURADOS"
            x(2) = "Producto;" & ""
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & ""
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;Proceso de actualización"
            dt = Metodos.Lista_Report_Errores(Metodos.DB.ORACLE, sNroArchivo)

            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso2_Errores.rpt"
            Session("NombreRPT") = "Errores"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Protected Sub btnCorrectosPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCorrectosPaso1.Click
        Try
            Dim dt As DataTable

            dtArchivo = Session("DATOS_ARCHIVO")
            Dim sNroArchivo As Integer = dtArchivo.Rows(0).Item("Num_Archivo").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString

            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & "ACTUA_ASEGURADOS"
            x(2) = "Producto;" & ""
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & ""
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;Proceso de actualización"

            dt = Metodos.Lista_Tabla_ACTUALIZARASEG_SUSALUD(Metodos.DB.ORACLE)

            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso2_Transferidos.rpt"
            Session("NombreRPT") = "Transferidos"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Protected Sub btnResumenPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResumenPaso1.Click
        Dim dt As DataTable
        Dim x(7) As String

        Try
            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo")
            Dim vTabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString

            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & "ACTUA_ASEGURADOS"
            x(2) = "Producto;" & ""
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & ""
            x(5) = "Archivo;" & vArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;Proceso de actualización"

            dt = Metodos.Lista_Report_Resumen(Metodos.DB.ORACLE, 0, vTabla, vArchivo)

            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso2_Resumen.rpt"
            Session("NombreRPT") = "Resumen"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Protected Sub btnCerrarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarPaso1.Click
        Try
            txtCiclo2.Text = ""
            txtAño2.Text = ""
            txtMes2.Text = ""
            txtReg2.Text = ""
            btnProcesarPaso1.Enabled = True
            btnProcesarPaso3.Enabled = True
            inicia()
            Call EstadoPasosNext(0)
            btnProcesar.Enabled = True

        Catch ex As Exception
            AlertaScripts("No es posible Cerrar Paso 2")
        End Try
    End Sub

    Protected Sub btnProcesarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso3.Click
        Try
            PnelCarga.Visible = True
            'lblMensajeProceso.Text = "¿Desea Crear la Tarea 89 (Carga Masiva de Asegurados)?"
            lblMensajeProceso.Text = "¿Desea actualizar los datos cargados(Actualización Masiva de Asegurados - SUSALUD)?"
            mpeCarga.Show()

        Catch ex As Exception
            AlertaScripts("Ocurrio un error inesperado al procesar")
        End Try
    End Sub

    Protected Sub imbAceptarCarga_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAceptarCarga.Click
        'Call Carga_Masiva_89()
        Call generaractualizacion()
        PnelCarga.Visible = False
        mpeCarga.Hide()
    End Sub


    Private Sub generaractualizacion()

        Dim estado As Boolean = Metodos.Genera_Actualizacion_susalud(Metodos.DB.ORACLE, Session("CodUsuario"))

        If estado Then
            btnProcesarPaso3.Enabled = False
            AlertaScripts("Se procesó correctamente la actualización")
        Else
            AlertaScripts("Hubo problemas al realizar la actualización")
        End If
    End Sub

    Private Sub Carga_Masiva_89()
        Try
            Dim DT As DataTable
            Dim sPeriodo As String = ""
            Dim sCiclo As Integer = 0

            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")
            Dim vTransformada As String = dtformato.Tables(0).Rows(0).Item("TRANSFORMADA").ToString


            sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
            'borra archivos
            Dim ourFilelst As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & 0 & ".LST")
            If ourFilelst.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & 0 & ".LST")
            End If
            Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & 0 & ".TXT")
            If ourFiletxt.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & 0 & ".TXT")
            End If
            'GUARDA DATOS PARA PASO 4 POR Q LIMPIA TABLA POL
            Session("TABLA_CARGA02") = Metodos.Lista_Tabla_CARGA02(Metodos.DB.ORACLE)
            DT = Session("TABLA_CARGA02")
            'Session("MODULOS") = Metodos.Lista_Modulo_Pol(Metodos.DB.ORACLE, 0)
            'genera archivo

            Metodos.Archivo_Genera_Tarea89(Metodos.DB.ORACLE, Session("CodUsuario"), vTransformada)

            'cambia de estado a 'G'
            Metodos.Archivo_Estado_PASO3(Metodos.DB.ORACLE, 0, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo, "G")

            If HF_TIPO.Value = "M" Then
                Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, "ACTUA_ASEGURADOS", 0, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo)
                btnCerrarPaso3.Enabled = False
            End If

            'System.Threading.Thread.Sleep(500)

            AlertaScripts("Generación exitosa")
            btnProcesarPaso3.Enabled = False
            btnCorrectosPaso1.Enabled = True
            Call EstadoPasosProc(3)
        Catch ex As Exception
            AlertaScripts(Err.Description)
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            theFileBat = File.Open(ruta_Bat & "\bat\error3.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try
    End Sub

    Protected Sub btnCerrarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarPaso3.Click
        Try
            txtCiclo3.Text = ""
            txtAño3.Text = ""
            txtMes3.Text = ""
            txtReg3.Text = ""
            btnProcesarPaso1.Enabled = True
            btnProcesarPaso3.Enabled = True
            inicia()

            Call EstadoPasosNext(0)
            btnProcesar.Enabled = True

        Catch ex As Exception
            AlertaScripts("No es posible Cerrar Paso 2")
        End Try
    End Sub

    Protected Sub BtnExportarExcel_Click(sender As Object, e As EventArgs) Handles BtnExportarExcel.Click
        If txFechaEfectoInicio.Text.Length <> 0 Or txFechaEfectoFin.Text.Length <> 0 Then

            If IsDate(txFechaEfectoInicio.Text.Trim) Then

                If IsDate(txFechaEfectoFin.Text.Trim) Then

                    If Convert.ToDateTime(txFechaEfectoFin.Text.Trim) < Convert.ToDateTime(txFechaEfectoInicio.Text.Trim) Then
                        AlertaScripts("La fecha de inicio no debe ser mayor a la fecha fin")
                        Exit Sub
                    End If
                    If Metodos.generarexcel(txFechaEfectoInicio.Text, txFechaEfectoFin.Text) = False Then
                        AlertaScripts("Hubo problemas al exportar el archivo Excel")
                    End If
                Else
                    AlertaScripts("Hubo problemas al exportar el archivo Excel")
                End If
            Else


            End If

        Else
            AlertaScripts("Debe ingresar la fecha de inicio y la fecha Fin para exportar el archivo")
        End If
    End Sub

    Protected Sub imabtncalendarioInicio_Click(sender As Object, e As ImageClickEventArgs) Handles imabtncalendarioInicio.Click
        If (CalendarioInicio.Visible) Then
            CalendarioInicio.Visible = False
        Else
            CalendarioInicio.Visible = True
        End If
    End Sub

    Protected Sub imabtncalendarioFin_Click(sender As Object, e As ImageClickEventArgs) Handles imabtncalendarioFin.Click
        If (CalendarioFin.Visible) Then
            CalendarioFin.Visible = False
        Else
            CalendarioFin.Visible = True
        End If
    End Sub

    Protected Sub CalendarioInicio_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarioInicio.SelectionChanged
        txFechaEfectoInicio.Text = CalendarioInicio.SelectedDate.ToString("dd-MM-yyyy")
        CalendarioInicio.Visible = False
    End Sub

    Protected Sub CalendarioFin_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarioFin.SelectionChanged
        txFechaEfectoFin.Text = CalendarioFin.SelectedDate.ToString("dd-MM-yyyy")
        CalendarioFin.Visible = False
    End Sub
End Class

