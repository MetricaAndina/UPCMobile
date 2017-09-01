using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using DC = UPCCInfo.DC;


namespace UPCCInfo.DA
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

        public DC.DCAlumno AutenticarAlumno(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            return this.getAlumno(sCodAlumno);
        }

        public string GenerarAlumnoToken(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            string token = Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.ToString("yyyyMMddhhmmss");
            this.MantToken(sCodAlumno, token, "I", cPlataforma.ToString());
            return token;
        }

        public void CerrarSession(string sCodAlumno)
        {
            this.MantToken(sCodAlumno, "", "C", "");

        }

        public bool AlumnoTokenObtener(string sCodAlumno, string sToken)
        {
            bool resultado;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("CINFO_OBTENERTOKEN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {

                    cmd.Parameters.Add("p_CodAlumno", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("p_Token", OracleDbType.Varchar2).Value = sToken;


                    cmd.Parameters.Add("o_Codigo", OracleDbType.Long, 109).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    resultado = (cmd.Parameters["o_Codigo"].Value.ToString().Equals("1") ? true : false);

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

        #region Privados

        private DC.DCAlumno getAlumno(string sCodAlumno)
        {
            DC.DCAlumno objAlumnoDC;
            string mensaje = "";
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("PQ_CENTINFO_MOBIL.SP_DATOS_ALUMNO", conn);
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
                        objAlumnoDC = new DC.DCAlumno();
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

        private void MantToken(String sCodAlumno, String sToken,
                                String sEstado, String cPlataforma)
        {

            string resultado = "";
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("CINFO_GENERARTOKEN", conn);
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
