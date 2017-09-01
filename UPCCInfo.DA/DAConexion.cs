using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using PR = DataProtector;

namespace UPCCInfo.DA
{
    public class DAConexion
    {
        public static string getConexion()
        {
            PR.DataProtector dp = new PR.DataProtector(PR.DataProtector.Store.USE_MACHINE_STORE);
            //string strConexionEncrypt = ConfigurationManager.AppSettings["connectionStringCinfo"];
            //cambiar para produccion
            string strConexionEncrypt = ConfigurationManager.AppSettings["connectionStringUPCMovil"];
            byte[] strToDecrypt = Convert.FromBase64String(strConexionEncrypt);
            string cnn = Encoding.ASCII.GetString(dp.Decrypt(strToDecrypt, null));
            return cnn;
        }

        public static string getConexionSQL()
        {
            PR.DataProtector dp = new PR.DataProtector(PR.DataProtector.Store.USE_MACHINE_STORE);
            string strConexionEncrypt = ConfigurationManager.AppSettings["ConnectionSPRING"];
            byte[] strToDecrypt = Convert.FromBase64String(strConexionEncrypt);
            string cnn = Encoding.ASCII.GetString(dp.Decrypt(strToDecrypt, null));
            return cnn;
        }
    }
}
