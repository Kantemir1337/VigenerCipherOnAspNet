using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vigener_Cipher.Models
{
    public class VigenerViewModel
    {
        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
        public string Key { get; set; }
        public bool isEncypting { get; set; }
    }
}
