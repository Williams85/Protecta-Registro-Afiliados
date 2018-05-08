Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Policy
Imports System.Diagnostics
Imports System.Threading
Imports System.Web.Configuration
Imports System.Xml
Imports System.Text

Partial Class ProcesosPCR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                HF_LIMITE.Value = ConfigurationManager.AppSettings("LimiteDeProcesos").ToString()
                HF_RUTA_MASIVA.Value = ConfigurationManager.AppSettings("RutaMasiva").ToString()
                Session("Control") = 0
                Dim txt As Label = CType(Me.FindControl("lblNomUsuario"), Label)
                txt.Text = Session("Nombre")

                Call ListaPolizaSeleccionada()
                creaArchivoControlTab()

                Dim fileName As String = (HF_RUTA_MASIVA.Value & "/TabSel/SelTabPol.xml")
                If ((Not (fileName) Is Nothing) OrElse (fileName <> String.Empty)) Then
                    If System.IO.File.Exists(fileName) Then
                        System.IO.File.Delete(fileName)
                    End If
                End If

                Dim rUTA As String = ""
                rUTA = HF_RUTA_MASIVA.Value & "/TabSel/"
                Dim xmlwriter As XmlTextWriter = New XmlTextWriter(rUTA & "/SelTabPol.xml", Encoding.UTF8)
                xmlwriter.Formatting = Formatting.Indented
                xmlwriter.WriteStartDocument()
                xmlwriter.WriteComment("Programmatically writing XML")
                xmlwriter.WriteStartElement("TabCarga")
                Dim dt As DataTable = estructuraDTXML()
                Dim i As Integer = 0
                xmlwriter.WriteStartElement("Carga")
                xmlwriter.WriteAttributeString("type", "Permanent")
                xmlwriter.WriteElementString("NroVentana", "0")
                xmlwriter.WriteElementString("Producto", "x")
                xmlwriter.WriteElementString("Poliza", "0")
                xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement()
                xmlwriter.WriteEndDocument()
                xmlwriter.Flush()
                xmlwriter.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Function estructuraDTXML() As DataTable
        Dim dt As DataTable = New DataTable
        Dim dc As DataColumn = New DataColumn
        dc.ColumnName = "NroVentana"
        dt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Producto"
        dt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Poliza"
        dt.Columns.Add(dc)
        dc = New DataColumn
    
        Dim dr As DataRow = dt.NewRow
        dr(0) = ""
        dr(1) = ""
        dr(2) = ""
        dt.Rows.Add(dr)
        Return dt
    End Function


    Public Sub creaArchivoControlTab()
        Dim di As DirectoryInfo
        Dim rUTA As String = ""
        Try
            rUTA = HF_RUTA_MASIVA.Value & "\TabSel"
            If Directory.Exists(rUTA) = False Then
                di = Directory.CreateDirectory(rUTA)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ListaPolizaSeleccionada()
        Dim dtValSel As New DataTable()

        Dim TabSel As New DataColumn("TabSel")
        TabSel.DataType = GetType(String)


        Dim Producto As New DataColumn("Producto")
        Producto.DataType = GetType(String)


        Dim Poliza As New DataColumn("Poliza")
        Poliza.DataType = GetType(String)

        Dim Seleccionado As New DataColumn("Seleccionado")
        Seleccionado.DataType = GetType(String)

        dtValSel.Columns.Add(TabSel)
        dtValSel.Columns.Add(Producto)
        dtValSel.Columns.Add(Poliza)
        dtValSel.Columns.Add(Seleccionado)

        Session("dtValSel") = dtValSel

    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Try
            Response.Redirect("Menu.aspx")
        Catch ex As Exception

        End Try
  
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
