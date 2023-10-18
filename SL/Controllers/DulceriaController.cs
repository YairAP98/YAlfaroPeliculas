using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class DulceriaController : Controller
    {
        [HttpGet]
        [Route("GetAllDulceria")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Dulceria.GetAll();

            if (result.Correct)
            {
                return Ok(result);
                //  return StatusCode(200,result);

            }
            else
            {
                return NotFound();
            }


        }
        [HttpGet]
        [Route("GetById/{IdDulce}")]
        public IActionResult GetById(int IdDulce)
        {
            ML.Result result = BL.Cine.GetById(IdDulce);

            if (result.Correct)
            {
                return Ok(result);
            }
            else if (result.ErrorMessage == "Not Found") // Puedes personalizar el mensaje de error según tus necesidades.
            {
                return NotFound();
            }
            else
            {
                return StatusCode(400, result);
            }
        }
    }
}
