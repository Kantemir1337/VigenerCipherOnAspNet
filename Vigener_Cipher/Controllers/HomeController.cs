using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vigener_Cipher.Interfaces;
using Vigener_Cipher.Models;

namespace Vigener_Cipher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly IVigener _crypto;

        private static readonly string DefaultKey = "скорпион";

        VigenerViewModel vigener = new VigenerViewModel();

        public HomeController(ILogger<HomeController> logger, IVigener crypto)
        {
            _logger = logger;
            _crypto = crypto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(bool isEncrypting, string key, string text)
        {
            if (key == null || key == "")
                _crypto.Initalize(text, DefaultKey, isEncrypting);
            else
                _crypto.Initalize(text, key, isEncrypting);

            vigener.DecryptedText = _crypto.GetDecryptedText;
            vigener.EncryptedText = _crypto.GetEncryptedText;
            vigener.Key = _crypto.GetKey;

            return View("Result", model: vigener);

        }
        
        public IActionResult Result()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
