using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNInasistencia
    {
        #region Singleton
        private static LNInasistencia _Instancia;
        private LNInasistencia() { }
        public static LNInasistencia Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNInasistencia();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos
        public DC.InasistenciaCollectionDC ObtenerInasistencias(string codUsuario)
        {
            DC.InasistenciaCollectionDC Inasistencias;
            string errMensaje = "";
            try
            {
                Collection<DC.InasistenciaDC> lstInasistencia = DA.DAInasistencia.Instancia.ObtenerInasistencias(codUsuario, ref errMensaje);
                if (lstInasistencia.Count <= 0)
                {
                    Inasistencias = new DC.InasistenciaCollectionDC();
                    Inasistencias.CodError = "00031";
                    Inasistencias.MsgError = "Ud. no se encuentra matriculado en el presente ciclo.";
                    return Inasistencias;
                }
                else
                {
                    Inasistencias = new DC.InasistenciaCollectionDC();

                    //var s = lstInasistencia.GroupBy(x => x.Fecha).Select(lg => new DC.HorarioDiaDC() { Clases = lg.ToList(), Fecha = lg.Key, CodAlumno = codUsuario, CodDia = int.Parse(lg.Key) });

                    Inasistencias.CodError = "00000";
                    Inasistencias.MsgError = "";
                    Inasistencias.Inasistencias = lstInasistencia;
                    return Inasistencias;
                }
            }
            catch (Exception ex)
            {
                Inasistencias = new DC.InasistenciaCollectionDC();
                Inasistencias.CodError = "11111";
                Inasistencias.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                return Inasistencias;
            }

        }
        #endregion
    }
}
