Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Private METODOS As New Metodos
    Private dt As DataTable

    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
         If txtUsuario.Text.Trim <> "" And txtClave.Text.Trim <> "" Then
            Dim STR_clave As String = METODOS.StrEncode(txtClave.Text.Trim)
            dt = METODOS.Lista_Datos_Usuario(METODOS.DB.ORACLE, txtUsuario.Text.Trim, STR_clave)
            If dt.Rows.Count > 0 Then
                    Session("Usuario") = txtUsuario.Text.Trim
                    Session("CodUsuario") = dt.Rows(0).Item(0).ToString
                    Session("Nombre") = dt.Rows(0).Item(2).ToString
                    Session("Perfil") = dt.Rows(0).Item(3).ToString

                    Response.Redirect("Menu.aspx", False)
            Else
               AlertaScripts("No existe el usuario o contraseña mal ingresada")
            End If
         Else
            If txtUsuario.Text.Trim = "" Then
               AlertaScripts("Ingresar Usuario")
               txtUsuario.Text = ""
            ElseIf txtClave.Text.Trim = "" Then
               AlertaScripts("Ingresar Contraseña")
               txtClave.Text = ""
            End If
         End If

      Catch ex As Exception
         AlertaScripts("Hay problemas con el acceso al sistema")
      End Try
    End Sub
End Class
