using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using PR=DataProtector;

namespace UPCMobile.DA
{
    public class DAConexion
    {  
        public static string getConexion(){
            PR.DataProtector dp = new PR.DataProtector(PR.DataProtector.Store.USE_MACHINE_STORE);
            string strConexionEncrypt = ConfigurationManager.AppSettings["connectionString"];//VERIFICAR PROD
            //string strConexionEncrypt = ConfigurationManager.AppSettings["connectionStringUPCMovil"];//DESO
            //string strConexionEncrypt = ConfigurationManager.AppSettings["connectionStringUPCInfo"];//DESI

            byte[] strToDecrypt = Convert.FromBase64String(strConexionEncrypt);
            string cnn = Encoding.ASCII.GetString(dp.Decrypt(strToDecrypt, null));

            //return "user id=MASTER;password=ocse7tra;data source=DESO"; //DESI*/
            //return "Data source=(DESCRIPTION =(ADDRESS_LIST = (ADDRESS = (COMMUNITY = tcp.world)(PROTOCOL = TCP)(Host = 10.10.2.191)(Port = 1521)) (ADDRESS = (COMMUNITY = tcp.world)(PROTOCOL = TCP)(Host = 10.10.2.191)(Port = 1526)))(CONNECT_DATA = (SID = dese2)));User Id=master;Password=s1mps0n;";

            return cnn;
            //return "Data source=10.10.0.213/DESOE.UPC.EDU.PE;User id=master;password=ocse7tra";//DESO
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
