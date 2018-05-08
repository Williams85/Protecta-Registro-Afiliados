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

Partial Class RptVISANET
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtCombos As New DataSet


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Session("MovSCTR") = "VISA" Then
                    lblTitulo.Text = "Reporte Detalle de Transacciones VISANET"
                    LblFecha.Text = "Fecha de Abono:"
                Else
                    lblTitulo.Text = "Reporte de Documentos Contables en Estado: Pendiente de Abono"
                    LblFecha.Text = "Fecha del Documento:"
                End If

                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")
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
        ddlComercio.Enabled = False
        txtFecInicio.Enabled = False
        txtFecFin.Enabled = False
        imgCalendarIni.Enabled = False
        imgCalendarFin.Enabled = False
        Tab_ReporteRecibos.Visible = False
        ImprimirVISANET.Visible = False

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
            ddlCanal.Enabled = True
            Call ddlCanal_SelectedIndexChanged(ddlCanal, System.EventArgs.Empty)
        Else
            ddlCanal.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlCanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCanal.SelectedIndexChanged
        Try
            If ddlCanal.SelectedIndex = 0 Then
                ddlProducto.SelectedIndex = 0
                ddlProducto.Enabled = False
                ddlComercio.Enabled = False
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

    Protected Sub ddlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try
            If ddlProducto.SelectedIndex = 0 Then
                ddlComercio.Enabled = False
                txtFecInicio.Enabled = False
                txtFecInicio.Text = ""
                txtFecFin.Enabled = False
                txtFecFin.Text = ""
                imgCalendarIni.Enabled = False
                imgCalendarFin.Enabled = False
            Else

                dtCombos = Session("dtCombos")
                Dim arr As New ArrayList
                For Each fila As DataRow In dtCombos.Tables(5).Select("CANAL='" & ddlCanal.SelectedValue & "'")
                    arr.Add(New Listado(fila("NCOMERCIO"), fila("DES_COMERCIO")))
                Next

                imbAceptar1.Enabled = True

                With ddlComercio
                    .DataSource = arr
                    .DataTextField = "Descripcion"
                    .DataValueField = "Codigo"
                    .DataBind()
                End With
                Global.Metodos.AgregarItemCombo(ddlComercio)
                ddlComercio.Enabled = True
                txtFecInicio.Enabled = True
                txtFecFin.Enabled = True
                imgCalendarIni.Enabled = True
                imgCalendarFin.Enabled = True
                txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
                txtFecFin.Text = DateSerial(Year(Date.Now), Month(Date.Now) + 1, 0)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
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

                    If Session("MovSCTR") = "VISA" Then
                        gvwRepVisanet.DataSource = Nothing
                        gvwRepVisanet.DataBind()
                        'REPORTE VISANET
                        dt = Metodos.Lista_Report_VISANET(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlComercio.SelectedValue, sfecIni, sfecFin)
                        If dt.Rows.Count > 0 Then
                            Session("dtVisanet") = dt
                            'mExportExcel(dt)
                            gvwRepVisanet.DataSource = dt
                            gvwRepVisanet.DataBind()
                            Tab_ReporteRecibos.Visible = True
                            ImprimirVISANET.Visible = True
                            ddlCanal.Enabled = False
                            ddlProducto.Enabled = False
                            ddlComercio.Enabled = False
                            lblNumListRecibos.Text = Convert.ToInt16(dt.Rows.Count)
                            If dt.Rows.Count > 10 Then
                                ddlListRecibos.Enabled = True
                            Else
                                ddlListRecibos.Enabled = False
                            End If
                        Else
                            Tab_ReporteRecibos.Visible = False
                            gvwRepVisanet.DataSource = Nothing
                            gvwRepVisanet.DataBind()
                            ImprimirVISANET.Visible = False
                            AlertaScripts("No se encontraron Datos")
                        End If
                    Else
                        gvwRepVisanet.DataSource = Nothing
                        gvwRepVisanet.DataBind()
                        'DOCUMENTOS CONTABLES PENDIENTES
                        dt = Metodos.Lista_Report_DC_Pendientes(Metodos.DB.ORACLE, ddlCanal.SelectedValue, ddlProducto.SelectedValue, ddlComercio.SelectedValue, sfecIni, sfecFin)
                        If dt.Rows.Count > 0 Then
                            Session("dtVisanet") = dt
                            mExportExcel_DC_PENDIENTES(dt)
                            Tab_ReporteRecibos.Visible = False
                            ImprimirVISANET.Visible = True
                        Else
                            Tab_ReporteRecibos.Visible = False
                            gvwRepVisanet.DataSource = Nothing
                            gvwRepVisanet.DataBind()
                            ImprimirVISANET.Visible = False
                            AlertaScripts("No se encontraron Datos")
                        End If
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
        Dim sFileName As String = "ReporteComisionesVISA_" & Replace(Date.Now.Date, "/", "") & ".xls"

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
        Response.Write("<table><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td colspan=4><b>REPORTE DETALLE DE TRANSACCIONES VISANET </b></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Canal: </b></td><td colspan=3 align=left><b>" + ddlCanal.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Producto: </b></td><td colspan=2 align=left><b>" + ddlProducto.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Código de Comercio: </b></td><td align=left><b>" + ddlComercio.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Fecha de Abono: </b></td><td><b>" + txtFecInicio.Text + "</b></td><td><b>" + txtFecFin.Text + "</b></td></tr>")
        Response.Write("<tr></tr></table>")
        Response.Write("<style> TABLE { border:dotted 1px #999; } " & _
            "TD { border:dotted 1px #D5D5D5 } td { mso-number-format:\@; } </style>")

        For Each row As GridViewRow In dg.Rows
            row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            row.Cells(4).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(4).Text))
            row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            row.Cells(5).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(5).Text))
            row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            row.Cells(10).HorizontalAlign = HorizontalAlign.Right
            row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            row.Cells(12).HorizontalAlign = HorizontalAlign.Right
            row.Cells(13).HorizontalAlign = HorizontalAlign.Right
            row.Cells(14).HorizontalAlign = HorizontalAlign.Right
            row.Cells(15).HorizontalAlign = HorizontalAlign.Center
            row.Cells(16).HorizontalAlign = HorizontalAlign.Center
            row.Cells(17).HorizontalAlign = HorizontalAlign.Center
            row.Cells(18).HorizontalAlign = HorizontalAlign.Center
            row.Cells(19).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(19).Text))
            row.Cells(19).HorizontalAlign = HorizontalAlign.Right
            row.Cells(20).HorizontalAlign = HorizontalAlign.Center
        Next

        dg.HeaderStyle.Font.Bold = True     ' SET EXCEL HEADERS AS BOLD.
        dg.RenderControl(objHTW)

        Response.Write(objSW.ToString())
        Response.Flush()
        Response.End()
        dg = Nothing

    End Sub

    Protected Sub mExportExcel_DC_PENDIENTES(dt As DataTable)

        Dim dg As New GridView
        dg.DataSource = dt : dg.DataBind()

        ' THE EXCEL FILE.
        Dim sFileName As String = "ReporteDC_Pendientes_" & Replace(Date.Now.Date, "/", "") & ".xls"

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
        Response.Write("<table><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td colspan=4><b>Reporte de Documentos Contables en Estado: Pendiente de Abono </b></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Canal: </b></td><td colspan=3 align=left><b>" + ddlCanal.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Producto: </b></td><td colspan=2 align=left><b>" + ddlProducto.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Código de Comercio: </b></td><td align=left><b>" + ddlComercio.SelectedItem.Text + "</b></td><td></td></tr>")
        Response.Write("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Fecha del Documento: </b></td><td><b>" + txtFecInicio.Text + "</b></td><td><b>" + txtFecFin.Text + "</b></td></tr>")
        Response.Write("<tr></tr></table>")
        Response.Write("<style> TABLE { border:dotted 1px #999; } " & _
            "TD { border:dotted 1px #D5D5D5 } td { mso-number-format:\@; } </style>")

        For Each row As GridViewRow In dg.Rows
            row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            row.Cells(2).HorizontalAlign = HorizontalAlign.Left
            row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            row.Cells(4).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(4).Text))
            row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            row.Cells(5).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(5).Text))
            row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            row.Cells(6).Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells(6).Text))
            row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            row.Cells(10).HorizontalAlign = HorizontalAlign.Right
            row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            row.Cells(12).HorizontalAlign = HorizontalAlign.Right
            row.Cells(13).HorizontalAlign = HorizontalAlign.Right
            row.Cells(14).HorizontalAlign = HorizontalAlign.Center
            row.Cells(15).HorizontalAlign = HorizontalAlign.Center
            row.Cells(16).HorizontalAlign = HorizontalAlign.Center
            row.Cells(17).HorizontalAlign = HorizontalAlign.Center
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
        'txtFecInicio.Text = ""
        'txtFecFin.Text = ""
        txtFecInicio.Text = DateSerial(Year(Date.Now), Month(Date.Now), 1)
        txtFecFin.Text = DateSerial(Year(Date.Now), Month(Date.Now) + 1, 0)
        ddlCanal.Enabled = True
        imbAceptar1.Enabled = True
        gvwRepVisanet.DataSource = Nothing
        gvwRepVisanet.DataBind()

    End Sub

    Protected Sub ImprimirVISANET_Click(sender As Object, e As ImageClickEventArgs) Handles ImprimirVISANET.Click
        Try
            gvwRepVisanet.PageSize = Convert.ToInt16(ddlListRecibos.SelectedValue)
            Dim dt As DataTable
            dt = Session("dtVisanet")
            If dt.Rows.Count > 0 Then
                mExportExcel(dt)
            Else
                AlertaScripts("No se encontraron Datos")
            End If
        Catch ex As Exception
            AlertaScripts("No se puede imprimir")
        End Try

    End Sub

    Protected Sub ddlListRecibos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListRecibos.SelectedIndexChanged
        gvwRepVisanet.PageSize = Convert.ToInt16(ddlListRecibos.SelectedValue)
        Dim dt As DataTable
        dt = Session("dtVisanet")
        gvwRepVisanet.DataSource = dt
        gvwRepVisanet.DataBind()
    End Sub

    Protected Sub gvwRepVisanet_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvwRepVisanet.PageIndexChanging
        gvwRepVisanet.PageIndex = e.NewPageIndex
        Dim dt As DataTable
        dt = Session("dtVisanet")
        gvwRepVisanet.DataSource = dt
        gvwRepVisanet.DataBind()
    End Sub
End Class
