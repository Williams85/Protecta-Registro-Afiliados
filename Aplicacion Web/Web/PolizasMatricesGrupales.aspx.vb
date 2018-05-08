Imports System
Imports System.Data
Imports System.IO.File
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Globalization

Partial Class PolizasMatricesGrupales
    Inherits System.Web.UI.Page

    Dim dtCombos As New DataSet
    Dim Metodos As New Metodos
    Dim dtformato As DataSet
    Dim dSource As New DataSet

    Dim dCodFormato As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If StatusSession() Then
                If Not IsPostBack Then

                    idejcucion.Value = 0

                    CargarCombos()
                    HF_RUTA_MASIVA.Value = ConfigurationManager.AppSettings("RutaMasiva").ToString()
                    Label5.Visible = False
                    rblmarcar.Visible = False

                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception
            AlertaScripts("No es posible cargar datos iniciales.")
            Response.Redirect("Default.aspx")
        End Try
    End Sub


#Region "Combo"

    Private Sub CargarCombos()
        dtCombos = Metodos.Lista_Datos_Combos(Metodos.DB.ORACLE)

        Dim arr As New ArrayList
        For Each fila As DataRow In dtCombos.Tables(0).Select("CANAL='VLEY'")
            arr.Add(New Listado(fila("CANAL"), fila("DES_CANAL")))
        Next

        With ddlCanal
            .DataSource = arr
            .DataTextField = "Descripcion"
            .DataValueField = "Codigo"
            .DataBind()
        End With

        Global.Metodos.AgregarItemCombo(ddlCanal)

        ddlProducto.Enabled = False
        Session("dtCombos") = dtCombos

        With ddlProducto
            .DataSource = dtCombos.Tables(1)
            .DataTextField = "NPRODUCT"
            .DataValueField = "NPRODUCT"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlProducto)

        ddlCanal.SelectedIndex = 0
        ddlProducto.SelectedIndex = 0

       
    End Sub

    Private Sub ObtenerFormato()
        dSource = Metodos.ListarFormatoxProducto(Metodos.DB.ORACLE, ddlProducto.SelectedValue)
        Dim datatable As New DataTable
        datatable = dSource.Tables(0)

        If (datatable.Rows.Count > 0) Then

            For Each rows As DataRow In datatable.Rows
                dCodFormato = (rows("FORMATO").ToString())
            Next
        End If



    End Sub

    Function StatusSession() As Boolean
        Dim rpsSession As Boolean = False
        Try
            Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
            txt.Text = Session("Nombre")
            If (String.IsNullOrEmpty(txt.Text) = False) Then
                rpsSession = True
            End If
        Catch ex As Exception
            rpsSession = False
        End Try
        Return rpsSession
    End Function


#End Region

#Region "Metodos"

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Public Function FormatNameTrama(ByVal FileName As String, ByVal Delimitador As Char) As Boolean
        Dim boolStatus As Boolean = True

        Dim Array() As String = {}
        Dim Array_() As String = {}
        Dim ArrayN() As String = {}
        Dim Fecha() As String = {}
        Dim ERROR_ As String = ""
        idejcucion.Value = 1
        Array = FileName.Split(Delimitador)
        Try

            If Array(0) <> "EMISION" Then
                ERROR_ += Array(0) + "<br>"
                boolStatus = False
                GoTo salto
            End If
            If Array(1) <> "POL" Then
                ERROR_ += Array(1) + "<br>"
                boolStatus = False
                GoTo salto
            End If
            Select Case Array.Length
                Case 3
                    Array_ = Array(2).Split(".")
                    Fecha = Array_(0).Split("-")
                Case 4
                    ArrayN = Array(3).Split(".")
                    Fecha = Array(2).Split("-")
            End Select


            If Fecha(0).Length = 2 And Fecha(1).Length = 2 And Fecha(2).Length = 4 Then

                If IsNumeric(Fecha(0)) And IsNumeric(Fecha(1)) And IsNumeric(Fecha(2)) Then

                    Select Case Array.Length
                        Case 4
                            Try
                                Dim correlativo As Integer
                                Integer.TryParse(ArrayN(0).ToString(), correlativo)

                                If correlativo > 0 Then
                                    boolStatus = True
                                Else
                                    ERROR_ += "Correlativo no tiene formato indicado." + "<br>"
                                    boolStatus = False
                                    GoTo salto
                                End If


                                'If IsNumeric(ArrayN(0)) Then
                                '    boolStatus = True
                                'Else
                                '    ERROR_ += "Correlativo no tiene formato indicado." + "<br>"
                                '    boolStatus = False
                                '    GoTo salto
                                'End If

                            Catch ex As Exception
                                ERROR_ += "Correlativo no tiene formato indicado." + "<br>"
                                boolStatus = False
                                GoTo salto
                            End Try
                    End Select
                Else

                    boolStatus = False
                    ERROR_ += "Fecha no tiene el formato correcto" + "<br>"
                    GoTo salto

                End If
            Else
                ERROR_ += "Fecha no válida" + "<br>"

                boolStatus = False
                GoTo salto
            End If



        Catch ex As Exception
            boolStatus = False
        End Try

salto:

        If boolStatus <> True Then
            Dim sArchivo As Integer = 0
            ERROR_ = ConfigurationManager.AppSettings("FormatoEmiMatriz").ToString()
            sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, ERROR_, Convert.ToInt32(hdIDArchivoProcesador.Value))
        End If

        If ERROR_ <> "" Then
            boolStatus = False
        End If
        Return boolStatus
    End Function

#End Region

    Protected Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        Dim ActionValidar As Integer = 0

        Try

            Dim FileName As String = FileUpload1.FileName.ToString()
            Dim dtResult As New DataSet

            If ddlCanal.SelectedIndex > 0 And ddlProducto.SelectedIndex > 0 And txtCabecera.Text <> "" And FileName <> "" Then
                ObtenerFormato()
                HiddenField2.Value = FileName
                lblruta.Text = FileName
                dtformato = Metodos.ListarFormato_Columna(Metodos.DB.ORACLE, dCodFormato)
                Dim dFecha As String = ""
                dFecha = Date.Now.Day.ToString() & Date.Now.Month.ToString() & Date.Now.Year.ToString()
                Call CreaDirectorioTrabajo(dFecha)
                Dim sRutaDestino As String = LTrim(RTrim(HF_RUTA_MASIVA.Value & "MATRIZ MASIVO" & "/" & ddlCanal.Text & "/" & dFecha & "/" & Session("Nombre").trim & "/" & HiddenField2.Value))
                Dim sRutaTemp As String = LTrim(RTrim(HF_RUTA_MASIVA.Value & "MATRIZ MASIVO" & "/" & ddlCanal.Text & "/" & dFecha & "/" & Session("Nombre").trim))

                Try
                    FileUpload1.SaveAs(sRutaDestino)

                Catch ex As Exception
                    AlertaScripts("Error al leer el archivo")
                End Try

                Dim sArchivo As Integer = 0
                Dim sArchivoOrigen As String = FileUpload1.PostedFile.FileName
                sArchivo = Metodos.OpenLoadFile(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, sArchivoOrigen, Convert.ToInt32(txtCabecera.Text), sRutaDestino, txtComentario.Text, Convert.ToInt32(Session("CodUsuario")))
                hdIDArchivoProcesador.Value = sArchivo
                If FormatNameTrama(FileName, "_") Then
                    AlertaScripts("Finalizó el Proceso de Validación")
                    btnError.Enabled = False

                    If sArchivo > 0 Then
                        Try
                            If (GetReadFile(sRutaDestino, sRutaTemp, sArchivo)) Then

                                FileUpload1.Enabled = False
                                FileUpload1.Visible = False
                                btnProcesar.Enabled = True
                                btnValidar.Enabled = False

                                dtResult = Metodos.ListarError_Polizas(Metodos.DB.ORACLE, Convert.ToInt32(hdIDArchivoProcesador.Value))
                                If dtResult.Tables(1).Rows.Count > 0 Then
                                    Session("ErrorCase") = 2
                                    btnError.Enabled = True
                                Else
                                    btnError.Enabled = False
                                End If

                            Else
                                lblruta.Text = ""
                                FileUpload1.Enabled = True
                                FileUpload1.Visible = True
                                txtCabecera.Text = ""
                                AlertaScripts("No fue posible procesar el archivo cargado.")
                            End If

                        Catch ex As Exception

                            txtCabecera.Text = ""
                            AlertaScripts("No fue posible procesar el archivo cargado.")
                        End Try
                    Else
                        AlertaScripts("No fue posible procesar el archivo cargado.")

                        txtCabecera.Text = ""

                        FileUpload1.Enabled = True
                    End If
                Else
                    Session("ErrorCase") = 1
                    btnError.Enabled = True

                    FileUpload1.Enabled = True
                    AlertaScripts("Finalizó el Proceso de Validación . El nombre del archivo no tiene el formato correcto.")

                    txtCabecera.Text = ""
                    lblruta.Text = ""
                    FileUpload1.Enabled = True
                    FileUpload1.Visible = True
                End If
            Else
                btnProcesar.Enabled = False

                FileUpload1.Enabled = True

                If ddlCanal.SelectedIndex = 0 And ddlProducto.SelectedIndex = 0 And String.IsNullOrEmpty(txtCabecera.Text) And String.IsNullOrEmpty(FileUpload1.FileName.ToString()) Then
                    AlertaScripts("Seleccionar Canal, código de Producto, ingresar el Documento a Procesar e indicar el número de Cabecera.")

                    txtCabecera.Text = ""
                    lblruta.Text = ""
                    FileUpload1.Enabled = True
                    FileUpload1.Visible = True
                Else
                    If ddlCanal.SelectedIndex = 0 Then
                        AlertaScripts("Seleccionar Canal.")

                        ddlCanal.SelectedIndex = 0
                    ElseIf ddlProducto.SelectedIndex = 0 Then
                        AlertaScripts("Seleccionar código de Producto.")

                        ddlProducto.SelectedIndex = 0
                    ElseIf String.IsNullOrEmpty(FileUpload1.FileName.ToString()) Then
                        AlertaScripts("Seleccione un archivo.")

                        txtCabecera.Text = ""
                    ElseIf String.IsNullOrEmpty(txtCabecera.Text) Then
                        AlertaScripts("Ingrese el número de cabecera.")

                        txtCabecera.Text = ""
                        lblruta.Text = ""
                        FileUpload1.Enabled = True
                        FileUpload1.Visible = True

                    End If
                End If
            End If

        Catch ex As Exception
            Dim message_ As String = String.Empty
            message_ = ex.Message.ToString()
            AlertaScripts("Error Interno.")
            lblruta.Text = ""
            FileUpload1.Enabled = True
            FileUpload1.Visible = True
            txtCabecera.Text = ""
        End Try

    End Sub

    Protected Sub btnError_Click(sender As Object, e As EventArgs) Handles btnError.Click

        Dim reporte As String = ""
        Dim number As Integer
        Select Case Session("ErrorCase")
            Case 1
                number = 2
            Case 2
                number = 4
            Case 3
                number = 4
            Case 4
                number = 4
        End Select
        Dim x(number) As String
        Try


            x(0) = "Canal;" & ddlCanal.SelectedItem.Text
            x(1) = "RutaArchivo;" & Convert.ToString(HiddenField2.Value)
            x(2) = "Titulo;" & "Registro de Errores"
            Session("TABLA_REPORTE") = GetErrorCase()

            Select Case Session("ErrorCase")
                Case 1
                    reporte = "RptSctrEmiError.rpt"
                Case 2
                    reporte = "RptErrorTramaSctr.rpt"
                    x(3) = "registro;" & herrores.Value
                    x(4) = "idcarga;" & hdIDArchivoProcesador.Value
                Case 3
                    reporte = "RptErrorTramaSctr.rpt"
                    x(3) = "registro;" & herrores.Value
                    x(4) = "idcarga;" & hdIDArchivoProcesador.Value
            End Select

            Session("RPT") = reporte
            Session("NombreRPT") = "Carga"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())

        Catch ex As Exception
            AlertaScripts("Ha ocurrido un problema al generar el reporte.")
        End Try



    End Sub

    Private Function GetReadFile(ByVal sRutaDestino As String, sRutaTemp As String, sIdArchivo As Integer) As Boolean

        Dim status As Boolean = True

        Try
            Dim sArchivoOrigen As String = FileUpload1.PostedFile.FileName
            Dim vDelimitador As String = "<br>"

            Dim dtTable As New DataTable
            dtTable = EstructuraTramaCorrecta()

            Dim dtTableError As New DataTable
            dtTableError = EstruturaTramaGenerica()

            Dim dtTableProcesar As New DataTable
            dtTableProcesar = EstructuraTramaCorrecta()



            Using fileWrite As New StreamWriter(sRutaTemp + "temp.txt", True, System.Text.Encoding.Default)
                Using fielRead As New StreamReader(sRutaDestino, System.Text.Encoding.Default)

                    Dim line As String = fielRead.ReadLine
                    Dim contador As Integer = 1
                    Do While (Not line Is Nothing)

                        Dim error_ As String = ""
                        Dim CadenaList() As String = line.Split(",")
                        Dim error_ref As String = ""
                        'Si Tiene Cabecera
                        If Convert.ToInt32(txtCabecera.Text) > 0 Then
                            If contador > Convert.ToInt32(txtCabecera.Text) Then
                                Try
                                    dtTable.Rows.Add(RetunrRowMatriz(dtTable, CadenaList, contador, error_, error_ref))

                                    If error_ref.Length > 0 Then
                                        error_ = error_ref
                                        Dim campo As Integer
                                        Dim observacion As String
                                        If (dtTable.Rows.Count > 0) Then
                                            For Each row_ As DataRow In dtTable.Rows
                                                campo = Convert.ToInt32(row_("NLINE").ToString())
                                                observacion = row_("SOBSERVATION").ToString()
                                                If observacion.Length > 0 Then
                                                    If campo = contador Then
                                                        dtTable.Rows.Remove(row_)
                                                        GoTo error_salto
                                                    End If
                                                End If

                                            Next
