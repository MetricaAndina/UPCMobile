using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNTramite
    {
        #region Singleton
        private static LNTramite _Instancia;
        private LNTramite() { }
        public static LNTramite Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNTramite();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos

        public DC.TramiteRealizadoCollectionDC getTramites(string codUsuario)
        {
            string error = "";

            Collection<DC.TramiteRealizadoDC> lista = DA.DATramite.Instancia.getTramites(codUsuario, ref error);

            DC.TramiteRealizadoCollectionDC tramites;
            try
            {

                if (lista.Count <= 0)
                {
                    tramites = new DC.TramiteRealizadoCollectionDC();
                    tramites.CodError = "00051";
                    tramites.MsgError = "Ud. no presenta trámites en este ciclo.";

                }
                else
                {
                    tramites = new DC.TramiteRealizadoCollectionDC();
                    tramites.CodError = "00000";
                    tramites.MsgError = "";
                    tramites.TramitesRealizados = lista;
                }
            }
            catch (Exception ex)
            {
                tramites = new DC.TramiteRealizadoCollectionDC();
                tramites.CodError = "11111";
                tramites.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
            }
            return tramites;
        }

        #endregion
    }
}
