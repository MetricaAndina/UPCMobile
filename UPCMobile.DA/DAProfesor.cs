using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Configuration;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using DC = UPCMobile.DC;

namespace UPCMobile.DA
{
    public class DAProfesor
    {
        #region Singleton
        private static DAProfesor _Instancia;
        private DAProfesor() { }
        public static DAProfesor Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DAProfesor();
                }
                return _Instancia;
            }
        }
        #endregion

        #region SS-2013-072

        public Collection<DC.ModalidadDC> ListadoCursosProfesor(string sCodigo, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_LISTADO_CURSOSPROF", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodigo.ToUpper();
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 5).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    ErrMensaje = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    conn.Close();
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

            return this.getModalidades(dt);
        }

        public Collection<DC.CursosDC> ListadoAlumnosProfesor(String sCodigo, String sModal, String sPeriodo, String sCurso, string sSeccion, String sGrupo, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_LISTADO_ALUMNOSPROF", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodigo.ToUpper();
                    cmd.Parameters.Add("PCOD_MODAL_EST", OracleDbType.Varchar2, 2).Value = sModal.ToUpper();
                    cmd.Parameters.Add("PCOD_PERIODO", OracleDbType.Varchar2, 6).Value = sPeriodo;
                    cmd.Parameters.Add("PCOD_CURSO", OracleDbType.Varchar2, 6).Value = sCurso;
                    cmd.Parameters.Add("PURL_WEB", OracleDbType.Varchar2, 100).Value = ConfigurationManager.AppSettings["RUTA_SERVER"];
                    cmd.Parameters.Add("PSECCION", OracleDbType.Varchar2, 10).Value = sSeccion;
                    cmd.Parameters.Add("PGRUPO", OracleDbType.Varchar2, 2).Value = sGrupo;
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 5).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    ErrMensaje = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    conn.Close();
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

            return this.getCursos(dt);
        }

        public Collection<DC.DiasProfesorDC> HorarioProfesor(String sCodigo, ref String errMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_HORARIOPROFESOR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodigo.ToUpper();
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 5).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    if (dt.Rows.Count == 0)
                    {
                        errMensaje = "El profesor no tiene clases programadas para la semana actual";
                    }
                    else
                    {
                        errMensaje = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    conn.Close();
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

            return this.getHorario(dt);
        }

        #endregion 

        #region internal

        internal Collection<DC.DiasProfesorDC> getHorario(DataTable tb)
        {
            Collection<DC.DiasProfesorDC> lista = new Collection<DC.DiasProfesorDC>();
            DC.DiasProfesorDC nodo;
            String valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(5))
                {
                    nodo = new DC.DiasProfesorDC();
                    nodo.CodDia = (string)row.Field<string>(0);
                    nodo.Fecha = (string)row.Field<string>(5);
                    nodo.Clases = getClases(tb, nodo.Fecha);
                    
                    valor = (string)row.Field<string>(5);

                    lista.Add(nodo);
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<DC.ClaseProfesorDC> getClases(DataTable tb, string fecha)
        {
            Collection<DC.ClaseProfesorDC> lista = new Collection<DC.ClaseProfesorDC>();
            DC.ClaseProfesorDC nodo;
            string valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(1))
                {
                    if (row.Field<string>(5) == fecha)
                    {
                        nodo = new DC.ClaseProfesorDC();
                        nodo.CodClase = (string)row.Field<string>(1);
                        nodo.CodCurso = (string)row.Field<string>(2);
                        nodo.CursoNombre = (string)row.Field<string>(3);
                        nodo.CursoNombreCorto = (string)row.Field<string>(4);
                        nodo.Fecha = (string)row.Field<string>(5);
                        nodo.HoraInicio = (string)row.Field<string>(6);
                        nodo.HoraFin = (string)row.Field<string>(7);
                        nodo.Sede = (string)row.Field<string>(8);
                        nodo.Seccion = (string)row.Field<string>(9);
                        nodo.Salon = (string)row.Field<string>(11);
                        
                        lista.Add(nodo);
                    }
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<DC.ModalidadDC> getModalidades(DataTable tb)
        {
            Collection<DC.ModalidadDC> lista = new Collection<DC.ModalidadDC>();
            DC.ModalidadDC nodo;
            String valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(0))
                {
                    nodo = new DC.ModalidadDC();
                    nodo.codigo = (string)row.Field<string>(0);
                    nodo.descripcion = (string)row.Field<string>(1);
                    nodo.periodo = (string)row.Field<string>(2);
                    nodo.cursos = getCursos(tb, nodo.codigo);
                    valor = (string)row.Field<string>(0);

                    lista.Add(nodo);
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<DC.DetalleCursoDC> getCursos(DataTable tb, string cod_modal)
        {
            Collection<DC.DetalleCursoDC> lista = new Collection<DC.DetalleCursoDC>();
            DC.DetalleCursoDC nodo;
            string valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(3))
                {
                    if (row.Field<string>(0) == cod_modal)
                    {
                        nodo = new DC.DetalleCursoDC();
                        nodo.curso = (string)row.Field<string>(3);
                        nodo.cursoId = (string)row.Field<string>(4);
                        nodo.seccion = (string)row.Field<string>(5);
                        nodo.grupo = (string)row.Field<string>(6);

                        lista.Add(nodo);
                    }
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<DC.CursosDC> getCursos(DataTable tb)
        {
            Collection<DC.CursosDC> lista = new Collection<DC.CursosDC>();
            DC.CursosDC nodo;
            String valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(1))
                {
                    nodo = new DC.CursosDC();
                    
                    nodo.curso = (string)row.Field<string>(0);
                    nodo.cursoId = (string)row.Field<string>(1);
                    nodo.seccion = (string)row.Field<string>(2);
                    nodo.grupo = (string)row.Field<string>(3);
                    nodo.alumnos = getAlumnos(tb, nodo.cursoId);

                    valor = (string)row.Field<string>(1);

                    lista.Add(nodo);
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<DC.CompaneroDC> getAlumnos(DataTable tb, string cursoId)
        {
            Collection<DC.CompaneroDC> lista = new Collection<DC.CompaneroDC>();
            DC.CompaneroDC nodo;
            string valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(5))
                {
                    if (row.Field<string>(1) == cursoId)
                    {
                        nodo = new DC.CompaneroDC();
                        nodo.nombre_completo = (string)row.Field<string>(4);
                        nodo.codigo = (string)row.Field<string>(5);
                        nodo.url_foto = (string)row.Field<string>(6);
                        nodo.carrera_actual = (string)row.Field<string>(7);

                        lista.Add(nodo);
                    }
                }
            }
            tb.Dispose();
            return lista;
        }

        #endregion 
    }
}
