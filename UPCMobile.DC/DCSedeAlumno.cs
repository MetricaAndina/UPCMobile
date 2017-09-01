using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCCInfo.DC
{
    [DataContract]
    public class DCSedeAlumno
    {
        //VER SI MAS ADELANTE SE ENVIA EL CODIGO DE ALUMNO
        private String _CodLocal;
        private String _Descripcion;
        private String _CodError;
        private String _MsgError;


        [DataMember(Order = 0)]
        public string CodLocal
        {
            get { return _CodLocal; }
            set { _CodLocal = value; }
        }

        [DataMember(Order = 1)]
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        [DataMember(Order = 2)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 3)]
        public String MsgError
        {
            get { return _MsgError; }
            set { _MsgError = value; }
        }
    }
}
