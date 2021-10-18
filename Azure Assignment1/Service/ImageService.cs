using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_Assignment1
{
    public class ImageService
    {
        public async Task<string> UploadImageAsync(IFormFile imageToUpload)
        {
            string imageFullPath = null;
            if (imageToUpload == null || imageToUpload.Length == 0)
            {
                return null;
            }
            try
            {
                CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("images");

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        }
                        );
                }
                //string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(imageToUpload.FileName);

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageToUpload.FileName);
                cloudBlockBlob.Properties.ContentType = imageToUpload.ContentType;
                //await cloudBlockBlob.UploadFromStreamAsync(imageToUpload.OpenReadStream());
                string strpath = Path.GetExtension(imageToUpload.FileName);

                if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".jfif" && strpath != ".gif" && strpath != ".png")
                {
                    Console.WriteLine("error");
                }
                else
                {
                    await cloudBlockBlob.UploadFromStreamAsync(imageToUpload.OpenReadStream());
                }

                imageFullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
            return imageFullPath;
        }
    }
}
