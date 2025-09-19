using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Application.Common
{
    public class clsEncryptDecrypt
    {
        private static string theKey = "P0teNt!@D$e&v*L0p%Me#nT!";

        private static string EncryptProcess(string parameter)
        {
            byte[] theInput = UTF8Encoding.UTF8.GetBytes(parameter);
            TripleDESCryptoServiceProvider theTripleDES = new TripleDESCryptoServiceProvider();
            theTripleDES.Key = UTF8Encoding.UTF8.GetBytes(theKey);
            theTripleDES.Mode = CipherMode.ECB;
            theTripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform theTransformer = theTripleDES.CreateEncryptor();
            byte[] theResult = theTransformer.TransformFinalBlock(theInput, 0, theInput.Length);
            theTripleDES.Clear();
            return Convert.ToBase64String(theResult, 0, theResult.Length);
        }

        private static string DecryptProcess(string parameter)
        {
            byte[] theInput = Convert.FromBase64String(parameter);
            TripleDESCryptoServiceProvider theTripleDES = new TripleDESCryptoServiceProvider();
            theTripleDES.Key = UTF8Encoding.UTF8.GetBytes(theKey);
            theTripleDES.Mode = CipherMode.ECB;
            theTripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform theTransformer = theTripleDES.CreateDecryptor();
            byte[] theResult = theTransformer.TransformFinalBlock(theInput,0,theInput.Length);
            theTripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(theResult);
        }

        public static string Encrypt(string theString)
        {
            return EncryptProcess(theString);
        }

        public static string Decrypt(string theString)
        {
            return DecryptProcess(theString);
        }

        public static string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); 
            return path.Substring(0, 8);
        }
    }
}
