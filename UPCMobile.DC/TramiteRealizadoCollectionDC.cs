using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class TramiteRealizadoCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<TramiteRealizadoDC> _TramitesRealizados;

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
        public Collection<TramiteRealizadoDC> TramitesRealizados
        {
            get { return _TramitesRealizados; }
            set { _TramitesRealizados = value; }
        }
    }
}
