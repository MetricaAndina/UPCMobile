using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace UPCMobile.DC
{

    [DataContract]
    public class Aut2
    {
        private String _sUsuario;
        [DataMember (Order=1)]
        public String sUsuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }

        private String _sContrasena;
        [DataMember (Order=2)]
        public String sContrasena
        {
            get { return _sContrasena; }
            set { _sContrasena = value; }
        }
    }
}
