Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.Data.OracleClient
Imports System.Configuration
Public Class Conex
    Private myConexion As System.Data.IDbConnection

    Private Function InitConnection(ByVal tipo As Integer)

        If tipo = 1 Then
            Me.myConexion = New System.Data.SqlClient.SqlConnection
            Me.myConexion.ConnectionString = ConfigurationManager.ConnectionStrings("SQL").ConnectionString.ToString
        ElseIf tipo = 2 Then
            Me.myConexion = New System.Data.OracleClient.OracleConnection
            Me.myConexion.ConnectionString = ConfigurationManager.ConnectionStrings("ORACLE").ConnectionString.ToString
        End If
        Return myConexion
    End Function

    Public Property ConexionDB(ByVal tipo As Integer) As System.Data.IDbConnection
        Get
            If (Me.myConexion Is Nothing) Then
                Me.InitConnection(tipo)
            End If
            Return Me.myConexion
        End Get
        Set(ByVal value As System.Data.IDbConnection)
            Me.myConexion = value
        End Set
    End Property

    Public Function CrearReader(ByVal tipo As Integer) As IDataReader

        CrearReader = Nothing

        Select Case tipo
            Case 1
                Dim MiReader As SqlClient.SqlDataReader = Nothing
                Return MiReader
            Case 2
                Dim MiReader As OracleClient.OracleDataReader = Nothing
                Return MiReader
        End Select

    End Function
    Public Function CrearAdaptador(ByVal tipo As Integer, ByVal STRquery As String, ByVal varCnx As DbConnection) As IDbDataAdapter

        CrearAdaptador = Nothing
        Select Case tipo
            Case 1
                Dim MiAdaptador As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(STRquery, varCnx)
                Return MiAdaptador
            Case 2
                Dim MiAdaptador As OracleClient.OracleDataAdapter = New OracleClient.OracleDataAdapter(STRquery, varCnx)
                Return MiAdaptador
        End Select


    End Function
    Public Function CrearAdaptador(ByVal tipo As Integer, ByVal MYCommand As DbCommand) As IDbDataAdapter

        CrearAdaptador = Nothing
        Select Case tipo
            Case 1
                Dim MiAdaptador As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(MYCommand)
                Return MiAdaptador
            Case 2
                Dim MiAdaptador As OracleClient.OracleDataAdapter = New OracleClient.OracleDataAdapter(MYCommand)
                Return MiAdaptador
        End Select
    End Function

    Public Function CrearCommando(ByVal tipo As Integer, ByVal STRquery As String) As IDbCommand

        CrearCommando = Nothing
        Select Case tipo
            Case 1
                Dim MiComando As SqlClient.SqlCommand = New SqlClient.SqlCommand(STRquery)
                Return MiComando
            Case 2
                Dim MiComando As OracleClient.OracleCommand = New OracleClient.OracleCommand(STRquery)
                Return MiComando
        End Select
    End Function
    Public Function CrearParametro(ByVal tipo As Integer) As IDbDataParameter

        CrearParametro = Nothing
        Select Case tipo
            Case 1
                Dim MiParametro As SqlClient.SqlParameter = New SqlClient.SqlParameter
                Return MiParametro
            Case 2
                Dim MiParametro As OracleClient.OracleParameter = New OracleClient.OracleParameter
                Return MiParametro
        End Select
    End Function
    Public Function CrearParametro(ByVal STRTipoMotor As String, ByVal STRnomParametro As String) As IDbDataParameter
        CrearParametro = Nothing
        Dim MiParametro As OracleClient.OracleParameter = New OracleClient.OracleParameter(STRnomParametro, OracleType.Cursor)
        Return MiParametro
    End Function

    Public Function CrearBuilder(ByVal TIPO As Integer) As Object
        CrearBuilder = Nothing
        Select Case TIPO

            Case 1
                Dim MiBuilder As New SqlClient.SqlCommandBuilder
                Return MiBuilder
            Case 2
                Dim MiBuilder As New OracleClient.OracleCommandBuilder
                Return MiBuilder
        End Select

    End Function
    Public Function Dev_Par(ByVal TIPO As Integer, ByVal STRparametro As String) As String
        Dim strValor As String = ""
        Select Case TIPO
            Case 0 To 1
                strValor = STRparametro.Trim
            Case 2
                strValor = STRparametro.Trim.Substring(1, Len(STRparametro.Trim) - 1)
        End Select
        Return strValor
    End Function
End Class
