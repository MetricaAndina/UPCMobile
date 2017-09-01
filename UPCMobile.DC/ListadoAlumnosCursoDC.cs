using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class ListadoAlumnosCursoDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<CursosDC> _Cursos;

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
        public Collection<CursosDC> Cursos
        {
            get { return _Cursos; }
            set { _Cursos = value; }
        }
    }

    [DataContract]
    public class CursosDC
    {
        private String _curso;
        private String _cursoId;
        private String _seccion;
        private String _grupo;
        private Collection<CompaneroDC> _alumnos;

        [DataMember(Order = 1)]
        public String curso
        {
            get { return _curso; }
            set { _curso = value; }
        }

        [DataMember(Order = 2)]
        public String cursoId
        {
            get { return _cursoId; }
            set { _cursoId = value; }
        }

        [DataMember(Order = 3)]
        public String seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        [DataMember(Order = 4)]
        public String grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        [DataMember(Order = 5)]
        public Collection<CompaneroDC> alumnos
        {
            get { return _alumnos; }
            set { _alumnos = value; }
        }
    }
}
