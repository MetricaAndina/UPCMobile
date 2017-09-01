using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNExcepcion
    {
        #region Singleton
        private static LNExcepcion _Instancia;
        private LNExcepcion() { }
        public static LNExcepcion Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNExcepcion();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos

        public void RegistrarExcepcion(int flag,
                                       String sCodAlumno, String sToken,
                                       String sMensaje, String sStacktrace,
                                       String sFecha, String sHora, Char cPlataforma)
        {
            DA.DAExcepcion.Instancia.RegistrarExcepcion(flag, sCodAlumno, sToken, sMensaje,
                                                        sStacktrace, sFecha, sHora, cPlataforma);
        }


        #endregion
    }
}
