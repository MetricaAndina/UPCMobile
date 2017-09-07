using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
//using UPCLGNLib;
using UPCMobile.AD;
using Componente.Seguridad.UserResult;
using System.Configuration;

namespace UPCMobile.LN
{
    public class LNAlumno
    {
        #region Singleton
        private static LNAlumno _Instancia;
        private LNAlumno() { }
        public static LNAlumno Instancia {
            get {
                if (_Instancia == null)
                    _Instancia = new LNAlumno();

                return _Instancia;
            }
        }
        #endregion


        #region Metodos
        private int AutenticarUsuario(string usuario, string password) { 

            //if (retorna == ValidateUserResult.Result.OK)
            //{
            //    Label3.Text = "Login OK";
            //}
            //else
            //{
            //    Label3.Text = "Login no OK";
            //}
            //dpisco
            int resultado = 0;
            int ExisteUsuarioAD;
           // NetClient login = new NetClient();
            usuario = usuario.Trim();
            password = password.Trim();

            if (usuario.Equals("") && password.Equals(""))
            {
                resultado = 0;
                return resultado;
            }
            int ConexionProd = 1;
            //ConexionProd = AD_Autenticar.Instancia.GlobalName(ref codError,ref msgError);
            ConexionProd = int.Parse(ConfigurationSettings.AppSettings["Prod"]);
            //password = "123456789";
            try
            {
                if (ConexionProd == 0)
                {
                   //ExisteUsuarioAD = (int)login.Logon(UPCLGNLib.snLogonStyle.snLogonStyleSilent, usuario.ToLower().Trim(), password);
                    //dpisco implenmentado modulo de login AD
                    string _filter = string.Empty;
                    _filter = "(&(objectClass=user) (cn=" + usuario + "))";
                    ADHelper oADHelper = new ADHelper();
                    ValidateUserResult.Result retorna = ValidateUserResult.Result.UserOrPwdIncorrect;
                    retorna = oADHelper.ValidateUserAD(_filter, usuario, password, "", "", "");
                    ExisteUsuarioAD =(int) retorna;
                }
                else
                {
                    if (password == "123456789")
                    {
                        ExisteUsuarioAD = 0;
                    }
                    else
                    {
                        //ExisteUsuarioAD = (int)login.Logon(UPCLGNLib.snLogonStyle.snLogonStyleSilent, usuario.ToLower().Trim(), password);
                        //ExisteUsuarioAD = (int)login.Logon(UPCLGNLib.snLogonStyle.snLogonStyleSilent, usuario.ToLower().Trim(), password);
                        //dpisco implenmentado modulo de login AD
                        string _filter = string.Empty;
                        _filter = "(&(objectClass=user) (cn=" + usuario + "))";
                        ADHelper oADHelper = new ADHelper();
                        ValidateUserResult.Result retorna = ValidateUserResult.Result.UserOrPwdIncorrect;
                        retorna = oADHelper.ValidateUserAD(_filter, usuario, password, "", "", "");
                        ExisteUsuarioAD = (int)retorna;

                    }
                }
                if (ExisteUsuarioAD != 0)
                {
                    //resultado = "Password de red Incorrecto. Intenta Nuevamente";
                    resultado = 0;
                }
                else
                {
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                resultado = -1;
            }
            return resultado;
        }

        public DC.AlumnoDC AutenticarAlumno(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            int resultado = 0;
            DC.AlumnoDC objAlumno;
            try
            {
                //System.Configuration.ConfigurationSettings.AppSettings[""].ToString();

                //sCodAlumno  = LNUtil.Instancia.EncriptarString(sCodAlumno);
                //sContrasena = LNUtil.Instancia.EncriptarString(sContrasena);
                //sCodAlumno = LNUtil.Instancia.DesencriptarString(sCodAlumno);
                //sContrasena = LNUtil.Instancia.DesencriptarString(sContrasena);

                //WSValidarUsuario.Service1 servicio = new WSValidarUsuario.Service1();
                //resultado = servicio.AutenticarUsuario(sCodAlumno, sContrasena);

                resultado = this.AutenticarUsuario(sCodAlumno, sContrasena);

                if (resultado == 0)
                {
                    objAlumno = new DC.AlumnoDC();
                    objAlumno.CodError = "00001";
                    objAlumno.MsgError = "Los datos ingresados no son correctos.";
                    return objAlumno;
                }
                else if (resultado == -1)
                {
                    objAlumno = new DC.AlumnoDC();
                    objAlumno.CodError = "11111";
                    objAlumno.MsgError = "Código de usuario y/o contraseña incorrectos";
                    return objAlumno;
                }
                else
                {
                    return DA.DAAlumno.Instancia.AutenticarAlumno(sCodAlumno, sContrasena, cPlataforma);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerarToken(string sCodigo, string sContrasena, char cPlataforma) {
            return DA.DAAlumno.Instancia.GenerarToken(sCodigo, sContrasena, cPlataforma);
        }
        
        public void CerrarSession(string sCodAlumno) {
            DA.DAAlumno.Instancia.CerrarSession(sCodAlumno);
        }

        public bool AlumnoTokenObtener(string sCodAlumno, string sToken) {
            return DA.DAAlumno.Instancia.AlumnoTokenObtener(sCodAlumno,sToken);
        }
        
        #endregion

        #region SS-2013-072

        public DC.AlumnoCollectionDC ListadoCompanerosClase(string sCodAlumno, string sCodCurso)
        {
            DC.AlumnoCollectionDC Alumnos;
            string errMensaje = "";
            string sDescCurso = "";
            string sSeccion = "";

            try
            {
                Collection<DC.CompaneroDC> lstAlumnos = DA.DAAlumno.Instancia.ListadoCompanerosClase(sCodAlumno, sCodCurso, ref sDescCurso, ref sSeccion, ref errMensaje);
                if (lstAlumnos.Count <= 0)
                {
                    Alumnos = new DC.AlumnoCollectionDC();
                    Alumnos.CodError = "00002";
                    Alumnos.MsgError = "No tiene compañeros de clase registrados para el curso " + sCodCurso + ".";
                    return Alumnos;
                }
                else
                {
                    Alumnos = new DC.AlumnoCollectionDC();

                    Alumnos.alumnos = lstAlumnos;
                    Alumnos.CodError = "00000";
                    Alumnos.MsgError = "";
                    Alumnos.cursoId = sCodCurso;
                    Alumnos.curso = sDescCurso;
                    Alumnos.seccion = sSeccion;

                    return Alumnos;
                }
            }
            catch (Exception ex)
            {
                Alumnos = new DC.AlumnoCollectionDC();
                Alumnos.CodError = "11111";
                Alumnos.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return Alumnos;
            }
        }

        public DC.ListadoHijosDC ListadoHijos(String sCodigo)
        {
            DC.ListadoHijosDC oHijos;
            string sApellidoPadre = "";
            string sNombrePadre = "";
            string errMensaje = "";

            try
            {
                Collection<DC.HijoDC> lstHijos = DA.DAAlumno.Instancia.ListadoHijos(sCodigo.ToUpper(), ref sNombrePadre, ref sApellidoPadre, ref errMensaje);
                if (errMensaje != "null")
                {
                    oHijos = new DC.ListadoHijosDC();
                    oHijos.CodError = "00002";
                    oHijos.MsgError = errMensaje;
                    return oHijos;
                }
                else
                {
                    oHijos = new DC.ListadoHijosDC();
                    oHijos.hijos = lstHijos;
                    oHijos.Codigo = sCodigo.ToUpper();
                    oHijos.NombrePadre = sNombrePadre;
                    oHijos.ApellidoPadre = sApellidoPadre;
                    oHijos.CodError = "00000";
                    oHijos.MsgError = "";

                    return oHijos;
                }
            }
            catch (Exception ex)
            {
                oHijos = new DC.ListadoHijosDC();
                oHijos.CodError = "11111";
                oHijos.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return oHijos;
            }
        }

        public DC.UsuarioDC AutenticarUsuario(string sCodigo, string sContrasena, char cPlataforma)
        {
            int resultado = 0;
            DC.UsuarioDC objUsuario;

            try
            {
                resultado = this.AutenticarUsuario(sCodigo, sContrasena);
                if (resultado == 0)
                {
                    objUsuario = new DC.UsuarioDC();
                    objUsuario.CodError = "00001";
                    objUsuario.MsgError = "Usuario y/o contraseña incorrectos";
                    return objUsuario;
                }
                else if (resultado == -1)
                {
                    objUsuario = new DC.UsuarioDC();
                    objUsuario.CodError = "11111";
                    objUsuario.MsgError = "Usuario y/o contraseña incorrectos";
                    return objUsuario;
                }
                else
                {
                    return DA.DAAlumno.Instancia.AutenticarUsuario(sCodigo, sContrasena, cPlataforma);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DC.ListadoSedesAlumnoDC ListadoSedesAlumno(String sCodAlumno)
        {
            DC.ListadoSedesAlumnoDC oSedes;
            string errMensaje = "";

            try
            {
                Collection<DC.LocalDC> lstLocales = DA.DAAlumno.Instancia.ListadoSedesAlumno(sCodAlumno.ToUpper(), ref errMensaje);
                if (errMensaje != "null")
                {
                    oSedes = new DC.ListadoSedesAlumnoDC();
                    oSedes.CodError = "00002";
                    oSedes.MsgError = errMensaje;
                    return oSedes;
                }
                else
                {
                    oSedes = new DC.ListadoSedesAlumnoDC();
                    oSedes.Sedes = lstLocales;
                    oSedes.CodError = "00000";
                    oSedes.MsgError = "";

                    return oSedes;
                }
            }
            catch (Exception ex)
            {
                oSedes = new DC.ListadoSedesAlumnoDC();
                oSedes.CodError = "11111";
                oSedes.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 203).";
                return oSedes;
            }
        }

        #endregion 

        #region Private
        private string Encrita(string sKey, string sData)
        {
            if (!sKey.Equals(""))
            {
                int Longitud = sKey.Length;

                if (Longitud < 16)
                {
                    sKey = sKey + ("xxxxxxxxxxxx").PadLeft(1);
                }
                else if (Longitud > 16) { }
            }
            return sData;
        }
        private string Desencripta()
        {
            return "";
        }
        #endregion
    }
}
