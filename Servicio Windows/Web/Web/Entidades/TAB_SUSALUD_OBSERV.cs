using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entidades
{
    public class TAB_SUSALUD_OBSERV
    {
        public Decimal NCODIGO_OBS { get; set; }
        public Decimal NCORRELATIVO { get; set; }
        public string PKAFILIADO { get; set; }
        public string PKAFILIACION { get; set; }
        public int NTIPO_ERROR { get; set; }
        public string SID_ERROR { get; set; }
        public string SDESCRIPCION_ERROR_SERVICIO { get; set; }
        public string SDESCRIPCION_ERROR_BD { get; set; }
        public string SID_CAMPO { get; set; }
        public string SNOMBRE_CAMPO{ get; set; }
        public string SID_REGLA_AFILIACION { get; set; }
    }
}