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

        [HttpGet]
        public IActionResult CsvUpload()
        {
            return View((bool)true);
        }
        [HttpPost]
        public async Task<IActionResult> Csv(IFormFile files)
        {
            string fileExtension = System.IO.Path.GetExtension(files.FileName);

            if (fileExtension == ".csv")
            {
                string blobstorageconnection = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

                byte[] dataFiles;

                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);

                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_configuration.GetValue<string>("ConnectionStrings:blobContainerNameForCsvFile"));

                await cloudBlobContainer.CreateIfNotExistsAsync();

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                string systemFileName = files.FileName;
                await cloudBlobContainer.SetPermissionsAsync(permissions);
                await using (var target = new MemoryStream())
                {
                    files.CopyTo(target);
                    dataFiles = target.ToArray();
                }

                // This also does not make a service call; it only creates a local object.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);

                await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
            }

            return View((bool)(fileExtension == ".csv") ? true : false);
        }

    


}
}