error_salto:
                                            dtTableError.Rows.Add(ReturnRowError(dtTableError, CadenaList, contador, error_))
                                        End If
                                    End If

                                Catch ex As Exception
                                    If (ex.Message.ToString().Length > 0) Then
                                        error_ = ex.Message.ToString()
                                    Else
                                        If (error_ref.Length > 0) Then

                                            error_ = error_ref
                                            Dim campo As Integer
                                            If (dtTable.Rows.Count > 0) Then
                                                For Each row_ As DataRow In dtTable.Rows
                                                    campo = Convert.ToInt32(row_("NLINE").ToString())
                                                    If campo = contador Then
                                                        dtTable.Rows.Remove(row_)
                                                    End If
                                                Next


                                            End If

                                        Else
                                        End If
                                    End If
                                    dtTableError.Rows.Add(ReturnRowError(dtTableError, CadenaList, contador, error_))
                                    status = False
                                End Try
                            End If
                        Else
                            'Si no tiene Cabecera

                            Try
                                dtTable.Rows.Add(RetunrRowMatriz(dtTable, CadenaList, contador, error_, error_ref))

                                If error_ref.Length > 0 Then
                                    error_ = error_ref
                                    Dim campo As Integer
                                    Dim observacion As String
                                    If (dtTable.Rows.Count > 0) Then
                                        For Each row_ As DataRow In dtTable.Rows
                                            campo = Convert.ToInt32(row_("NLINE").ToString())
                                            observacion = row_("SOBSERVATION").ToString()
                                            If observacion.Length > 0 Then
                                                If campo = contador Then
                                                    dtTable.Rows.Remove(row_)
                                                    GoTo error_salto_1
                                                End If
                                            End If
                                        Next
error_salto_1:
                                        dtTableError.Rows.Add(ReturnRowError(dtTableError, CadenaList, contador, error_))
                                    End If
                                End If


                            Catch ex As Exception
                                If (ex.Message.ToString().Length > 0) Then
                                    error_ = ex.Message.ToString()
                                Else
                                    If (error_ref.Length > 0) Then

                                        error_ = error_ref
                                        Dim campo As Integer
                                        If (dtTable.Rows.Count > 0) Then
                                            For Each row_ As DataRow In dtTable.Rows
                                                campo = Convert.ToInt32(row_("Fila").ToString())
                                                If campo = contador Then
                                                    dtTable.Rows.Remove(row_)
                                                End If
                                            Next


                                        End If

                                    Else
                                    End If
                                End If
                                dtTableError.Rows.Add(ReturnRowError(dtTableError, CadenaList, contador, error_))
                                status = False
                            End Try
                        End If

                        line = fielRead.ReadLine
                        contador = contador + 1
                    Loop
                End Using
            End Using



            If dtTable.Rows.Count = 0 Then
                status = False
                If (dtTableError.Rows.Count > 0) Then
                    Session("ErrorCase") = 2
                    Dim bool As Boolean = Metodos.InsertarErrores_Trama(Metodos.DB.ORACLE, dtTableError, hdIDArchivoProcesador.Value)
                    btnError.Enabled = True
                    'For Each row As DataRow In dtTableError.Rows
                    'dt_TramaError.Rows.Add(DatatableFin(dt_TramaError, row, "", 1))
                    'Next
                Else
                    btnError.Enabled = False
                End If
            Else


                For Each row As DataRow In dtTable.Rows
                    Dim sExistsPoliza As Integer = 0
                    Dim nPolicy As Integer = Convert.ToInt32(row("NPOLICY"))
                    sExistsPoliza = Metodos.ExistePolizaMatriz(Metodos.DB.ORACLE, nPolicy, ddlProducto.SelectedValue)
                    Dim errorExist As String
                    If sExistsPoliza = 0 Then
                        errorExist = ""
                        dtTableProcesar.Rows.Add(RetunrRowsV(dtTableProcesar, row, errorExist, 1))
                    Else
                        errorExist = "La póliza " & nPolicy & " ya se encuentra registrada en el sistema."
                        dtTableError.Rows.Add(RetunrRowsV(dtTableError, row, errorExist, 0))
                    End If

                Next

                If (dtTableProcesar.Rows.Count > 0) Then
                    status = True
                    Dim bool_ As Boolean = Metodos.InsertarTrama_Emision(Metodos.DB.ORACLE, dtTableProcesar, hdIDArchivoProcesador.Value)
                End If
                If (dtTableError.Rows.Count > 0) Then
                    Session("ErrorCase") = 2
                    Dim bool As Boolean = Metodos.InsertarErrores_Trama(Metodos.DB.ORACLE, dtTableError, hdIDArchivoProcesador.Value)
                    btnError.Enabled = True
                Else
                    btnError.Enabled = False
                End If

            End If
            Dim contador_respuesta As Integer
            Try
                contador_respuesta = Metodos.ValidacionTramaPolizas(Metodos.DB.ORACLE, hdIDArchivoProcesador.Value)
            Catch ex As Exception
                contador_respuesta = 0
                status = False
                Session("ErrorCase") = 2
                btnError.Enabled = True
            End Try

            If contador_respuesta = 0 Then
                Session("ErrorCase") = 2
                status = False
                btnError.Enabled = True
            End If


        Catch ex As Exception
            status = False
        End Try

        Return status
    End Function


