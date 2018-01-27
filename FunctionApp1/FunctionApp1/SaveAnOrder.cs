
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace FunctionApp1
{
    public static class SaveAnOrder
    {
        [FunctionName("SaveAnOrder")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            TraceWriter log,
            [Table("Orders", Connection = "StorageConnection")] ICollector<PhotoOrder> ordersTable
            )
        {
            PhotoOrder data;
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            data = JsonConvert.DeserializeObject<PhotoOrder>(requestBody);
            data.PartitionKey = DateTime.Now.DayOfYear.ToString();
            data.RowKey = data.FileName;
            ordersTable.Add(data);

            return (ActionResult)new OkObjectResult("OrderProcessed");
        }
    }
    public class PhotoOrder : TableEntity
    {
        public string CustomerEmail { get; set; }
        public string FileName { get; set; }
        public string RequiredHeight { get; set; }
        public string RequiredWidth { get; set; }

    }
}
