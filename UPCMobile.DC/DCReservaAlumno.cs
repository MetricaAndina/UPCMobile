using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class DCReservaAlumno
    {
        private decimal _CodReserva;
        private String _FecReserva;
        private Int32 _CodRecurso;
        private String _NomRecurso;
        private String _CodTipoRecurso;
        private String _DesTipoRecurso;

        private String _HoraIni;
        private String _HoraFin;
        private String _Horas;
        private String _CodEstado;
        private String _DesEstado;
        private String _DesLocal;
        private decimal _Tolerancia;
        private String _DesTipo;

         
        [DataMember(Order = 1)]
        public decimal CodReserva
        {
            get { return _CodReserva; }
            set { _CodReserva = value; }
        }

        [DataMember(Order = 2)]
        public String FecReserva
        {
            get { return _FecReserva; }
            set { _FecReserva = value; }
        }

        [DataMember(Order = 3)]
        public Int32 CodRecurso
        {
            get { return _CodRecurso; }
            set { _CodRecurso = value; }
        }

        [DataMember(Order = 4)]
        public String NomRecurso
        {
            get { return _NomRecurso; }
            set { _NomRecurso = value; }
        }

        [DataMember(Order = 5)]
        public String CodTipoRecurso
        {
            get { return _CodTipoRecurso; }
            set { _CodTipoRecurso = value; }
        }

        [DataMember(Order = 6)]
        public String DesTipoRecurso
        {
            get { return _DesTipoRecurso; }
            set { _DesTipoRecurso = value; }
        }

        [DataMember(Order = 7)]
        public String HoraIni
        {
            get { return _HoraIni; }
            set { _HoraIni = value; }
        }

        [DataMember(Order = 8)]
        public String HoraFin
        {
            get { return _HoraFin; }
            set { _HoraFin = value; }
        }

        [DataMember(Order = 9)]
        public String Horas
        {
            get { return _Horas; }
            set { _Horas = value; }
        }

        [DataMember(Order = 10)]
        public String CodEstado
        {
            get { return _CodEstado; }
            set { _CodEstado = value; }
        }

        [DataMember(Order = 11)]
        public String DesEstado
        {
            get { return _DesEstado; }
            set { _DesEstado = value; }
        }

        [DataMember(Order = 12)]
        public String DesLocal
        {
            get { return _DesLocal; }
            set { _DesLocal = value; }
        }

        [DataMember(Order = 13)]
        public decimal Tolerancia
        {
            get { return _Tolerancia; }
            set { _Tolerancia = value; }
        }

        [DataMember(Order = 14)]
        public String DesTipo
        {
            get { return _DesTipo; }
            set { _DesTipo = value; }
        }

    }
}
