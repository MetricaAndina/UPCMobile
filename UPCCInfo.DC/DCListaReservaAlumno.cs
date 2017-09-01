
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCCInfo.DC
{
    [DataContract]
    public class DCListaReservaAlumno
    {
        private String _CodError;
        private String _Mensaje;
        private String _CodAlumno;
        private String _FecIni;
        private String _FecFin;        
        private List<DCReservaAlumno> _Reservas;

        [DataMember(Order = 1)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 2)]
        public String Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }

        [DataMember(Order = 3)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 4)]
        public String FecIni
        {
            get { return _FecIni; }
            set { _FecIni = value; }
        }

        [DataMember(Order = 5)]
        public String FecFin
        {
            get { return _FecFin; }
            set { _FecFin = value; }
        }      

        [DataMember(Order = 6)]
        public List<DCReservaAlumno> Reservas
        {
            get { return _Reservas; }
            set { _Reservas = value; }
        }
    }
}
