using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPCMobile.SC;
using UPCMobile.DC;
using UPCMobile.LN;

namespace UPCMobile.Impl
{
    public class UPCMobileImp : IUPCMobile
    {

        #region Listos
        public DC.HorarioDiaCollectionDC HorarioDiaListar(string sCodAlumno, string sToken)
        {
            HorarioDiaCollectionDC ObjHorarioDiaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                    ObjHorarioDiaCollectionDC.CodError = "00003";
                    ObjHorarioDiaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else {
                    ObjHorarioDiaCollectionDC = LNHorario.Instancia.ObtenerHorario(sCodAlumno);
                }

                return ObjHorarioDiaCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                    ObjHorarioDiaCollectionDC.CodError = "11111";
                    ObjHorarioDiaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return ObjHorarioDiaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                    ObjHorarioDiaCollectionDC.CodError = "11111";
                    ObjHorarioDiaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return ObjHorarioDiaCollectionDC;
                }
            }

        }

        public InasistenciaCollectionDC InasistenciaListar(string sCodAlumno, string sToken)
        {
            InasistenciaCollectionDC ObjInasistenciaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {

                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "00003";
                    ObjInasistenciaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else {
                    ObjInasistenciaCollectionDC = LNInasistencia.Instancia.ObtenerInasistencias(sCodAlumno);
                }

                return ObjInasistenciaCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');

                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "11111";
                    ObjInasistenciaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return ObjInasistenciaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "11111";
                    ObjInasistenciaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return ObjInasistenciaCollectionDC;
                }
            }
        }

        public CursoAlumnoCollectionDC CursoAlumnoListar(string sCodAlumno, string sToken)
        {
            CursoAlumnoCollectionDC ObjCursoAlumnoCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken)) {
                    ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                    ObjCursoAlumnoCollectionDC.CodError = "00003";
                    ObjCursoAlumnoCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else {
                    ObjCursoAlumnoCollectionDC = LNCurso.Instancia.ObtenerCursos(sCodAlumno);
                }
                return ObjCursoAlumnoCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                    ObjCursoAlumnoCollectionDC.CodError = "11111";
                    ObjCursoAlumnoCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return ObjCursoAlumnoCollectionDC;
                }
                catch (Exception ex2)
                {   
                    ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                    ObjCursoAlumnoCollectionDC.CodError = "11111";
                    ObjCursoAlumnoCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return ObjCursoAlumnoCollectionDC;
                }
                
            }
        }

        public NotaCollectionDC NotaListar(string sCodAlumno, string sCodCurso, string sToken)
        {
            NotaCollectionDC ObjNotaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "00003";
                    ObjNotaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    ObjNotaCollectionDC = LNNota.Instancia.getNotas(sCodAlumno, sCodCurso);
                     
                }

                return ObjNotaCollectionDC;
            }
            catch (Exception ex)
            {

                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "11111";
                    ObjNotaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return ObjNotaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "11111";
                    ObjNotaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return ObjNotaCollectionDC;
                }
            }
        }

        public TramiteRealizadoCollectionDC TramiteRealizadoListar(string sCodAlumno, string sToken)
        {
            TramiteRealizadoCollectionDC ObjTramiteRealizadoCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                    ObjTramiteRealizadoCollectionDC.CodError = "00003";
                    ObjTramiteRealizadoCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else {
                    ObjTramiteRealizadoCollectionDC = LNTramite.Instancia.getTramites(sCodAlumno);
                }

                return ObjTramiteRealizadoCollectionDC;
            }
            catch (Exception ex)
            {
                  try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                    ObjTramiteRealizadoCollectionDC.CodError = "11111";
                    ObjTramiteRealizadoCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return ObjTramiteRealizadoCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                    ObjTramiteRealizadoCollectionDC.CodError = "11111";
                    ObjTramiteRealizadoCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return ObjTramiteRealizadoCollectionDC;
                }
            }
        }

        public ExcepcionDispositivoDC ExcepcionDispositivoRegistrar(string sCodAlumno, string sToken, 
                                                                    string sMensaje, string sStacktrace, 
                                                                    string sFecha, string sHora, 
                                                                    char cPlataforma)
        {
            ExcepcionDispositivoDC ObjExcepcionDispositivoDC = new ExcepcionDispositivoDC();
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                ObjExcepcionDispositivoDC = new ExcepcionDispositivoDC();
                ObjExcepcionDispositivoDC.CodError = "00000";
                ObjExcepcionDispositivoDC.MsgError = "";

                LNExcepcion.Instancia.RegistrarExcepcion(1,sCodAlumno, sToken, sMensaje, sStacktrace, sFecha, sHora, cPlataforma);
                return ObjExcepcionDispositivoDC;
            }
            catch (Exception ex)
            {
                ObjExcepcionDispositivoDC = new ExcepcionDispositivoDC();
                ObjExcepcionDispositivoDC.CodError = "00061";
                ObjExcepcionDispositivoDC.MsgError = "Ocurrió un error al registrar la excepción, por favor comuníquelo a IT Service al anexo 7799 (Cód. 300).";
                return ObjExcepcionDispositivoDC;
            }
        }

        public AlumnoDC AlumnoLogoff(string sCodAlumno, string sToken)
        {
            AlumnoDC objAlumnoDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {

                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "00003";
                    objAlumnoDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else {
                    LNAlumno.Instancia.CerrarSession(sCodAlumno);
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "99999";
                    objAlumnoDC.MsgError = "";
                }

                return objAlumnoDC;

            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 202).";
                    return objAlumnoDC;
                }
                catch (Exception ex2)
                {
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 202).";
                    return objAlumnoDC;
                }
            }
        }
        #endregion
        
        #region Pendientes
        
        public PagoPendienteCollectionDC PagoPendienteListar(string sCodAlumno, string sToken)
        {
            PagoPendienteCollectionDC objPagoPendienteCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {

                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                    objPagoPendienteCollectionDC.CodError = "00003";
                    objPagoPendienteCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else {
                    objPagoPendienteCollectionDC = LNPago.Instancia.ObtenerPagos(sCodAlumno);

                }


                return objPagoPendienteCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');

                    objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                    objPagoPendienteCollectionDC.CodError = "11111";
                    objPagoPendienteCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                }
                catch (Exception ex2)
                {
                    objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                    objPagoPendienteCollectionDC.CodError = "11111";
                    objPagoPendienteCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                }
                return objPagoPendienteCollectionDC;
            }
        }

        public AlumnoDC AlumnoAutenticar(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            AlumnoDC objAlumnoDC;
            sCodAlumno = sCodAlumno.ToUpper();
            string sToken = "";
            try
            {
                if (sCodAlumno == "")
                {
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Debe ingresar su código de alumno";
                    return objAlumnoDC;
                }

                if (sContrasena == "")
                {
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Debe ingresar su contraseña";
                    return objAlumnoDC;
                }

                if (sCodAlumno.StartsWith("P"))
                {
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Su perfil de usuario no es soportado por la aplicación.";
                }
                else
                {

                    objAlumnoDC = LNAlumno.Instancia.AutenticarAlumno(sCodAlumno, sContrasena, cPlataforma);
                    if (objAlumnoDC != null && objAlumnoDC.CodError != null)
                    {
                        if (objAlumnoDC.EsAlumno != null)
                        {
                            if (!objAlumnoDC.EsAlumno.Equals("ALU"))
                            {
                                objAlumnoDC = new AlumnoDC();
                                objAlumnoDC.CodError = "00002";
                                objAlumnoDC.MsgError = "Su perfil de usuario no es soportado por la aplicación.";
                            }
                            else if (objAlumnoDC.CodError.Equals("00000"))
                            {
                                sToken = LNAlumno.Instancia.GenerarToken(sCodAlumno, sContrasena, cPlataforma);
                                objAlumnoDC.Token = sToken;
                            }
                        }
                    }
                    else
                    {

                        objAlumnoDC = new AlumnoDC();
                        objAlumnoDC.CodError = "00002";
                        objAlumnoDC.MsgError = "Usuario y/o contraseña incorrectos.";
                    }
                }
                return objAlumnoDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = ex.Message;
                    return objAlumnoDC;
                }
                catch (Exception ex2)
                {
                    objAlumnoDC = new AlumnoDC();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = ex2.Message;
                    return objAlumnoDC;
                }
            }
        }
                
        #endregion

        #region SS-2013-072
        // ******************************************************************************************************************************************//

        /* Web Service que devuelve listado de compañeros de clase de un alumno en un determinado curso */
        public AlumnoCollectionDC CompanerosClase(String sCodAlumno, String sCodCurso, String sToken)
        {
            AlumnoCollectionDC oListadoAlumnos;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    oListadoAlumnos = new AlumnoCollectionDC();
                    oListadoAlumnos.CodError = "00003";
                    oListadoAlumnos.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oListadoAlumnos = LNAlumno.Instancia.ListadoCompanerosClase(sCodAlumno, sCodCurso);
                }

                return oListadoAlumnos;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oListadoAlumnos = new AlumnoCollectionDC();
                    oListadoAlumnos.CodError = "11111";
                    oListadoAlumnos.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oListadoAlumnos;
                }
                catch (Exception ex2)
                {
                    oListadoAlumnos = new AlumnoCollectionDC();
                    oListadoAlumnos.CodError = "11111";
                    oListadoAlumnos.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oListadoAlumnos;
                }
            }
        }

        /* Web Service que devuelve listado de espacios deportivos y sus actividades, agrupados por sede */
        public EspacioDeportivoCollectionDC PoblarEspaciosDeportivos(String sCodAlumno, String sToken)
        {
            EspacioDeportivoCollectionDC oEspacios;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    oEspacios = new EspacioDeportivoCollectionDC();
                    oEspacios.CodError = "00003";
                    oEspacios.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oEspacios = LNRecurso.Instancia.PoblarEspaciosDeportivos(sCodAlumno);
                }

                return oEspacios;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oEspacios = new EspacioDeportivoCollectionDC();
                    oEspacios.CodError = "11111";
                    oEspacios.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oEspacios;
                }
                catch (Exception ex2)
                {
                    oEspacios = new EspacioDeportivoCollectionDC();
                    oEspacios.CodError = "11111";
                    oEspacios.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oEspacios;
                }
            }
        }
        
        /* Web Service que devuelve disponibilidad de un espacio deportivo especificado como parametro */
        public DiasLibresEDCollectionDC DisponibilidadEspacioDeportivo(String sCodSede, String sCodED, String sNumHoras, String sCodAlumno, String sFechaIni, String sFechaFin, String sToken)
        {
            DiasLibresEDCollectionDC oHorario;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    oHorario = new DiasLibresEDCollectionDC();
                    oHorario.CodError = "00003";
                    oHorario.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oHorario = LNRecurso.Instancia.DisponibilidadEspacioDeportivo(sCodSede, sCodED, sNumHoras, sCodAlumno, sFechaIni, sFechaFin);
                }

                return oHorario;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oHorario = new DiasLibresEDCollectionDC();
                    oHorario.CodError = "11111";
                    oHorario.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oHorario;
                }
                catch (Exception ex2)
                {
                    oHorario = new DiasLibresEDCollectionDC();
                    oHorario.CodError = "11111";
                    oHorario.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oHorario;
                }
            }
        }

        /* Web Service que permite reservar un espacio deportivo para una actividad especifica */
        public ReservaEspacioDepDC ReservarEspacioDeportivo(String sCodSede, String sCodED, String sCodActiv, String sNumHoras, String sCodAlumno, String sFecha, String sHoraIni, String sHoraFin, String sDetalles, String sToken)
        {
            ReservaEspacioDepDC oReserva;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken))
                {
                    oReserva = new ReservaEspacioDepDC();
                    oReserva.CodError = "00003";
                    oReserva.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oReserva = LNRecurso.Instancia.ReservarEspacioDeportivo(sCodSede, sCodED, sCodActiv, sNumHoras, sCodAlumno, sFecha, sHoraIni, sHoraFin, sDetalles);
                }

                return oReserva;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oReserva = new ReservaEspacioDepDC();
                    oReserva.CodError = "11111";
                    oReserva.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oReserva;
                }
                catch (Exception ex2)
                {
                    oReserva = new ReservaEspacioDepDC();
                    oReserva.CodError = "11111";
                    oReserva.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oReserva;
                }
            }
        }

        /* Web Service que permite devolver los hijos asociados de un padre de familia */
        public ListadoHijosDC ListadoHijos(String sCodigo, String sToken)
        {
            ListadoHijosDC oListadoHijos;
            
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    oListadoHijos = new ListadoHijosDC();
                    oListadoHijos.CodError = "00003";
                    oListadoHijos.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oListadoHijos = LNAlumno.Instancia.ListadoHijos(sCodigo.ToUpper());
                }

                return oListadoHijos;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodigo.ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oListadoHijos = new ListadoHijosDC();
                    oListadoHijos.CodError = "11111";
                    oListadoHijos.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oListadoHijos;
                }
                catch (Exception ex2)
                {
                    oListadoHijos = new ListadoHijosDC();
                    oListadoHijos.CodError = "11111";
                    oListadoHijos.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oListadoHijos;
                }
            }
        }

        /* Web Service que habilita el login para padres de familia y profesores */
        public UsuarioDC UsuarioAutenticar(string sCodigo, string sContrasena, char cPlataforma)
        {
            UsuarioDC objUsuarioDC;
            string sToken = "";

            if(sCodigo == ""){
                objUsuarioDC = new UsuarioDC();
                objUsuarioDC.CodError = "11111";
                objUsuarioDC.MsgError = "Debe ingresar su código de usuario";
                return objUsuarioDC;
            }

            if(sContrasena == "") {
                objUsuarioDC = new UsuarioDC();
                objUsuarioDC.CodError = "11111";
                objUsuarioDC.MsgError = "Debe ingresar su contraseña";
                return objUsuarioDC;
            }

            try
            {
                objUsuarioDC = LNAlumno.Instancia.AutenticarUsuario(sCodigo.ToUpper(), sContrasena, cPlataforma);
                if (objUsuarioDC.CodError == "00000")
                {
                    sToken = LNAlumno.Instancia.GenerarToken(sCodigo.ToUpper(), sContrasena, cPlataforma);
                    objUsuarioDC.Token = sToken;
                }
                return objUsuarioDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodigo, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objUsuarioDC = new UsuarioDC();
                    objUsuarioDC.CodError = "11111";
                    objUsuarioDC.MsgError = ex.Message; // "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                    return objUsuarioDC;
                }
                catch (Exception ex2)
                {
                    objUsuarioDC = new UsuarioDC();
                    objUsuarioDC.CodError = "11111";
                    objUsuarioDC.MsgError = ex2.Message; //"Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                    return objUsuarioDC;
                }
            }
        }

        /* Web service que provee una nueva version del login (Metodo POST) para habilitar permisos a padres de familia y docentes */
        public UsuarioDC UsuarioAutenticarPost(Aut sAut)
        {
            UsuarioDC objUsuarioDC;
            sAut = new Aut { Usuario = sAut.Usuario, Contrasena = LNUtil.Instancia.DesencriptarString(sAut.Contrasena), Plataforma = sAut.Plataforma  };//habilitar
            //sAut = new Aut { Usuario = LNUtil.Instancia.DesencriptarString(sAut.Usuario), Contrasena = LNUtil.Instancia.DesencriptarString(sAut.Contrasena) };//habilitar
            string sToken = "";

            if (sAut.Usuario == "")
            {
                objUsuarioDC = new UsuarioDC();
                objUsuarioDC.CodError = "11111";
                objUsuarioDC.MsgError = "Debe ingresar su código de usuario";
                return objUsuarioDC;
            }

            if (sAut.Contrasena == "")
            {
                objUsuarioDC = new UsuarioDC();
                objUsuarioDC.CodError = "11111";
                objUsuarioDC.MsgError = "Debe ingresar su contraseña";
                return objUsuarioDC;
            }

            try
            {
                objUsuarioDC = LNAlumno.Instancia.AutenticarUsuario(sAut.Usuario.ToUpper(), sAut.Contrasena, sAut.Plataforma);
                if (objUsuarioDC.CodError == "00000")
                {
                    sToken = LNAlumno.Instancia.GenerarToken(sAut.Usuario.ToUpper(), sAut.Contrasena, sAut.Plataforma);
                    objUsuarioDC.Token = sToken;
                }
                return objUsuarioDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sAut.Usuario, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objUsuarioDC = new UsuarioDC();
                    objUsuarioDC.CodError = "11111";
                    objUsuarioDC.MsgError = ex.Message; // "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                    return objUsuarioDC;
                }
                catch (Exception ex2)
                {
                    objUsuarioDC = new UsuarioDC();
                    objUsuarioDC.CodError = "11111";
                    objUsuarioDC.MsgError = ex2.Message; //"Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                    return objUsuarioDC;
                }
            }
        }

        /* Web Service que devuelve el listado de cursos que dicta el profesor en el ciclo actual */
        public ListadoCursosDC ListadoCursosProfesor(String sCodigo, String sToken)
        {
            ListadoCursosDC oListadoCursos;

            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    oListadoCursos = new ListadoCursosDC();
                    oListadoCursos.CodError = "00003";
                    oListadoCursos.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oListadoCursos = LNProfesor.Instancia.ListadoCursosProfesor(sCodigo.ToUpper());
                }

                return oListadoCursos;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodigo.ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oListadoCursos = new ListadoCursosDC();
                    oListadoCursos.CodError = "11111";
                    oListadoCursos.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oListadoCursos;
                }
                catch (Exception ex2)
                {
                    oListadoCursos = new ListadoCursosDC();
                    oListadoCursos.CodError = "11111";
                    oListadoCursos.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oListadoCursos;
                }
            }
        }

        /* Web Service que devuelve el listado de alumnos matriculados en un curso determinado del profesor */
        public ListadoAlumnosCursoDC ListadoAlumnosProfesor(String sCodigo, String sToken, String sModal, String sPeriodo, String sCurso, String sSeccion, String sGrupo)
        {
            ListadoAlumnosCursoDC oListadoAlumnos;

            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    oListadoAlumnos = new ListadoAlumnosCursoDC();
                    oListadoAlumnos.CodError = "00003";
                    oListadoAlumnos.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oListadoAlumnos = LNProfesor.Instancia.ListadoAlumnosProfesor(sCodigo.ToUpper(), sModal, sPeriodo, sCurso, sSeccion, sGrupo);
                }

                return oListadoAlumnos;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodigo.ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oListadoAlumnos = new ListadoAlumnosCursoDC();
                    oListadoAlumnos.CodError = "11111";
                    oListadoAlumnos.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oListadoAlumnos;
                }
                catch (Exception ex2)
                {
                    oListadoAlumnos = new ListadoAlumnosCursoDC();
                    oListadoAlumnos.CodError = "11111";
                    oListadoAlumnos.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oListadoAlumnos;
                }
            }
        }

        /* Web Service que devuelve el horario actual del profesor */
        public HorarioProfesorDC HorarioProfesor(String sCodigo, String sToken)
        {
            HorarioProfesorDC oHorarioProf;

            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    oHorarioProf = new HorarioProfesorDC();
                    oHorarioProf.CodError = "00003";
                    oHorarioProf.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oHorarioProf = LNProfesor.Instancia.HorarioProfesor(sCodigo.ToUpper());
                }

                return oHorarioProf;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodigo.ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oHorarioProf = new HorarioProfesorDC();
                    oHorarioProf.CodError = "11111";
                    oHorarioProf.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oHorarioProf;
                }
                catch (Exception ex2)
                {
                    oHorarioProf = new HorarioProfesorDC();
                    oHorarioProf.CodError = "11111";
                    oHorarioProf.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oHorarioProf;
                }
            }
        }

        /* Interfaz que permite que un profesor pueda consultar las notas de un alumno */
        public NotaCollectionDC NotaListarProfesor(String sCodigo, String sCodAlumno, String sCodCurso, String sToken)
        {
            NotaCollectionDC ObjNotaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "00003";
                    ObjNotaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el docente dicta el curso seleccionado */
                    if(LNNota.Instancia.ValidaProfesorCurso(sCodigo, sCodAlumno, sCodCurso)) {
                        ObjNotaCollectionDC = LNNota.Instancia.getNotas(sCodAlumno, sCodCurso);
                    }
                    else {
                        ObjNotaCollectionDC = new NotaCollectionDC();
                        ObjNotaCollectionDC.CodError = "00004";
                        ObjNotaCollectionDC.MsgError = "El docente " + sCodigo + " no dicta actualmente el curso " + sCodCurso;
                    }
                }

                return ObjNotaCollectionDC;
            }
            catch (Exception ex)
            {

                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "11111";
                    ObjNotaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 204).";
                    return ObjNotaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "11111";
                    ObjNotaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 204).";
                    return ObjNotaCollectionDC;
                }
            }
        }

        /* Interfaz que permite que un profesor pueda consultar las inasistencias de un alumno */
        public InasistenciaCollectionDC InasistenciaListarProfesor(String sCodigo, String sCodAlumno, String sToken)
        {
            InasistenciaCollectionDC ObjInasistenciaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {

                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "00003";
                    ObjInasistenciaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno esta matriculado en algun curso que dicte el profesor */
                    if (LNNota.Instancia.ValidaProfesorAlumno(sCodigo, sCodAlumno))
                    {
                        ObjInasistenciaCollectionDC = LNInasistencia.Instancia.ObtenerInasistencias(sCodAlumno);
                    }
                    else
                    {
                        ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                        ObjInasistenciaCollectionDC.CodError = "00004";
                        ObjInasistenciaCollectionDC.MsgError = "El alumno " + sCodAlumno + " no está matriculado en ningún curso que dicte el docente " + sCodigo;
                    }
                }

                return ObjInasistenciaCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');

                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "11111";
                    ObjInasistenciaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 205).";
                    return ObjInasistenciaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "11111";
                    ObjInasistenciaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 205).";
                    return ObjInasistenciaCollectionDC;
                }
            }
        }

        /* Interfaz que permite que un padre de familia pueda ver el horario de clase de su hijo */
        public HorarioDiaCollectionDC HorarioDiaListarPadre(String sCodigo, String sCodAlumno, String sToken)
        {
            HorarioDiaCollectionDC ObjHorarioDiaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                    ObjHorarioDiaCollectionDC.CodError = "00003";
                    ObjHorarioDiaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno es padre del hijo seleccionado */
                    if (LNNota.Instancia.ValidaHijoPadre(sCodigo, sCodAlumno))
                    {
                        ObjHorarioDiaCollectionDC = LNHorario.Instancia.ObtenerHorario(sCodAlumno);
                    }
                    else
                    {
                        ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                        ObjHorarioDiaCollectionDC.CodError = "11111";
                        ObjHorarioDiaCollectionDC.MsgError = "El usuario " + sCodigo + " no está registrado como padre del alumno " + sCodAlumno;
                    }
                }

                return ObjHorarioDiaCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                    ObjHorarioDiaCollectionDC.CodError = "11111";
                    ObjHorarioDiaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return ObjHorarioDiaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjHorarioDiaCollectionDC = new HorarioDiaCollectionDC();
                    ObjHorarioDiaCollectionDC.CodError = "11111";
                    ObjHorarioDiaCollectionDC.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return ObjHorarioDiaCollectionDC;
                }
            }
        }

        /* Interfaz que permite que un padre de familia pueda consultar los cursos que lleva su hijo */
        public CursoAlumnoCollectionDC CursoAlumnoListarPadre(String sCodigo, String sCodAlumno, String sToken)
        {
            CursoAlumnoCollectionDC ObjCursoAlumnoCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                    ObjCursoAlumnoCollectionDC.CodError = "00003";
                    ObjCursoAlumnoCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno es padre del hijo seleccionado */
                    if (LNNota.Instancia.ValidaHijoPadre(sCodigo, sCodAlumno))
                    {
                        ObjCursoAlumnoCollectionDC = LNCurso.Instancia.ObtenerCursos(sCodAlumno);
                    }
                    else
                    {
                        ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                        ObjCursoAlumnoCollectionDC.CodError = "11111";
                        ObjCursoAlumnoCollectionDC.MsgError = "El usuario " + sCodigo + " no está registrado como padre del alumno " + sCodAlumno;
                    }
                }
                return ObjCursoAlumnoCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                    ObjCursoAlumnoCollectionDC.CodError = "11111";
                    ObjCursoAlumnoCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                    return ObjCursoAlumnoCollectionDC;
                }
                catch (Exception)
                {
                    ObjCursoAlumnoCollectionDC = new CursoAlumnoCollectionDC();
                    ObjCursoAlumnoCollectionDC.CodError = "11111";
                    ObjCursoAlumnoCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                    return ObjCursoAlumnoCollectionDC;
                }

            }
        }

        /* Interfaz que permite que un padre de familia pueda consultar las notas de su hijo */
        public NotaCollectionDC NotaListarPadre(String sCodigo, String sCodAlumno, String sCodCurso, String sToken)
        {
            NotaCollectionDC ObjNotaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "00003";
                    ObjNotaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno es padre del hijo seleccionado */
                    if (LNNota.Instancia.ValidaHijoPadre(sCodigo, sCodAlumno))
                    {
                        ObjNotaCollectionDC = LNNota.Instancia.getNotas(sCodAlumno, sCodCurso);
                    }
                    else
                    {
                        ObjNotaCollectionDC = new NotaCollectionDC();
                        ObjNotaCollectionDC.CodError = "11111";
                        ObjNotaCollectionDC.MsgError = "El usuario " + sCodigo + " no está registrado como padre del alumno " + sCodAlumno;
                    }
                }

                return ObjNotaCollectionDC;
            }
            catch (Exception ex)
            {

                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "11111";
                    ObjNotaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 204).";
                    return ObjNotaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjNotaCollectionDC = new NotaCollectionDC();
                    ObjNotaCollectionDC.CodError = "11111";
                    ObjNotaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 204).";
                    return ObjNotaCollectionDC;
                }
            }
        }

        /* Interfaz que permite que un padre de familia pueda consultar las inasistencias de su hijo */
        public InasistenciaCollectionDC InasistenciaListarPadre(String sCodigo, String sCodAlumno, String sToken)
        {
            InasistenciaCollectionDC ObjInasistenciaCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {

                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "00003";
                    ObjInasistenciaCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno es padre del hijo seleccionado */
                    if (LNNota.Instancia.ValidaHijoPadre(sCodigo, sCodAlumno))
                    {
                        ObjInasistenciaCollectionDC = LNInasistencia.Instancia.ObtenerInasistencias(sCodAlumno);
                    }
                    else
                    {
                        ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                        ObjInasistenciaCollectionDC.CodError = "11111";
                        ObjInasistenciaCollectionDC.MsgError = "El usuario " + sCodigo + " no está registrado como padre del alumno " + sCodAlumno;
                    }
                }

                return ObjInasistenciaCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');

                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "11111";
                    ObjInasistenciaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 205).";
                    return ObjInasistenciaCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjInasistenciaCollectionDC = new InasistenciaCollectionDC();
                    ObjInasistenciaCollectionDC.CodError = "11111";
                    ObjInasistenciaCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 205).";
                    return ObjInasistenciaCollectionDC;
                }
            }
        }

        /* Interfaz que permite que un padre de familia pueda consultar los pagos pendientes de su hijo */
        public PagoPendienteCollectionDC PagoPendienteListarPadre(String sCodigo, String sCodAlumno, String sToken)
        {
            PagoPendienteCollectionDC objPagoPendienteCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {

                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                    objPagoPendienteCollectionDC.CodError = "00003";
                    objPagoPendienteCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno es padre del hijo seleccionado */
                    if (LNNota.Instancia.ValidaHijoPadre(sCodigo, sCodAlumno))
                    {
                        objPagoPendienteCollectionDC = LNPago.Instancia.ObtenerPagos(sCodAlumno);
                    }
                    else
                    {
                        objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                        objPagoPendienteCollectionDC.CodError = "11111";
                        objPagoPendienteCollectionDC.MsgError = "El usuario " + sCodigo + " no está registrado como padre del alumno " + sCodAlumno;
                    }

                }


                return objPagoPendienteCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');

                    objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                    objPagoPendienteCollectionDC.CodError = "11111";
                    objPagoPendienteCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 207).";
                }
                catch (Exception ex2)
                {
                    objPagoPendienteCollectionDC = new PagoPendienteCollectionDC();
                    objPagoPendienteCollectionDC.CodError = "11111";
                    objPagoPendienteCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 207).";
                }
                return objPagoPendienteCollectionDC;
            }
        }

        /* Interfaz que permite que un padre de familia pueda consultar los tramites de su hijo */
        public TramiteRealizadoCollectionDC TramiteRealizadoListarPadre(String sCodigo, String sCodAlumno, String sToken)
        {
            TramiteRealizadoCollectionDC ObjTramiteRealizadoCollectionDC;
            sCodAlumno = sCodAlumno.ToUpper();
            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodigo.ToUpper(), sToken))
                {
                    ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                    ObjTramiteRealizadoCollectionDC.CodError = "00003";
                    ObjTramiteRealizadoCollectionDC.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    /* Validamos si el alumno es padre del hijo seleccionado */
                    if (LNNota.Instancia.ValidaHijoPadre(sCodigo, sCodAlumno))
                    {
                        ObjTramiteRealizadoCollectionDC = LNTramite.Instancia.getTramites(sCodAlumno);
                    }
                    else
                    {
                        ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                        ObjTramiteRealizadoCollectionDC.CodError = "11111";
                        ObjTramiteRealizadoCollectionDC.MsgError = "El usuario " + sCodigo + " no está registrado como padre del alumno " + sCodAlumno;
                    }
                }

                return ObjTramiteRealizadoCollectionDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno, sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                    ObjTramiteRealizadoCollectionDC.CodError = "11111";
                    ObjTramiteRealizadoCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 208).";
                    return ObjTramiteRealizadoCollectionDC;
                }
                catch (Exception ex2)
                {
                    ObjTramiteRealizadoCollectionDC = new TramiteRealizadoCollectionDC();
                    ObjTramiteRealizadoCollectionDC.CodError = "11111";
                    ObjTramiteRealizadoCollectionDC.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 208).";
                    return ObjTramiteRealizadoCollectionDC;
                }
            }
        }

        /* Web service que devuelve listado de sedes donde esta estudiando el alumno */
        public ListadoSedesAlumnoDC ListadoSedesAlumno(String sCodAlumno, String sToken)
        {
            ListadoSedesAlumnoDC oListadoSedes;

            try
            {
                if (!LNAlumno.Instancia.AlumnoTokenObtener(sCodAlumno.ToUpper(), sToken))
                {
                    oListadoSedes = new ListadoSedesAlumnoDC();
                    oListadoSedes.CodError = "00003";
                    oListadoSedes.MsgError = "La sesión ha expirado o no se reconoce el usuario que solicita la operación.";
                }
                else
                {
                    oListadoSedes = LNAlumno.Instancia.ListadoSedesAlumno(sCodAlumno.ToUpper());
                }

                return oListadoSedes;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                             DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    oListadoSedes = new ListadoSedesAlumnoDC();
                    oListadoSedes.CodError = "11111";
                    oListadoSedes.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                    return oListadoSedes;
                }
                catch (Exception ex2)
                {
                    oListadoSedes = new ListadoSedesAlumnoDC();
                    oListadoSedes.CodError = "11111";
                    oListadoSedes.MsgError = "Ocurrió un error con los servicios: " + ex2.Message;
                    return oListadoSedes;
                }
            }
        }

        // ******************************************************************************************************************************************//
        #endregion

        public UsuarioDC AlumnoAutenticarPost(Aut sAut)
        {
            UsuarioDC objAlumnoDC;
            
            sAut = new Aut { Usuario = sAut.Usuario, Contrasena = LNUtil.Instancia.DesencriptarString(sAut.Contrasena), Plataforma=sAut.Plataforma };//habilitar
            //sAut = new Aut { Usuario = LNUtil.Instancia.DesencriptarString(sAut.Usuario), Contrasena = LNUtil.Instancia.DesencriptarString(sAut.Contrasena) };//habilitar
            string sCodAlumno = sAut.Usuario.ToUpper();
            string sToken = "";
            try
            {
                if (sCodAlumno == "")
                {
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Debe ingresar su código de alumno";
                    return objAlumnoDC;
                }

                if (sAut.Contrasena == "")
                {
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Debe ingresar su contraseña";
                    return objAlumnoDC;
                }

                if (sCodAlumno.Length <= 6) {
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "00001";
                    objAlumnoDC.MsgError = "Còdigo de usuario incorrecto.";
                    return objAlumnoDC;
                }
                
                /*string scad = sCodAlumno.Substring(sCodAlumno.Length - 1 - 5);
                Int64 isNum;

                if (!Int64.TryParse(scad,out isNum))
                {
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Su perfil de usuario no es soportado por la aplicación.";
                }
                else
                {*/
                objAlumnoDC = LNAlumno.Instancia.AutenticarUsuario(sCodAlumno, sAut.Contrasena ,  'A');
                if (objAlumnoDC != null && objAlumnoDC.CodError != null)
                {
                    if (objAlumnoDC.CodError.Equals("00000"))
                    {
                        sToken = LNAlumno.Instancia.GenerarToken(sCodAlumno, sAut.Contrasena, 'A');
                        objAlumnoDC.Token = sToken;
                    }
                }
                else
                {
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "00002";
                    objAlumnoDC.MsgError = "Usuario y/o contraseña incorrectos";
                }
                //}
                return objAlumnoDC;
            }
            catch (Exception ex)
            {
                try
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno , sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = ex.Message;
                    return objAlumnoDC;
                }
                catch (Exception ex2)
                {
                    objAlumnoDC = new UsuarioDC();
                    objAlumnoDC.CodError = "11111";
                    objAlumnoDC.MsgError = ex2.Message;
                    return objAlumnoDC;
                }
            }
        }
        
        public Aut getAut()
        {
            return new Aut() { Usuario = "I611302", Contrasena = "1234564789", Plataforma = 'C' };
        }

        #region new methods
        public DCReservaRecurso ReservarRecurso(string sCodRecurso, string sNomRecurso, string sCodAlumno, string sCanHoras, string sfecIni, string sfecFin, string sToken)
        {
            DCReservaRecurso objReservaRecurso;
            try
            {
                objReservaRecurso = LNRecurso.Instancia.ReservarRecurso(sCodRecurso, sNomRecurso, sCodAlumno.ToString().ToUpper(), sCanHoras, sfecIni, sfecFin, sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToString().ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objReservaRecurso = new DCReservaRecurso();
                objReservaRecurso.CodError = "11111";
                objReservaRecurso.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objReservaRecurso;
        }

        public DCListaRecurso ListadoRecursos(string sTipoRecurso, string sLocal, string sFecIni, string sCanHoras, string sFechaFin, string sCodAlumno, string sToken)
        {
            DCListaRecurso objListaRecurso;
            try
            {
                objListaRecurso = LNRecurso.Instancia.ListaRecursos(sTipoRecurso, sLocal, sFecIni, sFechaFin, sCanHoras, sCodAlumno.ToString().ToUpper(), sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToString().ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objListaRecurso = new DCListaRecurso();
                objListaRecurso.CodError = "11111";
                objListaRecurso.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objListaRecurso;
        }

        public DCListaRecurso RandomRecursoDisp(string sTipoRecurso, string sLocal, string sFecIni, string sCanHoras, string sFechaFin, string sCodAlumno, string sToken)
        {
            DCListaRecurso objListaRecurso;
            try
            {
                objListaRecurso = LNRecurso.Instancia.RandomRecursoDisp(sTipoRecurso, sLocal, sFecIni, sFechaFin, sCanHoras, sCodAlumno.ToString().ToUpper(), sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToString().ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objListaRecurso = new DCListaRecurso();
                objListaRecurso.CodError = "11111";
                objListaRecurso.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objListaRecurso;
        }

        public DCListaReservaAlumno ReservasAlumno(string sFecIni, string sFechaFin, string sCodAlumno, string sToken)
        {
            DCListaReservaAlumno objListaReservaAlumno;
            try
            {
                objListaReservaAlumno = LNRecurso.Instancia.ListaReservaAlumno(sFecIni, sFechaFin, sCodAlumno.ToString().ToUpper(), sToken);
            }
            catch (Exception ex)
            {
                LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToString().ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                            DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                objListaReservaAlumno = new DCListaReservaAlumno();
                objListaReservaAlumno.CodError = "11111";
                objListaReservaAlumno.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
            }
            return objListaReservaAlumno;
        }

        public DCReservaRecurso ActivarReserva(string sCodReserva, string sCodAlumno, string sCodAlumno2, string sToken)
        {
            {
                DCReservaRecurso objReservaRecurso;
                try
                {
                    objReservaRecurso = LNRecurso.Instancia.ActivarReserva(sCodReserva, sCodAlumno.ToString().ToUpper(), sCodAlumno2.ToString().ToUpper(), sToken);
                }
                catch (Exception ex)
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToString().ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                                DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objReservaRecurso = new DCReservaRecurso();
                    objReservaRecurso.CodError = "11111";
                    objReservaRecurso.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                }
                return objReservaRecurso;
            }
        }

        public DCReservaRecurso VerificaReserva(string sCodReserva, string sCodRcurso, string sCodAlumno, string sToken)
        {
            {
                DCReservaRecurso objReservaRecurso;
                
                try
                {
                    //objReservaRecurso = new DCReservaRecurso();//borrar
                    objReservaRecurso = LNRecurso.Instancia.VerificaReserva(sCodReserva,sCodRcurso, sCodAlumno.ToString().ToUpper(), sToken);
                }
                catch (Exception ex)
                {
                    LNExcepcion.Instancia.RegistrarExcepcion(1, sCodAlumno.ToString().ToUpper(), sToken, ex.Message, ex.StackTrace,
                                                                DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay.ToString(), '0');
                    objReservaRecurso = new DCReservaRecurso();
                     objReservaRecurso.CodError = "11111";
                     objReservaRecurso.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
                     
                }
                return objReservaRecurso;
            }
        }
        #endregion 

    }
}
