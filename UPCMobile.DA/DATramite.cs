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
    public class DATramite
    {
        #region singleton
        private static DATramite _Instancia;
        private DATramite() { }
        public static DATramite Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DATramite();
                }
                return _Instancia;
            }
        }
        #endregion

        #region metodos

    
        public Collection<DC.TramiteRealizadoDC> getTramites(string codUsuario,ref string ErrMensaje)
        {

            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_OBTIENE_TRAMITES", conn);
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

            //return this.getListaCurso(dt);
            return this.getListaTramite(dt);
        }



        #endregion

        #region internal

        internal Collection<DC.TramiteRealizadoDC> getListaTramite(DataTable tb)
        {
            Collection<DC.TramiteRealizadoDC> lista = new Collection<DC.TramiteRealizadoDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaTramite(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.TramiteRealizadoDC getListaTramite(DataRow rw)
        {
            DC.TramiteRealizadoDC Tramite = new DC.TramiteRealizadoDC();

            Tramite.CodTramiteRealizado = rw.Field<Int64>(0);
            Tramite.CodAlumno = (string)rw.Field<string>(1);
            Tramite.NroSolicitud = (string)rw.Field<string>(2);//no serializable
            Tramite.Nombre = (string)rw.Field<string>(3);
            Tramite.Fecha = (string)rw.Field<string>(4);
            Tramite.Estado = (string)rw.Field<string>(5);
            return Tramite;
        }
        #endregion
    }
}
