using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class ClaseDC
    { 
        private Int64 _CodClase;
        private String _CodAlumno;
        private String _CodCurso;
        private String _CursoNombre;
        private String _CursoNombreCorto;
        private String _Fecha;
        private String _HoraInicio;
        private String _HoraFin;
        private String _Sede;
        private String _Seccion;
        private String _Salon;

        [DataMember(Order = 1)]
        public Int64 CodClase
        {
            get { return _CodClase; }
            set { _CodClase = value; }
        }

        [DataMember(Order = 2)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 3)]
        public String CodCurso
        {
            get { return _CodCurso; }
            set { _CodCurso = value; }
        }

        [DataMember(Order = 4)]
        public String CursoNombre
        {
            get { return _CursoNombre; }
            set { _CursoNombre = value; }
        }

        [DataMember(Order = 5)]
        public String CursoNombreCorto
        {
            get { return _CursoNombreCorto; }
            set { _CursoNombreCorto = value; }
        }

        [DataMember(Order = 6)]
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        [DataMember(Order = 7)]
        public String HoraInicio
        {
            get { return _HoraInicio; }
            set { _HoraInicio = value; }
        }

        [DataMember(Order = 8)]
        public String HoraFin
        {
            get { return _HoraFin; }
            set { _HoraFin = value; }
        }

        [DataMember(Order = 9)]
        public String Sede
        {
            get { return _Sede; }
            set { _Sede = value; }
        }

        [DataMember(Order = 10)]
        public String Seccion
        {
            get { return _Seccion; }
            set { _Seccion = value; }
        }

        [DataMember(Order = 11)]
        public String Salon
        {
            get { return _Salon; }
            set { _Salon = value; }
        }

    }
}
