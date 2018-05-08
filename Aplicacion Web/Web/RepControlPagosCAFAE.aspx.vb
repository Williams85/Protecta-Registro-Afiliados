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

Partial Class RepControlPagosCAFAE
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                lblTitulo.Text = "Reporte de Control de Pagos de Asegurados CAFAE"
                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")
                Call CargarCombos()
            End If
        Catch ex As Exception
            AlertaScripts("No es posible cargar datos iniciales")
        End Try
    End Sub

    Private Sub CargarCombos()
        dtCombos = Metodos.Lista_Datos_Combos_CAFAE(Metodos.DB.ORACLE)

        With ddlCanal
            .DataSource = dtCombos.Tables(0)
            .DataTextField = "DES_CANAL"
            .DataValueField = "CANAL"
            .DataBind()
        End With
        Global.Metodos.AgregarItemCombo(ddlCanal)

        ddlProducto.Enabled = False
        ddlPoliza.Enabled = False
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
                txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                txtFecFin.Text = DateSerial(Year(Date.Now), Month(Date.Now) + 1, 0)
                'txtFecFin.Text = Date.Now.ToString

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

        Tab_ReporteRecibos.Visible = False
        gvwRepRecibos.DataSource = Nothing
        gvwRepRecibos.DataBind()
        ImprimirRecibos.Visible = False
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
                    AlertaScripts("Rango de Fechas inválidos")
                    txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                    txtFecFin.Text = Date.Now.ToString
                Else
                    Dim sfecIni As String = fecIni.ToString("yyyyMMdd")
                    Dim sfecFin As String = fecFin.ToString("yyyyMMdd")
                    Dim dt As DataTable
                    gvwRepRecibos.DataSource = Nothing
                    gvwRepRecibos.DataBind()
                    dt = Metodos.Lista_Report_Recibos_CAFAE(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlPoliza.SelectedValue, sfecIni, sfecFin)
                    If dt.Rows.Count > 0 Then
                        Session("dtRecibos") = dt
                        mExportExcel(dt)
                        'gvwRepRecibos.DataSource = dt
                        'gvwRepRecibos.DataBind()
                        'Tab_ReporteRecibos.Visible = True
                        'ImprimirRecibos.Visible = True
                        'ddlCanal.Enabled = False
                        'ddlProducto.Enabled = False
                        ''ddlPoliza.Enabled = False
                        'lblNumListRecibos.Text = Convert.ToInt16(dt.Rows.Count)
                        'If dt.Rows.Count > 10 Then
                        '    ddlListRecibos.Enabled = True
                        'Else
                        '    ddlListRecibos.Enabled = False
                        'End If
                    Else
                        'Tab_ReporteRecibos.Visible = False
                        'gvwRepRecibos.DataSource = Nothing
                        'gvwRepRecibos.DataBind()
                        'ImprimirRecibos.Visible = False
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

    Protected Sub mExportExcel(dt As DataTable)

        Dim dg As New GridView
        dg.DataSource = dt : dg.DataBind()

        ' THE EXCEL FILE.
        Dim sFileName As String = "Reporte_" & Replace(Date.Now.Date, "/", "") & ".xls"

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
        Response.Write("<table><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td colspan=4><b>REPORTE DE CONTROL DE PAGOS DE ASEGURADOS CAFAE </b></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Canal: </b></td><td colspan=3 align=left><b>" + ddlCanal.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Producto: </b></td><td colspan=2 align=left><b>" + ddlProducto.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Póliza: </b></td><td align=left><b>" + ddlPoliza.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Fecha de Facturación: </b></td><td><b>" + txtFecInicio.Text + "</b></td><td><b>" + txtFecFin.Text + "</b></td></tr>")
        Response.Write("<tr></tr></table>")
        Response.Write("<style> TABLE { border:dotted 1px #999; } " & _
            "TD { border:dotted 1px #D5D5D5 } td { mso-number-format:\@; } </style>")

        For Each row As GridViewRow In dg.Rows
            row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            row.Cells(3).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(3).Text))
            row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            row.Cells(8).HorizontalAlign = HorizontalAlign.Right
            row.Cells(9).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(9).Text))
            row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            row.Cells(11).HorizontalAlign = HorizontalAlign.Center
            'row.Cells(13).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(13).Text))--FEC DEVENGUE
            row.Cells(13).Text = Month(Convert.ToDateTime(row.Cells(13).Text)).ToString.PadLeft(2, "0") & "/" & Year(Convert.ToDateTime(row.Cells(13).Text))
            row.Cells(13).HorizontalAlign = HorizontalAlign.Right
            row.Cells(14).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(14).Text))
            row.Cells(14).HorizontalAlign = HorizontalAlign.Right
            row.Cells(15).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(15).Text))
            row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            row.Cells(16).HorizontalAlign = HorizontalAlign.Center
            row.Cells(17).HorizontalAlign = HorizontalAlign.Right
            row.Cells(18).HorizontalAlign = HorizontalAlign.Right
            row.Cells(19).HorizontalAlign = HorizontalAlign.Right
            row.Cells(23).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(23).Text))
            row.Cells(23).HorizontalAlign = HorizontalAlign.Right
            row.Cells(24).HorizontalAlign = HorizontalAlign.Right
            row.Cells(25).HorizontalAlign = HorizontalAlign.Right
            row.Cells(26).HorizontalAlign = HorizontalAlign.Right
            row.Cells(27).HorizontalAlign = HorizontalAlign.Right
            row.Cells(28).HorizontalAlign = HorizontalAlign.Right
            row.Cells(29).HorizontalAlign = HorizontalAlign.Right
            row.Cells(30).HorizontalAlign = HorizontalAlign.Right
        Next

        dg.HeaderStyle.Font.Bold = True     ' SET EXCEL HEADERS AS BOLD.
        dg.RenderControl(objHTW)

        Response.Write(objSW.ToString())
        Response.Flush()
        Response.End()
        dg = Nothing

    End Sub

    Protected Sub imbCancelar_Click(sender As Object, e As ImageClickEventArgs) Handles imbCancelar.Click
        If ddlCanal.SelectedIndex > 0 Then
            ddlProducto.SelectedIndex = 0
        End If
        Call CargarCombos()
        txtFecInicio.Text = ""
        txtFecFin.Text = ""
        ddlCanal.Enabled = True
        imbAceptar1.Enabled = True
        gvwRepRecibos.DataSource = Nothing
        gvwRepRecibos.DataBind()
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
