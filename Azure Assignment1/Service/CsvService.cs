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
    public class CsvService
    {
        public async Task<string> UploadCsvAsync(IFormFile csvToUpload)
        {
            string csvFullPath = null;
            if (csvToUpload == null || csvToUpload.Length == 0)
            {
                return null;
            }
            try
            {
                CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("data");

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        }
                        );
                }

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(csvToUpload.FileName);
                cloudBlockBlob.Properties.ContentType = csvToUpload.ContentType;
                await cloudBlockBlob.UploadFromStreamAsync(csvToUpload.OpenReadStream());
                string strpath = Path.GetExtension(csvToUpload.FileName);

                if (strpath != ".csv")
                {
                    Console.WriteLine("error");
                }
                else
                {
                    await cloudBlockBlob.UploadFromStreamAsync(csvToUpload.OpenReadStream());
                }

                csvFullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
            return csvFullPath;
        }
    
}
}
