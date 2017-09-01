using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace UPCMobile.DA
{
    public class DANota
    {
        #region singleton
        private static DANota _Instancia;
        private DANota() { }
        public static DANota Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DANota();
                }
                return _Instancia;
            }
        }
        #endregion

        #region metodos

        public Collection<DC.NotaDC> getNotas(string codUsuario,string codCurso, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();

            //DAConexion x = new DAConexion();

            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_BUSCA_NOTACURSOS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("CodigoUsuario", OracleDbType.Varchar2).Value = codUsuario;
                    cmd.Parameters.Add("codigocurso", OracleDbType.Varchar2).Value = codCurso;
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
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

            //return this.getListaCurso(dt);

            return this.getListaNotas(dt);
        }

        #endregion

        #region SS-2013-072

        public bool ValidaProfesorAlumno(string codUsuario, string codAlumno)
        {
            bool resultado;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_VALIDAPROFALUMNO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2, 20).Value = codUsuario.ToUpper();
                    cmd.Parameters.Add("PCOD_ALUMNO", OracleDbType.Varchar2, 20).Value = codAlumno.ToUpper();
                    cmd.Parameters.Add("PVALIDO", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    resultado = (cmd.Parameters["PVALIDO"].Value.ToString().Equals("SI") ? true : false);
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

        public bool ValidaProfesorCurso(string codUsuario, string codAlumno, string codCurso)
        {
            bool resultado;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_VALIDAPROFCURSO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2, 20).Value = codUsuario.ToUpper();
                    cmd.Parameters.Add("PCOD_CURSO", OracleDbType.Varchar2, 10).Value = codCurso.ToUpper();
                    cmd.Parameters.Add("PCOD_ALUMNO", OracleDbType.Varchar2, 20).Value = codAlumno.ToUpper();
                    cmd.Parameters.Add("PVALIDO", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    resultado = (cmd.Parameters["PVALIDO"].Value.ToString().Equals("SI") ? true : false);
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

        public bool ValidaHijoPadre(string codUsuario, string codAlumno)
        {
            bool resultado;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_VALIDAHIJOPADRE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2, 20).Value = codUsuario.ToUpper();
                    cmd.Parameters.Add("PCOD_ALUMNO", OracleDbType.Varchar2, 20).Value = codAlumno.ToUpper();
                    cmd.Parameters.Add("PVALIDO", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PCOD_ERROR", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    resultado = (cmd.Parameters["PVALIDO"].Value.ToString().Equals("SI") ? true : false);
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

        #region internal

        internal Collection<DC.NotaDC> getListaNotas(DataTable tb)
        {
            Collection<DC.NotaDC> lista = new Collection<DC.NotaDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaNotas(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.NotaDC getListaNotas(DataRow rw)
        {
            DC.NotaDC Nota = new DC.NotaDC();

            Nota.CodNota = 0;
            Nota.CodCurso = (string)rw.Field<string>(0);
            Nota.CodAlumno = (string)rw.Field<string>(1);//no serializable
            Nota.NomCurso = (string)rw.Field<string>(2);
            Nota.NombreEvaluacion= (string)rw.Field<string>(3);
            Nota.NombreCorto = (string)rw.Field<string>(4);
            Nota.NroEvaluacion = Int16.Parse(rw.Field<decimal>(5).ToString());
            Nota.Peso = rw.Field<string>(6)+"%";
            Nota.Valor = rw.Field<string>(7).Replace(",",".");
            Nota.Formula = rw.Field<string>(8);//no serializable
            
            return Nota;
        }
        #endregion
    }
}
