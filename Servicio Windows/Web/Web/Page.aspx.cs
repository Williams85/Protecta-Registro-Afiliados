using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using pe.gob.susalud.jr.transaccion.susalud.service.imp;
using System.IO;
using System.Configuration;
using Web.Entidades;
using Oracle.DataAccess.Client;
using System.ComponentModel;

using OfficeOpenXml;
using OfficeOpenXml.Table;
using pe.gob.susalud.jr.transaccion.susalud.bean;
using java.util;
using System.Xml;
using System.Xml.Serialization;
using Web.Constants;


namespace Web
{


    public partial class Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Insertar_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Tramas");
            string file = rbOk.Checked ? "ok.txt" : "error.txt";
            path = path + @"\" + file;

            //Leyendo la trama XML
            XmlDocument _Doc = new XmlDocument();
            _Doc.Load(path);
            var ser = new XmlSerializer(typeof(Online997RegafiUpdateResponse));
            var _Online997RegafiUpdateResponse = (Online997RegafiUpdateResponse)ser.Deserialize(new StringReader(_Doc.OuterXml));

            //Convirtiendo el campo "txRespuesta" a un objeto
            RegafiUpdate997ServiceImpl impl = new RegafiUpdate997ServiceImpl();
            var objetorespuesta = impl.x12NToBean(_Online997RegafiUpdateResponse.txRespuesta);

            // ---> Obteniendo valores de la respuesta
            var excProceso = objetorespuesta.getExcProceso();
            var feTransaccion = objetorespuesta.getFeTransaccion();
            var flag = objetorespuesta.isFlag();
            var hoTransaccion = objetorespuesta.getHoTransaccion();
            var IdCorrelativo = objetorespuesta.getIdCorrelativo();
            var idReceptor = objetorespuesta.getIdReceptor();
            var idRemitente = objetorespuesta.getIdRemitente();
            var idTransaccion = objetorespuesta.getIdTransaccion();

            //Obteniendo lista de errores
            java.util.List ListaErrores = new java.util.ArrayList();
            ListaErrores = objetorespuesta.getIn271RegafiUpdateExcepcion();

            var noTransaccion = objetorespuesta.getNoTransaccion();
            var nuControl = objetorespuesta.getNuControl();
            var nuControlST = objetorespuesta.getNuControlST();

            // ---> Cargando el objeto a insertar
            TAB_SUSALUD objCabecera = new TAB_SUSALUD();
            objCabecera.SCERTYPE = "1";
            objCabecera.NBRANCH = 1200;
            objCabecera.NPRODUCT = 2200;
            objCabecera.NPOLICY = 3020;
            objCabecera.NCERTIF = 2562;
            objCabecera.DEFFECDATE = DateTime.Now;
            objCabecera.NCORRELATIVO = Convert.ToDecimal(IdCorrelativo);
            objCabecera.STIPO_OPERACION = "10"; //10: Actualiza, Afiliado y Afiliación
            //Verificando si hay errores a nivel del servicio y a nivel de base de datos
            if ((excProceso == EstadoServicio.Todo_Correcto) && !ExisteErrorBD(ListaErrores))
            {
                //Es correcto
                objCabecera.SESTADO_SUSALUD = EstadoTransaccion.Satisfactorio;
            }
            else
            {
                //Hay errores
                objCabecera.SESTADO_SUSALUD = EstadoTransaccion.Error;
            }

            //Cargando el detalle del error
            string coCampoErr = "";
            string excBD = "";
            string inCoErrorEncontrado = "";
            string pkAfiliado = "";
            string pkAfiliadopkAfiliacion = "";

            List<TAB_SUSALUD_OBSERV> _Lista_Observaciones = new List<TAB_SUSALUD_OBSERV>();

