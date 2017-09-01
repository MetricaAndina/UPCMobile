using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class UsuarioDC
    {
        private String _Codigo;
        private String _CodigoAlumno;
        private String _Nombres;
        private String _Apellidos;
        private String _Genero;
        private String _EsAlumno;
        private String _Estado;
        private String _TipoUser;
        private String _Token;
        private DatosAcademicosUsuarioDC _datos;
        private String _CodError;
        private String _MsgError;

        [DataMember(Order = 1)]
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        [DataMember(Order = 2)]
        public String CodigoAlumno
        {
            get { return _CodigoAlumno; }
            set { _CodigoAlumno = value; }
        }

        [DataMember(Order = 3)]
        public String Nombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
        }

        [DataMember(Order = 4)]
        public String Apellidos
        {
            get { return _Apellidos; }
            set { _Apellidos = value; }
        }

        [DataMember(Order = 5)]
        public String Genero
        {
            get { return _Genero; }
            set { _Genero = value; }
        }

        [DataMember(Order = 6)]
        public String EsAlumno
        {
            get { return _EsAlumno; }
            set { _EsAlumno = value; }
        }

        [DataMember(Order = 7)]
        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        [DataMember(Order = 8)]
        public String TipoUser
        {
            get { return _TipoUser; }
            set { _TipoUser = value; }
        }

        [DataMember(Order = 9)]
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        [DataMember(Order = 10)]
        public DatosAcademicosUsuarioDC Datos
        {
            get { return _datos; }
            set { _datos = value; }
        }

        [DataMember(Order = 11)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 12)]
        public String MsgError
        {
            get { return _MsgError; }
            set { _MsgError = value; }
        }
    }

    [DataContract]
    public class DatosAcademicosUsuarioDC
    {
        private String _cod_linea;
        private String _dsc_linea;
        private String _cod_modal;
        private String _dsc_modal;
        private String _cod_sede;
        private String _dsc_sede;
        private String _ciclo;

        [DataMember(Order = 1)]
        public String CodLinea
        {
            get { return _cod_linea; }
            set { _cod_linea = value; }
        }

        [DataMember(Order = 2)]
        public String DscLinea
        {
            get { return _dsc_linea; }
            set { _dsc_linea = value; }
        }

        [DataMember(Order = 3)]
        public String CodModal
        {
            get { return _cod_modal; }
            set { _cod_modal = value; }
        }

        [DataMember(Order = 4)]
        public String DscModal
        {
            get { return _dsc_modal; }
            set { _dsc_modal = value; }
        }

        [DataMember(Order = 5)]
        public String CodSede
        {
            get { return _cod_sede; }
            set { _cod_sede = value; }
        }

        [DataMember(Order = 6)]
        public String DscSede
        {
            get { return _dsc_sede; }
            set { _dsc_sede = value; }
        }

        [DataMember(Order = 7)]
        public String Ciclo
        {
            get { return _ciclo; }
            set { _ciclo = value; }
        }
    }
}
