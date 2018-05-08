Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.Data.OracleClient
Imports System.Diagnostics
Imports OfficeOpenXml
Imports OfficeOpenXml.Table
Imports System.Collections.Generic
Imports System.ComponentModel


Public Class Metodos
    Private STRsql As String
    Private CNXvar As DbConnection
    Private CMDvar As DbCommand
    Private ADAvar As DbDataAdapter
    Private DARvar As DbDataReader
    Private DATvar As New DataTable
    Private TRvar As DbTransaction
    Private saltvalue As String
    Private OBJconexion As Conex = New Conex
    Public EntExportar As SusaludEntExportar

#Region "PASO 1"

    Public Function Lista_Datos_Usuario(ByVal Tipo As Integer, ByVal STRusuario As String, ByVal STRclave As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_USUARIO.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParUSUARIO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParUSUARIO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@USUARIO")
                .DbType = DbType.String
                .Size = 12
                .Value = STRusuario
            End With
            CMDvar.Parameters.Add(ParUSUARIO)

            Dim ParCLAVE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCLAVE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@CLAVE")
                .DbType = DbType.String
                .Size = 12
                .Value = STRclave
            End With
            CMDvar.Parameters.Add(ParCLAVE)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Datos_Combos(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_COMBOS.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Canal")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Producto")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Poliza")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "Ciclo")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Datos_Combos_SCTR(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_COMBOS.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Canal")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Producto")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Poliza")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "Ciclo")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)

            Dim vardata4 As DbParameter = OBJconexion.CrearParametro(Tipo, "Estado_SCTR")
            With vardata4
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata4)

            Dim vardata5 As DbParameter = OBJconexion.CrearParametro(Tipo, "COMERCIO_SCTR")
            With vardata5
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata5)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Datos_Combos_CAFAE(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_COMBOS.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Canal")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Producto")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Poliza")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "Ciclo")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Comentario_Poliza(ByVal Tipo As Integer, ByVal producto As Integer, ByVal poliza As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_POLIZA_COMENTARIO.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TIPO_CARGA")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "RENOVACION")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)


            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Datos_Formato(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer, ByVal ciclo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_FORMATO.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParCANAL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCANAL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCANAL")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParCANAL)

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim ParPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParPeriodo)

            Dim Parciclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parciclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nciclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(Parciclo)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "FormatoPoliza")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Archivo")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "Columna")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)

            Dim vardata4 As DbParameter = OBJconexion.CrearParametro(Tipo, "Ruta")
            With vardata4
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata4)

            Dim vardata5 As DbParameter = OBJconexion.CrearParametro(Tipo, "RutaUser")
            With vardata5
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata5)

            Dim vardata6 As DbParameter = OBJconexion.CrearParametro(Tipo, "RutaSys")
            With vardata6
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata6)

            Dim vardata7 As DbParameter = OBJconexion.CrearParametro(Tipo, "TablaCanal")
            With vardata7
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata7)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function Lista_Datos_Archivo(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ARCHIVO.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@producto")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@poliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim Parperiodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parperiodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(Parperiodo)


            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "ARCHIVO")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)


            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function Lista_Datos_Archivo_SCTR(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As String, ByVal periodo As Integer, ByVal ciclo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ARCHIVO.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@producto")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@poliza")
                .DbType = DbType.String
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim Parperiodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parperiodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(Parperiodo)

            Dim Parciclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parciclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCiclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(Parciclo)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "ARCHIVO")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)


            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function Lista_Datos_Archivo_CAFAE(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As String, ByVal periodo As Integer, ByVal ciclo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ARCHIVO.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@producto")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@poliza")
                .DbType = DbType.String
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim Parperiodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parperiodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(Parperiodo)

            Dim Parciclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parciclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCiclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(Parciclo)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "ARCHIVO")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)


            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function Lista_Datos_Formato_SCTR(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer, ByVal ciclo As Integer, ByVal sTipFormato As String) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_FORMATO.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParCANAL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCANAL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCANAL")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParCANAL)

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim ParPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParPeriodo)

            Dim Parciclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parciclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nciclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(Parciclo)

            Dim TipFormato As DbParameter = OBJconexion.CrearParametro(Tipo)
            With TipFormato
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TipFormato")
                .DbType = DbType.String
                .Value = sTipFormato
            End With
            CMDvar.Parameters.Add(TipFormato)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "FormatoPoliza")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Archivo")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "Columna")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)

            Dim vardata4 As DbParameter = OBJconexion.CrearParametro(Tipo, "Ruta")
            With vardata4
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata4)

            Dim vardata5 As DbParameter = OBJconexion.CrearParametro(Tipo, "RutaUser")
            With vardata5
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata5)

            Dim vardata6 As DbParameter = OBJconexion.CrearParametro(Tipo, "RutaSys")
            With vardata6
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata6)

            Dim vardata7 As DbParameter = OBJconexion.CrearParametro(Tipo, "TablaCanal")
            With vardata7
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata7)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function Lista_Datos_Formato_CAFAE(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer, ByVal ciclo As Integer, ByVal sTipFormato As String) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_FORMATO.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParCANAL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCANAL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCANAL")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParCANAL)

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim ParPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParPeriodo)

            Dim Parciclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parciclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nciclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(Parciclo)

            Dim TipFormato As DbParameter = OBJconexion.CrearParametro(Tipo)
            With TipFormato
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TipFormato")
                .DbType = DbType.String
                .Value = sTipFormato
            End With
            CMDvar.Parameters.Add(TipFormato)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "FormatoPoliza")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Archivo")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "Columna")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)

            Dim vardata4 As DbParameter = OBJconexion.CrearParametro(Tipo, "Ruta")
            With vardata4
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata4)

            Dim vardata5 As DbParameter = OBJconexion.CrearParametro(Tipo, "RutaUser")
            With vardata5
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata5)

            Dim vardata6 As DbParameter = OBJconexion.CrearParametro(Tipo, "RutaSys")
            With vardata6
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata6)

            Dim vardata7 As DbParameter = OBJconexion.CrearParametro(Tipo, "TablaCanal")
            With vardata7
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata7)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function
    'modificado - requerimiento 
    Public Function Lista_Tabla_Canal(ByVal Tipo As Integer, ByVal canal As String, Optional ByVal policy As Integer = 0) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_CANAL.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@canal")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@npolicy")
                .DbType = DbType.Int64
                .Value = policy
            End With
            CMDvar.Parameters.Add(ParpPoliza)


            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_CANAL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_Canal_CAFAE(ByVal Tipo As Integer, ByVal canal As String)
        Dim intCiclo As Integer = 0
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_CANAL.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@canal")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim ParNRegistros As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNRegistros
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nRegistros")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParNRegistros)

            CMDvar.ExecuteNonQuery()
            intCiclo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nRegistros")).Value

            CNXvar.Close()
            CNXvar = Nothing
            Return intCiclo

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Abre_Ciclo(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer, ByVal glosa As String, _
                                ByVal RutaOrigen As String, ByVal cabecera As Integer, ByVal DescArchivo As String)

        Dim intCiclo As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_INSERTA_CICLO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPoliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpDes_control As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_control
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_control")
                .DbType = DbType.String
                .Size = 80
                .Value = glosa
            End With
            CMDvar.Parameters.Add(ParpDes_control)

            Dim ParpArchivo_origen As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpArchivo_origen
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pArchivo_origen")
                .DbType = DbType.String
                .Size = 255
                .Value = RutaOrigen
            End With
            CMDvar.Parameters.Add(ParpArchivo_origen)

            Dim ParpSkip As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSkip
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSkip")
                .DbType = DbType.Int64
                .Value = cabecera
            End With
            CMDvar.Parameters.Add(ParpSkip)

            Dim ParpDes_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_archivo")
                .DbType = DbType.String
                .Size = 80
                .Value = DescArchivo
            End With
            CMDvar.Parameters.Add(ParpDes_archivo)

            CMDvar.ExecuteNonQuery()
            'intCiclo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@pCiclo")).Value


            CNXvar.Close()
            CNXvar = Nothing

            Return intCiclo
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function

    Public Function Abre_Ciclo_SCTR(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer, ByVal glosa As String, _
                                ByVal RutaOrigen As String, ByVal cabecera As Integer, ByVal DescArchivo As String, ByVal Mov_SCTR As String)

        Dim intCiclo As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_INSERTA_CICLO_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPoliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpDes_control As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_control
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_control")
                .DbType = DbType.String
                .Size = 80
                .Value = glosa
            End With
            CMDvar.Parameters.Add(ParpDes_control)

            Dim ParpMov_SCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpMov_SCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pMov_SCTR")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParpMov_SCTR)

            Dim ParpArchivo_origen As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpArchivo_origen
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pArchivo_origen")
                .DbType = DbType.String
                .Size = 255
                .Value = RutaOrigen
            End With
            CMDvar.Parameters.Add(ParpArchivo_origen)

            Dim ParpSkip As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSkip
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSkip")
                .DbType = DbType.Int64
                .Value = cabecera
            End With
            CMDvar.Parameters.Add(ParpSkip)

            Dim ParpDes_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_archivo")
                .DbType = DbType.String
                .Size = 80
                .Value = DescArchivo
            End With
            CMDvar.Parameters.Add(ParpDes_archivo)

            Dim ParNCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCiclo")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParNCiclo)

            CMDvar.ExecuteNonQuery()
            intCiclo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nCiclo")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return intCiclo
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function

    Public Function Abre_Ciclo_CAFAE(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal periodo As Integer, ByVal glosa As String, _
                                ByVal RutaOrigen As String, ByVal cabecera As Integer, ByVal DescArchivo As String, ByVal Mov_SCTR As String)

        Dim intCiclo As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_INSERTA_CICLO_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPoliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpDes_control As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_control
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_control")
                .DbType = DbType.String
                .Size = 80
                .Value = glosa
            End With
            CMDvar.Parameters.Add(ParpDes_control)

            Dim ParpMov_SCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpMov_SCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pMov_CAFAE")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParpMov_SCTR)

            Dim ParpArchivo_origen As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpArchivo_origen
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pArchivo_origen")
                .DbType = DbType.String
                .Size = 255
                .Value = RutaOrigen
            End With
            CMDvar.Parameters.Add(ParpArchivo_origen)

            Dim ParpSkip As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSkip
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSkip")
                .DbType = DbType.Int64
                .Value = cabecera
            End With
            CMDvar.Parameters.Add(ParpSkip)

            Dim ParpDes_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_archivo")
                .DbType = DbType.String
                .Size = 80
                .Value = DescArchivo
            End With
            CMDvar.Parameters.Add(ParpDes_archivo)

            Dim ParNCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCiclo")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParNCiclo)

            CMDvar.ExecuteNonQuery()
            intCiclo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nCiclo")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return intCiclo
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function
    'RSIS01
    Public Function Lista_Report_Correctos(ByVal Tipo As Integer, ByVal canal As String, Optional ByVal poliza As String = "") As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTE_CORRECTOS.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@canal")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@npolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)


            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_CANAL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Correctos_SCTR(ByVal Tipo As Integer, ByVal canal As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTE_CORRECTOS.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@canal")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_CANAL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Correctos_CAFAE(ByVal Tipo As Integer, ByVal canal As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTE_CORRECTOS.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@canal")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_CANAL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Sub Cierra_Ciclo(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal periodo As Integer, ByVal ciclo As Integer)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CIERRA_CICLO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 12
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpDes_control As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_control
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(ParpDes_control)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Elimina_Archivo_Asociados(ByVal Tipo As String, ByVal archivo As Integer)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "SRV_TRX_DEL_ARCHIVO_ASOC")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNum_archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Elimina_Archivo_Asociados_SCTR(ByVal Tipo As String, ByVal archivo As Integer, ByVal Mov_SCTR As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "SRV_TRX_DEL_ARCHIVO_ASOC_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNum_archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            Dim ParpMov_SCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpMov_SCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pMov_SCTR")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParpMov_SCTR)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Genera_Secuencia(ByVal Tipo As String, ByVal formato As String)

        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_GENERA_SECUENCIA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpFormato As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpFormato
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pFormato")
                .DbType = DbType.String
                .Size = 16
                .Value = formato
            End With
            CMDvar.Parameters.Add(ParpFormato)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Function Genera(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal periodo As Integer, ByVal glosa As String)

        Dim intCiclo As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_INSERTA_CICLO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 12
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpDes_control As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpDes_control
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDes_control")
                .DbType = DbType.String
                .Size = 80
                .Value = glosa
            End With
            CMDvar.Parameters.Add(ParpDes_control)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            CMDvar.ExecuteNonQuery()
            intCiclo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@pCiclo")).Value


            CNXvar.Close()
            CNXvar = Nothing

            Return intCiclo
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

    Public Function Val_Estado_Job(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal poliza As String, ByVal Mov_SCTR As String)

        Dim intCountJob As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CONSULTAR_ESTADO_JOB")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPoliza")
                .DbType = DbType.String
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)

            Dim ParpMov_SCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpMov_SCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pMov_SCTR")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParpMov_SCTR)

            Dim ParNCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NVALIDADOR")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParNCiclo)

            CMDvar.ExecuteNonQuery()
            intCountJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@NVALIDADOR")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return intCountJob
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function

    Public Function Validar_Fecha_Efecto(ByVal Tipo As String, ByVal sFechaEfecto As String, ByVal sTipoMov As String, ByVal sProducto As String, ByVal sPoliza As String)

        Dim sMensaje As String
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_VALIDA_FECHA_EFECTO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParsFechaEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsFechaEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PFECEFECTO")
                .DbType = DbType.String
                .Size = 10
                .Value = sFechaEfecto
            End With
            CMDvar.Parameters.Add(ParsFechaEfecto)

            Dim ParsTipoMov As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsTipoMov
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PTIPOMOV")
                .DbType = DbType.String
                .Size = 3
                .Value = sTipoMov
            End With
            CMDvar.Parameters.Add(ParsTipoMov)

            Dim ParsProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PPRODUCTO")
                .DbType = DbType.String
                .Size = 4
                .Value = sProducto
            End With
            CMDvar.Parameters.Add(ParsProducto)

            Dim ParsPolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsPolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PPOLIZA")
                .DbType = DbType.String
                .Size = 10
                .Value = sPoliza
            End With
            CMDvar.Parameters.Add(ParsPolicy)

            Dim ParSMensaje As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParSMensaje
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PMSGVAL")
                .DbType = DbType.String
                .Size = 500
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParSMensaje)

            CMDvar.ExecuteNonQuery()
            sMensaje = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@PMSGVAL")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return sMensaje
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

#End Region

