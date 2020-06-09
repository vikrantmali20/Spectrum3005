using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Spectrum.Cryptography
{
    public static  class Encrypter
    {
         
        private static   byte[] GetEncryptedData(byte[] salt, string PlainPassword)
        {
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainPassword);
            byte[] plainTextWithSaltBytes = new byte[salt.Length + (PlainTextBytes.Length - 1) + 1];
            for (int i = 0; i <= salt.Length - 1; i++)
            {
                plainTextWithSaltBytes[i] = salt[i];
            }
            for (int i = 0; i <= PlainTextBytes.Length - 1; i++)
            {
                plainTextWithSaltBytes[salt.Length + i] = PlainTextBytes[i];
            }

            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            byte[] data = md5Hasher.ComputeHash(plainTextWithSaltBytes);

            return data;
        }
        private static byte[] getSalt(string encriptedPassword)
        {
            byte[] salt = new byte[12];

            if (string.IsNullOrEmpty(encriptedPassword))
            {
                RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
                salt = new byte[12];
                random.GetNonZeroBytes(salt);
            }
            else
            {
                string[] NwEncryptedPassword = encriptedPassword.Split(new string[] { ":-:" }, StringSplitOptions.RemoveEmptyEntries);
                string[] SaltArry = NwEncryptedPassword[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i <= SaltArry.Length - 1; i++)
                {
                    if ((Convert.ToInt32(SaltArry[i]) > 0))
                    {
                        salt[i] = Convert.ToByte(Convert.ToInt32(SaltArry[i]));
                    }
                    else
                    {
                        salt[i] = BitConverter.GetBytes(Convert.ToInt32(SaltArry[i]))[0];
                    }
                }
            }
            return salt;
        }
        public static  string getEncryptedPassword(string password)
        {

            try
            {
                byte[] salt = getSalt(null);


                byte[] data = GetEncryptedData(salt, password);

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i <= salt.Length - 1; i++)
                {
                    sBuilder.Append(Convert.ToInt32(salt[i]) + ",");
                }

                sBuilder.Append(":-:");

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i <= data.Length - 1; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
 
}
