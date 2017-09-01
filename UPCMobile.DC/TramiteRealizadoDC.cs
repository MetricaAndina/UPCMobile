using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class TramiteRealizadoDC
    {
        private Int64 _CodTramiteRealizado;
        private String _CodAlumno;
        private String _NroSolicitud;
        private String _Nombre;
        private String _Fecha;
        private String _Estado;

        [DataMember(Order = 1)]
        public Int64 CodTramiteRealizado
        {
            get { return _CodTramiteRealizado; }
            set { _CodTramiteRealizado = value; }
        }

        [DataMember(Order = 2)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 3)]
        public String NroSolicitud
        {
            get { return _NroSolicitud; }
            set { _NroSolicitud = value; }
        }

        [DataMember(Order = 4)]
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        [DataMember(Order = 5)]
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        [DataMember(Order = 6)]
        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
    }
}