#Region "Utilitarios"

    Private Function EvalColumn(ByVal InDicador As Integer, ByVal Campo As String) As Integer

        Dim accion As Integer = 0

        Dim dColumna As New DataTable
        dColumna = dtformato.Tables("Table1")
        Select Case dColumna.Rows(InDicador)("CAMPO_REQUERIDO")
            Case "N"
                If Campo.Length = 0 Then
                    accion = 0
                Else
                    If Campo.Length <= Convert.ToInt32(dColumna.Rows(InDicador)("LONGITUD")) Then
                        accion = 0
                    Else
                        accion = 2
                    End If
                End If
            Case "S"

                If Campo.Length = 0 Then
                    accion = 0
                Else
                    If Campo.Length <= Convert.ToInt32(dColumna.Rows(InDicador)("LONGITUD")) Then
                        accion = 0
                    Else
                        accion = 2
                    End If
                End If
        End Select
        Return accion
    End Function

    Private Sub CreaDirectorioTrabajo(ByVal dFecha As String)
        Dim di As DirectoryInfo
        Dim rUTA As String = ""

        Try

            rUTA = HF_RUTA_MASIVA.Value & "MATRIZ MASIVO" & "\" & ddlCanal.Text & "\" & dFecha & "\" & LTrim(RTrim(Session("Nombre")))
            If Directory.Exists(rUTA) = False Then
                di = Directory.CreateDirectory(rUTA)
            End If
        Catch ex As Exception
            AlertaScripts("No se pudo crear la carpeta de trabajo para la carga.")

        End Try
    End Sub

    Private Function Getfecha(ByVal dateString As String) As String
        Dim FechaResult As String = ""
        Try

            Dim month = dateString.Substring(4, 2)
            Dim year = dateString.Substring(0, 4)
            Dim day = dateString.Substring(6, 2)

            FechaResult = day + "/" + month + "/" + year

        Catch ex As Exception
            FechaResult = ""
        End Try
        Return FechaResult
    End Function

    Private Function GetErrorCase() As DataTable

        Dim dt As New DataTable
        Dim drw As DataRow
        Dim dtResult As New DataSet
        Try

            dtResult = Metodos.ListarError_Polizas(Metodos.DB.ORACLE, Convert.ToInt32(hdIDArchivoProcesador.Value))
            dt.Columns.Add("ID", Type.GetType("System.Int64"))
            dt.Columns.Add("Descripcion", Type.GetType("System.String"))

            Select Case Session("ErrorCase")
                Case 1
                    Dim contador As Integer = 0
                    For Each row_ As DataRow In dtResult.Tables(0).Rows
                        drw = dt.NewRow
                        drw(0) = contador
                        drw(1) = row_("NOBSERVATION").ToString()
                        contador = contador + 1
                        dt.Rows.Add(drw)
                    Next

                    herrores.Value = Convert.ToString(contador)


                Case 2
                    Dim contador As Integer = 0
                    For Each row_ As DataRow In dtResult.Tables(1).Rows
                        drw = dt.NewRow
                        drw(0) = Convert.ToInt32(row_("NLINE").ToString())
                        drw(1) = row_("SOBSERVATION").ToString()
                        contador = contador + 1
                        dt.Rows.Add(drw)
                    Next
                    herrores.Value = Convert.ToString(contador)
                Case 3
                    Dim contador As Integer = 0
                    For Each row_ As DataRow In dtResult.Tables(2).Rows
                        drw = dt.NewRow
                        drw(0) = Convert.ToInt32(row_("NLINE").ToString())
                        drw(1) = row_("SOBSERVATION").ToString()
                        contador = contador + 1
                        dt.Rows.Add(drw)
                    Next
                    herrores.Value = Convert.ToString(contador)
                Case 4
                    Dim contador As Integer = 0
                    For Each row_ As DataRow In dtResult.Tables(3).Rows
                        drw = dt.NewRow
                        drw(0) = Convert.ToInt32(row_("NLINE").ToString())
                        drw(1) = row_("SOBSERVATION").ToString()
                        contador = contador + 1
                        dt.Rows.Add(drw)
                    Next
                    hejecutados.Value = Convert.ToString(contador)
            End Select


        Catch ex As Exception

        End Try

        Return dt

    End Function

    Private Function GetResumen() As DataTable

        Dim dt As New DataTable
        Dim drw As DataRow
        Dim dtResult As New DataSet
        Try


            dtResult = Metodos.ListarEmision_Polizas(Metodos.DB.ORACLE, Convert.ToInt32(hdIDArchivoProcesador.Value))
            dt.Columns.Add("NLINE", Type.GetType("System.String"))
            dt.Columns.Add("NPOLICY", Type.GetType("System.String"))
            dt.Columns.Add("DSTARTDATE", Type.GetType("System.String"))
            dt.Columns.Add("DEXPIRDAT", Type.GetType("System.String"))
            dt.Columns.Add("SCLIENAME", Type.GetType("System.String"))
            dt.Columns.Add("SCLIENAME_INT", Type.GetType("System.String"))
            dt.Columns.Add("NPERCENT", Type.GetType("System.String"))
            dt.Columns.Add("SSPECIALITY", Type.GetType("System.String"))

            Dim contador As Integer = 0
            For Each row_ As DataRow In dtResult.Tables(0).Rows
                drw = dt.NewRow
                drw(0) = row_("NLINE").ToString()
                drw(1) = row_("NPOLICY").ToString()
                drw(2) = Convert.ToDateTime(row_("DSTARTDATE")).ToString("dd/MM/yyyyy")
                drw(3) = Convert.ToDateTime(row_("DEXPIRDAT")).ToString("dd/MM/yyyyy")
                drw(4) = row_("SCLIENAME").ToString()
                drw(5) = row_("SCLIENAME_INT").ToString()
                drw(6) = row_("NPERCENT").ToString()
                drw(7) = row_("SSPECIALITY").ToString()
                contador = contador + 1
                dt.Rows.Add(drw)
            Next
            hejecutados.Value = Convert.ToString(contador)



        Catch ex As Exception

        End Try

        Return dt

    End Function

    Private Function Getfecha() As String
        Dim f_return As String
        Try

            Dim ANIO, MES, DIA As String

            ANIO = Today.Year.ToString()
            MES = Today.Month.ToString()
            DIA = Today.Day.ToString()

            If Len(MES) = 1 Then
                MES = "0" + MES
            End If

            If Len(DIA) = 1 Then
                DIA = "0" + DIA
            End If

            f_return = DIA + "/" + MES + "/" + ANIO


        Catch ex As Exception

        End Try
        Return f_return
    End Function

    Private Sub CargarGrilla()
        Dim dts As DataSet
        Try

            dts = Metodos.ListarModulosxProducto(Metodos.DB.ORACLE, Getfecha(), ddlProducto.SelectedValue)
            gbModulo.DataSource = dts.Tables(0)
            gbModulo.DataBind()
            gbModulo.Width = 720

        Catch ex As Exception

        End Try
    End Sub



#End Region

