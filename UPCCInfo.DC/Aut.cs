using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCCInfo.DC
{
    [DataContract]
    public class Aut
    {

        private String _sUsuario;
        private String _sContrasena;
        private Char _cPlataforma;
        [DataMember]
        public String Usuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }

        [DataMember]
        public String Contrasena
        {
            get { return _sContrasena; }
            set { _sContrasena = value; }
        }
        [DataMember]
        public Char Plataforma
        {
            get { return _cPlataforma; }
            set { _cPlataforma = value; }
        }

    }
}
