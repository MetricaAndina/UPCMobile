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
    public class DAAlumno
    {
        #region Singleton
        private static DAAlumno _Instancia;
        private DAAlumno() { }
        public static DAAlumno Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DAAlumno();
                }
                return _Instancia;
            }
        }
        #endregion

        #region metodos

        public DC.AlumnoDC AutenticarAlumno(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            return this.getAlumno(sCodAlumno);
        }

        private DC.AlumnoDC getAlumno(string sCodAlumno)
        {
            
            DC.AlumnoDC objAlumnoDC;
            string mensaje = "";
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_DATOS_ALUMNO", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {

                    cmd.Parameters.Add("CodigoUsuario", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("mensaje_error", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("v_Nombres", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("v_Apellidos", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("v_Genero", OracleDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("v_Estado", OracleDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("v_esAlumno", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    mensaje = (string)cmd.Parameters["mensaje_error"].Value.ToString();



                    if (mensaje.Equals("") || mensaje.Equals("null"))
                    {
                        objAlumnoDC = new DC.AlumnoDC();
                        objAlumnoDC.CodAlumno = sCodAlumno;
                        objAlumnoDC.CodError = "00000";
                        objAlumnoDC.MsgError = "";
                        objAlumnoDC.Nombres = (string)cmd.Parameters["v_Nombres"].Value.ToString();
                        objAlumnoDC.Apellidos = (string)cmd.Parameters["v_Apellidos"].Value.ToString();
                        objAlumnoDC.Genero = char.Parse(cmd.Parameters["v_Genero"].Value.ToString());
                        //objAlumnoDC.Token = sToken;
                        objAlumnoDC.Estado = char.Parse(cmd.Parameters["v_Estado"].Value.ToString());
                        objAlumnoDC.EsAlumno = ((string)cmd.Parameters["v_esAlumno"].Value.ToString()).Trim();
                    }
                    else
                    {
                        objAlumnoDC = null;
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
            return objAlumnoDC;
        }

        public string GenerarToken(string sCodigo, string sContrasena, char cPlataforma)
        {
            string token =  Guid.NewGuid().ToString().Replace("-","") + DateTime.Now.ToString("yyyyMMddhhmmss");
            this.MantToken(sCodigo, token, "I", cPlataforma.ToString());
            return token;
        }
        
        public void CerrarSession(string sCodAlumno)
        {
            this.MantToken(sCodAlumno,"","C","");
             
        }
      
        public bool AlumnoTokenObtener(string sCodAlumno, string sToken)
        {
            bool resultado ;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("NXTSG_OBTENERTOKEN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    
                    cmd.Parameters.Add("p_CodAlumno", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("p_Token", OracleDbType.Varchar2).Value = sToken;
                    cmd.Parameters.Add("o_Codigo", OracleDbType.Long,109).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    resultado = (cmd.Parameters["o_Codigo"].Value.ToString().Equals("1")? true:false);
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


            return resultado;
        }

        #endregion

        #region SS-2013-072

        public Collection<DC.CompaneroDC> ListadoCompanerosClase(string sCodAlumno, string sCodCurso, ref string sDescCurso, ref string sSeccion, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_COMPANEROS_CLASE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodAlumno.ToUpper();
                    cmd.Parameters.Add("PCOD_CURSO", OracleDbType.Varchar2).Value = sCodCurso.ToUpper();
                    cmd.Parameters.Add("PURL_WEB", OracleDbType.Varchar2).Value = ConfigurationManager.AppSettings["RUTA_SERVER"];
                    cmd.Parameters.Add("PDESC_CURSO", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PSECCION", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    sDescCurso = (string)cmd.Parameters["PDESC_CURSO"].Value.ToString();
                    sSeccion = (string)cmd.Parameters["PSECCION"].Value.ToString();
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

            return this.getListaAlumnos(dt);
        }

        public Collection<DC.HijoDC> ListadoHijos(string sCodigo, ref string sNombrePadre, ref string sApellidoPadre, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_LISTADO_HIJOS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodigo.ToUpper();
                    cmd.Parameters.Add("PNOMBRES", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PAPELLIDOS", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 5).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    sNombrePadre = (string)cmd.Parameters["PNOMBRES"].Value.ToString();
                    sApellidoPadre = (string)cmd.Parameters["PAPELLIDOS"].Value.ToString();
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

            return this.getHijos(dt);
        }

        public Collection<DC.LocalDC> ListadoSedesAlumno(String sCodAlumno, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_LISTADO_SEDEALUMNO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodAlumno.ToUpper();
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

            return this.getLocales(dt);
        }

        public DC.UsuarioDC AutenticarUsuario(string sCodigo, string sContrasena, char cPlataforma)
        {
            return this.getUsuario(sCodigo);
        }

        private DC.UsuarioDC getUsuario(string sCodigo)
        {
            DC.UsuarioDC objUsuarioDC;
            string mensaje = "";

            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_DATOS_USUARIO", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodigo.ToUpper();
                    cmd.Parameters.Add("PCOD_ALUMNO", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PNOMBRES", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PAPELLIDOS", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PGENERO", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PES_ALU", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PESTADO", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PTIPO_USER", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_LINEA", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PDSC_LINEA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_MODAL", OracleDbType.Varchar2, 2).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PDSC_MODAL", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_SEDE", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PDSC_SEDE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCICLO", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    mensaje = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();

                    if (mensaje.Equals("") || mensaje.Equals("null"))
                    {
                        objUsuarioDC = new DC.UsuarioDC();

                        objUsuarioDC.Codigo = sCodigo.ToUpper();
                        objUsuarioDC.CodigoAlumno = (string)cmd.Parameters["PCOD_ALUMNO"].Value.ToString();
                        objUsuarioDC.Nombres = (string)cmd.Parameters["PNOMBRES"].Value.ToString();
                        objUsuarioDC.Apellidos = (string)cmd.Parameters["PAPELLIDOS"].Value.ToString();
                        objUsuarioDC.Genero = (string)cmd.Parameters["PGENERO"].Value.ToString();
                        objUsuarioDC.EsAlumno = (string)cmd.Parameters["PES_ALU"].Value.ToString();
                        objUsuarioDC.Estado = (string)cmd.Parameters["PESTADO"].Value.ToString();
                        objUsuarioDC.TipoUser = (string)cmd.Parameters["PTIPO_USER"].Value.ToString();

                        if(objUsuarioDC.TipoUser == "ALUMNO") {
                            objUsuarioDC.Datos = new DC.DatosAcademicosUsuarioDC() 
                            {
                                CodLinea = (string)cmd.Parameters["PCOD_LINEA"].Value.ToString(),
                                DscLinea = (string)cmd.Parameters["PDSC_LINEA"].Value.ToString(),
                                CodModal = (string)cmd.Parameters["PCOD_MODAL"].Value.ToString(),
                                DscModal = (string)cmd.Parameters["PDSC_MODAL"].Value.ToString(),
                                CodSede = (string)cmd.Parameters["PCOD_SEDE"].Value.ToString(),
                                DscSede = (string)cmd.Parameters["PDSC_SEDE"].Value.ToString(),
                                Ciclo = (string)cmd.Parameters["PCICLO"].Value.ToString()
                            };
                        }

                        objUsuarioDC.CodError = "00000";
                        objUsuarioDC.MsgError = "";
                    }
                    else
                    {
                        objUsuarioDC = new DC.UsuarioDC();
                        objUsuarioDC.CodError = (string)cmd.Parameters["PCOD_ERROR"].Value.ToString();
                        objUsuarioDC.MsgError = mensaje;
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
            return objUsuarioDC;
        }

        #region Internal

        internal Collection<DC.HijoDC> getHijos(DataTable tb)
        {
            Collection<DC.HijoDC> lista = new Collection<DC.HijoDC>();
            DC.HijoDC nodo;

            foreach (DataRow row in tb.Rows)
            {
                nodo = new DC.HijoDC();

                nodo.codigo = (string)row.Field<string>(0);
                nodo.nombres = (string)row.Field<string>(1);
                nodo.apellidos = (string)row.Field<string>(2);

                lista.Add(nodo);
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<DC.CompaneroDC> getListaAlumnos(DataTable tb)
        {
            Collection<DC.CompaneroDC> lista = new Collection<DC.CompaneroDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaAlumnosDet(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.CompaneroDC getListaAlumnosDet(DataRow rw)
        {
            DC.CompaneroDC alumno = new DC.CompaneroDC();
            alumno.nombre_completo = (string)rw.Field<string>(0);
            alumno.codigo = (string)rw.Field<string>(1);
            alumno.url_foto = (string)rw.Field<string>(2);
            alumno.carrera_actual = (string)rw.Field<string>(3);
            return alumno;
        }

        internal Collection<DC.LocalDC> getLocales(DataTable tb)
        {
            Collection<DC.LocalDC> lista = new Collection<DC.LocalDC>();
            DC.LocalDC nodo;

            foreach (DataRow row in tb.Rows)
            {
                nodo = new DC.LocalDC();

                nodo.Codigo = (string)row.Field<string>(0);
                nodo.Descripcion = (string)row.Field<string>(1);

                lista.Add(nodo);
            }
            tb.Dispose();
            return lista;
        }

        #endregion
              
        #endregion 

        #region Privados

        private void MantToken(String sCodAlumno, String sToken, String sEstado, String cPlataforma)
        {

            string resultado = "";
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("NXTSI_GENERARTOKEN", conn);
                //OracleCommand cmd = new OracleCommand("CINFO_GENERARTOKEN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {

                    cmd.Parameters.Add("p_CodAlumno", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("p_Token", OracleDbType.Varchar2).Value = sToken;
                    cmd.Parameters.Add("p_Estado", OracleDbType.Varchar2).Value = sEstado;
                    cmd.Parameters.Add("p_Plataforma", OracleDbType.Varchar2).Value = cPlataforma;

                    cmd.Parameters.Add("o_Codigo", OracleDbType.Long, 109).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    resultado = (string)cmd.Parameters["o_Codigo"].Value.ToString();
                    oratran.Commit();

                }
                catch (Exception ex)
                {
                    oratran.Rollback();
                    cmd.Dispose();
                    conn.Close();
                    throw ex;
                }
                finally
                {
                    oratran.Dispose();

                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        #endregion
    }
}
