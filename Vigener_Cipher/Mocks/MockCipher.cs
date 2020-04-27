using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vigener_Cipher.Interfaces;
using Vigener_Cipher.Models;

namespace Vigener_Cipher.Mocks
{
    public class MockCipher : IVigener
    {
        static Dictionary<string, int> alphabet { get; } = new Dictionary<string, int>
        {
            { "а", 0 },
            { "б", 1 },
            { "в", 2 },
            { "г", 3 },
            { "д", 4 },
            { "е", 5 },
            { "ё", 6 },
            { "ж", 7 },
            { "з", 8 },
            { "и", 9 },
            { "й", 10 },
            { "к", 11 },
            { "л", 12 },
            { "м", 13 },
            { "н", 14 },
            { "о", 15 },
            { "п", 16 },
            { "р", 17 },
            { "с", 18 },
            { "т", 19 },
            { "у", 20 },
            { "ф", 21 },
            { "х", 22 },
            { "ц", 23 },
            { "ч", 24 },
            { "ш", 25 },
            { "щ", 26 },
            { "ъ", 27 },
            { "ы", 28 },
            { "ь", 29 },
            { "э", 30 },
            { "ю", 31 },
            { "я", 32 }
        };
        static string alphabetKeys { get; } = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        static int startROT { get; } = 0; // Vigenere square starts with ROT0 ('а' becomes 'а')

        public Vigener vigener;

        public string GetDecryptedText { get { return vigener.DecText; } }
        public string GetEncryptedText { get { return vigener.EncText; } }
        public string GetKey { get { return vigener.Key; } }
        public bool isEncrypting { get { return vigener.isEncrypting; } }


        public void Initalize(string text, string key, bool isEncrypting)
        {
            vigener = new Vigener();

            vigener.Key = key;
            KeyParse();

            if (isEncrypting)
            {
                vigener.DecText = text;
                Encrypt();
            }
            else
            {
                vigener.EncText = text;
                Decrypt();
            }

            vigener.isEncrypting = isEncrypting;
        }

        public void Decrypt()
        {
            int i = 0; // iterator for text
            int rot = 0; // iterator for Key rotation
            while (vigener.EncText.Length > i)
            {
                string currentChar = vigener.EncText[i].ToString().ToLower();

                if (!alphabet.ContainsKey(currentChar))
                {
                    vigener.DecText += vigener.EncText[i].ToString();
                    i++;
                    continue;
                }


                if (rot == vigener.Key.Length)
                    rot = 0;

                int selfRotation = alphabet[currentChar];
                int fullRotation = selfRotation - vigener.KeyRotation[rot] - startROT;
                fullRotation = fullRotation < 0 ? fullRotation + alphabet.Count : fullRotation;

                char decChar = alphabetKeys[fullRotation];

                decChar = Char.IsUpper(vigener.EncText[i]) ? Char.ToUpper(decChar) : Char.ToLower(decChar);

                vigener.DecText += decChar.ToString();

                i++;
                rot++;
            }
        }

        public void Encrypt()
        {
            int i = 0; // iterator for text
            int rot = 0; // iterator for Key rotation
            while (vigener.DecText.Length > i)
            {
                string currentChar = vigener.DecText[i].ToString();

                if (!alphabet.Keys.Contains(vigener.DecText[i].ToString()))
                {
                    vigener.EncText += vigener.DecText[i].ToString();
                    i++;
                    continue;
                }

                if (rot == vigener.Key.Length)
                    rot = 0;

                int selfRotation = alphabet[currentChar];
                int fullRotation = selfRotation + vigener.KeyRotation[rot] + startROT;
                fullRotation = fullRotation >= alphabet.Count ? fullRotation - alphabet.Count : fullRotation;

                char encChar = alphabetKeys[fullRotation];

                encChar = Char.IsUpper(vigener.DecText[i]) ? Char.ToUpper(encChar) : Char.ToLower(encChar);

                vigener.EncText += encChar.ToString();

                i++;
                rot++;
            }
        }

        public void KeyParse()
        {
            string newKey = vigener.Key;
            List<int> indexWhereIsNotLetter = new List<int>();

            for (int i = 0; i < vigener.Key.Length; i++)
            {
                bool flag = false;
                foreach (var c in alphabet)
                {
                    if (vigener.Key[i].ToString() == c.Key)
                    {
                        vigener.KeyRotation.Add(c.Value);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    indexWhereIsNotLetter.Add(i);
            }
            
            foreach (var item in indexWhereIsNotLetter)
            {
                newKey = newKey.Substring(0, item) + newKey.Substring(item + 1);
            }

            vigener.Key = newKey;
        }
    }
}
