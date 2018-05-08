Imports System.Data

Partial Class GenRecibosFam
    Inherits System.Web.UI.Page
    Dim metodos As New Metodos
    Dim dtRecibos As DataTable
    Dim dtCombos As New DataSet
    Dim dtsListadoRecibos As New DataSet

    Dim FlagNreceipt As Boolean = False
    Dim vCant As Double = 0
    Dim vCantPol As Double = 0

    Dim vPoliza As Int64
    Dim vCertificadoS As Int64
    Dim vFechaFact As String
    Dim vProducto As Integer = 0
    Dim SvalFecha As String
    Dim SvalPoliza As String

    Dim año As String = ""
    Dim mes As String = ""
    Dim dia As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Me.btnGrabar.Attributes.Add("onclick", "return preguntar('Al procHesar creara las tareas necesarias y registrará asientos contables. ¿Desea Continuar?');") 'AMARRO ALA FUNCION JAVASCRIP
            If StatusSession() Then
                If Not Page.IsPostBack Then

                    Call CargarCombos()
                    ddlList.Enabled = False
                    'PnelCarga.Visible = False
                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception
            AlertaScripts("Los registros de cargas no se pudo cargar")
        End Try
    End Sub


    Function StatusSession() As Boolean
        Dim rpsSession As Boolean = False
        Try
            Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
            txt.Text = Session("Nombre")
            TxtUsuario.Text = Session("Nombre")
            If (String.IsNullOrEmpty(txt.Text) = False) Then
                rpsSession = True
            End If
        Catch ex As Exception
            rpsSession = False
        End Try
        Return rpsSession
    End Function
    Private Sub CargarCombos()
        dtCombos = metodos.Lista_ProductosCAFAE(metodos.DB.ORACLE)
        Session("dtCombos") = dtCombos

        With ddlProducto
            .DataSource = dtCombos.Tables(0)
            .DataTextField = "SDESCRIPT"
            .DataValueField = "NPRODUCT"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlProducto)

        With ddlPoliza
            .DataSource = dtCombos.Tables(1)
            .DataTextField = "NPOLICY"
            .DataValueField = "NPOLICY"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlPoliza)

        ddlProducto.SelectedIndex = 0
        ddlPoliza.Enabled = False
        txtFactura.Text = DateSerial(Year(Date.Now), Month(Date.Now), Day(Date.Now))

    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try

            If ddlProducto.SelectedIndex = 0 Then
                ddlPoliza.SelectedIndex = 0
                ddlPoliza.Enabled = False

            Else
                ddlPoliza.Enabled = True

                dtCombos = Session("dtCombos")
                Dim arr As New ArrayList
                For Each fila As DataRow In dtCombos.Tables(1).Select("NPRODUCT=" & ddlProducto.SelectedValue & "")
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

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            If ddlProducto.SelectedIndex > 0 And ddlPoliza.SelectedIndex > 0 Then
                gvDetalle.DataSource = Nothing
                gvDetalle.DataBind()

                dtRecibos = metodos.Lista_Registros_RecFam(metodos.DB.ORACLE, ddlProducto.SelectedValue, ddlPoliza.SelectedValue)

                If dtRecibos.Rows.Count > 0 Then
                    Session("dtDatos") = dtRecibos
                    gvDetalle.DataSource = dtRecibos
                    gvDetalle.DataBind()

                    If dtRecibos.Rows.Count > 10 Then
                        ddlList.Enabled = True
                    Else
                        ddlList.Enabled = False
                    End If

                    txtNumReg.Text = dtRecibos.Compute("count(CANTIDAD_CERT)", "").ToString
                    txtTotalPrima.Text = Math.Round(dtRecibos.Compute("Sum(BRUTO)", ""), 2).ToString
                Else
                    txtNumReg.Text = "0"
                    txtTotalPrima.Text = "0"
                    AlertaScripts("No se encontraron datos")
                End If

            Else
                gvDetalle.DataSource = Nothing
                gvDetalle.DataBind()
                txtNumReg.Text = "0"
                txtTotalPrima.Text = "0"
                AlertaScripts("Debe seleccionar Producto y Póliza")
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
        Catch ex As Exception
            AlertaScripts("Ocurrio un Error")
        End Try
    End Sub

    Protected Sub rblmarcar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblmarcar.SelectedIndexChanged
        If rblmarcar.Items(0).Selected Then
            System.Threading.Thread.Sleep(500)
            Marcar(True)
        Else
            System.Threading.Thread.Sleep(500)
            Marcar(False)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
    End Sub

    Protected Sub Marcar(ByVal checkState As Boolean)

        For Each row As GridViewRow In gvDetalle.Rows
            Dim cb As CheckBox = row.FindControl("ChkSel")
            If cb IsNot Nothing Then
                cb.Checked = checkState
            End If
        Next

    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Try
            Dim fecha As DateTime

            If TxtUsuario.Text.Trim <> "" Then
                For Each row As GridViewRow In gvDetalle.Rows
                    Dim cb As CheckBox = row.FindControl("ChkSel")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        vCant = vCant + 1
                        Exit For
                    End If
                Next

                If vCant = 0 Then
                    AlertaScripts("No ha marcado ningún registro para procesar")
                Else
                    PnelCarga.Visible = True
                    For Each row As GridViewRow In gvDetalle.Rows
                        Dim cb As CheckBox = row.FindControl("ChkSel")
                        If cb IsNot Nothing AndAlso cb.Checked Then
                            vCertificadoS = gvDetalle.Rows(row.RowIndex).Cells(1).Text
                            vProducto = ddlProducto.SelectedValue
                            vPoliza = ddlPoliza.SelectedValue
                            If txtFactura.Text.Trim <> "" Then
                                If Not DateTime.TryParse(txtFactura.Text, fecha) Then
                                    AlertaScripts("Debe ingresar una fecha Válida")
                                Else
                                    año = txtFactura.Text.Substring(6, 4)
                                    mes = txtFactura.Text.Substring(3, 2)
                                    dia = txtFactura.Text.Substring(0, 2)
                                    vFechaFact = año & mes & dia
                                    SvalFecha = metodos.Validar_Fecha_Factura(metodos.DB.ORACLE, vFechaFact)
                                    If SvalFecha = "FECHA VALIDA" Then

                                        SvalPoliza = metodos.Validar_Estado_Poliza(metodos.DB.ORACLE, vProducto, vPoliza)
                                        If SvalPoliza = "LIBRE" Then
                                            lblMensajeProceso.Text = "Desea generar " & vCertificadoS & " Recibos Familiares Individuales y Facturas de la Serie 006 con fecha " & txtFactura.Text
                                            mpeCarga.Show()
                                        Else
                                            AlertaScripts(SvalPoliza)
                                        End If
                                    Else
                                        AlertaScripts(SvalFecha)
                                    End If
                                End If
                            Else
                                AlertaScripts("Ingresar fecha de facturación")
                            End If

                        End If
                    Next

                End If

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)

            Else
                AlertaScripts("Expiró la sesión, favor de ingresar nuevamente al Sistema.")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub imbAceptarCarga_Click(sender As Object, e As ImageClickEventArgs) Handles imbAceptarCarga.Click

        Call Generacion_Rec_Fam()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()

        Call btnBuscar_Click(sender, New EventArgs())

    End Sub

    Private Sub Generacion_Rec_Fam()
        Try
            Dim sEstadoJob As String

            vProducto = ddlProducto.SelectedValue
            vPoliza = ddlPoliza.SelectedValue


            For Each row As GridViewRow In gvDetalle.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing AndAlso cb.Checked Then

                    'System.Threading.Thread.Sleep(500)
                    vCertificadoS = gvDetalle.Rows(row.RowIndex).Cells(1).Text

                    'Validar_Fecha_Factura
                    año = txtFactura.Text.Substring(6, 4)
                    mes = txtFactura.Text.Substring(3, 2)
                    dia = txtFactura.Text.Substring(0, 2)
                    vFechaFact = año & mes & dia

                    SvalFecha = metodos.Validar_Fecha_Factura(metodos.DB.ORACLE, vFechaFact)

                    System.Threading.Thread.Sleep(500)
                    sEstadoJob = metodos.Crea_Job_Recibos_Familiares(metodos.DB.ORACLE, vProducto, vPoliza, vCertificadoS, vFechaFact, Session("CodUsuario"))

                    If sEstadoJob = "5" Then
                        AlertaScripts("Ejecución exitosa")
                    Else
                        AlertaScripts("Ocurrio un error al Ejecutar el Proceso")
                    End If
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)

        Catch ex As Exception
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "End", "CerrarEspera();", True)
            AlertaScripts("Ocurrio un Error")
        End Try
    End Sub

    Protected Sub ddlList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlList.SelectedIndexChanged
        gvDetalle.PageSize = Convert.ToInt16(ddlList.SelectedValue)
        Dim dt As DataTable
        dt = Session("dtDatos")
        gvDetalle.DataSource = dt
        gvDetalle.DataBind()
    End Sub

    Protected Sub gvDetalle_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvDetalle.PageIndexChanging
        gvDetalle.PageIndex = e.NewPageIndex
        Dim dt As DataTable
        dt = Session("dtDatos")
        gvDetalle.DataSource = dt
        gvDetalle.DataBind()
    End Sub

    Protected Sub imbCancelarCarga_Click(sender As Object, e As ImageClickEventArgs) Handles imbCancelarCarga.Click
        PnelCarga.Visible = False
        mpeCarga.Hide()
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Response.Redirect("Menu.aspx")
    End Sub

End Class