#Region "PASO 2"

    Public Sub Inserta_errores(ByVal Tipo As String, ByVal archivo As Integer, ByVal Consistencia As String)  ', ByVal Tipo_carga As String

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, Consistencia)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNUM_ARCHIVO")
                .DbType = DbType.Double
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Inserta_errores(ByVal Tipo As String, ByVal archivo As Integer, ByVal Consistencia As String, ByVal Tipo_carga As String)  ', ByVal Tipo_carga As String

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, Consistencia)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNUM_ARCHIVO")
                .DbType = DbType.Double
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            Dim ParpTIPO_CARGA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpTIPO_CARGA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pTIPO_CARGA")
                .DbType = DbType.String
                .Size = 1
                .Value = Tipo_carga
            End With
            CMDvar.Parameters.Add(ParpTIPO_CARGA)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Inserta_errores_CAFAE(ByVal Tipo As String, ByVal archivo As Integer, ByVal Consistencia As String, ByVal pFecPeriodo As String)  ', ByVal Tipo_carga As String

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, Consistencia)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNUM_ARCHIVO")
                .DbType = DbType.Double
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            Dim ParpFecPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpFecPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pFECPERIODO")
                .DbType = DbType.String
                .Size = 10
                .Value = pFecPeriodo
            End With
            CMDvar.Parameters.Add(ParpFecPeriodo)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Traslado_Registros(ByVal Tipo As String, ByVal archivo As Integer, ByVal Translada As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, Translada)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNum_archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Function Lista_Tabla_Pol(ByVal Tipo As Integer, ByVal Poliza As String, ByVal Producto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_POL.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_Pol_SCTR(ByVal Tipo As Integer, ByVal Poliza As String, ByVal Producto As String, ByVal Mov_SCTR As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_POL.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_SCTR")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_Pol_CAFAE(ByVal Tipo As Integer, ByVal Poliza As String, ByVal Producto As String, ByVal Mov_SCTR As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_POL.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_CAFAE")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Procesados_SCTR_DC(ByVal Tipo As Integer, ByVal Producto As String, ByVal Mov_SCTR As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_PROCESADOS_SCTR.LISTAR_SCTR_DOC")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_SCTR")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Procesados_SCTR_MOV(ByVal Tipo As Integer, ByVal Poliza As String, ByVal Producto As String, ByVal Mov_SCTR As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_PROCESADOS_SCTR.LISTAR_SCTR_MOV")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_SCTR")
                .DbType = DbType.String
                .Size = 5
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Procesados_CAFAE(ByVal Tipo As Integer, ByVal Poliza As String, ByVal Producto As String, ByVal Mov_CAFAE As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_PROCESADOS_CAFAE.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParMovCAFAE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovCAFAE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_CAFAE")
                .DbType = DbType.String
                .Value = Mov_CAFAE
            End With
            CMDvar.Parameters.Add(ParMovCAFAE)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_CARGA02(ByVal Tipo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_CARGA02.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_CARGA")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Conf_Remmp_Renov(ByVal Tipo As Integer, ByVal canal As String, ByVal cod_prod As Int32) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_CONF_REMMP_RENOV.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parpar_canal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpar_canal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@par_canal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parpar_canal)

            Dim Parcodproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcodproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@codproduct")
                .DbType = DbType.Int32
                .Value = cod_prod
            End With
            CMDvar.Parameters.Add(Parcodproduct)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "RemplazoNomina")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Renonacion")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "TablaCanal")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_fechas_policy(ByVal Tipo As Integer, ByVal Poliza As String, ByVal Producto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_POLICY.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_fechas_policy_SCTR(ByVal Tipo As Integer, ByVal MovSCTR As String, ByVal Producto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_POLICY.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@MOVSCTR")
                .DbType = DbType.String
                .Value = MovSCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Tabla_fechas_policy_CAFAE(ByVal Tipo As Integer, ByVal MOVCAFAE As String, ByVal Producto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_TABLA_POLICY.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParMOVCAFAE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMOVCAFAE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@MOVCAFAE")
                .DbType = DbType.String
                .Value = MOVCAFAE
            End With
            CMDvar.Parameters.Add(ParMOVCAFAE)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Producto")
                .DbType = DbType.Int64
                .Value = Producto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Modulo_Pol(ByVal Tipo As Integer, ByVal Poliza As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_MODULOS_POL.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Modulo_Pol_SCTR(ByVal Tipo As Integer, ByVal Poliza As String, ByVal MovSCTR As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_MODULOS_POL.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_POL")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@MovSCTR")
                .DbType = DbType.String
                .Value = MovSCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Errores(ByVal Tipo As Integer, ByVal archivo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ERRORES.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@ARCHIVO")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Warning(ByVal Tipo As Integer, ByVal archivo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ERRORES.LISTAR_WARNING")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@ARCHIVO")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function
    'RSIS01
    Public Function Lista_Report_Agrobanco(ByVal Tipo As Integer, ByVal archivo As Integer, Optional ByVal poliza As String = "") As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_RESUMEN_AGROBANCO.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@ARCHIVO")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)


            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@policy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)


            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function
    'RSIS01
    Public Function Lista_Report_Agrobanco_Permanencia(ByVal Tipo As Integer, ByVal archivo As Integer, Optional ByVal poliza As String = "") As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_RESUMEN_AGROBANCO.LISTAR_PERMANENCIA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@ARCHIVO")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@policy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista2")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Resumen(ByVal Tipo As Integer, ByVal poliza As Integer, ByVal tabla As String, ByVal archivo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_RESUMEN.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParTablaCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParTablaCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TablaCanal")
                .DbType = DbType.String
                .Size = 30
                .Value = tabla
            End With
            CMDvar.Parameters.Add(ParTablaCanal)

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Resumen")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Resumen_SCTR(ByVal Tipo As Integer, ByVal poliza As Integer, ByVal tabla As String, ByVal archivo As Integer, ByVal Mov_SCTR As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_RESUMEN.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParTablaCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParTablaCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TablaCanal")
                .DbType = DbType.String
                .Size = 30
                .Value = tabla
            End With
            CMDvar.Parameters.Add(ParTablaCanal)

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim ParMovSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_SCTR")
                .DbType = DbType.String
                .Value = Mov_SCTR
            End With
            CMDvar.Parameters.Add(ParMovSCTR)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Resumen")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Resumen_CAFAE(ByVal Tipo As Integer, ByVal poliza As Integer, ByVal tabla As String, ByVal archivo As Integer, ByVal Mov_CAFAE As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_RESUMEN.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Poliza")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPoliza)

            Dim ParTablaCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParTablaCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TablaCanal")
                .DbType = DbType.String
                .Size = 30
                .Value = tabla
            End With
            CMDvar.Parameters.Add(ParTablaCanal)

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim ParMovCAFAE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParMovCAFAE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@Mov_CAFAE")
                .DbType = DbType.String
                .Value = Mov_CAFAE
            End With
            CMDvar.Parameters.Add(ParMovCAFAE)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Resumen")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

#End Region

#Region "PASO 3"

    Public Sub Archivo_Genera(ByVal Tipo As String, ByVal POLIZA As Integer, ByVal PERIODO As Integer, ByVal CICLO As Integer, ByVal vGenerador As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, vGenerador)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = POLIZA
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = PERIODO
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Value = CICLO
            End With
            CMDvar.Parameters.Add(ParpCiclo)


            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Function Archivo_Genera_Tarea90(ByVal Tipo As String, ByVal POLIZA As Integer, ByVal vNUSERCODE As Integer, ByVal fechaDeffec As String, ByVal vGenerador As String, ByVal vTipoCarga As String) As String
        Dim sEstadoJob As String = ""
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, vGenerador)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = POLIZA
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNUSERCODE")
                .DbType = DbType.Int64
                .Value = vNUSERCODE
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@FechDeffecdate")
                .DbType = DbType.String
                .Size = 10
                .Value = fechaDeffec
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            Dim ParTipoCarga As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParTipoCarga
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TipoCarga")
                .DbType = DbType.String
                .Size = 10
                .Value = vTipoCarga
            End With
            CMDvar.Parameters.Add(ParTipoCarga)



            Dim ParEstadojob As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstadojob
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstadojob)


            CMDvar.ExecuteNonQuery()

            sEstadoJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")).Value

            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing
            Return sEstadoJob
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

    Public Function Archivo_Genera_Tarea(ByVal Tipo As String, ByVal POLIZA As Integer, ByVal vNUSERCODE As Integer, ByVal fechaDeffec As String, ByVal vGenerador As String, ByVal vTipoCarga As String)
        Try
            Dim sEstadoJob As String
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, vGenerador)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = POLIZA
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNUSERCODE")
                .DbType = DbType.Int64
                .Value = vNUSERCODE
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@FechDeffecdate")
                .DbType = DbType.String
                .Size = 10
                .Value = fechaDeffec
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            Dim ParTipoCarga As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParTipoCarga
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TipoCarga")
                .DbType = DbType.String
                .Size = 10
                .Value = vTipoCarga
            End With
            CMDvar.Parameters.Add(ParTipoCarga)


            Dim ParEstadojob As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstadojob
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstadojob)

            CMDvar.ExecuteNonQuery()

            sEstadoJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")).Value
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing
            Return sEstadoJob

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function


    Public Sub Archivo_Genera_Tarea89(ByVal Tipo As String, ByVal vNUSERCODE As Integer, ByVal vGenerador As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, vGenerador)
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNUSERCODE")
                .DbType = DbType.Int64
                .Value = vNUSERCODE
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Archivo_Estado_PASO3(ByVal Tipo As String, ByVal POLIZA As Integer, ByVal PERIODO As Integer, ByVal CICLO As Integer, ByVal estado As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "SRV_TRX_UPD_ARCHIVO_ESTADO_POL")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = POLIZA
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = PERIODO
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Value = CICLO
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            Dim ParpEstado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpEstado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pEstado")
                .DbType = DbType.String
                .Size = 1
                .Value = estado
            End With
            CMDvar.Parameters.Add(ParpEstado)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

#End Region

#Region "PASO 4"

    Public Sub Crea_Job(ByVal Tipo As String, ByVal poliza As Integer, ByVal periodo As Integer, _
                    ByVal ciclo As Integer, ByVal modulo As Integer, ByVal fechaEfecto As String, _
                    ByVal archivo As String, ByVal CANAL As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_SRV_TRX_CREA_JOBBATCH")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            Dim ParpModulo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpModulo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pModulo")
                .DbType = DbType.Int64
                .Value = modulo
            End With
            CMDvar.Parameters.Add(ParpModulo)

            Dim ParpFechaEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpFechaEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pFechaEfecto")
                .DbType = DbType.String
                .Size = 16
                .Value = fechaEfecto
            End With
            CMDvar.Parameters.Add(ParpFechaEfecto)

            Dim ParpArchivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpArchivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pArchivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpArchivo)

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = CANAL
            End With
            CMDvar.Parameters.Add(ParpCanal)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Sub Actualiza_archivo_tarea(ByVal Tipo As String, ByVal poliza As Integer, ByVal periodo As Integer, _
                    ByVal ciclo As Integer, ByVal modulo As Integer, ByVal archivo As Integer, ByVal CANAL As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_ACTUALIZA_ARCH_TAREA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            Dim ParpModulo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpModulo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pModulo")
                .DbType = DbType.Int64
                .Value = modulo
            End With
            CMDvar.Parameters.Add(ParpModulo)

            Dim ParpArchivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpArchivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pArchivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpArchivo)

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = CANAL
            End With
            CMDvar.Parameters.Add(ParpCanal)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

    Public Function Lista_Arch_Tarea(ByVal Tipo As String, ByVal poliza As Integer, ByVal periodo As Integer, _
                    ByVal ciclo As Integer, ByVal modulo As Integer, ByVal archivo As Integer, ByVal CANAL As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
            If modulo = 0 Then
                CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ARCH_TAREA.LISTAR_TOT")
            Else
                CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_ARCH_TAREA.LISTAR")
            End If

            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = Poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpPeriodo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPeriodo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPeriodo")
                .DbType = DbType.Int64
                .Value = periodo
            End With
            CMDvar.Parameters.Add(ParpPeriodo)

            Dim ParpCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCiclo")
                .DbType = DbType.Int64
                .Value = ciclo
            End With
            CMDvar.Parameters.Add(ParpCiclo)

            Dim ParpModulo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpModulo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pModulo")
                .DbType = DbType.Int64
                .Value = modulo
            End With
            CMDvar.Parameters.Add(ParpModulo)

            Dim ParpArchivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpArchivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pArchivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpArchivo)

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = CANAL
            End With
            CMDvar.Parameters.Add(ParpCanal)

            If modulo = 0 Then
                Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "ListaTot")
                With vardata0
                    .Direction = ParameterDirection.Output
                End With
                CMDvar.Parameters.Add(vardata0)
            Else
                Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Lista")
                With vardata0
                    .Direction = ParameterDirection.Output
                End With
                CMDvar.Parameters.Add(vardata0)
            End If


            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Sub Archivo_Estado(ByVal Tipo As String, ByVal archivo As Integer, ByVal estado As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "SRV_TRX_UPD_ARCHIVO_ESTADO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpNum_archivo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNum_archivo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNum_archivo")
                .DbType = DbType.Int64
                .Value = archivo
            End With
            CMDvar.Parameters.Add(ParpNum_archivo)

            Dim ParpEstado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpEstado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pEstado")
                .DbType = DbType.String
                .Size = 1
                .Value = estado
            End With
            CMDvar.Parameters.Add(ParpEstado)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub

#End Region

#Region "Procedimientos de Recibos Familiares"

    Public Function Lista_ProductosCAFAE(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LOAD_RENOVACIONES.LISTAR_CAFAE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista2")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Registros_RecFam(ByVal Tipo As Integer, ByVal INTproducto As Integer, ByVal INTpoliza As Integer) As DataTable
        Dim dt As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LIST_RENOVACIONES.LISTAR_REC_FAM")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Double
                .Value = INTproducto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Double
                .Value = INTpoliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dt = New DataTable
            ADAvar.Fill(dt)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dt

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Crea_Job_Recibos_Familiares(ByVal Tipo As String, ByVal producto As Int64, ByVal poliza As Integer, ByVal certificados As Integer, _
                        ByVal FechaProxfact As String, ByVal Usuario As Integer) As String

        Try
            Dim sEstadoJob As String

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CREA_JOBBATCH_REC_FAM")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar
            'TRvar = CNXvar.BeginTransaction(IsolationLevel.Serializable)
            'CMDvar.Transaction = TRvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpCertificados As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCertificados
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCertificados")
                .DbType = DbType.Int64
                .Value = certificados
            End With
            CMDvar.Parameters.Add(ParpCertificados)

            Dim ParpProxfact As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpProxfact
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pProxfact")
                .DbType = DbType.String
                .Size = 10
                .Value = FechaProxfact
            End With
            CMDvar.Parameters.Add(ParpProxfact)

            Dim ParvNUSERCODE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNUSERCODE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNUSERCODE")
                .DbType = DbType.Int64
                .Value = Usuario
            End With
            CMDvar.Parameters.Add(ParvNUSERCODE)

            Dim ParEstadojob As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstadojob
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstadojob)

            CMDvar.ExecuteNonQuery()

            sEstadoJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")).Value
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing
            Return sEstadoJob

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

    Public Function Validar_Fecha_Factura(ByVal Tipo As String, ByVal sFechaFact As String) As String

        Dim sMensaje As String
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "VAL_FECHA_FACTURA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParsFEC_FACT As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsFEC_FACT
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@FEC_FACT")
                .DbType = DbType.String
                .Size = 10
                .Value = sFechaFact
            End With
            CMDvar.Parameters.Add(ParsFEC_FACT)

            Dim ParSMensaje As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParSMensaje
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PMSGVAL")
                .DbType = DbType.String
                .Size = 500
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParSMensaje)

            CMDvar.ExecuteNonQuery()
            sMensaje = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@PMSGVAL")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return sMensaje
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

    Public Function Validar_Estado_Poliza(ByVal tipo As String, ByVal producto As Int64, ByVal poliza As Int64) As String

        Dim sMensaje As String
        Try

            CNXvar = OBJconexion.ConexionDB(tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(tipo, "VAL_ESTADO_POLICY")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParSMensaje As DbParameter = OBJconexion.CrearParametro(tipo)
            With ParSMensaje
                .ParameterName = OBJconexion.Dev_Par(tipo, "@PMSGVAL")
                .DbType = DbType.String
                .Size = 500
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParSMensaje)

            CMDvar.ExecuteNonQuery()
            sMensaje = CMDvar.Parameters(OBJconexion.Dev_Par(tipo, "@PMSGVAL")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return sMensaje
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function


#End Region

#Region "Liquidaci�n de Comprobantes Serie 006"

    Public Function Lista_Recibos_Serie006(ByVal Tipo As Integer, ByVal INTproducto As Integer, ByVal INTpoliza As Integer) As DataTable
        Dim dt As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LIST_RENOVACIONES.LISTAR_LIQUIDACIONES")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Double
                .Value = INTproducto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Double
                .Value = INTpoliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "LISTA1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dt = New DataTable
            ADAvar.Fill(dt)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dt

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Validar_Fecha_Liquidacion(ByVal Tipo As String, ByVal vFechaLiquid As String) As String

        Dim sMensaje As String
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "VAL_FECHA_LIQUIDACION")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParsFEC_LIQUID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsFEC_LIQUID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@FEC_LIQUID")
                .DbType = DbType.String
                .Size = 10
                .Value = vFechaLiquid
            End With
            CMDvar.Parameters.Add(ParsFEC_LIQUID)

            Dim ParSMensaje As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParSMensaje
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PMSGVAL")
                .DbType = DbType.String
                .Size = 500
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParSMensaje)

            CMDvar.ExecuteNonQuery()
            sMensaje = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@PMSGVAL")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return sMensaje
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

    Public Function Crea_Job_Liquidacion_Recibos_Familiares(ByVal Tipo As String, ByVal producto As Int64, ByVal poliza As Integer, ByVal certificados As Integer, _
                        ByVal vFechaLiquid As String, ByVal Usuario As Integer) As String

        Try
            Dim sEstadoJob As String

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CREA_JOBBATCH_LIQUID")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar
            'TRvar = CNXvar.BeginTransaction(IsolationLevel.Serializable)
            'CMDvar.Transaction = TRvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpCertificados As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCertificados
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCertificados")
                .DbType = DbType.Int64
                .Value = certificados
            End With
            CMDvar.Parameters.Add(ParpCertificados)

            Dim ParsFEC_LIQUID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsFEC_LIQUID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pFEC_LIQUID")
                .DbType = DbType.String
                .Size = 10
                .Value = vFechaLiquid
            End With
            CMDvar.Parameters.Add(ParsFEC_LIQUID)

            Dim ParvNUSERCODE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNUSERCODE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNUSERCODE")
                .DbType = DbType.Int64
                .Value = Usuario
            End With
            CMDvar.Parameters.Add(ParvNUSERCODE)

            Dim ParEstadojob As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstadojob
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstadojob)

            CMDvar.ExecuteNonQuery()

            sEstadoJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")).Value
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing
            Return sEstadoJob

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

