using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class PagoPendienteCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<PagoPendienteDC> _PagosPendientes;

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
        public Collection<PagoPendienteDC> PagosPendientes
        {
            get { return _PagosPendientes; }
            set { _PagosPendientes = value; }
        }
    }
}
