using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppImageCopy
{
    [StorageAccount("BlobConnectionString")]
    public static class ImageCopyFunction
    {
       [FunctionName("ImageCopyFunction")]

        public static void Run([BlobTrigger("images/{name}", Connection = "BlobConnectionString")] Stream myBlob,
           [Blob("imagecopy/{name}", FileAccess.Write, Connection = "BlobConnectionString")] Stream anotherBlob,
           string name, ILogger log)
        {
            myBlob.CopyTo(anotherBlob);
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            //log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {anotherBlob.Length} Bytes");
        }
    }

}
