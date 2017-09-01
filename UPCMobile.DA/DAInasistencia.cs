using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using DC = UPCMobile.DC;


namespace UPCMobile.DA
{
    public class DAInasistencia
    {
        #region Singleton

        private static DAInasistencia _Instancia;
        private DAInasistencia() { }
        public static DAInasistencia Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DAInasistencia();
                }
                return _Instancia;
            }
        }
        #endregion

        #region metodos
        public Collection<DC.InasistenciaDC> ObtenerInasistencias(string codUsuario, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_BUSCA_INASISTENCIA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("CodigoUsuario", OracleDbType.Varchar2).Value = codUsuario;
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

            return this.getListaInasistencia(dt);
        }
        #endregion

        #region internal
        internal Collection<DC.InasistenciaDC> getListaInasistencia(DataTable tb)
        {
            Collection<DC.InasistenciaDC> lista = new Collection<DC.InasistenciaDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaInasistencia(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.InasistenciaDC getListaInasistencia(DataRow rw)
        {
            DC.InasistenciaDC clase = new DC.InasistenciaDC();

            clase.CodCurso = (string)rw.Field<string>(0);
            clase.CodInasistencia = (Int64)rw.Field<Int64>(1);
            clase.CodAlumno = (string)rw.Field<string>(2);
            clase.CursoNombre = (string)rw.Field<string>(3);
            clase.Maximo = rw.Field<int>(4).ToString();
            clase.Total = rw.Field<int>(5).ToString();
            return clase;
        }
        #endregion
    }
}
