using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class ListadoHijosDC
    {
        private String _codigo_padre;
        private String _nombre_padre;
        private String _apellido_padre;
        private String _cod_error;
        private String _msg_error;
        private Collection<HijoDC> _hijos;

        [DataMember(Order = 1)]
        public String Codigo
        {
            get { return _codigo_padre; }
            set { _codigo_padre = value; }
        }

        [DataMember(Order = 2)]
        public String NombrePadre
        {
            get { return _nombre_padre; }
            set { _nombre_padre = value; }
        }

        [DataMember(Order = 3)]
        public String ApellidoPadre
        {
            get { return _apellido_padre; }
            set { _apellido_padre = value; }
        }

        [DataMember(Order = 4)]
        public String CodError
        {
            get { return _cod_error; }
            set { _cod_error = value; }
        }

        [DataMember(Order = 5)]
        public String MsgError
        {
            get { return _msg_error; }
            set { _msg_error = value; }
        }

        [DataMember(Order = 6)]
        public Collection<HijoDC> hijos
        {
            get { return _hijos; }
            set { _hijos = value; }
        }
    }

    [DataContract]
    public class HijoDC
    {
        private String _codigo;
        private String _nombres;
        private String _apellidos;

        [DataMember(Order = 1)]
        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        [DataMember(Order = 2)]
        public String nombres
        {
            get { return _nombres; }
            set { _nombres = value; }
        }

        [DataMember(Order = 3)]
        public String apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }
    }
}
