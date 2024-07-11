using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route( "api/hotels" )]
public class HotelsController : ControllerBase
{
    private static List<Hotel> _hotels = new();

    public HotelsController()
    {
    }

    // GET api/hotels/info
    [HttpGet, Route( "info" )]
    public string GetApiInfo()
    {
        return "This api will work with hotels";
    }

    // GET api/hotels
    // Пагинация
    [HttpGet]
    public IActionResult GetHotels()
    {
        return Ok( _hotels );
    }

    // POST api/hotels
    [HttpPost]
    public IActionResult CreateHotel( [FromBody] Hotel hotel )
    {
        _hotels.Add( hotel );

        return Ok( hotel );
    }

    // PUT api/hotels/{id}
    [HttpPut, Route( "{id:int}" )]
    public IActionResult UpdateHotel( [FromRoute] int id, /*FromBody можно не писать*/ Contracts.UpdateHotel hotel )
    {
        Hotel existingHotel = _hotels.FirstOrDefault( x => x.Id == id );
        if ( existingHotel is null )
        {
            return BadRequest( $"No such hotel with Id = {id} exists" );
        }

        existingHotel.Name = hotel.Name;
        return Ok( existingHotel );
    }

    // DELETE api/hotels/{id}
    [HttpDelete, Route( "{id:int}" )]
    public IActionResult DeleteHotel( [FromRoute] int id )
    {
        Hotel hotel = _hotels.FirstOrDefault( x => x.Id == id );
        if ( hotel is not null )
        {
            _hotels.Remove( hotel );
            return Ok();
        }

        return NotFound();
    }
}