#Region "Retornar Rows Caso Trama"

    Private Function RetunrRowsV(ByVal Rows As DataTable, ByVal rowaTable As DataRow, ByVal mensaje As String, ByVal Condicion As Integer) As DataRow

        Dim Row As DataRow
        Row = Rows.NewRow

        'POLIZA
        Row(0) = rowaTable("NBRANCH")
        Row(1) = rowaTable("NPRODUCT")
        Row(2) = rowaTable("NPOLICY")
        Row(3) = rowaTable("NOFFICE")
        Row(4) = rowaTable("NSELLCHANNEL")
        Row(5) = rowaTable("SBUSSITYP")
        Row(6) = rowaTable("SCOLINVOT")
        Row(7) = rowaTable("SCOLREINT")
        Row(8) = rowaTable("SCOLTIMRE")
        Row(9) = rowaTable("DSTARTDATE")
        Row(10) = rowaTable("DEXPIRDAT")
        Row(11) = rowaTable("NPAYFREQ")
        Row(12) = rowaTable("SCLIENT_CONT")
        Row(13) = rowaTable("NINTERMED")
        Row(14) = rowaTable("SINTER_ID")
        Row(15) = rowaTable("SUSR_SANITAS")
        Row(16) = rowaTable("NPERCENT")
        Row(17) = rowaTable("NSPECIALITY")
        'CONTRATANTE => CNT_
        Row(18) = rowaTable("NPERSON_TYP_CONT")
        Row(19) = rowaTable("NIDDOC_TYPE_CONT")
        'CONTRATANTE NATURAL 
        Row(20) = rowaTable("NCOUNTRY_CONT")
        Row(21) = rowaTable("NMUNICIPALITY_CONT")
        Row(22) = rowaTable("SIDDOC_CONT")
        Row(23) = rowaTable("SLEGALNAME_CONT")
        Row(24) = rowaTable("SFIRSTNAME_CONT")
        Row(25) = rowaTable("SLASTNAME1_CONT")
        Row(26) = rowaTable("SLASTNAME2_CONT")
        'CONTRATANTE JURIDICO
        Row(27) = rowaTable("DBIRTHDAT_CONT")
        Row(28) = rowaTable("NCIVILSTA_CONT")
        'CONTRATANTE DATOS GENERALES 
        Row(29) = rowaTable("SSEXCLIEN_CONT")
        Row(30) = rowaTable("SE_MAIL_CONT")
        Row(31) = rowaTable("SRECTYPE_CONT")
        Row(32) = rowaTable("SSTREET_CONT")
        Row(33) = rowaTable("SBUILD_CONT")
        Row(34) = rowaTable("NFLOOR_CONT")
        Row(35) = rowaTable("SDEPARTAMENT_CONT")
        Row(36) = rowaTable("SPOPULATION_CONT")
        Row(37) = rowaTable("SREFERENCE_CONT")
        Row(38) = rowaTable("NZIPCODE_CONT")
        Row(39) = rowaTable("SPHONE_CONT")
        Row(40) = rowaTable("NPERSON_TYP_INT")
        Row(41) = rowaTable("NIDDOC_TYPE_INT")
        Row(42) = rowaTable("NCOUNTRY_INT")
        'INTERMEDIARIO =>INT_
        Row(43) = rowaTable("NMUNICIPALITY_INT")
        Row(44) = rowaTable("SIDDOC_INT")
        'INTERMEDIARIO NATURAL
        Row(45) = rowaTable("SLEGALNAME_INT")
        Row(46) = rowaTable("SFIRSTNAME_INT")
        Row(47) = rowaTable("SLASTNAME1_INT")
        Row(48) = rowaTable("SLASTNAME2_INT")
        Row(49) = rowaTable("DBIRTHDAT_INT")
        Row(50) = rowaTable("NCIVILSTA_INT")
        Row(51) = rowaTable("SSEXCLIEN_INT")
        'INTERMEDIARIO JURIDICO
        Row(52) = rowaTable("NVALSUSALUD")
        Row(53) = rowaTable("SE_MAIL_INT")
        'INTERMEDIARIO DATOS GENERALES
        Row(54) = rowaTable("SRECTYPE_INT")
        Row(55) = rowaTable("SSTREET_INT")
        Row(56) = rowaTable("SBUILD_INT")
        Row(57) = rowaTable("NFLOOR_INT")
        Row(58) = rowaTable("SDEPARTAMENT_INT")
        Row(59) = rowaTable("SPOPULATION_INT")
        Row(60) = rowaTable("SREFERENCE_INT")
        Row(61) = rowaTable("NZIPCODE_INT")
        Row(62) = rowaTable("SPHONE_INT")
        Row(63) = rowaTable("NCURRENCY")
        Row(64) = rowaTable("NINTERTYP")
        Select Case Condicion
            Case 0
                Row(65) = mensaje
            Case 1
                Row(65) = rowaTable("SOBSERVATION")
        End Select
        Row(66) = rowaTable("NLINE")
        Return Row

    End Function

    Private Function ReturnRowError(ByVal Rows As DataTable, ByVal Matriz() As String, ByVal fila As Integer, ByVal mensaje As String) As DataRow
        Dim Row As DataRow
        Row = Rows.NewRow
        Row(0) = Matriz(0)
        Row(1) = Matriz(1)
        Row(2) = Matriz(2)
        Row(3) = Matriz(3)
        Row(4) = Matriz(4)
        Row(5) = Matriz(5)
        Row(6) = Matriz(6)
        Row(7) = Matriz(7)
        Row(8) = Matriz(8)
        Row(9) = Matriz(9)
        Row(10) = Matriz(10)
        Row(11) = Matriz(11)
        Row(12) = Matriz(12)
        Row(13) = Matriz(13)
        Row(14) = Matriz(14)
        Row(15) = Matriz(15)
        Row(16) = Matriz(16)
        Row(17) = Matriz(17)
        Row(18) = Matriz(18)
        Row(19) = Matriz(19)
        Row(20) = Matriz(20)
        Row(21) = Matriz(21)
        Row(22) = Matriz(22)
        Row(23) = Matriz(23)
        Row(24) = Matriz(24)
        Row(25) = Matriz(25)
        Row(26) = Matriz(26)
        Row(27) = Matriz(27)
        Row(28) = Matriz(28)
        Row(29) = Matriz(29)
        Row(30) = Matriz(30)
        Row(31) = Matriz(31)
        Row(32) = Matriz(32)
        Row(33) = Matriz(33)
        Row(34) = Matriz(34)
        Row(35) = Matriz(35)
        Row(36) = Matriz(36)
        Row(37) = Matriz(37)
        Row(38) = Matriz(38)
        Row(39) = Matriz(39)
        Row(40) = Matriz(40)
        Row(41) = Matriz(41)
        Row(42) = Matriz(42)
        Row(43) = Matriz(43)
        Row(44) = Matriz(44)
        Row(45) = Matriz(45)
        Row(46) = Matriz(46)
        Row(47) = Matriz(47)
        Row(48) = Matriz(48)
        Row(49) = Matriz(49)
        Row(50) = Matriz(50)
        Row(51) = Matriz(51)
        Row(52) = Matriz(52)
        Row(53) = Matriz(53)
        Row(54) = Matriz(54)
        Row(55) = Matriz(55)
        Row(56) = Matriz(56)
        Row(57) = Matriz(57)
        Row(58) = Matriz(58)
        Row(59) = Matriz(59)
        Row(60) = Matriz(60)
        Row(61) = Matriz(61)
        Row(62) = Matriz(62)
        Row(63) = Matriz(63)
        Row(64) = Matriz(64)
        Row(65) = mensaje
        Row(66) = fila
        Return Row
    End Function

    Private Function RetunrRowMatriz(ByVal Rows As DataTable, ByVal Matriz() As String, ByVal fila As Integer,
                                     ByVal mensaje As String, ByRef ExistsMessage As String) As DataRow
        Dim Row As DataRow
        Row = Rows.NewRow

        ExistsMessage = ""
        'RAMO
        Select Case EvalColumn(0, Matriz(0))
            Case 0
                Try
                    Row(0) = Matriz(0)
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + " El campo NBRANCH no tiene formato válido" + "<br>"
                End Try
            Case 2
                ExistsMessage = ExistsMessage + " El campo NBRANCH excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NBRANCH está vacío" + "<br>"
        End Select
        'PRODUCTO
        Select Case EvalColumn(1, Matriz(1))
            Case 0
                Try
                    Row(1) = Matriz(1)
                    If Row(1) <> ddlProducto.SelectedValue Then
                        ExistsMessage = ExistsMessage + "El campo NPRODUCT tiene dato incorrecto" + "<br>"
                    End If
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NPRODUCT no tiene formato válido" + "<br>"
                End Try

            Case 2
                ExistsMessage = ExistsMessage + "El campo NPRODUCT excede el tamaño de carga " + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NPRODUCT está vacío" + "<br>"
        End Select
        'NRO. DE  POLIZA
        Select Case EvalColumn(2, Matriz(2))
            Case 0
                If String.IsNullOrEmpty(Matriz(2)) Then
                    Try
                        Row(2) = 0
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NPOLICY no tiene formato válido " + "<br>"
                    End Try
                Else
                    Try
                        Row(2) = Matriz(2)
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NPOLICY no tiene formato válido " + "<br>"
                    End Try

                End If
            Case 2
                ExistsMessage = ExistsMessage + "El campo NPOLICY excede el tamaño de carga " + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NPOLICY está vacío " + "<br>"
        End Select
        'OFICINA
        Select Case EvalColumn(3, Matriz(3))
            Case 0
                Try
                    Row(3) = Matriz(3)
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NOFFICE no tiene formato válido" + "<br>"
                End Try

            Case 2
                ExistsMessage = ExistsMessage + "El campo NOFFICE excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NOFFICE está vacío" + "<br>"
        End Select
        'CANAL DE VENTA
        Select Case EvalColumn(4, Matriz(4))
            Case 0
                Try
                    Row(4) = Matriz(4)
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NSELLCHANNEL no tiene formato válido " + "<br>"
                End Try
            Case 2
                ExistsMessage = ExistsMessage + "El campo NSELLCHANNEL excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NSELLCHANNEL está vacío" + "<br>"
        End Select
        'TIPO DE NEGOCIO
        Select Case EvalColumn(5, Matriz(5))
            Case 0
                Row(5) = Matriz(5)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SBUSSITYP excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SBUSSITYP está vacío" + "<br>"
        End Select
        'TIPO DE FACTURACIÓN
        Select Case EvalColumn(6, Matriz(6))
            Case 0
                Row(6) = Matriz(6)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SCOLINVOT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SCOLINVOT está vacío" + "<br>"
        End Select
        'TIPO DE RESEGURO
        Select Case EvalColumn(7, Matriz(7))
            Case 0
                Row(7) = Matriz(7)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SCOLREINT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SCOLREINT está vacío" + "<br>"
        End Select
        'TIPO DE RENOVACIÓN
        Select Case EvalColumn(8, Matriz(8))
            Case 0
                Row(8) = Matriz(8)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SCOLTIMRE excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SCOLTIMRE está vacío" + "<br>"
        End Select
        'INICIO VIGENCIA
        Select Case EvalColumn(9, Matriz(9))
            Case 0
                Row(9) = Getfecha(Matriz(9))
            Case 2
                ExistsMessage = ExistsMessage + "El campo DSTARTDATE excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + " El campo DSTARTDATE está vacío" + "<br>"
        End Select
        'FIN VIGENCIA
        Select Case EvalColumn(10, Matriz(10))
            Case 0
                Row(10) = Getfecha(Matriz(10))
            Case 2
                ExistsMessage = ExistsMessage + "El campo DEXPIRDAT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo DEXPIRDAT está vacío" + "<br>"
        End Select
        'FRECUENCIA 
        Select Case EvalColumn(11, Matriz(11))
            Case 0
                Try
                    Row(11) = Matriz(11)
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NPAYFREQ no tiene formato válido" + "<br>"
                End Try
            Case 2
                ExistsMessage = ExistsMessage + "El campo NPAYFREQ excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NPAYFREQ está vacío" + "<br>"
        End Select
        'COD INTERMEDIARIO SBS
        Select Case EvalColumn(14, Matriz(14))
            Case 0
                Row(14) = Matriz(14)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SINTER_ID excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SINTER_ID está vacío" + "<br>"
        End Select
        'USUARIO SANITAS
        Select Case EvalColumn(15, Matriz(15))
            Case 0
                Row(15) = Matriz(15)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SUSR_SANITAS excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SUSR_SANITAS está vacío" + "<br>"
        End Select

        'PORCENTAJE DE COMISIÓN
        If Matriz(64) = "3" Then
            Select Case EvalColumn(16, Matriz(16))
                Case 0
                    Try
                        Row(16) = Matriz(16)
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NPERCENT no tiene formato válido" + "<br>"
                    End Try
                Case 2
                    ExistsMessage = ExistsMessage + "El campo NPERCENT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NPERCENT está vacío" + "<br>"
            End Select
        Else
            Row(16) = 0
        End If
        'COD. CIIU
        Select Case EvalColumn(17, Matriz(17))
            Case 0
                If String.IsNullOrEmpty(Matriz(17)) Then
                    Row(17) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(17)) = 0 Then
                    Row(17) = Convert.ToInt64(0)
                Else
                    Try
                        Row(17) = Convert.ToInt64(Matriz(17))
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NSPECIALITY no tiene formato válido" + "<br>"
                    End Try
                End If
            Case 2
                ExistsMessage = ExistsMessage + "El campo NSPECIALITY excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NSPECIALITY está vacío" + "<br>"
        End Select

        'CONTRATANTE DATOS PRINCIPALES  / OTROS DATOS
        If Matriz(12).Length > 0 Then

            If String.IsNullOrEmpty(Matriz(12)) Then
                Row(12) = Convert.ToInt64(0)
            ElseIf Convert.ToInt32(Matriz(12)) = 0 Then
                Row(12) = Convert.ToInt64(0)
            Else
                Try
                    Row(12) = Matriz(12)
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo CLIENT_CONT no tiene formato válido" + "<br>"
                End Try
            End If

        End If

        'TIPO DE PERSONA CONTRATANTE
        Select Case EvalColumn(18, Matriz(18))
            Case 0
                If String.IsNullOrEmpty(Matriz(18)) Then
                    Row(18) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(18)) = 0 Then
                    Row(18) = Convert.ToInt64(0)
                Else
                    Try
                        Row(18) = Matriz(18)
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_CONT no tiene formato válido" + "<br>"
                    End Try
                End If

            Case 2
                ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_CONT está vacío" + "<br>"
        End Select
        'TIPO DE DOCUMENTO CONTRATANTE
        Select Case EvalColumn(19, Matriz(19))
            Case 0

                If String.IsNullOrEmpty(Matriz(20)) Then
                    Row(19) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(19)) = 0 Then
                    Row(19) = Convert.ToInt64(0)
                Else
                    Try
                        Row(19) = Matriz(19)
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NIDDOC_TYPE_CONT no tiene formato válido" + "<br>"
                    End Try
                End If

            Case 2
                ExistsMessage = ExistsMessage + "El campo NIDDOC_TYPE_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NIDDOC_TYPE_CONT está vacío" + "<br>"
        End Select

        'NACIONALIDAD CONTRATANTE
        Select Case EvalColumn(20, Matriz(20))
            Case 0
                If String.IsNullOrEmpty(Matriz(20)) Then
                    Row(20) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(21)) = 0 Then
                    Row(20) = Convert.ToInt64(0)
                Else
                    Try
                        Row(20) = Convert.ToInt64(Matriz(20))
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NCOUNTRY_CONT no tiene formato válido" + "<br>"
                    End Try
                End If
            Case 2
                ExistsMessage = ExistsMessage + "El campo NCOUNTRY_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NCOUNTRY_CONT está vacío" + "<br>"
        End Select
        'UBIGEO CONTRATANTE
        Select Case EvalColumn(21, Matriz(21))
            Case 0
                If String.IsNullOrEmpty(Matriz(21)) Then
                    Row(21) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(21)) = 0 Then
                    Row(21) = Convert.ToInt64(0)
                Else
                    Try
                        Row(21) = Convert.ToInt64(Matriz(21))
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_CONT no tiene formato válido" + "<br>"
                    End Try
                End If
            Case 2
                ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_CONT está vacío" + "<br>"
        End Select


        'CODE CONTRATANTE
        Select Case EvalColumn(22, Matriz(22))
            Case 0
                Row(22) = Matriz(22)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SIDDOC_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SIDDOC_CONT está vacío" + "<br>"
        End Select

        'TIPO DOCUMENTO
        Select Case Matriz(18)
            Case "1"
                'RAZÓN SOCIAL DEL CONTRATANTE (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(23, Matriz(23))
                    Case 0
                        If Matriz(23).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo SLEGALNAME_CONT debe ser vacío" + "<br>"
                        End If
                End Select
                'NOMBRE CONTRATANTE
                Select Case EvalColumn(24, Matriz(24))
                    Case 0
                        Row(24) = Matriz(24)
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo SFIRSTNAME_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo SFIRSTNAME_CONT está vacío" + "<br>"
                End Select
                'APELLIDO PATERNO  DEL CONTRATANTE
                Select Case EvalColumn(25, Matriz(25))
                    Case 0
                        Row(25) = Matriz(25)
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo SLASTNAME1_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo SLASTNAME1_CONT está vacío" + "<br>"
                End Select
                'APELLIDO MATERNO  DEL CONTRATANTE
                Select Case EvalColumn(26, Matriz(26))
                    Case 0
                        Row(26) = Matriz(26)
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo SLASTNAME2_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo SLASTNAME2_CONT está vacío" + "<br>"
                End Select
                'FECHA DE NACIMIENTO CONTRATANTE
                Select Case EvalColumn(27, Matriz(27))
                    Case 0
                        Row(27) = Getfecha(Matriz(27))
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo DBIRTHDAT_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo DBIRTHDAT_CONT está vacío" + "<br>"
                End Select
                'ESTADO CIVIL CONTRATANTE
                Select Case EvalColumn(28, Matriz(28))
                    Case 0
                        If String.IsNullOrEmpty(Matriz(38)) Then
                            Row(28) = Convert.ToInt64(0)
                        ElseIf Convert.ToInt32(Matriz(28)) = 0 Then
                            Row(28) = Convert.ToInt64(0)
                        Else
                            Try
                                Row(28) = Convert.ToInt64(Matriz(28))
                            Catch ex As Exception
                                ExistsMessage = ExistsMessage + "El campo NCIVILSTA_CONT no tiene formato válido" + "<br>"
                            End Try
                        End If
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo NCIVILSTA_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo NCIVILSTA_CONT está vacío" + "<br>"
                End Select
                'SEXO DEL CONTRATANTE
                Select Case EvalColumn(29, Matriz(29))
                    Case 0
                        Row(29) = Matriz(29)
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo SSEXCLIEN_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo SSEXCLIEN_CONT está vacío" + "<br>"
                End Select
            Case "2"
                'RAZÓN SOCIAL DEL CONTRATANTE
                Select Case EvalColumn(23, Matriz(23))
                    Case 0
                        Row(23) = Matriz(23)
                    Case 2
                        ExistsMessage = ExistsMessage + "El campo SLEGALNAME_CONT excede el tamaño de carga" + "<br>"
                    Case 1
                        ExistsMessage = ExistsMessage + "El campo SLEGALNAME_CONT está vacío" + "<br>"
                End Select

                'NOMBRE CONTRATANTE  (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(24, Matriz(24))
                    Case 0
                        If Matriz(24).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo SFIRSTNAME_CONT debe ser vacío" + "<br>"
                        End If
                End Select
                'APELLIDO PATERNO  DEL CONTRATANTE (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(25, Matriz(25))
                    Case 0
                        If Matriz(25).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo SLASTNAME1_CONT debe ser vacío" + "<br>"
                        End If
                End Select
                'APELLIDO MATERNO  DEL CONTRATANTE (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(26, Matriz(26))
                    Case 0
                        If Matriz(26).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo SLASTNAME2_CONT debe ser vacío" + "<br>"
                        End If
                End Select
                'FECHA DE NACIMIENTO CONTRATANTE (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(27, Matriz(27))
                    Case 0
                        If Matriz(27).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo DBIRTHDAT_CONT debe ser vacío" + "<br>"
                        End If
                End Select
                'ESTADO CIVIL CONTRATANTE (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(28, Matriz(28))
                    Case 0
                        If Matriz(28).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo NCIVILSTA_CONT debe ser vacío" + "<br>"
                        End If
                End Select
                'SEXO DEL CONTRATANTE (NO SE DEBE ENVIAR DATOS)
                Select Case EvalColumn(29, Matriz(29))
                    Case 0
                        If Matriz(29).Length > 0 Then
                            ExistsMessage = ExistsMessage + "El campo SSEXCLIEN_CONT debe ser vacío" + "<br>"
                        End If
                End Select

        End Select

        'EMAIL DEL CONTRATANTE 
        Select Case EvalColumn(30, Matriz(30))
            Case 0
                Row(30) = Matriz(30)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SE_MAIL_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SE_MAIL_CONT está vacío" + "<br>"
        End Select

        'TIPO DE VIA CONTRANTATE 
        Select Case EvalColumn(31, Matriz(31))
            Case 0
                Row(31) = Matriz(31)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SRECTYPE_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SRECTYPE_CONT está vacío" + "<br>"
        End Select

        'DIRECCION CONTRATANTE
        Select Case EvalColumn(32, Matriz(32))
            Case 0
                Row(32) = Matriz(32)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SSTREET_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SSTREET_CONT está vacío" + "<br>"
        End Select

        'NUMERO VIA CONTRATANTE  
        Select Case EvalColumn(33, Matriz(33))
            Case 0
                Row(33) = Matriz(33)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SBUILD_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SBUILD_CONT está vacio" + "<br>"
        End Select

        'MANZANA CONTRATANTE
        Select Case EvalColumn(34, Matriz(34))
            Case 0
                If String.IsNullOrEmpty(Matriz(34)) Then
                    Row(34) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(34)) = 0 Then
                    Row(34) = Convert.ToInt64(0)
                Else
                    Try
                        Row(34) = Convert.ToInt64(Matriz(34))
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NFLOOR_CONT no tiene formato válido" + "<br>"
                    End Try

                End If
            Case 2
                ExistsMessage = ExistsMessage + "El campo NFLOOR_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NFLOOR_CONT está vacío" + "<br>"
        End Select

        'LOTE CONTRATANTE
        Select Case EvalColumn(35, Matriz(35))
            Case 0
                Row(35) = Matriz(35)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SDEPARTAMENT_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SDEPARTAMENT_CONT está vacío" + "<br>"
        End Select

        'PISO CONTRATANTE
        Select Case EvalColumn(36, Matriz(36))
            Case 0
                Row(36) = Matriz(36)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SPOPULATION_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SPOPULATION_CONT está vacío" + "<br>"
        End Select

        'NRO DEPARTAMENTO CONTRATANTE
        Select Case EvalColumn(37, Matriz(37))
            Case 0
                Row(37) = Matriz(37)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SREFERENCE_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SREFERENCE_CONT está vacío" + "<br>"
        End Select

        'INTERIOR CONTRATANTE
        Select Case EvalColumn(38, Matriz(38))
            Case 0
                If String.IsNullOrEmpty(Matriz(38)) Then
                    Row(38) = Convert.ToInt64(0)
                ElseIf Convert.ToInt32(Matriz(38)) = 0 Then
                    Row(38) = Convert.ToInt64(0)
                Else
                    Try
                        Row(38) = Convert.ToInt64(Matriz(38))
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NZIPCODE_CONT no tiene formato válido" + "<br>"
                    End Try

                End If

            Case 2
                ExistsMessage = ExistsMessage + "El campo NZIPCODE_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo NZIPCODE_CONT está vacío" + "<br>"
        End Select

        'URBANIZACIÓN CONTRATANTE
        Select Case EvalColumn(39, Matriz(39))
            Case 0
                Row(39) = Matriz(39)
            Case 2
                ExistsMessage = ExistsMessage + "El campo SPHONE_CONT excede el tamaño de carga" + "<br>"
            Case 1
                ExistsMessage = ExistsMessage + "El campo SPHONE_CONT está vacío" + "<br>"
        End Select


        'INTERMEDIARIO DATOS PRINCIPALES  / OTROS DATOS
        If Matriz(64) = "3" Then

            'COD INTERMEDIARIO
            If Matriz(13).Length > 0 Then
                Try
                    Row(13) = Convert.ToInt64(LTrim(RTrim(Matriz(13))))
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NINTERMED no tiene formato válido" + "<br>"
                End Try
            End If

            'TIPO DE PERSONA INTERMEDIARIO
            Select Case EvalColumn(40, Matriz(40))
                Case 0
                    Try
                        If Len(Matriz(40)) > 0 Then
                            Row(40) = Matriz(40)
                        Else
                            Row(40) = 0
                        End If
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_INT no tiene formato válido" + "<br>"
                    End Try

                Case 2
                    ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_INT está vacío" + "<br>"
            End Select

            'TIPO DE DOCUMENTO INTERMEDIARIO
            Select Case EvalColumn(41, Matriz(41))
                Case 0
                    Try
                        If Len(Matriz(41)) > 0 Then
                            Row(41) = Matriz(41)
                        Else
                            Row(41) = 0
                        End If
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NIDDOC_TYPE_INT no tiene formato válido" + "<br>"
                    End Try

                Case 2
                    ExistsMessage = ExistsMessage + "El campo NIDDOC_TYPE_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NIDDOC_TYPE_INT está vacío" + "<br>"
            End Select

            'NACIONALIDAD INTERMEDIARIO
            Select Case EvalColumn(42, Matriz(42))
                Case 0
                    Try
                        If Len(Matriz(42)) > 0 Then
                            Row(42) = Matriz(42)
                        Else
                            Row(42) = 0
                        End If


                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NCOUNTRY_INT no tiene formato válido" + "<br>"
                    End Try

                Case 2
                    ExistsMessage = ExistsMessage + "El campo NCOUNTRY_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NCOUNTRY_INT está vacío" + "<br>"
            End Select

            'UBIGEO INTERMEDIARIO   
            Select Case EvalColumn(43, Matriz(43))
                Case 0
                    Try

                        If Len(Matriz(43)) > 0 Then
                            Row(43) = Matriz(43)
                        Else
                            Row(43) = 0
                        End If
                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_INT no tiene formato válido" + "<br>"
                    End Try

                Case 2
                    ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_INT está vacío" + "<br>"
            End Select

            'COD INTERMEDIARIO  
            Select Case EvalColumn(44, Matriz(44))
                Case 0
                    Row(44) = Matriz(44)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SIDDOC_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SIDDOC_INT está vacío" + "<br>"
            End Select

            Select Case Matriz(40)
                Case "1"
                    'RAZÓN SOCIAL DEL INTERMEDIARIO (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(45, Matriz(45))
                        Case 0
                            If Matriz(45).Length > 0 Then
                                ExistsMessage = ExistsMessage + "El campo SLEGALNAME_INT debe ser vacío" + "<br>"
                            End If
                    End Select
                    'NOMBRES DEL 'INTERMEDIARIO  
                    Select Case EvalColumn(46, Matriz(46))
                        Case 0
                            Row(46) = Matriz(46)
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo SFIRSTNAME_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo SFIRSTNAME_INT está vacío" + "<br>"
                    End Select
                    'APELLIDO PATERNO  DEL INTERMEDIARIO    
                    Select Case EvalColumn(47, Matriz(47))
                        Case 0
                            Row(47) = Matriz(47)
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo SLASTNAME1_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo SLASTNAME1_INT está vacío" + "<br>"
                    End Select
                    'APELLIDO MATERNO  DEL INTERMEDIARIO   
                    Select Case EvalColumn(48, Matriz(48))
                        Case 0
                            Row(48) = Matriz(48)
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo SLASTNAME2_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo SLASTNAME2_INT está vacío" + "<br>"
                    End Select
                    'FECHA DE NACIMIENTO INTERMEDIARIO
                    Select Case EvalColumn(49, Matriz(49))
                        Case 0
                            Row(49) = Getfecha(Matriz(49))
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo DBIRTHDAT_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo DBIRTHDAT_INT está vacío" + "<br>"
                    End Select
                    'ESTADO CIVIL INTERMEDIARIO  
                    Select Case EvalColumn(50, Matriz(50))
                        Case 0
                            If String.IsNullOrEmpty(Matriz(50)) Then
                                Row(50) = Convert.ToInt64(0)
                            ElseIf Convert.ToInt32(Matriz(50)) = 0 Then
                                Row(50) = Convert.ToInt64(0)
                            Else
                                Try
                                    Row(50) = Convert.ToInt64(Matriz(50))

                                Catch ex As Exception
                                    ExistsMessage = ExistsMessage + "El campo NCIVILSTA_INT no tiene formato válido" + "<br>"
                                End Try

                            End If
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo NCIVILSTA_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo NCIVILSTA_INT está vacío" + "<br>"
                    End Select
                    'SEXO DEL INTERMEDIARIO
                    Select Case EvalColumn(51, Matriz(51))
                        Case 0
                            Row(51) = Matriz(51)
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo SSEXCLIEN_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo SSEXCLIEN_INT está vacío" + "<br>"
                    End Select
                Case "2"
                    'RAZÓN SOCIAL DEL INTERMEDIARIO
                    Select Case EvalColumn(45, Matriz(45))
                        Case 0
                            Row(45) = Matriz(45)
                        Case 2
                            ExistsMessage = ExistsMessage + "El campo SLEGALNAME_INT excede el tamaño de carga" + "<br>"
                        Case 1
                            ExistsMessage = ExistsMessage + "El campo SLEGALNAME_INT está vacío" + "<br>"
                    End Select
                    'NOMBRES DEL 'INTERMEDIARIO  (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(46, Matriz(46))
                        Case 0
                            If Len(Matriz(46)) > 0 Then
                                ExistsMessage = ExistsMessage + "El campo SFIRSTNAME_INT debe ser vacío" + "<br>"
                            End If
                    End Select
                    'APELLIDO PATERNO  DEL INTERMEDIARIO (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(47, Matriz(47))
                        Case 0
                            If Matriz(47).Length > 0 Then
                                ExistsMessage = ExistsMessage + "El campo SLASTNAME1_INT debe ser vacío" + "<br>"
                            End If
                    End Select
                    'APELLIDO MATERNO  DEL INTERMEDIARIO (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(48, Matriz(48))
                        Case 0
                            If Matriz(48).Length > 0 Then
                                ExistsMessage = ExistsMessage + "El campo SLASTNAME2_INT debe ser vacío" + "<br>"
                            End If
                    End Select
                    'FECHA DE NACIMIENTO INTERMEDIARIO (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(49, Matriz(49))
                        Case 0
                            If Matriz(49).Length > 0 Then
                                ExistsMessage = ExistsMessage + "El campo DBIRTHDAT_INT debe ser vacío" + "<br>"
                            End If
                    End Select
                    'ESTADO CIVIL INTERMEDIARIO (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(50, Matriz(50))
                        Case 0
                            If Matriz(50).Length > 0 Then
                                ExistsMessage = ExistsMessage + "El campo NCIVILSTA_INT debe ser vacío" + "<br>"
                            End If
                    End Select
                    'SEXO DEL INTERMEDIARIO (NO SE DEBE ENVIAR DATOS)
                    Select Case EvalColumn(51, Matriz(51))
                        Case 0
                            If Matriz(51).Length > 0 Then
                                ExistsMessage = ExistsMessage + "El campo SSEXCLIEN_INT debe ser vacío" + "<br>"
                            End If
                    End Select
            End Select

            'CODIGO SALUD
            Select Case EvalColumn(52, Matriz(52))
                Case 0
                    If String.IsNullOrEmpty(Matriz(52)) Then
                        Row(52) = Convert.ToInt64(0)
                    ElseIf Convert.ToInt32(Matriz(52)) = 0 Then
                        Row(52) = Convert.ToInt64(0)
                    Else
                        Try
                            Row(52) = Convert.ToInt64(Matriz(52))

                        Catch ex As Exception
                            ExistsMessage = ExistsMessage + "El campo NVALSUSALUD no tiene formato válido" + "<br>"
                        End Try

                    End If
                Case 2
                    ExistsMessage = ExistsMessage + "El campo NVALSUSALUD excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NVALSUSALUD está vacío" + "<br>"
            End Select
            'EMAIL INTERMEDIARIO
            Select Case EvalColumn(53, Matriz(53))
                Case 0
                    Row(53) = Matriz(53)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SE_MAIL_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SE_MAIL_INT está vacío" + "<br>"
            End Select
            'TIPO VIA INTERMEDIARIO 
            Select Case EvalColumn(54, Matriz(54))
                Case 0
                    Row(54) = Matriz(54)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SRECTYPE_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SRECTYPE_INT está vacío" + "<br>"
            End Select
            'DIRECCION INTERMEDIARIO
            Select Case EvalColumn(55, Matriz(55))
                Case 0
                    Row(55) = Matriz(55)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SSTREET_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SSTREET_INT está vacío" + "<br>"
            End Select
            'NUMERO VIA INTERMEDIARIO  
            Select Case EvalColumn(56, Matriz(56))
                Case 0
                    Row(56) = Matriz(56)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SBUILD_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SBUILD_INT está vacío" + "<br>"
            End Select
            'MANZANA INTERMEDIARIO
            Select Case EvalColumn(57, Matriz(57))
                Case 0
                    Try
                        If Len(Matriz(57)) > 0 Then
                            Row(57) = Matriz(57)
                        Else
                            Row(57) = 0
                        End If

                    Catch ex As Exception
                        ExistsMessage = ExistsMessage + "El campo NFLOOR_INT no tiene formato válido" + "<br>"
                    End Try
                Case 2
                    ExistsMessage = ExistsMessage + "El campo NFLOOR_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NFLOOR_INT está vacío" + "<br>"
            End Select
            'LOTE INTERMEDIARIO
            Select Case EvalColumn(58, Matriz(58))
                Case 0
                    Row(58) = Matriz(58)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SDEPARTAMENT_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SDEPARTAMENT_INT está vacío" + "<br>"
            End Select
            'PISO INTERMEDIARIO
            Select Case EvalColumn(59, Matriz(59))
                Case 0
                    Row(59) = Matriz(59)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SPOPULATION_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SPOPULATION_INT está vacío" + "<br>"
            End Select
            'NRO DEPARTAMENTO INTERMEDIARIO
            Select Case EvalColumn(60, Matriz(60))
                Case 0
                    Row(60) = Matriz(60)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SREFERENCE_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SREFERENCE_INT está vacío " + "<br>"
            End Select
            'INTERIOR INTERMEDIARIO
            Select Case EvalColumn(61, Matriz(61))
                Case 0
                    If String.IsNullOrEmpty(Matriz(61)) Then
                        Row(61) = Convert.ToInt64(0)
                    ElseIf Convert.ToInt32(Matriz(61)) = 0 Then
                        Row(61) = Convert.ToInt64(0)
                    Else
                        Try
                            Row(61) = Convert.ToInt64(Matriz(61))
                        Catch ex As Exception
                            ExistsMessage = ExistsMessage + "El campo NZIPCODE_INT no tiene formato válido" + "<br>"
                        End Try

                    End If
                Case 2
                    ExistsMessage = ExistsMessage + "El campo NZIPCODE_INT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo NZIPCODE_INT está vacío" + "<br>"
            End Select
            'URBANIZACIÓN INTERMEDIARIO
            Select Case EvalColumn(62, Matriz(62))
                Case 0
                    Row(62) = Matriz(62)
                Case 2
                    ExistsMessage = ExistsMessage + "El campo SPHONE_CONT excede el tamaño de carga" + "<br>"
                Case 1
                    ExistsMessage = ExistsMessage + "El campo SPHONE_CONT está vacío" + "<br>"
            End Select


        Else
            'COD INTERMEDIARIO
            If Matriz(13).Length > 0 Then
                Try
                    Row(13) = Convert.ToInt64(LTrim(RTrim(Matriz(13))))
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NINTERMED no tiene formato válido" + "<br>"
                End Try
            End If
            Try
                If Len(Matriz(42)) > 0 Then
                    Row(42) = Matriz(42)
                Else
                    Row(42) = 0
                End If


            Catch ex As Exception
                ExistsMessage = ExistsMessage + "El campo NCOUNTRY_INT no tiene formato válido" + "<br>"
            End Try
            Try
                If Len(Matriz(40)) > 0 Then
                    Row(40) = Matriz(40)
                Else
                    Row(40) = 0
                End If
            Catch ex As Exception
                ExistsMessage = ExistsMessage + "El campo NPERSON_TYP_INT no tiene formato válido" + "<br>"
            End Try
            Row(49) = Getfecha(Matriz(49))
            Row(48) = Matriz(48)
            Row(47) = Matriz(47)
            Row(46) = Matriz(46)
            Row(44) = Matriz(44)
            Try

                If Len(Matriz(43)) > 0 Then
                    Row(43) = Matriz(43)
                Else
                    Row(43) = 0
                End If
            Catch ex As Exception
                ExistsMessage = ExistsMessage + "El campo NMUNICIPALITY_INT no tiene formato válido" + "<br>"
            End Try

            Try
                If Len(Matriz(50)) > 0 Then
                    Row(50) = Convert.ToInt64(Matriz(50))
                Else
                    Row(50) = 0
                End If


            Catch ex As Exception
                ExistsMessage = ExistsMessage + "El campo NCIVILSTA_INT no tiene formato válido" + "<br>"
            End Try
            Row(51) = Matriz(51)
            Row(45) = Matriz(45)
            Row(52) = 0
            Row(53) = Matriz(53)
            Row(54) = Matriz(54)
            Row(55) = Matriz(55)
            Row(56) = Matriz(56)
            Row(57) = 0
            Row(58) = Matriz(58)
            Row(59) = Matriz(59)
            Row(60) = Matriz(60)
            Row(61) = 0
            Row(62) = Matriz(62)
        End If



        'MONEDA
        Select Case EvalColumn(63, Matriz(63))
            Case 0
                Try

                    If Len(Matriz(63)) > 0 Then
                        Row(63) = Matriz(63)
                    Else
                        Row(63) = 0
                    End If
                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NCURRENCY no tiene formato válido" + "<br>"
                End Try

            Case 2
                ExistsMessage = ExistsMessage + "El campo NCURRENCY excede el tamaño de carga" + "<br>"
                Row(63) = 0
            Case 1
                ExistsMessage = ExistsMessage + "El campo NCURRENCY está vacío" + "<br>"
                Row(63) = 0
        End Select


        'tipo de intermediario
        Select Case EvalColumn(64, Matriz(64))
            Case 0
                Try
                    If Len(Matriz(64)) > 0 Then
                        Row(64) = Matriz(64)
                    Else
                        Row(64) = 0
                    End If

                Catch ex As Exception
                    ExistsMessage = ExistsMessage + "El campo NINTERTYP no tiene formato válido" + "<br>"
                End Try

            Case 2
                ExistsMessage = ExistsMessage + "El campo NINTERTYP excede el tamaño de carga" + "<br>"
                Row(64) = 0
            Case 1
                ExistsMessage = ExistsMessage + "El campo NINTERTYP está vacío " + "<br>"
                Row(64) = 0
        End Select

        If ExistsMessage.Length > 0 Then
            Row(65) = ExistsMessage
        Else
            Row(65) = mensaje
        End If


        Row(66) = fila
        Return Row
    End Function

#End Region

#Region "Trama modelo Tabla"

    Private Function EstruturaTramaGenerica() As DataTable

        Dim dt As New DataTable

        'POLIZA
        dt.Columns.Add("NBRANCH", Type.GetType("System.String"))
        dt.Columns.Add("NPRODUCT", Type.GetType("System.String"))
        dt.Columns.Add("NPOLICY", Type.GetType("System.String"))
        dt.Columns.Add("NOFFICE", Type.GetType("System.String"))
        dt.Columns.Add("NSELLCHANNEL", Type.GetType("System.String"))
        dt.Columns.Add("SBUSSITYP", Type.GetType("System.String"))
        dt.Columns.Add("SCOLINVOT", Type.GetType("System.String"))
        dt.Columns.Add("SCOLREINT", Type.GetType("System.String"))
        dt.Columns.Add("SCOLTIMRE", Type.GetType("System.String"))
        dt.Columns.Add("DSTARTDATE", Type.GetType("System.String"))
        dt.Columns.Add("DEXPIRDAT", Type.GetType("System.String"))
        dt.Columns.Add("NPAYFREQ", Type.GetType("System.String"))
        dt.Columns.Add("SCLIENT_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NINTERMED", Type.GetType("System.String"))
        dt.Columns.Add("SINTER_ID", Type.GetType("System.String"))
        dt.Columns.Add("SUSR_SANITAS", Type.GetType("System.String"))
        dt.Columns.Add("NPERCENT", Type.GetType("System.String"))
        dt.Columns.Add("NSPECIALITY", Type.GetType("System.String"))


        'CONTRATANTE => CNT_
        dt.Columns.Add("NPERSON_TYP_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NIDDOC_TYPE_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NCOUNTRY_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NMUNICIPALITY_CONT", Type.GetType("System.String"))

        'CONTRATANTE DATOS PERSONALES  
        dt.Columns.Add("SIDDOC_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SLEGALNAME_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SFIRSTNAME_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME1_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME2_CONT", Type.GetType("System.String"))
        dt.Columns.Add("DBIRTHDAT_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NCIVILSTA_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SSEXCLIEN_CONT", Type.GetType("System.String"))

        'CONTRATANTE DATOS GENERALES 
        dt.Columns.Add("SE_MAIL_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SRECTYPE_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SSTREET_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SBUILD_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NFLOOR_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SDEPARTAMENT_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SPOPULATION_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SREFERENCE_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NZIPCODE_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SPHONE_CONT", Type.GetType("System.String"))

        'INTERMEDIARIO =>INT_
        dt.Columns.Add("NPERSON_TYP_INT", Type.GetType("System.String"))
        dt.Columns.Add("NIDDOC_TYPE_INT", Type.GetType("System.String"))
        dt.Columns.Add("NCOUNTRY_INT", Type.GetType("System.String"))
        dt.Columns.Add("NMUNICIPALITY_INT", Type.GetType("System.String"))

        'INTERMEDIARIO DATOS GENERALES 
        dt.Columns.Add("SIDDOC_INT", Type.GetType("System.String"))
        dt.Columns.Add("SLEGALNAME_INT", Type.GetType("System.String"))
        dt.Columns.Add("SFIRSTNAME_INT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME1_INT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME2_INT", Type.GetType("System.String"))
        dt.Columns.Add("DBIRTHDAT_INT", Type.GetType("System.String"))
        dt.Columns.Add("NCIVILSTA_INT", Type.GetType("System.String"))
        dt.Columns.Add("SSEXCLIEN_INT", Type.GetType("System.String"))
        dt.Columns.Add("NVALSUSALUD", Type.GetType("System.String"))
        dt.Columns.Add("SE_MAIL_INT", Type.GetType("System.String"))
        dt.Columns.Add("SRECTYPE_INT", Type.GetType("System.String"))
        dt.Columns.Add("SSTREET_INT", Type.GetType("System.String"))
        dt.Columns.Add("SBUILD_INT", Type.GetType("System.String"))
        dt.Columns.Add("NFLOOR_INT", Type.GetType("System.String"))
        dt.Columns.Add("SDEPARTAMENT_INT", Type.GetType("System.String"))
        dt.Columns.Add("SPOPULATION_INT", Type.GetType("System.String"))
        dt.Columns.Add("SREFERENCE_INT", Type.GetType("System.String"))
        dt.Columns.Add("NZIPCODE_INT", Type.GetType("System.String"))
        dt.Columns.Add("SPHONE_INT", Type.GetType("System.String"))
        dt.Columns.Add("NCURRENCY", Type.GetType("System.String"))
        dt.Columns.Add("NINTERTYP", Type.GetType("System.String"))

        'DATOS INTERNOS <> NO ESTAN INCLUIDOS EN LA TRAMA 
        dt.Columns.Add("SOBSERVATION", Type.GetType("System.String"))
        dt.Columns.Add("NLINE", Type.GetType("System.Int64"))

        Return dt

    End Function

    Private Function EstructuraTramaCorrecta() As DataTable

        Dim dt As New DataTable

        'POLIZA
        dt.Columns.Add("NBRANCH", Type.GetType("System.Int64"))
        dt.Columns.Add("NPRODUCT", Type.GetType("System.Int64"))
        dt.Columns.Add("NPOLICY", Type.GetType("System.Int64"))
        dt.Columns.Add("NOFFICE", Type.GetType("System.Int64"))
        dt.Columns.Add("NSELLCHANNEL", Type.GetType("System.Int64"))
        dt.Columns.Add("SBUSSITYP", Type.GetType("System.String"))
        dt.Columns.Add("SCOLINVOT", Type.GetType("System.String"))
        dt.Columns.Add("SCOLREINT", Type.GetType("System.String"))
        dt.Columns.Add("SCOLTIMRE", Type.GetType("System.String"))
        dt.Columns.Add("DSTARTDATE", Type.GetType("System.String"))
        dt.Columns.Add("DEXPIRDAT", Type.GetType("System.String"))
        dt.Columns.Add("NPAYFREQ", Type.GetType("System.Int64"))
        dt.Columns.Add("SCLIENT_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NINTERMED", Type.GetType("System.Int64"))
        dt.Columns.Add("SINTER_ID", Type.GetType("System.String"))
        dt.Columns.Add("SUSR_SANITAS", Type.GetType("System.String"))
        dt.Columns.Add("NPERCENT", Type.GetType("System.Int64"))
        dt.Columns.Add("NSPECIALITY", Type.GetType("System.Int64"))


        'CONTRATANTE => CNT_
        dt.Columns.Add("NPERSON_TYP_CONT", Type.GetType("System.Int64"))
        dt.Columns.Add("NIDDOC_TYPE_CONT", Type.GetType("System.Int64"))
        dt.Columns.Add("NCOUNTRY_CONT", Type.GetType("System.Int64"))
        dt.Columns.Add("NMUNICIPALITY_CONT", Type.GetType("System.Int64"))

        'CONTRATANTE DATOS PERSONALES  
        dt.Columns.Add("SIDDOC_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SLEGALNAME_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SFIRSTNAME_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME1_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME2_CONT", Type.GetType("System.String"))
        dt.Columns.Add("DBIRTHDAT_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NCIVILSTA_CONT", Type.GetType("System.Int64"))
        dt.Columns.Add("SSEXCLIEN_CONT", Type.GetType("System.String"))

        'CONTRATANTE DATOS GENERALES 
        dt.Columns.Add("SE_MAIL_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SRECTYPE_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SSTREET_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SBUILD_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NFLOOR_CONT", Type.GetType("System.Int64"))
        dt.Columns.Add("SDEPARTAMENT_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SPOPULATION_CONT", Type.GetType("System.String"))
        dt.Columns.Add("SREFERENCE_CONT", Type.GetType("System.String"))
        dt.Columns.Add("NZIPCODE_CONT", Type.GetType("System.Int64"))
        dt.Columns.Add("SPHONE_CONT", Type.GetType("System.String"))

        'INTERMEDIARIO =>INT_
        dt.Columns.Add("NPERSON_TYP_INT", Type.GetType("System.Int64"))
        dt.Columns.Add("NIDDOC_TYPE_INT", Type.GetType("System.Int64"))
        dt.Columns.Add("NCOUNTRY_INT", Type.GetType("System.Int64"))
        dt.Columns.Add("NMUNICIPALITY_INT", Type.GetType("System.Int64"))
        'INTERMEDIARIO DATOS GENERALES 
        dt.Columns.Add("SIDDOC_INT", Type.GetType("System.String"))
        dt.Columns.Add("SLEGALNAME_INT", Type.GetType("System.String"))
        dt.Columns.Add("SFIRSTNAME_INT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME1_INT", Type.GetType("System.String"))
        dt.Columns.Add("SLASTNAME2_INT", Type.GetType("System.String"))
        dt.Columns.Add("DBIRTHDAT_INT", Type.GetType("System.String"))
        dt.Columns.Add("NCIVILSTA_INT", Type.GetType("System.Int64"))
        dt.Columns.Add("SSEXCLIEN_INT", Type.GetType("System.String"))
        dt.Columns.Add("NVALSUSALUD", Type.GetType("System.Int64"))
        dt.Columns.Add("SE_MAIL_INT", Type.GetType("System.String"))
        dt.Columns.Add("SRECTYPE_INT", Type.GetType("System.String"))
        dt.Columns.Add("SSTREET_INT", Type.GetType("System.String"))
        dt.Columns.Add("SBUILD_INT", Type.GetType("System.String"))
        dt.Columns.Add("NFLOOR_INT", Type.GetType("System.Int64"))
        dt.Columns.Add("SDEPARTAMENT_INT", Type.GetType("System.String"))
        dt.Columns.Add("SPOPULATION_INT", Type.GetType("System.String"))
        dt.Columns.Add("SREFERENCE_INT", Type.GetType("System.String"))
        dt.Columns.Add("NZIPCODE_INT", Type.GetType("System.Int64"))
        dt.Columns.Add("SPHONE_INT", Type.GetType("System.String"))
        dt.Columns.Add("NCURRENCY", Type.GetType("System.Int64"))
        dt.Columns.Add("NINTERTYP", Type.GetType("System.Int64"))

        'DATOS INTERNOS <> NO ESTAN INCLUIDOS EN LA TRAMA 
        dt.Columns.Add("SOBSERVATION", Type.GetType("System.String"))
        dt.Columns.Add("NLINE", Type.GetType("System.Int64"))


        Return dt
    End Function

#End Region


    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click

        Try
            Dim sArchivo As Integer = 0
            If btnValidar.Enabled = True Then
                If (idejcucion.Value = 1) Then
                    sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, "1", Convert.ToInt32(hdIDArchivoProcesador.Value))
                End If
                Response.Redirect("Menu.aspx")
            ElseIf btnProcesar.Enabled = True Then
                If (idejcucion.Value = 2) Then
                    AlertaScripts("No se puede cerrar la página, se está procesando informacion.")
                Else
                    sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, "1", Convert.ToInt32(hdIDArchivoProcesador.Value))
                    Response.Redirect("Menu.aspx")
                End If
            ElseIf btnResumen.Enabled = True Then
                Response.Redirect("Menu.aspx")
                sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, "1", Convert.ToInt32(hdIDArchivoProcesador.Value))
            ElseIf btnError.Enabled = True Then
                Response.Redirect("Menu.aspx")
                sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, "1", Convert.ToInt32(hdIDArchivoProcesador.Value))
            End If
        Catch ex As Exception
            AlertaScripts("No es posible cerrar la página  " & ex.Message.ToString())
        End Try



    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Try
            idejcucion.Value = 2
            Dim idrespuesa As Integer
            Dim vCant As Double = 0
            Dim dtResult As New DataSet
            Dim sArchivo As Integer = 0
            Dim cModulos As String = ""

            For Each row As GridViewRow In gbModulo.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    If cModulos = "" Then
                        cModulos = gbModulo.Rows(row.RowIndex).Cells(1).Text
                    Else
                        cModulos = cModulos + "," + gbModulo.Rows(row.RowIndex).Cells(1).Text
                    End If
                    vCant = vCant + 1

                    'Exit For

                End If
            Next


            If vCant = 0 Then

                AlertaScripts("No ha marcado ningún módulo para Procesar")

            Else
                idrespuesa = Metodos.Procesar_TramaCorrecta(Metodos.DB.ORACLE, Convert.ToInt32(hdIDArchivoProcesador.Value), cModulos)
                dtResult = Metodos.ListarError_Polizas(Metodos.DB.ORACLE, Convert.ToInt32(hdIDArchivoProcesador.Value))


                If idrespuesa = 0 Then
                    Session("ErrorCase") = 3
                    sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, "2", Convert.ToInt32(hdIDArchivoProcesador.Value))
                    btnProcesar.Enabled = True
                Else
                    sArchivo = Metodos.CloseLoadFile(Metodos.DB.ORACLE, "3", Convert.ToInt32(hdIDArchivoProcesador.Value))
                End If

                btnCerrar.Enabled = True
                If dtResult.Tables(3).Rows.Count > 0 Then
                    btnResumen.Enabled = True
                    btnProcesar.Enabled = False
                Else
                    btnResumen.Enabled = False
                End If
                If dtResult.Tables(2).Rows.Count > 0 Then
                    btnError.Enabled = True
                    Session("ErrorCase") = 3
                Else
                    btnError.Enabled = False
                End If

                AlertaScripts("Proceso de carga concluido.")

            End If

        Catch ex As Exception
            Dim message_ As String = String.Empty
            message_ = ex.Message.ToString()
            AlertaScripts("Procesar ::" + message_ + ".")
            Response.Redirect("Default.aspx")
        End Try


    End Sub

    Protected Sub btnResumen_Click(sender As Object, e As EventArgs) Handles btnResumen.Click
        Dim x(4) As String
        Dim reporte As String = ""

        Try

            Session("TABLA_REPORTE") = GetResumen()
            reporte = "RptSCTRResumen.rpt"



            x(0) = "Canal;" & ddlCanal.SelectedItem.Text
            x(1) = "RutaArchivo;" & Convert.ToString(HiddenField2.Value)
            x(2) = "Titulo;" & "Resumen de Emisiones"
            x(3) = "registro;" & hejecutados.Value
            x(4) = "idcarga;" & hdIDArchivoProcesador.Value

            Session("RPT") = reporte
            Session("NombreRPT") = "Carga"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())

        Catch ex As Exception
            AlertaScripts("Ha ocurrido un problema al generar el reporte.")
        End Try
    End Sub

    Protected Sub ddlCanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCanal.SelectedIndexChanged
        Try
            'If ddlCanal.SelectedIndex = 0 Then
            Label5.Visible = False
            gbModulo.DataSource = Nothing
            gbModulo.DataBind()
            rblmarcar.Visible = False
            'Else

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

            'End If

        Catch ex As Exception
            AlertaScripts("Ha ocurrido un problema al seleccionar Canal.")
        End Try

    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try
            Label5.Visible = True
            CargarGrilla()
            rblmarcar.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rblmarcar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblmarcar.SelectedIndexChanged
        Try
            If rblmarcar.Items(0).Selected Then
                System.Threading.Thread.Sleep(500)
                Marcar(True)
            Else
                System.Threading.Thread.Sleep(500)
                Marcar(False)
            End If
        Catch ex As Exception
            AlertaScripts("Ha ocurrido un problema al seleccionar módulo.")
        End Try

    End Sub

    Protected Sub Marcar(ByVal checkState As Boolean)
        For Each row As GridViewRow In gbModulo.Rows
            Dim cb As CheckBox = row.FindControl("ChkSel")
            If cb IsNot Nothing Then
                cb.Checked = checkState
            End If
        Next
    End Sub
End Class
