using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entidades
{
    public class Resultado
    {
        public string  ID_ORIGEN { get; set; }
        public string  PK_AFIL_PAIS { get; set; }
        public string  PK_AFIL_NUM_TIP_DOC { get; set; }
        public string  ESTADO { get; set; }
        public string  ID_ERROR { get; set; }
        public string  NOMBRE_CAMPO { get; set; }
        public string DESCRIPCION_ERROR{ get; set; }

    }
}