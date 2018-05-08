
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Session("Usuario") = "" Or Session("CodUsuario") = "" Or Session("Nombre") = "" Then
    '        Response.Redirect("Default.aspx")
    '    End If
    'End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub

   Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
      Response.Redirect("Menu.aspx")
    End Sub
End Class

