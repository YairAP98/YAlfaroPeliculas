using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EstadisticaController1 : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            cine.Cines = new List<object>();
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5211/");
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();
                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultCine in readTask.Result.Objects)
                    {
                        ML.Cine resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cine>(resultCine.ToString());
                        cine.Cines.Add(resultItemList);
                    }
                }
            }

            return View(cine);
        }
    }
}
