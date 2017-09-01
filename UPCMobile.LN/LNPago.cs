using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNPago
    {
        #region Singleton
        private static LNPago _Instancia;
        private LNPago() { }
        public static LNPago Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNPago();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos
        public DC.PagoPendienteCollectionDC ObtenerPagos(string codUsuario)
        {

            string gastoAdm = "";
            string porcMora = "";
            string moraDiaria = "";
            string tipoMora = "";
            string Moneda = "";
            string tipos = "";
            string comp = "";//codigo compania -- equivalencia
            string persona = "";
            string mensaje = "";
            DA.DAPago.Instancia.Parametros_Pago(codUsuario, ref gastoAdm, ref porcMora, ref moraDiaria, ref tipoMora, ref Moneda, ref tipos, ref comp, ref persona, ref mensaje);
            DC.PagoPendienteCollectionDC objPagoPendienteCollection;
            if (mensaje.Equals("") || mensaje.Equals("null"))
            {
                var lista = DA.DAPago.Instancia.ListarCuotas(Moneda, gastoAdm, porcMora, moraDiaria, tipoMora, comp, persona, codUsuario);

                if (lista.Count > 0)
                {
                    objPagoPendienteCollection = new DC.PagoPendienteCollectionDC();
                    objPagoPendienteCollection.CodError = "00000";
                    objPagoPendienteCollection.MsgError = "";
                    objPagoPendienteCollection.PagosPendientes = lista;//DA.DAPago.Instancia.ListarCuotas(Moneda, gastoAdm, porcMora, moraDiaria, tipoMora, comp, persona, codUsuario);

                }
                else
                {
                    objPagoPendienteCollection = new DC.PagoPendienteCollectionDC();
                    objPagoPendienteCollection.CodError = "00041";
                    objPagoPendienteCollection.MsgError = "Ud. no presenta deudas pendientes.";
                    objPagoPendienteCollection.PagosPendientes = lista;//DA.DAPago.Instancia.ListarCuotas(Moneda, gastoAdm, porcMora, moraDiaria, tipoMora, comp, persona, codUsuario);

                }

            }
            else {
                objPagoPendienteCollection = new DC.PagoPendienteCollectionDC();
                objPagoPendienteCollection.CodError = "00041";
                objPagoPendienteCollection.MsgError = "Ud. no presenta deudas pendientes.";
            }
            
            return objPagoPendienteCollection;
        }



        #endregion
    }
}
