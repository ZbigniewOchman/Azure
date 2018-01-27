
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class SaveAnOrder
    {
        [FunctionName("SaveAnOrder")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            PhotoOrder data;
            string requestBody = new StreamReader(req.Body).ReadToEnd();

            data = JsonConvert.DeserializeObject<PhotoOrder>(requestBody);
            return (ActionResult)new OkObjectResult("OrderProcessed");
        }
    }
    public class PhotoOrder
    {
        public string CustomerEmail { get; set; }
        public string FileName { get; set; }
        public string RequiredHeight { get; set; }
        public string RequiredWidth { get; set; }

    }
}
