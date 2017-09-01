using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class AlumnoCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private String _CodCurso;
        private String _DescCurso;
        private String _CodSeccion;
        private Collection<CompaneroDC> _Companeros;

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
        public String cursoId
        {
            get { return _CodCurso; }
            set { _CodCurso = value; }
        }

        [DataMember(Order = 4)]
        public String curso
        {
            get { return _DescCurso; }
            set { _DescCurso = value; }
        }

        [DataMember(Order = 5)]
        public String seccion
        {
            get { return _CodSeccion; }
            set { _CodSeccion = value; }
        }

        [DataMember(Order = 6)]
        public Collection<CompaneroDC> alumnos
        {
            get { return _Companeros; }
            set { _Companeros = value; }
        }
    }

    [DataContract]
    public class CompaneroDC
    {
        private String _Nombre;
        private String _Codigo;
        private String _Foto;
        private String _Carrera;

        [DataMember(Order = 1)]
        public String nombre_completo
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        [DataMember(Order = 2)]
        public String codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        [DataMember(Order = 3)]
        public String url_foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }

        [DataMember(Order = 4)]
        public String carrera_actual
        {
            get { return _Carrera; }
            set { _Carrera = value; }
        }
    }
}
