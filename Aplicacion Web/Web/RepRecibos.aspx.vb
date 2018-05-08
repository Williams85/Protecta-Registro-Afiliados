Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Data.OracleClient
Imports System.Drawing

Partial Class RepRecibos
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                lblTitulo.Text = "Avisos de Cobranza"
                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")

                Global.Metodos.AgregarItemCombo(ddlEstado)
                Call CargarCombos()
            End If
        Catch ex As Exception
            AlertaScripts("No es posible cargar datos iniciales")
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

        'If ddlEstado.Items.Count = 3 Then
        '    Global.Metodos.AgregarItemCombo(ddlEstado)
        'End If

        ddlProducto.Enabled = False
        ddlPoliza.Enabled = False
        ddlEstado.Enabled = False
        txtFecInicio.Enabled = False
        txtFecFin.Enabled = False
        imgCalendarIni.Enabled = False
        imgCalendarFin.Enabled = False
        Tab_ReporteRecibos.Visible = False
        ImprimirRecibos.Visible = False

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

        If ddlCanal.Items.Count = 2 Then
            ddlCanal.SelectedIndex = 1
            Call ddlCanal_SelectedIndexChanged(ddlCanal, System.EventArgs.Empty)
        Else
            ddlCanal.SelectedIndex = 0
        End If
        'ddlCanal.SelectedIndex = 0
        'ddlPoliza.SelectedIndex = 0
        ddlEstado.SelectedIndex = 0
    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Protected Sub ddlCanal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCanal.SelectedIndexChanged
        Try
            If ddlCanal.SelectedIndex = 0 Then
                ddlProducto.SelectedIndex = 0
                ddlProducto.Enabled = False
                ddlPoliza.SelectedIndex = 0
                ddlPoliza.Enabled = False
                ddlEstado.SelectedIndex = 0
                ddlEstado.Enabled = False
                txtFecInicio.Enabled = False
                txtFecInicio.Text = ""
                txtFecFin.Enabled = False
                txtFecFin.Text = ""
                imgCalendarIni.Enabled = False
                imgCalendarFin.Enabled = False
            Else

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

                If ddlProducto.Items.Count = 2 Then
                    ddlProducto.SelectedIndex = 1
                    Call ddlProducto_SelectedIndexChanged(ddlProducto, System.EventArgs.Empty)
                Else
                    ddlProducto.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try
            If ddlProducto.SelectedIndex = 0 Then
                ddlPoliza.SelectedIndex = 0
                ddlPoliza.Enabled = False
                ddlEstado.SelectedIndex = 0
                ddlEstado.Enabled = False
                txtFecInicio.Enabled = False
                txtFecInicio.Text = ""
                txtFecFin.Enabled = False
                txtFecFin.Text = ""
                imgCalendarIni.Enabled = False
                imgCalendarFin.Enabled = False
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

                imbAceptar1.Enabled = True

                txtFecInicio.Enabled = True
                txtFecFin.Enabled = True
                imgCalendarIni.Enabled = True
                imgCalendarFin.Enabled = True
                ddlEstado.Enabled = True
                txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                txtFecFin.Text = Date.Now.ToString

                If ddlPoliza.Items.Count = 2 Then
                    ddlPoliza.SelectedIndex = 1
                    Call ddlPoliza_SelectedIndexChanged(ddlProducto, System.EventArgs.Empty)
                Else
                    ddlPoliza.SelectedIndex = 0
                    
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlPoliza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoliza.SelectedIndexChanged
        'If ddlPoliza.SelectedIndex <> 0 Then
        '    txtFecInicio.Enabled = True
        '    txtFecFin.Enabled = True
        '    imgCalendarIni.Enabled = True
        '    imgCalendarFin.Enabled = True
        '    ddlEstado.Enabled = True
        '    txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
        '    txtFecFin.Text = Date.Now.ToString
        'End If
        Tab_ReporteRecibos.Visible = False
        gvwRepRecibos.DataSource = Nothing
        gvwRepRecibos.DataBind()
        ImprimirRecibos.Visible = False
    End Sub

    Protected Sub imbCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelar.Click

        If ddlCanal.SelectedIndex > 0 Then
            ddlProducto.SelectedIndex = 0
        End If
        Call CargarCombos()

        ddlCanal.Enabled = True
        imbAceptar1.Enabled = True
        gvwRepRecibos.DataSource = Nothing
        gvwRepRecibos.DataBind()

    End Sub

    Protected Sub imbAceptar1_Click(sender As Object, e As ImageClickEventArgs) Handles imbAceptar1.Click
        Try
            If ddlCanal.SelectedIndex > 0 And ddlProducto.SelectedIndex > 0 Then
                Dim fecIni As DateTime
                Dim fecFin As DateTime
                Dim isValidDate As Boolean
                If txtFecInicio.Text <> "" Then
                    isValidDate = IsDate(txtFecInicio.Text)
                    If isValidDate Then
                        fecIni = Convert.ToDateTime(txtFecInicio.Text)
                    Else
                        txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                        AlertaScripts("Fecha Incorrecta")
                    End If
                End If
                If txtFecFin.Text <> "" Then
                    isValidDate = IsDate(txtFecFin.Text)
                    If isValidDate Then
                        fecFin = Convert.ToDateTime(txtFecFin.Text)
                    Else
                        txtFecFin.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                        AlertaScripts("Fecha Incorrecta")
                    End If
                End If

                If fecIni.ToString <> "" And fecFin.ToString <> "" And fecIni > fecFin And isValidDate = True Then
                    AlertaScripts("Rango de Fechas de Emisión inválidos")
                    txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                    txtFecFin.Text = Date.Now.ToString
                Else
                    Dim dt As DataTable
                    gvwRepRecibos.DataSource = Nothing
                    gvwRepRecibos.DataBind()
                    dt = Metodos.Lista_Report_Recibos_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, ddlEstado.SelectedValue, txtFecInicio.Text, txtFecFin.Text)
                    If dt.Rows.Count > 0 Then
                        Session("dtRecibos") = dt
                        gvwRepRecibos.DataSource = dt
                        gvwRepRecibos.DataBind()
                        Tab_ReporteRecibos.Visible = True
                        ImprimirRecibos.Visible = True
                        ddlCanal.Enabled = False
                        ddlProducto.Enabled = False
                        'ddlPoliza.Enabled = False
                        lblNumListRecibos.Text = Convert.ToInt16(dt.Rows.Count)
                        If dt.Rows.Count > 10 Then
                            ddlListRecibos.Enabled = True
                        Else
                            ddlListRecibos.Enabled = False
                        End If
                    Else
                        Tab_ReporteRecibos.Visible = False
                        gvwRepRecibos.DataSource = Nothing
                        gvwRepRecibos.DataBind()
                        ImprimirRecibos.Visible = False
                        AlertaScripts("No se encontraron Datos")
                    End If
                End If
            Else
                AlertaScripts("Debe seleccionar el Canal y el Producto")
            End If
        Catch ex As Exception
            AlertaScripts("Ocurrio un Error")
        End Try
    End Sub

    Protected Sub ImprimirRecibos_Click(sender As Object, e As ImageClickEventArgs) Handles ImprimirRecibos.Click
        Try

            Dim dt As DataTable

            Dim x(6) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "FechaInicio;" & txtFecInicio.Text
            x(4) = "FechaFin;" & txtFecFin.Text
            x(5) = "Total;" & lblNumListRecibos.Text
            x(6) = "Estado;" & IIf(ddlEstado.SelectedIndex > 0, ddlEstado.SelectedItem.Text, "")
            'dt = Metodos.Lista_Report_Recibos_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, ddlEstado.SelectedValue, Convert.ToDateTime(txtFecInicio.Text), Convert.ToDateTime(txtFecVencimiento.Text))
            dt = Session("dtRecibos")
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Reporte_Recibos.rpt"

            Session("NombreRPT") = "Recibos"
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

    Protected Sub gvwRepRecibos_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvwRepRecibos.PageIndexChanging
        gvwRepRecibos.PageIndex = e.NewPageIndex
        Dim dt As DataTable
        dt = Session("dtRecibos")
        gvwRepRecibos.DataSource = dt
        gvwRepRecibos.DataBind()
    End Sub

    Protected Sub ddlListEndosos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListRecibos.SelectedIndexChanged
        gvwRepRecibos.PageSize = Convert.ToInt16(ddlListRecibos.SelectedValue)
        Dim dt As DataTable
        dt = Session("dtRecibos")
        gvwRepRecibos.DataSource = dt
        gvwRepRecibos.DataBind()
    End Sub

End Class
