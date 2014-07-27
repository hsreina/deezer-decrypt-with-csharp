using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Deezer {

    public class CryptApi {

        /// <summary>
        /// Convetr a string to Byte Array (copied from internet)
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] StringToByteArray(string hex) {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Implementation of Deezer DecryptUri
        /// </summary>
        /// <param name="url">the encryoted Uri</param>
        /// <param name="app_secret">The developper app secret</param>
        /// <returns></returns>
        public static string DecryptUri(String url, string app_secret) {

            string result = "";

            // Not usefull to decrypt empty stuff o_O
            if (url.Length == 0 || app_secret.Length == 0) {
                return result;
            }

            Encoding byteEncoder = Encoding.UTF8;

            // Rijndael my friend :)
            RijndaelManaged rijn = new RijndaelManaged();

            // Same as Deezer ECB mode
            rijn.Mode = CipherMode.ECB;

            // No padding fix
            rijn.Padding = PaddingMode.Zeros; 

            byte[] toDecrypt = StringToByteArray(url);

            byte[] key = byteEncoder.GetBytes(app_secret);

            // Deezer don't use IV, set initial matching 128bits block size as NULL
            byte[] InitialVector = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 

            using (MemoryStream streamIn = new MemoryStream(toDecrypt)) {
                using (ICryptoTransform transform = rijn.CreateDecryptor(key, InitialVector)) {
                    using (CryptoStream cryptoStream = new CryptoStream(streamIn, transform, CryptoStreamMode.Read)) {
                        using (StreamReader streamOut = new StreamReader(cryptoStream)) {
                            result = streamOut.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }

    }

}
