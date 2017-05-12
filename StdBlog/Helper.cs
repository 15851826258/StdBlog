using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace StdBlog.Helper
{
    public class AESHelper
    {

        public static string Encrypt(string str)
        {
            var aes = Aes.Create();
            byte[] myiv = new byte[16];

            //init iv
            for (int i = 0; i < 16; i++) myiv[i] = (byte)i;
            aes.IV = myiv;

            //init key
            aes.Key = Encoding.UTF8.GetBytes("StdBlog0StdBlog0");


            var ec = aes.CreateEncryptor();
            byte[] ori = getb(str);
            return gets(ec.TransformFinalBlock(ori, 0, ori.Length));

        }
        /*//Decrypt function only for test
                public static string Decrypt(string str)
                {
                    var aes = Aes.Create();
                    byte[] myiv = new byte[16]; for (int i = 0; i < 16; i++) myiv[i] = (byte)i;
                    aes.IV = myiv;
                    aes.Key = Encoding.UTF8.GetBytes("StdBlog0StdBlog0");
                    var dc = aes.CreateDecryptor();
                    byte[] ori = getb(str);
                    return gets(dc.TransformFinalBlock(ori, 0, ori.Length));
                }
         */
        //trans byte array into string
        static string gets(byte[] ba)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ba.Length; i++)
            {
                sb.Append(Convert.ToChar(ba[i]));
            }
            return sb.ToString();
        }
        //trans string into byte array
        static byte[] getb(string str)
        {
            List<byte> lb = new List<byte>();
            for (int i = 0; i < str.Length; i++)
            {
                lb.Add(Convert.ToByte(str[i]));
            }
            return lb.ToArray();
        }

    }


}