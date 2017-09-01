using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace UPCMobile.DC
{
    [DataContract]
    public class HorarioDiaDC
    {
        private int _CodDia;
        private String _CodAlumno;
        private String _Fecha;
        private List<ClaseDC> _Clases;

        [DataMember(Order = 1)]
        public int CodDia
        {
            get { return _CodDia; }
            set { _CodDia = value; }
        }

        [DataMember(Order = 2)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 3)]
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        [DataMember(Order = 4)]
        public List<ClaseDC> Clases
        {
            get { return _Clases; }
            set { _Clases = value; }
        }
    }
}