            TAB_SUSALUD_OBSERV objDetalle = new TAB_SUSALUD_OBSERV();
            //Cuando no hay errores en el servicio y en la base de datos a la vez
            if ((excProceso == EstadoServicio.Todo_Correcto) && !ExisteErrorBD(ListaErrores))
            {
                //Solo debe de haber un registro con el campo excBD = "0000"
                foreach (In997RegafiUpdateExcepcion item in ListaErrores.toArray())
                {
                    coCampoErr = item.getCoCampoErr();
                    excBD = item.getExcBD();
                    inCoErrorEncontrado = item.getInCoErrorEncontrado();
                    pkAfiliado = item.getPkAfiliado();
                    pkAfiliadopkAfiliacion = item.getPkAfiliadopkAfiliacion();
                }
                objDetalle.NCODIGO_OBS = 0; // No se llena porque es un correlativo, y se ingresa con un secuencial;
                objDetalle.NCORRELATIVO = Convert.ToDecimal(IdCorrelativo);
                objDetalle.PKAFILIADO = pkAfiliado;
                objDetalle.PKAFILIACION = pkAfiliadopkAfiliacion;
                objDetalle.NTIPO_ERROR = TipoError.Sin_Error;
                objDetalle.SID_ERROR = "0";
                objDetalle.SDESCRIPCION_ERROR_SERVICIO = "";
                objDetalle.SDESCRIPCION_ERROR_BD = "";
                objDetalle.SID_CAMPO = "";
                objDetalle.SNOMBRE_CAMPO = "";
                objDetalle.SID_REGLA_AFILIACION = "";

                _Lista_Observaciones.Add(objDetalle);
                objCabecera.DETALLE = _Lista_Observaciones;
            }

            //Cuando hay error en el servicio
            coCampoErr = "";
            excBD = "";
            inCoErrorEncontrado = "";
            pkAfiliado = "";
            pkAfiliadopkAfiliacion = "";

            if (excProceso != EstadoServicio.Todo_Correcto)
            {
                foreach (In997RegafiUpdateExcepcion item in ListaErrores.toArray())
                {
                    coCampoErr = item.getCoCampoErr();
                    excBD = item.getExcBD();
                    inCoErrorEncontrado = item.getInCoErrorEncontrado();
                    pkAfiliado = item.getPkAfiliado();
                    pkAfiliadopkAfiliacion = item.getPkAfiliadopkAfiliacion();
                }
                objDetalle.NCODIGO_OBS = 0; // No se llena porque es un correlativo, y se ingresa con un secuencial;
                objDetalle.NCORRELATIVO = Convert.ToDecimal(IdCorrelativo);
                objDetalle.PKAFILIADO = pkAfiliado;
                objDetalle.PKAFILIACION = pkAfiliadopkAfiliacion;
                objDetalle.NTIPO_ERROR = TipoError.Error_Servicio;
                objDetalle.SID_ERROR = excProceso;
                objDetalle.SDESCRIPCION_ERROR_SERVICIO = ""; //Se llena por base de datos
                objDetalle.SDESCRIPCION_ERROR_BD = "";
                objDetalle.SID_CAMPO = "";
                objDetalle.SNOMBRE_CAMPO = "";
                objDetalle.SID_REGLA_AFILIACION = "";

                _Lista_Observaciones.Add(objDetalle);
                objCabecera.DETALLE = _Lista_Observaciones;
            }

            //Cuando hay errores en la base de datos
            coCampoErr = "";
            excBD = "";
            inCoErrorEncontrado = "";
            pkAfiliado = "";
            pkAfiliadopkAfiliacion = "";

            if ((excProceso == EstadoServicio.Todo_Correcto) && ExisteErrorBD(ListaErrores))
            {
                foreach (In997RegafiUpdateExcepcion item in ListaErrores.toArray())
                {
                    coCampoErr = item.getCoCampoErr();
                    excBD = item.getExcBD();
                    inCoErrorEncontrado = item.getInCoErrorEncontrado();
                    pkAfiliado = item.getPkAfiliado();
                    pkAfiliadopkAfiliacion = item.getPkAfiliadopkAfiliacion();

                    objDetalle.NCODIGO_OBS = 0; // No se llena porque es un correlativo, y se ingresa con un secuencial;
                    objDetalle.NCORRELATIVO = Convert.ToDecimal(IdCorrelativo);
                    objDetalle.PKAFILIADO = pkAfiliado;
                    objDetalle.PKAFILIACION = pkAfiliadopkAfiliacion;
                    objDetalle.NTIPO_ERROR = TipoError.Error_BD;
                    objDetalle.SID_ERROR = excBD;
                    objDetalle.SDESCRIPCION_ERROR_SERVICIO = "";
                    objDetalle.SDESCRIPCION_ERROR_BD = ""; //Se llena por base de datos
                    objDetalle.SID_CAMPO = coCampoErr;
                    objDetalle.SNOMBRE_CAMPO = ""; //Se llena por base de datos;
                    objDetalle.SID_REGLA_AFILIACION = inCoErrorEncontrado;

                    _Lista_Observaciones.Add(objDetalle);
                }

                objCabecera.DETALLE = _Lista_Observaciones;
            }

