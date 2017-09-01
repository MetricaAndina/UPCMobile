using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UPCCInfo.SC;
using UPCCInfo.DC;
using UPCCInfo.LN;

namespace UPCCInfo.Impl
{
   public class UPCCinfoImp : IUPCCinfo
    {
        public DCAlumno AlumnoAutenticarPost(Aut sAut)
        //public DCAlumno AlumnoAutenticarPost(string sparametro1)
        {
           // Aut sAut=null;
            DCAlumno objAlumnoDC;
            
            //string sCodAlumno = sAut.Usuario.ToUpper();
            string sCodAlumno = sAut.Usuario.ToUpper();
            string contrasena = sAut.Contrasena;
            string sToken = "";

            try
            {

                if (sCodAlumno.Length <= 6)
                {
                    objAlumnoDC = new DCAlumno();
                    objAlumnoDC.CodError = "00001";
                    objAlumnoDC.MsgError = "Los datos ingresados no son correctos.";
                    return objAlumnoDC;
                }


                string scad = sCodAlumno.Substring(sCodAlumno.Length - 1 - 5);
                Int64 isNum;

                if (!Int64.TryParse(scad, out isNum))
                {
                    objAlumnoDC = new DCAlumno();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Su perfil de usuario no es soportado por la aplicación.";
                }
                else
                {

                    //objAlumnoDC = LNAlumno.Instancia.AutenticarAlumno(sCodAlumno, sAut.Contrasena, 'A');
                    objAlumnoDC = LNAlumno.Instancia.AutenticarAlumno(sCodAlumno, contrasena, 'A');
                    if (objAlumnoDC != null && objAlumnoDC.CodError != null)
                    {
                        if (objAlumnoDC.CodError.Equals("00000"))
                        {
                            //sToken = LNAlumno.Instancia.GenerarAlumnoToken(sCodAlumno, sAut.Contrasena, 'A');
                            sToken = LNAlumno.Instancia.GenerarAlumnoToken(sCodAlumno, contrasena, 'A');
                            objAlumnoDC.Token = sToken;
                        }
                    }
                    else
                    {

                        objAlumnoDC = new DCAlumno();
                        objAlumnoDC.CodError = "00002";
                        objAlumnoDC.MsgError = "Ud. no se encuentra matriculado en el presente ciclo.";
                    }
                }
                return objAlumnoDC;
            }
            catch (Exception ex)
            {
                try
                {
                    //LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                    //                                        DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objAlumnoDC = new DCAlumno();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                    return objAlumnoDC;
                }
                catch (Exception ex2)
                {
                    objAlumnoDC = new DCAlumno();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                    return objAlumnoDC;
                }
            }
        }
        
        public DCReservaRecurso ReservarRecurso(string sCodRecurso, string sNomRecurso, string sCodAlumno, string sCanHoras, string sfecIni, string sfecFin, string sToken)
        {
            DCReservaRecurso objReservaRecurso;
            try
            {
                objReservaRecurso = LNRecurso.Instancia.ReservarRecurso(sCodRecurso, sNomRecurso, sCodAlumno, sCanHoras, sfecIni, sfecFin, sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objReservaRecurso = new DCReservaRecurso();
                objReservaRecurso.CodError = "11111";
                objReservaRecurso.Mensaje = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";                
            }
            return objReservaRecurso;
        }

        public DCListaRecurso ListadoRecursos(string sTipoRecurso, string sLocal, string sFecIni, string sCanHoras, string sFechaFin, string sCodAlumno, string sToken)
        {
            DCListaRecurso objListaRecurso;
            try
            {
                objListaRecurso = LNRecurso.Instancia.ListaRecursos(sTipoRecurso, sLocal, sFecIni, sFechaFin, sCanHoras, sCodAlumno, sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objListaRecurso = new DCListaRecurso();
                objListaRecurso.CodError = "11111";
                objListaRecurso.Mensaje = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objListaRecurso;
        }

        public DCReservaRecurso ActivarReserva(string sCodRecurso, string sCodAlumno, string sToken)
        {
            {
                DCReservaRecurso objReservaRecurso;
                try
                {
                    objReservaRecurso = LNRecurso.Instancia.ActivarReserva(sCodRecurso,sCodAlumno,  sToken);
                }
                catch (Exception ex)
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                                DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objReservaRecurso = new DCReservaRecurso();
                    objReservaRecurso.CodError = "11111";
                    objReservaRecurso.Mensaje = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                }
                return objReservaRecurso;
            }
        }

        public DCReservaRecurso VerificaReserva(string sCodRecurso, string sCodAlumno, string sToken)
        {
            {
                DCReservaRecurso objReservaRecurso;
                try
                {
                    objReservaRecurso = LNRecurso.Instancia.VerificaReserva(sCodRecurso, sCodAlumno, sToken);
                }
                catch (Exception ex)
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                                DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objReservaRecurso = new DCReservaRecurso();
                    objReservaRecurso.CodError = "11111";
                    objReservaRecurso.Mensaje = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                }
                return objReservaRecurso;
            }
        }

        public DCListaRecurso RandomRecursoDisp(string sTipoRecurso, string sLocal, string sFecIni, string sCanHoras, string sFechaFin, string sCodAlumno, string sToken)
        {
            DCListaRecurso objListaRecurso;
            try
            {
                objListaRecurso = LNRecurso.Instancia.RandomRecursoDisp(sTipoRecurso, sLocal, sFecIni, sFechaFin, sCanHoras,sCodAlumno, sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objListaRecurso = new DCListaRecurso();
                objListaRecurso.CodError = "11111";
                objListaRecurso.Mensaje = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objListaRecurso;
        }

        public DCListaReservaAlumno ReservasAlumno(string sFecIni, string sFechaFin, string sCodAlumno, string sToken)
        {
            DCListaReservaAlumno objListaReservaAlumno;
            try
            {
                objListaReservaAlumno = LNRecurso.Instancia.ListaReservaAlumno(sFecIni, sFechaFin, sCodAlumno, sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objListaReservaAlumno = new DCListaReservaAlumno();
                objListaReservaAlumno.CodError = "11111";
                objListaReservaAlumno.Mensaje = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objListaReservaAlumno;
        }
    }
}
