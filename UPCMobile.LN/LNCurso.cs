using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNCurso
    {
        #region Singleton
        private static LNCurso _Instancia;
        private LNCurso() { }
        public static LNCurso Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNCurso();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos
        public DC.CursoAlumnoCollectionDC ObtenerCursos(string codUsuario)
        {
            DC.CursoAlumnoCollectionDC Cursos;
            string errMensaje = "";
            try
            {
                Collection<DC.CursoAlumnoDC> lstCurso = DA.DACurso.Instancia.ObtenerCursos(codUsuario, ref errMensaje);
                if (lstCurso.Count <= 0)
                {
                    Cursos = new DC.CursoAlumnoCollectionDC();
                    Cursos.CodError = "00002";
                    Cursos.MsgError = "Ud. no se encuentra matriculado en el presente ciclo.";
                    return Cursos;
                }
                else
                {
                    Cursos = new DC.CursoAlumnoCollectionDC();

                    Cursos.Cursos = lstCurso;
                    Cursos.CodError = "00000";
                    Cursos.MsgError = "";

                    return Cursos;
                }
            }
            catch (Exception ex)
            {
                Cursos = new DC.CursoAlumnoCollectionDC();
                Cursos.CodError = "11111";
                Cursos.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return Cursos;
            }

        }
        #endregion
    }
}
