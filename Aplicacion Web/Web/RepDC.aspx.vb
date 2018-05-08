Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Data.OracleClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Globalization

Partial Class RepEndosos
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet
    Dim dtEstados As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                lblTipoMovimiento.Text = "Documentos Contables"
                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")

                Global.Metodos.AgregarItemCombo(ddlTipoDoc)
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
        txtFecEfectoIni.Enabled = False
        txtFecEfectoFin.Enabled = False
        imgCalendarIni.Enabled = False
        imgCalendarFin.Enabled = False
        Tab_ReporteDC.Visible = False
        ImprimirFacturas.Visible = False

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

        With ddlEstado
            .DataSource = dtCombos.Tables(4)
            .DataTextField = "SDESCRIPT"
            .DataValueField = "NBILLSTAT"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlEstado)

        If ddlCanal.Items.Count = 2 Then
            ddlCanal.SelectedIndex = 1
            Call ddlCanal_SelectedIndexChanged(ddlCanal, System.EventArgs.Empty)
        Else
            ddlCanal.SelectedIndex = 0
        End If

        'ddlCanal.SelectedIndex = 0
        'ddlPoliza.SelectedIndex = 0
        ddlEstado.SelectedIndex = 0
        ddlTipoDoc.SelectedIndex = 0
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
                ddlTipoDoc.SelectedIndex = 0
                ddlTipoDoc.Enabled = False
                txtFecEfectoIni.Enabled = False
                txtFecEfectoIni.Text = ""
                txtFecEfectoFin.Enabled = False
                txtFecEfectoFin.Text = ""
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
                ddlTipoDoc.SelectedIndex = 0
                ddlTipoDoc.Enabled = False
                txtFecEfectoIni.Enabled = False
                txtFecEfectoIni.Text = ""
                txtFecEfectoFin.Enabled = False
                txtFecEfectoFin.Text = ""
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

                ddlTipoDoc.Enabled = True
                txtFecEfectoIni.Enabled = True
                txtFecEfectoFin.Enabled = True
                imgCalendarIni.Enabled = True
                imgCalendarFin.Enabled = True
                ddlEstado.Enabled = True
                txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                txtFecEfectoFin.Text = Date.Now.ToString

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

    Protected Sub imbCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbCancelar.Click

        Call CargarCombos()
        ddlCanal.Enabled = True

        imbAceptar1.Enabled = True
    End Sub

    Protected Sub ddlPoliza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoliza.SelectedIndexChanged
        'If ddlPoliza.SelectedIndex <> 0 Then
        '    txtFecEfectoIni.Enabled = True
        '    txtFecEfectoFin.Enabled = True
        '    imgCalendarIni.Enabled = True
        '    imgCalendarFin.Enabled = True
        '    ddlEstado.Enabled = True
        '    ddlTipoDoc.Enabled = True
        '    txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
        '    txtFecEfectoFin.Text = Date.Now.ToString
        'End If
        Tab_ReporteDC.Visible = False
        gvwRepFactura.DataSource = Nothing
        gvwRepFactura.DataBind()
        ImprimirFacturas.Visible = False
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
                        txtFecEfectoFin.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                        AlertaScripts("Fecha Incorrecta")
                    End If
                End If

                If fecIni.ToString <> "" And fecFin.ToString <> "" And fecIni > fecFin And isValidDate = True Then
                    AlertaScripts("Rango de Fechas de Emisión inválidos")
                    txtFecEfectoIni.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                    txtFecEfectoFin.Text = Date.Now.ToString
                Else

                    Dim dt As DataTable
                    gvwRepFactura.DataSource = Nothing
                    gvwRepFactura.DataBind()
                    dt = Metodos.Lista_Report_DC_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, ddlTipoDoc.SelectedValue, ddlEstado.SelectedValue, Convert.ToDateTime(txtFecEfectoIni.Text), Convert.ToDateTime(txtFecEfectoFin.Text))
                    If dt.Rows.Count > 0 Then
                        Session("dtDocumentosC") = dt
                        gvwRepFactura.DataSource = dt
                        gvwRepFactura.DataBind()

                        Tab_ReporteDC.Visible = True
                        ImprimirFacturas.Visible = True
                        lblNumList.Text = Convert.ToInt16(dt.Rows.Count)
                        If dt.Rows.Count > 10 Then
                            ddlList.Enabled = True
                        Else
                            ddlList.Enabled = False
                        End If
                    Else
                        Tab_ReporteDC.Visible = False
                        gvwRepFactura.DataSource = Nothing
                        gvwRepFactura.DataBind()
                        ImprimirFacturas.Visible = False
                        AlertaScripts("No se encontraron Datos")
                        ImprimirFacturas.Visible = False
                    End If
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

    Protected Sub ImprimirFacturas_Click(sender As Object, e As ImageClickEventArgs) Handles ImprimirFacturas.Click
        Try
            Dim dt As DataTable
            Dim x(5) As String

            x(0) = "NomEmpresa;Protecta Compañia de Seguros"
            x(1) = "Canal;" & ddlCanal.SelectedItem.Text
            x(2) = "Producto;" & ddlProducto.SelectedItem.Text
            x(3) = "Poliza;" & ddlPoliza.SelectedValue
            x(4) = "TipoDoc;" & IIf(ddlTipoDoc.SelectedIndex > 0, ddlTipoDoc.SelectedItem.Text, "")
            x(5) = "Estado;" & IIf(ddlEstado.SelectedIndex > 0, ddlEstado.SelectedItem.Text, "")

            'dt = Metodos.Lista_Report_DC_SCTR(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, ddlTipoDoc.SelectedValue, ddlEstado.SelectedValue, Convert.ToDateTime(txtFecEfectoIni.Text), Convert.ToDateTime(txtFecEfectoFin.Text))
            dt = Session("dtDocumentosC")
            Session("TABLA_REPORTE") = dt
            Session("RPT") = "Reporte_DC.rpt"

            Session("NombreRPT") = "Documento Comprobantes"
            Session("Arreglo") = x
            Session("Data") = "S"

            Dim SB As New System.Text.StringBuilder
            SB.Append("<SCRIPT LANGUAGE=javascript>")
            SB.Append("window.open('Reporte.aspx');")
            SB.Append("</script>")
            Page.RegisterStartupScript("open", SB.ToString())

            'gvwRepFactura.PageSize = Convert.ToInt16(ddlList.SelectedValue)
            'Dim dt As DataTable
            'dt = Session("dtDocumentosC")
            'If dt.Rows.Count > 0 Then
            ' mExportExcel(dt)
            'Else
            'AlertaScripts("No se encontraron Datos")
            'End If
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try
    End Sub

    Protected Sub mExportExcel(dt As DataTable)

        Dim dg As New GridView
        dg.DataSource = dt : dg.DataBind()

        ' THE EXCEL FILE.
        Dim sFileName As String = "ReporteComprobantesSCTR" & Replace(Date.Now.Date, "/", "") & ".xls"

        ' SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
        Response.Clear()
        Response.ClearContent()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment; filename=" & sFileName)
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False

        Dim objSW As New System.IO.StringWriter
        Dim objHTW As New HtmlTextWriter(objSW)

        ' STYLE THE SHEET AND WRITE DATA TO IT.
        Response.Write("<table><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td colspan=4><b>REPORTE DETALLE DE RECIBOS SCTR </b></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Canal: </b></td><td colspan=3 align=left><b>" + ddlCanal.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Producto: </b></td><td colspan=2 align=left><b>" + ddlProducto.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Póliza: </b></td><td align=left><b>" + ddlPoliza.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Estado: </b></td><td align=left><b>" + ddlEstado.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Fecha de Documento: </b></td><td><b>" + txtFecEfectoIni.Text + "</b></td><td><b>" + txtFecEfectoFin.Text + "</b></td></tr>")
        Response.Write("<tr></tr></table>")
        Response.Write("<style> TABLE { border:dotted 1px #999; } " & _
            "TD { border:dotted 1px #D5D5D5 } td { mso-number-format:\@; } </style>")

        For Each row As GridViewRow In dg.Rows
            row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            row.Cells(4).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(4).Text))
            row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            row.Cells(5).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(5).Text))
            row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            row.Cells(6).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(6).Text))
            row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            row.Cells(7).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(7).Text))
            row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            row.Cells(10).HorizontalAlign = HorizontalAlign.Right
            row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            row.Cells(12).HorizontalAlign = HorizontalAlign.Right
            row.Cells(13).HorizontalAlign = HorizontalAlign.Center
            row.Cells(14).HorizontalAlign = HorizontalAlign.Center
            row.Cells(15).HorizontalAlign = HorizontalAlign.Center
            row.Cells(16).HorizontalAlign = HorizontalAlign.Center
            row.Cells(17).HorizontalAlign = HorizontalAlign.Center
            row.Cells(18).HorizontalAlign = HorizontalAlign.Center
        Next

        dg.HeaderStyle.Font.Bold = True     ' SET EXCEL HEADERS AS BOLD.
        dg.RenderControl(objHTW)

        Response.Write(objSW.ToString())
        Response.Flush()
        Response.End()
        dg = Nothing

    End Sub


    Protected Sub ddlList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlList.SelectedIndexChanged
        gvwRepFactura.PageSize = Convert.ToInt16(ddlList.SelectedValue)
        Dim dt As DataTable
        dt = Session("dtDocumentosC")
        gvwRepFactura.DataSource = dt
        gvwRepFactura.DataBind()
    End Sub

    Protected Sub gvwRepFactura_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvwRepFactura.PageIndexChanging
        gvwRepFactura.PageIndex = e.NewPageIndex
        Dim dt As DataTable
        dt = Session("dtDocumentosC")
        gvwRepFactura.DataSource = dt
        gvwRepFactura.DataBind()
    End Sub

End Class