#End Region

#Region "Funciones varias"

    Public Enum DB
        SQL = 1
        ORACLE = 2
    End Enum

    Public Shared Sub AgregarItemCombo(ByVal combo As WebControls.DropDownList)
        combo.Items.Insert(0, "[ Selecciona ]")
        combo.Items(0).Value = ""
    End Sub

    Public Function StrEncode(ByVal s As String) As String
        '--------------------------------------------------------------------------------------------
        Dim key As Long
        Dim salt As Boolean
        Dim n As Long
        Dim i As Long
        Dim ss As String
        Dim k1 As Long
        Dim k2 As Long
        Dim k3 As Long
        Dim k4 As Long
        Dim t As Long
        'Dim sn As Long

        If Trim$(s) <> vbNullString Then

            key = 1234567890
            salt = False

            If salt Then
                For i = 1 To 4
                    t = 100 * (1 + Asc(Mid(saltvalue, i, 1))) * Rnd() * (Timer + 1)
                    Mid(saltvalue, i, 1) = Chr(t Mod 256)
                Next
                s = Mid(saltvalue, 1, 2) & s & Mid(saltvalue, 3, 2)
            End If

            n = Len(s)
            ss = Space(n)
            Dim sn(n) As Long
            'ReDim sn(0 To n)

            k1 = 11 + (key Mod 233) : k2 = 7 + (key Mod 239)
            k3 = 5 + (key Mod 241) : k4 = 3 + (key Mod 251)

            For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next

            For i = 2 To n : sn(i) = sn(i) Xor sn(i - 1) Xor _
                 ((k1 * sn(i - 1)) Mod 256) : Next
            For i = n - 1 To 1 Step -1 : sn(i) = sn(i) Xor sn(i + 1) Xor _
                 (k2 * sn(i + 1)) Mod 256 : Next
            For i = 3 To n : sn(i) = sn(i) Xor sn(i - 2) Xor _
                 (k3 * sn(i - 1)) Mod 256 : Next
            For i = n - 2 To 1 Step -1 : sn(i) = sn(i) Xor sn(i + 2) Xor _
                 (k4 * sn(i + 1)) Mod 256 : Next

            For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next
            StrEncode = ss
            Return StrEncode
        End If

    End Function

    Public Function StrDecode(ByVal s As String) As String
        '--------------------------------------------------------------------------------------------
        Dim key As Long
        Dim salt As Boolean
        Dim n As Long
        Dim i As Long
        Dim ss As String
        Dim k1 As Long
        Dim k2 As Long
        Dim k3 As Long
        Dim k4 As Long

        If Trim$(s) <> vbNullString Then

            key = 1234567890
            salt = False

            n = Len(s)
            ss = Space(n)
            Dim sn(n) As Long

            k1 = 11 + (key Mod 233) : k2 = 7 + (key Mod 239)
            k3 = 5 + (key Mod 241) : k4 = 3 + (key Mod 251)

            For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next

            For i = 1 To n - 2 : sn(i) = sn(i) Xor sn(i + 2) Xor _
                 (k4 * sn(i + 1)) Mod 256 : Next
            For i = n To 3 Step -1 : sn(i) = sn(i) Xor sn(i - 2) Xor _
                 (k3 * sn(i - 1)) Mod 256 : Next
            For i = 1 To n - 1 : sn(i) = sn(i) Xor sn(i + 1) Xor _
                 (k2 * sn(i + 1)) Mod 256 : Next
            For i = n To 2 Step -1 : sn(i) = sn(i) Xor sn(i - 1) Xor _
                 (k1 * sn(i - 1)) Mod 256 : Next

            For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next i

            If salt Then StrDecode = Mid(ss, 3, Len(ss) - 4) Else StrDecode = ss
        End If
        Return StrDecode
    End Function

    'Public Sub StartProcess(ByVal file As String, ByVal args As String, ByVal redirectStdout As Boolean, ByVal dir As String)
    '    Dim p As Process

    '        p.Exited += new EventHandler(p_Exited); 
    '        p.Start(); 

    '    If (redirectStdout) Then
    '        { 
    '            stdout = p.StandardOutput.ReadToEnd(); 
    '        } 
    '        Dim psi As New ProcessStartInfo
    '        p = New Process
    '        psi.WindowStyle = ProcessWindowStyle.Hidden
    '        psi.WorkingDirectory = dir
    '        psi.FileName = file
    '        psi.UseShellExecute = False
    '        psi.RedirectStandardOutput = redirectStdout
    '        p.StartInfo = psi
    '        p.EnableRaisingEvents = True
    '        p.ex()
    '        p.Start()
    '        '********



    '     foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcesses())
    '        {
    '            if (proc.ProcessName == nombre_proceso)
    '                System.Diagnostics.Process.Start("taskkill", "/IM " + proceso.exe);                 
    '        } 



    'End Sub

    'Private Sub p_Exited(ByVal sender As Object, ByVal e As EventArgs)

    'End Sub


  
#End Region

#Region "Funciones para Reportes"

    Public Function Lista_Client_SCTR(ByVal Tipo As Integer, ByVal producto As String, ByVal poliza As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_SCTR.LISTAR_CLIENT")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Endosos_SCTR(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal poliza As String, ByVal fecIniefecto As String, ByVal fecFinefecto As String, ByVal Estado As String, ByVal RUC As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_SCTR.LISTAR_ENDOSO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.String
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.String
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim ParEstado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_ESTADO")
                .DbType = DbType.String
                .Size = 1
                .Value = Estado
            End With
            CMDvar.Parameters.Add(ParEstado)

            Dim ParRUC As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParRUC
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_RUC")
                .DbType = DbType.String
                .Size = 11
                .Value = RUC
            End With
            CMDvar.Parameters.Add(ParRUC)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_DC_SCTR(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal poliza As String, ByVal tipodocumento As String, ByVal estado As String, ByVal fecIniefecto As DateTime, ByVal fecFinefecto As DateTime) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_SCTR.LISTAR_DC")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim Partipodocumento As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Partipodocumento
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_TIPODOCUMENTO")
                .DbType = DbType.String
                .Size = 30
                .Value = tipodocumento
            End With
            CMDvar.Parameters.Add(Partipodocumento)

            Dim Parestado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parestado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_ESTADO")
                .DbType = DbType.String
                .Size = 30
                .Value = estado
            End With
            CMDvar.Parameters.Add(Parestado)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.DateTime
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.DateTime
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_CO_SCTR(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal poliza As String, ByVal estado As String, ByVal fecIniefecto As DateTime, ByVal fecFinefecto As DateTime) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_SCTR.LISTAR_CO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim Parestado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parestado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_ESTADO")
                .DbType = DbType.String
                .Size = 30
                .Value = estado
            End With
            CMDvar.Parameters.Add(Parestado)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.DateTime
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.DateTime
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_DetEndosos_SCTR(ByVal Tipo As Integer, ByVal producto As String, ByVal poliza As String, ByVal renovacion As String, ByVal nummov As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_SCTR.LISTAR_DET_ENDOSO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim ParRenovacion As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParRenovacion
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_RENOVACION")
                .DbType = DbType.String
                .Size = 10
                .Value = renovacion
            End With
            CMDvar.Parameters.Add(ParRenovacion)

            Dim ParEndoso As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEndoso
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NMOVMIENTO")
                .DbType = DbType.String
                .Size = 10
                .Value = nummov
            End With
            CMDvar.Parameters.Add(ParEndoso)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Recibos_SCTR(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal poliza As String, ByVal estado As String, ByVal fecIniefecto As String, ByVal fecFinefecto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_SCTR.LISTAR_RECIBOS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim Parestado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parestado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_ESTADO")
                .DbType = DbType.String
                .Size = 30
                .Value = estado
            End With
            CMDvar.Parameters.Add(Parestado)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.String
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.String
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_Recibos_CAFAE(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal poliza As String, ByVal fecIniefecto As String, ByVal fecFinefecto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_CAFAE.LISTAR_RECIBOS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parpoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parpoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.String
                .Size = 30
                .Value = poliza
            End With
            CMDvar.Parameters.Add(Parpoliza)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.String
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.String
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function


    Public Function Lista_Report_VISANET(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal comercio As String, ByVal fecIniefecto As String, ByVal fecFinefecto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_VISANET.LISTAR_VISANET")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parcomercio As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcomercio
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCOMERCIO")
                .DbType = DbType.String
                .Size = 30
                .Value = comercio
            End With
            CMDvar.Parameters.Add(Parcomercio)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.String
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.String
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Report_DC_Pendientes(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As String, ByVal comercio As String, ByVal fecIniefecto As String, ByVal fecFinefecto As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_REPORTES_VISANET.LISTAR_DC_PENDIENTES")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Parcanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCANAL")
                .DbType = DbType.String
                .Size = 30
                .Value = canal
            End With
            CMDvar.Parameters.Add(Parcanal)

            Dim Parproducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parproducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.String
                .Size = 30
                .Value = producto
            End With
            CMDvar.Parameters.Add(Parproducto)

            Dim Parcomercio As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parcomercio
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NCOMERCIO")
                .DbType = DbType.String
                .Size = 30
                .Value = comercio
            End With
            CMDvar.Parameters.Add(Parcomercio)

            Dim ParFecIniEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecIniEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECINI")
                .DbType = DbType.String
                .Size = 10
                .Value = fecIniefecto
            End With
            CMDvar.Parameters.Add(ParFecIniEfecto)

            Dim ParFecFinEfecto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecFinEfecto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_FECFIN")
                .DbType = DbType.String
                .Size = 10
                .Value = fecFinefecto
            End With
            CMDvar.Parameters.Add(ParFecFinEfecto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "REPORTE")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

#End Region

#Region "Funciones Renovaciones"

    Public Function Lista_Renovacion(ByVal Tipo As Integer, ByVal INTproducto As Integer, ByVal INTpoliza As Integer, ByVal STRperiodo As String, ByVal Opcion As String) As DataTable
        Dim dt As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LIST_RENOVACIONES.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Double
                .Value = INTproducto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Double
                .Value = INTpoliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim ParPERIODO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPERIODO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PERIODO")
                .DbType = DbType.String
                .Size = 6
                .Value = STRperiodo
            End With
            CMDvar.Parameters.Add(ParPERIODO)

            Dim ParOPCION As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParOPCION
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@OPCION")
                .DbType = DbType.String
                .Size = 1
                .Value = Opcion
            End With
            CMDvar.Parameters.Add(ParOPCION)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dt = New DataTable
            ADAvar.Fill(dt)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dt

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Load_Renovacion(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LOAD_RENOVACIONES.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista2")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Crea_Job_Renovaciones(ByVal Tipo As String, ByVal poliza As Integer, ByVal certificado As Integer, _
                    ByVal FechaProxfactI As Integer, ByVal FechaProxfactS As Integer, ByVal nintermed As Integer, ByVal modo As Integer, _
                    ByVal producto As Int64, ByVal ramo As Integer, ByVal Usuario As Integer, ByVal tablaProdHabilitados As DataTable) As String


        Dim sEstadoJob As String = ""
        Try
            Dim vkey As String = ""

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CREA_JOBBATCH_RENOVACION")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar
            'TRvar = CNXvar.BeginTransaction(IsolationLevel.Serializable)
            'CMDvar.Transaction = TRvar

            Dim ParpNpolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNpolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNpolicy")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpNpolicy)

            Dim ParpCertificat As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCertificat
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCertificat")
                .DbType = DbType.Int64
                .Value = certificado
            End With
            CMDvar.Parameters.Add(ParpCertificat)

            Dim ParpProxfactI As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpProxfactI
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pProxfact")
                .DbType = DbType.Double
                .Value = FechaProxfactI
            End With
            CMDvar.Parameters.Add(ParpProxfactI)

            Dim ParpProxfactS As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpProxfactS
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pProxfact2")
                .DbType = DbType.Double
                .Value = FechaProxfactS
            End With
            CMDvar.Parameters.Add(ParpProxfactS)


            Dim ParpNintermed As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNintermed
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNintermed")
                .DbType = DbType.Int64
                .Value = nintermed
            End With
            CMDvar.Parameters.Add(ParpNintermed)

            Dim ParpTipo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpTipo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pTipo")
                .DbType = DbType.Int64
                .Value = modo
            End With
            CMDvar.Parameters.Add(ParpTipo)

            Dim ParpNumTarea As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNumTarea
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNumTarea")
                .DbType = DbType.Int64

                Dim fila2() As DataRow = tablaProdHabilitados.Select("NPRODUCT='" & producto & "'")
                If fila2.Length > 0 Then
                    .Value = 170   'renovacion nueva
                Else
                    .Value = 110   'renovacion antigua
                End If
                'If producto = 20 Or producto = 25 Or producto = 10 Or producto = 27 Or producto = 54 Or producto = 83 Or producto = 72 Or producto = 56 Or producto = 84 Then

            End With
            CMDvar.Parameters.Add(ParpNumTarea)


            Dim ParvNUSERCODE As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNUSERCODE
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNUSERCODE")
                .DbType = DbType.Int64
                .Value = Usuario
            End With
            CMDvar.Parameters.Add(ParvNUSERCODE)


            'Dim ParpkeyOut As DbParameter = OBJconexion.CrearParametro(Tipo)
            'With ParpkeyOut
            '    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pkeyOut")
            '    .DbType = DbType.String
            '    .Size = 20
            '    .Direction = ParameterDirection.Output
            'End With
            'CMDvar.Parameters.Add(ParpkeyOut)

            Dim ParEstadojob As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstadojob
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstadojob)


            CMDvar.ExecuteNonQuery()
            'vkey = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@pkeyOut")).Value
            sEstadoJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@SESTADOJOB")).Value
            '********* CUANDO ES CERTIFICADO EXECUTA LA TAREA *********



            'If modo = 1 Then 'si es por certificado hace el 110 sino la tarea q genero lanza el 170
            '    CMDvar = OBJconexion.CrearCommando(Tipo, "INSBTC00110")
            '    CMDvar.CommandType = CommandType.StoredProcedure
            '    CMDvar.Connection = CNXvar
            '    'CMDvar.Transaction = TRvar

            '    Dim ParSKEY As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParSKEY
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@SKEY")
            '        .DbType = DbType.String
            '        .Size = 20
            '        .Value = vkey
            '    End With
            '    CMDvar.Parameters.Add(ParSKEY)

            '    Dim ParNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNBRANCH
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NBRANCH")
            '        .DbType = DbType.Int64
            '        .Value = ramo
            '    End With
            '    CMDvar.Parameters.Add(ParNBRANCH)

            '    Dim ParNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNPRODUCT
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NPRODUCT")
            '        .DbType = DbType.Int64
            '        .Value = producto
            '    End With
            '    CMDvar.Parameters.Add(ParNPRODUCT)

            '    Dim ParNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNPOLICY
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NPOLICY")
            '        .DbType = DbType.Int64
            '        .Value = poliza
            '    End With
            '    CMDvar.Parameters.Add(ParNPOLICY)

            '    Dim ParNCERTIF As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNCERTIF
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NCERTIF")
            '        .DbType = DbType.Int64
            '        .Value = certificado
            '    End With
            '    CMDvar.Parameters.Add(ParNCERTIF)

            '    Dim ParDPRENEWDAT_I As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParDPRENEWDAT_I
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@DPRENEWDAT_I")
            '        .DbType = DbType.Date
            '        .Value = System.DBNull.Value
            '    End With
            '    CMDvar.Parameters.Add(ParDPRENEWDAT_I)

            '    Dim ParDPRENEWDAT_E As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParDPRENEWDAT_E
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@DPRENEWDAT_E")
            '        .DbType = DbType.Date
            '        .Value = FechaProxfact
            '    End With
            '    CMDvar.Parameters.Add(ParDPRENEWDAT_E)

            '    Dim ParNOFFICE As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNOFFICE
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NOFFICE")
            '        .DbType = DbType.Double
            '        .Value = System.DBNull.Value
            '    End With
            '    CMDvar.Parameters.Add(ParNOFFICE)

            '    Dim ParNOFFICEAGEN As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNOFFICEAGEN
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NOFFICEAGEN")
            '        .DbType = DbType.Double
            '        .Value = System.DBNull.Value
            '    End With
            '    CMDvar.Parameters.Add(ParNOFFICEAGEN)

            '    Dim ParNINTERMED As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNINTERMED
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NINTERMED")
            '        .DbType = DbType.Double
            '        .Value = System.DBNull.Value
            '    End With
            '    CMDvar.Parameters.Add(ParNINTERMED)

            '    Dim ParNUSERCODE As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNUSERCODE
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NUSERCODE")
            '        .DbType = DbType.Int64
            '        .Value = Usuario
            '    End With
            '    CMDvar.Parameters.Add(ParNUSERCODE)

            '    Dim ParNPREVAL_YEAR As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNPREVAL_YEAR
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NPREVAL_YEAR")
            '        .DbType = DbType.Double
            '        .Value = 0
            '    End With
            '    CMDvar.Parameters.Add(ParNPREVAL_YEAR)

            '    Dim ParNPREVAL_MONT As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNPREVAL_MONT
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NPREVAL_MONT")
            '        .DbType = DbType.Double
            '        .Value = 0
            '    End With
            '    CMDvar.Parameters.Add(ParNPREVAL_MONT)

            '    Dim ParNPPROCTYPE As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNPPROCTYPE
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NPPROCTYPE")
            '        .DbType = DbType.Double
            '        .Value = 99
            '    End With
            '    CMDvar.Parameters.Add(ParNPPROCTYPE)

            '    Dim ParNMASIVE As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNMASIVE
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NMASIVE")
            '        .DbType = DbType.Double
            '        .Value = 2
            '    End With
            '    CMDvar.Parameters.Add(ParNMASIVE)

            '    Dim ParNAGENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
            '    With ParNAGENCY
            '        .ParameterName = OBJconexion.Dev_Par(Tipo, "@NAGENCY")
            '        .DbType = DbType.Double
            '        .Value = System.DBNull.Value
            '    End With
            '    CMDvar.Parameters.Add(ParNAGENCY)

            '    CMDvar.ExecuteNonQuery()

            'End If

            'TRvar.Commit()

            CNXvar.Close()
            CNXvar = Nothing

            Return sEstadoJob
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

    Public Function Lista_Conf_producto(ByVal Tipo As Integer, ByVal Ramo As Integer, ByVal producto As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CONF_PRODUCTO.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParvNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNBRANCH
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNBRANCH")
                .DbType = DbType.Int64
                .Value = Ramo
            End With
            CMDvar.Parameters.Add(ParvNBRANCH)

            Dim ParvNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNPRODUCT
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNPRODUCT")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParvNPRODUCT)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Fecha_Expiracion(ByVal Tipo As Integer, ByVal RAMO As Integer, ByVal producto As Integer, ByVal POLIZA As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_FECHA_EXPIRACION.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParvNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNBRANCH
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NBRANCH")
                .DbType = DbType.Int64
                .Value = RAMO
            End With
            CMDvar.Parameters.Add(ParvNBRANCH)

            Dim ParvNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNPRODUCT
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParvNPRODUCT)

            Dim ParvNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNPOLICY
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPOLICY")
                .DbType = DbType.Int64
                .Value = POLIZA
            End With
            CMDvar.Parameters.Add(ParvNPOLICY)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Fecha_Expiracion_SCTR(ByVal Tipo As Integer, ByVal RAMO As Integer, ByVal producto As Integer, ByVal sMovSTRC As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_FECHA_EXPIRACION.LISTAR_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParvNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNBRANCH
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NBRANCH")
                .DbType = DbType.Int64
                .Value = RAMO
            End With
            CMDvar.Parameters.Add(ParvNBRANCH)

            Dim ParvNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNPRODUCT
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_NPRODUCT")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParvNPRODUCT)

            Dim ParvNMOVSCTR As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNMOVSCTR
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PAR_MOVSCTR")
                .DbType = DbType.String
                .Value = sMovSTRC
            End With
            CMDvar.Parameters.Add(ParvNMOVSCTR)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    Public Function Lista_Existe_Nreceipt_null(ByVal Tipo As Integer, ByVal Ramo As Integer, ByVal producto As Integer, ByVal poliza As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CONF_VALIDA_NRECEIPT.LISTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParvNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNBRANCH
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNBRANCH")
                .DbType = DbType.Int64
                .Value = Ramo
            End With
            CMDvar.Parameters.Add(ParvNBRANCH)

            Dim ParvNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNPRODUCT
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNPRODUCT")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParvNPRODUCT)

            Dim ParvNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvNPOLICY
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@vNPOLICY")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParvNPOLICY)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function

    'INICIO RQ2017-0100001 EFITEC-MCM
    Public Function Control_Procesos(ByVal Tipo As String, ByVal caso As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal user As String, ByVal terminal As String)

        Dim intCiclo As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CONTROL_PROCESOS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCase As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCase
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PCASE")
                .DbType = DbType.Int64
                .Value = caso
            End With
            CMDvar.Parameters.Add(ParpCase)

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PSCANAL")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPRODUCT")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)


            Dim ParsPolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParsPolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPOLICY")
                .DbType = DbType.String
                .Size = 10
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParsPolicy)

            Dim ParpUser As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpUser
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PSUSER")
                .DbType = DbType.String
                .Value = user
            End With
            CMDvar.Parameters.Add(ParpUser)

            Dim ParpSTerminal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSTerminal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PSTERMINAL")
                .DbType = DbType.String
                .Value = terminal
            End With
            CMDvar.Parameters.Add(ParpSTerminal)


            CMDvar.ExecuteNonQuery()

            CNXvar.Close()
            CNXvar = Nothing

            Return intCiclo
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function

    Public Function Val_Estado_Poliza_En_Proceso(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal poliza As String)

        Dim intCountJob As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_CONSULTAR_ESTADO_POLIZA_PR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pCanal")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNproduct")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPoliza")
                .DbType = DbType.String
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)


            Dim ParNCiclo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNCiclo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NVALIDADOR")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParNCiclo)

            CMDvar.ExecuteNonQuery()
            intCountJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@NVALIDADOR")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return intCountJob
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function

    'Consulta estado de poliza en proceso
    Public Function Consulta_Poliza_En_Proceso(ByVal Tipo As String, ByVal canal As String, ByVal producto As Integer, ByVal poliza As String, ByVal usuario As String) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_LISTA_POLICY_PR.CONSULTAR_ESTADO_POLIZA_PR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PCANAL")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpNproduct As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpNproduct
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPRODUCT")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParpNproduct)

            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PPOLIZA")
                .DbType = DbType.String
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)


            Dim ParUSUARIO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParUSUARIO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PSUSER")
                .DbType = DbType.String
                .Size = 12
                .Value = usuario
            End With
            CMDvar.Parameters.Add(ParUSUARIO)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "lista1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function




    Public Function Ejecuta_comando_90_pr(ByVal Tipo As String, ByVal skey As String)
        Dim intCountJob As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "INT_EJECUTA_COMANDO_90_PR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpSkey As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSkey
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PSKEY")
                .DbType = DbType.String
                .Size = 16
                .Value = skey
            End With
            CMDvar.Parameters.Add(ParpSkey)


            Dim ParNStatus As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNStatus
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PSKEYSTATUS")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParNStatus)

            CMDvar.ExecuteNonQuery()
            intCountJob = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@PSKEYSTATUS")).Value

            CNXvar.Close()
            CNXvar = Nothing

            Return intCountJob
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try

    End Function

    Public Sub ELIMINA_POLIZA_PR(ByVal Tipo As String, ByVal canal As String, ByVal poliza As String)

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "SRV_TRX_DEL_POLIZA_PR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PCANAL")
                .DbType = DbType.String
                .Size = 16
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)


            Dim ParpPoliza As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPoliza
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPOLICY")
                .DbType = DbType.String
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParpPoliza)

            CMDvar.ExecuteNonQuery()
            CMDvar.Parameters.Clear()

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Sub
    'FIN RQ2017-0100001 EFITEC-MCM
