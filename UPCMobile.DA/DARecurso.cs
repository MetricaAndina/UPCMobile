using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using DC = UPCMobile.DC;
using UPCMobile.DC;

namespace UPCMobile.DA
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

        #region SS-2013-072

        public Collection<SedeDC> PoblarEspaciosDeportivos(String sCodAlumno, ref String sMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_POBLARESPACIOS_DEP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodAlumno.ToUpper();
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    sMensaje = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();
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

            return this.getListaSedes(dt);
        }

        public Collection<DC.DiaLibreEDDC> DisponibilidadEspacioDeportivo(String sCodSede, String sCodED, String sNumHoras, String sCodAlumno, String sFechaIni, String sFechaFin, ref String errMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_BUSCADISPONIBED", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PHLD_CODLOZADEP", OracleDbType.Int32).Value = sCodED;
                    cmd.Parameters.Add("PFECHA1", OracleDbType.Varchar2, 20).Value = sFechaIni;
                    cmd.Parameters.Add("PFECHA2", OracleDbType.Varchar2, 20).Value = sFechaFin;
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodAlumno.ToUpper();
                    cmd.Parameters.Add("PCOD_SEDE", OracleDbType.Char, 1).Value = sCodSede;
                    cmd.Parameters.Add("PNUM_HORAS", OracleDbType.Int32).Value = sNumHoras;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    dt.Load(cmd.ExecuteReader());

                    errMensaje = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();
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

            return this.getListaDiasLibres(dt);
        }

        public ReservaEspacioDepDC ReservarEspacioDeportivo(String sCodSede, String sCodED, String sCodActiv, String sNumHoras, String sCodAlumno, String sFecha, String sHoraIni, String sHoraFin, String sDetalles, ref String errMensaje)
        {
            ReservaEspacioDepDC obj;

            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion()))
            {
                conn.Open();

                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_INTER_MOBILUPC.SP_RESERVA_ED", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("PCOD_SEDE", OracleDbType.Char, 1).Value = sCodSede;
                    cmd.Parameters.Add("PHLD_CODLOZADEP", OracleDbType.Int32).Value = sCodED;
                    cmd.Parameters.Add("PCOD_ACTIVIDAD", OracleDbType.Int32).Value = sCodActiv;
                    cmd.Parameters.Add("PNUM_HORAS", OracleDbType.Int32).Value = sNumHoras;
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2, 10).Value = sCodAlumno.ToUpper();
                    cmd.Parameters.Add("PFECHA", OracleDbType.Varchar2, 20).Value = sFecha;
                    cmd.Parameters.Add("PHORA1", OracleDbType.Varchar2, 10).Value = sHoraIni;
                    cmd.Parameters.Add("PHORA2", OracleDbType.Varchar2, 10).Value = sHoraFin;
                    cmd.Parameters.Add("PDETALLES", OracleDbType.Varchar2, 200).Value = sDetalles;
                    cmd.Parameters.Add("PESTADO", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("PMSG_ERROR", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    obj = new ReservaEspacioDepDC();
                    obj.CodAlumno = sCodAlumno;
                    obj.MsgError = (string)cmd.Parameters["PMSG_ERROR"].Value.ToString();
                    obj.Estado = (string)cmd.Parameters["PESTADO"].Value.ToString();
                    obj.CodError = (obj.Estado == "R") ? "00000" : "11111";
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    conn.Close();
                    obj = new ReservaEspacioDepDC();
                    obj.CodError = "11111";
                    obj.MsgError = ex.Message;
                    obj.CodAlumno = sCodAlumno;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

            return obj;
        }

        #endregion 

        #region Metodos

        //        public int ReservarRecurso(string sCodRecurso, string sAccion, string sCodAlumno, 
        public DCReservaRecurso ReservarRecurso(string sCodRecurso, string sAccion, string sCodAlumno, 
        string LineaNeg, string sCanHoras, string sFecReserv ,
        string sfecIni, string sfecFin ) {
            DCReservaRecurso oDCReservaRecurso=new DCReservaRecurso();
            int resultado;
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion())) {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("SF_VERIFICA_DISP_ASP_CINFO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                int ReturnValue;
                string V_RESPUESTA;
                string V_WMONTODEUDA;
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
                    cmd.Parameters.Add("WRESERVA", OracleDbType.Int32, 7).Direction = ParameterDirection.Output;

                    ReturnValue = cmd.ExecuteNonQuery();
                    resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());
                    V_RESPUESTA = (string)cmd.Parameters["WRESERVA"].Value.ToString();//DEVUELVE 0 CUANDO NO RECUPERA EL NUMERO DE RESERVA
                    
                    oratran.Commit();
                    oDCReservaRecurso.CodReserva = V_RESPUESTA;
                    oDCReservaRecurso.CodUsuario = sCodAlumno;
                    oDCReservaRecurso.CodError = resultado.ToString();
                    
                    if (resultado==11)
                    {
                        OracleCommand oCmd = new OracleCommand();
                        oCmd.CommandType = CommandType.Text;
                        oCmd.Connection = conn;
                        oCmd.CommandText = "SELECT PQ_FACTURACION.SF_ESMOROSO ('U','" + sCodAlumno + "', TO_CHAR(SYSDATE,'YYYY.MM.DD HH24:MI:SS')) AS MOROSO FROM DUAL";

                        using (IDataReader oReader = oCmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oDCReservaRecurso.MsgError = oReader["MOROSO"].ToString();
                            }

                        }
                    }
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

            //return resultado;
            return oDCReservaRecurso;
        }

        public DCReservaRecurso ActivarReserva(string sCodReserva, string sCodAlumno, string sCodAlumno2, string sToken)
        {
            int resultado;
            //DCReservaRecurso oDCReservaRecurso=new DCReservaRecurso();
            string ErrMensaje = string.Empty;
            string V_RESPUESTA = "00000";//no se realiza activacion
            string V_CODRECURSO = string.Empty;
            //string cr = string.Empty;

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
                    /* 
                    Nombre: ActivarReserva
                    ********************************************************** 
                      Ticket :  CSC-00263381-00
                      Responsable : Juan Carlos Castro Socla 
                      Fecha : 20/02/2017
                      Funcionalidad: Validación para que no se visualice NULL al retornar las variables, se amplio la longitud del mensaje error.
                    */
                    cmd.Parameters.Add("VCODRESERVA", OracleDbType.Varchar2).Value = sCodReserva;
                    cmd.Parameters.Add("VCODUSUARIO", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("VCODUSUARIO2", OracleDbType.Varchar2).Value = sCodAlumno2;
                    cmd.Parameters.Add("V_RESPUESTA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje_error", OracleDbType.Varchar2, 250).Direction = ParameterDirection.Output; //CSC-00263381-00
                    cmd.Parameters.Add("V_CODRECURSO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                                        
                    ReturnValue = cmd.ExecuteNonQuery();

                    ErrMensaje = (string)cmd.Parameters["mensaje_error"].Value.ToString();
                     V_RESPUESTA = (string)cmd.Parameters["V_RESPUESTA"].Value.ToString();
                     V_CODRECURSO = (string)cmd.Parameters["V_CODRECURSO"].Value.ToString();
                     if (V_RESPUESTA == "1")
                     {
                         V_RESPUESTA = "00000";//no hay error
                     }
                     else {
                         V_RESPUESTA = "00002";//no se realiza activacion
                     }
                    oratran.Commit();
                }
                catch (Exception ex)
                {
                    
                    oratran.Rollback();
                    cmd.Dispose();
                    conn.Close();
                    return new DCReservaRecurso { CodError = V_RESPUESTA };
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }

            }

            return new DCReservaRecurso { CodError = V_RESPUESTA, CodRecurso = V_CODRECURSO, CodUsuario = sCodAlumno, MsgError = ErrMensaje, CodReserva = sCodReserva };
        }

        public DCReservaRecurso VerificaReserva(string sCodReserva, string sCodAlumno, string sToken)
        {
            int resultado;
            DCReservaRecurso oDCReservaRecurso = new DCReservaRecurso();
            String ErrMensaje = string.Empty;
            String V_RESPUESTA = string.Empty;
            String V_CODRECURSO = string.Empty;
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
                    cmd.Parameters.Add("V_CODRESERVA", OracleDbType.Varchar2).Value = sCodReserva;
                    cmd.Parameters.Add("V_CODUSUARIO", OracleDbType.Varchar2).Value = sCodAlumno;
                    cmd.Parameters.Add("V_RESPUESTA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje_error", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("V_CODRECURSO", OracleDbType.Varchar2,100).Direction = ParameterDirection.Output;
                    
                    ReturnValue = cmd.ExecuteNonQuery();

                    ErrMensaje = (string)cmd.Parameters["mensaje_error"].Value.ToString();
                    V_RESPUESTA = (string)cmd.Parameters["V_RESPUESTA"].Value.ToString();
                    V_CODRECURSO = cmd.Parameters["V_CODRECURSO"].Value.ToString();
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

            return new DCReservaRecurso { CodError = V_RESPUESTA, CodReserva = sCodReserva, CodUsuario = sCodAlumno, MsgError = ErrMensaje, CodRecurso = V_CODRECURSO.ToString() };
        }

        public List<DC.DCRecurso> ListaRecursos(string sTipoRecurso, string sLocal, string sFecIni, string sFecFin, string canHoras, string sCodUsuario, ref string ErrMensaje)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(DAConexion.getConexion())) {
                conn.Open();
                OracleTransaction oratran = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand("PQ_CENTINFO_MOBIL.SP_LISTARECURSOS_XTRA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oratran;
                try
                {
                    cmd.Parameters.Add("V_COD_TIPO_RECURSO", OracleDbType.Varchar2).Value = sTipoRecurso;
                    cmd.Parameters.Add("V_COD_LOCAL", OracleDbType.Varchar2).Value = sLocal;
                    cmd.Parameters.Add("V_FECHA_INICIO", OracleDbType.Varchar2).Value = sFecIni;
                    cmd.Parameters.Add("V_FECHA_FIN", OracleDbType.Varchar2).Value = sFecFin;
                    cmd.Parameters.Add("PCOD_USUARIO", OracleDbType.Varchar2).Value = sCodUsuario;
                    cmd.Parameters.Add("PNUM_HORAS", OracleDbType.Int32).Value = Convert.ToInt32(canHoras);
                    cmd.Parameters.Add("MENSAJE_ERROR", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RET_O_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    dt.Load(cmd.ExecuteReader());
                    ErrMensaje = (string)cmd.Parameters["MENSAJE_ERROR"].Value.ToString();
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

        internal Collection<DC.DiaLibreEDDC> getListaDiasLibres(DataTable tb)
        {
            Collection<DC.DiaLibreEDDC> lista = new Collection<DC.DiaLibreEDDC>();
            DiaLibreEDDC nodo;
            String valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(4))
                {
                    nodo = new DiaLibreEDDC();
                    nodo.CodDia = equivalenteDia((string)row.Field<string>(1));
                    nodo.Fecha = (string)row.Field<string>(4);
                    nodo.Disponibles = getListaDetallePorDia(tb, nodo.Fecha);
                    valor = (string)row.Field<string>(4);

                    lista.Add(nodo);
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<HorasLibresEDDC> getListaDetallePorDia(DataTable tb, string fecha)
        {
            Collection<HorasLibresEDDC> lista = new Collection<HorasLibresEDDC>();
            HorasLibresEDDC nodo;
            //String valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if(row.Field<string>(4) == fecha)
                {
                    nodo = new HorasLibresEDDC();
                    nodo.Fecha = (string)row.Field<string>(4);
                    nodo.HoraFin = (string)row.Field<string>(3);
                    nodo.HoraInicio = (string)row.Field<string>(2);
                    nodo.Sede = (string)row.Field<string>(5);

                    lista.Add(nodo);
                }
            }
            tb.Dispose();
            return lista;
        }

        internal String equivalenteDia(String dia) {
            switch(dia) 
            {
                case "LU": 
                    return "1";
                case "MA":
                    return "2";
                case "MI":
                    return "3";
                case "JU":
                    return "4";
                case "VI":
                    return "5";
                case "SA":
                    return "6";
                default:
                    return "7";
            }
        }

        internal Collection<DC.SedeDC> getListaSedes(DataTable tb)
        {
            Collection<DC.SedeDC> lista = new Collection<DC.SedeDC>();
            SedeDC nodo;
            string valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != row.Field<string>(0))
                {
                    nodo = new SedeDC();
                    nodo.sede = (string)row.Field<string>(1);
                    nodo.key = (string)row.Field<string>(0);
                    nodo.espacios = getListaEspacios(tb, nodo.key);
                    valor = nodo.key;

                    lista.Add(nodo);
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<EspacioDC> getListaEspacios(DataTable tb, string sedeID)
        {
            Collection<EspacioDC> lista = new Collection<EspacioDC>();
            EspacioDC nodo;
            string valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != Convert.ToString(row.Field<Int32>(2)))
                {
                    if (row.Field<string>(0) == sedeID)
                    {
                        nodo = new EspacioDC();
                        nodo.nombre = (string)row.Field<string>(3);
                        nodo.codigo = Convert.ToString((Int32)row.Field<Int32>(2));
                        nodo.actividades = getListaActividades(tb, nodo.codigo);
                        valor = nodo.codigo;

                        lista.Add(nodo);
                    }
                }
            }
            tb.Dispose();
            return lista;
        }

        internal Collection<ActividadDC> getListaActividades(DataTable tb, string espacioID)
        {
            Collection<ActividadDC> lista = new Collection<ActividadDC>();
            ActividadDC nodo;
            string valor = "";

            foreach (DataRow row in tb.Rows)
            {
                if (valor != Convert.ToString(row.Field<Int32>(4)))
                {
                    if (Convert.ToString(row.Field<Int32>(2)) == espacioID)
                    {
                        nodo = new ActividadDC();
                        nodo.nombre = (string)row.Field<string>(5);
                        nodo.codigo = Convert.ToString((Int32)row.Field<Int32>(4));
                        valor = nodo.codigo;

                        lista.Add(nodo);
                    }
                }
            }
            tb.Dispose();
            return lista;
        }
        
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
            // agregar campo Estado ***********************
            // CSC-00262418-00 RTELLO 08-05-2015
            if (Convert.ToInt32(rw.Field<Object>(6)) == 0)
            {
                recurso.Estado = true;
            }
            else
            {
                recurso.Estado = false;
            }
            // agregar campo Estado ***********************
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
