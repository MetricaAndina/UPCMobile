using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace UPCMobile.DC
{
    [DataContract]
    public class CursoAlumnoCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<CursoAlumnoDC> _Cursos;

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
        public Collection<CursoAlumnoDC> Cursos
        {
            get { return _Cursos; }
            set { _Cursos = value; }
        }
    }
}
