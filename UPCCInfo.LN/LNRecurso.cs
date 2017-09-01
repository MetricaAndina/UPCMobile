using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPCCInfo.DC;

namespace UPCCInfo.LN
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

        #region Metodos
                
        public DC.DCReservaRecurso ReservarRecurso(string sCodRecurso,string sNomRecurso,string sCodAlumno, string sCanHoras, string sfecIni, string sfecFin, string sToken)
        {
            int resultado = 0;
            DC.DCReservaRecurso  objReservaRecurso;
            string mensaje;
            try
            {
                //resultado = DA.DARecurso.Instancia.ReservarRecurso(sCodRecurso, "2", sCodAlumno, "U", sCanHoras, "26092012", "19:00", "21:00"); 
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken)) {
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "0000" + resultado, CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, Mensaje = "Token no es valido. transacción no procesada" };
                } else {
                    resultado = DA.DARecurso.Instancia.ReservarRecurso(sCodRecurso, "2", sCodAlumno, "U", sCanHoras, sfecIni, sfecIni, sfecFin);
                    mensaje = LNUtil.Instancia.MensajeReservaResult(resultado, sNomRecurso, sfecIni, sfecIni);
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "0000" + resultado, CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, Mensaje = mensaje };

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
                    listaRecurso.Mensaje = "Token no es valido , no se puede procesar transacción";
                    listaRecurso.Recursos = null;
                }
                else {
                    lstRecurso = DA.DARecurso.Instancia.ListaRecursos(sTipoRecurso, sLocal, sFecIni, sFecFin, ref Mensaje);

                    if (lstRecurso.Count == 0)
                    {
                        listaRecurso.CodError = "00001";
                        listaRecurso.Mensaje = "No hay recursos disponibles para reservar.";
                        listaRecurso.Recursos = null;
                    }
                    else
                    {

                        listaRecurso.CodError = "00000";
                        listaRecurso.Mensaje = "";
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

        public DC.DCReservaRecurso ActivarReserva(string sCodRecurso, string sCodAlumno,string sToken)
        {
            DC.DCReservaRecurso objReservaRecurso;
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken)) {
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "0000", CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, Mensaje = "Token no es valido. transacción no procesada" };
                } else {
                    
                    objReservaRecurso = DA.DARecurso.Instancia.ActivarReserva(sCodRecurso, sCodAlumno, sToken);
                }
                return objReservaRecurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DC.DCReservaRecurso VerificaReserva(string sCodRecurso, string sCodAlumno, string sToken)
        {
            DC.DCReservaRecurso objReservaRecurso;
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    objReservaRecurso = new DC.DCReservaRecurso() { CodError = "0000", CodUsuario = sCodAlumno, CodRecurso = sCodRecurso, Mensaje = "Token no es valido. transacción no procesada" };
                }
                else
                {

                    objReservaRecurso = DA.DARecurso.Instancia.VerificaReserva(sCodRecurso, sCodAlumno, sToken);
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
                    listaRecurso.Mensaje = "Token invalido, no se puede procesar transacción";
                    listaRecurso.Recursos = null;
                }
                else {
                    lstRecurso = DA.DARecurso.Instancia.ListaRecursos(sTipoRecurso, sLocal, sFecIni, sFecFin, ref Mensaje);

                    if (lstRecurso.Count == 0)
                    {
                        listaRecurso.CodError = "00001";
                        listaRecurso.Mensaje = "No hay recursos disponibles para reservar.";
                        listaRecurso.Recursos = null;
                    }
                    else
                    {

                        listaRecurso.CodError = "00000";
                        listaRecurso.Mensaje = "";
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
                    listaReservaAlumno.Mensaje = "Tokenn invalido, no se puede procesar transacción";
                    listaReservaAlumno.Reservas = null;
                }
                else {
                    lstReserva = DA.DARecurso.Instancia.ListaReservaAlumno(sFecIni, sFecFin, sCodAlumno, ref Mensaje);

                    if (lstReserva.Count == 0)
                    {
                        listaReservaAlumno.CodError = "00001";
                        listaReservaAlumno.Mensaje = "No se han registrado reservas durante esta semana.";
                        listaReservaAlumno.Reservas = null;
                    }
                    else
                    {

                        listaReservaAlumno.CodError = "00000";
                        listaReservaAlumno.Mensaje = "";
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
