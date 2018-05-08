Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web
Imports System.Web.Configuration
Imports System.Data.OracleClient

Partial Class ProcesosSCTR_Mov
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet
    Dim dtformato As DataSet
    Dim dtArchivo As DataTable
    Dim sNroArchivo As Integer = 0
    Dim pro As New Process
    Dim dtCarga As DataSet
    Dim sPoliza As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                FileUpload1.Attributes.Add("onChange", "Seleccionar(this);")
                'Session("MovSCTR") = Convert.ToString(Request.QueryString("MovSTRC"))
                txtTitulo.Text = "Carga de Archivo"
                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")
                Call EstadoPasosNext(0)
                Call CargarCombos()
                Call mCargaTitulo()
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

    Private Sub mCargaTitulo()
        Dim sTipoMov As String
        sTipoMov = Session("MovSCTR").ToString

        If sTipoMov = "EM" Then
            lblTipoMovimiento.Text = "EMISIÓN"
        ElseIf sTipoMov = "IN" Then
            lblTipoMovimiento.Text = "INCLUSIÓN"
        ElseIf sTipoMov = "RE" Then
            lblTipoMovimiento.Text = "RENOVACIÓN"
        ElseIf sTipoMov = "EX" Then
            lblTipoMovimiento.Text = "EXCLUSIÓN"
        ElseIf sTipoMov = "AS" Then
            lblTipoMovimiento.Text = "ACTUALIZACIÓN DE DATOS DE ASEGURADOS"
        ElseIf sTipoMov = "IXE" Then
            lblTipoMovimiento.Text = "INCLUSIÓN POR EXCLUSIÓN"
            'INICIO RQ2017-016 MEJORAS SCTR
        ElseIf sTipoMov = "VISA" Then
            lblTipoMovimiento.Text = "REPORTE TRANSACCIONES VISANET"
            'FIN RQ2017-016 MEJORAS SCTR
        End If
    End Sub

    Protected Sub imbAceptar1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAceptar1.Click
        Try
            If ddlCanal.SelectedIndex > 0 And ddlProducto.SelectedIndex > 0 Then
                Dim periodo As Integer
                Dim ciclo As Integer
                Dim vPaso As Int16
                Dim ESTADO As String = ""
                Dim TipFormato As String
                Dim sTipoMov As String
                Try
                    'INICIO RQ2017-016 MEJORAS SCTR
                    sTipoMov = Session("MovSCTR").ToString
                    txtAño.Text = ""
                    If sTipoMov = "VISA" Then
                        txtCabecera.Text = "3"
                    Else
                        txtCabecera.Text = ""
                    End If
                    'FIN RQ2017-016 MEJORAS SCTR

                    txtComentario.Text = ""
                    ddlMeses.SelectedIndex = 0
                    trIcons.Visible = True

                    If ddlCiclo.SelectedIndex = 0 Then
                        If ddlCiclo.Items.Count > 1 Then
                            PanelCierre.Visible = True
                            ModalMensajeCierre.Show()
                        Else
                            Call inicia()
                            imbAceptar1.Enabled = False
                        End If
                        txtAño.Text = Date.Now.Year
                        ddlMeses.SelectedValue = Date.Now.Month.ToString.PadLeft(2, "0")
                    Else
                        MultiView1.Visible = True
                        ddlCanal.Enabled = False
                        ddlProducto.Enabled = False
                        ddlCiclo.Enabled = False

                        Dim X As Int16 = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
                        Dim Z As Int16 = ddlCiclo.Text.Length
                        Dim VCICLO As Integer = Z - X

                        periodo = ddlCiclo.Text.Substring(0, 6)
                        ciclo = ddlCiclo.Text.Substring(7, VCICLO)
                        TipFormato = Session("MovSCTR").ToString
                        dtformato = Metodos.Lista_Datos_Formato_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, 0, periodo, ciclo, TipFormato)
                        Session("DATOS_FORMATO") = dtformato
                        dtArchivo = Metodos.Lista_Datos_Archivo_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, 0, ddlCiclo.Text.Substring(0, 4) & ddlCiclo.Text.Substring(4, 2), ciclo)
                        Session("DATOS_ARCHIVO") = dtArchivo

                        ESTADO = dtformato.Tables(2).Rows(0).Item("ESTADO").ToString
                        Select Case ESTADO
                            Case "R"
                                vPaso = 1
                                txtTitulo.Text = "Carga de Archivo"
                                txtRuta.Text = dtformato.Tables(2).Rows(0).Item("ARCHIVO_ORIGEN").ToString
                                txtAño.Text = ddlCiclo.Text.Substring(0, 4)
                                ddlMeses.SelectedValue = ddlCiclo.Text.Substring(4, 2)
                                txtCabecera.Text = dtformato.Tables(2).Rows(0).Item("NUM_REG_SKIP").ToString
                                txtComentario.Text = dtformato.Tables(2).Rows(0).Item("DES_ARCHIVO").ToString
                                FileUpload1.Visible = False
                                txtRuta.Visible = True
                                txtRuta.ReadOnly = True
                                txtAño.ReadOnly = True
                                ddlMeses.Enabled = False
                                txtCabecera.ReadOnly = True
                                txtComentario.ReadOnly = True
                                btnProcesar.Enabled = False
                            Case "C"
                                vPaso = 2
                                txtTitulo.Text = "Validación y Transferencia"
                                txtAño2.Text = ddlCiclo.Text.Substring(0, 4)
                                ddlMeses.SelectedValue = ddlCiclo.Text.Substring(4, 2)
                                txtMes2.Text = ddlMeses.SelectedItem.Text
                                txtCiclo2.Text = ddlCiclo.Text
                                Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
                                txtReg2.Text = Metodos.Lista_Tabla_Canal(Metodos.DB.ORACLE, vtabla).Compute("count(NUM_REGISTRO)", "")
                                txtAño2.ReadOnly = True
                                txtMes2.ReadOnly = True
                                txtCiclo2.ReadOnly = True
                                txtReg2.ReadOnly = True
                                btnProcesarPaso1.Enabled = False
                                Dim DTf As DataTable = Metodos.Lista_Tabla_fechas_policy_SCTR(Metodos.DB.ORACLE, TipFormato, ddlProducto.SelectedValue)
                                If TipFormato = "RE" Then
                                    txtFechaDef.Text = DTf.Rows(0).Item(1).ToString
                                    txtFecEfecto0.Text = DTf.Rows(0).Item(1).ToString
                                Else
                                    txtFechaDef.Text = DTf.Rows(0).Item(0).ToString
                                    txtFecEfecto0.Text = DTf.Rows(0).Item(0).ToString
                                End If

                                txtFecEfecto0.Enabled = False
                                If TipFormato = "EX" Or TipFormato = "RE" Then
                                    txtFecEfecto0.Visible = True
                                    lblFecEfecto0.Visible = True
                                Else
                                    txtFecEfecto0.Visible = False
                                    lblFecEfecto0.Visible = False
                                End If

                            Case "G"
                                vPaso = 3
                                txtTitulo.Text = "Generación"
                                txtAño3.Text = ddlCiclo.Text.Substring(0, 4)
                                ddlMeses.SelectedValue = ddlCiclo.Text.Substring(4, 2)
                                txtMes3.Text = ddlMeses.SelectedItem.Text
                                txtCiclo3.Text = ddlCiclo.Text
                                txtReg3.Text = Metodos.Lista_Tabla_Pol_SCTR(Metodos.DB.ORACLE, 0, ddlProducto.SelectedValue, TipFormato).Compute("count(NUM_REGISTRO)", "")
                                txtAño3.ReadOnly = True
                                txtMes2.ReadOnly = True
                                txtCiclo3.ReadOnly = True
                                txtReg3.ReadOnly = True
                                btnProcesarPaso3.Enabled = False
                            Case Else
                                vPaso = 1
                                txtTitulo.Text = "Carga de Archivo"
                                txtAño.Text = ""
                                ddlMeses.SelectedIndex = 0
                                txtCabecera.Text = ""
                                txtComentario.Text = ""
                                FileUpload1.Visible = True
                                txtRuta.Visible = False
                        End Select
                        Call EstadoPasosNext(vPaso)
                        MultiView1.ActiveViewIndex = vPaso - 1
                        imbAceptar1.Enabled = False
                    End If
                    FileUpload1.Focus()
                Catch ex As Exception
                    AlertaScripts("No se pudo cargar los datos de carga")
                End Try
            Else
                AlertaScripts("Debe seleccionar CANAL y PRODUCTO para seguir con la carga.")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub inicia()
        Dim periodo As Integer
        Dim ciclo As Integer
        Dim vPaso As Int16
        Dim ESTADO As String = ""
        Dim TipFormato As String

        Try
            periodo = 0
            ciclo = 0
            TipFormato = Session("MovSCTR").ToString
            dtformato = Metodos.Lista_Datos_Formato_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, 0, periodo, ciclo, TipFormato)
            Session("DATOS_FORMATO") = dtformato
            FileUpload1.Visible = True
            txtRuta.Visible = False
            vPaso = 1

            MultiView1.Visible = True
            ddlCanal.Enabled = False
            ddlProducto.Enabled = False
            ddlCiclo.Enabled = False
            btnResTotal.Enabled = False
            btnRegCorrectos.Enabled = False
            btnProblemas.Enabled = False
            txtTitulo.Text = "Carga de Archivo"

            Call EstadoPasosNext(vPaso)
            MultiView1.ActiveViewIndex = vPaso - 1
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CargarCombos()
        dtCombos = Metodos.Lista_Datos_Combos_SCTR(Metodos.DB.ORACLE)

        With ddlCanal
            .DataSource = dtCombos.Tables(0)
            .DataTextField = "DES_CANAL"
            .DataValueField = "CANAL"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlCanal)

        ddlProducto.Enabled = False
        ddlCiclo.Enabled = False
        Session("dtCombos") = dtCombos

        With ddlProducto
            .DataSource = dtCombos.Tables(1)
            .DataTextField = "NPRODUCT"
            .DataValueField = "NPRODUCT"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlProducto)

        ddlCanal.SelectedIndex = 0

        If ddlCanal.Items.Count = 2 Then
            ddlCanal.SelectedIndex = 1
            Call ddlCanal_SelectedIndexChanged(ddlCanal, System.EventArgs.Empty)
            ddlCanal.Enabled = True
        Else
            ddlCanal.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarCombosCiclo()
        dtCombos = Metodos.Lista_Datos_Combos_SCTR(Metodos.DB.ORACLE)

        With ddlCiclo
            .DataSource = dtCombos.Tables(3)
            .DataTextField = "CICLO"
            .DataValueField = "CICLO"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlCiclo)

        ddlCiclo.SelectedIndex = 0
    End Sub

    Private Sub EstadoCombos()
        ddlCanal.Enabled = Not ddlCanal.Enabled
        ddlProducto.Enabled = Not ddlProducto.Enabled
        ddlCiclo.Enabled = Not ddlCiclo.Enabled
    End Sub

    Private Sub EstadoPasosNext(ByVal paso As Int16)
        Select Case paso
            Case 0
                Image1.Visible = False
                Image2.Visible = False
                Image3.Visible = False
                Image4.Visible = False
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

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguiente.Click
        If btnResTotal.Enabled = False Then
            AlertaScripts("Tiene que procesar para ir al siguiente paso")
            Exit Sub
        End If

        Try
            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")
            Dim sTipoMov As String
            sTipoMov = Session("MovSCTR").ToString

            Dim dt As DataTable
            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            dt = Metodos.Lista_Tabla_Canal(Metodos.DB.ORACLE, vtabla)
            Dim vCantCorrec As Int64 = dt.Compute("count(NUM_REGISTRO)", "")
            If (HF_RUTA_ARCHIVO.Value = "") Then
                HF_RUTA_ARCHIVO.Value = dtArchivo.Rows(0).Item("ARCHIVO").ToString + ".txt"
            End If

            If (CantRegArchivo() - txtCabecera.Text) - vCantCorrec > 0 Then
                AlertaScripts("No han pasado todos los registros. Verifique en el segundo paso de validación.")
            End If

            btnErroresPaso1.Enabled = False
            btnCorrectosPaso1.Enabled = False
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
                If sTipoMov = "EX" Or sTipoMov = "RE" Then
                    lblFecEfecto0.Visible = True
                    txtFecEfecto0.Visible = True
                    txtFecEfecto0.Enabled = True
                    Dim DTf As DataTable = Metodos.Lista_Tabla_fechas_policy_SCTR(Metodos.DB.ORACLE, sTipoMov, ddlProducto.SelectedValue)
                    If sTipoMov = "RE" Then
                        txtFecEfecto0.Text = DTf.Rows(0).Item(1).ToString
                    Else
                        txtFecEfecto0.Text = DTf.Rows(0).Item(0).ToString
                    End If

                Else
                    lblFecEfecto0.Visible = False
                    txtFecEfecto0.Visible = False

                End If
            End If

            Dim dtc As DataSet = Metodos.Lista_Conf_Remmp_Renov(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue)
            Session.Add("CONF_REMMP_RENOV", dtc)
            Dim fila() As DataRow = dtc.Tables(2).Select("")
            If fila.Length > 0 Then
                If (fila(0)(1).ToString = "S" And fila(0)(4).ToString = "S") Or (fila(0)(1).ToString = "S" And fila(0)(4).ToString = "") Then ' si es solo reemplazo o reemplazo y carga
                    Dim DTf As DataTable = Metodos.Lista_Tabla_fechas_policy_SCTR(Metodos.DB.ORACLE, sTipoMov, ddlProducto.SelectedValue)
                    Session("FECHAS_POLICY") = DTf
                    If sTipoMov = "RE" Then
                        txtFecEfecto0.Text = DTf.Rows(0).Item(1).ToString
                    Else
                        txtFecEfecto0.Text = DTf.Rows(0).Item(0).ToString
                    End If
                    lblFechaEfecto.Visible = True
                    txtFechaDef.Visible = True

                ElseIf fila(0)(1).ToString = "" And fila(0)(4).ToString = "S" Then ' sie secarga solamente
                Else
                    lblFechaEfecto.Visible = False
                    txtFechaDef.Visible = False
                End If
            Else
                lblFechaEfecto.Visible = False
                txtFechaDef.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSiguientePaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguientePaso1.Click
        If btnErroresPaso1.Enabled = False Then
            AlertaScripts("Tiene que procesar para ir al siguiente paso")
            Exit Sub
        End If
        Try
            Dim sTipoMov As String
            sTipoMov = Session("MovSCTR").ToString
            btnRegxModPaso3.Enabled = False
            If sTipoMov = "EM" Or sTipoMov = "IN" Or sTipoMov = "RE" Or sTipoMov = "IXE" Then
                btnRegxModPaso3.Visible = True
                btnRegxModPaso3.Text = "Registros por Módulo"
            ElseIf sTipoMov = "EX" Then
                btnRegxModPaso3.Visible = True
                btnRegxModPaso3.Text = "Detalle Exclusiones"
            ElseIf sTipoMov = "AS" Then
                btnRegxModPaso3.Visible = True
                btnRegxModPaso3.Text = "Detalle Asegurados"
                'INICIO RQ2017-016 MEJORAS SCTR
            ElseIf sTipoMov = "VISA" Then
                btnRegxModPaso3.Visible = True
                btnRegxModPaso3.Text = "Comisiones VISANET"
                'FIN RQ2017-016 MEJORAS SCTR
            Else
                btnRegxModPaso3.Visible = False
            End If
            txtAño3.Text = txtAño2.Text
            txtMes3.Text = txtMes2.Text
            txtCiclo3.Text = txtCiclo2.Text
            Dim sTipFormato As String = Session("MovSCTR").ToString
            txtReg3.Text = Metodos.Lista_Tabla_Pol_SCTR(Metodos.DB.ORACLE, 0, ddlProducto.SelectedValue, sTipFormato).Compute("count(NUM_REGISTRO)", "")

            If txtReg3.Text = "0" Then
                AlertaScripts("No se pudo cargar los datos. Revise el reporte de errores.")
                btnProcesarPaso1.Enabled = True
            Else
                MultiView1.ActiveViewIndex = 2
                Call EstadoPasosNext(3)
                txtTitulo.Text = "Generación"
                If sTipoMov = "EM" Or sTipoMov = "IN" Or sTipoMov = "AS" Or sTipoMov = "IXE" Then
                    Dim DTf As DataTable = Metodos.Lista_Tabla_fechas_policy_SCTR(Metodos.DB.ORACLE, sTipoMov, ddlProducto.SelectedValue)
                    txtFechaDef.Text = DTf.Rows(0).Item(0).ToString
                    lblFechaEfecto.Visible = True
                    txtFechaDef.Visible = True
                    txtFechaDef.Enabled = True
                ElseIf sTipoMov = "EX" Or sTipoMov = "RE" Then
                    lblFechaEfecto.Visible = True
                    txtFechaDef.Visible = True
                    txtFechaDef.Text = txtFecEfecto0.Text
                    txtFechaDef.Enabled = False
                Else
                    lblFechaEfecto.Visible = False
                    txtFechaDef.Visible = False
                    txtFechaDef.Enabled = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Try
            Dim pos As Integer
            Dim ciclo As Integer
            Dim periodo As String

            dtArchivo = Session("DATOS_ARCHIVO")
            periodo = dtArchivo.Rows(0).Item("PERIODO").ToString
            If ddlCiclo.SelectedIndex <> 0 Then
                pos = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
                ciclo = Mid(ddlCiclo.Text, pos + 1, ddlCiclo.Text.Length)
            Else
                ciclo = dtArchivo.Rows(0).Item("CICLO").ToString
            End If

            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño.Text.Trim & ddlMeses.Text, ciclo)
            MultiView1.Visible = False
            'Call CargarCombos()
            Call EstadoCombos()
            imbCancelar_Click(sender, New ImageClickEventArgs(0, 0))
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo")
        End Try
    End Sub

    Protected Sub btnCerrarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarPaso1.Click
        Try
            Dim pos As Integer
            Dim ciclo As Integer
            Dim periodo As String
            dtArchivo = Session("DATOS_ARCHIVO")
            periodo = dtArchivo.Rows(0).Item("PERIODO").ToString
            If ddlCiclo.SelectedIndex <> 0 Then
                pos = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
                ciclo = Mid(ddlCiclo.Text, pos + 1, ddlCiclo.Text.Length)
            Else
                ciclo = dtArchivo.Rows(0).Item("CICLO").ToString
            End If

            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, periodo, ciclo)
            MultiView1.Visible = False
            Call EstadoCombos()
            imbCancelar_Click(sender, New ImageClickEventArgs(0, 0))
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo")
        End Try
    End Sub

    Protected Sub btnCerrarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarPaso3.Click
        Try
            Dim pos As Integer
            Dim ciclo As Integer
            Dim periodo As String
            dtArchivo = Session("DATOS_ARCHIVO")
            periodo = dtArchivo.Rows(0).Item("PERIODO").ToString
            If ddlCiclo.SelectedIndex <> 0 Then
                pos = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
                ciclo = Mid(ddlCiclo.Text, pos + 1, ddlCiclo.Text.Length)
            Else
                ciclo = dtArchivo.Rows(0).Item("CICLO").ToString
            End If
            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, periodo, ciclo)
            MultiView1.Visible = False
            Call EstadoCombos()
            imbCancelar_Click(sender, New ImageClickEventArgs(0, 0))
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo")
        End Try
    End Sub

    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim RutaBat As String = ""
        Dim sPeriodo As String = ""
        Dim sFormato As String = ""
        Dim sCiclo As Integer = 0
        Dim sSecPol As Integer = 0
        Dim sTipFormato As String
        Dim nCiclo As Integer
        Dim SvalFecha As String
        Dim sFechaEfecto As String
        Dim sTipoMov As String
        sTipoMov = Session("MovSCTR").ToString

        Try
            sFechaEfecto = "01/" & ddlMeses.Text & "/" & txtAño.Text.Trim
            SvalFecha = Metodos.Validar_Fecha_Efecto(Metodos.DB.ORACLE, sFechaEfecto, "", ddlProducto.SelectedValue, sPoliza)
            'INICIO RQ2017-016 MEJORAS SCTR
            If SvalFecha = "FECHA CORRECTA" Or sTipoMov = "VISA" Then
                'FIN RQ2017-016 MEJORAS SCTR
                If FileUpload1.PostedFile.FileName.ToString <> "" And txtCabecera.Text.Trim <> "" Then
                    'Dim sArchivoOrigen As String = HiddenField1.Value

                    Dim sArchivoOrigen As String = FileUpload1.PostedFile.FileName
                    sTipFormato = Session("MovSCTR").ToString 'ddlTipoFormato.SelectedValue
                    nCiclo = Metodos.Abre_Ciclo_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, 0, txtAño.Text.Trim & ddlMeses.Text, txtComentario.Text.Trim, sArchivoOrigen, txtCabecera.Text, txtComentario.Text.Trim, sTipFormato)
                    System.Threading.Thread.Sleep(500)
                    dtArchivo = Metodos.Lista_Datos_Archivo_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, 0, txtAño.Text.Trim & ddlMeses.Text, nCiclo)
                    sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
                    sSecPol = dtArchivo.Rows(0).Item("SEC_POLIZA").ToString
                    sNroArchivo = dtArchivo.Rows(0).Item("Num_Archivo").ToString

                    dtformato = Metodos.Lista_Datos_Formato_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, 0, txtAño.Text.Trim & ddlMeses.Text, sCiclo, sTipFormato)

                    Session("DATOS_ARCHIVO") = dtArchivo
                    Session("DATOS_FORMATO") = dtformato

                    sFormato = dtformato.Tables(0).Rows(0).Item("FORMATO").ToString
                    RutaBat = Server.MapPath("").Replace("\", "/")
                    Dim ourFileGen As FileInfo = New FileInfo(RutaBat & "/bat/" & ddlCanal.SelectedValue & "POL" & sPoliza & ".bat")
                    If ourFileGen.Exists Then
                        My.Computer.FileSystem.DeleteFile(RutaBat & "/bat/" & ddlCanal.SelectedValue & "POL" & sPoliza & ".bat")
                    End If
                    'crea carpeta de trabajo
                    sPeriodo = txtAño.Text.Trim & ddlMeses.SelectedValue & "-" & sCiclo.ToString.PadLeft(3, "0")
                    Call CreaDirectorioTrabajo(sPeriodo)

                    ' Mover el fichero.si existe lo sobreescribe   
                    Dim sRutaDestino As String = HF_RUTA_MASIVA.Value & ddlCanal.Text & "/PROD" & ddlProducto.Text & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".TXT"

                    FileUpload1.SaveAs(sRutaDestino)

                    HF_RUTA_ARCHIVO.Value = sRutaDestino

                    'Elimina archivos
                    'Dim ourFile As FileInfo = New FileInfo(HF_RUTA_EJECUTABLE.Value & "SALIDA.CTL")
                    'If ourFile.Exists Then
                    '    My.Computer.FileSystem.DeleteFile(HF_RUTA_EJECUTABLE.Value & "SALIDA.CTL")
                    'End If

                    Dim ourFileCTL As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL")
                    If ourFileCTL.Exists Then
                        My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL")
                    End If

                    Dim ourFileLOG As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG")
                    If ourFileLOG.Exists Then
                        My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG")
                    End If

                    Dim ourFileBAD As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".BAD")
                    If ourFileBAD.Exists Then
                        My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".BAD")
                    End If

                    Dim ourFileDSC As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".DSC")
                    If ourFileDSC.Exists Then
                        My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".DSC")
                    End If

                    Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".txt")
                    If ourFiletxt.Exists Then
                        My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".txt")
                    End If

                    'CREA ARCHIVO .CTL Y .BAT 
                    Call CreaArchivoCTL(sPeriodo, sSecPol)
                    System.Threading.Thread.Sleep(1000)

                    Call CreaBat(sPeriodo, sSecPol)
                    'borra tablas asociadas
                    Metodos.Elimina_Archivo_Asociados_SCTR(Metodos.DB.ORACLE, sNroArchivo, sTipFormato)
                    'genera secuencia
                    Metodos.Genera_Secuencia(Metodos.DB.ORACLE, sFormato)
                    'SQL*LOADER y LO EJECUTA
                    RutaBat = Server.MapPath("").Replace("\", "/")

                    Call EjecutaHilo()
                    btnResTotal.Enabled = True
                    btnRegCorrectos.Enabled = True
                    btnProblemas.Enabled = True
                    txtRuta.Text = sArchivoOrigen
                    FileUpload1.Visible = False
                    txtRuta.Visible = True
                    btnProcesar.Enabled = False

                    txtRuta.ReadOnly = True
                    txtAño.ReadOnly = True
                    ddlMeses.Enabled = False
                    txtCabecera.ReadOnly = True
                    txtComentario.ReadOnly = True

                    Call EstadoPasosProc(1)
                    Call VerificaProceso()
                    Metodos.Archivo_Estado(Metodos.DB.ORACLE, sNroArchivo, "R")
                    'AlertaScripts("estado")

                    AlertaScripts("Proceso concluido")

                    'Actualiza estado
                Else
                    If FileUpload1.PostedFile.FileName.ToString = "" Then
                        AlertaScripts("Seleccione un archivo")
                    ElseIf txtCabecera.Text.Trim = "" Then
                        AlertaScripts("Indicar número de cabecera")
                    End If
                End If
            Else
                AlertaScripts(SvalFecha)
            End If

            'div loading
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)

        Catch ex As Exception
            'Throw ex
            AlertaScripts("No se pudo Procesar")
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            'AlertaScripts("bat 1 " & ruta_Bat)
            theFileBat = File.Open(ruta_Bat & "\bat\error.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try

    End Sub

    Private Sub EjecutaHilo()
        Dim arrThreads(2) As Thread

        arrThreads(0) = New Thread(New ThreadStart(AddressOf CorreProceso))
        arrThreads(0).Start()
    End Sub

    Private Sub CorreProceso()
        Dim RutaBat As String = Server.MapPath("")
        pro.StartInfo.WorkingDirectory = RutaBat & "\Bat\"

        pro.StartInfo.FileName = ddlCanal.SelectedValue & "POL" & sPoliza & ".bat"
        pro.Start()

    End Sub

    Private Sub VerificaProceso()
        Try
salto:
            System.Threading.Thread.Sleep(2000)
            If (pro.HasExited = False) Then
                If (pro.Responding) Then
                    'El proceso estaba respondiendo; cerrar la ventana principal.
                    GoTo salto
                Else
                    'El proceso no estaba respondiendo; forzar el cierre del proceso.
                    pro.Kill()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreaBat(ByVal Periodo As String, ByVal SecPol As Integer)
        Dim tex As String = ""
        Try
            Dim ruta As String = ""
            Dim ruta_Bat As String = ""
            Dim vBaseDatos As String = ConfigurationManager.AppSettings("BD").ToString()
            Dim vPasswordBD As String = ConfigurationManager.AppSettings("PasswordBD").ToString()
            Dim vUserBD As String = ConfigurationManager.AppSettings("UserBD").ToString()

            ruta = HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "\PROD" & ddlProducto.SelectedValue & "\" & Periodo & "\POL" & sPoliza & "-" & SecPol.ToString.PadLeft(2, "0")

            Dim s As New System.Text.StringBuilder
            s.AppendLine("@echo off")
            s.AppendLine("sqlldr " & vUserBD & "/" & vPasswordBD & "@" & vBaseDatos & " control=" & ruta & ".CTL")
            s.AppendLine("move POL" & sPoliza & "-" & SecPol.ToString.PadLeft(2, "0") & ".LOG  " & ruta & ".LOG")
            's.AppendLine("pause")
            tex = s.ToString

            Dim theFileBat As FileStream
            ruta_Bat = Server.MapPath("")

            theFileBat = File.Open(ruta_Bat & "\bat\" & ddlCanal.SelectedValue & "POL" & sPoliza & ".bat", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(tex)
            filaEscribebat.Close()
            theFileBat.Close()

        Catch ex As Exception
            Throw
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
            str.AppendLine("CHARACTERSET WE8MSWIN1252")
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
            rut = HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL"
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

    Private Sub CreaDirectorioTrabajo(ByVal sPeriodo As String)
        Dim di As DirectoryInfo
        Dim rUTA As String = ""
        Try
            rUTA = HF_RUTA_MASIVA.Value & ddlCanal.Text & "\PROD" & ddlProducto.Text & "\" & sPeriodo
            Try
                If Directory.Exists(rUTA) = False Then
                    di = Directory.CreateDirectory(rUTA)
                End If
            Finally
            End Try
        Catch ex As Exception
            AlertaScripts("No se pudo crear la carpeta de trabajo")
        End Try
    End Sub

    Protected Sub btnProcesarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso1.Click
        Try
            Dim fecha As DateTime
            Dim sTipoMov As String
            Dim SvalFecha As String
            Dim sFechaEfecto As String

            sTipoMov = Session("MovSCTR").ToString

            If sTipoMov = "EX" Or sTipoMov = "RE" Then
                If Not DateTime.TryParse(txtFecEfecto0.Text, fecha) Then
                    AlertaScripts("Debe ingresar una fecha Válida")
                Else
                    sFechaEfecto = txtFecEfecto0.Text
                    SvalFecha = Metodos.Validar_Fecha_Efecto(Metodos.DB.ORACLE, sFechaEfecto, sTipoMov, ddlProducto.SelectedValue, 0)
                    If SvalFecha = "FECHA CORRECTA" Then
                        dtArchivo = Session("DATOS_ARCHIVO")
                        dtformato = Session("DATOS_FORMATO")
                        Dim vConsistencia As String = dtformato.Tables(0).Rows(0).Item("CONSISTENCIA").ToString
                        Dim vTranslada As String = dtformato.Tables(0).Rows(0).Item("TRASLADA_REG").ToString
                        'inserta en la tabla de errores TBL_TRX_ERROR
                        Dim NumArchivo As Integer = dtArchivo.Rows(0).Item("num_archivo")

                        Metodos.Inserta_errores(Metodos.DB.ORACLE, NumArchivo, vConsistencia, "C")

                        Call EstadoPasosProc(2)

                        Metodos.Traslado_Registros(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), vTranslada)
                        'ACTUALIZA ESTADO
                        Metodos.Archivo_Estado(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), "C")
                        System.Threading.Thread.Sleep(500)

                        'div loading
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)

                        AlertaScripts("Proceso concluido")
                        btnProcesarPaso1.Enabled = False
                        btnErroresPaso1.Enabled = True
                        btnCorrectosPaso1.Enabled = True
                        btnResumenPaso1.Enabled = True
                        txtFecEfecto0.Enabled = False
                    Else
                        AlertaScripts(SvalFecha)
                    End If
                End If
            Else
                dtArchivo = Session("DATOS_ARCHIVO")
                dtformato = Session("DATOS_FORMATO")
                Dim vConsistencia As String = dtformato.Tables(0).Rows(0).Item("CONSISTENCIA").ToString
                Dim vTranslada As String = dtformato.Tables(0).Rows(0).Item("TRASLADA_REG").ToString
                'inserta en la tabla de errores TBL_TRX_ERROR
                Dim NumArchivo As Integer = dtArchivo.Rows(0).Item("num_archivo")

                Metodos.Inserta_errores(Metodos.DB.ORACLE, NumArchivo, vConsistencia, "C")

                Call EstadoPasosProc(2)

                'inserta en tabla tbl_trx_exclusion y tbl_trx_pol#poliza
                Metodos.Traslado_Registros(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), vTranslada)
                'ACTUALIZA ESTADO
                Metodos.Archivo_Estado(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), "C")
                System.Threading.Thread.Sleep(500)

                'div loading
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)

                AlertaScripts("Proceso concluido")
                btnProcesarPaso1.Enabled = False
                btnErroresPaso1.Enabled = True
                btnCorrectosPaso1.Enabled = True
                btnResumenPaso1.Enabled = True
            End If
        Catch ex As Exception
            AlertaScripts(Err.Description)
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("") 'HF_RUTA_SQL.Value & ddlCanal.SelectedValue & "POL" & sPoliza & ".bat" '& "-" & SecPol.ToString.PadLeft(2, "0") & ".BAT"   '
            'AlertaScripts("bat 1 " & ruta_Bat)
            theFileBat = File.Open(ruta_Bat & "\bat\error2.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try
    End Sub

    Protected Sub btnProcesarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso3.Click
        Dim dt As DataTable
        Try
            Dim fecha As DateTime
            Dim SvalFecha As String
            Dim sFechaEfecto As String
            Dim sTipoMov As String

            sTipoMov = Session("MovSCTR").ToString
            PnelCarga.Visible = True
            'INICIO RQ2017-016 MEJORAS SCTR
            If sTipoMov = "VISA" Then
                'FIN RQ2017-016 MEJORAS SCTR
                lblMensajeProceso.Text = mMensajeProceso()
                mpeCarga.Show()
            Else
                If Not DateTime.TryParse(txtFechaDef.Text, fecha) Then
                    AlertaScripts("Debe ingresar una fecha Válida")
                Else
                    sFechaEfecto = txtFechaDef.Text
                    SvalFecha = Metodos.Validar_Fecha_Efecto(Metodos.DB.ORACLE, sFechaEfecto, sTipoMov, ddlProducto.SelectedValue, sPoliza)

                    If SvalFecha = "FECHA CORRECTA" Then

                        dt = Metodos.Lista_Fecha_Expiracion_SCTR(Metodos.DB.ORACLE, 1, ddlProducto.SelectedValue, sTipoMov)
                        Dim fila() As DataRow = dt.Select("")
                        If fila.Length > 0 And fila(0)(0).ToString.Trim.Length > 0 Then
                            If CDate(fila(0)(0).ToString) < CDate(txtFechaDef.Text) Then
                                AlertaScripts("La fecha de Expiración de la póliza es menor que la fecha de efecto ingresada. verifique")
                                Exit Sub
                            End If
                        End If
                        lblMensajeProceso.Text = mMensajeProceso()
                        mpeCarga.Show()
                    Else
                        AlertaScripts(SvalFecha)
                    End If
                End If
            End If
        Catch ex As Exception
            AlertaScripts("Ocurrio un error inesperado al procesar")
        End Try
    End Sub

    Protected Function mMensajeProceso() As String
        Dim MsgProceso As String
        Dim sTipoMov As String
        sTipoMov = Session("MovSCTR").ToString

        If sTipoMov = "EM" Then
            MsgProceso = "Este proceso cargará el Movimiento de Emisión del Producto 120 - SCTR - Pensión. ¿Desea Procesar?"
        ElseIf sTipoMov = "IN" Then
            MsgProceso = "Este proceso cargará el Endoso de Inclusión del Producto 120 - SCTR - Pensión. ¿Desea Procesar?"
        ElseIf sTipoMov = "RE" Then
            MsgProceso = "Este proceso cargará el Movimiento Renovación del Producto 120 - SCTR - Pensión. ¿Desea Procesar?"
        ElseIf sTipoMov = "EX" Then
            MsgProceso = "Este proceso cargará el Endoso de Exclusión del Producto 120 - SCTR - Pensión. ¿Desea Procesar?"
        ElseIf sTipoMov = "AS" Then
            MsgProceso = "Este proceso realizará la Actualización de Datos de Asegurados del Producto 120 - SCTR - Pensión. ¿Desea Procesar?"
        ElseIf sTipoMov = "IXE" Then
            MsgProceso = "Este proceso cargará el Endoso de Inclusión por Exclusión del Producto 120 - SCTR - Pensión. ¿Desea Procesar?"
            'INICIO RQ2017-016 MEJORAS SCTR
        ElseIf sTipoMov = "VISA" Then
            MsgProceso = "Este proceso cargará las comisiones reportadas por VISANET. ¿Desea Procesar?"
            'FIN RQ2017-016 MEJORAS SCTR
        End If

        Return MsgProceso
    End Function

    Protected Sub imbCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelar.Click
        MultiView1.Visible = False

        Call CargarCombos()

        imbAceptar1.Enabled = True
        btnProcesar.Enabled = True

        txtCabecera.ReadOnly = False
        txtComentario.ReadOnly = False
        ddlMeses.Enabled = True
        txtAño.ReadOnly = False

        btnProcesarPaso1.Enabled = True
        btnProcesarPaso3.Enabled = True

        Call EstadoPasosNext(0)
        txtTitulo.Text = ""
        LblCCarga.Text = ""
        LblTit.Text = ""
        trIcons.Visible = False
    End Sub

    Protected Sub ddlCanal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCanal.SelectedIndexChanged
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
            ddlCiclo.SelectedIndex = 0
            ddlCiclo.Enabled = False

            LblCCarga.Text = ""
            LblTit.Text = ""

            If ddlProducto.Items.Count = 2 Then
                ddlProducto.SelectedIndex = 1
                Call ddlProducto_SelectedIndexChanged(ddlProducto, System.EventArgs.Empty)
            Else
                ddlProducto.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try
            If ddlProducto.SelectedIndex = 0 Then

                LblCCarga.Text = ""
                LblTit.Text = ""
            Else
                
                LblCCarga.Text = ""
                LblTit.Text = ""

                dtCombos = Session("dtCombos")

                Dim nCountJob As Integer
                Dim sTipFormato As String

                sTipFormato = Session("MovSCTR").ToString

                nCountJob = Metodos.Val_Estado_Job(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, "", sTipFormato)

                If nCountJob = 0 Then
                    Dim TipFormato As String = Session("MovSCTR").ToString
                    dtCombos = Session("dtCombos")
                    Dim arr As New ArrayList
                    For Each fila As DataRow In dtCombos.Tables(3).Select("CANAL='" & ddlCanal.SelectedValue & "' AND NPRODUCT=" & ddlProducto.SelectedValue & " AND MOV_SCTR ='" & TipFormato & "'")
                        arr.Add(New Listado(fila("CICLO"), fila("CICLO")))
                    Next

                    If arr.Count > 0 Then
                        With ddlCiclo
                            .DataSource = arr
                            .DataTextField = "Descripcion"
                            .DataValueField = "Codigo"
                            .DataBind()
                        End With
                        Global.Metodos.AgregarItemCombo(ddlCiclo)
                    Else
                        ddlCiclo.Items.Clear()
                    End If
                    If ddlCiclo.Items.Count = 0 Then
                        Global.Metodos.AgregarItemCombo(ddlCiclo)
                    End If

                    LblCCarga.Text = ""
                    LblTit.Text = ""

                    ddlCiclo.Enabled = True
                    ddlCiclo.SelectedIndex = 0
                Else
                    LblCCarga.Text = "Existe un JOB que no se ha ejecutado, para realizar una nueva carga debe culminar la ejecucion del JOB"
                    LblTit.Text = "Observación:"
                    imbAceptar1.Enabled = False

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CerrarTodosCiclos()
        Try
            For x As Integer = 1 To ddlCiclo.Items.Count - 1
                Dim pos As Integer = InStr(ddlCiclo.Items(x).Text, "-", CompareMethod.Text)
                Dim ciclo As Integer = Mid(ddlCiclo.Items(x).Text, pos + 1, ddlCiclo.Items(x).Text.Length)
                Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlCiclo.Items(x).Text.ToString.Substring(0, 4) & ddlCiclo.Items(x).Text.ToString.Substring(4, 2), ciclo)
            Next
            MultiView1.Visible = False
            Call CargarCombosCiclo()
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo")
        End Try
    End Sub

    Protected Sub imbAcepCierre_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAcepCierre.Click
        Call CerrarTodosCiclos()
        Call inicia()
        PanelCierre.Visible = False
        imbAceptar1.Enabled = False

    End Sub

    Protected Sub btnRegCorrectos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegCorrectos.Click
        Try
            dtformato = Session("DATOS_FORMATO")
            dtArchivo = Session("DATOS_ARCHIVO")
            Dim dt As DataTable
            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = dtArchivo.Rows(0).Item("PERIODO").ToString
            Dim sNumArchivo As Integer = dtArchivo.Rows(0).Item("NUM_ARCHIVO").ToString
            Dim sTipoMov As String = Session("MovSCTR").ToString

            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & sPeriodo & "-" & sCiclo
            x(5) = "Archivo;" & sNumArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & lblTipoMovimiento.Text

            'INICIO RQ2017-016 MEJORAS SCTR
            dt = Metodos.Lista_Report_Correctos_SCTR(Metodos.DB.ORACLE, vtabla)
            Session("TABLA_REPORTE") = dt
            If sTipoMov = "VISA" Then
                Session("RPT") = "Paso1_Detallado_ComisionVISA.rpt"
            Else
                Session("RPT") = "Paso1_Detallado_Mov.rpt"
            End If
            'FIN RQ2017-016 MEJORAS SCTR

            Session("NombreRPT") = "Detallado"
            Session("Arreglo") = x
            Session("Data") = "S"
            'Server.Transfer("Reportes_PL.aspx", True)
            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Protected Sub btnResTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResTotal.Click
        Try
            Dim dt As DataTable
            Dim vCantCorrec As Integer
            dtformato = Session("DATOS_FORMATO")
            dtArchivo = Session("DATOS_ARCHIVO")
            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = dtArchivo.Rows(0).Item("PERIODO").ToString
            Dim sNumArchivo As Integer = dtArchivo.Rows(0).Item("NUM_ARCHIVO").ToString
            Dim sTipoMov As String = Session("MovSCTR").ToString

            If (HF_RUTA_ARCHIVO.Value = "") Then
                HF_RUTA_ARCHIVO.Value = dtArchivo.Rows(0).Item("ARCHIVO").ToString + ".txt"
            End If
            dt = Metodos.Lista_Report_Correctos_SCTR(Metodos.DB.ORACLE, vtabla)

            vCantCorrec = dt.Compute("count(Numero)", "")

            Session("TABLA_REPORTE") = dt

            'INICIO RQ2017-016 MEJORAS SCTR
            If sTipoMov = "VISA" Then
                Session("RPT") = "Paso1_Resumido_ComisionVISA.rpt"
            Else
                Session("RPT") = "Paso1_Resumido_Mov.rpt"
            End If
            'FIN RQ2017-016 MEJORAS SCTR

            Session("NombreRPT") = "Detallado"
            Session("Data") = "S" '"S" si se manda datatable al reporte si ni "N"
            Dim x(9) As String
            x(0) = "CANT_TOT;" & CantRegArchivo() - CInt(txtCabecera.Text)
            x(1) = "CANT_CORRECT;" & vCantCorrec
            x(2) = "NomEmpresa;Protecta Compañia de Seguros"
            x(3) = "Canal;" & ddlCanal.SelectedItem.Text
            x(4) = "Producto;" & ddlProducto.SelectedItem.Text
            x(5) = "Poliza;" & ""
            x(6) = "ciclo;" & sPeriodo & "-" & sCiclo
            x(7) = "Archivo;" & sNumArchivo
            x(8) = "RutaArchivo;" & txtRuta.Text
            x(9) = "Titulo;" & lblTipoMovimiento.Text

            Session("Arreglo") = x

            'Server.Transfer("Reportes_PL.aspx", True)
            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try

    End Sub

    Protected Sub btnProblemas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProblemas.Click

        Dim x(7) As String

        Try
            dtArchivo = Session("DATOS_ARCHIVO")

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo")
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            'Dim sPeriodo As String = txtAño4.Text.Trim & ddlMeses.SelectedValue
            Dim sPeriodo As String = dtArchivo.Rows(0).Item("PERIODO").ToString

            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ""
            x(4) = "Ciclo;" & sPeriodo & "-" & sCiclo
            x(5) = "Archivo;" & vArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & lblTipoMovimiento.Text

            Session("TABLA_REPORTE") = TablaProblemasCargaInicial()
            Session("RPT") = "Paso1_Carga_Mov.rpt"
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

    Protected Sub btnErroresPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnErroresPaso1.Click
        Try
            Dim dt As DataTable
            dtArchivo = Session("DATOS_ARCHIVO")
            Dim sNroArchivo As Integer = dtArchivo.Rows(0).Item("Num_Archivo").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString

            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & lblTipoMovimiento.Text

            dt = Metodos.Lista_Report_Errores(Metodos.DB.ORACLE, sNroArchivo)

            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso2_Errores_Mov.rpt"
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
            Dim sTipFormato As String = Session("MovSCTR").ToString 'ddlTipoFormato.SelectedValue
            Dim sTipoMov As String = Session("MovSCTR").ToString

            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & lblTipoMovimiento.Text

            dt = Metodos.Lista_Tabla_Pol_SCTR(Metodos.DB.ORACLE, 0, ddlProducto.SelectedValue, sTipFormato)

            Session("TABLA_REPORTE") = dt

            'INICIO RQ2017-016 MEJORAS SCTR
            If sTipoMov = "VISA" Then
                Session("RPT") = "Paso2_Transferidos_ComisionVISA.rpt"
            Else
                Session("RPT") = "Paso2_Transferidos_Mov.rpt"
            End If
            'FIN RQ2017-016 MEJORAS SCTR

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

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo").ToString
            Dim vTabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sTipFormato As String = Session("MovSCTR").ToString
            Dim sTipoMov As String = Session("MovSCTR").ToString

            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & vArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & lblTipoMovimiento.Text

            dt = Metodos.Lista_Report_Resumen_SCTR(Metodos.DB.ORACLE, 0, vTabla, vArchivo, sTipFormato)

            Session("TABLA_REPORTE") = dt
 
            'INICIO RQ2017-016 MEJORAS SCTR
            If sTipoMov = "VISA" Then
                Session("RPT") = "Paso2_Resumen_ComisionVISA.rpt"
            Else
                Session("RPT") = "Paso2_Resumen_Mov.rpt"
            End If
            'FIN RQ2017-016 MEJORAS SCTR

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

    Protected Sub btnRegxModPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegxModPaso3.Click
        Dim dt As DataTable
        Dim x(8) As String

        Try
            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo")
            Dim vTabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sTipFormato As String = Session("MovSCTR").ToString

            dt = Metodos.Lista_Procesados_SCTR_MOV(Metodos.DB.ORACLE, 0, ddlProducto.SelectedValue, sTipFormato)
            'dt = Session("TABLA_POL")
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ""
            x(4) = "ciclo;" & txtAño3.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & vArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & lblTipoMovimiento.Text
            x(8) = "Total;" & dt.Rows.Count

            Session("TABLA_REPORTE") = dt

            'INICIO RQ2017-016 MEJORAS SCTR
            If sTipFormato = "VISA" Then
                Session("RPT") = "Paso3_Procesados_ComisionVISA.rpt"
            ElseIf sTipFormato = "AS" Then
                Session("RPT") = "Paso3_Detallado_Asegurados.rpt"
            Else
                Session("RPT") = "Paso3_Modulos_Mov.rpt"
            End If
            'FIN RQ2017-016 MEJORAS SCTR


            Session("NombreRPT") = "Modulos"
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
            Dim ruta As String = HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & sPoliza & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG"

            'defino columnas
            dt.Columns.Add("ID", Type.GetType("System.Int64"))
            dt.Columns.Add("Descripcion", Type.GetType("System.String"))


            'modo de apertura de archivo
            theFile = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.Read)
            Try
                Using filaLee As StreamReader = New StreamReader(theFile)
                    Dim line As String
                    Dim varid As Int64 = 0
                    Dim var_frace As String
                    'recorro linea por linea y grabo en datatable a partir q encuentra la palabra record  hasta q encuentre la palabra table '' o maximum
                    Do
                        line = filaLee.ReadLine()
                        var_frace = Mid(line, 1, 9)
                        exist = IIf(InStr(var_frace, "Registro", CompareMethod.Text), exist + 1, exist)
                        exist = IIf(InStr(var_frace, "ORA", CompareMethod.Text), exist + 1, exist)
                        If exist > 0 Then
                            varid += 1
                            dtr = dt.NewRow
                            dtr(0) = varid
                            dtr(1) = line
                            dt.Rows.Add(dtr)
                        End If
                    Loop Until line Is Nothing
                    filaLee.Close()
                End Using
            Catch ex As Exception

            End Try

            theFile.Close()

            TablaProblemasCargaInicial = dt

        Catch ex As Exception
            AlertaScripts("No se pudo abrir el archivo log")
        End Try
    End Function

    Protected Sub imbCancelarCarga_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelarCarga.Click
        PnelCarga.Visible = False
        mpeCarga.Hide()
    End Sub

    Protected Sub imbAceptarCarga_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAceptarCarga.Click

        Call Remplazo_Nomina_500()

    End Sub

    Private Sub Remplazo_Nomina_500()
        Try
            Dim DT As DataTable
            Dim sPeriodo As String = ""
            Dim sCiclo As Integer = 0
            Dim sEstadoJob As String
            Dim año As String
            Dim mes As String
            Dim dia As String

            dtArchivo = Session("DATOS_ARCHIVO")
            dtformato = Session("DATOS_FORMATO")
            Dim vTransformada As String = dtformato.Tables(0).Rows(0).Item("TRANSFORMADA").ToString
            Dim DT_FECHAS As DataTable = Session("FECHAS_POLICY")
            Dim sTipFormato As String = Session("MovSCTR").ToString

            sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
            'borra archivos
            Dim ourFilelst As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & sPoliza & ".LST")
            If ourFilelst.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & sPoliza & ".LST")
            End If
            Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & sPoliza & ".TXT")
            If ourFiletxt.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & sPoliza & ".TXT")
            End If
            'GUARDA DATOS PARA PASO 4 POR Q LIMPIA TABLA POL
            'Session("TABLA_POL") = Metodos.Lista_Tabla_Pol_SCTR(Metodos.DB.ORACLE, 0, ddlProducto.SelectedValue, sTipFormato)
            'DT = Session("TABLA_POL")

            'Session("MODULOS") = Metodos.Lista_Modulo_Pol_SCTR(Metodos.DB.ORACLE, 0, sTipFormato)
            'genera archivo

            If sTipFormato = "VISA" Then
                Dim thisDay As String = DateTime.Today.ToString
                año = thisDay.Substring(6, 4)
                mes = thisDay.Substring(3, 2)
                dia = thisDay.Substring(0, 2)
            Else
                año = txtFechaDef.Text.Substring(6, 4)
                mes = txtFechaDef.Text.Substring(3, 2)
                dia = txtFechaDef.Text.Substring(0, 2)
            End If

            sEstadoJob = Metodos.Archivo_Genera_Tarea(Metodos.DB.ORACLE, 0, Session("CodUsuario"), año & mes & dia, vTransformada, "C")
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
            If sEstadoJob = "5" Then
                'cambia de estado a 'G'
                Metodos.Archivo_Estado_PASO3(Metodos.DB.ORACLE, 0, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo, "G")

                Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo)
                btnCerrarPaso3.Enabled = False
                btnProcesarPaso3.Enabled = False
                txtFechaDef.Enabled = False
                Call EstadoPasosProc(3)
                btnRegxModPaso3.Enabled = True
                AlertaScripts("Ejecución exitosa")
            Else
                AlertaScripts("Ocurrio un error al Ejecutar el Proceso")
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
            'AlertaScripts(Err.Description)
            AlertaScripts("Ocurrio un Error")
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("") 'HF_RUTA_SQL.Value & ddlCanal.SelectedValue & "POL" & sPoliza & ".bat" '& "-" & SecPol.ToString.PadLeft(2, "0") & ".BAT"   '
            'AlertaScripts("bat 1 " & ruta_Bat)
            theFileBat = File.Open(ruta_Bat & "\bat\error3.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try
    End Sub
End Class
