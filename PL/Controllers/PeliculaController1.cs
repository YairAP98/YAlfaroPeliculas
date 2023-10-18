using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PL.Models;

namespace PL.Controllers
{
    public class PeliculaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            Pelicula pelicula = new Pelicula();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/movie/popular");
                string url = "?api_key=d0428d9bc599ca9d2edc3141a79036c7";

                var responseTask = client.GetAsync(url);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    pelicula.Peliculas = new List<object>();
                    foreach (var resultItem in resultJson.results)
                    {
                        Pelicula peliculaItem = new Pelicula();
                        peliculaItem.IdMovie = resultItem.id;
                        peliculaItem.Descripcion = resultItem.overview;
                        peliculaItem.Nombre = resultItem.title;
                        peliculaItem.Fecha = resultItem.release_date;
                        peliculaItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;
                        pelicula.Peliculas.Add(peliculaItem);
                     }
                }
            }

            return View(pelicula);
        }

        public ActionResult Favorite(int IdMovie, bool favorite )
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://api.themoviedb.org/3/account/");

                var json = new
                {
                    media_type = "movie",
                    media_id = (int)IdMovie,
                    favorite = favorite

                };

                var postTask = client.PostAsJsonAsync("20522458/favorite?session_id=644aa977848f323a618515643275b96956f26b1c&api_key=d0428d9bc599ca9d2edc3141a79036c7", json);
                postTask.Wait();
                var resultMovie = postTask.Result;
                if (favorite)
                {
                    if (resultMovie.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Agregado a favoritos";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Eliminado de favoritos";

                    }

                }
              
                return PartialView("Modal");
            }

        }
      

        [HttpGet]
        public IActionResult GetFavorite()
        {
            Pelicula pelicula = new Pelicula();

            using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/account/20522454/favorite/movies");
                string url = "?api_key=d0428d9bc599ca9d2edc3141a79036c7&session_id=644aa977848f323a618515643275b96956f26b1c";

                var responseTask = client.GetAsync(url);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    pelicula.Peliculas = new List<object>();
                    foreach (var resultItem in resultJson.results)
                    {
                        Pelicula peliculaItem = new Pelicula();
                        peliculaItem.IdMovie = resultItem.id;
                        peliculaItem.Descripcion = resultItem.overview;
                        peliculaItem.Nombre = resultItem.title;
                        peliculaItem.Fecha = resultItem.release_date;
                        peliculaItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;
                        pelicula.Peliculas.Add(peliculaItem);
                    }
                }
            }

            return View(pelicula);
        }

    }
}
