using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure_Assignment1.Controllers
{
    public class CsvController : Controller
    {
        private readonly IConfiguration _configuration;
        public CsvController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        CsvService csvService = new CsvService();


        [HttpGet]
        public IActionResult CsvUpload()
        {
            return View((bool)true);
        }
        [HttpPost]
        public async Task<ActionResult> CsvUpload(IFormFile photo)
        {
            var csvUrl = await csvService.UploadCsvAsync(photo);
            return RedirectToAction("CsvUpload");
        }


    }
}
