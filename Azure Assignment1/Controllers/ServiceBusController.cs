using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Azure_Assignment1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Azure_Assignment1.Controllers
{
    public class ServiceBusController : Controller
    {

        private readonly IConfiguration _configuration;
        public ServiceBusController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string messageToSend)
        {
            IQueueClient queueClient = new QueueClient(_configuration.GetValue<string>("ConnectionStrings:ServiceBusConnectionString"), _configuration.GetValue<string>("ConnectionStrings:QueueName"));

            var messageJSON = JsonConvert.SerializeObject(messageToSend);

            var queueMessage = new Microsoft.Azure.ServiceBus.Message(Encoding.UTF8.GetBytes(messageJSON))
            {

                MessageId = Guid.NewGuid().ToString(),
                ContentType = "application/json"
            };

            await queueClient.SendAsync(queueMessage);

            return View();
        }



    }
}
