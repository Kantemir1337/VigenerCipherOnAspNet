using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vigener_Cipher.Interfaces
{
    public interface IVigener
    {
        string GetDecryptedText { get; }
        string GetEncryptedText { get; }
        string GetKey { get; }
        bool isEncrypting { get; }

        void Encrypt();
        void Decrypt();
        void Initalize(string text, string key, bool isEncrypting);
        void KeyParse();
    }
}
