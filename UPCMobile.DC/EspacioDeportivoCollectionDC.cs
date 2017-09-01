using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class EspacioDeportivoCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<SedeDC> _sedes;

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
        public Collection<SedeDC> Sedes
        {
            get { return _sedes; }
            set { _sedes = value; }
        }
    }

    [DataContract]
    public class SedeDC
    {
        private String _sede;
        private String _key;
        private Collection<EspacioDC> _espacios;

        [DataMember(Order = 1)]
        public String sede
        {
            get { return _sede; }
            set { _sede = value; }
        }

        [DataMember(Order = 2)]
        public String key
        {
            get { return _key; }
            set { _key = value; }
        }

        [DataMember(Order = 3)]
        public Collection<EspacioDC> espacios
        {
            get { return _espacios; }
            set { _espacios = value; }
        }
    }

    [DataContract]
    public class EspacioDC
    {
        private String _nombre;
        private String _codigo;
        private Collection<ActividadDC> _actividades;

        [DataMember(Order = 1)]
        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        [DataMember(Order = 2)]
        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        [DataMember(Order = 3)]
        public Collection<ActividadDC> actividades
        {
            get { return _actividades; }
            set { _actividades = value; }
        }
    }

    [DataContract]
    public class ActividadDC
    {
        private String _nombre;
        private String _codigo;

        [DataMember(Order = 1)]
        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        [DataMember(Order = 2)]
        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}
