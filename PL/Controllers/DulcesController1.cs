using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace PL.Controllers
{
    public class DulcesController1 : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Dulceria dulceria = new ML.Dulceria();
            dulceria.Dulces = new List<object>();
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5211/");
                var responseTask = client.GetAsync("GetAllDulceria");
                responseTask.Wait();
                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultCine in readTask.Result.Objects)
                    {
                        ML.Dulceria resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Dulceria>(resultCine.ToString());
                        dulceria.Dulces.Add(resultItemList);
                    }
                }
            }

            return View(dulceria);
        }
   
    }
}
