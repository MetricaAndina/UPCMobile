using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCCInfo.DC
{
    [DataContract]
    public class DCReservaRecurso
    {
        private String _CodRecurso;//LISTO INTEGRO O NEMONICO        
        private String _CodReserva;//LISTO INTEGRO O NEMONICO    
        private String _CodUsuario;//HAY
        private String _CodError;//DEPENDE
        private String _Mensaje;//DEPENDE

        
        [DataMember(Order = 1)]
        public String CodRecurso
        {
            get { return _CodRecurso; }
            set { _CodRecurso = value; }
        }

        [DataMember(Order = 2)]
        public String CodUsuario
        {
            get { return _CodUsuario; }
            set { _CodUsuario = value; }
        }

        #region error
        [DataMember(Order = 4)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 5)]
        public String Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }
        [DataMember(Order = 6)]
        public String CodReserva
        {
            get { return _CodReserva; }
            set { _CodReserva = value; }
        }
        #endregion
    }
}