            //Insertando 
            int result = InsertarResultado(objCabecera);

        }

        public Boolean ExisteErrorBD(java.util.List ListaErrores)
        {
            Boolean result = false;
            string excBD;
            foreach (In997RegafiUpdateExcepcion item in ListaErrores.toArray())
            {
                excBD = item.getExcBD();
                result = (excBD != EstadoBaseDatos.Todo_Correcto) ? true : false;
            }
            return result;
        }


        protected void btn_Listar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ConvertToDataTable(GetResultado());

            gridRespuesta.DataSource = dt;
            gridRespuesta.DataBind();

        }

        public static int InsertarResultado(TAB_SUSALUD objCabecera)
        {
            int result = 1;
            string _Cnx = ConfigurationManager.ConnectionStrings["CnxOra"].ConnectionString;            
            List<OracleParameter> Parms = new List<OracleParameter>();

            try
            {
                OracleConnection con = new OracleConnection(_Cnx);
                con.Open();

                OracleTransaction myTrans;
                myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_RESULTADO.PRO_INSERTAR_CABECERA", con))
                    {
                        Parms.Add(new OracleParameter("P_SCERTYPE", OracleDbType.Char, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.SCERTYPE;

                        Parms.Add(new OracleParameter("P_NBRANCH", OracleDbType.Decimal, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.NBRANCH;

                        Parms.Add(new OracleParameter("P_NPRODUCT", OracleDbType.Decimal, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.NBRANCH;

                        Parms.Add(new OracleParameter("P_NPOLICY", OracleDbType.Decimal, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.NPOLICY;

                        Parms.Add(new OracleParameter("P_NCERTIF", OracleDbType.Decimal, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.NCERTIF;

                        Parms.Add(new OracleParameter("P_DEFFECDATE", OracleDbType.Date, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.DEFFECDATE;

                        Parms.Add(new OracleParameter("P_NCORRELATIVO", OracleDbType.Decimal, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.NCORRELATIVO;

                        Parms.Add(new OracleParameter("P_STIPO_OPERACION", OracleDbType.Char, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.STIPO_OPERACION;

                        Parms.Add(new OracleParameter("P_SESTADO_SUSALUD", OracleDbType.Char, ParameterDirection.Input));
                        Parms.Last().Value = objCabecera.SESTADO_SUSALUD;

                        cmd.Transaction = myTrans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(Parms.ToArray());
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var det in objCabecera.DETALLE)
                    {

                        Parms.Clear();

                        using (OracleCommand cmd = new OracleCommand("PKG_RESULTADO.PRO_INSERTAR_DETALLE", con))
                        {
                            Parms.Add(new OracleParameter("P_NCORRELATIVO", OracleDbType.Decimal, ParameterDirection.Input));
                            Parms.Last().Value = det.NCORRELATIVO;

                            Parms.Add(new OracleParameter("P_PKAFILIADO", OracleDbType.Varchar2, ParameterDirection.Input));
                            Parms.Last().Value = det.PKAFILIADO;

                            Parms.Add(new OracleParameter("P_PKAFILIACION", OracleDbType.Varchar2, ParameterDirection.Input));
                            Parms.Last().Value = det.PKAFILIACION;

                            Parms.Add(new OracleParameter("P_NTIPO_ERROR", OracleDbType.Decimal, ParameterDirection.Input));
                            Parms.Last().Value = det.NTIPO_ERROR;

                            Parms.Add(new OracleParameter("P_SID_ERROR", OracleDbType.Char, ParameterDirection.Input));
                            Parms.Last().Value = det.SID_ERROR;

                            Parms.Add(new OracleParameter("P_SDESCRIPCION_ERROR_SERVICIO", OracleDbType.Varchar2, ParameterDirection.Input));
                            Parms.Last().Value = det.SDESCRIPCION_ERROR_SERVICIO;

                            Parms.Add(new OracleParameter("P_SDESCRIPCION_ERROR_BD", OracleDbType.Varchar2, ParameterDirection.Input));
                            Parms.Last().Value = det.SDESCRIPCION_ERROR_BD;

                            Parms.Add(new OracleParameter("P_SID_CAMPO", OracleDbType.Char, ParameterDirection.Input));
                            Parms.Last().Value = det.SID_CAMPO;

                            Parms.Add(new OracleParameter("P_SNOMBRE_CAMPO", OracleDbType.Varchar2, ParameterDirection.Input));
                            Parms.Last().Value = det.SNOMBRE_CAMPO;

                            Parms.Add(new OracleParameter("P_SID_REGLA_AFILIACION", OracleDbType.Decimal, ParameterDirection.Input));
                            Parms.Last().Value = det.SID_REGLA_AFILIACION;

                            cmd.Transaction = myTrans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(Parms.ToArray());
                            cmd.ExecuteNonQuery();
                        }

                    }

                    //Commit
                    myTrans.Commit();
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    result = 0;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                    myTrans.Dispose();
                }

            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public static List<Resultado> GetResultado()
        {
            string _Cnx = ConfigurationManager.ConnectionStrings["CnxOra"].ConnectionString;

            OracleParameter[] Parms = new OracleParameter[1];
            List<Resultado> ListaRetorno = new List<Resultado>();

            try
            {

                //Parms[0] = new OracleParameter("CodDepartamento_", OracleDbType.Char, ParameterDirection.Input);
                //Parms[0].Value = ConvertToDBNull(entidad.CodDepartamento);

                //Parms[1] = new OracleParameter("CodProvincia_", OracleDbType.Char, ParameterDirection.Input);
                //Parms[1].Size = 2;
                //Parms[1].Value = ConvertToDBNull(entidad.CodProvincia);

                Parms[0] = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                //using (OracleConnection con = new OracleConnection("Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST=192.168.10.115) (PORT=1980) ) (CONNECT_DATA= (SERVER=dedicated) (SERVICE_NAME=dbdev) ) );user id=ES_RENIPRESS2;password=admsistemas;persist security info=True"))
                using (OracleConnection con = new OracleConnection(_Cnx))
                {
                    con.Open();

                    using (OracleCommand cmd = new OracleCommand("PKG_SUSALUD_RESULTADO.PRO_GET_GRID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(Parms);

                        using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if ((dr.HasRows))
                            {
                                int Ord_ID_ORIGEN = dr.GetOrdinal("ID_ORIGEN");
                                int Ord_PK_AFIL_PAIS = dr.GetOrdinal("PK_AFIL_PAIS");
                                int Ord_PK_AFIL_NUM_TIP_DOC = dr.GetOrdinal("PK_AFIL_NUM_TIP_DOC");
                                int Ord_ESTADO = dr.GetOrdinal("ESTADO");
                                int Ord_ID_ERROR = dr.GetOrdinal("ID_ERROR");
                                int Ord_NOMBRE_CAMPO = dr.GetOrdinal("NOMBRE_CAMPO");
                                int Ord_DESCRIPCION_ERROR = dr.GetOrdinal("DESCRIPCION_ERROR");

                                Resultado _Resultado = default(Resultado);
                                object[] objReader = new object[dr.FieldCount];

                                while (dr.Read())
                                {
                                    //dr.GetValues(obj);
                                    dr.GetValues(objReader);
                                    _Resultado = new Resultado();
                                    _Resultado.ID_ORIGEN = Convert.ToString(objReader[Ord_ID_ORIGEN]);
                                    _Resultado.PK_AFIL_PAIS = Convert.ToString(objReader[Ord_PK_AFIL_PAIS]);
                                    _Resultado.PK_AFIL_NUM_TIP_DOC = Convert.ToString(objReader[Ord_PK_AFIL_NUM_TIP_DOC]);
                                    _Resultado.ESTADO = Convert.ToString(objReader[Ord_ESTADO]);
                                    _Resultado.ID_ERROR = Convert.ToString(objReader[Ord_ID_ERROR]);
                                    _Resultado.NOMBRE_CAMPO = Convert.ToString(objReader[Ord_NOMBRE_CAMPO]);
                                    _Resultado.DESCRIPCION_ERROR = Convert.ToString(objReader[Ord_DESCRIPCION_ERROR]);

                                    ListaRetorno.Add(_Resultado);
                                }
                            }
                            return ListaRetorno;
                        } //--> DataReader
                    } //--> Commmand
                } //--> Conexio BD
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        protected void btn_Exportar_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = ConvertToDataTable(GetResultado());

            ExcelPackage pck = new ExcelPackage();

            ExcelWorksheet wsDt = pck.Workbook.Worksheets.Add("testsheet");
            wsDt.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.Medium9);
            //wsDt.Cells[2, 2, dt.Rows.Count + 1, 2].Style.Numberformat.Format = "#,##0";
            //wsDt.Cells[2, 3, dt.Rows.Count + 1, 4].Style.Numberformat.Format = "mm-dd-yy";
            wsDt.Cells[wsDt.Dimension.Address].AutoFitColumns();

            //var stream = new MemoryStream(pck.GetAsByteArray());
            Byte[] fileBytes = pck.GetAsByteArray();

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Respuestas_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx");
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.BinaryWrite(fileBytes);
            HttpContext.Current.Response.End();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                var hola = "asdsadsa";
            }
            else {

                var hola2 = "asdsadsa";
            }
        }





    }
}