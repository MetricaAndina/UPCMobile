using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class ListadoSedesAlumnoDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<LocalDC> _sedes;

        [DataMember(Order = 1)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 2)]
        public String MsgError
        {
            get { return _MsgError; }
            set { _MsgError = value; }
        }

        [DataMember(Order = 3)]
        public Collection<LocalDC> Sedes
        {
            get { return _sedes; }
            set { _sedes = value; }
        }
    }

    [DataContract]
    public class LocalDC
    {
        private String _Codigo;
        private String _Descripcion;

        [DataMember(Order = 1)]
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        [DataMember(Order = 2)]
        public String Descripcion 
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
    }
}
