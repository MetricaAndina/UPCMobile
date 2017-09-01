using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class AlumnoDC
    {

        private String _CodAlumno;//LISTO INTEGRO O NEMONICO
        private String _CodError;//DEPENDE
        private String _MsgError;//DEPENDE
        private String _Nombres;//HAY
        private String _Apellidos;//HAY
        private Char _Genero;//HAY
        private string _EsAlumno;

        public string EsAlumno
        {
            get { return _EsAlumno; }
            set { _EsAlumno = value; }
        }
        
        private String _Token;//NO HAY
        private Char _Estado;//HAY

        [DataMember(Order = 1)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 2)]
        public String CodError
        {
            get { return _CodError; }
            set { _CodError = value; }
        }

        [DataMember(Order = 3)]
        public String MsgError
        {
            get { return _MsgError; }
            set { _MsgError = value; }
        }

        [DataMember(Order = 4)]
        public String Nombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
        }

        [DataMember(Order = 5)]
        public String Apellidos
        {
            get { return _Apellidos; }
            set { _Apellidos = value; }
        }

        [DataMember(Order = 6)]
        public Char Genero
        {
            get { return _Genero; }
            set { _Genero = value; }
        }


        [DataMember(Order = 7)]
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        [DataMember(Order = 8)]
        public Char Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }



    }
}
