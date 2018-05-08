﻿Partial Class Menu
    Inherits System.Web.UI.Page

    Protected Sub MenuPrincipal_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles MenuPrincipal.MenuItemClick
        Session("MovSCTR") = ""
        Session("TipoDocumento") = ""

        If MenuPrincipal.SelectedValue = "mnuCargaMasivaAsegurados" Then
            Response.Redirect("CargaMasivaAsegurados.aspx")
        End If

        'INICIO RQ2017-0100001 EFITEC-MCM
        If MenuPrincipal.SelectedValue = "mnuNuevoProcesoCarga" Then
            Response.Redirect("ProcesosPCR.aspx")
        End If

        If MenuPrincipal.SelectedValue = "mnuNuevaGeneracionRecibos" Then
            Response.Redirect("RenovacionesPCRP.aspx")
        End If
        'FIN RQ2017-0100001 EFITEC-MCM


        If MenuPrincipal.SelectedValue = "Emision" Then
            Session("MovSCTR") = "EM"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Inclusion" Then
            Session("MovSCTR") = "IN"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Renovacion" Then
            Session("MovSCTR") = "RE"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Exclusion" Then
            Session("MovSCTR") = "EX"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "IncxExclu" Then
            Session("MovSCTR") = "IXE"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Asegurados" Then
            Session("MovSCTR") = "AS"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Recibos" Then
            Session("MovSCTR") = "PR"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "RecibosRegula" Then
            Session("MovSCTR") = "PRR"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Factura" Then
            Session("MovSCTR") = "DC"
            Session("TipoDocumento") = "FAC"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "NotaCredito" Then
            Session("MovSCTR") = "DC"
            Session("TipoDocumento") = "NC"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "DocumentosEstados" Then
            Session("MovSCTR") = "EDC"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Comisiones" Then
            Session("MovSCTR") = "CO"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        'INICIO RQ2017-016 MEJORAS SCTR
        If MenuPrincipal.SelectedValue = "ComisionesVISANET" Then
            Session("MovSCTR") = "VISA"
            Response.Redirect("ProcesosSCTR_Mov.aspx")
        End If

        If MenuPrincipal.SelectedValue = "ReporteVISANET" Then
            Session("MovSCTR") = "VISA"
            Response.Redirect("RepVISANET.aspx")
        End If

        If MenuPrincipal.SelectedValue = "ReporteDocPendientes" Then
            Session("MovSCTR") = "PEND"
            Response.Redirect("RepVISANET.aspx")
        End If
        'INICIO RQ2017-016 MEJORAS SCTR

        If MenuPrincipal.SelectedValue = "RepEndosos" Then
            Response.Redirect("RepEndosos.aspx")
        End If

        If MenuPrincipal.SelectedValue = "RepDC" Then
            Response.Redirect("RepDC.aspx")
        End If

        If MenuPrincipal.SelectedValue = "RepCO" Then
            Response.Redirect("RepCO.aspx")
        End If

        If MenuPrincipal.SelectedValue = "RepRecibos" Then
            Response.Redirect("RepRecibos.aspx")
        End If

        If MenuPrincipal.SelectedValue = "Movimientos" Then
            Session("MovSCTR") = "MO"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "EstadoComisiones" Then
            Session("MovSCTR") = "ECO"
            Response.Redirect("ProcesosSCTR_Doc.aspx")
        End If

        If MenuPrincipal.SelectedValue = "CAFAEVigente" Then
            Session("CAFAE") = "VIG"
            Response.Redirect("ProcesosCAFAE.aspx")
        End If

        If MenuPrincipal.SelectedValue = "CAFAEVencido" Then
            Session("CAFAE") = "VEN"
            Response.Redirect("ProcesosCAFAE.aspx")
        End If

        If MenuPrincipal.SelectedValue = "GenRecibosFam" Then
            Response.Redirect("GenRecibosFam.aspx")
        End If

        If MenuPrincipal.SelectedValue = "LiqRecibosFam" Then
            Response.Redirect("LiqRecibosFam.aspx")
        End If

        If MenuPrincipal.SelectedValue = "RepPagosCAFAE" Then
            Response.Redirect("RepControlPagosCAFAE.aspx")
        End If

        If MenuPrincipal.SelectedValue = "PolizaMatriz" Then
            Response.Redirect("ProcesosSCTR_Emi.aspx")
        End If

        If MenuPrincipal.SelectedValue = "PolizaMatrizGrupal" Then
            Response.Redirect("PolizasMatricesGrupales.aspx")
        End If


		If MenuPrincipal.SelectedValue = "menMinibatch" Then
            Response.Redirect("ProcesoMinibatchSusalud.aspx")
        End If

        If MenuPrincipal.SelectedValue = "menReenvio" Then
            Response.Redirect("ProcesoReenvioSusalud.aspx")
        End If

        If MenuPrincipal.SelectedValue = "menActuDatosAseg" Then
            Response.Redirect("ActualizaciondatosSusalud.aspx")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim txt As Label = CType(Master.FindControl("lblNomUsuario"), Label)
        Dim txtPerfil As Label = CType(Master.FindControl("lblPerfil"), Label)

        txt.Text = Session("Nombre")
        txtPerfil.Text = Session("Perfil")

        If txt.Text.Trim <> "" Or txtPerfil.Text.Trim <> "" Then
            If txtPerfil.Text = "07ADMF" Then
                MenuPrincipal.Items(0).Enabled = False
                MenuPrincipal.Items(1).Enabled = False
                MenuPrincipal.Items(2).Enabled = False
                MenuPrincipal.Items(3).Enabled = True
            Else
                MenuPrincipal.Items(0).Enabled = True
                MenuPrincipal.Items(1).Enabled = True
                MenuPrincipal.Items(2).Enabled = True
                MenuPrincipal.Items(3).Enabled = False
            End If
        Else
            Response.Redirect("Default.aspx")
        End If
    End Sub
End Class