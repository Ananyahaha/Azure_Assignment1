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
        //public async Task<IActionResult> ImgUpload(IFormFile files)
        //{
            //    string imageFullPath = null;
            //    string strpath=null;
            //    if (files == null || files.Length == 0)
            //    {
            //        return null;
            //    }
            //    try
            //    {
            //        string connectionString =  _configuration.GetValue<string>("ConnectionStrings: DefaultConnection");
            //        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            //        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("images");

            //        if (await cloudBlobContainer.CreateIfNotExistsAsync())
            //        {
            //            await cloudBlobContainer.SetPermissionsAsync(
            //                new BlobContainerPermissions
            //                {
            //                    PublicAccess = BlobContainerPublicAccessType.Blob
            //                }
            //                );
            //        }
            //        //string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(imageToUpload.FileName);

            //        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(files.FileName);
            //        cloudBlockBlob.Properties.ContentType = files.ContentType;
            //        await cloudBlockBlob.UploadFromStreamAsync(files.OpenReadStream());
            //        strpath = Path.GetExtension(files.FileName);

            //        if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".jfif" && strpath != ".gif" && strpath != ".png")
            //        {
            //            Console.WriteLine("error");
            //        }
            //        else
            //        {
            //            await cloudBlockBlob.UploadFromStreamAsync(files.OpenReadStream());
            //        }

            //        imageFullPath = cloudBlockBlob.Uri.ToString();
            //        bool flag = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error: '{ex}'");
            //    }
            //    return View((bool)(strpath == ".jpg") ? true : false);

            public async Task<ActionResult> ImgUpload(IFormFile photo)
            {
                var imageUrl = await imageService.UploadImageAsync(photo);
                //TempData["LatestImage"] = imageUrl.ToString();
                return RedirectToAction("ImgUpload");
            }

        }

    }



