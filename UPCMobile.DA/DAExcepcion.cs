using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using DC = UPCMobile.DC;

namespace UPCMobile.DA
{
    public class DAExcepcion
    {
        #region singleton
        private static DAExcepcion _Instancia;
        private DAExcepcion() { }
        public static DAExcepcion Instancia {
            get {
                if (_Instancia == null)
                    _Instancia = new DAExcepcion();

                return _Instancia;
            }
        }
        #endregion

        #region metodos


        public void RegistrarExcepcion(   int flag,
                                                                String sCodAlumno, String sToken, 
                                                                String sMensaje, String sStacktrace, 
                                                                String sFecha, String sHora, Char cPlataforma)
        {
            
            string resultado = "";
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("P_MANTEXCEPCION", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("p_Flag", OracleDbType.Varchar2).Value = flag;
                    cmd.Parameters.Add("p_CodAlumno", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("p_Token", OracleDbType.Varchar2).Value = sToken;
                    cmd.Parameters.Add("p_Mensaje", OracleDbType.Varchar2).Value = sMensaje;
                    cmd.Parameters.Add("p_StrackTrace", OracleDbType.Varchar2).Value = sStacktrace;
                    cmd.Parameters.Add("p_Fecha", OracleDbType.Varchar2).Value = sFecha;
                    cmd.Parameters.Add("p_Hora", OracleDbType.Varchar2).Value = sHora;
                    cmd.Parameters.Add("p_Plataforma", OracleDbType.Varchar2).Value = cPlataforma;

                    cmd.Parameters.Add("p_CodErr", OracleDbType.Long,109).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    resultado = (string)cmd.Parameters["p_CodErr"].Value.ToString();
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

        

        #region internal
        #endregion
    }
}
