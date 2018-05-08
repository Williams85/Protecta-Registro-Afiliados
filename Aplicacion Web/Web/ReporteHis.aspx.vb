Imports System.Data
'Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Web
'Imports CrystalDecisions.ReportAppServer.ClientDoc
Partial Class Reporte
    Inherits System.Web.UI.Page

    Dim DSReporte As DataTable
    Dim STRreporte As String
    Dim STRnombre As String
    Dim STRparametros() As String
    Dim STRmuestraData As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        STRmuestraData = Session("Data")
        If STRmuestraData = "S" Then
            DSReporte = Session("TABLA_REPORTE")
        End If
        STRreporte = Session("RPT")
        STRnombre = Session("NombreRPT")
        STRparametros = Session("Arreglo")

        Call Muestra_Reporte(STRreporte, DSReporte, STRnombre, STRmuestraData, STRparametros)

    End Sub
    Public Sub Muestra_Reporte(ByVal STRnombreReporte As String, _
      ByVal myDatos As DataTable, ByVal STRnombreTabla As String)
        Try

            Dim myReporte As New ReportDocument

            'Cargo el reporte segun ruta
            myReporte.Load(Server.MapPath("Reportes\" & STRnombreReporte), OpenReportMethod.OpenReportByTempCopy)
            myReporte.SetDataSource(myDatos)

            CrystalReportViewer1.ReportSource = myReporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Public Sub Muestra_Reporte(ByVal STRnombreReporte As String, _
    ByVal myDatos As DataTable, ByVal STRnombreTabla As String, ByVal datos As String, ByVal ParamArray Parametros() As String)
        Try

            Dim myReporte As New ReportDocument

            'Cargo el reporte segun ruta
            myReporte.Load(Server.MapPath("Reportes\" & STRnombreReporte), OpenReportMethod.OpenReportByTempCopy)

            If Parametros.Length > 0 Then
                CrystalReportViewer1.ParameterFieldInfo = Genera_Parametros(Parametros)
            End If

            If datos = "S" Then
                myReporte.SetDataSource(myDatos)
            End If
            CrystalReportViewer1.ReportSource = myReporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Shared Function Genera_Parametros(ByVal ParamArray MyMatriz() As String) As ParameterFields
        Dim c As Long, STRnombre As String, STRvalor As String, l As Integer

        Dim parametros As New ParameterFields
        For c = 0 To MyMatriz.Length - 1
            l = InStr(MyMatriz(c), ";")
            If l > 0 Then
                STRnombre = Mid(MyMatriz(c), 1, l - 1)
                STRvalor = Mid(MyMatriz(c), l + 1, Len(MyMatriz(c)) - l)
                Dim parametro As New ParameterField
                Dim dVal As New ParameterDiscreteValue
                parametro.ParameterFieldName = STRnombre
                dVal.Value = STRvalor
                parametro.CurrentValues.Add(dVal)
                parametros.Add(parametro)
            End If
        Next
        Return (parametros)
    End Function
    Public Sub AlertaScripts(ByVal P_strMensaje As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("alert('" + P_strMensaje + "');")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "PopupAlert", sb.ToString, False)
    End Sub
End Class
