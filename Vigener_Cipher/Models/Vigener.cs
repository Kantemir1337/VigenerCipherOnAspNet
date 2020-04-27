using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vigener_Cipher.Models
{
    public class Vigener
    {
        public string EncText { get; set; }
        public string DecText { get; set; }
        public string Key { get; set; }
        public bool isEncrypting { get; set; }
        public List<int> KeyRotation { get; set; } = new List<int>();
    }
}
