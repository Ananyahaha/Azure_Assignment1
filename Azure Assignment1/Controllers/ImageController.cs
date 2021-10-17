using Azure_Assignment1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_Assignment1.Controllers
{
    public class ImageController : Controller
    {
        private readonly IConfiguration _configuration;
        ImageService imageService = new ImageService();


        public object ConnectionString { get; private set; }

        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ImgUpload()
        {
            return View((bool)true);
        }
        [HttpPost]
        
            public async Task<ActionResult> ImgUpload(IFormFile photo)
            {
                var imageUrl = await imageService.UploadImageAsync(photo);
                //TempData["LatestImage"] = imageUrl.ToString();
                return RedirectToAction("ImgUpload");
            }

        }

    }



