
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCCInfo.DC
{
    [DataContract]
    public class DCListaRecurso
    {
        private String _CodError;
        private String _Mensaje;
        private String _TipoRecurso;
        private String _FecReserva;
        private String _CanHoras;
        private List<DCRecurso> _Recursos;

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
        public String TipoRecurso
        {
            get { return _TipoRecurso; }
            set { _TipoRecurso = value; }
        }

        [DataMember(Order = 4)]
        public String FecReserva
        {
            get { return _FecReserva; }
            set { _FecReserva = value; }
        }         

        [DataMember(Order = 5)]
        public String CanHoras
        {
            get { return _CanHoras; }
            set { _CanHoras = value; }
        }

        [DataMember(Order = 6)]
        public List<DCRecurso> Recursos
        {
            get { return _Recursos; }
            set { _Recursos = value; }
        }
    }
}
