using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CinesController1 : Controller
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
        [HttpGet]
        public IActionResult Estadistica()
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
        [HttpPost]
        public IActionResult GetAll(ML.Cine cine)
        {
            ML.Result result = BL.Cine.GetAll();
            cine.Cines = result.Objects;

            return View(cine);
        }

        [HttpGet]
        public IActionResult Form(int? IdCine)
        {
            ML.Cine cine = new ML.Cine();
            cine.Cines = new List<object>();
            ML.Result result = new ML.Result();

            if (IdCine != null)
            {
                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri("http://localhost:5211/");
                    var responseTask = client.GetAsync("GetById/" + IdCine.Value);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Cine resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cine>(readTask.Result.Object.ToString());
                        cine = resultItemList;
                        result.Correct = true;
                    }
                }
            }

            return View(cine);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Form(ML.Cine cine)
        {

            cine.Cines = new List<object>();
            cine.Zona.Zonas = new List<object>();
            cine.Zona.Nombre = "";
            ML.Result result = new ML.Result();
            if (cine.IdCine == 0 || cine.IdCine == null)
            {

                using (var client = new HttpClient())
                {
                    var responseTask = client.PostAsJsonAsync<ML.Cine>("http://localhost:5211/", cine);
                    responseTask.Wait();

                    var resultApi = responseTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                        ViewBag.Mensaje = "Cine agregado correctamente";
                    }
                    else
                    {
                        result.Correct = false;
                        ViewBag.Mensaje = "No se pudo agregar el cine, Error en: " + result.ErrorMessage;
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var responseTask = client.PutAsJsonAsync<ML.Cine>("http://localhost:5211/" + cine.IdCine ,cine);
                    responseTask.Wait();

                    var resultApi = responseTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                        ViewBag.Mensaje = "Cine actualizado correctamente";
                    }
                    else
                    {
                        result.Correct = false;
                        ViewBag.Mensaje = "No se pudo actualizar el cine, Error en: " + result.ErrorMessage;
                    }
                }
            }


            return PartialView("Modal");


        }

        [HttpGet]
        public ActionResult Delete(int IdCine)
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {


                var responseTask = client.DeleteAsync("http://localhost:5211/Delete/" + IdCine);
                responseTask.Wait();
                var resultApi = responseTask.Result;
                if (resultApi.IsSuccessStatusCode)
                {
                    result.Correct = true;
                }
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Cine eliminado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "No se logro eliminar el cine: " + result.ErrorMessage;
                }

                return PartialView("Modal");
            }
        }
    }
}
