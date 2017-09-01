using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class ListadoCursosDC
    {
        private String _CodError;
        private String _MsgError;
        private Collection<ModalidadDC> _modalidades;

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
        public Collection<ModalidadDC> modalidades
        {
            get { return _modalidades; }
            set { _modalidades = value; }
        }
    }

    [DataContract]
    public class ModalidadDC
    {
        private String _codigo;
        private String _descripcion;
        private String _periodo;
        private Collection<DetalleCursoDC> _cursos;

        [DataMember(Order = 1)]
        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        [DataMember(Order = 2)]
        public String descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        [DataMember(Order = 2)]
        public String periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        [DataMember(Order = 4)]
        public Collection<DetalleCursoDC> cursos
        {
            get { return _cursos; }
            set { _cursos = value; }
        }
    }

    [DataContract]
    public class DetalleCursoDC
    {
        private String _curso;
        private String _cursoId;
        private String _seccion;
        private String _grupo;

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
    }
}