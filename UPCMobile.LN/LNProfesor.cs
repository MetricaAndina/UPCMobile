using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UPCMobile.AD;
using Componente.Seguridad.UserResult;
using System.Configuration;

namespace UPCMobile.LN
{
    public class LNProfesor
    {
        #region Singleton
        private static LNProfesor _Instancia;
        private LNProfesor() { }
        public static LNProfesor Instancia
        {
            get {
                if (_Instancia == null)
                    _Instancia = new LNProfesor();

                return _Instancia;
            }
        }
        #endregion

        #region SS-2013-072

        public DC.ListadoCursosDC ListadoCursosProfesor(String sCodigo)
        {
            DC.ListadoCursosDC oCursos;
            string errMensaje = "";

            try
            {
                Collection<DC.ModalidadDC> lstModalidades = DA.DAProfesor.Instancia.ListadoCursosProfesor(sCodigo.ToUpper(), ref errMensaje);
                if (errMensaje != "null")
                {
                    oCursos = new DC.ListadoCursosDC();
                    oCursos.CodError = "00002";
                    oCursos.MsgError = errMensaje;
                    return oCursos;
                }
                else
                {
                    oCursos = new DC.ListadoCursosDC();
                    oCursos.modalidades = lstModalidades;
                    oCursos.CodError = "00000";
                    oCursos.MsgError = "";

                    return oCursos;
                }
            }
            catch (Exception ex)
            {
                oCursos = new DC.ListadoCursosDC();
                oCursos.CodError = "11111";
                oCursos.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                return oCursos;
            }
        }

        public DC.ListadoAlumnosCursoDC ListadoAlumnosProfesor(String sCodigo, String sModal, String sPeriodo, String sCurso, String sSeccion, String sGrupo)
        {
            DC.ListadoAlumnosCursoDC oAlumnos;
            string errMensaje = "";
            string wCurso = (Convert.ToString(sCurso) == "") ? String.Empty : sCurso;

            try
            {
                Collection<DC.CursosDC> lstCursos = DA.DAProfesor.Instancia.ListadoAlumnosProfesor(sCodigo.ToUpper(), sModal, sPeriodo, wCurso, sSeccion, sGrupo, ref errMensaje);
                if (errMensaje != "null")
                {
                    oAlumnos = new DC.ListadoAlumnosCursoDC();
                    oAlumnos.CodError = "00002";
                    oAlumnos.MsgError = errMensaje;
                    return oAlumnos;
                }
                else
                {
                    oAlumnos = new DC.ListadoAlumnosCursoDC();
                    oAlumnos.Cursos = lstCursos;
                    oAlumnos.CodError = "00000";
                    oAlumnos.MsgError = "";

                    return oAlumnos;
                }
            }
            catch (Exception ex)
            {
                oAlumnos = new DC.ListadoAlumnosCursoDC();
                oAlumnos.CodError = "11111";
                oAlumnos.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                return oAlumnos;
            }
        }

        public DC.HorarioProfesorDC HorarioProfesor(String sCodigo)
        {
            DC.HorarioProfesorDC oHorario;
            string errMensaje = "";

            try
            {
                Collection<DC.DiasProfesorDC> lstDias = DA.DAProfesor.Instancia.HorarioProfesor(sCodigo.ToUpper(), ref errMensaje);
                if (errMensaje != "null")
                {
                    oHorario = new DC.HorarioProfesorDC();
                    oHorario.CodError = "00002";
                    oHorario.MsgError = errMensaje;
                    return oHorario;
                }
                else
                {
                    oHorario = new DC.HorarioProfesorDC();
                    oHorario.HorarioDia = lstDias;
                    oHorario.CodError = "00000";
                    oHorario.MsgError = "";

                    return oHorario;
                }
            }
            catch (Exception ex)
            {
                oHorario = new DC.HorarioProfesorDC();
                oHorario.CodError = "11111";
                oHorario.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                return oHorario;
            }
        }

        #endregion 
    }
}
