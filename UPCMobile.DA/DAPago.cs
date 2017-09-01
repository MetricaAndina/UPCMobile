using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;


using System.Data.SqlClient;


namespace UPCMobile.DA
{
    public class DAPago
    {
        #region singleton
        private static DAPago _Instancia;
        private DAPago() { }
        public static DAPago Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new DAPago();

                return _Instancia;
            }
        }
        #endregion

        #region metodos



     


        public Collection<DC.PagoPendienteDC> ListarCuotas(string moneda, string gastoAdm,
                                    string porcMora,string MoraDia,
                                    string tipoMora,string codEquival,
                                    string codPersona,string codAlumno
                                        ) {

            //SqlConnection conn = new SqlConnection("Data Source=simbad3;Initial Catalog=UPC_DES;User ID=desarrollo;pwd=cosmo2010");

                                            //SqlConnection conn = new SqlConnection("");
            SqlConnection conn = new SqlConnection(DAConexion.getConexionSQL());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            DataTable dt  = new DataTable();

            StringBuilder sb ;
            try
            {
                sb = new StringBuilder();

                sb.Append(" SELECT TOP (5) '");
                sb.Append(codAlumno);
                sb.Append("' as Alumno,");
                sb.Append(" C.TipoDocumento,");
                sb.Append(" C.NumeroDocumento,");
                sb.Append(" case when C.MonedaDocumento = 'LO' then 'Nacional' else 'Extranjera' end as Moneda,");
                sb.Append(" C.MontoTotal + C.MontoDescuentos AS MonInicial,");
                sb.Append(" C.MontoDescuentos,");
                sb.Append(" C.MontoTotal,");
                sb.Append(" C.MontoPagado,");
                sb.Append(" C.MontoTotal - C.MontoPagado AS MonSaldo,");
                sb.Append(" C.FechaDocumento,");
                sb.Append(" C.FechaVencimiento,");

                sb.Append(" convert(decimal(18, 2),");
                sb.Append(" ROUND(C.MontoTotal - C.MontoPagado + ");
                sb.Append(" (case when (C.MONEDADOCUMENTO = 'LO' and @pMoneda = 'LO') OR ");
                sb.Append(" (C.MONEDADOCUMENTO = 'EX' and @pMoneda = 'EX') then ");
                sb.Append("  dbo.udf_Mora_Documento(C.CompaniaSocio,C.TipoDocumento,C.NumeroDocumento, ");
                sb.Append("  C.MONTOTOTAL-C.MONTOPAGADO, C.MONTOAFECTO+C.MONTONOAFECTO,  ");
                sb.Append("  C.FECHAVENCIMIENTO, getdate(), @pGasto , @pPorMora , @pMoraDia , @pTipoMora)  ");
                sb.Append(" else 0 end),2)) mo,");
                sb.Append(" CI.Cuota");
                sb.Append(" FROM CO_Documento C");
                sb.Append(" LEFT JOIN CO_InterfaseDocumento CI");
                sb.Append(" ON C.PROCESOIMPORTACIONNUMERO = CI.NUMEROINTERNO");
                sb.Append(" AND C.CompaniaSocio = CI.CompaniaSocio");
                sb.Append(" AND C.TipoDocumento = CI.TipoDocumento");
                                
                sb.Append(" WHERE (C.CompaniaSocio = @pCodEqui)    ");
                sb.Append(" AND (C.ClienteNumero = @pCodPers)");
                sb.Append(" AND (C.MontoPagado <> C.MontoTotal)");
                sb.Append(" AND (C.Estado IN ('PR', 'CA'))");
                sb.Append(" AND (C.TipoDocumento IN ('BV', 'FC', 'LC'))");
                //sb.Append(" AND (CONVERT(CHAR, C.FechaVencimiento, 112) < CONVERT(varchar, GETDATE(), 112))");
                sb.Append(" ORDER BY C.FechaVencimiento DESC");
        
                cmd.CommandText = sb.ToString();
                conn.Open();


                cmd.Parameters.Add("@pMoneda", SqlDbType.Char, 20).Value = moneda;
                cmd.Parameters.Add("@pGasto", SqlDbType.Char, 30).Value = gastoAdm;
                cmd.Parameters.Add("@pPorMora", SqlDbType.Char, 30).Value = porcMora.Replace(",",".");
                cmd.Parameters.Add("@pMoraDia", SqlDbType.Char, 30).Value = MoraDia;
                cmd.Parameters.Add("@pTipoMora", SqlDbType.Char, 30).Value = tipoMora;
                cmd.Parameters.Add("@pCodEqui", SqlDbType.Char, 30).Value = codEquival;
                cmd.Parameters.Add("@pCodPers", SqlDbType.Char, 30).Value = codPersona;
                //cmd.Parameters.Add("@pCodPers", SqlDbType.Char, 30).Value = (codPersona == "466061" ? "593170" : codPersona);



                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return this.getListaPago(dt);
        }

        public void Parametros_Pago(string codUsuario, ref string gastoAdm,
                                     ref string porcMora, ref string moraDiaria,
                                     ref string tipoMora, ref string Moneda,
                                     ref string tipos, ref string comp,
                                     ref string persona, ref string mensaje)
        {
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_OBT_PARAM_PAGOS_PEND", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;

                try
                {
                    cmd.Parameters.Add("CODIGOUSUARIO", OracleDbType.Varchar2).Value = codUsuario;


                    cmd.Parameters.Add("RET_GASTO_ADM", OracleDbType.Char, 10).Direction = ParameterDirection.Output; ;
                    cmd.Parameters.Add("RET_PORC_MORA", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_MORA_DIARIA", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_TIPO_MORA", OracleDbType.Char, 2).Direction = ParameterDirection.Output; ;
                    cmd.Parameters.Add("RET_MONEDA", OracleDbType.Char, 2).Direction = ParameterDirection.Output; ;
                    cmd.Parameters.Add("RET_TIPOS", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_COMP", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_PERSONA", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output; ;
                    cmd.Parameters.Add("RET_MENSAJE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    gastoAdm = (string)cmd.Parameters["RET_GASTO_ADM"].Value.ToString();
                    porcMora = (string)cmd.Parameters["RET_PORC_MORA"].Value.ToString();
                    moraDiaria = (string)cmd.Parameters["RET_MORA_DIARIA"].Value.ToString();
                    tipoMora = (string)cmd.Parameters["RET_TIPO_MORA"].Value.ToString();
                    Moneda = (string)cmd.Parameters["RET_MONEDA"].Value.ToString();
                    tipos = (string)cmd.Parameters["RET_TIPOS"].Value.ToString();
                    comp = (string)cmd.Parameters["RET_COMP"].Value.ToString();
                    persona = (string)cmd.Parameters["RET_PERSONA"].Value.ToString();
                    mensaje = (string)cmd.Parameters["RET_MENSAJE"].Value.ToString();

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
        }

        #endregion

        #region internal

        internal Collection<DC.PagoPendienteDC> getListaPago(DataTable tb)
        {
            Collection<DC.PagoPendienteDC> lista = new Collection<DC.PagoPendienteDC>();
            foreach (DataRow row in tb.Rows)
            {
                lista.Add(getListaPago(row));
            }
            tb.Dispose();
            return lista;
        }

        internal DC.PagoPendienteDC getListaPago(DataRow rw)
        {
            DC.PagoPendienteDC cuota = new DC.PagoPendienteDC();

            //cuota.CodPagoPendiente = 0;
            cuota.CodAlumno = rw.Field<string>(0);
            
            cuota.TipoDocumento = rw.Field<string>(1);
            cuota.NroDocumento = rw.Field<string>(2);
            cuota.Moneda = rw.Field<string>(3);
            cuota.Importe = rw.Field<decimal>(4).ToString() ;
            cuota.Descuento = rw.Field<decimal>(5).ToString();
            cuota.Impuesto = "0";
            cuota.ImporteCancelado = rw.Field<decimal>(7).ToString();
            cuota.Saldo = rw.Field<decimal>(8).ToString();
            cuota.FecEmision = rw.Field<DateTime>(9).ToString("yyyyMMdd");
            cuota.FecVencimiento = rw.Field<DateTime>(10).ToString("yyyyMMdd");

            cuota.Mora = (rw.Field<decimal>(11) - rw.Field<decimal>(8)).ToString();
            cuota.Total = rw.Field<decimal>(11).ToString();
            cuota.NroCuota = int.Parse((rw.Field<string>(12) == null || rw.Field<string>(12).Equals("")) ? "0" : rw.Field<string>(12));
            
            return cuota;
        }
         
        #endregion
    }
}