#End Region


#Region "REQ2017-010003-INSUDB MATRIZ MASIVA EFITEC PPP"


    Public Function ListGridModul(ByVal Tipo As Integer, ByVal vFecha As String) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.LISTAR_MODUL")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParvFecha As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvFecha
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@dFecha")
                .DbType = DbType.String
                .Value = vFecha
            End With
            CMDvar.Parameters.Add(ParvFecha)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Modulo")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try
        Return dts
    End Function

    Public Function List_Param_Formato_SCTR(ByVal Tipo As Integer, ByVal vProducto As Integer) As DataSet

        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.LISTAR_FORMATO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NPRODC_POL")
                .DbType = DbType.Int64
                .Value = vProducto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try
        Return dts
    End Function

    Public Function List_Emision(ByVal Tipo As Integer, ByVal ID As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.REAMASSIVE_REP")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NCONTROL")
                .DbType = DbType.Int64
                .Value = ID
            End With
            CMDvar.Parameters.Add(ParpID)


            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "RC1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            CNXvar = Nothing
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try
        Return dts
    End Function

    Public Function List_Error_SCTR_Masiva(ByVal Tipo As Integer, ByVal ID As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.LISTAR_SCTR_MASIVO_ERRORES")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nID_")
                .DbType = DbType.Int64
                .Value = ID
            End With
            CMDvar.Parameters.Add(ParpID)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "ListError")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "ErrorTrama")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Error_INS")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "EMISION")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)



            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            CNXvar = Nothing
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try

    End Function

    Public Function List_Format_SCTR_Masiva(ByVal Tipo As Integer, ByVal vFormato As String) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.LISTAR_SCTR_MASIVO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParCANAL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCANAL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@VFORMATO")
                .DbType = DbType.String
                .Size = 20
                .Value = vFormato
            End With
            CMDvar.Parameters.Add(ParCANAL)



            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Columna")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)



            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function ExistPoliza(ByVal Tipo As String, ByVal nPolicy As String) As Integer

        Dim exists As Integer = 0

        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.INT_EXIST_SCTR_POLIZA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpPolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPolicy")
                .DbType = DbType.Int64
                .Value = Convert.ToInt64(nPolicy)
            End With
            CMDvar.Parameters.Add(ParpPolicy)
            Dim ParnCount As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnCount
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCount")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnCount)

            CMDvar.ExecuteNonQuery()
            exists = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nCount")).Value

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            exists = 0
        End Try
        Return exists

    End Function

    Public Function CloseLoadFileSctr(ByVal Tipo As String, ByVal Obeservacion As String, ByVal id As Integer) As Boolean
        Dim status As Boolean = True

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.INT_UPD_SCTR_MASIVAS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParpID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pID")
                .DbType = DbType.Int64
                .Value = id
            End With
            CMDvar.Parameters.Add(ParpID)

            Dim ParpComentary As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpComentary
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pComentary")
                .DbType = DbType.String
                .Size = 255
                .Value = Obeservacion
            End With
            CMDvar.Parameters.Add(ParpComentary)

            CMDvar.ExecuteNonQuery()

            CNXvar.Close()
            CNXvar = Nothing
            status = True
        Catch ex As Exception
            CNXvar = Nothing
            status = False
        End Try

        Return status

    End Function

    Public Function OpenLoadFileSctr(ByVal Tipo As String, ByVal canal As String, ByVal RutaOrigen As String, ByVal cabecera As Integer, ByVal DescArchivo As String, ByVal glosa As String, ByVal CodUser As Integer) As Integer

        Dim idArchivo As Integer = 0

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.INT_INS_SCTR_MASIVAS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pChannel")
                .DbType = DbType.String
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParpSourFile As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSourFile
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSourFile")
                .DbType = DbType.String
                .Size = 255
                .Value = RutaOrigen
            End With
            CMDvar.Parameters.Add(ParpSourFile)


            Dim ParpFile_ As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpFile_
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pFile_")
                .DbType = DbType.String
                .Size = 255
                .Value = DescArchivo
            End With
            CMDvar.Parameters.Add(ParpFile_)

            Dim ParpHeader As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpHeader
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pHeader")
                .DbType = DbType.Int64
                .Value = cabecera
            End With
            CMDvar.Parameters.Add(ParpHeader)

            Dim ParpComentary As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpComentary
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pComentary")
                .DbType = DbType.String
                .Size = 255
                .Value = glosa
            End With
            CMDvar.Parameters.Add(ParpComentary)

            Dim ParpUserCode As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpUserCode
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pUserCode")
                .DbType = DbType.Int64
                .Value = CodUser
            End With
            CMDvar.Parameters.Add(ParpUserCode)

            Dim ParnNumFile As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnNumFile
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nNumFile")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnNumFile)


            CMDvar.ExecuteNonQuery()
            idArchivo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nNumFile")).Value

            CNXvar.Close()
            CNXvar = Nothing


        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            idArchivo = 0
        End Try

        Return idArchivo

    End Function



    Public Function ValidateTrama(ByVal Tipo As String, ByVal IDArchivo As String) As Integer

        Dim exists As Integer = 0
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
            CMDvar = OBJconexion.CrearCommando(Tipo, "VALMASSIVE_LOAD")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim pNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With pNCONTROL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NCONTROL")
                .DbType = DbType.Int64
                .Value = Convert.ToInt64(IDArchivo)
            End With
            CMDvar.Parameters.Add(pNCONTROL)
            Dim ParnCount As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnCount
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NREGVAL")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnCount)

            CMDvar.ExecuteNonQuery()
            exists = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@NREGVAL")).Value

            CMDvar.ExecuteNonQuery()

            CNXvar.Close()
            CNXvar = Nothing
        Catch ex As Exception
            exists = 0
            CNXvar.Close()
            CNXvar = Nothing
        End Try


        Return exists
    End Function

    Public Function Ins_ErrorTrama(ByVal Tipo As String, ByVal dtsTable As DataTable, ByVal IDArchivo As String) As Boolean
        Dim status As Boolean = False
        Try



            For Each row As DataRow In dtsTable.Rows

                CNXvar = OBJconexion.ConexionDB(Tipo)
                If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
                CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.INT_INS_TRAMA_ERROR")
                CMDvar.CommandType = CommandType.StoredProcedure
                CMDvar.Connection = CNXvar

                Dim pNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONTROL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCONTROL")
                    .DbType = DbType.Int64
                    .Value = IDArchivo
                End With
                CMDvar.Parameters.Add(pNCONTROL)
                Dim pNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNBRANCH
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNBRANCH")
                    .DbType = DbType.String
                    .Value = row("NBRANCH")
                End With
                CMDvar.Parameters.Add(pNBRANCH)
                Dim pNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPRODUCT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPRODUCT")
                    .DbType = DbType.String
                    .Value = row("NPRODUCT")
                End With
                CMDvar.Parameters.Add(pNPRODUCT)
                Dim pNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPOLICY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPOLICY")
                    .DbType = DbType.String
                    .Value = row("NPOLICY")
                End With
                CMDvar.Parameters.Add(pNPOLICY)
                Dim pNOFFICE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNOFFICE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNOFFICE")
                    .DbType = DbType.String
                    .Value = row("NOFFICE")
                End With
                CMDvar.Parameters.Add(pNOFFICE)
                Dim pNSELCHANNEL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNSELCHANNEL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSELLCHANNEL")
                    .DbType = DbType.String
                    .Value = row("NSELLCHANNEL")
                End With
                CMDvar.Parameters.Add(pNSELCHANNEL)
                Dim pSBUSSITYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSITYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUSSITYP")
                    .DbType = DbType.String
                    .Value = row("SBUSSITYP")
                End With
                CMDvar.Parameters.Add(pSBUSSITYP)
                Dim pSCOLINVOT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLINVOT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLINVOT")
                    .DbType = DbType.String
                    .Value = row("SCOLINVOT")
                End With
                CMDvar.Parameters.Add(pSCOLINVOT)
                Dim pSCOLREINT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLREINT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLREINT")
                    .DbType = DbType.String
                    .Value = row("SCOLREINT")
                End With
                CMDvar.Parameters.Add(pSCOLREINT)
                Dim pSCOLTIMRE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLTIMRE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLTIMRE")
                    .DbType = DbType.String
                    .Value = row("SCOLTIMRE")
                End With
                CMDvar.Parameters.Add(pSCOLTIMRE)
                Dim pDSTARDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDSTARDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDSTARTDATE")
                    .DbType = DbType.String
                    .Value = row("DSTARTDATE")
                End With
                CMDvar.Parameters.Add(pDSTARDATE)
                Dim pDENDDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDENDDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDEXPIRDAT")
                    .DbType = DbType.String
                    .Value = row("DEXPIRDAT")
                End With
                CMDvar.Parameters.Add(pDENDDATE)
                Dim pNFREQUENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFREQUENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPAYFREQ")
                    .DbType = DbType.String
                    .Value = row("NPAYFREQ")
                End With
                CMDvar.Parameters.Add(pNFREQUENCY)
                Dim pSCODE_CONTRCTOR As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_CONTRCTOR
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCLIENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SCLIENT_CONT")
                End With
                CMDvar.Parameters.Add(pSCODE_CONTRCTOR)
                Dim pSCODE_INTERMDIATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_INTERMDIATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERMED")
                    .DbType = DbType.String
                    .Value = row("NINTERMED")
                End With
                CMDvar.Parameters.Add(pSCODE_INTERMDIATE)
                Dim pSCODE_INTERMDIATE_SBS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_INTERMDIATE_SBS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSINTER_ID")
                    .DbType = DbType.String
                    .Value = row("SINTER_ID")
                End With
                CMDvar.Parameters.Add(pSCODE_INTERMDIATE_SBS)
                Dim pSUSER_SANITAS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSUSER_SANITAS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSUSR_SANITAS")
                    .DbType = DbType.String
                    .Value = row("SUSR_SANITAS")
                End With
                CMDvar.Parameters.Add(pSUSER_SANITAS)
                Dim pNCONN_PERCEN As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONN_PERCEN
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERCENT")
                    .DbType = DbType.String
                    .Value = row("NPERCENT")
                End With
                CMDvar.Parameters.Add(pNCONN_PERCEN)
                Dim pNCOD_CIIU As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOD_CIIU
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSPECIALITY")
                    .DbType = DbType.String
                    .Value = row("NSPECIALITY")
                End With
                CMDvar.Parameters.Add(pNCOD_CIIU)
                Dim pSTYPE_PERSON_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_PERSON_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_CONT")
                    .DbType = DbType.String
                    .Value = row("NPERSON_TYP_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_PERSON_C)
                Dim pSTYPE_DOCUMNT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_DOCUMNT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_CONT")
                    .DbType = DbType.String
                    .Value = row("NIDDOC_TYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_DOCUMNT_C)
                Dim pSNATIONALITY_C_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNATIONALITY_C_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_CONT")
                    .DbType = DbType.String
                    .Value = row("NCOUNTRY_CONT")
                End With
                CMDvar.Parameters.Add(pSNATIONALITY_C_C)
                Dim pSMUNICIPALITY_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSMUNICIPALITY_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_CONT")
                    .DbType = DbType.String
                    .Value = row("NMUNICIPALITY_CONT")
                End With
                CMDvar.Parameters.Add(pSMUNICIPALITY_C)
                Dim pSNCODE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNCODE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_CONT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_CONT")
                End With
                CMDvar.Parameters.Add(pSNCODE_C)
                Dim pSBUSSINES_NAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSINES_NAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSBUSSINES_NAME_C)
                Dim pSNAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSNAME_C)
                Dim pSLAST_FAT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_FAT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_FAT_C)
                Dim pSLAST_MOT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_C)
                Dim pDBIRTHDATE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDATE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_CONT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_CONT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDATE_C)
                Dim pSCIVIL_STUS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCIVIL_STUS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_CONT")
                    .DbType = DbType.String
                    .Value = row("NCIVILSTA_CONT")
                End With
                CMDvar.Parameters.Add(pSCIVIL_STUS_C)
                Dim pSSEXCLIEN_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_CONT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_CONT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_CONT)
                Dim pSE_MAIL_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_CONT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_CONT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_CONT)
                Dim pSRECTYPE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_CONT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_CONT)
                Dim pSADDRESS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSADDRESS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_CONT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_CONT")
                End With
                CMDvar.Parameters.Add(pSADDRESS_C)
                Dim pSBUILD_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_CONT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_CONT")
                End With
                CMDvar.Parameters.Add(pSBUILD_CONT)
                Dim pNFLOOR_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_CONT")
                    .DbType = DbType.String
                    .Value = row("NFLOOR_CONT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_CONT)
                Dim pSDEPARTAMENT_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_CONT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_CONT)
                Dim pSPOPULATION_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_CONT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_CONT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_CONT)
                Dim pSREFERENCE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_CONT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_CONT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_CONT)
                Dim pNZIPCODE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_CONT")
                    .DbType = DbType.String
                    .Value = row("NZIPCODE_CONT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_CONT)
                Dim pSPHONE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_CONT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_CONT")
                End With
                CMDvar.Parameters.Add(pSPHONE_CONT)
                Dim pNPERSON_TYP_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPERSON_TYP_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_INT")
                    .DbType = DbType.String
                    .Value = row("NPERSON_TYP_INT")
                End With
                CMDvar.Parameters.Add(pNPERSON_TYP_INT)
                Dim pNIDDOC_TYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNIDDOC_TYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_INT")
                    .DbType = DbType.String
                    .Value = row("NIDDOC_TYPE_INT")
                End With
                CMDvar.Parameters.Add(pNIDDOC_TYPE_INT)
                Dim pNCOUNTRY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOUNTRY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_INT")
                    .DbType = DbType.String
                    .Value = row("NCOUNTRY_INT")
                End With
                CMDvar.Parameters.Add(pNCOUNTRY_INT)
                Dim pNMUNICIPALITY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNMUNICIPALITY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_INT")
                    .DbType = DbType.String
                    .Value = row("NMUNICIPALITY_INT")
                End With
                CMDvar.Parameters.Add(pNMUNICIPALITY_INT)

                Dim pSIDDOC_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSIDDOC_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_INT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_INT")
                End With
                CMDvar.Parameters.Add(pSIDDOC_INT)
                Dim pSLEGALNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLEGALNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_INT")
                End With
                CMDvar.Parameters.Add(pSLEGALNAME_INT)
                Dim pSFIRSTNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSFIRSTNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_INT")
                End With
                CMDvar.Parameters.Add(pSFIRSTNAME_INT)
                Dim pSLASTNAME1_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME1_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME1_INT)


                Dim pSLASTNAME2_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME2_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME2_INT)
                Dim pDBIRTHDAT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDAT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_INT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_INT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDAT_INT)
                Dim pNCIVILSTA_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCIVILSTA_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_INT")
                    .DbType = DbType.String
                    .Value = row("NCIVILSTA_INT")
                End With
                CMDvar.Parameters.Add(pNCIVILSTA_INT)
                Dim pSSEXCLIEN_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_INT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_INT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_INT)
                Dim pSLAST_MOT_I As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_I
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNVALSUSALUD")
                    .DbType = DbType.String
                    .Value = row("NVALSUSALUD")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_I)
                Dim pSE_MAIL_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_INT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_INT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_INT)
                Dim pSRECTYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_INT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_INT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_INT)
                Dim pSSTREET_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSTREET_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_INT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_INT")
                End With
                CMDvar.Parameters.Add(pSSTREET_INT)
                Dim pSBUILD_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_INT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_INT")
                End With
                CMDvar.Parameters.Add(pSBUILD_INT)
                Dim pNFLOOR_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_INT")
                    .DbType = DbType.String
                    .Value = row("NFLOOR_INT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_INT)
                Dim pSDEPARTAMENT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_INT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_INT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_INT)
                Dim pSPOPULATION_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_INT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_INT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_INT)
                Dim pSREFERENCE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_INT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_INT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_INT)
                Dim pNZIPCODE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_INT")
                    .DbType = DbType.String
                    .Value = row("NZIPCODE_INT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_INT)
                'La columna 'NZIPCODE_INT' no pertenece a la tabla .
                Dim pSPHONE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_INT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_INT")
                End With
                CMDvar.Parameters.Add(pSPHONE_INT)
                Dim pNCURRENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCURRENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCURRENCY")
                    .DbType = DbType.String
                    .Value = row("NCURRENCY")
                End With
                CMDvar.Parameters.Add(pNCURRENCY)
                Dim pNINTERTYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNINTERTYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERTYP")
                    .DbType = DbType.String
                    .Value = row("NINTERTYP")
                End With
                CMDvar.Parameters.Add(pNINTERTYP)
                Dim pSOBSERVATION As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSOBSERVATION
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSOBSERVATION")
                    .DbType = DbType.String
                    .Value = row("SOBSERVATION")
                End With
                CMDvar.Parameters.Add(pSOBSERVATION)
                Dim pNLINE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNLINE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNLINE")
                    .DbType = DbType.Int64
                    .Value = row("NLINE")
                End With
                CMDvar.Parameters.Add(pNLINE)



                CMDvar.ExecuteNonQuery()

                CNXvar.Close()
                CNXvar = Nothing
            Next




        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            status = False
        End Try
        Return status
    End Function

    Public Function Ins_Trama(ByVal Tipo As String, ByVal dtsTable As DataTable, ByVal IDArchivo As String) As Boolean
        Dim status As Boolean = False
        Try



            For Each row As DataRow In dtsTable.Rows

                CNXvar = OBJconexion.ConexionDB(Tipo)
                If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
                CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.INT_INS_TRAMA_VALIDA")
                CMDvar.CommandType = CommandType.StoredProcedure
                CMDvar.Connection = CNXvar

                Dim pNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONTROL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCONTROL")
                    .DbType = DbType.Int64
                    .Value = IDArchivo
                End With
                CMDvar.Parameters.Add(pNCONTROL)
                Dim pNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNBRANCH
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNBRANCH")
                    .DbType = DbType.Int64
                    .Value = row("NBRANCH")
                End With
                CMDvar.Parameters.Add(pNBRANCH)
                Dim pNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPRODUCT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPRODUCT")
                    .DbType = DbType.Int64
                    .Value = row("NPRODUCT")
                End With
                CMDvar.Parameters.Add(pNPRODUCT)
                Dim pNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPOLICY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPOLICY")
                    .DbType = DbType.Int64
                    .Value = row("NPOLICY")
                End With
                CMDvar.Parameters.Add(pNPOLICY)
                Dim pNOFFICE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNOFFICE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNOFFICE")
                    .DbType = DbType.Int64
                    .Value = row("NOFFICE")
                End With
                CMDvar.Parameters.Add(pNOFFICE)
                Dim pNSELCHANNEL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNSELCHANNEL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSELLCHANNEL")
                    .DbType = DbType.Int64
                    .Value = row("NSELLCHANNEL")
                End With
                CMDvar.Parameters.Add(pNSELCHANNEL)
                Dim pSBUSSITYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSITYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUSSITYP")
                    .DbType = DbType.String
                    .Value = row("SBUSSITYP")
                End With
                CMDvar.Parameters.Add(pSBUSSITYP)
                Dim pSCOLINVOT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLINVOT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLINVOT")
                    .DbType = DbType.String
                    .Value = row("SCOLINVOT")
                End With
                CMDvar.Parameters.Add(pSCOLINVOT)
                Dim pSCOLREINT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLREINT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLREINT")
                    .DbType = DbType.String
                    .Value = row("SCOLREINT")
                End With
                CMDvar.Parameters.Add(pSCOLREINT)
                Dim pSCOLTIMRE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLTIMRE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLTIMRE")
                    .DbType = DbType.String
                    .Value = row("SCOLTIMRE")
                End With
                CMDvar.Parameters.Add(pSCOLTIMRE)
                Dim pDSTARDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDSTARDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDSTARTDATE")
                    .DbType = DbType.String
                    .Value = row("DSTARTDATE")
                End With
                CMDvar.Parameters.Add(pDSTARDATE)
                Dim pDENDDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDENDDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDEXPIRDAT")
                    .DbType = DbType.String
                    .Value = row("DEXPIRDAT")
                End With
                CMDvar.Parameters.Add(pDENDDATE)
                Dim pNFREQUENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFREQUENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPAYFREQ")
                    .DbType = DbType.Int64
                    .Value = row("NPAYFREQ")
                End With
                CMDvar.Parameters.Add(pNFREQUENCY)
                Dim pSCLIENT_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCLIENT_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCLIENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SCLIENT_CONT")
                End With
                CMDvar.Parameters.Add(pSCLIENT_CONT)
                Dim pNINTERMED As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNINTERMED
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERMED")
                    .DbType = DbType.Int64
                    .Value = row("NINTERMED")
                End With
                CMDvar.Parameters.Add(pNINTERMED)
                Dim pSCODE_INTERMDIATE_SBS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_INTERMDIATE_SBS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSINTER_ID")
                    .DbType = DbType.String
                    .Value = row("SINTER_ID")
                End With
                CMDvar.Parameters.Add(pSCODE_INTERMDIATE_SBS)
                Dim pSUSER_SANITAS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSUSER_SANITAS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSUSR_SANITAS")
                    .DbType = DbType.String
                    .Value = row("SUSR_SANITAS")
                End With
                CMDvar.Parameters.Add(pSUSER_SANITAS)
                Dim pNCONN_PERCEN As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONN_PERCEN
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERCENT")
                    .DbType = DbType.Int64
                    .Value = row("NPERCENT")
                End With
                CMDvar.Parameters.Add(pNCONN_PERCEN)
                Dim pNCOD_CIIU As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOD_CIIU
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSPECIALITY")
                    .DbType = DbType.Int64
                    .Value = row("NSPECIALITY")
                End With
                CMDvar.Parameters.Add(pNCOD_CIIU)
                Dim pSTYPE_PERSON_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_PERSON_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NPERSON_TYP_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_PERSON_C)
                Dim pSTYPE_DOCUMNT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_DOCUMNT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NIDDOC_TYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_DOCUMNT_C)
                Dim pSNATIONALITY_C_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNATIONALITY_C_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NCOUNTRY_CONT")
                End With
                CMDvar.Parameters.Add(pSNATIONALITY_C_C)
                Dim pSMUNICIPALITY_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSMUNICIPALITY_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NMUNICIPALITY_CONT")
                End With
                CMDvar.Parameters.Add(pSMUNICIPALITY_C)
                Dim pSNCODE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNCODE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_CONT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_CONT")
                End With
                CMDvar.Parameters.Add(pSNCODE_C)
                Dim pSBUSSINES_NAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSINES_NAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSBUSSINES_NAME_C)
                Dim pSNAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSNAME_C)
                Dim pSLAST_FAT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_FAT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_FAT_C)
                Dim pSLAST_MOT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_C)
                Dim pDBIRTHDATE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDATE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_CONT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_CONT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDATE_C)
                Dim pSCIVIL_STUS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCIVIL_STUS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NCIVILSTA_CONT")
                End With
                CMDvar.Parameters.Add(pSCIVIL_STUS_C)
                Dim pSSEXCLIEN_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_CONT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_CONT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_CONT)
                Dim pSE_MAIL_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_CONT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_CONT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_CONT)
                Dim pSRECTYPE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_CONT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_CONT)
                Dim pSADDRESS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSADDRESS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_CONT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_CONT")
                End With
                CMDvar.Parameters.Add(pSADDRESS_C)
                Dim pSBUILD_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_CONT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_CONT")
                End With
                CMDvar.Parameters.Add(pSBUILD_CONT)
                Dim pNFLOOR_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NFLOOR_CONT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_CONT)
                Dim pSDEPARTAMENT_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_CONT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_CONT)
                Dim pSPOPULATION_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_CONT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_CONT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_CONT)
                Dim pSREFERENCE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_CONT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_CONT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_CONT)
                Dim pNZIPCODE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NZIPCODE_CONT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_CONT)
                Dim pSPHONE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_CONT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_CONT")
                End With
                CMDvar.Parameters.Add(pSPHONE_CONT)
                Dim pNPERSON_TYP_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPERSON_TYP_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_INT")
                    .DbType = DbType.Int64
                    .Value = row("NPERSON_TYP_INT")
                End With
                CMDvar.Parameters.Add(pNPERSON_TYP_INT)
                Dim pNIDDOC_TYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNIDDOC_TYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_INT")
                    .DbType = DbType.Int64
                    .Value = row("NIDDOC_TYPE_INT")
                End With
                CMDvar.Parameters.Add(pNIDDOC_TYPE_INT)
                Dim pNCOUNTRY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOUNTRY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_INT")
                    .DbType = DbType.Int64
                    .Value = row("NCOUNTRY_INT")
                End With
                CMDvar.Parameters.Add(pNCOUNTRY_INT)
                Dim pNMUNICIPALITY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNMUNICIPALITY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_INT")
                    .DbType = DbType.Int64
                    .Value = row("NMUNICIPALITY_INT")
                End With
                CMDvar.Parameters.Add(pNMUNICIPALITY_INT)

                Dim pSIDDOC_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSIDDOC_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_INT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_INT")
                End With
                CMDvar.Parameters.Add(pSIDDOC_INT)
                Dim pSLEGALNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLEGALNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_INT")
                End With
                CMDvar.Parameters.Add(pSLEGALNAME_INT)
                Dim pSFIRSTNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSFIRSTNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_INT")
                End With
                CMDvar.Parameters.Add(pSFIRSTNAME_INT)
                Dim pSLASTNAME1_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME1_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME1_INT)
                Dim pSLASTNAME2_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME2_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME2_INT)
                Dim pDBIRTHDAT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDAT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_INT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_INT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDAT_INT)
                Dim pNCIVILSTA_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCIVILSTA_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_INT")
                    .DbType = DbType.Int64
                    .Value = row("NCIVILSTA_INT")
                End With
                CMDvar.Parameters.Add(pNCIVILSTA_INT)
                Dim pSSEXCLIEN_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_INT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_INT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_INT)
                Dim pSLAST_MOT_I As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_I
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNVALSUSALUD")
                    .DbType = DbType.Int64
                    .Value = row("NVALSUSALUD")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_I)
                Dim pSE_MAIL_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_INT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_INT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_INT)
                Dim pSRECTYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_INT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_INT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_INT)
                Dim pSSTREET_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSTREET_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_INT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_INT")
                End With
                CMDvar.Parameters.Add(pSSTREET_INT)
                Dim pSBUILD_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_INT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_INT")
                End With
                CMDvar.Parameters.Add(pSBUILD_INT)
                Dim pNFLOOR_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_INT")
                    .DbType = DbType.Int64
                    .Value = row("NFLOOR_INT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_INT)
                Dim pSDEPARTAMENT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_INT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_INT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_INT)
                Dim pSPOPULATION_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_INT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_INT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_INT)
                Dim pSREFERENCE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_INT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_INT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_INT)
                Dim pNZIPCODE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_INT")
                    .DbType = DbType.Int64
                    .Value = row("NZIPCODE_INT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_INT)
                Dim pSPHONE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_INT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_INT")
                End With
                CMDvar.Parameters.Add(pSPHONE_INT)
                Dim pNCURRENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCURRENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCURRENCY")
                    .DbType = DbType.Int64
                    .Value = row("NCURRENCY")
                End With
                CMDvar.Parameters.Add(pNCURRENCY)
                Dim pNINTERTYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNINTERTYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERTYP")
                    .DbType = DbType.UInt64
                    .Value = row("NINTERTYP")
                End With
                CMDvar.Parameters.Add(pNINTERTYP)
                Dim pSOBSERVATION As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSOBSERVATION
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSOBSERVATION")
                    .DbType = DbType.String
                    .Value = row("SOBSERVATION")
                End With
                CMDvar.Parameters.Add(pSOBSERVATION)
                Dim pNLINE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNLINE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNLINE")
                    .DbType = DbType.Int64
                    .Value = row("NLINE")
                End With
                CMDvar.Parameters.Add(pNLINE)

                CMDvar.ExecuteNonQuery()

                CNXvar.Close()
                CNXvar = Nothing
            Next





        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            status = False
        End Try
        Return status
    End Function

    Public Function Ins_Procesar_core(ByVal Tipo As String, ByVal id As Integer) As Integer
        Dim respuesta As Integer
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_SCTR_MASIVO.INT_INST_CORE_SCTR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNCONTROL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@rNCONTROL")
                .DbType = DbType.String
                .Value = id
            End With
            CMDvar.Parameters.Add(ParNCONTROL)

            Dim ParnNumFile As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnNumFile
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nNumFile")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnNumFile)


            CMDvar.ExecuteNonQuery()
            respuesta = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nNumFile")).Value

            CNXvar.Close()
            CNXvar = Nothing


        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            respuesta = 0
        End Try
        Return respuesta
    End Function

#End Region


#Region "EMISI�N POLIZAS MATRICES GRUPALES"


    Public Function ListarModulosxProducto(ByVal Tipo As Integer, ByVal vFecha As String, ByVal vProducto As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.LISTAR_MODUL")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParvFecha As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParvFecha
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@DFECHA")
                .DbType = DbType.String
                .Value = vFecha
            End With
            CMDvar.Parameters.Add(ParvFecha)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPRODUCT")
                .DbType = DbType.Int64
                .Value = vProducto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Modulo")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try
        Return dts
    End Function

    Public Function ListarFormatoxProducto(ByVal Tipo As Integer, ByVal vProducto As Integer) As DataSet

        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.LISTAR_FORMATO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPRODUCT")
                .DbType = DbType.Int64
                .Value = vProducto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try
        Return dts
    End Function

    Public Function ListarEmision_Polizas(ByVal Tipo As Integer, ByVal ID As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.REAMASSIVE_REP")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NCONTROL")
                .DbType = DbType.Int64
                .Value = ID
            End With
            CMDvar.Parameters.Add(ParpID)


            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "RC1")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            CNXvar = Nothing
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try
        Return dts
    End Function

    Public Function ListarError_Polizas(ByVal Tipo As Integer, ByVal ID As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.LISTAR_MASIVO_ERRORES")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nID_")
                .DbType = DbType.Int64
                .Value = ID
            End With
            CMDvar.Parameters.Add(ParpID)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "ListError")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "ErrorTrama")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Error_INS")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            Dim vardata3 As DbParameter = OBJconexion.CrearParametro(Tipo, "EMISION")
            With vardata3
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata3)



            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            CNXvar = Nothing
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
        End Try

    End Function

    Public Function ListarFormato_Columna(ByVal Tipo As Integer, ByVal vFormato As String) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.LISTAR_FORMATO_COLUMNA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParCANAL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCANAL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@VFORMATO")
                .DbType = DbType.String
                .Size = 20
                .Value = vFormato
            End With
            CMDvar.Parameters.Add(ParCANAL)



            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Formato")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Columna")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)



            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
            'Throw ex
        End Try
    End Function

    Public Function ExistePolizaMatriz(ByVal Tipo As String, ByVal nPolicy As String, ByVal vProducto As Integer) As Integer

        Dim exists As Integer = 0

        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.INT_EXIST_POLIZA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParpPolicy As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpPolicy
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pPolicy")
                .DbType = DbType.Int64
                .Value = Convert.ToInt64(nPolicy)
            End With
            CMDvar.Parameters.Add(ParpPolicy)


            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPRODUCT")
                .DbType = DbType.Int64
                .Value = vProducto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParnCount As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnCount
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nCount")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnCount)

            CMDvar.ExecuteNonQuery()
            exists = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nCount")).Value

            CNXvar.Close()
            CNXvar = Nothing

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            exists = 0
        End Try
        Return exists

    End Function

    Public Function CloseLoadFile(ByVal Tipo As String, ByVal Obeservacion As String, ByVal id As Integer) As Boolean
        Dim status As Boolean = True

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.INT_UPD_ARCHIVO_POLIZA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParpID As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpID
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pID")
                .DbType = DbType.Int64
                .Value = id
            End With
            CMDvar.Parameters.Add(ParpID)

            Dim ParpComentary As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpComentary
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pComentary")
                .DbType = DbType.String
                .Size = 255
                .Value = Obeservacion
            End With
            CMDvar.Parameters.Add(ParpComentary)

            CMDvar.ExecuteNonQuery()

            CNXvar.Close()
            CNXvar = Nothing
            status = True
        Catch ex As Exception
            CNXvar = Nothing
            status = False
        End Try

        Return status

    End Function

    Public Function OpenLoadFile(ByVal Tipo As String, ByVal canal As String, ByVal vProducto As Integer, ByVal RutaOrigen As String, ByVal cabecera As Integer, ByVal DescArchivo As String, ByVal glosa As String, ByVal CodUser As Integer) As Integer

        Dim idArchivo As Integer = 0

        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.INT_INS_ARCHIVO_POLIZA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParpCanal As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpCanal
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pChannel")
                .DbType = DbType.String
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParpCanal)

            Dim ParProducto As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParProducto
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PNPRODUCT")
                .DbType = DbType.Int64
                .Value = vProducto
            End With
            CMDvar.Parameters.Add(ParProducto)

            Dim ParpSourFile As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpSourFile
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSourFile")
                .DbType = DbType.String
                .Size = 255
                .Value = RutaOrigen
            End With
            CMDvar.Parameters.Add(ParpSourFile)


            Dim ParpFile_ As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpFile_
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pFile_")
                .DbType = DbType.String
                .Size = 255
                .Value = DescArchivo
            End With
            CMDvar.Parameters.Add(ParpFile_)

            Dim ParpHeader As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpHeader
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pHeader")
                .DbType = DbType.Int64
                .Value = cabecera
            End With
            CMDvar.Parameters.Add(ParpHeader)

            Dim ParpComentary As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpComentary
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pComentary")
                .DbType = DbType.String
                .Size = 255
                .Value = glosa
            End With
            CMDvar.Parameters.Add(ParpComentary)

            Dim ParpUserCode As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParpUserCode
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@pUserCode")
                .DbType = DbType.Int64
                .Value = CodUser
            End With
            CMDvar.Parameters.Add(ParpUserCode)

            Dim ParnNumFile As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnNumFile
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nNumFile")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnNumFile)


            CMDvar.ExecuteNonQuery()
            idArchivo = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nNumFile")).Value

            CNXvar.Close()
            CNXvar = Nothing


        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            idArchivo = 0
        End Try

        Return idArchivo

    End Function

    Public Function ValidacionTramaPolizas(ByVal Tipo As String, ByVal IDArchivo As String) As Integer

        Dim exists As Integer = 0
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
            CMDvar = OBJconexion.CrearCommando(Tipo, "VALMASSIVE_LOAD_POL")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim pNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With pNCONTROL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NCONTROL")
                .DbType = DbType.Int64
                .Value = Convert.ToInt64(IDArchivo)
            End With
            CMDvar.Parameters.Add(pNCONTROL)
            Dim ParnCount As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnCount
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@NREGVAL")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnCount)

            CMDvar.ExecuteNonQuery()
            exists = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@NREGVAL")).Value

            CMDvar.ExecuteNonQuery()

            CNXvar.Close()
            CNXvar = Nothing
        Catch ex As Exception
            exists = 0
            CNXvar.Close()
            CNXvar = Nothing
        End Try


        Return exists
    End Function

    Public Function InsertarErrores_Trama(ByVal Tipo As String, ByVal dtsTable As DataTable, ByVal IDArchivo As String) As Boolean
        Dim status As Boolean = False
        Try
            For Each row As DataRow In dtsTable.Rows

                CNXvar = OBJconexion.ConexionDB(Tipo)
                If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
                CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.INT_INS_TRAMA_ERROR")
                CMDvar.CommandType = CommandType.StoredProcedure
                CMDvar.Connection = CNXvar

                Dim pNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONTROL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCONTROL")
                    .DbType = DbType.Int64
                    .Value = IDArchivo
                End With
                CMDvar.Parameters.Add(pNCONTROL)
                Dim pNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNBRANCH
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNBRANCH")
                    .DbType = DbType.String
                    .Value = row("NBRANCH")
                End With
                CMDvar.Parameters.Add(pNBRANCH)
                Dim pNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPRODUCT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPRODUCT")
                    .DbType = DbType.String
                    .Value = row("NPRODUCT")
                End With
                CMDvar.Parameters.Add(pNPRODUCT)
                Dim pNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPOLICY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPOLICY")
                    .DbType = DbType.String
                    .Value = row("NPOLICY")
                End With
                CMDvar.Parameters.Add(pNPOLICY)
                Dim pNOFFICE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNOFFICE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNOFFICE")
                    .DbType = DbType.String
                    .Value = row("NOFFICE")
                End With
                CMDvar.Parameters.Add(pNOFFICE)
                Dim pNSELCHANNEL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNSELCHANNEL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSELLCHANNEL")
                    .DbType = DbType.String
                    .Value = row("NSELLCHANNEL")
                End With
                CMDvar.Parameters.Add(pNSELCHANNEL)
                Dim pSBUSSITYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSITYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUSSITYP")
                    .DbType = DbType.String
                    .Value = row("SBUSSITYP")
                End With
                CMDvar.Parameters.Add(pSBUSSITYP)
                Dim pSCOLINVOT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLINVOT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLINVOT")
                    .DbType = DbType.String
                    .Value = row("SCOLINVOT")
                End With
                CMDvar.Parameters.Add(pSCOLINVOT)
                Dim pSCOLREINT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLREINT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLREINT")
                    .DbType = DbType.String
                    .Value = row("SCOLREINT")
                End With
                CMDvar.Parameters.Add(pSCOLREINT)
                Dim pSCOLTIMRE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLTIMRE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLTIMRE")
                    .DbType = DbType.String
                    .Value = row("SCOLTIMRE")
                End With
                CMDvar.Parameters.Add(pSCOLTIMRE)
                Dim pDSTARDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDSTARDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDSTARTDATE")
                    .DbType = DbType.String
                    .Value = row("DSTARTDATE")
                End With
                CMDvar.Parameters.Add(pDSTARDATE)
                Dim pDENDDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDENDDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDEXPIRDAT")
                    .DbType = DbType.String
                    .Value = row("DEXPIRDAT")
                End With
                CMDvar.Parameters.Add(pDENDDATE)
                Dim pNFREQUENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFREQUENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPAYFREQ")
                    .DbType = DbType.String
                    .Value = row("NPAYFREQ")
                End With
                CMDvar.Parameters.Add(pNFREQUENCY)
                Dim pSCODE_CONTRCTOR As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_CONTRCTOR
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCLIENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SCLIENT_CONT")
                End With
                CMDvar.Parameters.Add(pSCODE_CONTRCTOR)
                Dim pSCODE_INTERMDIATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_INTERMDIATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERMED")
                    .DbType = DbType.String
                    .Value = row("NINTERMED")
                End With
                CMDvar.Parameters.Add(pSCODE_INTERMDIATE)
                Dim pSCODE_INTERMDIATE_SBS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_INTERMDIATE_SBS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSINTER_ID")
                    .DbType = DbType.String
                    .Value = row("SINTER_ID")
                End With
                CMDvar.Parameters.Add(pSCODE_INTERMDIATE_SBS)
                Dim pSUSER_SANITAS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSUSER_SANITAS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSUSR_SANITAS")
                    .DbType = DbType.String
                    .Value = row("SUSR_SANITAS")
                End With
                CMDvar.Parameters.Add(pSUSER_SANITAS)
                Dim pNCONN_PERCEN As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONN_PERCEN
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERCENT")
                    .DbType = DbType.String
                    .Value = row("NPERCENT")
                End With
                CMDvar.Parameters.Add(pNCONN_PERCEN)
                Dim pNCOD_CIIU As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOD_CIIU
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSPECIALITY")
                    .DbType = DbType.String
                    .Value = row("NSPECIALITY")
                End With
                CMDvar.Parameters.Add(pNCOD_CIIU)
                Dim pSTYPE_PERSON_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_PERSON_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_CONT")
                    .DbType = DbType.String
                    .Value = row("NPERSON_TYP_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_PERSON_C)
                Dim pSTYPE_DOCUMNT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_DOCUMNT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_CONT")
                    .DbType = DbType.String
                    .Value = row("NIDDOC_TYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_DOCUMNT_C)
                Dim pSNATIONALITY_C_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNATIONALITY_C_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_CONT")
                    .DbType = DbType.String
                    .Value = row("NCOUNTRY_CONT")
                End With
                CMDvar.Parameters.Add(pSNATIONALITY_C_C)
                Dim pSMUNICIPALITY_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSMUNICIPALITY_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_CONT")
                    .DbType = DbType.String
                    .Value = row("NMUNICIPALITY_CONT")
                End With
                CMDvar.Parameters.Add(pSMUNICIPALITY_C)
                Dim pSNCODE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNCODE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_CONT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_CONT")
                End With
                CMDvar.Parameters.Add(pSNCODE_C)
                Dim pSBUSSINES_NAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSINES_NAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSBUSSINES_NAME_C)
                Dim pSNAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSNAME_C)
                Dim pSLAST_FAT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_FAT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_FAT_C)
                Dim pSLAST_MOT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_C)
                Dim pDBIRTHDATE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDATE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_CONT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_CONT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDATE_C)
                Dim pSCIVIL_STUS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCIVIL_STUS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_CONT")
                    .DbType = DbType.String
                    .Value = row("NCIVILSTA_CONT")
                End With
                CMDvar.Parameters.Add(pSCIVIL_STUS_C)
                Dim pSSEXCLIEN_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_CONT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_CONT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_CONT)
                Dim pSE_MAIL_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_CONT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_CONT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_CONT)
                Dim pSRECTYPE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_CONT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_CONT)
                Dim pSADDRESS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSADDRESS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_CONT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_CONT")
                End With
                CMDvar.Parameters.Add(pSADDRESS_C)
                Dim pSBUILD_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_CONT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_CONT")
                End With
                CMDvar.Parameters.Add(pSBUILD_CONT)
                Dim pNFLOOR_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_CONT")
                    .DbType = DbType.String
                    .Value = row("NFLOOR_CONT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_CONT)
                Dim pSDEPARTAMENT_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_CONT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_CONT)
                Dim pSPOPULATION_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_CONT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_CONT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_CONT)
                Dim pSREFERENCE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_CONT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_CONT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_CONT)
                Dim pNZIPCODE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_CONT")
                    .DbType = DbType.String
                    .Value = row("NZIPCODE_CONT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_CONT)
                Dim pSPHONE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_CONT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_CONT")
                End With
                CMDvar.Parameters.Add(pSPHONE_CONT)
                Dim pNPERSON_TYP_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPERSON_TYP_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_INT")
                    .DbType = DbType.String
                    .Value = row("NPERSON_TYP_INT")
                End With
                CMDvar.Parameters.Add(pNPERSON_TYP_INT)
                Dim pNIDDOC_TYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNIDDOC_TYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_INT")
                    .DbType = DbType.String
                    .Value = row("NIDDOC_TYPE_INT")
                End With
                CMDvar.Parameters.Add(pNIDDOC_TYPE_INT)
                Dim pNCOUNTRY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOUNTRY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_INT")
                    .DbType = DbType.String
                    .Value = row("NCOUNTRY_INT")
                End With
                CMDvar.Parameters.Add(pNCOUNTRY_INT)
                Dim pNMUNICIPALITY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNMUNICIPALITY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_INT")
                    .DbType = DbType.String
                    .Value = row("NMUNICIPALITY_INT")
                End With
                CMDvar.Parameters.Add(pNMUNICIPALITY_INT)

                Dim pSIDDOC_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSIDDOC_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_INT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_INT")
                End With
                CMDvar.Parameters.Add(pSIDDOC_INT)
                Dim pSLEGALNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLEGALNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_INT")
                End With
                CMDvar.Parameters.Add(pSLEGALNAME_INT)
                Dim pSFIRSTNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSFIRSTNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_INT")
                End With
                CMDvar.Parameters.Add(pSFIRSTNAME_INT)
                Dim pSLASTNAME1_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME1_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME1_INT)


                Dim pSLASTNAME2_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME2_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME2_INT)
                Dim pDBIRTHDAT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDAT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_INT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_INT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDAT_INT)
                Dim pNCIVILSTA_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCIVILSTA_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_INT")
                    .DbType = DbType.String
                    .Value = row("NCIVILSTA_INT")
                End With
                CMDvar.Parameters.Add(pNCIVILSTA_INT)
                Dim pSSEXCLIEN_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_INT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_INT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_INT)
                Dim pSLAST_MOT_I As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_I
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNVALSUSALUD")
                    .DbType = DbType.String
                    .Value = row("NVALSUSALUD")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_I)
                Dim pSE_MAIL_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_INT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_INT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_INT)
                Dim pSRECTYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_INT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_INT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_INT)
                Dim pSSTREET_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSTREET_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_INT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_INT")
                End With
                CMDvar.Parameters.Add(pSSTREET_INT)
                Dim pSBUILD_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_INT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_INT")
                End With
                CMDvar.Parameters.Add(pSBUILD_INT)
                Dim pNFLOOR_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_INT")
                    .DbType = DbType.String
                    .Value = row("NFLOOR_INT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_INT)
                Dim pSDEPARTAMENT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_INT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_INT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_INT)
                Dim pSPOPULATION_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_INT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_INT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_INT)
                Dim pSREFERENCE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_INT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_INT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_INT)
                Dim pNZIPCODE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_INT")
                    .DbType = DbType.String
                    .Value = row("NZIPCODE_INT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_INT)
                'La columna 'NZIPCODE_INT' no pertenece a la tabla .
                Dim pSPHONE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_INT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_INT")
                End With
                CMDvar.Parameters.Add(pSPHONE_INT)
                Dim pNCURRENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCURRENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCURRENCY")
                    .DbType = DbType.String
                    .Value = row("NCURRENCY")
                End With
                CMDvar.Parameters.Add(pNCURRENCY)
                Dim pNINTERTYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNINTERTYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERTYP")
                    .DbType = DbType.String
                    .Value = row("NINTERTYP")
                End With
                CMDvar.Parameters.Add(pNINTERTYP)
                Dim pSOBSERVATION As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSOBSERVATION
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSOBSERVATION")
                    .DbType = DbType.String
                    .Value = row("SOBSERVATION")
                End With
                CMDvar.Parameters.Add(pSOBSERVATION)
                Dim pNLINE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNLINE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNLINE")
                    .DbType = DbType.Int64
                    .Value = row("NLINE")
                End With
                CMDvar.Parameters.Add(pNLINE)



                CMDvar.ExecuteNonQuery()

                CNXvar.Close()
                CNXvar = Nothing
            Next




        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            status = False
        End Try
        Return status
    End Function

    Public Function InsertarTrama_Emision(ByVal Tipo As String, ByVal dtsTable As DataTable, ByVal IDArchivo As String) As Boolean
        Dim status As Boolean = False
        Try
            For Each row As DataRow In dtsTable.Rows

                CNXvar = OBJconexion.ConexionDB(Tipo)
                If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()
                CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.INT_INS_TRAMA_VALIDA")
                CMDvar.CommandType = CommandType.StoredProcedure
                CMDvar.Connection = CNXvar

                Dim pNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONTROL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCONTROL")
                    .DbType = DbType.Int64
                    .Value = IDArchivo
                End With
                CMDvar.Parameters.Add(pNCONTROL)
                Dim pNBRANCH As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNBRANCH
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNBRANCH")
                    .DbType = DbType.Int64
                    .Value = row("NBRANCH")
                End With
                CMDvar.Parameters.Add(pNBRANCH)
                Dim pNPRODUCT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPRODUCT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPRODUCT")
                    .DbType = DbType.Int64
                    .Value = row("NPRODUCT")
                End With
                CMDvar.Parameters.Add(pNPRODUCT)
                Dim pNPOLICY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPOLICY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPOLICY")
                    .DbType = DbType.Int64
                    .Value = row("NPOLICY")
                End With
                CMDvar.Parameters.Add(pNPOLICY)
                Dim pNOFFICE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNOFFICE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNOFFICE")
                    .DbType = DbType.Int64
                    .Value = row("NOFFICE")
                End With
                CMDvar.Parameters.Add(pNOFFICE)
                Dim pNSELCHANNEL As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNSELCHANNEL
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSELLCHANNEL")
                    .DbType = DbType.Int64
                    .Value = row("NSELLCHANNEL")
                End With
                CMDvar.Parameters.Add(pNSELCHANNEL)
                Dim pSBUSSITYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSITYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUSSITYP")
                    .DbType = DbType.String
                    .Value = row("SBUSSITYP")
                End With
                CMDvar.Parameters.Add(pSBUSSITYP)
                Dim pSCOLINVOT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLINVOT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLINVOT")
                    .DbType = DbType.String
                    .Value = row("SCOLINVOT")
                End With
                CMDvar.Parameters.Add(pSCOLINVOT)
                Dim pSCOLREINT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLREINT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLREINT")
                    .DbType = DbType.String
                    .Value = row("SCOLREINT")
                End With
                CMDvar.Parameters.Add(pSCOLREINT)
                Dim pSCOLTIMRE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCOLTIMRE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCOLTIMRE")
                    .DbType = DbType.String
                    .Value = row("SCOLTIMRE")
                End With
                CMDvar.Parameters.Add(pSCOLTIMRE)
                Dim pDSTARDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDSTARDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDSTARTDATE")
                    .DbType = DbType.String
                    .Value = row("DSTARTDATE")
                End With
                CMDvar.Parameters.Add(pDSTARDATE)
                Dim pDENDDATE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDENDDATE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDEXPIRDAT")
                    .DbType = DbType.String
                    .Value = row("DEXPIRDAT")
                End With
                CMDvar.Parameters.Add(pDENDDATE)
                Dim pNFREQUENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFREQUENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPAYFREQ")
                    .DbType = DbType.Int64
                    .Value = row("NPAYFREQ")
                End With
                CMDvar.Parameters.Add(pNFREQUENCY)
                Dim pSCLIENT_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCLIENT_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSCLIENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SCLIENT_CONT")
                End With
                CMDvar.Parameters.Add(pSCLIENT_CONT)
                Dim pNINTERMED As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNINTERMED
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERMED")
                    .DbType = DbType.Int64
                    .Value = row("NINTERMED")
                End With
                CMDvar.Parameters.Add(pNINTERMED)
                Dim pSCODE_INTERMDIATE_SBS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCODE_INTERMDIATE_SBS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSINTER_ID")
                    .DbType = DbType.String
                    .Value = row("SINTER_ID")
                End With
                CMDvar.Parameters.Add(pSCODE_INTERMDIATE_SBS)
                Dim pSUSER_SANITAS As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSUSER_SANITAS
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSUSR_SANITAS")
                    .DbType = DbType.String
                    .Value = row("SUSR_SANITAS")
                End With
                CMDvar.Parameters.Add(pSUSER_SANITAS)
                Dim pNCONN_PERCEN As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCONN_PERCEN
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERCENT")
                    .DbType = DbType.Int64
                    .Value = row("NPERCENT")
                End With
                CMDvar.Parameters.Add(pNCONN_PERCEN)
                Dim pNCOD_CIIU As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOD_CIIU
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNSPECIALITY")
                    .DbType = DbType.Int64
                    .Value = row("NSPECIALITY")
                End With
                CMDvar.Parameters.Add(pNCOD_CIIU)
                Dim pSTYPE_PERSON_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_PERSON_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NPERSON_TYP_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_PERSON_C)
                Dim pSTYPE_DOCUMNT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSTYPE_DOCUMNT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NIDDOC_TYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSTYPE_DOCUMNT_C)
                Dim pSNATIONALITY_C_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNATIONALITY_C_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NCOUNTRY_CONT")
                End With
                CMDvar.Parameters.Add(pSNATIONALITY_C_C)
                Dim pSMUNICIPALITY_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSMUNICIPALITY_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NMUNICIPALITY_CONT")
                End With
                CMDvar.Parameters.Add(pSMUNICIPALITY_C)
                Dim pSNCODE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNCODE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_CONT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_CONT")
                End With
                CMDvar.Parameters.Add(pSNCODE_C)
                Dim pSBUSSINES_NAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUSSINES_NAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSBUSSINES_NAME_C)
                Dim pSNAME_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSNAME_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_CONT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_CONT")
                End With
                CMDvar.Parameters.Add(pSNAME_C)
                Dim pSLAST_FAT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_FAT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_FAT_C)
                Dim pSLAST_MOT_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_CONT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_CONT")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_C)
                Dim pDBIRTHDATE_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDATE_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_CONT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_CONT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDATE_C)
                Dim pSCIVIL_STUS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSCIVIL_STUS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NCIVILSTA_CONT")
                End With
                CMDvar.Parameters.Add(pSCIVIL_STUS_C)
                Dim pSSEXCLIEN_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_CONT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_CONT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_CONT)
                Dim pSE_MAIL_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_CONT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_CONT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_CONT)
                Dim pSRECTYPE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_CONT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_CONT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_CONT)
                Dim pSADDRESS_C As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSADDRESS_C
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_CONT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_CONT")
                End With
                CMDvar.Parameters.Add(pSADDRESS_C)
                Dim pSBUILD_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_CONT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_CONT")
                End With
                CMDvar.Parameters.Add(pSBUILD_CONT)
                Dim pNFLOOR_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NFLOOR_CONT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_CONT)
                Dim pSDEPARTAMENT_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_CONT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_CONT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_CONT)
                Dim pSPOPULATION_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_CONT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_CONT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_CONT)
                Dim pSREFERENCE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_CONT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_CONT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_CONT)
                Dim pNZIPCODE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_CONT")
                    .DbType = DbType.Int64
                    .Value = row("NZIPCODE_CONT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_CONT)
                Dim pSPHONE_CONT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_CONT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_CONT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_CONT")
                End With
                CMDvar.Parameters.Add(pSPHONE_CONT)
                Dim pNPERSON_TYP_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNPERSON_TYP_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNPERSON_TYP_INT")
                    .DbType = DbType.Int64
                    .Value = row("NPERSON_TYP_INT")
                End With
                CMDvar.Parameters.Add(pNPERSON_TYP_INT)
                Dim pNIDDOC_TYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNIDDOC_TYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNIDDOC_TYPE_INT")
                    .DbType = DbType.Int64
                    .Value = row("NIDDOC_TYPE_INT")
                End With
                CMDvar.Parameters.Add(pNIDDOC_TYPE_INT)
                Dim pNCOUNTRY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCOUNTRY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCOUNTRY_INT")
                    .DbType = DbType.Int64
                    .Value = row("NCOUNTRY_INT")
                End With
                CMDvar.Parameters.Add(pNCOUNTRY_INT)
                Dim pNMUNICIPALITY_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNMUNICIPALITY_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNMUNICIPALITY_INT")
                    .DbType = DbType.Int64
                    .Value = row("NMUNICIPALITY_INT")
                End With
                CMDvar.Parameters.Add(pNMUNICIPALITY_INT)

                Dim pSIDDOC_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSIDDOC_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSIDDOC_INT")
                    .DbType = DbType.String
                    .Value = row("SIDDOC_INT")
                End With
                CMDvar.Parameters.Add(pSIDDOC_INT)
                Dim pSLEGALNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLEGALNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLEGALNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SLEGALNAME_INT")
                End With
                CMDvar.Parameters.Add(pSLEGALNAME_INT)
                Dim pSFIRSTNAME_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSFIRSTNAME_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSFIRSTNAME_INT")
                    .DbType = DbType.String
                    .Value = row("SFIRSTNAME_INT")
                End With
                CMDvar.Parameters.Add(pSFIRSTNAME_INT)
                Dim pSLASTNAME1_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME1_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME1_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME1_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME1_INT)
                Dim pSLASTNAME2_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLASTNAME2_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSLASTNAME2_INT")
                    .DbType = DbType.String
                    .Value = row("SLASTNAME2_INT")
                End With
                CMDvar.Parameters.Add(pSLASTNAME2_INT)
                Dim pDBIRTHDAT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pDBIRTHDAT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pDBIRTHDAT_INT")
                    .DbType = DbType.String
                    .Value = row("DBIRTHDAT_INT")
                End With
                CMDvar.Parameters.Add(pDBIRTHDAT_INT)
                Dim pNCIVILSTA_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCIVILSTA_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCIVILSTA_INT")
                    .DbType = DbType.Int64
                    .Value = row("NCIVILSTA_INT")
                End With
                CMDvar.Parameters.Add(pNCIVILSTA_INT)
                Dim pSSEXCLIEN_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSEXCLIEN_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSEXCLIEN_INT")
                    .DbType = DbType.String
                    .Value = row("SSEXCLIEN_INT")
                End With
                CMDvar.Parameters.Add(pSSEXCLIEN_INT)
                Dim pSLAST_MOT_I As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSLAST_MOT_I
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNVALSUSALUD")
                    .DbType = DbType.Int64
                    .Value = row("NVALSUSALUD")
                End With
                CMDvar.Parameters.Add(pSLAST_MOT_I)
                Dim pSE_MAIL_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSE_MAIL_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSE_MAIL_INT")
                    .DbType = DbType.String
                    .Value = row("SE_MAIL_INT")
                End With
                CMDvar.Parameters.Add(pSE_MAIL_INT)
                Dim pSRECTYPE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSRECTYPE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSRECTYPE_INT")
                    .DbType = DbType.String
                    .Value = row("SRECTYPE_INT")
                End With
                CMDvar.Parameters.Add(pSRECTYPE_INT)
                Dim pSSTREET_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSSTREET_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSSTREET_INT")
                    .DbType = DbType.String
                    .Value = row("SSTREET_INT")
                End With
                CMDvar.Parameters.Add(pSSTREET_INT)
                Dim pSBUILD_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSBUILD_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSBUILD_INT")
                    .DbType = DbType.String
                    .Value = row("SBUILD_INT")
                End With
                CMDvar.Parameters.Add(pSBUILD_INT)
                Dim pNFLOOR_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNFLOOR_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNFLOOR_INT")
                    .DbType = DbType.Int64
                    .Value = row("NFLOOR_INT")
                End With
                CMDvar.Parameters.Add(pNFLOOR_INT)
                Dim pSDEPARTAMENT_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSDEPARTAMENT_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSDEPARTAMENT_INT")
                    .DbType = DbType.String
                    .Value = row("SDEPARTAMENT_INT")
                End With
                CMDvar.Parameters.Add(pSDEPARTAMENT_INT)
                Dim pSPOPULATION_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPOPULATION_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPOPULATION_INT")
                    .DbType = DbType.String
                    .Value = row("SPOPULATION_INT")
                End With
                CMDvar.Parameters.Add(pSPOPULATION_INT)
                Dim pSREFERENCE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSREFERENCE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSREFERENCE_INT")
                    .DbType = DbType.String
                    .Value = row("SREFERENCE_INT")
                End With
                CMDvar.Parameters.Add(pSREFERENCE_INT)
                Dim pNZIPCODE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNZIPCODE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNZIPCODE_INT")
                    .DbType = DbType.Int64
                    .Value = row("NZIPCODE_INT")
                End With
                CMDvar.Parameters.Add(pNZIPCODE_INT)
                Dim pSPHONE_INT As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSPHONE_INT
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSPHONE_INT")
                    .DbType = DbType.String
                    .Value = row("SPHONE_INT")
                End With
                CMDvar.Parameters.Add(pSPHONE_INT)
                Dim pNCURRENCY As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNCURRENCY
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNCURRENCY")
                    .DbType = DbType.Int64
                    .Value = row("NCURRENCY")
                End With
                CMDvar.Parameters.Add(pNCURRENCY)
                Dim pNINTERTYP As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNINTERTYP
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNINTERTYP")
                    .DbType = DbType.UInt64
                    .Value = row("NINTERTYP")
                End With
                CMDvar.Parameters.Add(pNINTERTYP)
                Dim pSOBSERVATION As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pSOBSERVATION
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pSOBSERVATION")
                    .DbType = DbType.String
                    .Value = row("SOBSERVATION")
                End With
                CMDvar.Parameters.Add(pSOBSERVATION)
                Dim pNLINE As DbParameter = OBJconexion.CrearParametro(Tipo)
                With pNLINE
                    .ParameterName = OBJconexion.Dev_Par(Tipo, "@pNLINE")
                    .DbType = DbType.Int64
                    .Value = row("NLINE")
                End With
                CMDvar.Parameters.Add(pNLINE)

                CMDvar.ExecuteNonQuery()

                CNXvar.Close()
                CNXvar = Nothing
            Next

        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            status = False
        End Try
        Return status
    End Function

    Public Function Procesar_TramaCorrecta(ByVal Tipo As String, ByVal id As Integer, ByVal Modulos As String) As Integer
        Dim respuesta As Integer
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PROC_EMISION_POLIZAS.INT_INST_CORE")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParNCONTROL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNCONTROL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@rNCONTROL")
                .DbType = DbType.String
                .Value = id
            End With
            CMDvar.Parameters.Add(ParNCONTROL)

            Dim ParNMODULES As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParNMODULES
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@LNMODULEC")
                .DbType = DbType.String
                .Value = Modulos
            End With
            CMDvar.Parameters.Add(ParNMODULES)

            Dim ParnNumFile As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParnNumFile
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nNumFile")
                .DbType = DbType.String
                .Size = 20
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParnNumFile)


            CMDvar.ExecuteNonQuery()
            respuesta = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nNumFile")).Value

            CNXvar.Close()
            CNXvar = Nothing


        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            respuesta = 0
        End Try
        Return respuesta
    End Function

