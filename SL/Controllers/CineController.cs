using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
 
    public class CineController : Controller
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();

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

        [HttpDelete]
        [Route("Delete/{IdCine}")]
        public ActionResult Delete(int IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetById/{IdCine}")]
        public IActionResult GetById(int IdCine)
        {
            ML.Result result = BL.Cine.GetById(IdCine);

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

        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] ML.Cine cine)
        {
            var result = BL.Cine.Add(cine);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdCine}")]
        public IActionResult Update(int IdCine, [FromBody] ML.Cine cine)
        {
            var result = BL.Cine.Update(cine);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
