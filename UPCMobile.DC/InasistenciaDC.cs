using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class InasistenciaDC
    {
        private Int64 _CodInasistencia;
        private String _CodCurso;

      
        private String _CodAlumno;
        private String _CursoNombre;
        private String _Maximo;
        private String _Total;

        [DataMember(Order = 1)]
        public String CodCurso
        {
            get { return _CodCurso; }
            set { _CodCurso = value; }
        }

        [DataMember(Order = 2)]
        public Int64 CodInasistencia
        {
            get { return _CodInasistencia; }
            set { _CodInasistencia = value; }
        }

        [DataMember(Order = 3)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 4)]
        public String CursoNombre
        {
            get { return _CursoNombre; }
            set { _CursoNombre = value; }
        }

        [DataMember(Order = 5)]
        public String Maximo
        {
            get { return _Maximo; }
            set { _Maximo = value; }
        }

        [DataMember(Order = 6)]
        public String Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
    }
}
