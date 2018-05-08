using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Constants
{
    static class EstadoServicio
    {
        public const string Todo_Correcto = "0000";
        public const string Error_en_estructura_XML_de_entrada = "0010";
        public const string Error_del_sistema = "0020"; 
        public const string No_ingreso_nombre_de_Transaccion = "0300"; 
        public const string No_existe_el_nombre_de_la_transaccion = "0310"; 
        public const string No_ingreso_trama = "0500"; 
    }

    static class EstadoBaseDatos
    {
        public const string Todo_Correcto = "0000";
        public const string Error_al_validar_la_información_input = "0001";
        public const string Error_de_Base_de_Datos = "0002";
    }

    static class EstadoTransaccion
    {
        public const string Pendiente = "P";
        public const string Satisfactorio = "S";
        public const string Error = "E";
    }

    static class TipoError
    {
        public const int Sin_Error = 0;
        public const int Error_Servicio = 1;
        public const int Error_BD = 2;
    }


}