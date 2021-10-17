using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;

namespace Azure_Assignment1
{
    internal class ConnectionString
    {
        static string account = CloudConfigurationManager.GetSetting("StorageAccountName");
        static string key = CloudConfigurationManager.GetSetting("StorageAccountKey");

        public static CloudStorageAccount GetConnectionString()
        {
            //string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storage1479;AccountKey=wGEhYx7JEpP8jQRpAeUyHi6Yni0F9WKL520jkIpAaEWFf6njL5C8+URvvAb5ZlnnYfMHZMBKRXLK57IXV8c8QA==;EndpointSuffix=core.windows.net";

            return CloudStorageAccount.Parse(connectionString);
        }
    }
}
