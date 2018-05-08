Imports System.Data
Partial Class RenovacionesHCRP
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtRenovacion As DataTable
    Dim dtsLoadRenovacion As New DataSet
    Private isEditMode As Boolean = False

    Public Sub LimpiaSeleccionTabReemplazo(Optional ByVal tipo As Integer = 0)
        Try
            Dim ds As DataSet = New DataSet
            ds.ReadXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")

            Dim i As Integer = 0

            For i = 0 To ds.Tables("Carga").Rows.Count - 1
                If (ds.Tables("Carga").Rows(i)("NroVentana").ToString = HF_NUMPAGINA.Value.ToString()) Then
                    ds.Tables("Carga").Rows(i).Delete()
                    ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try
    End Sub

    Public Function AdicionaPolizaPorControl(Optional ByVal tipo As Integer = 0) As Integer
        Try
            Dim resultado As Integer = 0
            'HF_NUMPAGINA
            'validamos que la poliza no este tomada en otro formulario
            Dim procede As Integer = 1
            'validamos si ya fue seleccionada la poliza
            Dim ds As DataSet = New DataSet
            ds.ReadXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")

            Dim valorrec As String = ""
            Dim i As Integer = 0
            For i = 0 To ds.Tables("Carga").Rows.Count - 1

                If (ds.Tables("Carga").Rows(i)("Tipo").ToString = tipo.ToString) Then

                    If (ds.Tables("Carga").Rows(i)("NroVentana").ToString <> HF_NUMPAGINA.Value.ToString()) Then
                        If (ds.Tables("Carga").Rows(i)("Producto").ToString = ddlProducto.SelectedValue.ToString() And ds.Tables("Carga").Rows(i)("Fecha").ToString = txtFecha.Text) Then
                            AlertaScripts("Ya se tiene filtrado por este Producto y fecha en en Recibo Nro " & ds.Tables("Carga").Rows(i)("NroVentana").ToString)
                            resultado = 1
                            txtFecha.Text = ""
                            txtFecha.Focus()
                            procede = 0
                            Exit For

                        End If
                    Else
                        'Si es la misma ventana limpia registro para insertar el nuevo
                        For e = 0 To ds.Tables("Carga").Rows.Count - 1
                            If (ds.Tables("Carga").Rows(e)("NroVentana").ToString = HF_NUMPAGINA.Value.ToString()) Then
                                ds.Tables("Carga").Rows(e).Delete()
                                ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")
                                Exit For
                            End If
                        Next
                    End If

                Else
                    If (ds.Tables("Carga").Rows(i)("NroVentana").ToString <> HF_NUMPAGINA.Value.ToString()) Then
                        If (ds.Tables("Carga").Rows(i)("Producto").ToString = ddlProducto.SelectedValue.ToString() And ds.Tables("Carga").Rows(i)("Poliza").ToString = txtPoliza.Text) Then
                            AlertaScripts("Poliza ya esta seleccionada en Recibo Nro " & ds.Tables("Carga").Rows(i)("NroVentana").ToString)
                            resultado = 1
                            txtPoliza.Text = ""
                            txtPoliza.Focus()
                            procede = 0
                            Exit For

                        End If
                    Else
                        'Si es la misma ventana limpia registro para insertar el nuevo
                        For e = 0 To ds.Tables("Carga").Rows.Count - 1
                            If (ds.Tables("Carga").Rows(e)("NroVentana").ToString = HF_NUMPAGINA.Value.ToString()) Then
                                ds.Tables("Carga").Rows(e).Delete()
                                ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")
                                Exit For
                            End If
                        Next
                    End If

                End If
            Next

            'Inserta sin no existe
            If procede > 0 Then
                Dim fila As DataRow
                fila = ds.Tables("Carga").NewRow
                fila("NroVentana") = HF_NUMPAGINA.Value
                fila("Tipo") = tipo.ToString()
                fila("Producto") = ddlProducto.SelectedValue
                If tipo = 2 Then
                    fila("Poliza") = txtPoliza.Text
                    fila("Fecha") = ""
                Else
                    fila("Poliza") = ""
                    fila("Fecha") = txtFecha.Text
                End If

                ds.Tables("Carga").Rows.Add(fila)
                ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")

            Else
                AlertaScripts("no existen datos para mostrar")

            End If

            Return resultado
        Catch ex As Exception

        End Try

    End Function
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try


            If HF_TIPO_RECIBO.Value = "1" Then
                'validacion por fecha
                Dim fechaPeriodo As Date = txtFecha.Text
                dtRenovacion = Metodos.Lista_Renovacion(Metodos.DB.ORACLE, ddlProducto.SelectedValue, IIf(txtPoliza.Visible = True, txtPoliza.Text, 0), fechaPeriodo.Year & fechaPeriodo.Month.ToString.PadLeft(2, "0"), IIf(HF_TIPO_RECIBO.Value = "1", "P", "C"))

            Else
                'validacion por poliza
                If rblTipo.Items(2).Selected = True Then
                    dtRenovacion = Metodos.Lista_Renovacion(Metodos.DB.ORACLE, ddlProducto.SelectedValue, IIf(txtPoliza.Visible = True, txtPoliza.Text, 0), "", "R")
                Else
                    'Aquellos que usan la casilla para el ingreso de la poliza
                    dtRenovacion = Metodos.Lista_Renovacion(Metodos.DB.ORACLE, ddlProducto.SelectedValue, IIf(txtPoliza.Visible = True, txtPoliza.Text, 0), "", IIf(HF_TIPO_RECIBO.Value = "1", "P", "C"))
                End If

            End If

            Session.Add("LoadRenovacion" & HF_NUMPAGINA.Value.ToString(), dtRenovacion)

            If HF_TIPO_RECIBO.Value = "1" Then 'POLIZA

                gvDetalle.Visible = False
                gvDetalleP.DataSource = dtRenovacion
                gvDetalleP.DataBind()
                gvDetalleP.Visible = True
                If dtRenovacion.Rows.Count > 0 Then
                    txtPoliza.Text = dtRenovacion.Rows(0).Item(0).ToString
                    txtNumReg.Text = dtRenovacion.Compute("count(POLIZA)", "").ToString
                    txtTotalPrima.Text = Math.Round(dtRenovacion.Compute("Sum(PRIMA)", ""), 2).ToString
                    If AdicionaPolizaPorControl(1) > 0 Then
                        gvDetalleP.Visible = False
                        gvDetalle.Visible = False
                        txtNumReg.Text = "0"
                        txtTotalPrima.Text = "0"
                        txtFecha.Text = ""
                        txtFecha.Focus()
                    End If


                Else
                    txtNumReg.Text = "0"
                    txtTotalPrima.Text = "0"
                    AlertaScripts("No existen datos a mostrar en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                    LimpiaSeleccionTabReemplazo()
                End If
            Else
                gvDetalleP.Visible = False
                gvDetalle.DataSource = dtRenovacion
                gvDetalle.DataBind()
                gvDetalle.Visible = True
                If dtRenovacion.Rows.Count > 0 Then
                    txtNumReg.Text = dtRenovacion.Compute("count(Cantidad_Cert)", "").ToString
                    txtTotalPrima.Text = Math.Round(dtRenovacion.Compute("Sum(Prima)", ""), 2).ToString
                    If AdicionaPolizaPorControl(2) > 0 Then
                        gvDetalleP.Visible = False
                        gvDetalle.Visible = False
                        txtNumReg.Text = "0"
                        txtTotalPrima.Text = "0"
                        txtPoliza.Text = ""
                        txtPoliza.Focus()
                    End If
                Else
                    txtNumReg.Text = "0"
                    txtTotalPrima.Text = "0"
                    AlertaScripts("No existen datos a mostrar en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                    LimpiaSeleccionTabReemplazo()
                End If
            End If

        Catch ex As Exception
            AlertaScripts("No se pudo cargar los datos  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
        End Try

    End Sub

    Public Sub RecargaPagina()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                dtsLoadRenovacion = Metodos.Lista_Load_Renovacion(Metodos.DB.ORACLE)
                Session.Add("Load_Renovacion", dtsLoadRenovacion)

                With ddlRamo
                    .DataSource = dtsLoadRenovacion.Tables(0)
                    .DataTextField = "sdescript"
                    .DataValueField = "nbranch"
                    .DataBind()
                End With
                Global.Metodos.AgregarItemCombo(ddlRamo)

                ddlProducto.Enabled = False
                txtPoliza.Visible = False
                txtPoliza.Visible = False
                txtNumReg.Text = "0"
                txtTotalPrima.Text = "0"

                rblTipo.Items(1).Selected = True
                HF_NUMPAGINA.Value = Request.QueryString("nuevapag")
                HF_RUTA_MASIVA.Value = ConfigurationManager.AppSettings("RutaMasiva").ToString()
                HF_PROSIGUE.Value = 0
            End If

        Catch ex As Exception
            AlertaScripts("Los registros de cargas no se pudo cargar  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
        End Try
    End Sub

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub

    Protected Sub ddlRamo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRamo.SelectedIndexChanged

        If ddlRamo.SelectedIndex = 0 Then
            ddlProducto.Enabled = False
        Else
            ddlProducto.Enabled = True
            dtsLoadRenovacion = Session("Load_Renovacion")
            Dim arr As New ArrayList
            For Each fila As DataRow In dtsLoadRenovacion.Tables(1).Select("nbranch='" & ddlRamo.SelectedValue & "'")
                arr.Add(New Listado(fila("nproduct"), fila("sdescript")))
            Next

            With ddlProducto
                .DataSource = arr
                .DataTextField = "Descripcion"
                .DataValueField = "Codigo"
                .DataBind()
            End With
            Global.Metodos.AgregarItemCombo(ddlProducto)
        End If



    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim FlagNreceipt As Boolean = False
        Dim vCant As Double = 0
        Dim polizaAct As Int32 = 0
        Dim vCantPol As Double = 0

        Dim tarea As String = ""
        Dim codTarea As String = ""
        Dim año As String = ""
        Dim mes As String = ""
        Dim dia As String = ""

        Dim sEstadoJob As String

        If HF_TIPO_RECIBO.Value = "1" Then 'poliza
            For Each row As GridViewRow In gvDetalleP.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    If polizaAct <> gvDetalleP.Rows(row.RowIndex).Cells(1).Text Then
                        polizaAct = gvDetalleP.Rows(row.RowIndex).Cells(1).Text
                        vCantPol = 1
                    Else
                        vCantPol = vCantPol + 1
                        If vCantPol > 1 Then
                            AlertaScripts("No se puede marcar mas de un registro de la misma póliza")
                            Exit Sub
                        End If
                    End If
                    vCant = vCant + 1
                End If
            Next
        Else
            For Each row As GridViewRow In gvDetalle.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    vCant = vCant + 1
                    Exit For
                End If
            Next
        End If
        If vCant = 0 Then
            AlertaScripts("No ha marcado ningún registro para procesar")
        Else
            mpeCarga.Show()
        End If

    End Sub



    Protected Sub Marcar(ByVal checkState As Boolean)
        If HF_TIPO_RECIBO.Value = "1" Then 'poliza
            For Each row As GridViewRow In gvDetalleP.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing Then
                    cb.Checked = checkState
                End If
            Next
        Else
            For Each row As GridViewRow In gvDetalle.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                Dim codTarea As String = gvDetalle.Rows(row.RowIndex).Cells(7).Text 'gvDetalle.Rows(row.RowIndex).Cells(13).Text
                If cb IsNot Nothing And (codTarea = "" Or codTarea = "&nbsp;" Or codTarea = "04") Then
                    cb.Checked = checkState
                End If
            Next
        End If

    End Sub

    Protected Sub rblmarcar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblmarcar.SelectedIndexChanged
        If rblmarcar.Items(0).Selected Then
            System.Threading.Thread.Sleep(500)
            Marcar(True)
        Else
            System.Threading.Thread.Sleep(500)
            Marcar(False)
        End If
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType, "redirect", "if(top!=self) {top.location.href = 'Menu.aspx';}", True)
    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged

        Try
            Dim dt As DataTable = Metodos.Lista_Conf_producto(Metodos.DB.ORACLE, ddlRamo.SelectedValue, ddlProducto.SelectedValue)
            If dt.Rows(0).Item(0).ToString = "2" Then
                lblRecibo.Text = "Por Certificado"
                HF_TIPO_RECIBO.Value = dt.Rows(0).Item(0).ToString
                txtPoliza.Visible = True
                lblPoliza.Visible = True
                lblFechaPeriodo.Visible = False
                txtFecha.Visible = False
                ImgbFecha.Visible = False
            Else
                lblRecibo.Text = "Por Póliza"
                HF_TIPO_RECIBO.Value = dt.Rows(0).Item(0).ToString
                txtPoliza.Visible = False
                lblPoliza.Visible = False
                lblFechaPeriodo.Visible = True
                txtFecha.Visible = True
                ImgbFecha.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ValidaRegistro(ByVal grilla As GridView)

        Dim cod As Integer
        For Each row As GridViewRow In grilla.Rows
            cod = grilla.Rows(row.RowIndex).Cells(12).Text.ToString()
            If cod = 4 Or cod = "&nbsp;" Then

            End If
        Next


    End Sub


    Protected Sub gvDetalle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        gvDetalle.PageIndex = e.NewPageIndex
        Call btnBuscar_Click(sender, e)
    End Sub

    Protected Sub gvDetalleP_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs)
        gvDetalleP.PageIndex = e.NewSelectedIndex
        Call btnBuscar_Click(sender, e)
    End Sub

    Protected Sub txtFecha_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub


    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim valor As String
        Dim count As Integer = 1
        Dim fechafin As DateTime
        valor = hidden2.Value


        If valor.ToString.Trim = "" Then
            AlertaScripts("Se debe ingresar Fecha Fin de Renovación en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())

        Else
            If Not DateTime.TryParse(valor.ToString.Trim, fechafin) Then
                AlertaScripts("La fecha ingresada no es válida en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
            Else
                If CDate(valor.ToString.Trim).Year > "2000" And FormatDateTime(txtFecha.Text, DateFormat.ShortDate) < CDate(valor.ToString.Trim) Then
                    Dim dtRenova As DataTable = Session("LoadRenovacion" & HF_NUMPAGINA.Value.ToString())

                    For Each row As DataRow In dtRenova.Rows
                        If count = hidden3.Value Then
                            row("Fecha_Fin_Renova") = valor
                        End If
                        count += 1
                    Next
                    gvDetalleP.DataSource = dtRenova
                    gvDetalleP.DataBind()
                ElseIf CDate(valor.ToString.Trim).Year < "2000" Then
                    AlertaScripts("El año ingresado no es válido en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                ElseIf FormatDateTime(txtFecha.Text, DateFormat.ShortDate) > CDate(valor.ToString.Trim) Then
                    AlertaScripts("La Fecha de Renovación Final debe ser mayor a la Fecha de Renovación Inicial  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                ElseIf FormatDateTime(txtFecha.Text, DateFormat.ShortDate) = CDate(valor.ToString.Trim) Then
                    AlertaScripts("La Fecha de Renovación Final no debe ser igual a la Fecha de Renovación Inicial  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                End If


            End If

        End If



    End Sub


    Protected Sub imbCancelarCarga_Click(sender As Object, e As ImageClickEventArgs) Handles imbCancelarCarga.Click

    End Sub

    Protected Sub imbAceptarCarga_Click(sender As Object, e As ImageClickEventArgs) Handles imbAceptarCarga.Click
        Dim FlagNreceipt As Boolean = False
        Dim vCant As Double = 0
        Dim polizaAct As Int32 = 0
        Dim vCantPol As Double = 0

        Dim tarea As String = ""
        Dim codTarea As String = ""
        Dim año As String = ""
        Dim mes As String = ""
        Dim dia As String = ""

        Dim sEstadoJob As String


        If HF_TIPO_RECIBO.Value = "1" Then 'poliza
            For Each row As GridViewRow In gvDetalleP.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    If polizaAct <> gvDetalleP.Rows(row.RowIndex).Cells(1).Text Then
                        polizaAct = gvDetalleP.Rows(row.RowIndex).Cells(1).Text
                        vCantPol = 1
                    Else
                        vCantPol = vCantPol + 1
                        If vCantPol > 1 Then
                            AlertaScripts("No se puede marcar mas de un registro de la misma póliza")
                            Exit Sub
                        End If
                    End If
                    vCant = vCant + 1
                End If
            Next
        Else
            For Each row As GridViewRow In gvDetalle.Rows
                Dim cb As CheckBox = row.FindControl("ChkSel")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    vCant = vCant + 1
                    Exit For
                End If
            Next
        End If


        If vCant = 0 Then
            AlertaScripts("No ha marcado ningún registro para procesar")
        Else
            Dim vPoliza As Int64
            Dim vCertificado As Int64
            Dim vIntermed As Int64
            Dim vFechaFactI As Date
            Dim vFechaFactI2 As Integer = 0

            Dim vFechaFactS As Integer = 0
            Dim vRamo As Integer = 0
            Dim vProducto As Integer = 0
            Dim vkey As String = ""


            Try
                Dim dtc As DataTable = Metodos.Lista_Conf_Remmp_Renov(Metodos.DB.ORACLE, 0, ddlProducto.SelectedValue).Tables(1)
                If HF_TIPO_RECIBO.Value = "1" Then 'poliza
                    For Each row As GridViewRow In gvDetalleP.Rows
                        Dim cb As CheckBox = row.FindControl("ChkSel")
                        If cb IsNot Nothing AndAlso cb.Checked Then
                            vRamo = ddlRamo.SelectedValue
                            vProducto = ddlProducto.SelectedValue
                            vPoliza = gvDetalleP.Rows(row.RowIndex).Cells(1).Text
                            vCertificado = 0
                            vFechaFactI = gvDetalleP.Rows(row.RowIndex).Cells(8).Text


                            Dim DTnreceip As DataTable = Metodos.Lista_Existe_Nreceipt_null(Metodos.DB.ORACLE, 1, vProducto, vPoliza)
                            Dim fila() As DataRow = DTnreceip.Select("")
                            If fila.Length > 0 Then
                                If fila(0)(0).ToString = 0 Then 'no encontro registros q tubieran nreceipt vacio en la life 
                                    FlagNreceipt = True
                                End If
                            End If
                            'almacena key para mostrar los registros q ya tienen tarea hecha
                            tarea = gvDetalleP.Rows(row.RowIndex).Cells(13).Text
                            codTarea = gvDetalleP.Rows(row.RowIndex).Cells(14).Text

                            'solo procesa los q tienen codigo 04 o en blanco
                            If (codTarea = "" Or codTarea = "&nbsp;" Or codTarea = "04") Then
                                vkey = vkey & ""
                            Else
                                vkey = vkey & gvDetalleP.Rows(row.RowIndex).Cells(13).Text & ","
                            End If


                            If rblTipo.Items(0).Selected = True Then 'carga
                                vFechaFactI = FormatDateTime(txtFecha.Text, DateFormat.ShortDate)
                                año = vFechaFactI.Year.ToString
                                mes = vFechaFactI.Month.ToString.PadLeft(2, "0")
                                dia = vFechaFactI.Day.ToString.PadLeft(2, "0")
                                vFechaFactI2 = año & mes & dia



                                vFechaFactS = CDate(gvDetalleP.Rows(row.RowIndex).Cells(9).Text).Year.ToString & CDate(gvDetalleP.Rows(row.RowIndex).Cells(9).Text).Month.ToString.PadLeft(2, "0") & CDate(gvDetalleP.Rows(row.RowIndex).Cells(9).Text).Day.ToString.PadLeft(2, "0")


                                If gvDetalleP.Rows(row.RowIndex).Cells(12).Text = "&nbsp;" Then
                                    vIntermed = 0
                                Else
                                    vIntermed = gvDetalleP.Rows(row.RowIndex).Cells(12).Text
                                End If
                                System.Threading.Thread.Sleep(500)

                                sEstadoJob = Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 0, vProducto, vRamo, Session("CodUsuario"), dtc)

                            Else 'POLIZA
                                If (Len(Trim(vkey)) = 0 Or vkey = "&nbsp;," Or (Len(Trim(vkey)) > 0 And FlagNreceipt = False)) Then
                                    año = vFechaFactI.Year.ToString
                                    mes = vFechaFactI.Month.ToString.PadLeft(2, "0")
                                    dia = vFechaFactI.Day.ToString.PadLeft(2, "0")
                                    vFechaFactI2 = año & mes & dia

                                    vFechaFactS = CDate(gvDetalleP.Rows(row.RowIndex).Cells(9).Text).Year.ToString & CDate(gvDetalleP.Rows(row.RowIndex).Cells(9).Text).Month.ToString.PadLeft(2, "0") & CDate(gvDetalleP.Rows(row.RowIndex).Cells(9).Text).Day.ToString.PadLeft(2, "0")

                                    If gvDetalleP.Rows(row.RowIndex).Cells(12).Text = "&nbsp;" Then
                                        vIntermed = 0
                                    Else
                                        vIntermed = gvDetalleP.Rows(row.RowIndex).Cells(12).Text
                                    End If
                                    System.Threading.Thread.Sleep(500)

                                    sEstadoJob = Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 0, vProducto, vRamo, Session("CodUsuario"), dtc)
                                End If
                            End If


                        End If
                    Next
                    If vkey <> "" Then
                        vkey = Mid(vkey, 1, Len(vkey) - 1)
                    End If

                    'MOSTRAR FECHA FIN DE VIGENCIA

                Else

                    vRamo = ddlRamo.SelectedValue
                    vProducto = ddlProducto.SelectedValue
                    vPoliza = txtPoliza.Text


                    For Each row As GridViewRow In gvDetalle.Rows
                        Dim cb As CheckBox = row.FindControl("ChkSel")
                        If cb IsNot Nothing AndAlso cb.Checked Then

                            System.Threading.Thread.Sleep(500)
                            vCertificado = gvDetalle.Rows(row.RowIndex).Cells(1).Text
                            vFechaFactI = Date.Now

                            'almacena key para mostrar los registros q ya tienen tarea hecha
                            tarea = gvDetalle.Rows(row.RowIndex).Cells(5).Text
                            codTarea = gvDetalle.Rows(row.RowIndex).Cells(6).Text

                            'solo procesa los q tienen codigo 04 o en blanco
                            If (codTarea = "" Or codTarea = "&nbsp;" Or codTarea = "04") Then
                                vkey = vkey & ""
                            Else
                                vkey = vkey & gvDetalle.Rows(row.RowIndex).Cells(5).Text & ","
                            End If

                            If rblTipo.Items(0).Selected = True Then
                                If txtFecha.Visible = True Then
                                    vFechaFactI = FormatDateTime(txtFecha.Text, DateFormat.ShortDate)
                                End If
                                año = vFechaFactI.Year.ToString
                                mes = vFechaFactI.Month.ToString.PadLeft(2, "0")
                                dia = vFechaFactI.Day.ToString.PadLeft(2, "0")

                                vFechaFactI2 = año & mes & dia

                                vFechaFactS = 0
                                vIntermed = 0

                                System.Threading.Thread.Sleep(500)
                                sEstadoJob = Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 1, vProducto, vRamo, Session("CodUsuario"), dtc)

                            Else
                                If Len(Trim(vkey)) = 0 Or vkey = "&nbsp;," Then
                                    año = vFechaFactI.Year.ToString
                                    mes = vFechaFactI.Month.ToString.PadLeft(2, "0")
                                    dia = vFechaFactI.Day.ToString.PadLeft(2, "0")

                                    vFechaFactI2 = año & mes & dia

                                    vFechaFactS = 0
                                    vIntermed = 0
                                    System.Threading.Thread.Sleep(500)
                                    sEstadoJob = Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 1, vProducto, vRamo, Session("CodUsuario"), dtc)

                                End If
                            End If

                        End If
                    Next
                    If vkey <> "" Then
                        vkey = Mid(vkey, 1, Len(vkey) - 1)
                    End If

                End If
                If sEstadoJob = "5" Then
                    AlertaScripts("Ejecución exitosa  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                Else
                    AlertaScripts("Ocurrio un error al Ejecutar el Proceso  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                End If
                Call limpiaSeleccion()
                Call btnBuscar_Click(sender, e)

            Catch ex As Exception
                AlertaScripts("No se pudo generar las tareas  en Gen. Rec Nro " & HF_NUMPAGINA.Value.ToString())
                Throw ex
                Return
            End Try
        End If
        PnelCarga.Visible = False
        mpeCarga.Hide()

    End Sub
    Public Sub limpiaSeleccion()
        Try
            Dim ds As DataSet = New DataSet
            ds.ReadXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")
            For e = 0 To ds.Tables("Carga").Rows.Count - 1
                If (ds.Tables("Carga").Rows(e)("NroVentana").ToString = HF_NUMPAGINA.Value.ToString()) Then
                    ds.Tables("Carga").Rows(e).Delete()
                    ds.WriteXml(HF_RUTA_MASIVA.Value & "/TabRecSel/SelTabRecPol.xml")
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class