#End Region


#Region " Funciones para  env�o a SUSALUD"

    Public Function Lista_Datos_Combos_SUSALUD(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.LISTARCOMBOS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "Canal")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            Dim vardata1 As DbParameter = OBJconexion.CrearParametro(Tipo, "Producto")
            With vardata1
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata1)

            Dim vardata2 As DbParameter = OBJconexion.CrearParametro(Tipo, "Poliza")
            With vardata2
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata2)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function


    Public Function Lista_asegurado_trama_SUSALUD(ByVal Tipo As Integer, ByVal canal As String, ByVal producto As Integer, ByVal poliza As Integer, ByVal vtipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.LISTAR_ASEGURADO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim ParCANAL As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCANAL
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@CANAL")
                .DbType = DbType.String
                .Value = canal
            End With
            CMDvar.Parameters.Add(ParCANAL)

            Dim ParPRODUCTO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPRODUCTO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@PRODUCTO")
                .DbType = DbType.Int64
                .Value = producto
            End With
            CMDvar.Parameters.Add(ParPRODUCTO)

            Dim ParPOLIZA As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParPOLIZA
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@POLIZA")
                .DbType = DbType.Int64
                .Value = poliza
            End With
            CMDvar.Parameters.Add(ParPOLIZA)

            Dim ParTIPO As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParTIPO
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@TIPOASE")
                .DbType = DbType.Int64
                .Value = vtipo
            End With
            CMDvar.Parameters.Add(ParTIPO)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TIPO_CARGA")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function



    Public Function generarexcel(ByVal fechaInicio As String, ByVal fechafin As String) As Boolean
        Try
            Dim dt As New DataTable
            dt = ConvertToDataTable(GetEntExportar(Metodos.DB.ORACLE, fechaInicio, fechafin))

            Dim pck As New ExcelPackage()

            'Dim wsDt As ExcelWorksheet = pck.Workbook.Worksheets.Add("testsheet")
            Dim wsDt As ExcelWorksheet = pck.Workbook.Worksheets.Add("Estado-ERROR")
            wsDt.Cells("A1").LoadFromDataTable(dt, True, TableStyles.Medium9)

            wsDt.Cells(wsDt.Dimension.Address).AutoFitColumns()

            Dim fileBytes As [Byte]() = pck.GetAsByteArray()

            HttpContext.Current.Response.ClearContent()
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ListaDeRegistroConError_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx")
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            HttpContext.Current.Response.BinaryWrite(fileBytes)
            HttpContext.Current.Response.[End]()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ConvertToDataTable(Of T)(data As IList(Of T)) As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim table As New DataTable()
        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next
        For Each item As T In data
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
            Next
            table.Rows.Add(row)
        Next
        Return table

    End Function



    Public Function GetEntExportar(ByVal Tipo As Integer, ByVal fechainicio As String, ByVal fechafin As String) As List(Of SusaludEntExportar)


        Try
            'Dim dr As OracleDataReader
            Dim ListaRetorno As New List(Of SusaludEntExportar)()
            CNXvar = OBJconexion.ConexionDB(Tipo)

            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.PRO_GET_GRID_EXPORTAR")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar


            Dim Parfechaini As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parfechaini
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@FECHAINICIO")
                .DbType = DbType.String
                .Value = fechainicio
            End With
            CMDvar.Parameters.Add(Parfechaini)

            Dim Parfechafin As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Parfechafin
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@FECHAFIN")
                .DbType = DbType.String
                .Value = fechafin
            End With
            CMDvar.Parameters.Add(Parfechafin)

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "P_CURSOR")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)


            Using dr As OracleDataReader = CMDvar.ExecuteReader(CommandBehavior.CloseConnection)
                If (dr.HasRows) Then
                    Dim Ord_IDENTIFICADORSUSALUD As Integer = dr.GetOrdinal("IDENTIFICADORSUSALUD")
                    Dim Ord_IDENTIFICADORIAFAS As Integer = dr.GetOrdinal("IDENTIFICADORIAFAS")
                    Dim Ord_DEFFECDATE As Integer = dr.GetOrdinal("DEFFECDATE")
                    Dim Ord_DFRESPUESTASUSALUD As Integer = dr.GetOrdinal("DFRESPUESTASUSALUD")
                    Dim Ord_AP_PATERNO_AFILIADO As Integer = dr.GetOrdinal("AP_PATERNO_AFILIADO")
                    Dim Ord_AP_MATERNO_AFILIADO As Integer = dr.GetOrdinal("AP_MATERNO_AFILIADO")
                    Dim Ord_NOMBRES_AFILIADO As Integer = dr.GetOrdinal("NOMBRES_AFILIADO")
                    Dim Ord_FECHA_NACIMIENTO_AFILIADO As Integer = dr.GetOrdinal("FECHA_NACIMIENTO_AFILIADO")
                    Dim Ord_GENERO_AFILIADO As Integer = dr.GetOrdinal("GENERO_AFILIADO")
                    Dim Ord_TIPO_DOCUMENTO_AFILIADO As Integer = dr.GetOrdinal("TIPO_DOCUMENTO_AFILIADO")
                    Dim Ord_NUMERO_DOCUMENTO_AFILIADO As Integer = dr.GetOrdinal("NUMERO_DOCUMENTO_AFILIADO")
                    Dim Ord_CODIGO_PAIS_AFILIADO As Integer = dr.GetOrdinal("CODIGO_PAIS_AFILIADO")
                    Dim Ord_TIPO_CONTRATANTE As Integer = dr.GetOrdinal("TIPO_CONTRATANTE")
                    Dim Ord_NOMBRE_CONTRATANTE As Integer = dr.GetOrdinal("NOMBRE_CONTRATANTE")
                    Dim Ord_TIPO_DOCUMENTO_CONTRATANTE As Integer = dr.GetOrdinal("TIPO_DOCUMENTO_CONTRATANTE")
                    Dim Ord_NUMERO_DOCUMENTO_CONTRATANTE As Integer = dr.GetOrdinal("NUMERO_DOCUMENTO_CONTRATANTE")
                    Dim Ord_CODIGO_PAIS_CONTRATANTE As Integer = dr.GetOrdinal("CODIGO_PAIS_CONTRATANTE")
                    Dim Ord_CODIGO_INTERNO As Integer = dr.GetOrdinal("CODIGO_INTERNO")
                    Dim Ord_CODIGO_CONTRATO As Integer = dr.GetOrdinal("CODIGO_CONTRATO")
                    Dim Ord_NPRODUCT As Integer = dr.GetOrdinal("NPRODUCT")
                    Dim Ord_FECHA_INICIO_AFILIACION As Integer = dr.GetOrdinal("FECHA_INICIO_AFILIACION")
                    Dim Ord_FECHA_FIN_AFILIACION As Integer = dr.GetOrdinal("FECHA_FIN_AFILIACION")
                    Dim Ord_SNOMBRE_CAMPO As Integer = dr.GetOrdinal("SNOMBRE_CAMPO")
                    Dim Ord_SDESCRIPCION_ERROR_BD As Integer = dr.GetOrdinal("SDESCRIPCION_ERROR_BD")

                    Dim _Resultado As SusaludEntExportar = Nothing
                    Dim objReader As Object() = New Object(dr.FieldCount - 1) {}

                    While dr.Read()
                        'dr.GetValues(obj);
                        dr.GetValues(objReader)
                        _Resultado = New SusaludEntExportar()
                        _Resultado.IDENTIFICADORSUSALUD = Convert.ToString(objReader(Ord_IDENTIFICADORSUSALUD))
                        _Resultado.IDENTIFICADORIAFAS = Convert.ToString(objReader(Ord_IDENTIFICADORIAFAS))
                        _Resultado.DEFFECDATE = Convert.ToString(objReader(Ord_DEFFECDATE))
                        _Resultado.DFRESPUESTASUSALUD = Convert.ToString(objReader(Ord_DFRESPUESTASUSALUD))
                        _Resultado.AP_PATERNO_AFILIADO = Convert.ToString(objReader(Ord_AP_PATERNO_AFILIADO))
                        _Resultado.AP_MATERNO_AFILIADO = Convert.ToString(objReader(Ord_AP_MATERNO_AFILIADO))
                        _Resultado.NOMBRES_AFILIADO = Convert.ToString(objReader(Ord_NOMBRES_AFILIADO))
                        _Resultado.FECHA_NACIMIENTO_AFILIADO = Convert.ToString(objReader(Ord_FECHA_NACIMIENTO_AFILIADO))
                        _Resultado.GENERO_AFILIADO = Convert.ToString(objReader(Ord_GENERO_AFILIADO))
                        _Resultado.TIPO_DOCUMENTO_AFILIADO = Convert.ToString(objReader(Ord_TIPO_DOCUMENTO_AFILIADO))
                        _Resultado.NUMERO_DOCUMENTO_AFILIADO = Convert.ToString(objReader(Ord_NUMERO_DOCUMENTO_AFILIADO))
                        _Resultado.CODIGO_PAIS_AFILIADO = Convert.ToString(objReader(Ord_CODIGO_PAIS_AFILIADO))
                        _Resultado.TIPO_CONTRATANTE = Convert.ToString(objReader(Ord_TIPO_CONTRATANTE))
                        _Resultado.NOMBRE_CONTRATANTE = Convert.ToString(objReader(Ord_NOMBRE_CONTRATANTE))
                        _Resultado.TIPO_DOCUMENTO_CONTRATANTE = Convert.ToString(objReader(Ord_TIPO_DOCUMENTO_CONTRATANTE))
                        _Resultado.NUMERO_DOCUMENTO_CONTRATANTE = Convert.ToString(objReader(Ord_NUMERO_DOCUMENTO_CONTRATANTE))
                        _Resultado.CODIGO_PAIS_CONTRATANTE = Convert.ToString(objReader(Ord_CODIGO_PAIS_CONTRATANTE))
                        _Resultado.CODIGO_INTERNO = Convert.ToString(objReader(Ord_CODIGO_INTERNO))
                        _Resultado.CODIGO_CONTRATO = Convert.ToString(objReader(Ord_CODIGO_CONTRATO))
                        _Resultado.NPRODUCT = Convert.ToString(objReader(Ord_NPRODUCT))
                        _Resultado.FECHA_INICIO_AFILIACION = Convert.ToString(objReader(Ord_FECHA_INICIO_AFILIACION))
                        _Resultado.FECHA_FIN_AFILIACION = Convert.ToString(objReader(Ord_FECHA_FIN_AFILIACION))
                        _Resultado.SNOMBRE_CAMPO = Convert.ToString(objReader(Ord_SNOMBRE_CAMPO))
                        _Resultado.SDESCRIPCION_ERROR_BD = Convert.ToString(objReader(Ord_SDESCRIPCION_ERROR_BD))

                        ListaRetorno.Add(_Resultado)
                    End While
                End If

                CNXvar.Close()

                Return ListaRetorno
                '--> DataReader
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function Lista_EstadosDeEnvios(ByVal Tipo As Integer) As DataSet
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.LISTARCOMBOESTADO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "EstadosEnvio")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function


    Public Function Insertar_datos_auditoria(ByVal Tipo As Integer, ByVal NombreArchivo As String, ByVal Fecha As String, ByVal CodUsuario As String, ByVal Comentario As String) As Boolean
        Dim dts As New DataSet
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.PRO_INSERTAR_AUDITORIA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParDescripcion As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParDescripcion
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@P_SDESCRIPCION")
                .DbType = DbType.String
                .Value = nombrearchivo
            End With
            CMDvar.Parameters.Add(ParDescripcion)

            Dim ParFecha As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParFecha
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@P_FECHA")
                .DbType = DbType.DateTime
                .Value = Convert.ToDateTime(Fecha)
            End With
            CMDvar.Parameters.Add(ParFecha)


            Dim ParCodUsuario As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParCodUsuario
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@P_USER")
                .DbType = DbType.String
                .Value = CodUsuario
            End With
            CMDvar.Parameters.Add(ParCodUsuario)

            Dim ParComentario As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParComentario
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@P_COMENTARIO")
                .DbType = DbType.String
                .Value = Comentario
            End With
            CMDvar.Parameters.Add(ParComentario)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataSet
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return True

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return False

        End Try
    End Function


    Public Function Lista_Tabla_ACTUALIZARASEG_SUSALUD(ByVal Tipo As Integer) As DataTable
        Dim dts As New DataTable
        Try
            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.LISTAR_REGISTRO")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim vardata0 As DbParameter = OBJconexion.CrearParametro(Tipo, "TABLA_CARGA")
            With vardata0
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(vardata0)

            ADAvar = OBJconexion.CrearAdaptador(Tipo, CMDvar)
            dts = New DataTable
            ADAvar.Fill(dts)

            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Return dts

        Catch ex As Exception
            CNXvar.Close()
            ADAvar = Nothing
            CNXvar = Nothing
            Throw
        End Try
    End Function


    Public Function Genera_Actualizacion_susalud(ByVal Tipo As String, ByVal usuario As Integer) As Boolean


        Dim EstadoRegistro As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "ACT_SUSALUD_MASIVA_ASEGURADOS")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim ParUsuario As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParUsuario
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@par_usuario")
                .DbType = DbType.Int64
                .Size = 12
                .Value = usuario
            End With
            CMDvar.Parameters.Add(ParUsuario)

            Dim ParEstado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@P_ESTADO")
                .DbType = DbType.Int64
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstado)

            CMDvar.ExecuteNonQuery()
            EstadoRegistro = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@P_ESTADO")).Value


            CNXvar.Close()
            CNXvar = Nothing

            Return EstadoRegistro
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function


    Public Function Actualizar_estado(ByVal Tipo As String, ByVal idcorrelativo As Long) As Boolean


        Dim EstadoRegistro As Integer = 0
        Try

            CNXvar = OBJconexion.ConexionDB(Tipo)
            If CNXvar.State = ConnectionState.Closed Then CNXvar.Open()

            CMDvar = OBJconexion.CrearCommando(Tipo, "PKG_SUSALUD.ACTUALIZAR_ESTADO_LISTA")
            CMDvar.CommandType = CommandType.StoredProcedure
            CMDvar.Connection = CNXvar

            Dim Paridcorrelativo As DbParameter = OBJconexion.CrearParametro(Tipo)
            With Paridcorrelativo
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@idcorrelativo")
                .DbType = DbType.Int64
                .Size = 12
                .Value = idcorrelativo
            End With
            CMDvar.Parameters.Add(Paridcorrelativo)

            Dim ParEstado As DbParameter = OBJconexion.CrearParametro(Tipo)
            With ParEstado
                .ParameterName = OBJconexion.Dev_Par(Tipo, "@nestado")
                .DbType = DbType.Int64
                .Direction = ParameterDirection.Output
            End With
            CMDvar.Parameters.Add(ParEstado)

            CMDvar.ExecuteNonQuery()
            EstadoRegistro = CMDvar.Parameters(OBJconexion.Dev_Par(Tipo, "@nestado")).Value


            CNXvar.Close()
            CNXvar = Nothing

            Return EstadoRegistro
        Catch ex As Exception
            CNXvar.Close()
            CNXvar = Nothing
            Throw
        End Try

    End Function

#End Region


End Class
