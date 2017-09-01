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
    public class DAHorario
    {
        #region Singleton
        private static DAHorario _Instancia;
        private DAHorario() { }
        public static DAHorario Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DAHorario();
                }
                return _Instancia;
            }
        }
        #endregion

        #region Metodos

        public Collection<DC.ClaseDC> ObtenerClases(string codUsuario, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_BUSCA_HORARIO", conn);
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

            return this.getListaClase(dt);
        }

        #endregion

        #region internal
        internal Collection<DC.ClaseDC> getListaClase(DataTable tb)
        {
            Collection<DC.ClaseDC> lista = new Collection<DC.ClaseDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaClase(row));
            }
            tb.Dispose();
            return lista;
        }
        internal DC.ClaseDC getListaClase(DataRow rw)
        {
            DC.ClaseDC clase = new DC.ClaseDC();

            clase.CodClase = (Int64)rw.Field<Int64>(0);
            clase.CodAlumno = (string)rw.Field<string>(1);
            clase.CodCurso = (string)rw.Field<string>(2);
            clase.CursoNombre = (string)rw.Field<string>(3);
            clase.CursoNombreCorto = (string)rw.Field<string>(4);
            clase.Fecha = (string)rw.Field<string>(5);
            clase.HoraInicio = (string)rw.Field<string>(6);
            clase.HoraFin = (string)rw.Field<string>(7);
            clase.Sede = (string)rw.Field<string>(8);
            clase.Seccion = (string)rw.Field<string>(9);
            clase.Salon = (string)rw.Field<string>(10);
           
            return clase;
        }
        #endregion

    }
}
