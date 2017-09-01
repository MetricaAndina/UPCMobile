using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class NotaCollectionDC
    {
        private String _CodError;
        private String _MsgError;
        private String _CursoNombre;// ok
        private String _CodCurso; // ok 
        private String _Formula; // ok

        private String _PorcentajeAvance;
        private String _NotaFinal;


        private List<NotaDC> _Notas;

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
        public String CursoNombre
        {
            get { return _CursoNombre; }
            set { _CursoNombre = value; }
        }

        [DataMember(Order = 4)]
        public String CodCurso
        {
            get { return _CodCurso; }
            set { _CodCurso = value; }
        }

        [DataMember(Order = 5)]
        public String Formula
        {
            get { return _Formula; }
            set { _Formula = value; }
        }

        [DataMember(Order = 6)]
        public String PorcentajeAvance
        {
            get { return _PorcentajeAvance; }
            set { _PorcentajeAvance = value; }
        }

        [DataMember(Order = 7)]
        public String NotaFinal
        {
            get { return _NotaFinal; }
            set { _NotaFinal = value; }
        }

        [DataMember(Order = 8)]
        public List<NotaDC> Notas
        {
            get { return _Notas; }
            set { _Notas = value; }
        }
    }
}
