Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Data.OracleClient
Imports System.Drawing

Partial Class RepEndosos
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                lblTitulo.Text = "Movimientos"
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

        ddlProducto.Enabled = False
        ddlPoliza.Enabled = False
        ddlEstado.Enabled = False
        txtRuc.Enabled = False
        txtRuc.Text = ""
        txtFecEfectoIni.Enabled = False
        txtFecEfectoFin.Enabled = False
        imgCalendarIni.Enabled = False
        imgCalendarFin.Enabled = False
        Tab_ReporteEndoso.Visible = False
        ImprimirEndosos.Visible = False
        ImprimirDetEndosos.Visible = False

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

    Protected Sub ddlCanal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCanal.SelectedIndexChanged
        Try
            If ddlCanal.SelectedIndex = 0 Then
                ddlProducto.SelectedIndex = 0
                ddlProducto.Enabled = False
                ddlPoliza.SelectedIndex = 0
                ddlPoliza.Enabled = False
                ddlEstado.SelectedIndex = 0
                ddlEstado.Enabled = False
                txtRuc.Enabled = False
                txtRuc.Text = ""
                txtFecEfectoIni.Enabled = False
                txtFecEfectoFin.Enabled = False
                imgCalendarIni.Enabled = False
                imgCalendarFin.Enabled = False
                txtFecEfectoIni.Text = ""
                txtFecEfectoFin.Text = ""
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
                txtRuc.Enabled = False
                txtRuc.Text = ""
                txtFecEfectoIni.Enabled = False
                txtFecEfectoFin.Enabled = False
                imgCalendarIni.Enabled = False
                imgCalendarFin.Enabled = False
                txtFecEfectoIni.Text = ""
                txtFecEfectoFin.Text = ""
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

                ddlEstado.Enabled = True
                txtRuc.Enabled = True
                txtFecEfectoIni.Enabled = True
                txtFecEfectoFin.Enabled = True
                imgCalendarIni.Enabled = True
                imgCalendarFin.Enabled = True
                txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                txtFecEfectoFin.Text = Date.Now.ToString

                If ddlPoliza.Items.Count = 2 Then
                    ddlPoliza.SelectedIndex = 1
                    Call ddlPoliza_SelectedIndexChanged(ddlPoliza, System.EventArgs.Empty)
                Else
                    ddlPoliza.SelectedIndex = 0
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlPoliza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoliza.SelectedIndexChanged
        'If ddlPoliza.SelectedIndex = 0 Then
        '    ddlEstado.Enabled = False
        '    txtFecEfectoIni.Enabled = False
        '    txtFecEfectoFin.Enabled = False
        '    imgCalendarIni.Enabled = False
        '    imgCalendarFin.Enabled = False
        '    txtFecEfectoIni.Text = ""
        '    txtFecEfectoFin.Text = ""

        'Else
        '    ddlEstado.Enabled = True
        '    txtFecEfectoIni.Enabled = True
        '    txtFecEfectoFin.Enabled = True
        '    imgCalendarIni.Enabled = True
        '    imgCalendarFin.Enabled = True
        '    txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
        '    txtFecEfectoFin.Text = Date.Now.ToString

        'End If

        mOcultarDetEndosos()

    End Sub

    Protected Sub imbCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelar.Click

        If ddlCanal.SelectedIndex > 0 Then
            ddlProducto.SelectedIndex = 0
        End If
        Call CargarCombos()
        'txtFecEfectoIni.Text = ""
        'txtFecEfectoFin.Text = ""

        ddlCanal.Enabled = True

        imbAceptar1.Enabled = True

        Tab_ReporteEndoso.Visible = False
        gvwRepEndosos.DataSource = Nothing
        gvwRepEndosos.DataBind()
        gvwRepEndososDet.DataSource = Nothing
        gvwRepEndososDet.DataBind()

    End Sub

    Private Sub mOcultarDetEndosos()
        Tab_ReporteEndoso.Visible = False
        ImprimirDetEndosos.Visible = False
        UpdatePanel3.Update()

        gvwRepEndosos.DataSource = Nothing
        gvwRepEndosos.DataBind()
        gvwRepEndososDet.DataSource = Nothing
        gvwRepEndososDet.DataBind()
    End Sub

    Protected Sub imbAceptar1_Click(sender As Object, e As ImageClickEventArgs) Handles imbAceptar1.Click
        Try
            If ddlCanal.SelectedIndex > 0 And ddlProducto.SelectedIndex > 0 Then
                Dim fecIni As DateTime
                Dim fecFin As DateTime
                Dim isValidDate As Boolean
                If txtFecEfectoIni.Text <> "" Then
                    isValidDate = IsDate(txtFecEfectoIni.Text)
                    If isValidDate Then
                        fecIni = Convert.ToDateTime(txtFecEfectoIni.Text)
                    Else
                        txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                        AlertaScripts("Fecha Incorrecta")
                    End If
                End If
                If txtFecEfectoFin.Text <> "" Then
                    isValidDate = IsDate(txtFecEfectoFin.Text)
                    If isValidDate Then
                        fecFin = Convert.ToDateTime(txtFecEfectoFin.Text)
                    Else
                        txtFecEfectoFin.Text = Date.Now.ToString
                        AlertaScripts("Fecha Incorrecta")
                    End If
                End If

                If fecIni.ToString <> "" And fecFin.ToString <> "" And fecIni > fecFin And isValidDate = True Then
                    AlertaScripts("Rango de Fechas de Emisión inválidos")
                    txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                    txtFecEfectoFin.Text = Date.Now.ToString
                Else
                    Dim dt As DataTable
                    gvwRepEndosos.DataSource = Nothing
                    gvwRepEndosos.DataBind()
                    dt = Metodos.Lista_Report_Endosos_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, txtFecEfectoIni.Text, txtFecEfectoFin.Text, ddlEstado.SelectedValue, txtRuc.Text)
                    If dt.Rows.Count > 0 Then
                        Session("dtEndosos") = dt
                        gvwRepEndosos.DataSource = dt
                        gvwRepEndosos.DataBind()
                        Tab_ReporteEndoso.Visible = True
                        ImprimirEndosos.Visible = True
                        'Call DatosCliente()
                        ddlCanal.Enabled = False
                        ddlProducto.Enabled = False
                        'ddlPoliza.Enabled = False
                        lblNumListEndosos.Text = Convert.ToInt16(dt.Rows.Count)
                        If dt.Rows.Count > 10 Then
                            ddlListEndosos.Enabled = True
                        Else
                            ddlListEndosos.Enabled = False
                        End If
                        ImprimirDetEndosos.Visible = False
                        UpdatePanel3.Update()

                        gvwRepEndososDet.DataSource = Nothing
                        gvwRepEndososDet.DataBind()
                    Else
                        mOcultarDetEndosos()
                        AlertaScripts("No se encontraron Datos")
                    End If
                    lblTotalDetEndosos.Visible = False
                    lblNumDetEndosos.Visible = False
                    lblFilDetEndosos.Visible = False
                    ddlListDetEndosos.Visible = False
                End If
            Else
                AlertaScripts("Debe seleccionar el Canal y el Producto")
            End If
        Catch ex As Exception
            AlertaScripts("Ocurrio un Error")
        End Try
    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Protected Sub gvwRepEndosos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvwRepEndosos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.backgroundColor='#F3F781';this.style.cursor='pointer';"
            e.Row.Attributes("onmouseout") = "this.style.backgroundColor='white';"
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvwRepEndosos, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Seleccionar Fila"
        End If
    End Sub

    Protected Sub gvwRepEndosos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvwRepEndosos.SelectedIndexChanged
        Dim index As Integer = gvwRepEndosos.SelectedRow.RowIndex
        Dim rb As RadioButton = DirectCast(gvwRepEndosos.SelectedRow.Cells(0).FindControl("RadioButton1"), RadioButton)
        If rb IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "radiobutton", "checkRadioBtn(" & rb.ClientID & ");", True)
            rb.Checked = True
        End If
        Dim sPoliza As String = gvwRepEndosos.SelectedRow.Cells(1).Text
        Dim sRUC As String = gvwRepEndosos.SelectedRow.Cells(2).Text
        Dim sRazonSocial As String = gvwRepEndosos.SelectedRow.Cells(3).Text
        Dim sRenovacion As String = gvwRepEndosos.SelectedRow.Cells(4).Text
        Dim sNumMov As String = gvwRepEndosos.SelectedRow.Cells(12).Text
        Dim sTipoMov As String = gvwRepEndosos.SelectedRow.Cells(5).Text
        HF_POLIZA.Value = sPoliza
        HF_RUC.Value = sRUC
        HF_RAZONSOCIAL.Value = sRazonSocial
        HF_RENOVACION.Value = sRenovacion
        HF_NUM_MOVIMIENTO.Value = sNumMov
        HF_TIPOMOV.Value = sTipoMov
        Dim dt As DataTable
        If sTipoMov = "INCLUSION POR EXCLUSION" Then
            gvwRepEndososDet.Columns(10).Visible = True
        Else
            gvwRepEndososDet.Columns(10).Visible = False
        End If

        gvwRepEndososDet.DataSource = Nothing
        gvwRepEndososDet.DataBind()
        dt = Metodos.Lista_Report_DetEndosos_SCTR(Metodos.DB.ORACLE, ddlProducto.SelectedValue, sPoliza, sRenovacion, sNumMov)
        If dt.Rows.Count > 0 Then
            Session("dtDetEndosos") = Nothing
            Session("dtDetEndosos") = dt
            gvwRepEndososDet.DataSource = dt
            gvwRepEndososDet.DataBind()
            gvwRepEndososDet.PageIndex = 0
            ImprimirDetEndosos.Visible = True
            lblNumDetEndosos.Text = dt.Rows.Count
            If dt.Rows.Count > 10 Then
                ddlListDetEndosos.Enabled = True
            Else
                ddlListDetEndosos.Enabled = False
            End If
            lblTotalDetEndosos.Visible = True
            lblNumDetEndosos.Visible = True
            lblFilDetEndosos.Visible = True
            ddlListDetEndosos.Visible = True
        Else
        lblTotalDetEndosos.Visible = False
        lblNumDetEndosos.Visible = False
        lblFilDetEndosos.Visible = False
        ddlListDetEndosos.Visible = False
        End If
        'UpdatePanel2.Update()
        UpdatePanel3.Update()
        udpLisDetEndosos.Update()
    End Sub

    Protected Sub ImprimirEndosos_Click(sender As Object, e As ImageClickEventArgs) Handles ImprimirEndosos.Click
        Try

            Dim dt As DataTable

            Dim x(6) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & "" 'ddlPoliza.SelectedValue
            x(4) = "FechaInicio;" & txtFecEfectoIni.Text
            x(5) = "FechaFin;" & txtFecEfectoFin.Text
            x(6) = "Total;" & lblNumListEndosos.Text

            dt = Metodos.Lista_Report_Endosos_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Convert.ToDateTime(txtFecEfectoIni.Text), Convert.ToDateTime(txtFecEfectoFin.Text), ddlEstado.SelectedValue, txtRuc.Text)
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Reporte_Endosos.rpt"

            Session("NombreRPT") = "Endoso"
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

    Protected Sub ImprimirDetEndosos_Click(sender As Object, e As ImageClickEventArgs) Handles ImprimirDetEndosos.Click
        Try
            Dim dt As DataTable

            Dim x(11) As String
            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & HF_POLIZA.Value
            x(4) = "FechaInicio;" & txtFecEfectoIni.Text
            x(5) = "FechaFin;" & txtFecEfectoFin.Text
            x(6) = "Total;" & lblNumDetEndosos.Text
            x(7) = "RUC;" & HF_RUC.Value
            x(8) = "RazonSocial;" & HF_RAZONSOCIAL.Value
            x(9) = "Renovacion;" & HF_RENOVACION.Value
            x(10) = "Endoso;" & HF_NUM_MOVIMIENTO.Value
            x(11) = "Movimiento;" & HF_TIPOMOV.Value

            'dt = Metodos.Lista_Report_Endosos_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, Convert.ToDateTime(txtFecEfectoIni.Text), Convert.ToDateTime(txtFecEfectoFin.Text))
            dt = Session("dtDetEndosos")
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Reporte_Detalle_Endosos.rpt"

            Session("NombreRPT") = "DetalleEndoso"
            Session("Arreglo") = x
            Session("Data") = "S"
            'Server.Transfer("Reportes_PL.aspx", True)
            Dim SB As New System.Text.StringBuilder
            'SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            'SB.Append("</script>")
            'Page.RegisterStartupScript("open", SB.ToString())
            ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "open", SB.ToString(), True)

        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Protected Sub gvwRepEndososDet_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvwRepEndososDet.PageIndexChanging
        gvwRepEndososDet.PageIndex = e.NewPageIndex
        Dim dt As DataTable
        dt = Session("dtDetEndosos")
        gvwRepEndososDet.DataSource = dt
        gvwRepEndososDet.DataBind()
    End Sub

    Protected Sub gvwRepEndosos_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvwRepEndosos.PageIndexChanging
        gvwRepEndosos.PageIndex = e.NewPageIndex
        Dim dt As DataTable
        dt = Session("dtEndosos")
        gvwRepEndosos.DataSource = dt
        gvwRepEndosos.DataBind()
    End Sub

    Protected Sub ddlListEndosos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListEndosos.SelectedIndexChanged
        gvwRepEndosos.PageSize = Convert.ToInt16(ddlListEndosos.SelectedValue)
        Dim dt As DataTable
        dt = Session("dtEndosos")
        gvwRepEndosos.DataSource = dt
        gvwRepEndosos.DataBind()
    End Sub

    Protected Sub ddlListDetEndosos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListDetEndosos.SelectedIndexChanged
        gvwRepEndososDet.PageSize = Convert.ToInt16(ddlListDetEndosos.SelectedValue)
        Dim dt As DataTable
        dt = Session("dtDetEndosos")
        gvwRepEndososDet.DataSource = dt
        gvwRepEndososDet.DataBind()
    End Sub

End Class
