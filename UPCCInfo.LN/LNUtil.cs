using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace UPCCInfo.LN
{
    public class LNUtil
    {
        private static LNUtil _Instancia;
        private LNUtil() { }
        public static LNUtil Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNUtil();

                return _Instancia;
            }
        }

        private static RijndaelManaged getAESCBCCipher(byte[] keyBytes, byte[] IVBytes, PaddingMode padding)
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.KeySize = 128;
            cipher.BlockSize = 128;
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = padding;
            cipher.IV = IVBytes;
            cipher.Key = keyBytes;
            return cipher;
        }

        private byte[] decrypt(RijndaelManaged cipher, byte[] encrypted)
        {
            ICryptoTransform decryptor = cipher.CreateDecryptor();
            MemoryStream msDecrypt = new MemoryStream(encrypted);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            byte[] fromEncrypt = new byte[encrypted.Length];

            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
            return fromEncrypt;
        }

        private byte[] demo1decrypt(byte[] keyBytes, byte[] ivBytes, PaddingMode padding, byte[] encryptedMessageBytes)
        {
            RijndaelManaged decipher = getAESCBCCipher(keyBytes, ivBytes, padding);
            return decrypt(decipher, encryptedMessageBytes);
        }

        private byte[] DesencriptarArrBytes(byte[] keyBytes, byte[] ivBytes, PaddingMode padding, byte[] encryptedMessageBytes)
        {
            RijndaelManaged decipher = getAESCBCCipher(keyBytes, ivBytes, padding);
            return decrypt(decipher, encryptedMessageBytes);
        }

        public String DesencriptarString(String sTexto)
        {
            UTF8Encoding textConverter = new UTF8Encoding();

            byte[] bTextoBytes = Convert.FromBase64String(sTexto.Replace(" ", "+"));

            byte[] demoKeyBytes = textConverter.GetBytes("0000000000000000");
            byte[] demoIVBytes = textConverter.GetBytes("0000000000000000");

            PaddingMode padding = PaddingMode.PKCS7;

            byte[] demo1DecryptedBytes = demo1decrypt(demoKeyBytes, demoIVBytes, padding, bTextoBytes);
            String sPass = (textConverter.GetString(demo1DecryptedBytes.ToArray())).TrimEnd(char.Parse("\0"));

            return sPass;
        }


        public string Encrypt(string sCadena)
        {

            string LeftVal = "0000000000000000";
            string RightVal = "0000000000000000";
            string AlgoritmoHash = "SHA1";
            int Iteraccion = 2;
            int keysize = 256;


            byte[] LeftBytes = Encoding.ASCII.GetBytes(LeftVal);
            byte[] RightBytes = Encoding.ASCII.GetBytes(RightVal);
            byte[] cadenaBytes = Encoding.UTF8.GetBytes(sCadena);

            PasswordDeriveBytes password = new PasswordDeriveBytes(LeftVal, RightBytes, AlgoritmoHash, Iteraccion);
            byte[] KeyBytes = password.GetBytes(keysize / 8);

            RijndaelManaged pwdSimetrico = new RijndaelManaged();
            pwdSimetrico.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = pwdSimetrico.CreateEncryptor(KeyBytes, LeftBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(cadenaBytes, 0, cadenaBytes.Length);

            cryptoStream.FlushFinalBlock();

            byte[] cypherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cypherText = Convert.ToBase64String(cypherTextBytes);

            return cypherText;
        }




        public string EncriptarString(string sCadena)
        {

            byte[] leftBytes = Encoding.UTF8.GetBytes("0000000000000000");
            byte[] rightBytes = Encoding.UTF8.GetBytes("0000000000000000");
            byte[] cadenaBytes = Encoding.UTF8.GetBytes(sCadena);

            PaddingMode padding = PaddingMode.PKCS7;
            //RijndaelManaged rijnObj = LNUtil.getAESCBCCipher(leftBytes,rightBytes,padding,cadenaBytes);
            RijndaelManaged rijnObj = LNUtil.getAESCBCCipher(leftBytes, rightBytes, padding);


            ICryptoTransform encryptor = rijnObj.CreateEncryptor(leftBytes, rightBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(cadenaBytes, 0, cadenaBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cypherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cypherText = Convert.ToBase64String(cypherTextBytes);
            return cypherText;
        }


        public string MensajeReservaResult(int Result,string nomrecurso, string fecreserv, string horafin) {
            string Mensaje = "";
            switch (Result)
            {
                case 1:
                    Mensaje = "No se pudo generar la reserva, por favor verifique que el usuario no tenga otras reservas a esta misma hora.";
                    break;
                case 2:
                    Mensaje = "No cuenta con horas diarias disponibles para reservar el recurso.";
                    break;
                case 3:
                    Mensaje = "No cuenta con horas disponibles en esta semana para reservar el recurso.";
                    break;
                case 4:
                    Mensaje = "Usted se encuentra sancionado y no puede realizar reservas.";
                    break;
                case 5:
                    Mensaje = "No se encuentra en el intervalo de tiempo para reservar el recurso.";
                    break;
                case 6:
                    Mensaje = "No se encuentra definido el reglamento de reserva para este recurso.";
                    break;
                case 7:
                    Mensaje = "No se pudo generar la reserva. Verifique nuevamente la disponibilidad del recurso...";
                    break;
                case 8:
                    Mensaje = "No se pudo generar la reserva. Bloqueo del recurso...";
                    break;
                case 9:
                    Mensaje = "Ha ocurrido un error a la hora de determinar el bloqueo del recurso... ";
                    break;
                case 10:
                    Mensaje = "La reserva  del recurso 'xxx' registrada para el dia xxx, a las xxx horas, ha sido puesto en uso";
                    break;
                default:
                    Mensaje = "Estimado(a) alumno(a): Se ha reservado el recurso " + nomrecurso + " para el día " + fecreserv + ", a las " + horafin + ".";
                    break;
            }
            return Mensaje;
        }

    }
}
