Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration

Partial Class ProcesosHCR
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet
    Dim dtformato As DataSet
    Dim dtArchivo As DataTable
    Dim sNroArchivo As Integer = 0
    Dim pro As New Process
    Dim dtCarga As DataSet
    Dim validaPoliza As Integer
    Private dtConsulta As DataTable
    Protected Property MaxRequestLength() As Integer
        Get
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Protected Sub inicia()
        Dim periodo As Integer
        Dim ciclo As Integer
        Dim vPaso As Int16
        Dim ESTADO As String = ""
        Try

            periodo = 0
            ciclo = 0

            Dim mensaje As String
           
            Try

                validaPoliza = Metodos.Val_Estado_Poliza_En_Proceso(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue)

            Catch ex As Exception

            End Try



            dtformato = Metodos.Lista_Datos_Formato(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, periodo, ciclo)
            Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue) = dtformato
            FileUpload1.Visible = True
            txtRuta.Visible = False
            vPaso = 1

            MultiView1.Visible = True
            ddlCanal.Enabled = False
            ddlProducto.Enabled = False
            ddlPoliza.Enabled = False
            ddlCiclo.Enabled = False
            btnResTotal.Enabled = False
            btnRegCorrectos.Enabled = False
            btnProblemas.Enabled = False
            txtTitulo.Text = "Carga de Archivo"

            If dtformato.Tables(0).Rows(0).Item("TIPO_CARGA") = "C" Then
                HF_TIPO.Value = "C"
                Call EstadoPasosNext(vPaso, HF_TIPO.Value) 'COLECTIVA
                MultiView1.ActiveViewIndex = vPaso - 1
            Else
                HF_TIPO.Value = "M"
                Call EstadoPasosNext(vPaso, HF_TIPO.Value)
                MultiView1.ActiveViewIndex = vPaso - 1
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imbAceptar1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAceptar1.Click
        Try

          

            If ddlCanal.SelectedIndex > 0 And ddlProducto.SelectedIndex > 0 And ddlPoliza.SelectedIndex > 0 Then
                Dim periodo As Integer
                Dim ciclo As Integer
                Dim vPaso As Int16
                Dim ESTADO As String = ""

                Try

                    txtAño.Text = ""
                    txtCabecera.Text = ""
                    txtComentario.Text = ""
                    ddlMeses.SelectedIndex = 0

                    If ddlCiclo.SelectedIndex = 0 Then
                        If ddlCiclo.Items.Count > 1 Then
                            PanelCierre.Visible = True
                            ModalMensajeCierre.Show()
                        Else
                            'Validammos PR EN OTRA  PC
                            dtConsulta = Metodos.Consulta_Poliza_En_Proceso(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Session("Usuario"))
                            If dtConsulta.Rows(0).Item(3) <> "NONE" Then
                                If dtConsulta.Rows(0).Item(5) = 0 Then
                                    'SI ESTA DISPONIBLE
                                    Call inicia()
                                    imbAceptar1.Enabled = False
                                    txtFechaEfecto.ReadOnly = False
                                Else
                                    'VALIDAMOS SI ES EL MISMO USUARIO Y ESTA TOMADO EL LO RETOMA
                                    If Session("Usuario") = dtConsulta.Rows(0).Item(3).ToString Then

                                        Call inicia()
                                        imbAceptar1.Enabled = False
                                        txtFechaEfecto.ReadOnly = False
                                        'CONTINUAR
                                        'MENSAJE SI DESEA LIBERARLO POR QUE LO TOMO OTRO USUARIO
                                    Else
                                        'ALERT SI O NO
                                        PanelConsulta.Visible = True
                                        lblNroPoliza.Text = ddlPoliza.SelectedValue
                                        lblUsuario.Text = dtConsulta.Rows(0).Item(3).ToString
                                        ModalMensajeConsulta.Show()
                                    End If
                                End If
                            Else
                                'Seguimiento de la poliza para evitar sea tomada en otro proceso
                                Metodos.Control_Procesos(Metodos.DB.ORACLE, 1, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Session("Usuario"), ".")
                                'PROCEDE NORMAL NO HAY IMPEDIMENTO NADIE LO TOMO
                                Call inicia()
                                imbAceptar1.Enabled = False
                                txtFechaEfecto.ReadOnly = False
                            End If
                        End If
                        txtAño.Text = Date.Now.Year
                        ddlMeses.SelectedValue = Date.Now.Month.ToString.PadLeft(2, "0")
                    Else
                        MultiView1.Visible = True
                        ddlCanal.Enabled = False
                        ddlProducto.Enabled = False
                        ddlPoliza.Enabled = False
                        ddlCiclo.Enabled = False

                        Dim X As Int16 = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
                        Dim Z As Int16 = ddlCiclo.Text.Length
                        Dim VCICLO As Integer = Z - X

                        periodo = ddlCiclo.Text.Substring(0, 6)
                        ciclo = ddlCiclo.Text.Substring(7, VCICLO)
                        dtformato = Metodos.Lista_Datos_Formato(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, periodo, ciclo)
                        Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue) = dtformato
                        dtArchivo = Metodos.Lista_Datos_Archivo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, ddlCiclo.Text.Substring(0, 4) & ddlCiclo.Text.Substring(4, 2))
                        Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue) = dtArchivo

                        'Seguimiento de la poliza para evitar sea tomada en otro proceso
                        Metodos.Control_Procesos(Metodos.DB.ORACLE, 1, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Session("Usuario"), "")


                        ESTADO = dtformato.Tables(2).Rows(0).Item("ESTADO").ToString
                        Select Case ESTADO
                            Case "R"
                                vPaso = 1
                                txtTitulo.Text = "Carga de Archivo"
                                txtRuta.Text = dtformato.Tables(2).Rows(0).Item("ARCHIVO_ORIGEN")
                                txtAño.Text = ddlCiclo.Text.Substring(0, 4)
                                ddlMeses.SelectedValue = ddlCiclo.Text.Substring(4, 2)
                                txtCabecera.Text = dtformato.Tables(2).Rows(0).Item("NUM_REG_SKIP")
                                txtComentario.Text = dtformato.Tables(2).Rows(0).Item("DES_ARCHIVO")
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
                                txtReg2.Text = Metodos.Lista_Tabla_Canal(Metodos.DB.ORACLE, vtabla, ddlPoliza.SelectedValue).Compute("count(SIDDOC)", "")
                                txtAño2.ReadOnly = True
                                txtMes2.ReadOnly = True
                                txtCiclo2.ReadOnly = True
                                txtReg2.ReadOnly = True
                                btnProcesarPaso1.Enabled = False
                            Case "G"
                                vPaso = 3
                                txtTitulo.Text = "Generación"
                                txtAño3.Text = ddlCiclo.Text.Substring(0, 4)
                                ddlMeses.SelectedValue = ddlCiclo.Text.Substring(4, 2)
                                txtMes3.Text = ddlMeses.SelectedItem.Text
                                txtCiclo3.Text = ddlCiclo.Text
                                txtReg3.Text = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue).Compute("count(NCERTIF)", "")
                                txtAño3.ReadOnly = True
                                txtMes2.ReadOnly = True
                                txtCiclo3.ReadOnly = True
                                txtReg3.ReadOnly = True
                                btnProcesarPaso3.Enabled = False
                            Case "L"
                                vPaso = 4
                                txtTitulo.Text = "Carga colectiva"
                                txtAño4.Text = ddlCiclo.Text.Substring(0, 4)
                                ddlMeses.SelectedValue = ddlCiclo.Text.Substring(4, 2)
                                txtMes4.Text = ddlMeses.SelectedItem.Text
                                txtCiclo4.Text = ddlCiclo.Text
                                txtReg4.Text = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue).Compute("count(NCERTIF)", "")
                                txtAño4.ReadOnly = True
                                txtMes4.ReadOnly = True
                                txtCiclo4.ReadOnly = True
                                txtReg4.ReadOnly = True
                                btnProcesarPaso4.Enabled = False
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
                        If dtformato.Tables(0).Rows(0).Item("TIPO_CARGA") = "C" Then
                            HF_TIPO.Value = "C"
                            Call EstadoPasosNext(vPaso, HF_TIPO.Value) 'COLECTIVA
                            MultiView1.ActiveViewIndex = vPaso - 1
                        Else
                            HF_TIPO.Value = "M"
                            Call EstadoPasosNext(vPaso, HF_TIPO.Value)
                            MultiView1.ActiveViewIndex = vPaso - 1
                        End If
                        imbAceptar1.Enabled = False
                        txtFechaEfecto.ReadOnly = False
                    End If

                    FileUpload1.Focus()
                Catch ex As Exception
                    AlertaScripts("No se pudo cargar los datos de carga de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
                End Try

            Else
                AlertaScripts("Debe seleccionar CANAL, PRODUCTO y PÓLIZA para seguir con la carga de Carga Nro " & HF_NUMPAGINA.Value.ToString() & ".")
            End If


        Catch ex As Exception

        End Try


    End Sub

    Public Sub Logout()
        Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        AlertaScripts("evento logout")
    End Sub

    Protected Sub Page_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        AlertaScripts("evento logout")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then

                FileUpload1.Attributes.Add("onChange", "Seleccionar(this);")

                Call EstadoPasosNext(0, HF_TIPO.ToString)
                Call CargarCombos()
                HF_SERVER.Value = ConfigurationManager.AppSettings("Server").ToString()
                HF_RUTA_MASIVA.Value = ConfigurationManager.AppSettings("RutaMasiva").ToString()
                HF_RUTA_CARGA.Value = ConfigurationManager.AppSettings("RutaCarga").ToString()
                HF_RUTA_SQL.Value = ConfigurationManager.AppSettings("RutaSql").ToString()
                HF_RUTA_TEMP.Value = ConfigurationManager.AppSettings("RutaTemp").ToString()
                HF_RUTA_EJECUTABLE.Value = ConfigurationManager.AppSettings("RutaEjecutables").ToString()
                HF_PASSINT.Value = ConfigurationManager.AppSettings("PasswordUserInt").ToString()
                HF_SERVER_SECURITY.Value = ConfigurationManager.AppSettings("ServerSecurity").ToString()
                HF_NUMPAGINA.Value = Request.QueryString("nuevapag")
                PanelCierre.Visible = False
                PnelCarga.Visible = False
            End If

        Catch ex As Exception
            AlertaScripts("No es posible cargar datos iniciales de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Private Sub CargarCombos()
        dtCombos = Metodos.Lista_Datos_Combos(Metodos.DB.ORACLE)

        With ddlCanal
            .DataSource = dtCombos.Tables(0)
            .DataTextField = "DES_CANAL"
            .DataValueField = "CANAL"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlCanal)

        ddlProducto.Enabled = False
        ddlPoliza.Enabled = False
        ddlCiclo.Enabled = False
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

        With ddlCiclo
            .DataSource = dtCombos.Tables(3)
            .DataTextField = "CICLO"
            .DataValueField = "CICLO"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlCiclo)

        ddlCanal.SelectedIndex = 0
        ddlPoliza.SelectedIndex = 0
        ddlProducto.SelectedIndex = 0
        ddlCiclo.SelectedIndex = 0
    End Sub
    Private Sub CargarCombosCiclo()
        dtCombos = Metodos.Lista_Datos_Combos(Metodos.DB.ORACLE)

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
        ddlPoliza.Enabled = Not ddlPoliza.Enabled
        ddlCiclo.Enabled = Not ddlCiclo.Enabled
    End Sub
    Private Sub EstadoPasosNext(ByVal paso As Int16, ByVal tipo As String)
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
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4r.png"
                End If
            Case 2
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2a.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4r.png"
                End If
            Case 3
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3a.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4r.png"
                End If
            Case 4
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4a.png"
                End If
            Case 5
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4v.png"
                End If
        End Select
    End Sub
    Private Sub EstadoPasosProc(ByVal paso As Int16, ByVal tipo As String)
        Select Case paso
            Case 1
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2r.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4r.png"
                End If
            Case 2
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3r.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4r.png"
                End If
            Case 3
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4r.png"
                End If
            Case 4
                Image1.Visible = True
                Image1.ImageUrl = "~\Imagenes\b1v.png"

                Image2.Visible = True
                Image2.ImageUrl = "~\Imagenes\b2v.png"

                Image3.Visible = True
                Image3.ImageUrl = "~\Imagenes\b3v.png"
                If tipo = "C" Then
                    Image4.Visible = True
                    Image4.ImageUrl = "~\Imagenes\b4v.png"
                End If


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
            AlertaScripts("Tiene que procesar para ir al siguiente paso en Carga Nº " & HF_NUMPAGINA.Value.ToString)
            Exit Sub
        End If


        Try

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)

            Dim dt As DataTable
            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            dt = Metodos.Lista_Tabla_Canal(Metodos.DB.ORACLE, vtabla, ddlPoliza.SelectedValue)
            Dim vCantCorrec As Int64 = dt.Compute("count(NUM_REGISTRO)", "")

            If (CantRegArchivo() - txtCabecera.Text) - vCantCorrec > 0 Then
                AlertaScripts("No han pasado todos los registros. Verifique en el segundo paso de validación en Carga Nº " & HF_NUMPAGINA.Value.ToString & ".")
            End If

            btnErroresPaso1.Enabled = False
            btnCorrectosPaso1.Enabled = False
            btnResumenPaso1.Enabled = False
            txtAño2.Text = txtAño.Text
            txtMes2.Text = ddlMeses.SelectedItem.Text
            txtCiclo2.Text = dtArchivo.Rows(0).Item("Periodo").ToString & dtArchivo.Rows(0).Item("Ciclo").ToString

            txtReg2.Text = vCantCorrec
            If txtReg2.Text = "0" Then
                AlertaScripts("No se pudo cargar los datos en Carga Nro " & HF_NUMPAGINA.Value.ToString)
                btnProcesar.Enabled = True
            Else
                MultiView1.ActiveViewIndex = 1
                Call EstadoPasosNext(2, HF_TIPO.ToString)
                txtTitulo.Text = "Validación y Transferencia"
            End If

            Dim dtc As DataSet = Metodos.Lista_Conf_Remmp_Renov(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue)
            Session.Add("CONF_REMMP_RENOV", dtc)
            Dim fila() As DataRow = dtc.Tables(2).Select("")
            If fila.Length > 0 Then
                If (fila(0)(1).ToString = "S" And fila(0)(4).ToString = "S") Or (fila(0)(1).ToString = "S" And fila(0)(4).ToString = "") Then ' si es solo reemplazo o reemplazo y carga
                    Dim DTf As DataTable = Metodos.Lista_Tabla_fechas_policy(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue)
                    Session("FECHAS_POLICY") = DTf
                    txtFechaDef.Text = DTf.Rows(0).Item(0).ToString
                    lblFechaEfecto.Visible = True
                    txtFechaDef.Visible = True
                    RadioButtonList2.Enabled = True
                    RadioButtonList2.Visible = True

                    'MODIFICACION
                    RadioButtonList2.Items(0).Selected = False
                    RadioButtonList2.Items(1).Selected = False
                    If fila(0)(1).ToString = "S" And fila(0)(4).ToString = "" Then '  si es reemplazo
                        RadioButtonList2.Items(0).Selected = True
                        RadioButtonList2.Enabled = False
                        RadioButtonList2.Visible = True
                    End If
                    'FIN

                ElseIf fila(0)(1).ToString = "" And fila(0)(4).ToString = "S" Then ' sie secarga solamente
                    RadioButtonList2.Items(1).Selected = True
                    RadioButtonList2.Enabled = False
                    RadioButtonList2.Visible = True

                Else
                    lblFechaEfecto.Visible = False
                    txtFechaDef.Visible = False
                    RadioButtonList1.Visible = False
                    RadioButtonList2.Visible = False
                End If
            Else
                lblFechaEfecto.Visible = False
                txtFechaDef.Visible = False
                RadioButtonList1.Visible = False
                RadioButtonList2.Visible = False
            End If


            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSiguientePaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguientePaso1.Click

        If btnErroresPaso1.Enabled = False Then
            AlertaScripts("Tiene que procesar para ir al siguiente paso en Carga Nro " & HF_NUMPAGINA.Value.ToString())
            Exit Sub
        End If

        Try

            btnRegxModPaso3.Enabled = False
            txtAño3.Text = txtAño2.Text
            txtMes3.Text = txtMes2.Text
            txtCiclo3.Text = txtCiclo2.Text
            txtReg3.Text = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue).Compute("count(NUM_REGISTRO)", "")

            If RadioButtonList2.Visible = True Then
                If RadioButtonList2.Items(0).Selected = True Then
                    RadioButtonList1.Items(0).Selected = True
                Else
                    RadioButtonList1.Items(1).Selected = True
                End If
            End If
            RadioButtonList1.Enabled = False
            If txtReg3.Text = "0" Then
                AlertaScripts("No se pudo cargar los datos. Revise el reporte de errores de la Carga Nro " & HF_NUMPAGINA.Value.ToString() & ".")
                btnProcesarPaso1.Enabled = True
            Else
                MultiView1.ActiveViewIndex = 2
                Call EstadoPasosNext(3, HF_TIPO.ToString)
                txtTitulo.Text = "Generación"
            End If



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSiguientePaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguientePaso3.Click
        If HF_TIPO.Value = "M" Then
            AlertaScripts("No es posible acceder al siguiente paso por no tratarse de una carga colectiva en Carga Nro " & HF_NUMPAGINA.Value.ToString())
            Exit Sub
        End If
        If btnRegxModPaso3.Enabled = False Then
            AlertaScripts("Tiene que procesar para ir al siguiente paso en Carga Nro " & HF_NUMPAGINA.Value.ToString())
            Exit Sub
        End If
        gvProcesos.DataSource = Nothing
        gvProcesos.DataBind()
        MultiView1.ActiveViewIndex = 3
        Call EstadoPasosNext(4, HF_TIPO.Value)
        btnRepFinalPaso4.Enabled = False


        txtAño4.Text = txtAño3.Text
        txtMes4.Text = txtMes3.Text
        txtCiclo4.Text = txtCiclo3.Text
        txtTitulo.Text = "Carga colectiva"
        txtFechaEfecto.Text = Date.Now.ToString

    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

        Try
            Dim pos As Integer = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
            Dim ciclo As Integer = Mid(ddlCiclo.Text, pos + 1, ddlCiclo.Text.Length)
            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño.Text.Trim & ddlMeses.Text, ciclo)
            MultiView1.Visible = False
            Call CargarCombos()
            Call EstadoCombos()
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo en Carga Nro " & HF_NUMPAGINA.Value.ToString)
        End Try


    End Sub

    Protected Sub btnCerrarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarPaso1.Click

        Try
            Dim pos As Integer = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
            Dim ciclo As Integer = Mid(ddlCiclo.Text, pos + 1, ddlCiclo.Text.Length)
            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño.Text.Trim & ddlMeses.Text, ciclo)
            MultiView1.Visible = False
            Call EstadoCombos()
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Protected Sub btnCerrarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarPaso3.Click
        Try
            Dim pos As Integer = InStr(ddlCiclo.Text, "-", CompareMethod.Text)
            Dim ciclo As Integer = Mid(ddlCiclo.Text, pos + 1, ddlCiclo.Text.Length)
            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño.Text.Trim & ddlMeses.Text, ciclo)
            MultiView1.Visible = False
            Call EstadoCombos()
        Catch ex As Exception
            AlertaScripts("No es posible Cerrar el ciclo en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub


    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim RutaBat As String = ""
        Dim sPeriodo As String = ""
        Dim sFormato As String = ""
        Dim vComplemento As String
        Dim sCiclo As Integer = 0
        Dim sSecPol As Integer = 0
        Dim bProcede As Boolean = False

        Try

            If FileUpload1.PostedFile.FileName.ToString <> "" And txtCabecera.Text.Trim <> "" Then
                HF_CADENA.Value = ""
                Dim sArchivoOrigen As String = FileUpload1.PostedFile.FileName
                Metodos.Abre_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, txtAño.Text.Trim & ddlMeses.Text, txtComentario.Text.Trim, sArchivoOrigen, txtCabecera.Text, txtComentario.Text.Trim)
                System.Threading.Thread.Sleep(500)
                dtArchivo = Metodos.Lista_Datos_Archivo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, txtAño.Text.Trim & ddlMeses.Text)
                sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
                sSecPol = dtArchivo.Rows(0).Item("SEC_POLIZA").ToString
                sNroArchivo = dtArchivo.Rows(0).Item("Num_Archivo").ToString

                dtformato = Metodos.Lista_Datos_Formato(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, txtAño.Text.Trim & ddlMeses.Text, sCiclo)



                Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue) = dtArchivo
                Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue) = dtformato

                Try
                    HF_TABLA.Value = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
                    HF_CICLO.Value = dtArchivo.Rows(0).Item("CICLO").ToString
                    HF_ARCHIVO.Value = dtArchivo.Rows(0).Item("NUM_ARCHIVO").ToString
                Catch ex As Exception

                End Try

                sFormato = dtformato.Tables(0).Rows(0).Item("FORMATO").ToString
                RutaBat = Server.MapPath("").Replace("\", "/")
                Dim ourFileGen As FileInfo = New FileInfo(RutaBat & "/bat/" & ddlCanal.SelectedValue & "POL" & ddlPoliza.SelectedValue & ".bat")
                If ourFileGen.Exists Then
                    My.Computer.FileSystem.DeleteFile(RutaBat & "/bat/" & ddlCanal.SelectedValue & "POL" & ddlPoliza.SelectedValue & ".bat")
                End If
                sPeriodo = txtAño.Text.Trim & ddlMeses.SelectedValue & "-" & sCiclo.ToString.PadLeft(3, "0")
                Call CreaDirectorioTrabajo(sPeriodo)

                Dim sRutaDestino As String = HF_RUTA_MASIVA.Value & ddlCanal.Text & "/PROD" & ddlProducto.Text & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".TXT"

                FileUpload1.SaveAs(sRutaDestino)

                Dim vDelimitador As String = "" & dtformato.Tables(0).Rows(0).Item("Delimitador").ToString
                Dim vTipoFormato As String = dtformato.Tables(0).Rows(0).Item("Tip_formato").ToString
                ''--------------------------------------------------------

                Dim numlinea As Integer = 0
                Dim sRutaTemp As String = HF_RUTA_MASIVA.Value & ddlCanal.Text & "/PROD" & ddlProducto.Text & "/" & sPeriodo & "/"
                Dim icont As Integer = 0
                Dim Inicio As Integer = 0
                Dim Fin As Integer = 0
                Dim indexColumna As Integer = 0

                For Each fila As DataRow In dtformato.Tables(3).Select("")
                    icont = icont + 1

                    If fila("COLUMNA") = "NPOLICY" And fila("CAMPO_REQUERIDO") = "N" Then
                        vComplemento = ""
                        Exit For
                    ElseIf fila("COLUMNA") = "NPOLICY" And fila("CAMPO_REQUERIDO") = "S" Then
                        If vTipoFormato = "F" Then
                            vComplemento = " POSITION (" & fila("INICIO") & ":" & fila("FIN") & ") CHAR"
                            Inicio = Int32.Parse(fila("INICIO"))
                            Fin = Int32.Parse(fila("FIN"))
                            bProcede = True
                            Exit For
                        Else
                            indexColumna = icont
                            vComplemento = ""
                            bProcede = True
                            Exit For
                        End If

                    ElseIf fila("COLUMNA") = "POLICY" And fila("CAMPO_REQUERIDO") = "S" Then
                        If vTipoFormato = "F" Then
                            vComplemento = " POSITION (" & fila("INICIO") & ":" & fila("FIN") & ") CHAR"
                            Inicio = Int32.Parse(fila("INICIO"))
                            Fin = Int32.Parse(fila("FIN"))
                            bProcede = True
                            Exit For
                        Else
                            indexColumna = icont
                            vComplemento = ""
                            bProcede = True
                            Exit For
                        End If
                    Else
                        bProcede = False
                        If vTipoFormato = "F" Then
                            vComplemento = " POSITION (" & fila("INICIO") & ":" & fila("FIN") & ") CHAR"
                            Inicio = Int32.Parse(fila("INICIO"))
                            Fin = Int32.Parse(fila("FIN"))
                            Exit For
                        Else
                            indexColumna = icont
                            vComplemento = ""
                            Exit For
                        End If
                    End If
                Next

                If bProcede Then
                    Using fileWrite As New StreamWriter(sRutaTemp + "temp.txt", True, System.Text.Encoding.Default)

                        Using fielRead As New StreamReader(sRutaDestino, System.Text.Encoding.Default)

                            Dim line As String = fielRead.ReadLine

                            Do While (Not line Is Nothing)
                                'CON DELIMITADOR COMA
                                If vDelimitador = "," Then
                                    Dim cad As String = ""
                                    Dim cade As String
                                    Dim cad1 As String = ""
                                    Dim car As String
                                    Dim pos As Integer
                                    Dim posicion As Integer
                                    Dim iniciando As Integer
                                    Dim contadr As Integer = 0
                                    Dim i As Integer = 0
                                    HF_CADENA.Value = line
                                    cad = line
                                    car = ","
                                    HF_CADENA.Value = line

                                    For i = 1 To Len(cad) Step 1
                                        cad1 = ""
                                        cade = ""
                                        pos = InStr(cad, car)
                                        contadr = contadr + 1
                                        posicion = InStr(HF_CADENA.Value, car)
                                        cade = cade & Mid(HF_CADENA.Value, 1, posicion)
                                        If contadr = indexColumna Then
                                            cad1 = Mid(cade, 1, Len(cade) - Len(Mid(HF_CADENA.Value, 1, posicion)))
                                            cad1 = cad1 & Trim(ddlPoliza.SelectedValue.ToString()).PadLeft(10, "0"c)
                                            cad1 = cad1 & Mid(HF_CADENA.Value, iniciando + Len(Mid(HF_CADENA.Value, 1, posicion)))
                                            Exit For
                                        End If
                                        HF_CADENA.Value = Mid(HF_CADENA.Value, posicion + 1)
                                    Next
                                    line = cad1
                                Else
                                    'Sin delimitador
                                    line = line.Remove(Inicio - 1, Fin).Insert(Inicio - 1, Trim(ddlPoliza.SelectedValue.ToString()).PadLeft(10, "0"c))
                                End If

                                Dim datos As String() = line.Split(New Char() {","c})
                                Dim partes As String() = line.Split("&"c)
                                fileWrite.WriteLine(String.Join("&", partes))

                                line = fielRead.ReadLine
                            Loop

                        End Using
                    End Using
                    'aqui se renombrea el archivo temporal
                    File.Delete(sRutaDestino)
                    File.Move(sRutaTemp + "temp.txt", sRutaDestino)

                End If
                ''--------------------------------------------------------

                HF_RUTA_ARCHIVO.Value = sRutaDestino
                'Elimina archivos
                Dim ourFile As FileInfo = New FileInfo(HF_RUTA_EJECUTABLE.Value & "SALIDA.CTL")
                If ourFile.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_EJECUTABLE.Value & "SALIDA.CTL")
                End If

                Dim ourFileCTL As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL")
                If ourFileCTL.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL")
                End If

                Dim ourFileLOG As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG")
                If ourFileLOG.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG")
                End If

                Dim ourFileBAD As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".BAD")
                If ourFileBAD.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".BAD")
                End If

                Dim ourFileDSC As FileInfo = New FileInfo(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".DSC")
                If ourFileDSC.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".DSC")
                End If

                Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".txt")
                If ourFiletxt.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".txt")
                End If
                'CREA ARCHIVO .CTL Y .BAT 
                Call CreaArchivoCTL(sPeriodo, sSecPol)
                System.Threading.Thread.Sleep(1000)

                'ELIMINAR LAS POLIZAS DEL CASO ACTUAL
                Metodos.ELIMINA_POLIZA_PR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlPoliza.SelectedValue)

                Call CreaBat(sPeriodo, sSecPol)
                'borra tablas asociadas
                Metodos.Elimina_Archivo_Asociados(Metodos.DB.ORACLE, sNroArchivo)
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

                Call EstadoPasosProc(1, HF_TIPO.Value)
                Call VerificaProceso()
                Metodos.Archivo_Estado(Metodos.DB.ORACLE, sNroArchivo, "R")
                AlertaScripts("Proceso concluido en Carga Nº " & HF_NUMPAGINA.Value.ToString)
            Else
                If FileUpload1.PostedFile.FileName.ToString = "" Then
                    AlertaScripts("Seleccione un archivo en Carga Nº " & HF_NUMPAGINA.Value.ToString)
                ElseIf txtCabecera.Text.Trim = "" Then
                    AlertaScripts("Indicar número de cabecera en Carga Nº " & HF_NUMPAGINA.Value.ToString)
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
        Catch ex As Exception
            AlertaScripts("No se pudo Procesar en Carga Nº " & HF_NUMPAGINA.Value.ToString)
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            theFileBat = File.Open(ruta_Bat & "\bat\error1.txt", FileMode.OpenOrCreate, FileAccess.Write)
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
        pro.StartInfo.FileName = ddlCanal.SelectedValue & "POL" & ddlPoliza.SelectedValue & ".bat"
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

            ruta = HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "\PROD" & ddlProducto.SelectedValue & "\" & Periodo & "\POL" & ddlPoliza.SelectedValue & "-" & SecPol.ToString.PadLeft(2, "0")

            Dim s As New System.Text.StringBuilder
            s.AppendLine("@echo off")
            s.AppendLine("sqlldr " & vUserBD & "/" & vPasswordBD & "@" & vBaseDatos & " control=" & ruta & ".CTL")
            s.AppendLine("move POL" & ddlPoliza.SelectedValue & "-" & SecPol.ToString.PadLeft(2, "0") & ".LOG  " & ruta & ".LOG")
            tex = s.ToString

            Dim theFileBat As FileStream
            ruta_Bat = Server.MapPath("")

            theFileBat = File.Open(ruta_Bat & "\bat\" & ddlCanal.SelectedValue & "POL" & ddlPoliza.SelectedValue & ".bat", FileMode.OpenOrCreate, FileAccess.Write)
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
        Dim bPolAdd As Boolean = False

        Try

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)

            Dim vSkip As Integer = dtArchivo.Rows(0).Item("num_reg_skip")
            Dim vArchivo As String = dtArchivo.Rows(0).Item("archivo")
            Dim vTabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim vTipoFormato As String = dtformato.Tables(0).Rows(0).Item("Tip_formato").ToString
            Dim vDelimitador As String = "" & dtformato.Tables(0).Rows(0).Item("Delimitador").ToString
            Dim vFormato As String = "" & dtformato.Tables(0).Rows(0).Item("Formato").ToString
            System.Threading.Thread.Sleep(500)


            str.AppendLine("OPTIONS ( SKIP=" & vSkip & ")")
            str.AppendLine("LOAD DATA")
            str.AppendLine("CHARACTERSET WE8MSWIN1252")
            str.AppendLine("INFILE '" & vArchivo & ".txt'")
            str.AppendLine("BADFILE '" & vArchivo & ".bad'")
            str.AppendLine("DISCARDFILE '" & vArchivo & ".dsc'")
            str.AppendLine("APPEND")
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
                If fila("COLUMNA") = "NPOLICY" And fila("CAMPO_REQUERIDO") = "N" Then
                    bPolAdd = True
                Else
                    str.AppendLine("   " & fila("COLUMNA") & vComplemento)
                    caracter = ","
                End If
            Next
            If bPolAdd Then
                str.AppendLine("   NUM_REGISTRO    ""seq_trx_in_" & vFormato & ".nextval""")
                str.AppendLine(",")
                str.AppendLine("   NPOLICY    """ & Trim(ddlPoliza.SelectedValue.ToString()).PadLeft(10, "0"c))
                str.AppendLine(""")")
            Else
                str.AppendLine(",")
                str.AppendLine("   NUM_REGISTRO    ""seq_trx_in_" & vFormato & ".nextval""")
                str.AppendLine(")")
            End If
            texto = str.ToString

            Dim theFile As FileStream
            rut = HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".CTL"


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
            If Directory.Exists(rUTA) = False Then
                di = Directory.CreateDirectory(rUTA)
            End If
        Catch ex As Exception
            AlertaScripts("No se pudo crear la carpeta de trabajo de la Carga Nro " & HF_NUMPAGINA.Value.ToString())

        End Try
    End Sub


    Protected Sub btnProcesarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso1.Click
        Try

            If RadioButtonList2.Visible = True Then
                If RadioButtonList2.Items(0).Selected = False And RadioButtonList2.Items(1).Selected = False Then
                    AlertaScripts("Debe seleccionar una opción de proceso en carga Nro " & HF_NUMPAGINA.Value.ToString())
                    Exit Sub
                End If
            End If
            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)
            Dim vConsistencia As String = dtformato.Tables(0).Rows(0).Item("CONSISTENCIA").ToString
            Dim vTranslada As String = dtformato.Tables(0).Rows(0).Item("TRASLADA_REG").ToString
            'inserta en la tabla de errores TBL_TRX_ERROR
            Dim NumArchivo As Integer = dtArchivo.Rows(0).Item("num_archivo")
            If RadioButtonList2.Visible = True Then
                Metodos.Inserta_errores(Metodos.DB.ORACLE, NumArchivo, vConsistencia, IIf(RadioButtonList2.Items(0).Selected = True, "R", "C"))
            Else
                Metodos.Inserta_errores(Metodos.DB.ORACLE, NumArchivo, vConsistencia)
            End If

            Call EstadoPasosProc(2, HF_TIPO.Value)

            'inserta en tabla tbl_trx_exclusion y tbl_trx_pol#poliza
            Metodos.Traslado_Registros(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), vTranslada)
            'ACTUALIZA ESTADO
            Metodos.Archivo_Estado(Metodos.DB.ORACLE, dtArchivo.Rows(0).Item("num_archivo"), "C")
            System.Threading.Thread.Sleep(500)

            AlertaScripts("Proceso concluido en Carga Nro " & HF_NUMPAGINA.Value.ToString())
            btnProcesarPaso1.Enabled = False
            btnErroresPaso1.Enabled = True
            btnCorrectosPaso1.Enabled = True
            btnResumenPaso1.Enabled = True

            If ddlCanal.SelectedValue = "AGROBANCO" Then 'agrobanco
                btnRptAgrobanco.Enabled = True
                BtnPermanencia.Enabled = True
            Else
                btnRptAgrobanco.Enabled = False
                BtnPermanencia.Enabled = False
            End If


    

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
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

    Protected Sub btnProcesarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso3.Click

        Dim dt As DataTable
        Try

            If RadioButtonList1.Visible = True Then

                PnelCarga.Visible = True
                If RadioButtonList1.Items(0).Selected = True Then
                    lblMensajeProceso.Text = "Este proceso cargará certificados nuevos y actualizará los existentes en Carga Nro " & HF_NUMPAGINA.Value.ToString() & ". ¿Desea de Procesar?"
                Else
                    'valida fecha de fin de vigencia
                    dt = Metodos.Lista_Fecha_Expiracion(Metodos.DB.ORACLE, 1, ddlProducto.SelectedValue, ddlPoliza.SelectedValue)
                    Dim fila() As DataRow = dt.Select("")
                    If fila.Length > 0 And fila(0)(0).ToString.Trim.Length > 0 Then
                        If CDate(fila(0)(0).ToString) < CDate(txtFechaDef.Text) Then
                            lblMensajeProceso.Text = "La fecha de Expiración de la póliza es menor que la fecha de efecto ingresada en Carga Nro " & HF_NUMPAGINA.Value.ToString() & ". verifique"
                            Exit Sub
                        End If
                    End If
                    lblMensajeProceso.Text = "Este proceso cargará certificados nuevos en Carga Nro " & HF_NUMPAGINA.Value.ToString() & ". ¿Desea Procesar?"
                End If
                mpeCarga.Show()
            Else
                Call Generacion()
            End If


            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
        Catch ex As Exception
            AlertaScripts("Ocurrio un error inesperado al procesar en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try

    End Sub

    Protected Sub btnProcesarPaso4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesarPaso4.Click


        Try
            Dim dtModulos As DataTable
            Dim sCiclo As Integer = 0
            Dim RutaBat As String = ""

            If IsDate(txtFechaEfecto.Text) = False Then
                AlertaScripts("Ingrese la fecha correctamente en Carga Nro " & HF_NUMPAGINA.Value.ToString())
                Exit Sub
            End If

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtModulos = Session("MODULOS_" & ddlPoliza.SelectedValue)

            sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sSecPol As String = dtArchivo.Rows(0).Item("SEC_POLIZA").ToString
            Dim sPeriodo As String = txtAño4.Text.Trim & ddlMeses.SelectedValue & "-" & sCiclo.ToString.PadLeft(3, "0")
            Dim sNroArchivo As String = dtArchivo.Rows(0).Item("Num_Archivo").ToString

            'crea bat para el sql*loader
            Call CreaBAT_Colectiva(sPeriodo, sSecPol)

            System.Threading.Thread.Sleep(1000)


            For Each fila As DataRow In dtModulos.Select("")
                'invoca procedure q crea job
                Metodos.Crea_Job(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño4.Text.Trim & ddlMeses.SelectedValue, sCiclo, fila("NMODULEC"), txtFechaEfecto.Text, sNroArchivo, ddlCanal.SelectedValue)
                'crea archivo .ctl para sql*loader
                Call CreaArchivoCTL_Paso4(fila("NMODULEC"))
                System.Threading.Thread.Sleep(1000)
                'SQL*LOADER y LO EJECUTA
                RutaBat = Server.MapPath("").Replace("\", "/")

                Call EjecutaHiloColectiva()

                System.Threading.Thread.Sleep(200)
                System.Threading.Thread.Sleep(200)
                System.Threading.Thread.Sleep(200)
                System.Threading.Thread.Sleep(200)
                System.Threading.Thread.Sleep(200)

                Dim ourFileCTL As FileInfo = New FileInfo(HF_RUTA_TEMP.Value & "TRXCCOLEC.CTL")
                If ourFileCTL.Exists Then
                    My.Computer.FileSystem.DeleteFile(HF_RUTA_TEMP.Value & "TRXCCOLEC.CTL")
                End If
                System.Threading.Thread.Sleep(500)
                Metodos.Actualiza_archivo_tarea(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño4.Text.Trim & ddlMeses.SelectedValue, sCiclo, fila("NMODULEC"), sNroArchivo, ddlCanal.SelectedValue)

            Next

            Dim dtArchTarea As DataTable = Metodos.Lista_Arch_Tarea(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño4.Text.Trim & ddlMeses.SelectedValue, sCiclo, 0, sNroArchivo, ddlCanal.SelectedValue)
            gvProcesos.DataSource = dtArchTarea
            gvProcesos.DataBind()

            Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño4.Text.Trim & ddlMeses.SelectedValue, sCiclo)

            AlertaScripts("Carga Completada en Carga Nro " & HF_NUMPAGINA.Value.ToString())
            btnRepFinalPaso4.Enabled = True
            btnProcesarPaso4.Enabled = False
            btnRptAgrobanco.Enabled = True
            BtnPermanencia.Enabled = True
            Call EstadoPasosProc(4, HF_TIPO.Value)
            txtFechaEfecto.ReadOnly = True
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
        Catch ex As Exception
            AlertaScripts(Err.Description)  '"No se pudo Procesar"
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            theFileBat = File.Open(ruta_Bat & "\bat\error4.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try
    End Sub
    Private Sub EjecutaHiloColectiva()
        Dim arrThreads(2) As Thread
        arrThreads(0) = New Thread(New ThreadStart(AddressOf CorreProcesoColectiva))
        arrThreads(0).Start()
    End Sub
    Private Sub CorreProcesoColectiva()
        Dim RutaBat As String = Server.MapPath("").Replace("\", "/")
        pro.StartInfo.WorkingDirectory = RutaBat & "/bat/"
        pro.StartInfo.FileName = ddlCanal.SelectedValue & "POL" & ddlPoliza.SelectedValue & "_CC.bat"
        pro.Start()
    End Sub
    Private Sub VerificaProcesoColectiva()
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

    Private Sub CreaBAT_Colectiva(ByVal Periodo As String, ByVal SecPol As String)
        Try


            Dim ruta_Bat As String
            Dim tex As String
            Dim vBaseDatos As String = ConfigurationManager.AppSettings("BD").ToString()
            Dim vPasswordBD As String = ConfigurationManager.AppSettings("PasswordBD").ToString()
            Dim vUserBD As String = ConfigurationManager.AppSettings("UserBD").ToString()
            Dim MAQUINA As String = Replace(HF_SERVER.Value, "\\", "")

            Dim s As New System.Text.StringBuilder
            s.AppendLine("@echo off")
            If UCase(HF_SERVER_SECURITY.Value) = "SI" Then
                s.AppendLine("NET USE Z: " & HF_SERVER.Value & "\CargaMasiva " & HF_PASSINT.Value & " /USER:" & MAQUINA & "\IUSR_INTEGRA /Y")
            Else
                s.AppendLine("NET USE Z: " & HF_SERVER.Value & "\CargaMasiva /Y")
            End If
            s.AppendLine("sqlldr " & vUserBD & "/" & vPasswordBD & "@" & vBaseDatos & " control=" & HF_RUTA_TEMP.Value & "TRXCCOLEC.CTL")
            s.AppendLine("move TRXCCOLEC.LOG  " & HF_RUTA_TEMP.Value & "TRXCCOLEC.LOG")
            tex = s.ToString

            Dim theFile As FileStream
            ruta_Bat = Server.MapPath("")
            theFile = File.Open(ruta_Bat & "\bat\" & ddlCanal.SelectedValue & "POL" & ddlPoliza.SelectedValue & "_CC.bat", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribe As StreamWriter = New StreamWriter(theFile)
            filaEscribe.WriteLine(tex)
            filaEscribe.Close()
            theFile.Close()
        Catch ex As Exception
            Dim theFileBat As FileStream
            Dim ruta_Bat As String = Server.MapPath("")
            theFileBat = File.Open(ruta_Bat & "\bat\creabat4.txt", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribebat As StreamWriter = New StreamWriter(theFileBat)
            filaEscribebat.WriteLine(ex.Message)
            filaEscribebat.Close()
            theFileBat.Close()
        End Try
    End Sub
    Protected Sub imbCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelar.Click
        Call cancelar(0)
    End Sub
    Public Sub LimpiaSeleccionTabCanceladoFinalizado()
        Try
            Dim ds As DataSet = New DataSet
            ds.ReadXml(HF_RUTA_MASIVA.Value & "/TabSel/SelTabPol.xml")

            Dim i As Integer = 0

            For i = 0 To ds.Tables("Carga").Rows.Count - 1
                If (ds.Tables("Carga").Rows(i)("Producto").ToString = ddlProducto.SelectedValue.ToString() And ds.Tables("Carga").Rows(i)("Poliza").ToString = ddlPoliza.SelectedValue.ToString()) Then
                    ds.Tables("Carga").Rows(i).Delete()
                    ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabSel/SelTabPol.xml")
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub cancelar(Optional ByRef tipo As Integer = 0)
        Try
            If tipo = 0 Then
                LimpiaSeleccionTabCanceladoFinalizado()
            End If
        Catch ex As Exception

        End Try
      

        MultiView1.Visible = False
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
        ddlCiclo.SelectedIndex = 0
        ddlCiclo.Enabled = False
        imbAceptar1.Enabled = True
        btnProcesar.Enabled = True
        btnProcesarPaso1.Enabled = True
        btnProcesarPaso3.Enabled = True
        btnProcesarPaso4.Enabled = True
        Call EstadoPasosNext(0, "M")
        txtTitulo.Text = ""
        LblCCarga.Text = ""
        LblCRenova.Text = ""
        LblTit.Text = ""
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
            ddlPoliza.SelectedIndex = 0
            ddlPoliza.Enabled = False
            ddlCiclo.SelectedIndex = 0
            ddlCiclo.Enabled = False

            LblCCarga.Text = ""
            LblCRenova.Text = ""
            LblTit.Text = ""


        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try

            If ddlProducto.SelectedIndex = 0 Then
                ddlPoliza.SelectedIndex = 0
                ddlPoliza.Enabled = False

                LblCCarga.Text = ""
                LblCRenova.Text = ""
                LblTit.Text = ""

            Else
                ddlPoliza.Enabled = True

                LblCCarga.Text = ""
                LblCRenova.Text = ""
                LblTit.Text = ""

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

    Public Sub AdicionaPolizaPorControl()
        Try
            'validamos que la poliza no este tomada en otro formulario
            Dim procede As Integer = 1
            'validamos si ya fue seleccionada la poliza
            Dim ds As DataSet = New DataSet
            ds.ReadXml(HF_RUTA_MASIVA.Value & "/TabSel/SelTabPol.xml")

            Dim valorrec As String = ""
            Dim i As Integer = 0

            For i = 0 To ds.Tables("Carga").Rows.Count - 1
                If (ds.Tables("Carga").Rows(i)("Producto").ToString = ddlProducto.SelectedValue.ToString() And ds.Tables("Carga").Rows(i)("Poliza").ToString = ddlPoliza.SelectedValue.ToString()) Then
                    AlertaScripts("Poliza ya esta seleccionada en Carga Nro " & ds.Tables("Carga").Rows(i)("NroVentana").ToString)
                    ddlPoliza.SelectedIndex = 0
                    procede = 0
                    Exit For
                End If
            Next

                'Inserta sin no existe
                If procede > 0 Then
                    Dim fila As DataRow
                    fila = ds.Tables("Carga").NewRow
                    fila("NroVentana") = HF_NUMPAGINA.Value
                    fila("Producto") = ddlProducto.SelectedValue
                    fila("Poliza") = ddlPoliza.SelectedValue
                    ds.Tables("Carga").Rows.Add(fila)
                    ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabSel/SelTabPol.xml")

                Else
                    Call cancelar(1)

            End If
              

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlPoliza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoliza.SelectedIndexChanged

        Try
           
            Call AdicionaPolizaPorControl()

            dtCombos = Session("dtCombos")
            Dim arr As New ArrayList
            For Each fila As DataRow In dtCombos.Tables(3).Select("CANAL='" & ddlCanal.SelectedValue & "' AND NPRODUCT=" & ddlProducto.SelectedValue & "")
                arr.Add(New Listado(fila("CICLO"), fila("CICLO")))
            Next

            With ddlCiclo
                .DataSource = arr
                .DataTextField = "Descripcion"
                .DataValueField = "Codigo"
                .DataBind()
            End With
            Global.Metodos.AgregarItemCombo(ddlCiclo)



            'MOSTRAR COMENTARIO  16/04/2015
            If ddlProducto.SelectedValue <> "" And ddlPoliza.SelectedValue <> "" Then
                dtCarga = Metodos.Lista_Comentario_Poliza(Metodos.DB.ORACLE, ddlProducto.SelectedValue, ddlPoliza.SelectedValue)
                If dtCarga.Tables(0).Rows.Count > 0 Then
                    LblTit.Text = "La trama de la póliza " + ddlPoliza.SelectedValue + " se procesa de la siguiente forma:"
                    LblCCarga.Text = dtCarga.Tables(0).Rows(0).Item(0).ToString()
                Else
                    LblCCarga.Text = ""
                End If

                If dtCarga.Tables(1).Rows.Count > 0 Then
                    LblTit.Text = "La trama de la póliza " + ddlPoliza.SelectedValue + " se procesa de la siguiente forma:"
                    LblCRenova.Text = dtCarga.Tables(1).Rows(0).Item(0).ToString()
                Else
                    LblCRenova.Text = ""
                End If
            Else
                LblCCarga.Text = ""
                LblCRenova.Text = ""
                LblTit.Text = ""
            End If

            ddlCiclo.Enabled = True


        Catch ex As Exception

        End Try

    End Sub

    Private Sub CreaArchivoCTL_Paso4(ByVal modulo As Integer)
        Dim texto As String
        Dim caracter As String = "("
        Dim caracter2 As String = """"

        Dim str As New System.Text.StringBuilder
        dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
        dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)


        Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
        Dim sNroArchivo As String = dtArchivo.Rows(0).Item("Num_Archivo").ToString

        Dim dtArchTarea As DataTable = Metodos.Lista_Arch_Tarea(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño4.Text.Trim & ddlMeses.SelectedValue, sCiclo, modulo, sNroArchivo, ddlCanal.SelectedValue)
        Dim vArchivo As String = dtArchivo.Rows(0).Item("archivo")
        Dim vDelimitador As String = "" & dtformato.Tables(0).Rows(0).Item("Delimitador").ToString

        Dim vSKEY As String = dtArchTarea.Rows(0).Item("SKEY_T").ToString
        Dim vSKEY2 As String = dtArchTarea.Rows(0).Item("SKEY_B").ToString
        Dim vSKEY3 As String = dtArchTarea.Rows(0).Item("SKEY_I").ToString
        Dim vCargaBeneficiario As String = "" & dtformato.Tables(1).Rows(0).Item("carga_beneficiario").ToString
        Dim vCargaIntermediario As String = "" & dtformato.Tables(1).Rows(0).Item("Carga_intermediario").ToString
        Dim vTipoCarga As String = "" & dtformato.Tables(0).Rows(0).Item("tipo_carga").ToString

        texto = ""
        If vTipoCarga = "C" Then
            str.AppendLine("LOAD DATA")
            str.AppendLine("CHARACTERSET WE8MSWIN1252")
            str.AppendLine("INFILE '" & HF_RUTA_CARGA.Value & "POL-" & ddlPoliza.SelectedValue & "-M" & modulo & ".TXT'")
            str.AppendLine("BADFILE '" & HF_RUTA_CARGA.Value & "POL-" & ddlPoliza.SelectedValue & "-M" & modulo & ".BAD'")
            str.AppendLine("DISCARDFILE '" & HF_RUTA_CARGA.Value & "POL-" & ddlPoliza.SelectedValue & "-M" & modulo & ".DSC'")
            str.AppendLine("APPEND")
            str.AppendLine("INTO TABLE ""T_MASIVECHARGE"" WHEN tab='1'")
            str.AppendLine("FIELDS TERMINATED BY "","" OPTIONALLY ENCLOSED BY '" & caracter2 & "' TRAILING NULLCOLS")    '"",""
            str.AppendLine("( tab FILLER CHAR(1) ,")
            str.AppendLine("  SKEY  CONSTANT '" & vSKEY & "',")
            str.AppendLine("  NROWS ,")
            str.AppendLine("  NCOLUMNS ,")
            str.AppendLine("  SFIELD ,")
            str.AppendLine("  SVALUE ,")
            str.AppendLine("  NSEARCH ,")
            str.AppendLine("  STABLE ,")
            str.AppendLine("  SVALUESLIST )")
            If vCargaBeneficiario = "S" Then
                str.AppendLine("INTO TABLE ""T_MASIVECHARGE"" WHEN tab='2'")
                str.AppendLine("FIELDS TERMINATED BY "","" OPTIONALLY ENCLOSED BY '" & caracter2 & "' TRAILING NULLCOLS")
                str.AppendLine("( tab FILLER POSITION (1:1) CHAR(1) ,")
                str.AppendLine("  coma filler char(1) ,")
                str.AppendLine("  SKEY  CONSTANT '" & vSKEY2 & "',")
                str.AppendLine("  NROWS ,")
                str.AppendLine("  NCOLUMNS ,")
                str.AppendLine("  SFIELD ,")
                str.AppendLine("  SVALUE ,")
                str.AppendLine("  NSEARCH ,")
                str.AppendLine("  STABLE ,")
                str.AppendLine("  SVALUESLIST )")
            End If
            texto = str.ToString

            Dim theFile As FileStream
            theFile = File.Open(HF_RUTA_TEMP.Value & "TRXCCOLEC.CTL", FileMode.OpenOrCreate, FileAccess.Write)
            Dim filaEscribe As StreamWriter = New StreamWriter(theFile)
            filaEscribe.WriteLine(texto)
            filaEscribe.Flush()
            filaEscribe.Close()
            filaEscribe.Dispose()
            theFile.Close()
        End If
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
            AlertaScripts("No es posible Cerrar el ciclo de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try

    End Sub


    Protected Sub imbAcepCierre_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAcepCierre.Click
        Call CerrarTodosCiclos()
        Call inicia()
        PanelCierre.Visible = False
        imbAceptar1.Enabled = False
        txtFechaEfecto.ReadOnly = False
    End Sub

    Protected Sub imbCancelCierre_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelCierre.Click
        ModalMensajeCierre.Hide()
        PanelCierre.Visible = False
    End Sub

    Function ValidaExpresion(ByVal patron As String, ByVal dato As String) As Boolean
        Try
            Dim reg As New Regex(patron)
            Return reg.IsMatch(dato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub btnRegCorrectos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegCorrectos.Click

        Try

            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)
            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)

            Dim dt As DataTable
            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sNumArchivo As Integer = dtArchivo.Rows(0).Item("NUM_ARCHIVO").ToString

            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNumArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & ""

            dt = Metodos.Lista_Report_Correctos(Metodos.DB.ORACLE, vtabla, ddlPoliza.SelectedValue)
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso1_Detallado.rpt"
            Session("NombreRPT") = "Detallado"
            Session("Arreglo") = x
            Session("Data") = "S"
            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString)
        End Try


    End Sub

    Protected Sub btnResTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResTotal.Click

        Try
            Dim dt As DataTable
            Dim vCantCorrec As Integer

            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)
            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)


            Dim vtabla As String = dtformato.Tables(0).Rows(0).Item("Tabla").ToString
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sNumArchivo As Integer = dtArchivo.Rows(0).Item("NUM_ARCHIVO").ToString

            dt = Metodos.Lista_Report_Correctos(Metodos.DB.ORACLE, vtabla, ddlPoliza.SelectedValue)
            vCantCorrec = dt.Compute("count(Numero)", "")
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso1_Resumido.rpt"
            Session("NombreRPT") = "Detallado"
            Session("Data") = "S" '"S" si se manda datatable al reporte si ni "N"
            Dim x(9) As String
            x(0) = "CANT_TOT;" & CantRegArchivo() - CInt(txtCabecera.Text)
            x(1) = "CANT_CORRECT;" & vCantCorrec
            x(2) = "NomEmpresa;Protecta Compañia de Seguros"
            x(3) = "Canal;" & ddlCanal.SelectedItem.Text
            x(4) = "Producto;" & ddlProducto.SelectedItem.Text
            x(5) = "Poliza;" & ddlPoliza.SelectedValue
            x(6) = "ciclo;" & txtAño.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(7) = "Archivo;" & sNumArchivo
            x(8) = "RutaArchivo;" & txtRuta.Text
            x(9) = "Titulo;" & ""

            Session("Arreglo") = x

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx','_blank');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())

        Catch ex As Exception
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString)
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
            Dim sCiclo As Integer
            Dim sNroArchivo As Integer
            Try
                'dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
                sCiclo = HF_CICLO.Value
                sNroArchivo = HF_ARCHIVO.Value
            Catch ex As Exception

            End Try


            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & ""

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
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Protected Sub btnCorrectosPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCorrectosPaso1.Click
        Try
            Dim dt As DataTable
            Dim sCiclo As Integer
            Dim sNroArchivo As Integer
            Try
                sCiclo = HF_CICLO.Value
                sNroArchivo = HF_ARCHIVO.Value
            Catch ex As Exception

            End Try
            Dim x(7) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & ""

            dt = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue)

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
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Protected Sub btnResumenPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResumenPaso1.Click
        Dim dt As DataTable
        Dim x(7) As String

        Try

            Dim vTabla As String
            Dim sCiclo As Integer
            Dim vArchivo As String
            Try
                vTabla = HF_TABLA.Value
                sCiclo = HF_CICLO.Value
                vArchivo = HF_ARCHIVO.Value
            Catch ex As Exception

            End Try


            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & vArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & ""

            dt = Metodos.Lista_Report_Resumen(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, vTabla, vArchivo)

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
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try

    End Sub

    Protected Sub btnRegxModPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegxModPaso3.Click
        Dim dt As DataTable
        Dim x(7) As String

        Try

            Dim vArchivo As String
            Dim vTabla As String
            Dim sCiclo As Integer

            Try
                vTabla = HF_TABLA.Value
                sCiclo = HF_CICLO.Value
                vArchivo = HF_ARCHIVO.Value
            Catch ex As Exception

            End Try



            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño3.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & vArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & ""

            dt = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue)
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso3_Modulos.rpt"
            Session("NombreRPT") = "Modulos"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Protected Sub btnRepFinalPaso4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRepFinalPaso4.Click
        Dim dt As DataTable
        Dim x(6) As String

        Try

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo")
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = txtAño4.Text.Trim & ddlMeses.SelectedValue

            x(0) = "Canal;" & ddlCanal.SelectedItem.Text
            x(1) = "Producto;" & ddlProducto.SelectedItem.Text
            x(2) = "Poliza;" & ddlPoliza.SelectedValue
            x(3) = "ciclo;" & txtAño4.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(4) = "Archivo;" & vArchivo
            x(5) = "NomEmpresa;Protecta Compañia de Seguros"
            x(6) = "RutaArchivo;" & txtRuta.Text

            dt = Metodos.Lista_Arch_Tarea(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, sPeriodo, sCiclo, 0, vArchivo, ddlCanal.SelectedValue)

            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso4_ResumenTotal.rpt"
            Session("NombreRPT") = "Resumen"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Private Function TablaProblemasCargaInicial() As DataTable

        Try
            Dim dtr As DataRow
            Dim dt As New DataTable
            Dim errores As String = ""
            Dim exist As Integer = 0
            Dim exist2 As Integer = 0

            Dim dtArchivo As DataTable = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)


            Dim sCiclo As String = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = txtAño.Text.Trim & ddlMeses.SelectedValue & "-" & sCiclo.ToString.PadLeft(3, "0")
            Dim sSecPol As String = dtArchivo.Rows(0).Item("SEC_POLIZA").ToString
            Dim theFile As FileStream
            Dim ruta As String = HF_RUTA_MASIVA.Value & ddlCanal.SelectedValue & "/PROD" & ddlProducto.SelectedValue & "/" & sPeriodo & "/POL" & ddlPoliza.SelectedValue & "-" & sSecPol.ToString.PadLeft(2, "0") & ".LOG"

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
            AlertaScripts("No se pudo abrir el archivo log de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try

    End Function

    Protected Sub btnProblemas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProblemas.Click

        Dim x(7) As String

        Try

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)

            Dim vArchivo As String = dtArchivo.Rows(0).Item("num_archivo")
            Dim sCiclo As Integer = dtArchivo.Rows(0).Item("CICLO").ToString
            Dim sPeriodo As String = txtAño4.Text.Trim & ddlMeses.SelectedValue

            x(0) = "Canal;" & ddlCanal.SelectedItem.Text
            x(1) = "Producto;" & ddlProducto.SelectedItem.Text
            x(2) = "Poliza;" & ddlPoliza.SelectedValue
            x(3) = "ciclo;" & txtAño4.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(4) = "Archivo;" & vArchivo
            x(5) = "NomEmpresa;Protecta Compañia de Seguros"
            x(6) = "RutaArchivo;" & txtRuta.Text
            x(7) = "Titulo;" & ""

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
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try

    End Sub

    Protected Sub btnRptAgrobanco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRptAgrobanco.Click
        Try
            Dim dt As DataTable
            Dim vTabla As String
            Dim sCiclo As Integer
            Dim sNroArchivo As Integer
            Try
                vTabla = HF_TABLA.Value
                sCiclo = HF_CICLO.Value
                sNroArchivo = HF_ARCHIVO.Value
            Catch ex As Exception

            End Try

            Dim x(6) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text


            dt = Metodos.Lista_Report_Agrobanco(Metodos.DB.ORACLE, sNroArchivo, ddlPoliza.SelectedValue)

            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Paso4_ResumenAgrobanco.rpt"
            Session("NombreRPT") = "Resumen Agrobanco"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())
        Catch ex As Exception
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Protected Sub BtnPermanencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPermanencia.Click
        Try
            Dim sCiclo As Integer
            Dim sNroArchivo As Integer
            Try
                sCiclo = HF_CICLO.Value
                sNroArchivo = HF_ARCHIVO.Value
            Catch ex As Exception

            End Try
            Dim x(6) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "ciclo;" & txtAño2.Text & ddlMeses.SelectedValue & "-" & sCiclo
            x(5) = "Archivo;" & sNroArchivo
            x(6) = "RutaArchivo;" & txtRuta.Text

            Session("TABLA_REPORTE") = Metodos.Lista_Report_Agrobanco_Permanencia(Metodos.DB.ORACLE, sNroArchivo)
            Session("RPT") = "Paso4_ResumenAgrobancoPermanencia.rpt"
            Session("NombreRPT") = "Resumen Permanencia Agrobanco"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())

        Catch ex As Exception
            AlertaScripts("No se puede imprimir en Carga Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub
    Protected Sub imbCancelarCarga_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelarCarga.Click
        PnelCarga.Visible = False
        mpeCarga.Hide()
    End Sub

    Protected Sub imbAceptarCarga_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAceptarCarga.Click
        Call Remplazo_Nomina_90()
        PnelCarga.Visible = False
        mpeCarga.Hide()
    End Sub

    Private Sub Generacion()
        Try
            Dim DT As DataTable
            Dim sPeriodo As String = ""
            Dim sCiclo As Integer = 0

            btnRegxModPaso3.Enabled = True

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)


            Dim vTransformada As String = dtformato.Tables(0).Rows(0).Item("TRANSFORMADA").ToString

            sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
            'borra archivos
            Dim ourFilelst As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".LST")
            If ourFilelst.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".LST")
            End If
            Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".TXT")
            If ourFiletxt.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".TXT")
            End If
            'GUARDA DATOS PARA PASO 4 POR Q LIMPIA TABLA POL
            Session("TABLA_POL_" & ddlPoliza.SelectedValue) = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue)



            DT = Session("TABLA_POL_" & ddlPoliza.SelectedValue)
            txtReg4.Text = DT.Compute("count(NUM_REGISTRO)", "")
            Session("MODULOS") = Metodos.Lista_Modulo_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue)
            'genera archivo
            'genera txt
            Metodos.Archivo_Genera(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo, vTransformada)

            'cambia de estado a 'G'
            Metodos.Archivo_Estado_PASO3(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo, "G")

            If HF_TIPO.Value = "M" Then
                Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo)
                btnCerrarPaso3.Enabled = False
            End If

            AlertaScripts("Generación exitosa de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
            btnProcesarPaso3.Enabled = False
            Call EstadoPasosProc(3, HF_TIPO.Value)
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

    Private Sub Remplazo_Nomina_90()
        Try
            Dim DT As DataTable
            Dim sPeriodo As String = ""
            Dim sCiclo As Integer = 0
            Dim sKey As String
            Dim sEstadoJob As String

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)
            Dim vTransformada As String = dtformato.Tables(0).Rows(0).Item("TRANSFORMADA").ToString
            Dim DT_FECHAS As DataTable = Session("FECHAS_POLICY")

            If RadioButtonList1.Items(0).Selected = True And (ddlProducto.SelectedValue <> "93" _
               Or ddlProducto.SelectedValue <> "22" Or ddlProducto.SelectedValue <> "87" _
               Or ddlProducto.SelectedValue <> "88" Or ddlProducto.SelectedValue <> "103" _
               Or ddlProducto.SelectedValue <> "105") Then 'SI ES REEMPLAZO VALIDA FECHAS SI NO NO LO HACE
                If DT_FECHAS.Rows(0).Item(2).ToString <> "" Then

                    If RadioButtonList1.Visible = True And RadioButtonList1.Items(1).Selected = True Then
                        If CDate(txtFechaDef.Text) < CDate(DT_FECHAS.Rows(0).Item(0).ToString) Then
                            AlertaScripts("No puede colocar una fecha menor a la cargada por el sistema de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
                            Exit Sub
                        End If
                    ElseIf RadioButtonList1.Visible = True And RadioButtonList1.Items(0).Selected = True Then
                        If CDate(txtFechaDef.Text) <= CDate(DT_FECHAS.Rows(0).Item(0).ToString) Then
                            AlertaScripts("No puede colocar una fecha menor o igual a la cargada por el sistema de la carga Nro " & HF_NUMPAGINA.Value.ToString())
                            Exit Sub
                        End If
                    End If
                    If DT_FECHAS.Rows(0).Item(2).ToString <> "6" Then
                        If CDate(txtFechaDef.Text) > CDate(DT_FECHAS.Rows(0).Item(1).ToString) Then
                            AlertaScripts("No puede colocar una fecha mayor a la fecha de proxima emisión de recibo de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
                            Exit Sub
                        End If
                    End If
                End If
            End If
            btnRegxModPaso3.Enabled = True



            sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
            'borra archivos
            Dim ourFilelst As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".LST")
            If ourFilelst.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".LST")
            End If
            Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".TXT")
            If ourFiletxt.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".TXT")
            End If
            'GUARDA DATOS PARA PASO 4 POR Q LIMPIA TABLA POL
            Session("TABLA_POL") = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue)
            DT = Session("TABLA_POL")
            txtReg4.Text = DT.Compute("count(NUM_REGISTRO)", "")
            Session("MODULOS") = Metodos.Lista_Modulo_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue)
            'genera archivo

            Dim año As String = txtFechaDef.Text.Substring(6, 4)
            Dim mes As String = txtFechaDef.Text.Substring(3, 2)
            Dim dia As String = txtFechaDef.Text.Substring(0, 2)

            sEstadoJob = Metodos.Archivo_Genera_Tarea90(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, Session("CodUsuario"), año & mes & dia, vTransformada, IIf(RadioButtonList1.Items(0).Selected = True, "R", "C"))

            'cambia de estado a 'G'
            Metodos.Archivo_Estado_PASO3(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo, "G")


            If HF_TIPO.Value = "M" Then
                Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo)
                btnCerrarPaso3.Enabled = False
            End If

            If sEstadoJob = "5" Then
                AlertaScripts("Generación exitosa de la Carga Nro " & HF_NUMPAGINA.Value.ToString())

            Else
                AlertaScripts("Ocurrio un error al Ejecutar el Proceso  en Carga Nro " & HF_NUMPAGINA.Value.ToString())
            End If

            Metodos.Control_Procesos(Metodos.DB.ORACLE, 3, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Session("Usuario"), ".")

            'AlertaScripts("Generación exitosa de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
            btnProcesarPaso3.Enabled = False
            Call EstadoPasosProc(3, HF_TIPO.Value)
            'Ejecuta comando
            LimpiaSeleccionTabCanceladoFinalizado()
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

    Private Sub CargaMasiva()
        Try
            Dim DT As DataTable
            Dim sPeriodo As String = ""
            Dim sCiclo As Integer = 0

            Dim DT_FECHAS As DataTable = Session("FECHAS_POLICY")
            If DT_FECHAS.Rows(0).Item(2).ToString <> "" Then
                If ddlProducto.SelectedValue = 61 Then
                    If CDate(txtFechaDef.Text) < CDate(DT_FECHAS.Rows(0).Item(0).ToString) Then
                        AlertaScripts("No puede colocar una fecha menor o igual a la cargada por el sistema de la carga Nro " & HF_NUMPAGINA.Value.ToString())
                        Exit Sub
                    End If
                Else
                    If CDate(txtFechaDef.Text) <= CDate(DT_FECHAS.Rows(0).Item(0).ToString) Then
                        AlertaScripts("No puede colocar una fecha menor o igual a la cargada por el sistema de la carga Nro " & HF_NUMPAGINA.Value.ToString())
                        Exit Sub
                    End If
                End If
                If CDate(txtFechaDef.Text) > CDate(DT_FECHAS.Rows(0).Item(1).ToString) Then
                    AlertaScripts("No puede colocar una fecha mayor a la fecha de proxima emisión de recibo de la Carga Nro " & HF_NUMPAGINA.Value.ToString())
                    Exit Sub
                End If
            End If

            btnRegxModPaso3.Enabled = True

            dtArchivo = Session("DATOS_ARCHIVO_" & ddlPoliza.SelectedValue)
            dtformato = Session("DATOS_FORMATO_" & ddlPoliza.SelectedValue)
            Dim vTransformada As String = dtformato.Tables(0).Rows(0).Item("TRANSFORMADA").ToString

            sCiclo = dtArchivo.Rows(0).Item("CICLO").ToString
            'borra archivos
            Dim ourFilelst As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".LST")
            If ourFilelst.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".LST")
            End If
            Dim ourFiletxt As FileInfo = New FileInfo(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".TXT")
            If ourFiletxt.Exists Then
                My.Computer.FileSystem.DeleteFile(HF_RUTA_CARGA.Value & "POL" & ddlPoliza.SelectedValue & ".TXT")
            End If
            'GUARDA DATOS PARA PASO 4 POR Q LIMPIA TABLA POL
            Session("TABLA_POL") = Metodos.Lista_Tabla_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, ddlProducto.SelectedValue)
            DT = Session("TABLA_POL")
            txtReg4.Text = DT.Compute("count(NUM_REGISTRO)", "")
            Session("MODULOS") = Metodos.Lista_Modulo_Pol(Metodos.DB.ORACLE, ddlPoliza.SelectedValue)
            'genera archivo

            Dim año As String = txtFechaDef.Text.Substring(6, 4)
            Dim mes As String = txtFechaDef.Text.Substring(3, 2)
            Dim dia As String = txtFechaDef.Text.Substring(0, 2)
            Metodos.Archivo_Genera_Tarea(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, Session("CodUsuario"), año & mes & dia, vTransformada, IIf(RadioButtonList1.Items(0).Selected = True, "R", "C"))

            'cambia de estado a 'G'
            Metodos.Archivo_Estado_PASO3(Metodos.DB.ORACLE, ddlPoliza.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo, "G")

            If HF_TIPO.Value = "M" Then
                Metodos.Cierra_Ciclo(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, txtAño3.Text.Trim & ddlMeses.SelectedValue, sCiclo)
                btnCerrarPaso3.Enabled = False
            End If
            AlertaScripts("Generación exitosa")
            btnProcesarPaso3.Enabled = False
            Call EstadoPasosProc(3, HF_TIPO.Value)
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


    Protected Sub imbAceptarConsulta_Click(sender As Object, e As ImageClickEventArgs) Handles imbAceptarConsulta.Click
        Try
            Metodos.Control_Procesos(Metodos.DB.ORACLE, 2, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Session("Usuario"), "")
            Call inicia()
            imbAceptar1.Enabled = False
            txtFechaEfecto.ReadOnly = False

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub imbCancelarConsulta_Click(sender As Object, e As ImageClickEventArgs) Handles imbCancelarConsulta.Click

    End Sub


    Protected Sub ddlCiclo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCiclo.SelectedIndexChanged

    End Sub

End Class

