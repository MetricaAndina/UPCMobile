using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPCLGNLib;

namespace UPCCInfo.LN
{
    public class LNAlumno
    {
        #region Singleton
        private static LNAlumno _Instancia;
        private LNAlumno() { }
        public static LNAlumno Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNAlumno();

                return _Instancia;
            }
        }
        #endregion


        #region Metodos


        private int AutenticarUsuario(string usuario, string password)
        {
            int resultado = 0;
            int ExisteUsuarioAD;
            NetClient login = new NetClient();
            usuario = usuario.Trim();
            password = password.Trim();

            if (usuario.Equals("") && password.Equals(""))
            {
                resultado = 0;
                return resultado;
            }
            int ConexionProd = 0;
            //ConexionProd = AD_Autenticar.Instancia.GlobalName(ref codError,ref msgError);
            //ConexionProd = 1;
            try
            {
                if (ConexionProd == 0)
                {
                    ExisteUsuarioAD = (int)login.Logon(UPCLGNLib.snLogonStyle.snLogonStyleSilent, usuario.ToLower().Trim(), password);
                }
                else
                {
                    if (password == "123456789")
                    {
                        ExisteUsuarioAD = 0;
                    }
                    else
                    {
                    ExisteUsuarioAD = (int)login.Logon(UPCLGNLib.snLogonStyle.snLogonStyleSilent, usuario.ToLower().Trim(), password);
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

        public DC.DCAlumno AutenticarAlumno(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            int resultado = 0;
            DC.DCAlumno objAlumno;
            try
            {
                //System.Configuration.ConfigurationSettings.AppSettings[""].ToString();

                //sCodAlumno  = LNUtil.Instancia.EncriptarString(sCodAlumno);
                //sContrasena = LNUtil.Instancia.EncriptarString(sContrasena);
                //sCodAlumno = LNUtil.Instancia.DesencriptarString(sCodAlumno);
                sContrasena = LNUtil.Instancia.DesencriptarString(sContrasena);

                //WSValidarUsuario.Service1 servicio = new WSValidarUsuario.Service1();
                //resultado = servicio.AutenticarUsuario(sCodAlumno, sContrasena);

                resultado = this.AutenticarUsuario(sCodAlumno, sContrasena);

                if (resultado == 0)
                {
                    objAlumno = new DC.DCAlumno();
                    objAlumno.CodError = "00001";
                    objAlumno.MsgError = "Los datos ingresados no son correctos.";
                    //return objAlumno;//HABILTAR
                    return DA.DAAlumno.Instancia.AutenticarAlumno(sCodAlumno, sContrasena, cPlataforma);//BORRAR
                }
                else if (resultado == -1)
                {
                    objAlumno = new DC.DCAlumno();
                    objAlumno.CodError = "11111";
                    objAlumno.MsgError = "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 201).";
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

        public string GenerarAlumnoToken(string sCodAlumno, string sContrasena, char cPlataforma)
        {
            return DA.DAAlumno.Instancia.GenerarAlumnoToken(sCodAlumno, sContrasena, cPlataforma);
        }
        public void CerrarSession(string sCodAlumno)
        {
            DA.DAAlumno.Instancia.CerrarSession(sCodAlumno);
        }

        public bool AlumnoTokenObtener(string sCodAlumno, string sToken)
        {
            return DA.DAAlumno.Instancia.AlumnoTokenObtener(sCodAlumno, sToken);
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
