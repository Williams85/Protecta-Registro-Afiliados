using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entidades
{
    public class TAB_SUSALUD
    {
        public string SCERTYPE { get; set; }
        public Decimal NBRANCH { get; set; }
        public Decimal NPRODUCT { get; set; }
        public Decimal NPOLICY { get; set; }
        public Decimal NCERTIF { get; set; }
        public DateTime DEFFECDATE { get; set; }
        public Decimal NCORRELATIVO { get; set; }
        public string STIPO_OPERACION { get; set; }
        public string SESTADO_SUSALUD { get; set; }
        public List<TAB_SUSALUD_OBSERV> DETALLE { get; set; }

    }
}