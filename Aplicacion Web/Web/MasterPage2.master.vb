
Partial Class MasterPage2
    Inherits System.Web.UI.MasterPage

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub

   Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
      Response.Redirect("Menu.aspx")
   End Sub
End Class

