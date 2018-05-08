Imports System.Data
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Partial Class Reporte2
    Inherits System.Web.UI.Page
    Dim reporte As New ReportDocument
    Dim myFOpts As Integer

    Dim DSReporte As DataTable
    Dim STRreporte As String
    Dim STRnombre As String
    Dim STRparametros() As String
    Dim STRmuestraData As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
    ByVal myDatos As DataTable, ByVal STRnombreTabla As String, ByVal datos As String, ByVal ParamArray Parametros() As String)
        Try
            reporte.Load(Server.MapPath("Reportes\" & STRnombreReporte), OpenReportMethod.OpenReportByTempCopy)
            reporte.SetDataSource(myDatos)
            Dim CantParametro As Integer
            CantParametro = Parametros.Length - 1

            If Parametros.Length > 0 Then
                For i As Integer = 0 To CantParametro
                    Dim texto() As String = Split(STRparametros(i), ";")
                    reporte.SetParameterValue(texto(0), texto(1))
                Next
            End If
            Me.CrystalReportViewer1.DisplayStatusbar = False
            Me.CrystalReportViewer1.ReportSource = reporte
            Me.CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Throw
        End Try

    End Sub

End Class
