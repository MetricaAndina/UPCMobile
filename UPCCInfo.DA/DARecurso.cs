using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using DC = UPCCInfo.DC;
using UPCCInfo.DC;

namespace UPCCInfo.DA
{
    public class DARecurso
    {
        #region Singleton
        private static DARecurso _Instancia;
        private DARecurso() { }
        public static DARecurso Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DARecurso();
                }
                return _Instancia;
            }
        }
        #endregion

        #region Metodos


        public int ReservarRecurso(string sCodRecurso, string sAccion, string sCodAlumno, 
        string LineaNeg, string sCanHoras, string sFecReserv ,
        string sfecIni, string sfecFin ) {

            int resultado;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion())) {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("SF_VERIFICA_DISP_ASP_CINFO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                int ReturnValue;
                try
                {
                    cmd.Parameters.Add("Return_Value", OracleDbType.Long,3,"", ParameterDirection.ReturnValue); 

                    cmd.Parameters.Add("P_COD_RECURSO", OracleDbType.Long,10).Value = int.Parse(sCodRecurso);
                    cmd.Parameters.Add("P_ACCION", OracleDbType.Long,10).Value = int.Parse(sAccion);
                    cmd.Parameters.Add("P_COD_USUARIO", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("P_NUM_HORAS", OracleDbType.Long,10).Value = int.Parse(sCanHoras);
                    cmd.Parameters.Add("P_FECHA", OracleDbType.Varchar2).Value = sFecReserv;
                    cmd.Parameters.Add("P_COD_LINEA", OracleDbType.Varchar2).Value = "U";
                    cmd.Parameters.Add("P_HORA_INICIO", OracleDbType.Varchar2).Value = sfecIni;
                    cmd.Parameters.Add("P_HORA_TERMINO", OracleDbType.Varchar2).Value = sfecFin;
                    cmd.Parameters.Add("P_TEMA", OracleDbType.Varchar2).Value = "";
                    cmd.Parameters.Add("P_COD_CURSO", OracleDbType.Varchar2).Value = "";
                    cmd.Parameters.Add("P_OBSERVACION", OracleDbType.Varchar2).Value = "";
                    cmd.Parameters.Add("P_AULA", OracleDbType.Char).Value = null;

                    

                    ReturnValue = cmd.ExecuteNonQuery();
                    resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());
                    oratran.Commit();
                }
                catch (Exception ex)
                {
                    oratran.Rollback();
                    cmd.Dispose();
                    conn.Close();
                    throw ex;
                }
                finally {                    
                    cmd.Dispose();
                    conn.Close();
                }
            
            }

            return resultado;
        }

        public DCReservaRecurso ActivarReserva(string sCodRecurso,string sCodAlumno,string sToken)
        {
            int resultado;
            DCReservaRecurso oDCReservaRecurso=new DCReservaRecurso();
            String ErrMensaje = string.Empty;
            String V_RESPUESTA = string.Empty;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_CENTINFO_MOBIL.SP_ACTIVARESERVA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                int ReturnValue;
                try
                {
                    cmd.Parameters.Add("VCODRESERVA", OracleDbType.Varchar2).Value = sCodRecurso;
                    cmd.Parameters.Add("VCODUSUARIO", OracleDbType.Varchar2).Value = sCodAlumno;
                     cmd.Parameters.Add("V_RESPUESTA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje_error", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    ReturnValue = cmd.ExecuteNonQuery();

                    ErrMensaje = (string)cmd.Parameters["mensaje_error"].Value.ToString();
                     V_RESPUESTA = (string)cmd.Parameters["V_RESPUESTA"].Value.ToString();
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
                    cmd.Dispose();
                    conn.Close();
                }

            }

            return new DCReservaRecurso { CodError = V_RESPUESTA, CodRecurso = sCodRecurso, CodUsuario = sCodAlumno, Mensaje = ErrMensaje };
        }

        public DCReservaRecurso VerificaReserva(string sCodRecurso, string sCodAlumno, string sToken)
        {
            int resultado;
            DCReservaRecurso oDCReservaRecurso = new DCReservaRecurso();
            String ErrMensaje = string.Empty;
            String V_RESPUESTA = string.Empty;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_CENTINFO_MOBIL.SP_VERIFICARESERVA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                int ReturnValue;
                try
                {
                    cmd.Parameters.Add("V_CODRECURSO", OracleDbType.Varchar2).Value = sCodRecurso;
                    cmd.Parameters.Add("V_CODUSUARIO", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("V_RESPUESTA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje_error", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    ReturnValue = cmd.ExecuteNonQuery();

                    ErrMensaje = (string)cmd.Parameters["mensaje_error"].Value.ToString();
                    V_RESPUESTA = (string)cmd.Parameters["V_RESPUESTA"].Value.ToString();
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
                    cmd.Dispose();
                    conn.Close();
                }

            }

            return new DCReservaRecurso { CodError = V_RESPUESTA, CodReserva = sCodRecurso, CodUsuario = sCodAlumno, Mensaje = ErrMensaje };
        }

        public List<DC.DCRecurso> ListaRecursos(string sTipoRecurso, string sLocal, string sFecIni, string sFecFin, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion())) {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_CENTINFO_MOBIL.SP_LISTARECURSOS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("V_COD_TIPO_RECURSO", OracleDbType.Varchar2).Value = sTipoRecurso;
                    cmd.Parameters.Add("V_COD_LOCAL", OracleDbType.Varchar2).Value = sLocal;
                    cmd.Parameters.Add("V_FECHA_INICIO", OracleDbType.Varchar2).Value = sFecIni;
                    cmd.Parameters.Add("V_FECHA_FIN", OracleDbType.Varchar2).Value = sFecFin;
                    cmd.Parameters.Add("mensaje_error", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    dt.Load(cmd.ExecuteReader());
                    ErrMensaje = (string)cmd.Parameters["mensaje_error"].Value.ToString();
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    conn.Close();
                    throw ex;
                } finally{
                    cmd.Dispose();
                    conn.Close();
                }
            
            }

            return this.getListaRecurso(dt);
        }




        public List<DC.DCReservaAlumno> ListaReservaAlumno(string sFecIni, string sFecFin, string sCodAlumno,ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_CENTINFO_MOBIL.SP_LISTARESERVALUMNO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("V_COD_USUARIO", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("V_FECHA_INICIO", OracleDbType.Varchar2).Value = sFecIni;
                    cmd.Parameters.Add("V_FECHA_FIN", OracleDbType.Varchar2).Value = sFecFin;
                    cmd.Parameters.Add("MENSAJE_ERROR", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    dt.Load(cmd.ExecuteReader());
                    ErrMensaje = (string)cmd.Parameters["MENSAJE_ERROR"].Value.ToString();
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

            return this.getListaReservas(dt);
        }  
        #endregion

        #region Internal
        
        internal List<DC.DCRecurso> getListaRecurso(DataTable tb)
        {
            List<DC.DCRecurso> lista = new List<DC.DCRecurso>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaRecurso(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.DCRecurso getListaRecurso(DataRow rw)
        {
            DC.DCRecurso recurso = new DC.DCRecurso();
            recurso.CodRecurso =  rw.Field<Int32>(0); 
            recurso.NomRecurso = (string)rw.Field<string>(1); 
            recurso.Local = (string)rw.Field<string>(2); 
            recurso.FecReserva = (string)rw.Field<string>(3); 
            recurso.HoraIni = (string)rw.Field<string>(4); 
            recurso.HoraFin = (string)rw.Field<string>(5); 
            return recurso;
        }



        internal List<DC.DCReservaAlumno> getListaReservas(DataTable tb)
        {
            List<DC.DCReservaAlumno> lista = new List<DC.DCReservaAlumno>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaReservas(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.DCReservaAlumno getListaReservas(DataRow rw)
        {
            DC.DCReservaAlumno reserva = new DC.DCReservaAlumno();
            //string s = rw.Field<string>(0);
            reserva.CodReserva = rw.Field<decimal>(0);
            reserva.FecReserva = (string)rw.Field<string>(1);
            reserva.CodRecurso = rw.Field<Int32>(2);
            reserva.NomRecurso = (string)rw.Field<string>(3);
            reserva.CodTipoRecurso =(string)rw.Field<string>(4);
            reserva.DesTipoRecurso = (string)rw.Field<string>(5);
            reserva.HoraIni = (string)rw.Field<string>(6);
            reserva.HoraFin = (string)rw.Field<string>(7);
            reserva.Horas = (string)rw.Field<string>(8);
            reserva.CodEstado = (string)rw.Field<string>(9);
            reserva.DesEstado = (string)rw.Field<string>(10);
            reserva.DesLocal = (string)rw.Field<string>(11);
            reserva.Tolerancia = rw.Field<decimal>(12);
            reserva.DesTipo = (string)rw.Field<string>(13);
            return reserva;
        }
        
        #endregion 
    }
}
