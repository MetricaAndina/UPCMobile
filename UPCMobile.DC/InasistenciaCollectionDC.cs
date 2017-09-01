using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class InasistenciaCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<InasistenciaDC> _Inasistencias;

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
        public Collection<InasistenciaDC> Inasistencias
        {
            get { return _Inasistencias; }
            set { _Inasistencias = value; }
        }
    }
}
