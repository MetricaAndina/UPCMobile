using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class ReservaEspacioDepDC
    {
        private String _CodAlumno;
        private String _CodError;
        private String _MsgError;
        private String _MsgConfirmacion;
        private String _Estado;

        [DataMember(Order = 1)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 2)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 3)]
        public String MsgError
        {
            get { return _MsgError; }
            set { _MsgError = value; }
        }

        [DataMember(Order = 4)]
        public String MsgConfirmacion
        {
            get { return _MsgConfirmacion; }
            set { _MsgConfirmacion = value; }
        }

        [DataMember(Order = 5)]
        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
    }
}
