using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UPCMobile.DC;
namespace UPCMobile.LN
{
    public class LNRecurso
    {
        #region Singleton
        private static LNRecurso _Instancia;
        private LNRecurso() { }
        public static LNRecurso Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNRecurso();

                return _Instancia;
            }
        }
        #endregion

        #region SS-2013-075

        public EspacioDeportivoCollectionDC PoblarEspaciosDeportivos(String sCodAlumno)
        {
            DC.EspacioDeportivoCollectionDC espacios;
            string errMensaje = "";

            try
            {
                Collection<DC.SedeDC> lstSedes = DA.DARecurso.Instancia.PoblarEspaciosDeportivos(sCodAlumno, ref errMensaje);
                if (lstSedes.Count <= 0)
                {
                    espacios = new DC.EspacioDeportivoCollectionDC();
                    espacios.CodError = "00002";
                    espacios.MsgError = errMensaje;
                    return espacios;
                }
                else
                {
                    espacios = new DC.EspacioDeportivoCollectionDC();

                    espacios.Sedes = lstSedes;
                    espacios.CodError = "00000";
                    espacios.MsgError = "";

                    return espacios;
                }
            }
            catch (Exception ex)
            {
                espacios = new DC.EspacioDeportivoCollectionDC();
                espacios.CodError = "11111";
                espacios.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return espacios;
            }
        }

        public DiasLibresEDCollectionDC DisponibilidadEspacioDeportivo(String sCodSede, String sCodED, String sNumHoras, String sCodAlumno, String sFechaIni, String sFechaFin)
        {
            DC.DiasLibresEDCollectionDC horario;
            string errMensaje = "";

            try
            {
                Collection<DC.DiaLibreEDDC> lstDias = DA.DARecurso.Instancia.DisponibilidadEspacioDeportivo(sCodSede, sCodED, sNumHoras, sCodAlumno, sFechaIni, sFechaFin, ref errMensaje);
                if (lstDias.Count <= 0)
                {
                    horario = new DC.DiasLibresEDCollectionDC();
                    horario.CodError = "00002";
                    horario.MsgError = errMensaje;
                    return horario;
                }
                else
                {
                    horario = new DC.DiasLibresEDCollectionDC();

                    horario.HorarioDia = lstDias;
                    horario.CodError = "00000";
                    horario.MsgError = "";

                    return horario;
                }
            }
            catch (Exception ex)
            {
                horario = new DC.DiasLibresEDCollectionDC();
                horario.CodError = "11111";
                horario.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return horario;
            }
        }

        public ReservaEspacioDepDC ReservarEspacioDeportivo(String sCodSede, String sCodED, String sCodActiv, String sNumHoras, String sCodAlumno, String sFecha, String sHoraIni, String sHoraFin, String sDetalles)
        {
            DC.ReservaEspacioDepDC reserva;
            string errMensaje = "";

            try
            {
                reserva = DA.DARecurso.Instancia.ReservarEspacioDeportivo(sCodSede, sCodED, sCodActiv, sNumHoras, sCodAlumno, sFecha, sHoraIni, sHoraFin, sDetalles, ref errMensaje);
                return reserva;
            }
            catch (Exception ex)
            {
                reserva = new DC.ReservaEspacioDepDC();
                reserva.CodError = "11111";
                reserva.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return reserva;
            }
        }

        #endregion 

        #region Metodos
        public DC.DCReservaRecurso ReservarRecurso(string sCodRecurso,string sNomRecurso,string sCodAlumno, string sCanHoras, string sfecIni, string sfecFin, string sToken)
        {
            DCReservaRecurso oDCReservaRecurso = new DCReservaRecurso();
            int resultado = 0;
            DC.DCReservaRecurso  objReservaRecurso;
            string mensaje;
            try
            {
                //resultado = DA.DARecurso.Instancia.ReservarRecurso(sCodRecurso, "2", sCodAlumno, "U", sCanHoras, "26092012", "19:00", "21:00"); 
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken)) {
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00001" + resultado, CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, MsgError = "Token no es valido. transacción no procesada" };
                } else {
                    //resultado = DA.DARecurso.Instancia.ReservarRecurso(sCodRecurso, "2", sCodAlumno, "U", sCanHoras, sfecIni, sfecIni, sfecFin);
                    oDCReservaRecurso = DA.DARecurso.Instancia.ReservarRecurso(sCodRecurso, "2", sCodAlumno, "U", sCanHoras, sfecIni, sfecIni, sfecFin);
                    if (oDCReservaRecurso.CodReserva.ToString()=="0")// no devolvio reserva producto de error
                      {
                          //mensaje = LNUtil.Instancia.MensajeReservaResult(resultado, sNomRecurso, sfecIni, sfecIni);
                          mensaje = LNUtil.Instancia.MensajeReservaResult(int.Parse(oDCReservaRecurso.CodError), sNomRecurso, sfecIni, sfecIni);
                          mensaje += oDCReservaRecurso.MsgError;
                          objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00002", CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, MsgError = mensaje, CodReserva = oDCReservaRecurso.CodReserva };
                        }
                    else
                        {
                            mensaje = LNUtil.Instancia.MensajeReservaResult(resultado, sNomRecurso, sfecIni, sfecIni);
                            mensaje = LNUtil.Instancia.MensajeReservaResult(int.Parse(oDCReservaRecurso.CodError), sNomRecurso, sfecIni, sfecIni);
                            objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00000", CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, MsgError = mensaje, CodReserva = oDCReservaRecurso.CodReserva };
                        }
                }




               
                return objReservaRecurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DC.DCListaRecurso ListaRecursos(string sTipoRecurso, string sLocal, string sFecIni, string sFecFin,string canHoras,string sCodAlumno,string sToken)
        {
            DC.DCListaRecurso listaRecurso = new DC.DCListaRecurso();
            List<DC.DCRecurso> lstRecurso;
            string Mensaje = "";
            string format = "";
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    listaRecurso.CodError = "00001";
                    listaRecurso.MsgError = "Token no es valido , no se puede procesar transacción";
                    listaRecurso.Recursos = null;
                }
                else {
                    lstRecurso = DA.DARecurso.Instancia.ListaRecursos(sTipoRecurso, sLocal, sFecIni, sFecFin, canHoras, sCodAlumno, ref Mensaje);

                    if (lstRecurso.Count == 0)
                    {
                        listaRecurso.CodError = "00002";
                        listaRecurso.MsgError = "No hay recursos disponibles para reservar.";
                        listaRecurso.Recursos = null;
                    }
                    else
                    {

                        listaRecurso.CodError = "00000";
                        listaRecurso.MsgError = "";
                        listaRecurso.TipoRecurso = sTipoRecurso;
                        listaRecurso.FecReserva = sFecIni;
                        listaRecurso.CanHoras = canHoras;
                        listaRecurso.Recursos = lstRecurso;
                    }
                }

                
            }
            catch (Exception ex)
            {   
                throw ex;
            }
            return listaRecurso;
        }

        public DC.DCReservaRecurso ActivarReserva(string sCodRecurso, string sCodAlumno, string sCodAlumno2, string sToken)
        {
            DC.DCReservaRecurso objReservaRecurso;
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken)) {
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00001", CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, MsgError = "Token no es valido. transacción no procesada" };
                } else {
                    
                    objReservaRecurso = DA.DARecurso.Instancia.ActivarReserva(sCodRecurso, sCodAlumno, sCodAlumno2, sToken);
                }
                return objReservaRecurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DC.DCReservaRecurso VerificaReserva(string sCodReserva,string sCodRcurso ,string sCodAlumno, string sToken)
        {
            DC.DCReservaRecurso objReservaRecurso;
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00001", CodUsuario = sCodAlumno, CodReserva = sCodReserva, CodRecurso = sCodRcurso, MsgError = "Token no es valido. transacción no procesada" };
                }
                else
                {
                    objReservaRecurso = DA.DARecurso.Instancia.VerificaReserva(sCodReserva, sCodAlumno, sToken);
                    if (objReservaRecurso.CodRecurso == sCodRcurso)
                    {
                        objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00000", CodUsuario = sCodAlumno, CodReserva = sCodReserva, CodRecurso = sCodRcurso, MsgError = "El recurso SI corresponde a la reserva realizada" };
                    }
                    else
                    {
                        objReservaRecurso = new DC.DCReservaRecurso() { CodError = "00002", CodUsuario = sCodAlumno, CodReserva = sCodReserva, CodRecurso = sCodRcurso, MsgError = "El recurso NO corresponde a la reserva realizada" };
                    }
                }
                return objReservaRecurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DC.DCListaRecurso RandomRecursoDisp(string sTipoRecurso, string sLocal, string sFecIni, string sFecFin, string canHoras, string sCodAlumno, string sToken)
        {
            DC.DCListaRecurso listaRecurso = new DC.DCListaRecurso();
            List<DC.DCRecurso> lstRecurso;
            string Mensaje = "";
            string format = "";
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    listaRecurso.CodError = "00001";
                    listaRecurso.MsgError = "Token invalido, no se puede procesar transacción";
                    listaRecurso.Recursos = null;
                }
                else {
                    lstRecurso = DA.DARecurso.Instancia.ListaRecursos(sTipoRecurso, sLocal, sFecIni, sFecFin, canHoras, sCodAlumno, ref Mensaje);

                    if (lstRecurso.Count == 0)
                    {
                        listaRecurso.CodError = "00002";
                        listaRecurso.MsgError = "No hay recursos disponibles para reservar.";
                        listaRecurso.Recursos = null;
                    }
                    else
                    {

                        listaRecurso.CodError = "00000";
                        listaRecurso.MsgError = "";
                        listaRecurso.TipoRecurso = sTipoRecurso;
                        listaRecurso.FecReserva = sFecIni;
                        listaRecurso.CanHoras = canHoras;

                        Random rand = new Random();
                        int x = rand.Next(0, lstRecurso.Count - 1);
                        //listaRecurso.Recursos.Add(lstRecurso[x]);
                        listaRecurso.Recursos = new List<DC.DCRecurso>() { lstRecurso[x] };
                    }                
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaRecurso;
        }

        public DC.DCListaReservaAlumno ListaReservaAlumno(string sFecIni, string sFecFin, string sCodAlumno,string  sToken)
        {
            DC.DCListaReservaAlumno listaReservaAlumno = new DC.DCListaReservaAlumno();
            List<DC.DCReservaAlumno> lstReserva;
            string Mensaje = "";
            string format = "";
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    listaReservaAlumno.CodError = "00001";
                    listaReservaAlumno.MsgError = "Tokenn invalido, no se puede procesar transacción";
                    listaReservaAlumno.Reservas = null;
                }
                else {
                    lstReserva = DA.DARecurso.Instancia.ListaReservaAlumno(sFecIni, sFecFin, sCodAlumno, ref Mensaje);

                    if (lstReserva.Count == 0)
                    {
                        listaReservaAlumno.CodError = "00002";
                        listaReservaAlumno.MsgError = "No se han registrado reservas durante esta semana.";
                        listaReservaAlumno.Reservas = null;
                    }
                    else
                    {

                        listaReservaAlumno.CodError = "00000";
                        listaReservaAlumno.MsgError = "";
                        listaReservaAlumno.CodAlumno = sCodAlumno;
                        listaReservaAlumno.FecIni = sFecIni;
                        listaReservaAlumno.FecFin = sFecFin;

                        listaReservaAlumno.Reservas = lstReserva;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaReservaAlumno;
        } 
        
        #endregion 
    }
}
