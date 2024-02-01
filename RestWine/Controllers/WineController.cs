using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WineLib.Models;
using WineLib.Repository;

namespace RestWine.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]

    public class WineController : ControllerBase
    {

        WineRepository _wineRepository = new WineRepository();
        public WineController(WineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        // GET: api/<WineController>
        [HttpGet]
        public IEnumerable<Wine> Get()
        {
            return _wineRepository.GetAll();
        }


        // GET(int id): api/<WineController>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]         //Disse statuskoder har noget at gøre med beskeder fra SWAGGER
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Wine> Get(int id)
        {

            if (id < 0 )
            {
                return BadRequest($"CODE 400:\nID must be a non-negative integer!");
            }

            Wine wine = _wineRepository.GetById(id);

            if (wine == null)
            {
                return NotFound($"CODE 404:\nID: {id} NOT FOUND!");
            }
            return Ok(wine);
        }


        // POST: api/<WineController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[EnableCors("MyAllowedOrigin")]
        public ActionResult<Wine> Post([FromBody] Wine value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid input data");
                }

                _wineRepository.Add(value);
                //return CreatedAtAction("Get", new { id = value.Id }, value);
                return Created($"/api/{value.Id}", value);     // Her sættes den direkte URL
            }
            catch (Exception ex)
            {
                return BadRequest($"CODE 400:\n{ex.Message}");
            }
        }

        // PUT api/<WineController>/
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[EnableCors("MyAllowedOrigin")]
        public ActionResult<Wine> Put(int id, [FromBody] Wine value)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest($"CODE 400:\nID must be a non-negative integer!");
                }

                Wine updatedWine = _wineRepository.Update(id, value);

                if (updatedWine == null)
                {
                    return NotFound($"CODE 404:\nID: {id} NOT FOUND!");
                }

                return Ok(updatedWine);
            }
            catch (Exception ex)
            {
                return BadRequest($"CODE 400:\n{ex.Message}");
            }
        }

        // DELETE api/<WineController>/
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[EnableCors("MyAllowedOrigin")]
        public ActionResult<Wine> Delete(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest($"CODE 400:\nID must be a non-negative integer!");
                }

                Wine deletedWine = _wineRepository.Remove(id);

                if (deletedWine == null)
                {
                    return NotFound($"CODE: 404:\nID: {id} NOT FOUND!");
                }

                return Ok(deletedWine);
            }
            catch (Exception ex)
            {
                return BadRequest($"CODE 400:\n{ex.Message}");
            }
        }
    }
}
