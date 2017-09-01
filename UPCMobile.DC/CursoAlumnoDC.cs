using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class CursoAlumnoDC
    {
        private String _CodCurso;
        private String _CodAlumno;
        private String _CursoNombre;

        [DataMember(Order = 1)]
        public String CodCurso
        {
            get { return _CodCurso; }
            set { _CodCurso = value; }
        }

        [DataMember(Order = 2)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 3)]
        public String CursoNombre
        {
            get { return _CursoNombre; }
            set { _CursoNombre = value; }
        }
    }
}
