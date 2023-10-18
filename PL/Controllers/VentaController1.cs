using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaController1 : Controller
    {
        public ActionResult GetAll()
        {
            ML.Dulceria dulceria = new ML.Dulceria();
           
            ML.Result result = BL.Dulceria.GetAll();
            dulceria.Dulces = result.Objects;
            return View(dulceria);
        }
        public ActionResult AddCarrito(int IdDulce)
        {
            bool existe = false;
            ML.Venta venta = new ML.Venta();
            venta.Carrito = new List<object>();
            ML.Result result = BL.Dulceria.GetById(IdDulce);
            if (HttpContext.Session.GetString("Carrito") == null)
            {

                if (result.Correct)
                {
                    ML.Dulceria dulceria = (ML.Dulceria)result.Object;
                    dulceria.Cantidad = 1;
                    venta.Carrito.Add(dulceria);
                    //serializar carrito
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(venta.Carrito));
                }

            }
            else
            {

                ML.Dulceria producto = (ML.Dulceria)result.Object;
                GetCarrito(venta); //ya recupere el carrito
                foreach (ML.Dulceria producto1 in venta.Carrito)
                {
                    if (producto.IdDulce == producto1.IdDulce)
                    {
                        producto1.Cantidad += 1;
                        existe = true;
                        break;
                    }
                    else
                    {
                        existe = false;
                    }
                }
                if (existe == true)
                {
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(venta.Carrito));
                }
                else
                {
                    producto.Cantidad = 1;
                    venta.Carrito.Add(producto);
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(venta.Carrito));
                }

            }

            return RedirectToAction("GetAll","DulcesController1");
        }
        public ML.Venta GetCarrito(ML.Venta venta)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito"));

            foreach (var obj in ventaSession)
            {
                ML.Dulceria objMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Dulceria>(obj.ToString());
                venta.Carrito.Add(objMateria);
            }
            return venta;
        }
        public ActionResult Carrito()
        {
            ML.Venta venta = new ML.Venta();
            venta.Carrito = new List<object>();
            if (HttpContext.Session.GetString("Carrito") == null)
            {
                return View(venta);
            }
            else
            {
                GetCarrito(venta);
                return View(venta);
            }

        }
    }
}
