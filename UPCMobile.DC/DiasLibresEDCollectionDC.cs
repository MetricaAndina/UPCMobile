using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class DiasLibresEDCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<DiaLibreEDDC> _horario;

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
        public Collection<DiaLibreEDDC> HorarioDia
        {
            get { return _horario; }
            set { _horario = value; }
        }
    }

    [DataContract]
    public class DiaLibreEDDC
    {
        private String _coddia;
        private String _fecha;
        private Collection<HorasLibresEDDC> _disponibles;

        [DataMember(Order = 1)]
        public String CodDia
        {
            get { return _coddia; }
            set { _coddia = value; }
        }

        [DataMember(Order = 2)]
        public String Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        [DataMember(Order = 3)]
        public Collection<HorasLibresEDDC> Disponibles
        {
            get { return _disponibles; }
            set { _disponibles = value; }
        }
    }

    [DataContract]
    public class HorasLibresEDDC
    {
        private String _fecha;
        private String _horafin;
        private String _horainicio;
        private String _sede;

        [DataMember(Order = 1)]
        public String Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        [DataMember(Order = 2)]
        public String HoraFin
        {
            get { return _horafin; }
            set { _horafin = value; }
        }

        [DataMember(Order = 3)]
        public String HoraInicio
        {
            get { return _horainicio; }
            set { _horainicio = value; }
        }

        [DataMember(Order = 4)]
        public String Sede
        {
            get { return _sede; }
            set { _sede = value; }
        }
    }
}
