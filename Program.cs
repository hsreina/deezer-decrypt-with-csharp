using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1 {

    class Program {

        static void Main(string[] args) {

            // Your application secret from you Deezer developper page.
            string app_secret = "your_app_secret";

            // Supposed to be hex string like (6acbf466190f1dba4b8829a907027bad18610845fffb7152c723f1d98a61646a2fd951832a8b90ac5b405e2632c397d4c453dc88b1593955a8db0a5851ce314fa931210b3afedd8d47aac6ee07691f2c3366ae67378f74183a8b1ce87e50caae8e60dbeec1a78ab8f6eb3fcc9f41d86703e85189d66b3a7ad6d78470d5a22fd8cc0bc689939a3f86a8328ab63b27b47d1bc131b705e3b0cf6606458b6fd3f65310c9928e1bf20bd08b0e29f0837e3f2f23c1fa1928aa187bfdf93c9121ed9900f2f6f761f7b62e46c664d37e6590427e)
            string toDecryptStr = "you_url_to_decrypt"; 

            string result = "";

            try {
                result = Deezer.CryptApi.DecryptUri(toDecryptStr, app_secret);
            } catch (Exception e) {
                result = "Unable to decrypt! You should check your app_secret";
            }

            Console.WriteLine("decrypted : " + result);

            Console.WriteLine("Press a key for continue.");
            Console.ReadKey();
        }
    }
}
