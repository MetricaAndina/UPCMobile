using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCCInfo.DC
{
    [DataContract]
    public class DCRecurso
    {
        private Int32 _CodRecurso;
        private String _NomRecurso;
        private String _Local;
        private String _FecReserva;
        private String _HoraIni;
        private String _HoraFin;
        private Boolean _Estado;
        private String _CodError;
        private String _Mensaje;
 
        [DataMember(Order = 1)]
        public Int32 CodRecurso
        {
            get { return _CodRecurso; }
            set { _CodRecurso = value; }
        }

        [DataMember(Order = 2)]
        public String NomRecurso
        {
            get { return _NomRecurso; }
            set { _NomRecurso = value; }
        }

        [DataMember(Order = 3)]
        public String Local
        {
            get { return _Local; }
            set { _Local = value; }
        }

        [DataMember(Order = 4)]
        public String FecReserva
        {
            get { return _FecReserva; }
            set { _FecReserva = value; }
        }

        [DataMember(Order = 5)]
        public String HoraIni
        {
            get { return _HoraIni; }
            set { _HoraIni = value; }
        }

        [DataMember(Order = 6)]
        public String HoraFin
        {
            get { return _HoraFin; }
            set { _HoraFin = value; }
        }

        [DataMember(Order = 7)]
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        [DataMember(Order = 8)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 9)]
        public String Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }
    }
}
