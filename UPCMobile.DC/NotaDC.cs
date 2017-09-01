using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace UPCMobile.DC
{
    [DataContract]
    public class NotaDC
    {
        private int _CodNota;// no hay
        private String _CodCurso;//hay
        private String _CodAlumno;//hay

        private String _NomCurso;
        public String NomCurso
        {
            get { return _NomCurso; }
            set { _NomCurso = value; }
        }
        private String _Formula;
        public String Formula
        {
            get { return _Formula; }
            set { _Formula = value; }
        }


        private String _NombreEvaluacion;//hay di
        private String _NombreCorto;//nemonico ci
        private Int16 _NroEvaluacion;// hay
        private String _Peso;//hay
        private String _Valor;//si hay

        [DataMember(Order = 1)]
        public int CodNota
        {
            get { return _CodNota; }
            set { _CodNota = value; }
        }

        [DataMember(Order = 2)]
        public String CodCurso
        {
            get { return _CodCurso; }
            set { _CodCurso = value; }
        }

        [DataMember(Order = 3)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 4)]
        public String NombreEvaluacion
        {
            get { return _NombreEvaluacion; }
            set { _NombreEvaluacion = value; }
        }

        [DataMember(Order = 5)]
        public String NombreCorto
        {
            get { return _NombreCorto; }
            set { _NombreCorto = value; }
        }

        [DataMember(Order = 6)]
        public Int16 NroEvaluacion
        {
            get { return _NroEvaluacion; }
            set { _NroEvaluacion = value; }
        }

        [DataMember(Order = 7)]
        public String Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }

        [DataMember(Order = 8)]
        public String Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }
}
