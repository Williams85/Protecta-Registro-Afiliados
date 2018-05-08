Imports System.Data

Partial Class Renovaciones
    Inherits System.Web.UI.Page
    Dim Metodos As New Metodos
    Dim dtRenovacion As DataTable
   Dim dtsLoadRenovacion As New DataSet
   Private isEditMode As Boolean = False

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try

            If HF_TIPO_RECIBO.Value = "1" Then
                Dim fechaPeriodo As Date = txtFecha.Text
                dtRenovacion = Metodos.Lista_Renovacion(Metodos.DB.ORACLE, ddlProducto.SelectedValue, IIf(txtPoliza.Visible = True, txtPoliza.Text, 0), fechaPeriodo.Year & fechaPeriodo.Month.ToString.PadLeft(2, "0"), IIf(HF_TIPO_RECIBO.Value = "1", "P", "C"))
            Else
                If rblTipo.Items(2).Selected = True Then
                    dtRenovacion = Metodos.Lista_Renovacion(Metodos.DB.ORACLE, ddlProducto.SelectedValue, IIf(txtPoliza.Visible = True, txtPoliza.Text, 0), "", "R")
                Else
                    dtRenovacion = Metodos.Lista_Renovacion(Metodos.DB.ORACLE, ddlProducto.SelectedValue, IIf(txtPoliza.Visible = True, txtPoliza.Text, 0), "", IIf(HF_TIPO_RECIBO.Value = "1", "P", "C"))
            End If

            End If

            Session.Add("LoadRenovacion", dtRenovacion)

            If HF_TIPO_RECIBO.Value = "1" Then 'POLIZA

                gvDetalle.Visible = False
                gvDetalleP.DataSource = dtRenovacion
                gvDetalleP.DataBind()
                gvDetalleP.Visible = True
                If dtRenovacion.Rows.Count > 0 Then
                    txtPoliza.Text = dtRenovacion.Rows(0).Item(0).ToString
                    txtNumReg.Text = dtRenovacion.Compute("count(POLIZA)", "").ToString
                    txtTotalPrima.Text = Math.Round(dtRenovacion.Compute("Sum(PRIMA)", ""), 2).ToString
                Else
                    txtNumReg.Text = "0"
                    txtTotalPrima.Text = "0"
                    AlertaScripts("No existen datos a mostrar")
                End If
            Else
                gvDetalleP.Visible = False
                gvDetalle.DataSource = dtRenovacion
                gvDetalle.DataBind()
                gvDetalle.Visible = True
                If dtRenovacion.Rows.Count > 0 Then
                    txtNumReg.Text = dtRenovacion.Compute("count(Cantidad_Cert)", "").ToString
                    txtTotalPrima.Text = Math.Round(dtRenovacion.Compute("Sum(Prima)", ""), 2).ToString
                Else
                    txtNumReg.Text = "0"
                    txtTotalPrima.Text = "0"
                    AlertaScripts("No existen datos a mostrar")
                End If
            End If

        Catch ex As Exception
            AlertaScripts("No se pudo cargar los datos")
        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Me.btnGrabar.Attributes.Add("onclick", "return preguntar('Al procHesar creara las tareas necesarias y registrará asientos contables. ¿Desea Continuar?');") 'AMARRO ALA FUNCION JAVASCRIP

            If Not Page.IsPostBack Then
                Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")

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

            End If

        Catch ex As Exception
            AlertaScripts("Los registros de cargas no se pudo cargar")
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
                    'Exit For
                    'polizaAct = gvDetalleP.Rows(row.RowIndex).Cells(1).Text

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
            'ModalPopupExtender1.Show()

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
                'If hide.Value = "SI" Then
                'System.Threading.Thread.Sleep(1000)
                If HF_TIPO_RECIBO.Value = "1" Then 'poliza
                    For Each row As GridViewRow In gvDetalleP.Rows
                        Dim cb As CheckBox = row.FindControl("ChkSel")
                        If cb IsNot Nothing AndAlso cb.Checked Then
                     'vFechaFactF = CDate(hidden2.Value.ToString)

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
                        ' vIntermed = IIf(gvDetalleP.Rows(row.RowIndex).Cells(12).Text = "&nbsp;", "", gvDetalleP.Rows(row.RowIndex).Cells(12).Text)
                                System.Threading.Thread.Sleep(500)

                        Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 0, vProducto, vRamo, Session("CodUsuario"), dtc)

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
                           ' vIntermed = IIf(gvDetalleP.Rows(row.RowIndex).Cells(12).Text = "&nbsp;", "", gvDetalleP.Rows(row.RowIndex).Cells(12).Text)
                                    System.Threading.Thread.Sleep(500)

                           Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 0, vProducto, vRamo, Session("CodUsuario"), dtc)
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
                     vFechaFactI = Date.Now 'gvDetalle.Rows(row.RowIndex).Cells(8).Text

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

                                'If gvDetalle.Rows(row.RowIndex).Cells(11).Text = "&nbsp;" Then
                                vIntermed = 0
                                'Else
                                '    vIntermed = gvDetalle.Rows(row.RowIndex).Cells(11).Text
                                'End If
                                System.Threading.Thread.Sleep(500)
                        Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 1, vProducto, vRamo, Session("CodUsuario"), dtc)

                            Else
                                If Len(Trim(vkey)) = 0 Or vkey = "&nbsp;," Then
                           año = vFechaFactI.Year.ToString
                           mes = vFechaFactI.Month.ToString.PadLeft(2, "0")
                           dia = vFechaFactI.Day.ToString.PadLeft(2, "0")

                           vFechaFactI2 = año & mes & dia

                           vFechaFactS = 0

                                    'If gvDetalle.Rows(row.RowIndex).Cells(11).Text = "&nbsp;" Then
                                    vIntermed = 0
                                    'Else
                                    '    vIntermed = gvDetalle.Rows(row.RowIndex).Cells(11).Text
                                    'End If
                                    System.Threading.Thread.Sleep(500)
                           Metodos.Crea_Job_Renovaciones(Metodos.DB.ORACLE, vPoliza, vCertificado, vFechaFactI2, vFechaFactS, vIntermed, 1, vProducto, vRamo, Session("CodUsuario"), dtc)

                                End If
                            End If

                        End If
                    Next
                    If vkey <> "" Then
                        vkey = Mid(vkey, 1, Len(vkey) - 1)
                    End If

                End If

                'End If
                If Len(Trim(vkey)) = 0 And FlagNreceipt = False Then
                    AlertaScripts("Proceso terminado")

                Else
                    AlertaScripts("Proceso terminado. Hubieron registros que no se procesaron por existir tareas ya creadas para las mismas caracteristicas. Las siguientes son las tareas encontradas:" & vkey)
                End If

                Call btnBuscar_Click(sender, e)


            Catch ex As Exception
                'AlertaScripts("No se pudo generar las tareas")
                Throw ex
                Return
            End Try
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
        Response.Redirect("Menu.aspx")
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

    ' Protected Sub rblTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblTipo.SelectedIndexChanged

    'Dim TIPO As Integer
    '    TIPO = HF_TIPO_RECIBO.Value
    'ElseIf rblTipo.Items(0).Selected = True Then
    '    HF_TIPO_RECIBO.Value = "1"
    'Else
    '    If rblTipo.Items(2).Selected = True Then
    '        HF_TIPO_RECIBO.Value = "2"
    '    Else
    '        HF_TIPO_RECIBO.Value = TIPO
    '    End If
    'End Sub

   Protected Sub txtFecha_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

   End Sub


   Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)



      Dim valor As String
      Dim count As Integer = 1
      Dim fechafin As DateTime
      valor = hidden2.Value


      If valor.ToString.Trim = "" Then
         AlertaScripts("Se debe ingresar Fecha Fin de Renovación")

      Else

         'if FormatDateTime(txtFecha.Text, DateFormat.ShortDate) > CDate(valor.ToString.Trim)
         If Not DateTime.TryParse(valor.ToString.Trim, fechafin) Then
            AlertaScripts("La fecha ingresada no es válida")
         Else

            If CDate(valor.ToString.Trim).Year > "2000" And FormatDateTime(txtFecha.Text, DateFormat.ShortDate) < CDate(valor.ToString.Trim) Then
               Dim dtRenova As DataTable = Session("LoadRenovacion")

               For Each row As DataRow In dtRenova.Rows
                  If count = hidden3.Value Then
                     row("Fecha_Fin_Renova") = valor
                  End If
                  count += 1
               Next
               gvDetalleP.DataSource = dtRenova
               gvDetalleP.DataBind()
            ElseIf CDate(valor.ToString.Trim).Year < "2000" Then
               AlertaScripts("El año ingresado no es válido")
            ElseIf FormatDateTime(txtFecha.Text, DateFormat.ShortDate) > CDate(valor.ToString.Trim) Then
               AlertaScripts("La Fecha de Renovación Final debe ser mayor a la Fecha de Renovación Inicial")
            ElseIf FormatDateTime(txtFecha.Text, DateFormat.ShortDate) = CDate(valor.ToString.Trim) Then
               AlertaScripts("La Fecha de Renovación Final no debe ser igual a la Fecha de Renovación Inicial")
            End If


         End If

      End If



   End Sub
End Class


