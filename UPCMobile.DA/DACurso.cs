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
    public class DACurso
    {
        #region Singleton
        private static DACurso _Instancia;
        private DACurso() { }
        public static DACurso Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    _Instancia = new DACurso();
                }
                return _Instancia;
            }
        }
        #endregion

        #region Metodos

        public Collection<DC.CursoAlumnoDC> ObtenerCursos(string codUsuario, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_BUSCA_CURSO", conn);
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

            return this.getListaCurso(dt);//this.getListaClase(dt);
        }



        #endregion

        #region Internal

        internal Collection<DC.CursoAlumnoDC> getListaCurso(DataTable tb)
        {
            Collection<DC.CursoAlumnoDC> lista = new Collection<DC.CursoAlumnoDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaCurso(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.CursoAlumnoDC getListaCurso(DataRow rw)
        {
            DC.CursoAlumnoDC curso = new DC.CursoAlumnoDC();
            curso.CodCurso = (string)rw.Field<string>(0);
            curso.CodAlumno = (string)rw.Field<string>(1);
            curso.CursoNombre = (string)rw.Field<string>(2);
            return curso;
        }


        #endregion

    }
}
